using System;
using Animations;
using Character.FSM;
using Control;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private InputHandler _inputHandler;

        private FSMCharacter _stateMachine;

        private void Awake()
        {
            _stateMachine = new FSMCharacter(_animationController, _rigidbody);

            _inputHandler.InputCompleted += _stateMachine.HandleInput;
            _inputHandler.InputCompleted += HandleInput;
            
            _stateMachine.Run();
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
            
            if (Input.GetKeyDown(KeyCode.I))
                Debug.Log(_stateMachine.CurrentState);
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate(Time.fixedDeltaTime);
        }

        private void HandleInput(InputData data)
        {
            // if (data.State == InputState.JumpClick && _stateMachine.CurrentState is not JumpState)
            //     _stateMachine.ForceSetState<JumpState>();
        }
    }
}