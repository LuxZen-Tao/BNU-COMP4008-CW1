using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireRate = 0.5f; // Time between shots
    public float bulletSpawnOffset = 1f; // Distance forward from the parent
    public float debugLineLength = 5f; // Length of the debug line

    private float nextFireTime = 0f;

    void Update()
    {
        HandleShooting();
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Calculate the spawn position in front of the parent GameObject
            Vector3 spawnPosition = firePoint.position + firePoint.forward * bulletSpawnOffset;

            // Instantiate the bullet
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, firePoint.rotation);

            // Draw a debug line to visualise the bullet's spawn direction
            Debug.DrawLine(spawnPosition, spawnPosition + firePoint.forward * debugLineLength, Color.red, 2f);

            // Apply velocity to the bullet
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = firePoint.forward * bulletSpeed;
            }
        }
    }
}
