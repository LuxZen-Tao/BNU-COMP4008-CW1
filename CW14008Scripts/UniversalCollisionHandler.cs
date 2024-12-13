using UnityEngine;

public class UniversalCollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Log the collision with the name of the other object
        Debug.Log($"{gameObject.name} collided with {collision.gameObject.name} at {collision.contacts[0].point}");
    }

    void OnCollisionStay(Collision collision)
    {
        // Log ongoing collision
        Debug.Log($"{gameObject.name} is still colliding with {collision.gameObject.name}");
    }

    void OnCollisionExit(Collision collision)
    {
        // Log when the collision ends
        Debug.Log($"{gameObject.name} stopped colliding with {collision.gameObject.name}");
    }

    void OnTriggerEnter(Collider other)
    {
        // Log trigger interaction
        Debug.Log($"{gameObject.name} entered trigger of {other.gameObject.name}");
    }

    void OnTriggerStay(Collider other)
    {
        // Log ongoing trigger interaction
        Debug.Log($"{gameObject.name} is staying in trigger of {other.gameObject.name}");
    }

    void OnTriggerExit(Collider other)
    {
        // Log when exiting the trigger
        Debug.Log($"{gameObject.name} exited trigger of {other.gameObject.name}");
    }
}
