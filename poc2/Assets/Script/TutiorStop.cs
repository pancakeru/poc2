using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
public enum ButtonNeedToPress
{
    Space,
    Left,
    Right,
    A,
    D
}

public class TutiorStop : MonoBehaviour
{
    public MMF_Player TimeSlower;
    public ButtonNeedToPress Button;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TimeSlower.PlayFeedbacks();

        }
    }

    private void Update()
    {
        switch (Button)
        {
            case ButtonNeedToPress.Space:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TimeSlower.StopFeedbacks();
                }
                break;

            case ButtonNeedToPress.A:
                if (Input.GetKeyDown(KeyCode.A))
                {
                    TimeSlower.StopFeedbacks();
                }
                break;

            case ButtonNeedToPress.D:
                if (Input.GetKeyDown(KeyCode.D))
                {
                    TimeSlower.StopFeedbacks();
                }
                break;

            case ButtonNeedToPress.Left:
                if (Input.GetMouseButtonDown(0))
                {
                    TimeSlower.StopFeedbacks();
                }
                break;
            case ButtonNeedToPress.Right:
                if (Input.GetMouseButtonDown(1))
                {
                    TimeSlower.StopFeedbacks();
                }
                break;
        }
    }
}
