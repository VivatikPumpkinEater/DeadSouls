using UnityEngine;
using UnityEngine.InputSystem;

namespace Control
{
    public partial class InputHandler
    {
        private void InitAttackInput()
        {
            _inputActions.Player.FastAttack.started += OnFastAttackStarted;
            _inputActions.Player.FastAttack.performed += OnFastAttackPerformed;
            _inputActions.Player.FastAttack.canceled += OnFastAttackCanceled;
            
            _inputActions.Player.HeavyAttack.started += OnHeavyAttackStarted;
            _inputActions.Player.HeavyAttack.performed += OnHeavyAttackPerformed;
            _inputActions.Player.HeavyAttack.canceled += OnHeavyAttackCanceled;
        }

        #region FastAttack

        private void OnFastAttackStarted(InputAction.CallbackContext context)
        {
            Debug.Log("Start");
        }
        
        private void OnFastAttackPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("Performed");
        }
        
        private void OnFastAttackCanceled(InputAction.CallbackContext context)
        {
            Debug.Log("Canceled");

            InputCompleted?.Invoke(new InputData(InputState.FastAttack, Vector2.zero, Vector2.zero));
        }

        #endregion
        
        #region HeavyAttack

        private void OnHeavyAttackStarted(InputAction.CallbackContext context)
        {
            
        }
        
        private void OnHeavyAttackPerformed(InputAction.CallbackContext context)
        {
            
        }
        
        private void OnHeavyAttackCanceled(InputAction.CallbackContext context)
        {
            InputCompleted?.Invoke(new InputData(InputState.HeavyAttack, Vector2.zero, Vector2.zero));

        }

        #endregion
    }
}