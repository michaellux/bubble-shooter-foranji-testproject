using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GoalBubble : MonoBehaviour
{
    [SerializeField] public GoalBubbleData scriptableObjectWithModel;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Projectile")
        {
            // Опция. Применить паттерн стратегия
            double currentDegreeOfTension = collision.collider.gameObject.GetComponent<ProjectileBubble>().GetDegreeOfTension();
            BubbleTypes type = collision.collider.gameObject.GetComponent<ProjectileBubble>().scriptableObjectWithModel.GetBubbleType();
            if (currentDegreeOfTension >= 100)
            {
                CrashGoalBubbleAlgorithm(collision);
            }
        }
    }

    public void CrashGoalBubbleAlgorithm(Collision2D collision)
    {
        GameObject currentGoalBubbleGameObject = gameObject;
        DestroyCurrentGoalBubble(currentGoalBubbleGameObject);
        DestroyCurrentProjectile(collision);

        GenerateNewGoalBubble(currentGoalBubbleGameObject);
        GameManager.instance.GenerateProjectileWithConcreteType();
        UpdateNextMovesLeftBubble();
        
    }

    public void DestroyCurrentGoalBubble(GameObject currentGoalBubbleGameObject)
    {
        Destroy(currentGoalBubbleGameObject);
    }

    public void DestroyCurrentProjectile(Collision2D collision)
    {
        GameObject projectile = collision.collider.gameObject;
        ProjectileBubble projectileBubble = projectile.GetComponent<ProjectileBubble>();
        Destroy(projectile);
    }

    public void UpdateNextMovesLeftBubble()
    {   
        BubbleTypes randomType = Utilities.RandomEnumValue<BubbleTypes>();

        GameObject nextMovesLeftBubbleInScene = GameManager.instance.nextMovesLeftBubbleInScene;
        NextMovesLeftBubbleModel nextMovesLeftBubbleModel = GameManager.instance.nextMovesLeftBubbleModel;

        nextMovesLeftBubbleModel.movesLeft--;
        nextMovesLeftBubbleModel.type = randomType;

        GameManager.instance.ConfigColorBubbleInScene(nextMovesLeftBubbleInScene, nextMovesLeftBubbleModel);
        GameManager.instance.ConfigNextMovesLeftBubbleInScene();

        //Обновление отображения модели в редакторе
        NextMovesLeftBubbleData scriptableObjectNextMovesBubbleData = ScriptableObject.CreateInstance<NextMovesLeftBubbleData>();
        scriptableObjectNextMovesBubbleData.SetBubbleModel(nextMovesLeftBubbleModel);
        nextMovesLeftBubbleInScene.GetComponent<NextMovesLeftBubble>().scriptableObjectWithModel = scriptableObjectNextMovesBubbleData;
    }

    public void GenerateNewGoalBubble(GameObject currentGoalBubbleInScene)
    {
        GameManager.instance.GenerateGoalBubble(GameManager.instance.projectileModel, currentGoalBubbleInScene.transform, currentGoalBubbleInScene.transform.parent);
    }
}
