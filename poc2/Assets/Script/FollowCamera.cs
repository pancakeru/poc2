using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowCamera : MonoBehaviour
{
    CinemachineVirtualCamera vCam;
    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void flip()
    {
        if (vCam != null)
        {

            CinemachineFramingTransposer framingTransposer =
                vCam.GetCinemachineComponent<CinemachineFramingTransposer>();

            if (framingTransposer != null)
            {


                framingTransposer.m_ScreenX = 1 - Mathf.Abs(framingTransposer.m_ScreenX);
            }

        }

    }
}
