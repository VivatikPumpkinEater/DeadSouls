using Animancer;
using Character;
using UnityEngine;

namespace Animations
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private AnimancerComponent _animancerComponent;
        [SerializeField] private MovementAnimations _movementAnimations;
        [SerializeField] private AttackAnimations _attackAnimations;

        public float MovementBlendRate
        {
            get => _movementAnimations.Movement.State.Parameter;
            set => _movementAnimations.Movement.State.Parameter = value;
        }

        public void PlayMovementAnimation()
        {
            _animancerComponent.Play(_movementAnimations.Movement);
        }

        public AnimancerState PlayStopAnimation()
        {
            return _animancerComponent.Play(_movementAnimations.RunToStop);
        }

        public AnimancerState PlayAttackAnimation(AttackType attackType)
        {
            return _animancerComponent.Play(attackType == AttackType.Heavy
                ? _attackAnimations.HeavyAttack
                : _attackAnimations.FastAttack);
        }
    }
}