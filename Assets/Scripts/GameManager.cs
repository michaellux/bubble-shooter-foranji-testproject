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
        GenerateBubbleField();
    }

    public void GenerateBubbleField()
    {
        GameObject bubbleFieldInScene = Instantiate(bubbleField, playFieldTransform);
        foreach (var bubbleRow in playField.bubbleField.bubbleRows)
        {
            GameObject bubbleRowPrefab = Resources.Load("Prefabs/PlayField/BubbleRow") as GameObject;
            GameObject bubbleRowInScene = Instantiate(bubbleRowPrefab, bubbleFieldInScene.transform.position, 
                bubbleFieldInScene.transform.rotation);
            bubbleRowInScene.transform.SetParent(bubbleFieldInScene.transform);
            bubbleRowInScene.transform.localPosition = new Vector3(bubbleRow.position.x, bubbleRow.position.y);

            foreach (var goalBubble in bubbleRow.goalBubbles)
            {
                GameObject goalBubblePrefab = Resources.Load("Prefabs/Balls/GoalBubble") as GameObject;
                GameObject goalBubbleInScene = Instantiate(goalBubblePrefab, bubbleRowInScene.transform.position, bubbleRowInScene.transform.rotation);
                goalBubbleInScene.transform.SetParent(bubbleRowInScene.transform);
                ConfigGoalBubbleInScene(goalBubbleInScene, goalBubble);
            }
        }
    }

    public void ConfigGoalBubbleInScene(GameObject goalBubbleInScene, PlayField.GoalBubble goalBubble)
    {
        //goalBubbleInScene.transform.position = new Vector3(goalBubble.position.x, goalBubble.position.y, goalBubble.position.z);
        goalBubbleInScene.transform.localPosition = new Vector3(goalBubble.position.x, 0);
        ConfigColorGoalBubbleInScene(goalBubbleInScene, goalBubble);
        ConfigSpringJoint(goalBubbleInScene);
    }

    public void ConfigColorGoalBubbleInScene(GameObject goalBubbleInScene, PlayField.GoalBubble goalBubble)
    {
        SpriteRenderer goalBubbleInSceneSpriteRenderer = goalBubbleInScene.GetComponent<SpriteRenderer>();

        switch (goalBubble.type)
        {
            case PlayField.GoalBubbleType.RED:
                goalBubbleInSceneSpriteRenderer.color = Color.red;
                break;
            case PlayField.GoalBubbleType.GREEN:
                goalBubbleInSceneSpriteRenderer.color = Color.green;
                break;
            case PlayField.GoalBubbleType.YELLOW:
                goalBubbleInSceneSpriteRenderer.color = Color.yellow;
                break;
            case PlayField.GoalBubbleType.BLUE:
                goalBubbleInSceneSpriteRenderer.color = Color.blue;
                break;
            default:
                break;
        }
    }

    public void ConfigSpringJoint(GameObject goalBubbleInScene)
    {
        SpringJoint2D goalBubbleInSceneSpringJoint = goalBubbleInScene.GetComponent<SpringJoint2D>();
        goalBubbleInSceneSpringJoint.connectedAnchor =
            new Vector2(goalBubbleInScene.transform.position.x, goalBubbleInScene.transform.position.y + (goalBubbleInScene.transform.localScale.y / 2));
    }
}
