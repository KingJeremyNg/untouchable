using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Transform LookAtTarget;
    public Vector3 offset = new Vector3(0, 3, -4); // Default offset
    public float smoothSpeed = 0.125f; // For smooth follow
    public float angleOffset = -5f; // Angle offset for looking upwards

    void LateUpdate()
    {
        if (target == null) return;

        // Get the desired position with offset
        Vector3 desiredPosition = target.position + offset;

        // Apply rotation to offset
        Vector3 rotatedOffset = Quaternion.Euler(0f, 0f, 210f) * LookAtTarget.rotation * offset;

        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position + rotatedOffset, smoothSpeed);

        // Set the position and look at the target
        transform.position = smoothedPosition;
        if (LookAtTarget != null)
            transform.LookAt(LookAtTarget);
        else
            transform.LookAt(target);

        // Apply angle offset
        transform.rotation *= Quaternion.Euler(angleOffset, 0f, 0f);
    }
}