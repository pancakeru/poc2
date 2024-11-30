using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMove : MonoBehaviour
{
    public PlayerMove playerMove;
    public GameObject[] slot;
    public int index;
    private void Update()
    {
        if (playerMove.shotGun > 0)
        {
            index = playerMove.shotGun - 1;
            this.transform.position = slot[index].transform.position;
        }

    }

    

}
