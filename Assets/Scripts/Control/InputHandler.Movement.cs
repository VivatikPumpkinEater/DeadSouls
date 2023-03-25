using UnityEngine;
using UnityEngine.InputSystem;

namespace Control
{
    public partial class InputHandler
    {
        private void InitMovementInput()
        {
            _inputActions.Player.Movement.started += OnMovementStarted;
            _inputActions.Player.Movement.performed += OnMovementPerformed;
            _inputActions.Player.Movement.canceled += OnMovementCanceled;
        }

        private void OnMovementStarted(InputAction.CallbackContext context)
        {
            InputCompleted?.Invoke(new InputData(InputState.Press, Vector2.zero, Vector3.zero));

        }
        
        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            var inputVector = context.ReadValue<Vector2>();

            InputCompleted?.Invoke(new InputData(InputState.Hold, inputVector, Vector3.zero));
        }
        
        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            var inputVector = context.ReadValue<Vector2>();

            InputCompleted?.Invoke(new InputData(InputState.UnPress, inputVector, Vector3.zero));
        }
    }
}