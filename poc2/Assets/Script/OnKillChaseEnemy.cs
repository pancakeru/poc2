using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Rendering;

public class OnKillChaseEnemy : MonoBehaviour
{
    public MMF_Player onEnterplayer;
    public MMF_Player onPlayerKill;
    public bool isTimeStopped;
    public bool onFire;
    public PlayerMove playerMove;
    public Animator Panimator;
    public Animator Fanimator;
    public Animator Banimator;
    public Animator Biganimator;
    public Animator Aanimator;
    public Volume BloomVolume;
    void Update()
    {
        
        if (isTimeStopped && Input.GetMouseButtonDown(0))
        {
            
            isTimeStopped = false;
            onEnterplayer.StopFeedbacks();
            onPlayerKill.PlayFeedbacks();
            Panimator.SetTrigger("ChangeColor");
            Fanimator.SetTrigger("ChangeColor");
            Banimator.SetTrigger("ChangeColor");
            Biganimator.SetTrigger("ChangeColor");
            StartCoroutine(bloom());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        onEnterplayer.PlayFeedbacks();
        isTimeStopped = true;
        playerMove.rotationMount = 360;
    }

    IEnumerator bloom()
    {
        BloomVolume.weight = 1.0f;
        yield return new WaitForSeconds(1f);
        BloomVolume.weight = 0f;
    }
    
}

