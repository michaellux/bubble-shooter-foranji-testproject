using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMovesLeftBubble : MonoBehaviour
{
    [SerializeField] public NextMovesLeftBubbleData scriptableObjectWithModel;

    public static NextMovesLeftBubble instance = null;

    void Awake()
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
}
