using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class StateMachine
{
    internal State State { get; set; }

    public StateMachine()
    {
        State = new StartState();

        Debug.Log("StartState");
    }

    public void FindOut(Events eventItem)
    {
        State.HandleButton(this, eventItem);
    }
}
