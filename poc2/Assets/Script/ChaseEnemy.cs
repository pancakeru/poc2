using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class ChaseEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    public PlayerMove playerMove;
    public Transform player;
    public float maxSpeed = 500f;  
    public float minSpeed = 50f;  
    public float distanceFactor = 1;
    public PlayerDead PlayerDead;
    public int playerDeadTime;
    public float timer;
    public GameObject playerDeadTimerUI;
    public Volume Volume;
    public float value;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        timer = playerDeadTime; 
        Volume.weight = 0; 
        playerDeadTimerUI.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        if (playerMove.playerIsMoving == true)
        {
            agent.SetDestination(player.position);
            float distance = Vector3.Distance(transform.position, player.position);
            agent.speed = Mathf.Clamp(minSpeed + (distance / distanceFactor), minSpeed, maxSpeed);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timer -= Time.deltaTime;
            timer = Mathf.Max(0, timer); 
            float value = 1 - (timer / playerDeadTime);
            Volume.weight = Mathf.Clamp01(value); 
            playerDeadTimerUI.SetActive(true);
            if (timer <= 0)
            {
                PlayerDead.PlayerisDead();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(setToZero());
            timer = playerDeadTime;
            playerDeadTimerUI.SetActive(false);
        }
        
    }

    IEnumerator setToZero()
    {
        while (Volume.weight > 0)
        {
            Volume.weight -= Time.deltaTime;
            Volume.weight = Mathf.Clamp01(Volume.weight);
            yield return null; 
        }

        Volume.weight = 0; 
    }
}
