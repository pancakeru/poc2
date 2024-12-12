using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Feel;
using MoreMountains.Tools;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    public PlayerMove playerMove;
    public GameObject DeadUI;
    public WheelJoint2D FWheelJoint2D;
    public WheelJoint2D BWheelJoint2D;
    public MMF_Player MMF_Player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            PlayerisDead();
        }
    }

    public void PlayerisDead()
    {
        JointMotor2D Bmotor = BWheelJoint2D.motor;
        Bmotor.motorSpeed = 0;
        BWheelJoint2D.motor = Bmotor;
        playerMove.enabled = false;
        DeadUI.SetActive(true);
        MMF_Player.PlayFeedbacks();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerisDead();
        }
    }

}
