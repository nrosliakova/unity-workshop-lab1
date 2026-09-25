using UnityEngine;

/// <summary>
/// A collectible item that adds 1 point when the dinosaur touches it.
/// The collider must have Is Trigger enabled.
/// Attach this to every collectible GameObject.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Collectible2D : MonoBehaviour
{
    // Prevents the same collectible from being counted more than once.
    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore if already collected.
        if (collected) return;

        // Ignore if the game has ended.
        if (GameManager2D.Instance != null && GameManager2D.Instance.IsGameEnded) return;

        // Only react to the player.
        if (other.CompareTag("Player"))
        {
            collected = true;

            // Add 1 point to the score.
            if (GameManager2D.Instance != null)
            {
                GameManager2D.Instance.AddCollectible();
            }

            // Remove the collectible from the scene.
            Destroy(gameObject);
        }
    }
}
