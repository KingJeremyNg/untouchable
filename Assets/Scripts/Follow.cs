using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    // public Transform directionTarget;
    public Vector3 offset = new Vector3(0, 3, -4); // Default offset
    public float smoothSpeed = 0.125f; // For smooth follow

    void LateUpdate()
    {
        if (target == null) return;

        // For direct follow (no smoothing)
        transform.position = target.position + offset;

        // For smooth follow
        // Vector3 desiredPosition = target.position + offset;
        // Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // transform.position = smoothedPosition;

        // Calculate the offset in the direction directionTarget is facing
        // Vector3 rotatedOffset = directionTarget.rotation * offset;
        // transform.position = target.position + rotatedOffset;
        // transform.LookAt(target);
    }
}