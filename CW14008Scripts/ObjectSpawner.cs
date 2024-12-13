using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The GameObject to instantiate
    public Transform spawnLocation; // Optional: Location where the object will spawn
    public float spawnInterval = 2f; // Time interval in seconds between spawns

    private void Start()
    {
        // Start the spawning process at regular intervals
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        // Ensure there is an object to spawn
        if (objectToSpawn != null)
        {
            // Spawn the object at the specified location or at the spawner's position
            Instantiate(objectToSpawn, spawnLocation != null ? spawnLocation.position : transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No object assigned to spawn!");
        }
    }

    
}
