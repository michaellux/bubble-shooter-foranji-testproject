using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal abstract class State
{
    internal virtual void HandleButton(StateMachine stateMachine, Events eventItem)
    {
        ChangeState(stateMachine, eventItem);
    }

    protected abstract void ChangeState(StateMachine stateMachine, Events eventItem);
}
