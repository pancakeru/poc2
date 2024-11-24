using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class EnemyOnHit : MonoBehaviour
{
    public GameObject brustImage;
    public MMF_Player onHitFB;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        { 
            onHitFB.PlayFeedbacks();
            brustImage.SetActive(true);
            brustImage.transform.rotation = Quaternion.Euler(0, 0, collision.transform.rotation.eulerAngles.z);

        }
    }
}
