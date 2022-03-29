using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation;

public class ProjectileBubble : MonoBehaviour
{
    [SerializeField]
    private Obstacle[] obstacles;

    private LineRenderer trajectoryLine;
    private Ray potentialTrajectory;
    private Color rayColor;
    private Vector3 normal;
    private float distance;
    [SerializeField]
    private List<Vector3> trajectoryPoints = new List<Vector3>();

    private int invokeCountCalcilateRaycastCollisions = 0;
    private int invokeCountCalcilateRaycastCollisionsLimit = 3;

    [SerializeField]
    private GameObject projectilePath;

    [SerializeField]
    private int power;
    [SerializeField]
    private Vector3 initialVelocity;
    [SerializeField]
    private string degreeOfTension;

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

            CalculateParabolicTrajectory();
            //CalculateTrajectory();
            DrawTrajectory(trajectoryPoints.ToArray());
            GetComponent<ProjectileBubbleFollower>().speed = Mathf.Abs(initialVelocity.y);
            degreeOfTension = $"{GetComponent<ProjectileBubbleFollower>().speed / 1.4}%";

            trajectoryPoints.Clear();
        }
        if (Input.GetMouseButtonUp(0))
        {
            CalculateParabolicTrajectory();
            //CalculateTrajectory();
            DrawTrajectory(trajectoryPoints.ToArray());

            Vector2[] trajectoryPoints2D = trajectoryPoints.ConvertAll<Vector2>(trajectoryPoint => new Vector2(trajectoryPoint.x, trajectoryPoint.y)).ToArray();
            //Debug.Log(projectilePath.GetComponent<PathCreator>().bezierPath);

            projectilePath = Resources.Load("Prefabs/ProjectileBubblePath") as GameObject;
            GameObject projectilePathClone = Instantiate(projectilePath, transform.parent);

            

            PathCreator projectilePathClonePathCreator = projectilePathClone.GetComponent<PathCreator>();
            BezierPath bezierPath = new BezierPath(trajectoryPoints2D, false, PathSpace.xy);
            projectilePathClonePathCreator.bezierPath = bezierPath;
            projectilePathClonePathCreator.bezierPath.ControlPointMode = BezierPath.ControlMode.Automatic;
            projectilePathClonePathCreator.bezierPath.AutoControlLength = 0.0f;
            GetComponent<ProjectileBubbleFollower>().pathCreator = projectilePathClonePathCreator;
            GetComponent<ProjectileBubbleFollower>().speed = Mathf.Abs(initialVelocity.y);
            degreeOfTension = $"{GetComponent<ProjectileBubbleFollower>().speed / 1.4}%";           
            //trajectoryPoints.Clear();
        }
    }

    public Vector3 getWorldMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        #if UNITY_EDITOR
                //Debug.Log(worldPosition.x);
                //Debug.Log(worldPosition.y);
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
        trajectoryLine.SetPositions(positions);
    }

    public void CalculateParabolicTrajectory()
    {
        //
        float enter;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        new Plane(-Vector3.forward, transform.position).Raycast(ray, out enter);

        Vector3 mouseInWorld = ray.GetPoint(enter);
        initialVelocity = (mouseInWorld - transform.position) * power;

        float t;
        t = (-1f * initialVelocity.y) / Physics.gravity.y;
        t = 2f * t;


        for (int i = 0; i < 100; i++)
        {
            float time = t * i / (float)(100);
            Vector3 currentPoint = transform.position + initialVelocity * time + 0.5f * Physics.gravity * time * time;
            trajectoryPoints.Add(currentPoint);
        }
        //
    }

    public void CalculateTrajectory()
    {
        invokeCountCalcilateRaycastCollisions = 0;
        Vector3 projectTilePosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
        Vector3 currentWorldMousePosition = new Vector3(getWorldMousePosition().x, getWorldMousePosition().y);
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
                rayColor = Color.green;
                if (invokeCountCalcilateRaycastCollisions == 1)
                {
                    Debug.DrawRay(transform.position, potentialTrajectory.direction * distance, rayColor);
                }
                else
                {
                    Debug.DrawRay(originRaypoint, potentialTrajectory.direction * distance, rayColor);
                }
                

                Vector3 pointCollision = potentialTrajectory.GetPoint(distance);
                Vector3 currentPointCollision = new Vector3(pointCollision.x, pointCollision.y);
                trajectoryPoints.Add(currentPointCollision);

                Debug.Log("Предусловие:" + invokeCountCalcilateRaycastCollisions + "<" + invokeCountCalcilateRaycastCollisionsLimit);
                if (invokeCountCalcilateRaycastCollisions < invokeCountCalcilateRaycastCollisionsLimit)
                {
                    Vector3 reflectColissionVector = Vector3.Reflect(currentPointCollision, obstacle.Plane.normal);
                    Debug.Log(invokeCountCalcilateRaycastCollisions);
                    CalculateRaycastCollisionsWithObstacles(currentPointCollision, reflectColissionVector);
                }
            }
        }
    }
}
