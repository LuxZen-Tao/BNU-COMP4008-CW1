using UnityEngine;

public class ShootingAtIntervals : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile to shoot
    public Transform shootPoint; // The point from where the projectile will be shot
    public float shootIntervalInSeconds = 2f; // Interval between shots in seconds
    public float shootSpeed = 10f; // Speed at which the projectile will be shot
    public int maxShots = 10; // Maximum number of shots to fire

    private int currentShotCount = 0; // Tracks how many shots have been fired

    private void Start()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab is not assigned in the Inspector.");
            return;
        }

        // Start shooting at regular intervals
        StartCoroutine(ShootAtIntervals());
    }

    private System.Collections.IEnumerator ShootAtIntervals()
    {
        while (currentShotCount < maxShots)
        {
            ShootProjectile();
            currentShotCount++;

            // Wait for the fixed interval before shooting the next projectile
            yield return new WaitForSeconds(shootIntervalInSeconds);
        }

        Debug.Log("Max shots reached. Stopping shooter.");
    }

    private void ShootProjectile()
    {
        // Instantiate the projectile at the shoot point with the same rotation as the shooter
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Apply force to the projectile (if it has a Rigidbody component)
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply forward force based on shootSpeed
            rb.AddForce(shootPoint.forward * shootSpeed, ForceMode.Impulse);
        }

        Debug.Log($"Shot {projectile.name} from {shootPoint.position} with speed {shootSpeed}. Shot count: {currentShotCount + 1}");
    }
}
