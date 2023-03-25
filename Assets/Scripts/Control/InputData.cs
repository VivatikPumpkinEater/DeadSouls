using UnityEngine;

namespace Control
{
    public class InputData
    {
        /// <summary> Состояние ввода </summary>
        public readonly InputState State;

        /// <summary> Точка нажатия </summary>
        public  Vector2 StartTouch;

        /// <summary> Точка отпускания </summary>
        public readonly Vector2 EndTouch;

        public InputData(InputState state, Vector2 startTouch, Vector2 endTouch)
        {
            State = state;
            StartTouch = startTouch;
            EndTouch = endTouch;
        }

    }
    
    /// <summary> Состояние ввода </summary>
    public enum InputState
    {
        None,
        Swipe,
        Click,
        Press,
        Hold,
        UnPress,
        RollClick,
        JumpClick
    }

}