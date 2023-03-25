using UnityEngine.InputSystem;

namespace Control
{
    public partial class InputHandler
    {
        private void InitJumpInput()
        {
            _inputActions.Player.Jump.started += OnJumpStarted;
            _inputActions.Player.Jump.performed += OnJumpPerformed;
            _inputActions.Player.Jump.canceled += OnJumpCanceled;
        }

        private void OnJumpStarted(InputAction.CallbackContext context)
        {
            
        }
        
        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            
        }
        
        private void OnJumpCanceled(InputAction.CallbackContext context)
        {
            
        }
    }
}