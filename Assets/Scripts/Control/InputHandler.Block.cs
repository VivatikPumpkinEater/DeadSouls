using UnityEngine;
using UnityEngine.InputSystem;

namespace Control
{
    public partial class InputHandler
    {
        private void InitBlockInput()
        {
            _inputActions.Player.Block.started += OnBlockStarted;
            _inputActions.Player.Block.performed += OnBlockPerformed;
            _inputActions.Player.Block.canceled += OnBlockCanceled;
        }

        private void OnBlockStarted(InputAction.CallbackContext context)
        {
            Debug.Log("Block");
            InputCompleted?.Invoke(new InputData(InputState.BlockStart, Vector2.zero, Vector2.zero));
        }
        
        private void OnBlockPerformed(InputAction.CallbackContext context)
        {
            
        }
        
        private void OnBlockCanceled(InputAction.CallbackContext context)
        {
            InputCompleted?.Invoke(new InputData(InputState.BlockEnd, Vector2.zero, Vector2.zero));
        }
    }
}