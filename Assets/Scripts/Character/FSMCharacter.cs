using Animations;
using Character.FSM;
using FSM;
using UnityEngine;

namespace Character
{
    public class FSMCharacter : FSMController
    {
        public FSMCharacter
        (
            AnimationController animationController,
            Rigidbody rigidbody
        )
        {
            var movementState = new MovementState(animationController, rigidbody);

            RegisterState(movementState);
            RegisterState(new JumpState(rigidbody));
            RegisterState(new AttackState(animationController));
            RegisterState(new RollState(animationController, rigidbody));

            SetDefaultState(movementState);
        }
    }
}