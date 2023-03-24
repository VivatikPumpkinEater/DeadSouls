using UnityEngine;
using UnityEngine.InputSystem;

namespace Control
{
    public class InputHandler : MonoBehaviour
    {
        private InputActions _inputActions;

        private void Awake()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {

        }

        public void OnJump(InputAction.CallbackContext context)
        {
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
        }
    }
}