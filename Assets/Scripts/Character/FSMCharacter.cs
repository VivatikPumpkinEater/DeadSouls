using Animations;
using Character.FSM;
using FSM;

namespace Character
{
    public class FSMCharacter : FSMController
    {
        public FSMCharacter
        (
            AnimationController animationController,
            BodyController bodyController
        )
        {
            var movementState = new MovementState(animationController, bodyController);

            RegisterState(movementState);
            RegisterState(new JumpState(animationController, bodyController));
            RegisterState(new AttackState(animationController));
            RegisterState(new RollState(animationController, bodyController));

            SetDefaultState(movementState);
        }
    }
}