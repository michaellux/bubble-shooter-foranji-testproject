using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

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
            //case Events.NewGameButtonPressed:
            //    {
            //        SceneManager.sceneLoaded += OnSceneLoaded;
            //        SceneManager.LoadScene("Gameplay");
                   
            //        break;
            //    }
        }
    }

    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    Debug.Log("OnSceneLoaded: " + scene.name);
    //    Debug.Log(mode);
       
    //    bool status = SceneManager.GetSceneByName("Gameplay").isLoaded;
    //    GameManager.instance.LoadGameData();
    //    GameManager.instance.GeneratePlayField();
    //    //stateMachine.State = new StartBallPositionState();
    //}
}