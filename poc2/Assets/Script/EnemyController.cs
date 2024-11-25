using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Parolling,
    Shooting,
    Chasing,
    Still
}

public class EnemyController : MonoBehaviour
{

    public EnemyType enemy;

    [Header("Parolling Properties")]
    [SerializeField]
    Transform[] points;
    int currentPoint;
    [SerializeField]
    bool showDebugLines=true;

    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float moveSpeed = 5f;
    bool movingForward=true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (enemy)
        {
            case EnemyType.Parolling:
                Vector2 target = points[currentPoint].position;
                Vector2 currentPos = rb.position;
                Vector2 newPos = Vector2.MoveTowards(currentPos, target, moveSpeed * Time.deltaTime);
                rb.MovePosition(newPos);
                if (Vector2.Distance(currentPos,target)<0.1f)
                {
                    if(movingForward && currentPoint < points.Length)
                    {
                        currentPoint++;
                       if(currentPoint== points.Length - 1)
                        {
                            movingForward = false;
                        }
                    }
                    else if(!movingForward&& currentPoint > -1)
                    {
                        currentPoint--;
                        if (currentPoint == 0)
                        {
                            movingForward = true;
                        }
                    }
                   
                    
                    //currentPoint = (currentPoint == pointB) ? pointA : pointB;
                }
                break;

        }
    }

    private void OnDrawGizmos()
    {
        if (showDebugLines && enemy==EnemyType.Parolling)
        {
            Gizmos.color = Color.white;
            for (int i = 0; i < points.Length; i++)
            {
                Transform currentWaypoint = points[i];
                Transform nextWaypoint = points[(i + 1) % points.Length]; // Loop back to the first waypoint
                if (nextWaypoint == points[0])
                {
                    nextWaypoint = null;
                }
                if (currentWaypoint != null && nextWaypoint != null)
                {
                    Debug.DrawLine(currentWaypoint.position, nextWaypoint.position);
                    Gizmos.DrawSphere(currentWaypoint.position, 1f);
                    Gizmos.DrawSphere(nextWaypoint.position, 1f);

                }
            }
           
        }
    }
}
