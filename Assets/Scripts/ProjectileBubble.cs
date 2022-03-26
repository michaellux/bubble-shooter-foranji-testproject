using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBubble : MonoBehaviour
{
    [SerializeField]
    private Obstacle[] obstacles;
    private LineRenderer trajectoryLine;
    Ray potentialTrajectory;
    Vector3 normal;
    float distance;
    private List<Vector3> trajectoryPoints = new List<Vector3>();

    private int invokeCountCalcilateRaycastCollisions = 0;
    private int invokeCountCalcilateRaycastCollisionsLimit = 3;

    // Start is called before the first frame update
    void Start()
    {
        InitTrajectoryLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CalculateTrajectory();
            DrawTrajectory(trajectoryPoints.ToArray());
            trajectoryPoints.Clear();
        }
        if (Input.GetMouseButtonUp(0))
        {
            CalculateTrajectory();
            DrawTrajectory(trajectoryPoints.ToArray());
            trajectoryPoints.Clear();
        }
    }

    public Vector3 getWorldMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        #if UNITY_EDITOR
                Debug.Log(worldPosition.x);
                Debug.Log(worldPosition.y);
        #endif

        return worldPosition;
    }

    public void InitTrajectoryLine()
    {
        trajectoryLine = gameObject.AddComponent<LineRenderer>() as LineRenderer;
        Material trajectoryLineMaterial = Resources.Load("Materials/TrajectoryMaterial") as Material;
        trajectoryLine.material = trajectoryLineMaterial;
        trajectoryLine.startColor = Color.black;
        trajectoryLine.endColor = Color.black;
        trajectoryLine.positionCount = 0;
    }

    public void DrawTrajectory(Vector3[] positions)
    {
        trajectoryLine.startWidth = 0.08f;
        trajectoryLine.endWidth = 0.08f;
        trajectoryLine.positionCount = positions.Length;
        Debug.Log("Position x" + positions);
        trajectoryLine.SetPositions(positions);
    }

    public void CalculateTrajectory()
    {
            invokeCountCalcilateRaycastCollisions = 0;
            Vector3 projectTilePosition = gameObject.transform.position;
            Vector3 currentWorldMousePosition = getWorldMousePosition();
            Vector3 targetPosition = currentWorldMousePosition * -1;
            trajectoryPoints.Add(projectTilePosition);
            CalculateRaycastCollisionsWithObstacles(projectTilePosition, targetPosition);
        
    }

    public void CalculateRaycastCollisionsWithObstacles(Vector3 originRaypoint, Vector3 directionRay)
    {
        invokeCountCalcilateRaycastCollisions++;

        foreach (Obstacle obstacle in obstacles)
        {
            potentialTrajectory = new Ray(originRaypoint, directionRay);

            if (obstacle.Plane.Raycast(potentialTrajectory, out distance))
            {
                Debug.Log("Raycast");
                Debug.Log(obstacle.gameObject.name);
                Debug.DrawRay(transform.position, potentialTrajectory.direction * distance, Color.green);

                Vector3 pointCollision = potentialTrajectory.GetPoint(distance);
                Vector3 currentPointCollision = new Vector3(pointCollision.x, pointCollision.y, 0);
                trajectoryPoints.Add(currentPointCollision);
                Debug.Log("Предусловие:" + invokeCountCalcilateRaycastCollisions + "<" + invokeCountCalcilateRaycastCollisionsLimit);
                if (invokeCountCalcilateRaycastCollisions < invokeCountCalcilateRaycastCollisionsLimit)
                {
                    Vector3 reflectColissionVector = Vector3.Reflect(currentPointCollision, obstacle.Plane.normal);
                    Debug.Log(invokeCountCalcilateRaycastCollisions);
                    CalculateRaycastCollisionsWithObstacles(originRaypoint, reflectColissionVector);
                }
            }
        }
    }
}
