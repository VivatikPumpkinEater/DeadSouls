using System;
using System.Threading;
using Control;
using Cysharp.Threading.Tasks;
using FSM;

namespace Character.FSM
{
    public class IdleState : FSMState
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
    }
}