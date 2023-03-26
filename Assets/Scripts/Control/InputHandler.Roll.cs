using UnityEngine;
using UnityEngine.InputSystem;

namespace Control
{
    public partial class InputHandler
    {
        private void InitRollInput()
        {
            _inputActions.Player.Roll.started += OnRollStarted;
            _inputActions.Player.Roll.performed += OnRollPerformed;
            _inputActions.Player.Roll.canceled += OnRollCanceled;
        }

        private void OnRollStarted(InputAction.CallbackContext context)
        {
            InputCompleted?.Invoke(new InputData(InputState.RollClick, Vector2.zero, Vector2.zero));
        }
        
        private void OnRollPerformed(InputAction.CallbackContext context)
        {
            
        }
        
        private void OnRollCanceled(InputAction.CallbackContext context)
        {
            
        }
    }
}