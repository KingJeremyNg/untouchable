using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class BulletCollision : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;
    private MeshCollider col;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<MeshCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Iterate to the highest parent
        Transform current = collision.gameObject.transform;
        while (current.parent != null)
        {
            current = current.parent;
        }

        // Disable animator if the collided object is the player
        if (current.gameObject.name == "Player")
        {
            Animator animator = current.gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }
        }
    }
}
