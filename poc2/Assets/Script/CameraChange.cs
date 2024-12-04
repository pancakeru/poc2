using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCameraFrom;
    public CinemachineVirtualCamera VirtualCameraTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            VirtualCameraFrom.m_Priority = 0;
            VirtualCameraTo.m_Priority= 10;
        }
    }
}
