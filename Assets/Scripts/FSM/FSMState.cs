using System;
using System.Threading;
using Control;
using Cysharp.Threading.Tasks;

namespace FSM
{
    public abstract class FSMState : IDisposable
    {
        /// <summary> Вход в состояние </summary>
        public abstract UniTask<(Type, InputData)> Execute(CancellationToken token = default);

        /// <summary> Получить ввод </summary>
        /// <param name="data"> Данные ввода </param>
        public abstract void HandleInput(InputData data);

        /// <summary> Попытаться выйти в другое состояние </summary>
        /// <param name="nextState"> Следующее состояние </param>
        public abstract void TryInterrupt(Type nextState);

        /// <summary> Сброс данных </summary>
        public virtual void Dispose()
        {
        }

    }
}