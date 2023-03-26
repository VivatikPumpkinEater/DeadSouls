using System;
using System.Threading;
using Animations;
using Control;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using FSM;
using Unity.Mathematics;
using UnityEngine;

namespace Character.FSM
{
    /// <summary> Стэйт движения а так же покоя </summary>
    public class MovementState : FSMState, IFixedUpdateListener, IUpdateListener
    {
        private readonly AnimationController _animationController;
        private readonly BodyController _bodyController;
        
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

        public MovementState(AnimationController animationController, BodyController bodyController)
        {
            _animationController = animationController;
            _bodyController = bodyController;
        }
        
        public override async UniTask<(Type, InputData)> Execute(CancellationToken token = default)
        {
            _tcs = new();
            token.Register(() => _tcs.TrySetCanceled());
            
            //TODO костыль, анимансер не успевает проиниться
            await UniTask.DelayFrame(1, cancellationToken: token);
            _animationController.PlayMovementAnimation();
            
            var result = await _tcs.Task;

            //Выход из стейта, сбрасываем все данные

            _animationController.EnableShieldAnimation(false, 0f);

            // _direction = null;
            _magnitude = 0.0f;

            return result;
        }
        
        public void FixedUpdate(float fixedDeltaTime)
        {
            Move(fixedDeltaTime);
            _bodyController.LookAtMovementDirection(_movementVector);
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
            _magnitude = Mathf.Lerp(_magnitude, Mathf.Abs(_movementVector.x),
                deltaTime * MovementSpeedSettings.MovementSpeedLerp);
            
            Speed = MovementSpeedSettings.MovementEase.Evaluate(_magnitude);
        }

        /// <summary> Переместить перса </summary>
        private void Move(float fixedDeltaTime)
        {
            var velocity = _movementVector.normalized * Speed * fixedDeltaTime * 50;

            _bodyController.ChangeVelocity(velocity);
        }

        public override void HandleInput(InputData data)
        {
            switch (data.State)
            {
                case InputState.RollClick:
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
                    _movementVector = Vector3.zero;
                    break;
            }

            switch (data.State)
            {
                case InputState.BlockStart:
                    _animationController.EnableShieldAnimation(true);
                    break;
                case InputState.BlockEnd:
                    _animationController.EnableShieldAnimation(false);
                    break;
            }
        }

        public override void TryInterrupt(Type nextState)
        {
            _tcs.TrySetResult((nextState, default));
        }
    }
}
