using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform playField;
    [SerializeField] private GameObject bubbleField;
    [SerializeField] private GameObject player;
    public StateMachine StateMachine { get; set; }

    public static GameManager instance = null;

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

        StateMachine = new StateMachine();

        //SaveGameData();
        LoadGameData();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SaveGameData()
    {
        Debug.Log("SaveGameData");
    }

    public void LoadGameData()
    {
        if (File.Exists(Application.dataPath + "/gamedata.json"))
        {
            string gameSaveString = File.ReadAllText(Application.dataPath + "/gamedata.json");
            PlayField playField = JsonConvert.DeserializeObject<PlayField>(gameSaveString);
        }
    }
    public void GeneratePlayField()
    {
        //playField = Resources.Load("Prefabs/PlayField/PlayField") as GameObject;
        //GameObject bubbleField = Resources.Load("Prefabs/PlayField/BubbleField") as GameObject;
        //Instantiate()
        Instantiate(bubbleField, playField);
    }
}
