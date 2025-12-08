using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShootBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public Transform shootTarget;
    public float shootForce = 1500f;
    private Vector3 shootFromOffset;
    private Vector3 spawnPosition;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        shootFromOffset = shootPoint.forward * 0.17f + shootPoint.up * 0.07f;
        spawnPosition = shootPoint.position + shootFromOffset;
    }

    public Vector3 GetRandomShootTarget()
    {
        float randomX = Random.Range(-shootTarget.localScale.x / 2, shootTarget.localScale.x / 2);
        float randomY = Random.Range(-shootTarget.localScale.y / 2, shootTarget.localScale.y / 2);
        Vector3 randomPosition = shootTarget.position + shootTarget.right * randomX + shootTarget.up * randomY;
        return randomPosition;
    }

    public LineRenderer bulletPath(Vector3 randomShootTarget)
    {
        Vector3 shootDirection = (randomShootTarget - spawnPosition).normalized;

        Ray ray = new Ray(spawnPosition, shootDirection);
        // Debug.DrawRay(spawnPosition, shootDirection * 100f, Color.red, 1f);
        RaycastHit hit;
        bool isHit = false;
        if (Physics.Raycast(ray, out hit))
        {
            // Debug.Log("Raycast hit: " + hit.collider.name);
            Transform current = hit.collider.gameObject.transform;
            while (current.parent != null) current = current.parent;
            if (current.transform.gameObject.name == "Player") isHit = true;
        }

        if (isHit) {
            GameObject lineObj = Instantiate(new GameObject("BulletPath"), spawnPosition, Quaternion.identity);
            LineRenderer newLine = lineObj.AddComponent<LineRenderer>();
            newLine.startWidth = lineRenderer.startWidth;
            newLine.endWidth = lineRenderer.endWidth;
            newLine.material = lineRenderer.material;
            newLine.startColor = lineRenderer.startColor;
            newLine.endColor = lineRenderer.endColor;
            newLine.positionCount = 2;
            newLine.SetPosition(0, spawnPosition);
            newLine.SetPosition(1, randomShootTarget);
            Destroy(lineObj, 5f);
            return newLine;
        }
        else {
            GameObject lineObj = Instantiate(new GameObject("BulletPath"), spawnPosition, Quaternion.identity);
            LineRenderer newLine = lineObj.AddComponent<LineRenderer>();
            newLine.startWidth = lineRenderer.startWidth;
            newLine.endWidth = lineRenderer.endWidth;
            newLine.material = lineRenderer.material;
            newLine.startColor = lineRenderer.startColor;
            newLine.endColor = lineRenderer.endColor;
            newLine.positionCount = 2;
            newLine.SetPosition(0, spawnPosition);
            newLine.SetPosition(1, randomShootTarget + (randomShootTarget - spawnPosition).normalized * 100f);
            Destroy(lineObj, 5f);
            return newLine;
        }
    }

    public void Shoot(Vector3 randomShootTarget)
    {
        Quaternion rotation = shootPoint.rotation * Quaternion.Euler(90, 0, 0);
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        Vector3 shootDirection = (randomShootTarget - spawnPosition).normalized;
        bulletRb.AddForce(shootDirection * shootForce);
        Destroy(bullet, 5f); // Destroy the bullet after 5 seconds to clean up
    }
}
