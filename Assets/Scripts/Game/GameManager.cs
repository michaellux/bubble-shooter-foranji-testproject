using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using Newtonsoft.Json;
using TMPro;

public class GameManager : MonoBehaviour
{
    public StateMachine StateMachine { get; set; }

    public static GameManager instance = null;

    [SerializeField] private GameObject player;
    public GameObject playFieldInScene;
    public GameObject nextMovesLeftBubbleInScene;
    [SerializeField] private Transform playFieldTransform;
    [SerializeField] private GameObject bubbleField;

    public ProjectileBubbleModel projectileModel;
    public NextMovesLeftBubbleModel nextMovesLeftBubbleModel;

    private ProjectileBubbleData scriptableObjectProjectileBubbleData;
    private GoalBubbleData scriptableObjectGoalBubbleData;

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

        DataManager.loadData();
    }

    void Start()
    {
        GeneratePlayField();
    }

    void Update()
    {
        
    }

    public void SaveGameData()
    {
        Debug.Log("SaveGameData");
    }

    public void GeneratePlayField()
    {
        GenerateBubbleField();
        GenerateProjectile();
        GenerateNextMovesLeftBubble();
    }

    public void GenerateBubbleField()
    {
        GameObject bubbleFieldInScene = Instantiate(bubbleField, playFieldTransform.transform.position, playFieldTransform.transform.rotation);
        foreach (var bubbleRow in DataManager.GetPlayFieldModel().bubbleField.bubbleRows)
        {
            GameObject bubbleRowPrefab = Resources.Load("Prefabs/PlayField/BubbleRow") as GameObject;
            GameObject bubbleRowInScene = Instantiate(bubbleRowPrefab, bubbleFieldInScene.transform.position,
                bubbleFieldInScene.transform.rotation);
            bubbleRowInScene.transform.SetParent(bubbleFieldInScene.transform);
            bubbleRowInScene.transform.localPosition = new Vector3(bubbleRow.position.x, bubbleRow.position.y);

            foreach (var goalBubbleModel in bubbleRow.goalBubbles)
            {
                GameObject goalBubblePrefab = Resources.Load("Prefabs/Balls/GoalBubble") as GameObject;
                GameObject goalBubbleInScene = Instantiate(goalBubblePrefab, bubbleRowInScene.transform.position, bubbleRowInScene.transform.rotation);
                goalBubbleInScene.transform.SetParent(bubbleRowInScene.transform);
                ConfigGoalBubbleInScene(goalBubbleInScene, goalBubbleModel);

                //Установка модели и отображение в редакторе
                scriptableObjectGoalBubbleData = ScriptableObject.CreateInstance<GoalBubbleData>();
                scriptableObjectGoalBubbleData.SetBubbleModel(goalBubbleModel);
                goalBubbleInScene.GetComponent<GoalBubble>().scriptableObjectWithModel = scriptableObjectGoalBubbleData;
            }
        }
    }

    public void ConfigGoalBubbleInScene(GameObject goalBubbleInScene, GoalBubbleModel goalBubble)
    {
        goalBubbleInScene.transform.localPosition = new Vector3(goalBubble.position.x, 0);
        ConfigColorBubbleInScene(goalBubbleInScene, goalBubble);
        ConfigSpringJoint(goalBubbleInScene);
    }

    public void ConfigColorBubbleInScene(GameObject bubbleInScene, BubbleModel bubbleModel)
    {
        SpriteRenderer goalBubbleInSceneSpriteRenderer = bubbleInScene.GetComponent<SpriteRenderer>();

        switch (bubbleModel.type)
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
        projectileModel = new ProjectileBubbleModel(randomType);
        ConfigColorBubbleInScene(projectileBubbleInScene, projectileModel);

        //Установка модели и отображение в редакторе
        scriptableObjectProjectileBubbleData = ScriptableObject.CreateInstance<ProjectileBubbleData>();
        scriptableObjectProjectileBubbleData.SetBubbleModel(projectileModel);
        projectileBubbleInScene.GetComponent<ProjectileBubble>().scriptableObjectWithModel = scriptableObjectProjectileBubbleData;
    }

    public void GenerateProjectileWithConcreteType()
    {
        GameObject projectileBubblePrefab = Resources.Load("Prefabs/Balls/ProjectileBubble") as GameObject;
        GameObject projectileBubbleInScene = Instantiate(projectileBubblePrefab);
        projectileBubbleInScene.transform.SetParent(player.transform);
        projectileBubbleInScene.transform.localPosition = new Vector3(projectileBubblePrefab.transform.position.x,
            projectileBubblePrefab.transform.position.y);
        projectileModel.type = nextMovesLeftBubbleModel.type;
        ConfigColorBubbleInScene(projectileBubbleInScene, projectileModel);

        //Установка модели и отображение в редакторе
        scriptableObjectProjectileBubbleData = ScriptableObject.CreateInstance<ProjectileBubbleData>();
        scriptableObjectProjectileBubbleData.SetBubbleModel(projectileModel);
        projectileBubbleInScene.GetComponent<ProjectileBubble>().scriptableObjectWithModel = scriptableObjectProjectileBubbleData;
    }

    public void GenerateGoalBubble(BubbleModel bubbleModel, Transform transform, Transform parent)
    {
        GameObject goalBubblePrefab = Resources.Load("Prefabs/Balls/GoalBubble") as GameObject;
        GameObject goalBubbleInScene = Instantiate(goalBubblePrefab, transform.position, transform.rotation, parent);
        

        ConfigColorBubbleInScene(goalBubbleInScene, bubbleModel);
        ConfigSpringJoint(goalBubbleInScene);

        //Установка модели и отображение в редакторе
        scriptableObjectGoalBubbleData = ScriptableObject.CreateInstance<GoalBubbleData>();
        scriptableObjectGoalBubbleData.SetBubbleModel(bubbleModel);
        goalBubbleInScene.GetComponent<GoalBubble>().scriptableObjectWithModel = scriptableObjectGoalBubbleData;
    }

    public void GenerateNextMovesLeftBubble()
    {
        GameObject nextMovesLeftBubblePrefab = Resources.Load("Prefabs/Balls/NextMovesLeftBubble") as GameObject;
        nextMovesLeftBubbleInScene = Instantiate(nextMovesLeftBubblePrefab);
        nextMovesLeftBubbleInScene.transform.SetParent(playFieldInScene.transform);

        nextMovesLeftBubbleInScene.transform.localPosition = new Vector3(nextMovesLeftBubblePrefab.transform.position.x,
           nextMovesLeftBubblePrefab.transform.position.y);

        BubbleTypes randomType = Utilities.RandomEnumValue<BubbleTypes>();
        nextMovesLeftBubbleModel = new NextMovesLeftBubbleModel(randomType, player.GetComponent<Player>().GetPossibleMoves());

        ConfigColorBubbleInScene(nextMovesLeftBubbleInScene, nextMovesLeftBubbleModel);
        ConfigNextMovesLeftBubbleInScene();
        //Отображение модели в редакторе
        NextMovesLeftBubbleData scriptableObjectNextMovesBubbleData = ScriptableObject.CreateInstance<NextMovesLeftBubbleData>();
        scriptableObjectNextMovesBubbleData.SetBubbleModel(nextMovesLeftBubbleModel);
        nextMovesLeftBubbleInScene.GetComponent<NextMovesLeftBubble>().scriptableObjectWithModel = scriptableObjectNextMovesBubbleData;
    }

    public void ConfigNextMovesLeftBubbleInScene()
    {
        nextMovesLeftBubbleInScene.transform.Find("NextMovesLeftValue").GetComponent<TextMeshPro>().text = $"{nextMovesLeftBubbleModel.movesLeft}";
    }
}
