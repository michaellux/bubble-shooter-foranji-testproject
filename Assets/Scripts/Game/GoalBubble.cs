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
            // �����. ��������� ������� ���������
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
        GameObject currentGameObject = gameObject;
        Destroy(gameObject);


        GameObject projectile = collision.collider.gameObject;
        ProjectileBubble projectileBubble = projectile.GetComponent<ProjectileBubble>();
        Destroy(projectile);

        Destroy(GameManager.instance.nextMovesLeftBubbleInScene);
        //��������!
        //GameManager.instance.GenerateNextMovesLeftBubble();

        //GameManager.instance.GenerateProjectile(GameManager.instance.nextMovesLeftBubbleInScene.GetComponent<NextMovesLeftBubble>().type);
        //GameManager.instance.GenerateGoalBubble(projectileBubble.GetProjectileBubbleType(),
        //currentGameObject.transform, currentGameObject.transform.parent);
        Debug.Log("Old position x:" + currentGameObject.GetComponent<GoalBubble>().transform.position.x);
        Debug.Log("Old position y:" + currentGameObject.GetComponent<GoalBubble>().transform.position.y);


    }
}
