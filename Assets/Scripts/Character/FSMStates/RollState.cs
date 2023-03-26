using System;
using System.Threading;
using Animancer;
using Animations;
using Control;
using Cysharp.Threading.Tasks;
using FSM;
using UnityEngine;

namespace Character.FSM
{
    public class RollState : FSMState, IFixedUpdateListener
    {
       /// <summary> Ключ начала движения в кувырке </summary>
        private const string StartMovementKey = "StartMovement";

        /// <summary> Ключ окончания движения в кувырке </summary>
        private const string EndMovementKey = "EndMovement";

        // private readonly StaminaController _staminaController;
        // private readonly CharacterView _view;
        private readonly AnimationController _animationController;

        private readonly BodyController _bodyController;
        // private readonly SearchController _searchController;
        // private readonly CollisionTriggerController _collisionTriggerController;

        private AnimancerState _state;

        private (Type, InputData) _result;
        private Vector3 _direction;
        private Vector3 _position;

        public RollState
        (
            // StaminaController staminaController,
            // CharacterView view,
            AnimationController animationController,
            BodyController bodyController
            // SearchController searchController,
            // CollisionTriggerController collisionTriggerController
        )
        {
            // _staminaController = staminaController;
            // _view = view;
            _animationController = animationController;
            _bodyController = bodyController;
            // _searchController = searchController;

            // _collisionTriggerController = collisionTriggerController;
            // _collisionTriggerController.OnEnter += OnTriggerEnter;
        }

        /// <inheritdoc />
        public override async UniTask<(Type, InputData)> Execute(CancellationToken token = default)
        {
            var input = _result.Item2;
            _direction = _bodyController.ForwardDirection;
            _position = _bodyController.Position + _direction * 10f;

            _result = (typeof(MovementState), default);

            // _collisionTriggerController.SetActive(true);

            if (!TrySubtractStamina())
                return _result;

            // VibrationManager.Vibrate(VibrationManager.CharacterRollHaptic);
            // _view.PlayRollFX();

            _state = _animationController.PlayRollAnimation();

            await UniTask.WaitWhile(() => _state.NormalizedEndTime >= _state.NormalizedTime,
                cancellationToken: token);

            _state = null;

            _direction = Vector3.zero;
            _position = Vector3.zero;

            // _collisionTriggerController.SetActive(false);

            return _result;
        }

        /// <summary> Возможно ли отнять стамину </summary>
        private bool TrySubtractStamina()
        {
            // EnableCharacterStaminaConsumption();
            //
            // if (!_enableCharacterStaminaConsumption)
            //     return true;
            //
            // if (!_searchController.IsContainsTarget)
            //     return true;
            //
            // var cost = CharacterCharacteristicSettings.MaxCostRollStamina;
            // if (_staminaController.CurrentValue < cost)
            //     cost = CharacterCharacteristicSettings.MinCostRollStamina;
            //
            // if (_staminaController.CurrentValue < cost)
            //     return false;
            //
            // _staminaController.Apply(cost);
        
            return true;
        }

        /// <inheritdoc />
        public void FixedUpdate(float fixedDeltaTime)
        {
            if (_state == null)
                return;

            //Если нормализированное время не находится в диапазоне, на перса действует усиленное притяжение
            // var normalized = _state.NormalizedTime;
            // if (normalized < _state.Events[StartMovementKey].normalizedTime
            //     || normalized > _state.Events[EndMovementKey].normalizedTime)
            // {
            //     _rigidbody.velocity = Vector3.up * _rigidbody.velocity.y;
            //     _rigidbody.angularVelocity = Vector3.up * _rigidbody.angularVelocity.y;
            //     return;
            // }

            Move(fixedDeltaTime);
            // Rotate(fixedDeltaTime);
        }

        /// <summary> Переместить перса </summary>
        private void Move(float fixedDeltaTime)
        {
            var velocity = (_position - _bodyController.Position) * (15f * fixedDeltaTime);

            _bodyController.ChangeVelocity(velocity);
        }

        // /// <summary> Повернуть перса </summary>
        // private void Rotate(float fixedDeltaTime)
        // {
        //     if (_direction.sqrMagnitude <= 0.0f)
        //         return;
        //
        //     var lookRotation = Quaternion.LookRotation(_direction);
        //     var targetRotation = Quaternion.Lerp(_view.Rigidbody.transform.rotation, lookRotation,
        //         fixedDeltaTime * MovementSpeedSettings.RollRotateSpeed);
        //
        //     _view.Rigidbody.MoveRotation(targetRotation);
        // }

        // /// <summary> Перс сколлизился с бочкой </summary>
        // private void OnTriggerEnter(Collider other)
        // {
        //     var decorationView = other.gameObject.GetComponent<DecorationView>();
        //     if (decorationView != null)
        //         decorationView.Destroy();
        // }

        /// <inheritdoc />
        public override void HandleInput(InputData data)
        {
            switch (data.State)
            {
                case InputState.Swipe:
                    _result = (typeof(RollState), data);
                    break;
                case InputState.Click:
                    _result = (typeof(AttackState), data);
                    break;
            }
        }

        /// <inheritdoc />
        public override void TryInterrupt(Type nextState)
        {
            if (_result != default && _result.Item1 == typeof(RollState))
                return;

            _result = (nextState, default);
        }

        // public override void Dispose()
        // {
        //     _collisionTriggerController.OnEnter -= OnTriggerEnter;
        // }
    }
}
