using UnityEngine;

public class GemCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for collision with player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with the gem!");
            Destroy(gameObject);
        }
    }
}
