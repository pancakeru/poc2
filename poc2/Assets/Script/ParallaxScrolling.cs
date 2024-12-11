using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public float XstartPos;
    public GameObject Maincamera;
    public float XparallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        XstartPos = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float xdistance = (Maincamera.transform.position.x ) * XparallaxEffect;
        transform.position = new Vector3(XstartPos + xdistance, transform.position.y, transform.position.z);

    }
}
