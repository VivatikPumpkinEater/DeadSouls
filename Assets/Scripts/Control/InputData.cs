using UnityEngine;

namespace Control
{
    public class InputData
    {
        /// <summary> Состояние ввода </summary>
        // public readonly InputState State;

        /// <summary> Точка нажатия </summary>
        public  Vector3 StartTouch;

        /// <summary> Точка отпускания </summary>
        public readonly Vector3 EndTouch;

        public InputData(Vector3 startTouch, Vector3 endTouch)
        {
            // State = state;
            StartTouch = startTouch;
            EndTouch = endTouch;
        }

    }
}