using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnSpawn : MonoBehaviour
{
    public MMF_Player player;
    public int LifeTime;
    public GameObject itemItself;
    public bool usingLifeTime;
    private void Start()
    {
        player.PlayFeedbacks();
    }

    private void FixedUpdate()
    {
        if (usingLifeTime)
        {
            LifeTime -= 1;
            if (LifeTime == 0)
            {
                Destroy(itemItself);
            }
        }
    }

   
}
