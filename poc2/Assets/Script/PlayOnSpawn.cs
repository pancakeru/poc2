using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnSpawn : MonoBehaviour
{
    public MMF_Player player;
    private void Start()
    {
        player.PlayFeedbacks();
    }
}
