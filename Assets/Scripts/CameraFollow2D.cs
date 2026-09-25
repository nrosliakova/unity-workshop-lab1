using UnityEngine;

/// <summary>
/// Makes the camera follow the dinosaur horizontally.
/// The camera keeps its original Y and Z positions.
/// Attach this to the Main Camera.
/// </summary>
public class CameraFollow2D : MonoBehaviour
{
    // The dinosaur (or any target) the camera should follow.
    [SerializeField] private Transform target;

    // Horizontal offset from the target (positive = camera is ahead).
    [SerializeField] private float offsetX = 3f;

    // How smoothly the camera catches up (lower = smoother). Set to 0 for instant follow.
    [SerializeField] private float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (target == null) return;

        // Calculate the desired X position.
        float desiredX = target.position.x + offsetX;

        // Smoothly move toward the desired X, or snap instantly if smoothSpeed is 0.
        float newX;
        if (smoothSpeed <= 0f)
        {
            newX = desiredX;
        }
        else
        {
            newX = Mathf.Lerp(transform.position.x, desiredX, smoothSpeed * Time.deltaTime);
        }

        // Keep the camera's original Y and Z positions.
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
