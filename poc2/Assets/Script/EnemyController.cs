using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Parolling,
    Shooting,
    Chasing,
    Still
}

public class EnemyController : MonoBehaviour
{
    public bool canKill;
    [SerializeField]
    bool facingRight;
    public EnemyType enemy;
    [Header("General")]
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    bool showDebugLines = true;

    [Header("Parolling Properties")]
    [SerializeField]
    Transform[] points;
    int currentPoint;
    

    [SerializeField]
    Rigidbody2D rb;
    
    
    bool movingForward=true;


    [Header("Chasing Properties")]
    [SerializeField] Transform player;
    [SerializeField] Collider2D boundary;
    [SerializeField] float triggerDistance;
    //public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        if (facingRight)
        {
            flip();
        }
        switch (enemy)
        {
            case EnemyType.Chasing:
                //if(player==null)
                player = GameObject.FindGameObjectWithTag("Player").transform;
                //agent = GetComponent<NavMeshAgent>();
                //agent.updatePosition = false;
                //agent.updateRotation = false;
                break;
        }
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

                        if (!facingRight)
                        {
                            flip();
                        }
                    }
                    else if(!movingForward&& currentPoint > -1)
                    {
                        currentPoint--;
                        if (currentPoint == 0)
                        {
                            
                            movingForward = true;
                        }
                        if (facingRight)
                        {
                            flip();
                        }
                    }
                   
                    
                    //currentPoint = (currentPoint == pointB) ? pointA : pointB;
                }
                break;

            case EnemyType.Chasing:
                //agent.SetDestination(player.position);


                float playerDistance = Vector2.Distance(gameObject.transform.position, player.position);
                if (playerDistance > triggerDistance)
                {
                    return;
                }
                Vector2 direction = (player.position - transform.position).normalized;

                // Calculate new position
                Vector2 newPosition = rb.position + direction * moveSpeed * Time.deltaTime;

                // Check if the new position is within the boundary
                if (boundary.OverlapPoint(newPosition))
                {
                    rb.MovePosition(newPosition); // Move the enemy
                }

                if (player.position.x > transform.position.x)
                {
                    if (!facingRight)
                    {
                        flip();
                    }
                }
                else
                {
                    if (facingRight)
                    {
                        flip();
                    }
                }
                break;

        }
    }

    private void OnDrawGizmos()
    {
        if (!showDebugLines)
        {
            return;
        }
        Gizmos.color = Color.white;
        if (enemy==EnemyType.Parolling)
        {
           
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
        if (boundary != null && enemy==EnemyType.Chasing)
        {
          
            Gizmos.DrawWireCube(boundary.bounds.center,boundary.bounds.size);
        }
    }

    void flip()
    {
        facingRight = facingRight ? false : true;

        Vector3 scale = rb.gameObject.transform.localScale;

        scale.x *= -1;

        rb.gameObject.transform.localScale = scale;

    }
}
