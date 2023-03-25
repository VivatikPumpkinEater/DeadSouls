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
            
        }
        
        private void OnRollPerformed(InputAction.CallbackContext context)
        {
            
        }
        
        private void OnRollCanceled(InputAction.CallbackContext context)
        {
            
        }
    }
}