using System;
using System.Collections.Generic;
using System.Threading;
using Control;
using Cysharp.Threading.Tasks;

namespace FSM
{
    /// <summary> Прослушиватель стейтов </summary>
    public interface IStateObserver
    {
        /// <summary> Подписатся на начало стейта </summary>
        /// <param name="owner">Источник откуда идет событие</param>
        /// <param name="action">Событие</param>
        /// <typeparam name="T">Тип стейта</typeparam>
        void SubscribeRunState<T>(object owner, Action<T> action) where T : FSMState;

        /// <summary> Подписатся на конец стейта </summary>
        /// <param name="owner">Источник откуда идет событие</param>
        /// <param name="action">Событие</param>
        /// <typeparam name="T">Тип стейта</typeparam>
        void SubscribeCompleteState<T>(object owner, Action<T> action) where T : FSMState;

        /// <summary> Отписаться от начала стейта </summary>
        /// <param name="owner">Источник откуда идет событие</param>
        /// <typeparam name="T">Тип стейта</typeparam>
        void UnsubscribeRunState<T>(object owner) where T : FSMState;

        /// <summary> Отписаться от конца стейта </summary>
        /// <param name="owner">Источник откуда идет событие</param>
        /// <typeparam name="T">Тип стейта</typeparam>
        void UnsubscribeCompleteState<T>(object owner) where T : FSMState;
    }
    
    public class FSMController : IStateObserver
    {
        private Dictionary<Type, FSMState> _states = new();
        
        private readonly Dictionary<Type, Dictionary<object, Action<FSMState>>> _stateRunListeners = new();
        private readonly Dictionary<Type, Dictionary<object, Action<FSMState>>> _stateCompleteListeners = new();


        private CancellationTokenSource _cts;
        private FSMState _defaultState;
        
        public FSMState CurrentState { get; private set; }

        public void RegisterState(FSMState state)
        {
            _states[state.GetType()] = state;
        }

        public void SetDefaultState(FSMState state)
        {
            _defaultState = state;
        }
        
        /// <summary> Запустить стейт машину </summary>
        public void Run()
        {
            _cts = new CancellationTokenSource();

            var token = _cts.Token;

            UniTask.RunOnThreadPool(async () =>
            {
                var next = _defaultState.GetType();
                InputData inputData = null;

                while (!token.IsCancellationRequested)
                {
                    CurrentState = !_states.ContainsKey(next) ? _defaultState : _states[next];

                    if (inputData != null)
                        CurrentState.HandleInput(inputData);

                    var currentType = CurrentState.GetType();

                    if (_stateRunListeners.ContainsKey(currentType))
                    {
                        foreach (var action in _stateRunListeners[currentType].Values)
                            action.Invoke(CurrentState);
                    }

                    (next, inputData) = await CurrentState.Execute(token);

                    if (_stateCompleteListeners.ContainsKey(currentType))
                    {
                        foreach (var action in _stateCompleteListeners[currentType].Values)
                            action.Invoke(CurrentState);
                    }
                }
            }, cancellationToken: token).Forget(); //TODO
        }

        /// <summary> Фиксированный апдейт стейт машины </summary>
        public void FixedUpdate(float fixedDeltaTime) => (CurrentState as IFixedUpdateListener)?.FixedUpdate(fixedDeltaTime);

        /// <summary> Апдрейт каждый кадр  стейт машины </summary>
        /// <param name="deltaTime">Разница времени между кадрами</param>
        public void Update(float deltaTime) => (CurrentState as IUpdateListener)?.Update(deltaTime);

        /// <summary> Апдейт после кадра </summary>
        public void LateUpdate() => (CurrentState as ILateUpdateListener)?.LateUpdate();

        /// <summary> Посрединк для передачи инпута </summary>
        public void HandleInput(InputData data)
        {
            CurrentState.HandleInput(data);
        }

        
        public T GetState<T>() where T : FSMState
        {
            var type = typeof(T);
            if (!_states.ContainsKey(type))
                return default;

            return _states[type] as T;
        }
        
        /// <summary> Принудительно сменить состояние </summary>
        /// <param name="data">Требуемые данные. Нужно кастить в стейте</param>
        /// <typeparam name="T">На какое состояние менять</typeparam>
        public void ForceSetState<T>(object data = null)
        {
            var type = typeof(T);

            if (data != null)
            {
                var state = !_states.ContainsKey(type) ? null : _states[type];
                (state as IAssignedData)?.SetData(data);
            }

            if (data != null)
            {
                var state = !_states.ContainsKey(type) ? null : _states[type];
                (state as IAssignedData)?.SetData(data);
            }

            CurrentState.TryInterrupt(typeof(T));
        }

        /// <inheritdoc />
        public void SubscribeRunState<T>(object owner, Action<T> action) where T : FSMState
        {
            var type = typeof(T);

            if (!_stateRunListeners.ContainsKey(type))
                _stateRunListeners[type] = new Dictionary<object, Action<FSMState>>();

            if (!_stateRunListeners[type].ContainsKey(owner))
                _stateRunListeners[type][owner] = state => action.Invoke(state as T);
        }

        /// <inheritdoc />
        public void SubscribeCompleteState<T>(object owner, Action<T> action) where T : FSMState
        {
            var type = typeof(T);

            if (!_stateCompleteListeners.ContainsKey(type))
                _stateCompleteListeners[type] = new Dictionary<object, Action<FSMState>>();

            if (!_stateCompleteListeners[type].ContainsKey(owner))
                _stateCompleteListeners[type][owner] = (state) => action.Invoke(state as T);
        }

        /// <inheritdoc />
        public void UnsubscribeRunState<T>(object owner) where T : FSMState
        {
            var type = typeof(T);

            if (!_stateRunListeners.ContainsKey(type))
                return;

            _stateRunListeners[type].Remove(owner);
        }

        /// <inheritdoc />
        public void UnsubscribeCompleteState<T>(object owner) where T : FSMState
        {
            var type = typeof(T);

            if (!_stateCompleteListeners.ContainsKey(type))
                return;

            _stateCompleteListeners[type].Remove(owner);
        }

        /// <summary> Завершить работу стейт машины </summary>
        public void Stop()
        {
            _cts?.Cancel();
            _cts = null;
        }

        public void Dispose()
        {
            Stop();

            foreach (var state in _states.Values)
                state.Dispose();

            _states.Clear();
            _stateCompleteListeners.Clear();
            _stateRunListeners.Clear();
        }

    }
}
