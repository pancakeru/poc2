using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCursorMove : MonoBehaviour
{
    public PlayerMove playerMove;
    public GameObject[] slot;
    public int index;
    private void Update()
    {
        if (playerMove.defendFilp > 0)
        {
            index = playerMove.defendFilp - 1;
            this.transform.position = slot[index].transform.position;
        }

    }


}
