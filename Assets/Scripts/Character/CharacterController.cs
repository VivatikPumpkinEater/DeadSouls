using System;
using Animations;
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
            
            _stateMachine.Run();
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate(Time.fixedDeltaTime);
        }
    }
}