using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuController : MonoBehaviour
{
    public static MenuController instance = null;

    void Start()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    public void StartNewGame()
    {
        GameManager.instance.StateMachine.FindOut(Events.NewGameButtonPressed);
    }
    public void ShowCredits()
    {
        //SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        UIManager.instance.ShowPreQuitChoicePanel();
    }

}
