using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMovesLeftBubble : MonoBehaviour
{
    public Vector3 position;
    public BubbleTypes type;
    public bool isExists;
    [SerializeField] int moves;

    public static NextMovesLeftBubble instance = null;

    public NextMovesLeftBubble(Vector3 position, BubbleTypes type, bool isExists)
    {
        this.position = position;
        this.type = type;
        this.isExists = isExists;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
