using UnityEngine;

public class CollisionUITrigger : MonoBehaviour
{
    // Reference to the UI GameObject
    public GameObject uiElement;

    // Trigger collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the UI element is assigned and set it active
            if (uiElement != null)
            {
                uiElement.SetActive(true);
            }
        }

    }
}