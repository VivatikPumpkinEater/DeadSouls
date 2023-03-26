using System;
using Animancer;
using UnityEngine;

namespace Animations
{
    [Serializable]
    public struct ShieldAnimation
    {
        public TransitionClip Block;
        public AvatarMask Mask;
    }
}