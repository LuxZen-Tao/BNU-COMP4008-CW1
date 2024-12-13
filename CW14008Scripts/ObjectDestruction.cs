using UnityEngine;

public class ObjectDestruction : MonoBehaviour
{
    [SerializeField] private int hitPoints = 3; // Number of hits the object can take before destruction
    [SerializeField] private GameObject hitEffect; // Effect to play when the object is hit
    [SerializeField] private GameObject destructionEffect; // Effect to play when the object is destroyed

    public void TakeHit()
    {
        // Reduce hit points when hit
        hitPoints--;
        Debug.Log($"{gameObject.name} hit! Remaining hit points: {hitPoints}");

        // Play hit effect, if assigned
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Debug.Log($"{gameObject.name}: Hit effect played.");
        }

        // Check if object should be destroyed
        if (hitPoints <= 0)
        {
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        Debug.Log($"{gameObject.name} is destroyed!");

        // Play destruction effect, if assigned
        if (destructionEffect != null)
        {
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
            Debug.Log($"{gameObject.name}: Destruction effect played.");
        }

        // Destroy the object
        Destroy(gameObject);
    }
}
