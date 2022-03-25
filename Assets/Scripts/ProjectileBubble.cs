using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBubble : MonoBehaviour
{
    [SerializeField]
    private Obstacle[] obstacles;
    private LineRenderer trajectoryLine;
    Ray potentialTrajectory;
    float distance;
    private ArrayList<Vector3> trajectory = new ArrayList<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        CreateTrajectory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 projectTilePosition = gameObject.transform.position;
            Vector3 currentWorldMousePosition = getWorldMousePosition();
            Vector3 targetPosition = currentWorldMousePosition * -1;

            if (CheckRaycastCollisionsWithObstacles(projectTilePosition, targetPosition))
            {
                Vector3 pointCollision = potentialTrajectory.GetPoint(distance);
                trajectory.
                DrawTrajectory(trajectory);
            }

            //Vector3[] positions = new Vector3[2] { projectTilePosition, targetPosition };
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 projectTilePosition = gameObject.transform.position;
            Vector3 currentWorldMousePosition = getWorldMousePosition();
            Vector3 targetPosition = currentWorldMousePosition * -1;
            Vector3[] positions = new Vector3[2] { projectTilePosition, targetPosition };
            DrawTrajectory(positions);
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

    public void CreateTrajectory()
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
        trajectoryLine.SetPositions(positions);
    }

    public bool CheckRaycastCollisionsWithObstacles(Vector3 originRaypoint, Vector3 directionRay)
    {
        foreach (Obstacle obstacle in obstacles)
        {
            potentialTrajectory = new Ray(originRaypoint, directionRay);

            if (obstacle.Plane.Raycast(potentialTrajectory, out distance))
            {
                Debug.Log("Raycast");
                Debug.Log(obstacle.gameObject.name);
                Debug.DrawRay(transform.position, potentialTrajectory.direction * distance, Color.green);

                return true;
            }
        }

        return false;
    }
}
