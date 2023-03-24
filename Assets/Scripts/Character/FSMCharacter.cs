using Character.FSM;
using FSM;

namespace Character
{
    public class FSMCharacter : FSMController
    {
        public FSMCharacter()
        {
            RegisterState(new IdleState());
            RegisterState(new MoveState());
            RegisterState(new RollState());
        }
    }
}