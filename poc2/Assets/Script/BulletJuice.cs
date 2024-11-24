using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletJuice : MonoBehaviour
{
    public Transform aim;
    public bool lockRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lockRotation == false)
        {
            this.gameObject.transform.rotation = aim.rotation;
        }
    }


}
