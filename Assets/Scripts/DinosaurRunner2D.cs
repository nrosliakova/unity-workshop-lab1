using UnityEngine;

/// <summary>
/// Automatically moves the dinosaur to the right at a constant speed.
/// Attach this to the Dinosaur GameObject.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class DinosaurRunner2D : MonoBehaviour
{
    // How fast the dinosaur runs to the right (units per second).
    [SerializeField] private float runSpeed = 8f;

    // Cached reference to the Rigidbody2D component.
    private Rigidbody2D rb;

    // When true, the dinosaur stops moving.
    private bool stopped = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (stopped) return;

        // Move right by setting horizontal velocity.
        // Keep the current vertical velocity so gravity and jumping still work.
        rb.linearVelocity = new Vector2(runSpeed, rb.linearVelocity.y);
    }

    /// <summary>
    /// Call this to stop the dinosaur permanently (Game Over or Win).
    /// </summary>
    public void StopMovement()
    {
        stopped = true;
        rb.linearVelocity = Vector2.zero;
    }
}
