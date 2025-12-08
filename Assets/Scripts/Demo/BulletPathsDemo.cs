using UnityEngine;

public class BulletPathsDemo : MonoBehaviour
{
    public ShootBullet shooter;
    private Vector3 bulletTarget;
    private LineRenderer bulletPath;

    void FixedUpdate()
    {
        if (Time.fixedTime % 1f > Time.fixedDeltaTime) return;
        // Destroy(bulletPath.gameObject);

        bulletTarget = shooter.GetRandomShootTarget();
        bulletPath = shooter.bulletPath(bulletTarget);
        shooter.Shoot(bulletTarget);
    }
}