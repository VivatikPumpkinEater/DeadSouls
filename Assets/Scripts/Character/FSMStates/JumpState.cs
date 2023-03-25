using System;
using System.Threading;
using Control;
using Cysharp.Threading.Tasks;
using FSM;

namespace Character.FSM
{
    public class JumpState : FSMState, IFixedUpdateListener, IUpdateListener
    {
        public override UniTask<(Type, InputData)> Execute(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public override void HandleInput(InputData data)
        {
            throw new NotImplementedException();
        }

        public override void TryInterrupt(Type nextState)
        {
            throw new NotImplementedException();
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            throw new NotImplementedException();
        }

        public void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}