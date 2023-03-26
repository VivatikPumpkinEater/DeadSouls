using System;
using System.Threading;
using Animations;
using Control;
using Cysharp.Threading.Tasks;
using FSM;
using UnityEngine;

namespace Character.FSM
{
    public class JumpState : FSMState, IFixedUpdateListener, IUpdateListener
    {
        private readonly AnimationController _animationController;
        private readonly BodyController _bodyController;
        private UniTaskCompletionSource<(Type, InputData)> _tcs = new();

        private Vector3 _movementVector;
        private float _speed;
        private float _magnitude;
        
        private float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public JumpState(AnimationController animationController, BodyController bodyController)
        {
            _animationController = animationController;
            _bodyController = bodyController;
        }

        public override async UniTask<(Type, InputData)> Execute(CancellationToken token = default)
        {
            _tcs = new();
            token.Register(() => _tcs.TrySetCanceled());

            _bodyController.AddForce(Vector3.up * 5f);
            
            var result = await _tcs.Task;

            //Выход из стейта, сбрасываем все данные
            
            return result;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            Move(fixedDeltaTime);
            
            if (IsGround())
                TryInterrupt(typeof(MovementState));
        }

        public void Update(float deltaTime)
        {
            CalculateSpeed(deltaTime);
        }
        
        private void Move(float fixedDeltaTime)
        {
            var velocity = _movementVector.normalized * Speed * fixedDeltaTime * 50;

            _bodyController.ChangeVelocity(velocity);
        }
        
        private void CalculateSpeed(float deltaTime)
        {
            _magnitude = Mathf.Lerp(_magnitude, _movementVector.magnitude,
                deltaTime * 1);
            
            Speed = MovementSpeedSettings.MovementEase.Evaluate(_magnitude);
        }

        private bool IsGround()
        {
            return Physics.Raycast(_bodyController.Position, Vector3.down, 0.1f);
        }
        
        public override void HandleInput(InputData data)
        {
            if (data.State is InputState.Hold or InputState.UnPress)
            {
                _movementVector = data.StartTouch;
            }
        }
        
        public override void TryInterrupt(Type nextState)
        {
            _tcs.TrySetResult((nextState, default));
        }
    }
}