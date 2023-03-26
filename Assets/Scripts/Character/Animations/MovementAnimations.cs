using System;

namespace Animations
{
    [Serializable]
    public struct MovementAnimations
    {
        public LinearMixerTransitionClips Movement;
        public TransitionClip RunToStop;
    }
}