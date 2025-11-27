using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class BulletCollision : MonoBehaviour
{
    public float force = 150f;
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
        // if (current.gameObject.name == "Player")
        // {
        //     Animator animator = current.gameObject.GetComponent<Animator>();
        //     Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
        //     // Disable the animator to stop player animations
        //     animator.enabled = false;
        //     // Apply an impulse force to the player in the direction of the bullet's velocity
        //     playerRb.AddForce(rb.linearVelocity.normalized * force, ForceMode.Impulse);
        //     Destroy(gameObject);
        // }
    }
}
