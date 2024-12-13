using UnityEngine;

public class FixedIntervalSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab to spawn
    public Transform spawnLocation; // Optional: spawn location
    public float spawnInterval = 2f; // Fixed interval between spawns
    public int maxSpawnCount = 10; // Maximum number of objects to spawn

    private int currentSpawnCount = 0; // Tracks how many objects have been spawned

    private void Start()
    {
        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab to spawn is not assigned in the Inspector.");
            return;
        }

        // Start spawning with a fixed interval
        StartCoroutine(SpawnAtFixedInterval());
    }

    private System.Collections.IEnumerator SpawnAtFixedInterval()
    {
        while (currentSpawnCount < maxSpawnCount)
        {
            SpawnObject();
            currentSpawnCount++;

            // Wait for the fixed interval before spawning the next object
            yield return new WaitForSeconds(spawnInterval);
        }

        Debug.Log("Max spawn count reached. Stopping spawner.");
    }

    private void SpawnObject()
    {
        // Determine the spawn position (default to the spawner's position if no location is specified)
        Vector3 spawnPos = spawnLocation != null ? spawnLocation.position : transform.position;

        // Instantiate the prefab
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        Debug.Log($"Spawned {spawnedObject.name} at {spawnPos}. Spawn count: {currentSpawnCount + 1}");
    }
}
