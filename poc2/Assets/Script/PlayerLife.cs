using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    SpawnPoint respawnPoint;
    public bool canDie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnPoint newSpawn;
        if (collision.TryGetComponent<SpawnPoint>(out newSpawn))
        {

            respawnPoint.setActive(false);
            respawnPoint = newSpawn;
            respawnPoint.setActive(true);

        }

        EnemyController newEnim;
        if(collision.TryGetComponent<EnemyController>(out newEnim))
        {
            if (newEnim.canKill)
            {
                dead();
            }
        }

        
    }
    void dead()
    {
        respawn();
    }
    void respawn()
    {
        transform.position = respawnPoint.GetPos();
    }

}
