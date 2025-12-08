using UnityEngine;

public class ShootBulletDemo : MonoBehaviour
{
    public ShootBullet shooter;
    private Vector3 bulletTarget;

    void FixedUpdate()
    {
        if (Time.fixedTime % 1f > Time.fixedDeltaTime) return;

        bulletTarget = shooter.GetRandomShootTarget();
        // bulletPath = shooter.bulletPath(bulletTarget);
        shooter.Shoot(bulletTarget);
    }
}