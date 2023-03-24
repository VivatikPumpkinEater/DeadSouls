using Animations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Draft
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private AnimationController _animationController;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _movementSpeedLerp;
        [SerializeField] private AnimationCurve _movementEase;
        
        private InputActions _inputActions;

        private Vector3 _movementVector;
        
        private float _speed;
        private float _magnitude;
        
        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                _animationController.MovementBlendRate = value;
            }
        }

        // private void Awake()
        // {
        //     _inputActions = new InputActions();
        //     _inputActions.Player.Move.started += OnMovementStarted;
        //     _inputActions.Player.Move.performed += OnMovementPerformed;
        //     _inputActions.Player.Move.canceled += OnMovementCanceled;
        //     _inputActions.Enable();
        //     
        //     _animationController.PlayMovementAnimation();
        // }
        //
        // private void OnMovementStarted(InputAction.CallbackContext context)
        // {
        // }
        //
        // private void OnMovementPerformed(InputAction.CallbackContext context)
        // {
        //     _movementVector = context.ReadValue<Vector2>();
        // }
        //
        // private void OnMovementCanceled(InputAction.CallbackContext context)
        // {
        //     _movementVector = Vector3.zero;
        // }
        //
        // private void Update()
        // {
        //     CalculateSpeed(Time.deltaTime);
        // }
        //
        // private void FixedUpdate()
        // {
        //     Move(Time.fixedDeltaTime);
        // }
        //
        // private void CalculateSpeed(float deltaTime)
        // {
        //     _magnitude = Mathf.Lerp(_magnitude, _movementVector.magnitude,
        //         deltaTime * _movementSpeedLerp);
        //
        //     Speed = _movementEase.Evaluate(_magnitude);
        // }
        //
        //
        // private void Move(float fixedDeltaTime)
        // {
        //     var velocity = _movementVector.normalized * Speed * fixedDeltaTime * 50;
        //     velocity.y = _rigidbody.velocity.y;
        //
        //     _rigidbody.velocity = velocity;
        // }

    }
}