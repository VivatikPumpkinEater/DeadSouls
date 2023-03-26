using System;
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
        [SerializeField] private TransitionClip _rollAnimation;
        [SerializeField] private JumpAnimations _jumpAnimations;
        [SerializeField] private ShieldAnimation _shieldAnimations;

        private AnimancerLayer _shieldLayer;
        
        public float MovementBlendRate
        {
            get => _movementAnimations.Movement.State.Parameter;
            set => _movementAnimations.Movement.State.Parameter = value;
        }

        private void Start()
        {
            _shieldLayer = _animancerComponent.Layers[1];
            _shieldLayer.SetMask(_shieldAnimations.Mask);
        }

        public void PlayMovementAnimation()
        {
            _animancerComponent.Play(_movementAnimations.Movement);
        }

        public AnimancerState PlayStopAnimation()
        {
            return _animancerComponent.Play(_movementAnimations.RunToStop);
        }

        public AnimancerState PlayRollAnimation()
        {
            return _animancerComponent.Play(_rollAnimation);
        }

        public AnimancerState PlayJumpAnimation(JumpStage stage)
        {
            switch (stage)
            {
                case JumpStage.Start:
                    return _animancerComponent.Play(_jumpAnimations.StartJump);
                case JumpStage.Falling:
                    return _animancerComponent.Play(_jumpAnimations.Falling);
                case JumpStage.End:
                    return _animancerComponent.Play(_jumpAnimations.EndJump);
            }

            return null;
        }

        public void EnableShieldAnimation(bool value, float fadeDuration = 0.2f)
        {
            if(value)
                _shieldLayer.Play(_shieldAnimations.Block);
            else
                _shieldLayer.StartFade(0, fadeDuration);
        }

        public AnimancerState PlayAttackAnimation(AttackType attackType)
        {
            return _animancerComponent.Play(attackType == AttackType.Heavy
                ? _attackAnimations.HeavyAttack
                : _attackAnimations.FastAttack);
        }
    }
}