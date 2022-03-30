using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public StateMachine StateMachine { get; set; }

    public static GameManager instance = null;

    [SerializeField] private Transform playField;
    [SerializeField] private GameObject bubbleField;

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
        LoadGameData();
        GeneratePlayField();
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
        //GameObject playField = Resources.Load("Prefabs/PlayField/PlayField") as GameObject;
        //GameObject bubbleField = Resources.Load("Prefabs/PlayField/BubbleField") as GameObject;
        //Instantiate()
        //
        //GameObject bubbleFieldInScene =  (GameObject)Instantiate(bubbleField, playField.position, playField.rotation);
        //GameObject bubbleFieldInScene = (GameObject)Instantiate(bubbleField);
        //bubbleFieldInScene.transform.parent = playField.transform;

        Instantiate(bubbleField, playField);
    }
}
