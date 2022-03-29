using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine.SceneManagement;
internal class StartState : State
{
    internal StartState()
    {
        //SceneManager.LoadScene(0);
    }

    protected override void ChangeState(StateMachine stateMachine, Events eventItem)
    {
        switch (eventItem)
        {
            case Events.NewGameButtonPressed:
                {
                    SceneManager.LoadScene(1);
                    GameManager.instance.LoadGameData();
                    GameManager.instance.GeneratePlayField();
                    //stateMachine.State = new StartBallPositionState();
                    break;
                }
        }
    }
}