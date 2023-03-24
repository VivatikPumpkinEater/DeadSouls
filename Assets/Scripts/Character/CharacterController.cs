using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        private FSMCharacter _stateMachine;

        private void Awake()
        {
            _stateMachine = new FSMCharacter();
        }
    }
}