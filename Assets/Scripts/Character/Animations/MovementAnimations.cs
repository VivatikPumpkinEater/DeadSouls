using System;
using Animancer;

namespace Animations
{
    [Serializable]
    public struct MovementAnimations
    {
        public LinearMixerTransition Movement;
        public ClipTransition RunToStop;
    }
}