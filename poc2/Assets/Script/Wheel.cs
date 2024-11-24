using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool onAir;
    public ParticleSystem BWheelParticleSystem;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onAir = false;
            if (BWheelParticleSystem != null)
            {
                BWheelParticleSystem.Play();
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("land!");
            onAir = true;
            if (BWheelParticleSystem != null)
            {
                BWheelParticleSystem.Stop();
            }
        }

    }
}
