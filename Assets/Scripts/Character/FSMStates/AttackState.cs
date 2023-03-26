using System;
using System.Threading;
using Animancer;
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

        private bool _currentAttackCompleted;
        private AnimancerState _currentAttack;

        public AttackState(AnimationController animationController)
        {
            _animationController = animationController;
        }

        public override async UniTask<(Type, InputData)> Execute(CancellationToken token = default)
        {
            _tcs = new();
            token.Register(() => _tcs.TrySetCanceled());

            var result = await _tcs.Task;

            //Выход из стейта, сбрасываем все данные

            return result;
        }

        public override void HandleInput(InputData data)
        {
            switch (data.State)
            {
                case InputState.FastAttack:
                    RunAttack(AttackType.Fast);
                    break;
                case InputState.HeavyAttack:
                    RunAttack(AttackType.Heavy);
                    break;
            }
        }

        //TODO добавить очередь атаки (последовательность/комбо)
        private void RunAttack(AttackType attackType)
        {
            if (_currentAttack != null)
                return;

            _currentAttack = _animationController.PlayAttackAnimation(attackType);
            _currentAttack.Events.OnEnd = OnAttackComplete;
        }

        private void OnAttackComplete()
        {
            _currentAttack = null;

            TryInterrupt(typeof(MovementState));
        }

        public override void TryInterrupt(Type nextState)
        {
            _tcs.TrySetResult((nextState, default));
        }
    }
}