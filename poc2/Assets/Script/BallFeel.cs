using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class BallFeel : MonoBehaviour
{
    public MMF_Player onTrigger;
    public MMF_Player onDestory;

    public void playOnTrigger()
    {
        onTrigger.PlayFeedbacks();
    }

    public void playOnDestory()
    { 
        onDestory.PlayFeedbacks();
    }
}
