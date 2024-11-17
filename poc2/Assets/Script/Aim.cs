using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Rigidbody2D rb; 
    public float ShouGunrecoilForce = 10f;
    public GameObject ShouGun;

    void Update()
    {
        
    }

    public void ShootShotGun()
    {

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; 
        Vector2 directionToMouse = (mouseWorldPosition - transform.position).normalized;
        Vector2 recoilDirection = -directionToMouse;
        rb.AddForce(recoilDirection * ShouGunrecoilForce, ForceMode2D.Impulse);
        Vector3 spawnPosition = transform.position + (Vector3)directionToMouse;
        GameObject projectile = Instantiate(ShouGun, spawnPosition, Quaternion.identity);
        Vector2 directionToPlayer = (transform.position - spawnPosition).normalized; 
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb != null)
        {
            projectileRb.velocity = recoilDirection * -20;
        }
    }
}
