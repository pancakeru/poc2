using UnityEngine;

public class GemCollect : MonoBehaviour
{
    public ParticleSystem gemDestroyEffect; // Particle effect for gem destruction
    public AudioClip gemSound; // Audio clip for gem sound
    public bool isOrb = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for collision with the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with the gem!");

            // Play the gem sound at the gem's position
            AudioSource.PlayClipAtPoint(gemSound, transform.position);
            if (isOrb)
            {

            }

            // Instantiate the particle effect
            Instantiate(gemDestroyEffect, transform.position, Quaternion.identity);

            // Destroy the gem object
            Destroy(gameObject);
        }
        
    }
}