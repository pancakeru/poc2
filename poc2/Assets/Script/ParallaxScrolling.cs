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
<<<<<<< HEAD
        float xdistance = (Maincamera.transform.position.x - XstartPos) * XparallaxEffect;
        float yPosition = YstartPos;//Maincamera.transform.position.y; //+ YstartPos /2 ;
        transform.position = new Vector3(XstartPos + xdistance, yPosition, transform.position.z);
=======
        float xdistance = (Maincamera.transform.position.x ) * XparallaxEffect;
        transform.position = new Vector3(XstartPos + xdistance, transform.position.y, transform.position.z);
>>>>>>> 57ad9bee6f36e59b02f34332c44c61b093348397
    }
}
