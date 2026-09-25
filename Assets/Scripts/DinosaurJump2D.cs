using UnityEngine;

/// <summary>
/// Lets the player press Space to make the dinosaur jump.
/// Uses a GroundCheck child transform and Physics2D.OverlapCircle.
/// Attach this to the Dinosaur GameObject.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class DinosaurJump2D : MonoBehaviour
{
    // How much upward force is applied when jumping.
    [SerializeField] private float jumpForce = 14f;

    // A child Transform placed at the dinosaur's feet.
    [SerializeField] private Transform groundCheck;

    // The radius used for the ground-check circle.
    [SerializeField] private float groundCheckRadius = 0.2f;

    // Only colliders on this layer count as "ground".
    [SerializeField] private LayerMask groundLayer;

    // Cached reference to the Rigidbody2D component.
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (groundCheck == null)
        {
            Debug.LogError("DinosaurJump2D: GroundCheck transform is not assigned!", this);
        }
    }

    private void Update()
    {
        // Do nothing if the game has ended (Game Over or Win).
        if (GameManager2D.Instance != null && GameManager2D.Instance.IsGameEnded)
        {
            return;
        }

        // Check if the player pressed the jump key.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }
    }

    /// <summary>
    /// Jump only if the dinosaur is touching the ground.
    /// </summary>
    private void TryJump()
    {
        if (groundCheck == null) return;

        // Check if a ground collider overlaps with the small circle at the feet.
        bool isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (isGrounded)
        {
            // Apply an instant upward impulse.
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    /// <summary>
    /// Draw the ground-check circle in the Scene view when this object is selected.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
