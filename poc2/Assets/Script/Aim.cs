using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Rigidbody2D rb; 
    public float ShouGunrecoilForce;
    public GameObject ShouGun;
    public MMF_Player player;
    public BulletJuice BulletJuice;
    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; 
        Vector2 directionToMouse = (mouseWorldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void ShootShotGun()
    {
        player.PlayFeedbacks();
        StartCoroutine(lockRotation());
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
            //projectileRb.velocity = recoilDirection * -20;
        }
    }

    IEnumerator lockRotation()
    {
        BulletJuice.lockRotation = true;
        yield return new WaitForSeconds(2f);
        BulletJuice.lockRotation = false;
    }
}
