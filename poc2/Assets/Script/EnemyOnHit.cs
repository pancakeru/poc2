using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class EnemyOnHit : MonoBehaviour
{
    public GameObject brustImage;
    public MMF_Player onHitFB;
    public MMF_Player onClashFB;
    public PlayerDead playerDead;

    private void Awake()
    {
        playerDead = GameObject.FindGameObjectWithTag("playerDead").GetComponent<PlayerDead>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        { 
            onHitFB.PlayFeedbacks();
            brustImage.SetActive(true);
            brustImage.transform.rotation = Quaternion.Euler(0, 0, collision.transform.rotation.eulerAngles.z);

        }

        if (collision.gameObject.tag == "Player")
        {
            if (collision.GetComponent<PlayerMove>().inDefendFilp == true)
            {
                onClashFB.PlayFeedbacks();
                brustImage.SetActive(true);
                brustImage.transform.rotation = Quaternion.Euler(0, 0, collision.transform.rotation.eulerAngles.z);
            }
            else if (collision.GetComponent<PlayerMove>().inDefendFilp == false)
            {
                playerDead.PlayerisDead();
            }
        }
    }
}
