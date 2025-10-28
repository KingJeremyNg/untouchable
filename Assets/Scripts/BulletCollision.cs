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
        print("Bullet collided with " + collision.gameObject.name);
    }
}
