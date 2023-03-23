using Character.FSM;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        private FSMController _stateMachine;

        private void Awake()
        {
            _stateMachine = new FSMController();
        }
    }
}