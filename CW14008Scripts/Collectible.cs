using Unity.VisualScripting;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public float rotationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public GameObject onCollectEffect; // Reference to the effect that plays when the object is collected.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0); // Rotate the object around the Y axis.
        
    }




    private void OnTriggerEnter(Collider other) {// When the object collides with another object.
    
    if (other.CompareTag("Player")) {
 
}

    Destroy(gameObject); // Destroy the object.

    // instantiate the particle effect
    Instantiate(onCollectEffect, transform.position, transform.rotation);
    }

    



}