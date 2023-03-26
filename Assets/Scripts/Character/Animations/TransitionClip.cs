using System;
using Animancer;
using UnityEngine;

namespace Animations
{
    /// <summary> Клип с включением root motion </summary>
    [Serializable]
    public class TransitionClip : ClipTransition
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