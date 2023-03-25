using System;
using UnityEngine;

namespace Control
{
    public partial class InputHandler : MonoBehaviour
    {
        public event Action<InputData> InputCompleted; 

        private InputActions _inputActions;

        private void Awake()
        {
            _inputActions = new InputActions();
            
            InitMovementInput();
            InitJumpInput();
            InitRollInput();
            InitAttackInput();

            _inputActions.Enable();
        }
    }
}