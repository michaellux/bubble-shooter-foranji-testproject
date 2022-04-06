using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour
{
    #region Panels
    [SerializeField]
    private GameObject preQuitChoicePanel;
    #endregion

    public static UIManager instance = null;
    void Start()
    {
        
    }

    public void ShowPreQuitChoicePanel()
    {
        preQuitChoicePanel.SetActive(true);
    }

    public void ReturnToMenu()
    {
        preQuitChoicePanel.SetActive(false);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit();
        #endif
    }

    public void GoToDeveloperByLink()
    {
        Application.OpenURL("https://t.me/michaelluxsd");
    }

    public void BackToMainScreen()
    {
       SceneManager.LoadScene("MainScreen");
    }
}
