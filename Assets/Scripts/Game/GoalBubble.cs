using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class GoalBubble : MonoBehaviour
{
    [SerializeField] public GoalBubbleData scriptableObjectWithModel;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Projectile")
        {
            foreach (GameObject goalBubbleInScene in GameObject.FindGameObjectsWithTag("Goal"))
            {
                goalBubbleInScene.GetComponent<CircleCollider2D>().enabled = false;
            }    

            // Опция. Применить паттерн стратегия
            double currentDegreeOfTension = collision.collider.gameObject.GetComponent<ProjectileBubble>().GetDegreeOfTension();
            BubbleTypes type = collision.collider.gameObject.GetComponent<ProjectileBubble>().scriptableObjectWithModel.GetBubbleType();

            GameObject newGoalBubble = null;
            GameObject oldGoalBubble = null;
            if (currentDegreeOfTension >= 100)
            {
                oldGoalBubble = gameObject;
                //Debug.Log("Old rowPosition:" + gameObject.GetComponent<GoalBubble>().scriptableObjectWithModel.GetBubbleModel().positionInRow);
                //Debug.Log("Old columnPosition:" + gameObject.GetComponent<GoalBubble>().scriptableObjectWithModel.GetBubbleModel().positionInColumn);
                newGoalBubble = CrashGoalBubbleAlgorithm(collision);
                //Debug.Log("New rowPosition:" + newGoalBubble.GetComponent<GoalBubble>().scriptableObjectWithModel.GetBubbleModel().positionInRow);
                //Debug.Log("New columnPosition:" + newGoalBubble.GetComponent<GoalBubble>().scriptableObjectWithModel.GetBubbleModel().positionInColumn);
            }

            //CheckNeighborBubblesWithSameNewGoalBubbleType(newGoalBubble, type);
            CheckNeighborBubblesWithSameNewGoalBubbleType(oldGoalBubble, type);
            CheckBubblesInAir();

            foreach (GameObject goalBubbleInScene in GameObject.FindGameObjectsWithTag("Goal"))
            {
                goalBubbleInScene.GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    }

    public GameObject CrashGoalBubbleAlgorithm(Collision2D collision)
    {
        GameObject newGoalBubble;

        GameObject currentGoalBubbleGameObject = gameObject;
        DestroyCurrentGoalBubble(currentGoalBubbleGameObject);
        DestroyCurrentProjectile(collision);

        newGoalBubble = GenerateNewGoalBubble(currentGoalBubbleGameObject);
        GameManager.instance.GenerateProjectileWithConcreteType();
        UpdateNextMovesLeftBubble();

        return newGoalBubble;
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

    public GameObject GenerateNewGoalBubble(GameObject currentGoalBubbleInScene)
    {
        return GameManager.instance.GenerateGoalBubbleFromProjectileBubbleModel(GameManager.instance.projectileModel, currentGoalBubbleInScene);
    }

    public void CheckNeighborBubblesWithSameNewGoalBubbleType(GameObject newGoalBubble, BubbleTypes type)
    {
        var neighborBubbles = GetNeighborBubbles(newGoalBubble, false);
        var neighborBubblesWithSameNewGoalBubbleType = GetNeighborBubblesWithSameType(neighborBubbles, type);

        if (neighborBubblesWithSameNewGoalBubbleType.Count > 2)
        {
            foreach (GameObject neighborBubble in neighborBubblesWithSameNewGoalBubbleType)
            {
                Destroy(neighborBubble);
            }
        }
    }

    public List<GameObject> GetNeighborBubbles(GameObject newGoalBubble, bool withDiagonalNeighborBubbles)
    {
        int newGoalBubblePositionInRow = newGoalBubble.GetComponent<GoalBubble>().scriptableObjectWithModel.GetBubbleModel().positionInRow;
        int newGoalBubblePositionInColumn = newGoalBubble.GetComponent<GoalBubble>().scriptableObjectWithModel.GetBubbleModel().positionInColumn;
        var neighborBubbles = new List<GameObject>();
        #region Horizontal and vertical
        Predicate<BubbleModel> isUpNeighbor = goalBubbleModel =>
        {
            return goalBubbleModel.positionInRow == newGoalBubblePositionInRow - 1 &&
            goalBubbleModel.positionInColumn == newGoalBubblePositionInColumn;
        };
        Predicate<BubbleModel> isDownNeighbor = goalBubbleModel =>
        {
            return goalBubbleModel.positionInRow == newGoalBubblePositionInRow + 1 &&
            goalBubbleModel.positionInColumn == newGoalBubblePositionInColumn;
        };
        Predicate<BubbleModel> isLeftNeighbor = goalBubbleModel =>
        {
            return goalBubbleModel.positionInRow == newGoalBubblePositionInRow && 
            goalBubbleModel.positionInColumn == newGoalBubblePositionInColumn - 1;
        };
        Predicate<BubbleModel> isRightNeighbor = goalBubbleModel =>
        {
            return goalBubbleModel.positionInColumn == newGoalBubblePositionInColumn + 1 &&
            goalBubbleModel.positionInRow == newGoalBubblePositionInRow;
        };
        #endregion

        #region Diagonal
        Predicate<BubbleModel> isUpLeftNeighbor = goalBubbleModel =>
        {
            return goalBubbleModel.positionInRow == newGoalBubblePositionInRow - 1 &&
            goalBubbleModel.positionInColumn == newGoalBubblePositionInColumn - 1;
        };
        Predicate<BubbleModel> isUpRightNeighbor = goalBubbleModel =>
        {
            return goalBubbleModel.positionInRow == newGoalBubblePositionInRow - 1 &&
            goalBubbleModel.positionInColumn == newGoalBubblePositionInColumn + 1;
        };
        Predicate<BubbleModel> isDownLeftNeighbor = goalBubbleModel =>
        {
            return goalBubbleModel.positionInRow == newGoalBubblePositionInRow + 1 &&
            goalBubbleModel.positionInColumn == newGoalBubblePositionInColumn - 1;
        };
        Predicate<BubbleModel> isDownRightNeighbor = goalBubbleModel =>
        {
            return goalBubbleModel.positionInRow == newGoalBubblePositionInRow + 1 &&
            goalBubbleModel.positionInColumn == newGoalBubblePositionInColumn + 1;
        };
        #endregion

        Predicate<BubbleModel>[] conditions;

        if (withDiagonalNeighborBubbles)
        {
            conditions = new[] { isUpNeighbor, isDownNeighbor, isLeftNeighbor, isRightNeighbor,
                    isUpLeftNeighbor, isUpRightNeighbor, isDownLeftNeighbor, isDownRightNeighbor
                    };
        }
        else
        {
            conditions = new[] { isUpNeighbor, isDownNeighbor, isLeftNeighbor, isRightNeighbor };
        }

        var allGoalBubbes = GameObject.FindGameObjectsWithTag("Goal");

        foreach (GameObject goalBubbleInScene in allGoalBubbes)
        {
            var goalBubbleModelNeighbor = goalBubbleInScene.GetComponent<GoalBubble>().scriptableObjectWithModel.GetBubbleModel();

            if (goalBubbleModelNeighbor.positionInColumn == 0 && goalBubbleModelNeighbor.positionInRow == 0)
            {
                Debug.Log("00");
            }

            //if (isLeftNeighbor(goalBubbleModelNeighbor) ||
            //    isRightNeighbor(goalBubbleModelNeighbor) ||
            //    isUpNeighbor(goalBubbleModelNeighbor) ||
            //    isDownNeighbor(goalBubbleModelNeighbor))
            //{
            //    neighborBubbles.Add(goalBubbleInScene);
            //}
            if (conditions.Any(condition => condition.Invoke(goalBubbleModelNeighbor)))
            {
                neighborBubbles.Add(goalBubbleInScene);
            }
        }

        return neighborBubbles;
    }

    public List<GameObject> GetNeighborBubblesWithSameType(List<GameObject> neighborBubbles, BubbleTypes type)
    {
        var neighborBubblesWithSameNewGoalBubbleType = new List<GameObject>();
        Predicate<BubbleModel> sameType = goalBubbleModel => goalBubbleModel.type == type;

        foreach (var neighborBubble in neighborBubbles)
        {
            var goalBubbleModelNeighbor = neighborBubble.GetComponent<GoalBubble>().scriptableObjectWithModel.GetBubbleModel();
            if (sameType(goalBubbleModelNeighbor))
            {
                neighborBubblesWithSameNewGoalBubbleType.Add(neighborBubble);
            }
        }

        return neighborBubblesWithSameNewGoalBubbleType;
    }

    public void CheckBubblesInAir()
    {
        foreach (GameObject goalBubbleInScene in GameObject.FindGameObjectsWithTag("Goal"))
        {
            var countOfNeighborBubbles = GetNeighborBubbles(goalBubbleInScene, true).Count;
            if (countOfNeighborBubbles == 0)
            {
                Destroy(goalBubbleInScene);
            }
        }
    }
}
