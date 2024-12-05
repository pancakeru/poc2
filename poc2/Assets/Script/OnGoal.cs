using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.AI;

public class OnGoal : MonoBehaviour
{
    public MMF_Player MMF_Player;
    public NavMeshAgent NavMeshAgent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MMF_Player.PlayFeedbacks();
            if (NavMeshAgent != null)
            {
                NavMeshAgent.enabled = false;
            }
        }
    }
}
