using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public float XstartPos;
    public float YstartPos;
    public GameObject Maincamera;
    public float XparallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        XstartPos = transform.position.x;
        YstartPos = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xdistance = (Maincamera.transform.position.x - XstartPos) * XparallaxEffect;
        float yPosition = YstartPos;//Maincamera.transform.position.y; //+ YstartPos /2 ;
        transform.position = new Vector3(XstartPos + xdistance, yPosition, transform.position.z);
    }
}
