using System;
using System.Threading;
using Animations;
using Control;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using FSM;

namespace Character.FSM
{
    public class AttackState : FSMState
    {
        private readonly AnimationController _animationController;

        private UniTaskCompletionSource<(Type, InputData)> _tcs = new();
        
        private Tween _resetSpeedTween;

        public AttackState(AnimationController animationController)
        {
            _animationController = animationController;
        }

        public override async UniTask<(Type, InputData)> Execute(CancellationToken token = default)
        {
            _tcs = new();
            token.Register(() => _tcs.TrySetCanceled());
            
            _resetSpeedTween?.Kill();

            var result = await _tcs.Task;

            //Выход из стейта, сбрасываем все данные

            return result;
        }

        public override void HandleInput(InputData data)
        {
            switch (data.State)
            {
                case InputState.Swipe:
                    _tcs.TrySetResult((typeof(RollState), data));
                    break;
                case InputState.FastAttack:
                    RunAttack(AttackType.Fast);
                    break;
                case InputState.HeavyAttack:
                    RunAttack(AttackType.Heavy);
                    break;
                case InputState.Hold:
                    _tcs.TrySetResult((typeof(MovementState), data));
                    break;
            }
        }

        private void RunAttack(AttackType attackType)
        {
            _animationController.PlayAttackAnimation(attackType);
        }

        public override void TryInterrupt(Type nextState)
        {
            _tcs.TrySetResult((nextState, default));
        }
    }
}