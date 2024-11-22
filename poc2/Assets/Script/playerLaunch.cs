using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerLaunch : MonoBehaviour
{
    public MMF_Player Spamplayer;
    public MMF_Player SpamFinishPlayer;
    public int AmountOfSPam;
    public PlayerMove PlayerMove;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (AmountOfSPam > 0) { 
                Spamplayer.PlayFeedbacks();
                AmountOfSPam -= 1;
            }
            if (AmountOfSPam == 0 && PlayerMove.playerIsMoving == false)
            {
                SpamFinishPlayer.PlayFeedbacks();
                PlayerMove.playerIsMoving = true;
            }
        }
    }
}
