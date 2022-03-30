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

    [SerializeField] private Transform playFieldTransform;
    [SerializeField] private GameObject bubbleField;

    private PlayField playField;

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
            playField = JsonConvert.DeserializeObject<PlayField>(gameSaveString);
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
        GameObject bubbleFieldInScene = Instantiate(bubbleField, playFieldTransform);
        foreach (var bubbleRow in playField.bubbleField.bubbleRows)
        {
            GameObject bubbleRowPrefab = Resources.Load("Prefabs/PlayField/BubbleRow") as GameObject;
            //bubbleRowPrefab.transform.SetParent(bubbleFieldInScene.transform);
            GameObject bubbleRowInScene = Instantiate(bubbleRowPrefab, bubbleFieldInScene.transform.position, bubbleField.transform.rotation);
            bubbleRowInScene.transform.SetParent(bubbleFieldInScene.transform);

            foreach (var goalBubble in bubbleRow.goalBubbles)
            {
                GameObject goalBubblePrefab = Resources.Load("Prefabs/Balls/GoalBubble") as GameObject;
                GameObject goalBubbleInScene = Instantiate(goalBubblePrefab, bubbleRowInScene.transform.position, bubbleRowInScene.transform.rotation);
                goalBubbleInScene.transform.SetParent(bubbleRowInScene.transform);
            }
        }
    }
}
