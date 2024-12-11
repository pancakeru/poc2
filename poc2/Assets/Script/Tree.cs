using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public enum layerGround
    {
        foreground,
        ground,
        background
    }

    public layerGround layerG;
    public SpriteRenderer spr; 
    // Start is called before the first frame update
    void Awake()
    {
        //spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
