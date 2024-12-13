using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float lifetime = 5f; // Time before the bullet is automatically destroyed

    void Start()
    {
        // Destroy the bullet after a set lifetime to prevent it from existing indefinitely
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Optional: Check what the bullet collided with
        // For example, you can filter only certain layers or tags
        // if (collision.gameObject.CompareTag("Enemy"))
        
        // Destroy the bullet when it collides with something
        Destroy(gameObject);
    }
}
