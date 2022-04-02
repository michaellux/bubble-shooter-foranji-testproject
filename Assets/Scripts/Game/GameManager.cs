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

    [SerializeField] private GameObject player;
    public GameObject playFieldInScene;
    public GameObject nextMovesLeftBubbleInScene;
    [SerializeField] private Transform playFieldTransform;
    [SerializeField] private GameObject bubbleField;

    private PlayFieldModel playField;

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
        //if (File.Exists(Application.dataPath + "/gamedata.json"))
        //{
        //    string gameSaveString = File.ReadAllText(Application.dataPath + "/gamedata.json");
        //    playField = JsonConvert.DeserializeObject<PlayField>(gameSaveString);
        //}
    }
    public void GeneratePlayField()
    {
        //GenerateBubbleField();
        GenerateProjectile();
        GenerateNextMovesLeftBubble();
    }

    //public void GenerateBubbleField()
    //{
    //    GameObject bubbleFieldInScene = Instantiate(bubbleField, playFieldTransform);
    //    foreach (var bubbleRow in playField.bubbleField.bubbleRows)
    //    {
    //        GameObject bubbleRowPrefab = Resources.Load("Prefabs/PlayField/BubbleRow") as GameObject;
    //        GameObject bubbleRowInScene = Instantiate(bubbleRowPrefab, bubbleFieldInScene.transform.position, 
    //            bubbleFieldInScene.transform.rotation);
    //        bubbleRowInScene.transform.SetParent(bubbleFieldInScene.transform);
    //        bubbleRowInScene.transform.localPosition = new Vector3(bubbleRow.position.x, bubbleRow.position.y);

    //        foreach (var goalBubble in bubbleRow.goalBubbles)
    //        {
    //            GameObject goalBubblePrefab = Resources.Load("Prefabs/Balls/GoalBubble") as GameObject;
    //            GameObject goalBubbleInScene = Instantiate(goalBubblePrefab, bubbleRowInScene.transform.position, bubbleRowInScene.transform.rotation);
    //            goalBubbleInScene.transform.SetParent(bubbleRowInScene.transform);
    //            ConfigGoalBubbleInScene(goalBubbleInScene, goalBubble);

    //            //Обновить модель
    //            //GoalBubble goal = goalBubbleInScene.GetComponent<GoalBubble>();
    //        }
    //    }
    //}

    public void ConfigGoalBubbleInScene(GameObject goalBubbleInScene, GoalBubble goalBubble)
    {
        //goalBubbleInScene.transform.position = new Vector3(goalBubble.position.x, goalBubble.position.y, goalBubble.position.z);
        goalBubbleInScene.transform.localPosition = new Vector3(goalBubble.position.x, 0);
        ConfigColorBubbleInScene(goalBubbleInScene, goalBubble.type);
        ConfigSpringJoint(goalBubbleInScene);
    }

    public void ConfigColorBubbleInScene(GameObject bubbleInScene, BubbleTypes bubbleType)
    {
        SpriteRenderer goalBubbleInSceneSpriteRenderer = bubbleInScene.GetComponent<SpriteRenderer>();

        switch (bubbleType)
        {
            case BubbleTypes.RED:
                goalBubbleInSceneSpriteRenderer.color = Color.red;
                break;
            case BubbleTypes.GREEN:
                goalBubbleInSceneSpriteRenderer.color = Color.green;
                break;
            case BubbleTypes.YELLOW:
                goalBubbleInSceneSpriteRenderer.color = Color.yellow;
                break;
            case BubbleTypes.BLUE:
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

    public void GenerateProjectile()
    {
        GameObject projectileBubblePrefab = Resources.Load("Prefabs/Balls/ProjectileBubble") as GameObject;
        GameObject projectileBubbleInScene = Instantiate(projectileBubblePrefab);
        projectileBubbleInScene.transform.SetParent(player.transform);
        projectileBubbleInScene.transform.localPosition = new Vector3(projectileBubblePrefab.transform.position.x, 
            projectileBubblePrefab.transform.position.y);

        BubbleTypes randomType = Utilities.RandomEnumValue<BubbleTypes>();
        projectileBubbleInScene.GetComponent<ProjectileBubble>().SetProjectileBubbleType(randomType);
        ConfigColorBubbleInScene(projectileBubbleInScene, randomType);
    }

    public void GenerateProjectile(BubbleTypes type)
    {
        GameObject projectileBubblePrefab = Resources.Load("Prefabs/Balls/ProjectileBubble") as GameObject;
        GameObject projectileBubbleInScene = Instantiate(projectileBubblePrefab);
        projectileBubbleInScene.transform.SetParent(player.transform);
        projectileBubbleInScene.transform.localPosition = new Vector3(projectileBubblePrefab.transform.position.x,
            projectileBubblePrefab.transform.position.y);
        projectileBubbleInScene.GetComponent<ProjectileBubble>().SetProjectileBubbleType(type);
        ConfigColorBubbleInScene(projectileBubbleInScene, type);
    }

    public void GenerateGoalBubble(BubbleTypes type, Transform transform, Transform parent)
    {
        GameObject goalBubblePrefab = Resources.Load("Prefabs/Balls/GoalBubble") as GameObject;
        GameObject goalBubbleInScene = Instantiate(goalBubblePrefab, transform.position, transform.rotation, parent);
        ConfigColorBubbleInScene(goalBubbleInScene, goalBubbleInScene.GetComponent<GoalBubble>().type);
        ConfigSpringJoint(goalBubbleInScene);
        Debug.Log("New position x:" + transform.position.x);
        Debug.Log("New position y:" + transform.position.y);
    }

    public void GenerateNextMovesLeftBubble()
    {
        GameObject nextMovesLeftBubblePrefab = Resources.Load("Prefabs/Balls/NextMovesLeftBubble") as GameObject;
        nextMovesLeftBubbleInScene = Instantiate(nextMovesLeftBubblePrefab);
        nextMovesLeftBubbleInScene.transform.SetParent(playFieldInScene.transform);

        nextMovesLeftBubbleInScene.transform.localPosition = new Vector3(nextMovesLeftBubblePrefab.transform.position.x,
           nextMovesLeftBubblePrefab.transform.position.y);

        BubbleTypes randomType = Utilities.RandomEnumValue<BubbleTypes>();
        nextMovesLeftBubbleInScene.GetComponent<NextMovesLeftBubble>().type = randomType;
        ConfigColorBubbleInScene(nextMovesLeftBubbleInScene, randomType);
    }
}
