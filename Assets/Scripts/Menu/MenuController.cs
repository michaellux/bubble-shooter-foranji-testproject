using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void StartNewGame()
    {
        //GameManager.instance.StateMachine.FindOut(Events.NewGameButtonPressed);
        SceneManager.LoadScene("Gameplay");
    }
    public void ShowCredits()
    {
        //SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        UIManager.instance.ShowPreQuitChoicePanel();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Gameplay")
        {
            DataManager.loadData();
        }
    }
}
