using System;
using System.Threading;
using Animations;
using Control;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using FSM;
using UnityEngine;

namespace Character.FSM
{
    public class MovementState : FSMState, IFixedUpdateListener, IUpdateListener
    {
        private readonly AnimationController _animationController;
        private readonly Rigidbody _rigidbody;
        
        private UniTaskCompletionSource<(Type, InputData)> _tcs = new();
        
        private Tween _resetSpeedTween;

        private Vector3 _movementVector;

        private float _speed;
        private float _magnitude;
        
        /// <summary> Скорость перемещения перса </summary>
        private float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                _animationController.MovementBlendRate = value;
            }
        }

        public MovementState(AnimationController animationController, Rigidbody rigidbody)
        {
            _animationController = animationController;
            _rigidbody = rigidbody;
        }
        
        public void FixedUpdate(float fixedDeltaTime)
        {
            Move(fixedDeltaTime);
        }

        public void Update(float deltaTime)
        {
            if (_tcs.Task.Status != UniTaskStatus.Pending)
                return;

            // var direction = GetDirection(_searchController.IsContainsTarget);
            // if (direction != _direction)
            //     ChangeDirection(direction);

            CalculateSpeed(deltaTime);

            // _searchController.Search();
        }
        
        /// <summary> Расчитать скорость перемещение перса </summary>
        private void CalculateSpeed(float deltaTime)
        {
            _magnitude = Mathf.Lerp(_magnitude, _movementVector.magnitude,
                deltaTime * MovementSpeedSettings.MovementSpeedLerp);
            
            Speed = MovementSpeedSettings.MovementEase.Evaluate(_magnitude);
        }

        /// <summary> Переместить перса </summary>
        private void Move(float fixedDeltaTime)
        {
            var velocity = _movementVector.normalized * Speed * fixedDeltaTime * 50;
            velocity.y = _rigidbody.velocity.y;

            _rigidbody.velocity = velocity;
        }

        
        public override async UniTask<(Type, InputData)> Execute(CancellationToken token = default)
        {
            _tcs = new();
            token.Register(() => _tcs.TrySetCanceled());
            
            _resetSpeedTween?.Kill();

            var result = await _tcs.Task;

            //Выход из стейта, сбрасываем все данные
            await StopAnimation();

            // _direction = null;
            _magnitude = 0.0f;

            return result;
        }

        private async UniTask StopAnimation()
        {
            var state = _animationController.PlayStopAnimation();
            
            _resetSpeedTween = DOTween.Sequence()
                .Append(DOTween.To(() => Speed, x => { Speed = x; }, 0.0f,
                    state.Duration/2f)
                .SetEase(Ease.Linear)
                .SetUpdate(UpdateType.Manual)
                .OnUpdate(() => Move(Time.fixedDeltaTime)));

            await UniTask.WaitWhile(() => state.NormalizedTime < 0.99f);
        }

        public override void HandleInput(InputData data)
        {
            switch (data.State)
            {
                case InputState.Swipe:
                    _tcs.TrySetResult((typeof(RollState), data));
                    break;
                case InputState.FastAttack:
                    _tcs.TrySetResult((typeof(AttackState), data));
                    break;
                case InputState.HeavyAttack:
                    _tcs.TrySetResult((typeof(AttackState), data));
                    break;
                case InputState.Hold:
                    _movementVector = data.StartTouch;
                    break;
                case InputState.UnPress:
                    _tcs.TrySetResult((typeof(IdleState), data));
                    break;

            }
        }

        public override void TryInterrupt(Type nextState)
        {
            _tcs.TrySetResult((nextState, default));
        }
    }
}
