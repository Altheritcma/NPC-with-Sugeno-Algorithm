using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RaycastDirections
{
    public string name = "Name of ray direction.";
    public List<GameObject> rayPoints = new List<GameObject>();
    public Vector2 direction = Vector2.zero;
}


public class RaycastCollisions : MonoBehaviour {

    #region Public Variables
    [Header("Raycasting Setup")]
    public List<RaycastDirections> rayCastDirections = new List<RaycastDirections>();
    // Show rays in debug
    public bool showRays = false;
    #endregion

    #region Private Variables
    [Header("Raycasting Checks")]
    [SerializeField]
    private bool collisionUp = false;
    [SerializeField]
    private bool collisionDown = false;
    [SerializeField]
    private bool collisionLeft = false;
    [SerializeField]
    private bool collisionRight = false;
    [SerializeField]
    private float rayDistance = 5f;
    private RaycastHit2D hit;

    private List<Ray2D> raysUp;
    private List<Ray2D> raysDown;
    private List<Ray2D> raysLeft;
    private List<Ray2D> raysRight;
    #endregion

    void Update()
    {
        CheckCollision();

        // Debugging
        if (showRays == true)
        {
            DebugRaycast();
        }
    }

    void CheckCollision()
    {
        List<Ray2D> raysUp = new List<Ray2D>();
        List<Ray2D> raysDown = new List<Ray2D>();
        List<Ray2D> raysLeft = new List<Ray2D>();
        List<Ray2D> raysRight = new List<Ray2D>();
        hit = new RaycastHit2D();

        for(int i = 0; i < rayCastDirections.Count; i++)
        {
            for (int j = 0; j < rayCastDirections[i].rayPoints.Count; j++)
            {
                if (rayCastDirections[i].rayPoints.Count > 0)
                {
                    Vector2 rayPointsPos = new Vector2(rayCastDirections[i].rayPoints[j].transform.position.x, rayCastDirections[i].rayPoints[j].transform.position.y);
                    Ray2D ray = new Ray2D(rayPointsPos, rayCastDirections[i].direction);

                    // Down
                    if (rayCastDirections[i].direction.y == -1)
                    {
                        // Add to the list all rays pointing down
                        raysDown.Add(ray);
                    }
                    // Left
                    if(rayCastDirections[i].direction.x == -1)
                    {
                        // Add to the list all rays pointing left
                        raysLeft.Add(ray);
                    }
                    // Up
                    if (rayCastDirections[i].direction.y == 1)
                    {
                        // Add to the list all rays pointing up
                        raysUp.Add(ray);
                    }
                    // Right
                    if (rayCastDirections[i].direction.x == 1)
                    {
                        // Add to the list all rays pointing right
                        raysRight.Add(ray);
                    }
                }
            }
        }

        collisionDown = CheckCollision(raysDown);
        collisionUp = CheckCollision(raysUp);
        collisionLeft = CheckCollision(raysLeft);
        collisionRight = CheckCollision(raysRight);
    }

    void DebugRaycast()
    {
        for (int i = 0; i < rayCastDirections.Count; i++)
        {
            for (int j = 0; j < rayCastDirections[i].rayPoints.Count; j++)
            {
                if (rayCastDirections[i].rayPoints.Count > 0)
                {
                    // Down
                    if (rayCastDirections[i].direction.y == -1)
                    {
                        Vector3 rayPointsPos = new Vector3(rayCastDirections[i].rayPoints[j].transform.position.x, rayCastDirections[i].rayPoints[j].transform.position.y - rayDistance, rayCastDirections[i].rayPoints[j].transform.position.z);
                        Debug.DrawLine(rayCastDirections[i].rayPoints[j].gameObject.transform.position, rayPointsPos, Color.red);
                    }
                    // Left
                    else if (rayCastDirections[i].direction.x == -1)
                    {
                        Vector3 rayPointsPos = new Vector3(rayCastDirections[i].rayPoints[j].transform.position.x - rayDistance, rayCastDirections[i].rayPoints[j].transform.position.y, rayCastDirections[i].rayPoints[j].transform.position.z);
                        Debug.DrawLine(rayCastDirections[i].rayPoints[j].gameObject.transform.position, rayPointsPos, Color.red);
                    }
                    // Up
                    if (rayCastDirections[i].direction.y == 1)
                    {
                        Vector3 rayPointsPos = new Vector3(rayCastDirections[i].rayPoints[j].transform.position.x, rayCastDirections[i].rayPoints[j].transform.position.y + rayDistance, rayCastDirections[i].rayPoints[j].transform.position.z);
                        Debug.DrawLine(rayCastDirections[i].rayPoints[j].gameObject.transform.position, rayPointsPos, Color.red);
                    }
                    // Right
                    else if (rayCastDirections[i].direction.x == 1)
                    {
                        Vector3 rayPointsPos = new Vector3(rayCastDirections[i].rayPoints[j].transform.position.x + rayDistance, rayCastDirections[i].rayPoints[j].transform.position.y, rayCastDirections[i].rayPoints[j].transform.position.z);
                        Debug.DrawLine(rayCastDirections[i].rayPoints[j].gameObject.transform.position, rayPointsPos, Color.red);
                    }
                }
            }
        }
    }

    bool CheckCollision(List<Ray2D> rayList)
    {
        for (int i = 0; i < rayList.Count; i++)
        {
            // Check every ray
            hit = Physics2D.Raycast(rayList[i].origin, rayList[i].direction, rayDistance + .001f);

            // If it is true and the collider isn't null then return true
            if (hit == true && hit.collider != null)
            {
                return true;
            }
        }

        return false;
    }
}