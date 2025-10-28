using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootForce = 1500f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time % 1f < Time.fixedDeltaTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 offset = shootPoint.forward * 0.17f + shootPoint.up * 0.07f;
        Vector3 spawnPosition = shootPoint.position + offset;
        Quaternion rotation = shootPoint.rotation * Quaternion.Euler(90, 0, 0);
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(shootPoint.forward * shootForce);
        Destroy(bullet, 5f); // Destroy the bullet after 5 seconds to clean up
    }
}
