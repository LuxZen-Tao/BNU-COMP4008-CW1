using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject player; // Reference to the player
    public float moveSpeed = 5f; // Speed at which the prefab moves towards the player

    private void Update()
    {
        // If the player reference is set, move the prefab towards the player
        if (player != null)
        {
            // Calculate direction and move towards the player
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}
