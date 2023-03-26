using System;
using Animancer;
using UnityEngine;

namespace Animations
{
    [Serializable]
    public class LinearMixerTransitionClips : LinearMixerTransition
    {
        [SerializeField] private bool _applyRootMotion;

        /// <inheritdoc />
        public override void Apply(AnimancerState state)
        {
            base.Apply(state);
            state.Root.Component.Animator.applyRootMotion = _applyRootMotion;
        }
    }
}