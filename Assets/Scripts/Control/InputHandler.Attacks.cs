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
            
        }
        
        private void OnFastAttackPerformed(InputAction.CallbackContext context)
        {
            
        }
        
        private void OnFastAttackCanceled(InputAction.CallbackContext context)
        {
            
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
            
        }

        #endregion
    }
}