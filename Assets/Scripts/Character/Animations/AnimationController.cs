using Animancer;
using UnityEngine;

namespace Animations
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private AnimancerComponent _animancerComponent;
        [SerializeField] private MovementAnimations _movementAnimations;

        public float MovementBlendRate
        {
            get => _movementAnimations.Movement.State.Parameter;
            set => _movementAnimations.Movement.State.Parameter = value;
        }

        public void PlayMovementAnimation()
        {
            _animancerComponent.Play(_movementAnimations.Movement);
        }
    }
}