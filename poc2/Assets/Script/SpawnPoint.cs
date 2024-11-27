using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    bool active=false;

    SpriteRenderer mySp;
    [SerializeField] Color unactiveCol;
    Color activeCol;
    // Start is called before the first frame update
    void Start()
    {
      
        mySp = GetComponent<SpriteRenderer>();
        activeCol = mySp.color;

        if (!active)
        {
            setActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetPos()
    {
        return transform.position;
    }
    public void setActive(bool active)
    {
        if (active)
        {
            active = true;
            if (mySp != null)
            {
                mySp.color = activeCol;
            }
        }
        else
        {
            active = false;
            if (mySp != null)
            {
                mySp.color = unactiveCol;
            }
        }
    }
}
