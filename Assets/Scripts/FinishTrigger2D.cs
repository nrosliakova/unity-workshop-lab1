using UnityEngine;

/// <summary>
/// The finish line trigger. When the dinosaur enters this trigger, the player wins.
/// The collider must have Is Trigger enabled.
/// Attach this to the finish-line GameObject.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class FinishTrigger2D : MonoBehaviour
{
    // Ensures the win only triggers once.
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        // Only react to the player.
        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (GameManager2D.Instance != null)
            {
                GameManager2D.Instance.Win();
            }
        }
    }
}
