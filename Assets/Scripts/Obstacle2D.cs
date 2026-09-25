using UnityEngine;

/// <summary>
/// Causes Game Over when the dinosaur collides with this cactus.
/// Use the same script on every individual cactus obstacle.
/// Attach this to every cactus GameObject.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Obstacle2D : MonoBehaviour
{
    // Prevents repeated Game Over calls from the same cactus.
    private bool hasTriggered = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleContact(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleContact(other);
    }

    /// <summary>
    /// Shared logic for both collision and trigger contact.
    /// </summary>
    private void HandleContact(Collider2D other)
    {
        if (hasTriggered) return;

        // Only react to the player.
        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (GameManager2D.Instance != null)
            {
                GameManager2D.Instance.GameOver();
            }
        }
    }
}
