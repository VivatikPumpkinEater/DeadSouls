using System;
using System.Threading;
using Control;
using Cysharp.Threading.Tasks;
using FSM;
using UnityEngine;

namespace Character.FSM
{
    public class JumpState : FSMState, IFixedUpdateListener, IUpdateListener
    {
        private readonly Rigidbody _rigidbody;
        private UniTaskCompletionSource<(Type, InputData)> _tcs = new();

        private Vector3 _movementVector;
        private float _speed;
        private float _magnitude;
        
        private float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public JumpState(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public override async UniTask<(Type, InputData)> Execute(CancellationToken token = default)
        {
            _tcs = new();
            token.Register(() => _tcs.TrySetCanceled());

            _rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);

            var result = await _tcs.Task;

            //Выход из стейта, сбрасываем все данные

            
            
            return result;
        }

        public override void HandleInput(InputData data)
        {
            if (data.State == InputState.Hold)
            {
                _movementVector = data.StartTouch;
            }
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            Move(fixedDeltaTime);
        }

        public void Update(float deltaTime)
        {
            CalculateSpeed(deltaTime);
        }
        
        private void Move(float fixedDeltaTime)
        {
            var velocity = _movementVector.normalized * Speed * fixedDeltaTime * 50;
            velocity.y = _rigidbody.velocity.y - 0.1f;

            _rigidbody.velocity = velocity;
        }
        
        private void CalculateSpeed(float deltaTime)
        {
            _magnitude = Mathf.Lerp(_magnitude, _movementVector.magnitude,
                deltaTime * MovementSpeedSettings.MovementSpeedLerp);
            
            Speed = MovementSpeedSettings.MovementEase.Evaluate(_magnitude);
        }
        
        public override void TryInterrupt(Type nextState)
        {
            _tcs.TrySetResult((nextState, default));
        }
    }
}