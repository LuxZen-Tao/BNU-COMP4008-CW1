using UnityEngine;

public class DestroyAfterHits : MonoBehaviour
{
    public int hitLimit = 3; // Number of hits before destruction
    private int hitCount = 0; // Counter to track collisions

    private void OnCollisionEnter(Collision collision)
    {
        // Increment the hit counter
        hitCount++;

        // Log the current hit count to the Console
        Debug.Log($"Hit received. Current hit count: {hitCount}");

        // Check if the hit count reaches the limit
        if (hitCount >= hitLimit)
        {
            // Log that the object is being destroyed
            Debug.Log($"Hit limit reached ({hitLimit}). Destroying the object.");

            // Destroy this GameObject (the one the script is attached to)
            Destroy(gameObject);
        }
    }
}
