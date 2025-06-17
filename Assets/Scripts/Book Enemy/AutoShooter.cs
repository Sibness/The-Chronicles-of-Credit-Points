using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;           // Wo das Projektil gespawnt wird
    public Vector2 shootDirection = Vector2.right;
    public float projectileSpeed = 5f;
    public float projectileRange = 10f;

    public float shootInterval = 2f;      // Sekundentakt zwischen Schüssen
    private float shootTimer = 0f;

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile p = proj.GetComponent<Projectile>();

        p.direction = shootDirection.normalized;
        p.speed = projectileSpeed;
        p.maxDistance = projectileRange;
    }
}
