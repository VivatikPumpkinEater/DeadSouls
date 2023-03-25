using System;
using System.Threading;
using Animations;
using Control;
using Cysharp.Threading.Tasks;
using FSM;

namespace Character.FSM
{
    public class IdleState : FSMState, IFixedUpdateListener
    {
        private readonly AnimationController _animationController;

        private UniTaskCompletionSource<(Type, InputData)> _tcs = new();

        public IdleState
        (
            AnimationController animationController
        )
        {
            _animationController = animationController;
        }

        /// <inheritdoc />
        public override async UniTask<(Type, InputData)> Execute(CancellationToken token = default)
        {
            _tcs = new();
            token.Register(() => _tcs.TrySetCanceled());

            // var direction = _searchController.IsContainsTarget
            //     ? MovementDirection.CombatForward
            //     : MovementDirection.Forward;

            //НЕ успевает проинится анимансер. Пока в качестве костыля и затычки
            await UniTask.DelayFrame(1, cancellationToken: token);

            _animationController.PlayMovementAnimation();
            _animationController.MovementBlendRate = 0f;

            return await _tcs.Task;
        }

        /// <inheritdoc />
        public void FixedUpdate(float fixedDeltaTime)
        {
            // if (_searchController.IsContainsTarget)
            //     Rotate(fixedDeltaTime);
        }

        /// <inheritdoc />
        public override void HandleInput(InputData data)
        {
            switch (data.State)
            {
                case InputState.Swipe:
                    _tcs.TrySetResult((typeof(RollState), data));
                    break;
                case InputState.Click:
                    // _tcs.TrySetResult((typeof(AttackState), data));
                    break;
                case InputState.Hold:
                    _tcs.TrySetResult((typeof(MovementState), data));
                    break;
            }
        }

        /// <inheritdoc />
        public override void TryInterrupt(Type nextState)
        {
            _tcs.TrySetResult((nextState, default));
        }

    }
}