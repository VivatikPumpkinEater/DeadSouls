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
            var idleState = new IdleState(animationController);

            RegisterState(idleState);
            RegisterState(new MovementState(animationController, rigidbody));
            RegisterState(new AttackState(animationController));
            // RegisterState(new RollState());

            SetDefaultState(idleState);
        }
    }
}