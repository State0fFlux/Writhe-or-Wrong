using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // Assign your character's Transform in the Inspector
    public Vector2 offset; // Offset in the X and Y directions
    public float smoothSpeed = 0.125f; // Smoothness of camera movement

    private void LateUpdate()
    {
        // Calculate the target position for the camera
        Vector3 targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);

        // Smoothly interpolate to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}