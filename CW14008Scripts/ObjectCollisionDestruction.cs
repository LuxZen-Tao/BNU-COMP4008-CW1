using UnityEngine;

public class ObjectCollisionDestruction : MonoBehaviour
{
    [Header("Default Settings")]
    [SerializeField] private int defaultHitPoints = 3; // Default hit points for this object
    [SerializeField] private GameObject defaultHitEffect; // Default effect when hit
    [SerializeField] private GameObject defaultDestructionEffect; // Default effect when destroyed

    [Header("Custom Collision Settings")]
    [SerializeField] private CustomCollisionSettings[] customSettings; // Array of custom collision settings

    [Header("Allowed Objects")]
    [SerializeField] private GameObject[] allowedColliders = new GameObject[5]; // List of up to 5 specific objects to detect

    [Header("Debug Settings")]
    [SerializeField] private bool logCollisions = true; // Toggle debug logging for collisions

    [Header("Hit Point Adder Settings")]
    [SerializeField] private int hitPointIncreasePerStay = 1; // Number of hit points added per frame
    [SerializeField] private float hitPointIncreaseInterval = 1f; // Time interval (in seconds) to add hit points
    [SerializeField] private bool logContinuousHits = true; // Toggle logging for continuous hit point addition

    private int currentHitPoints; // Tracks remaining hit points
    private float lastHitTime; // Time when the last hit point was added

    private void Start()
    {
        // Initialize the object's hit points
        currentHitPoints = defaultHitPoints;
    }

    public void TakeHit(GameObject collider)
    {
        // Get the custom settings for this specific collider
        CustomCollisionSettings customSetting = GetCustomSetting(collider);

        // Reduce hit points (use custom or default)
        currentHitPoints -= customSetting != null ? customSetting.hitPoints : 1;
        Debug.Log($"{gameObject.name} hit by {collider.name}! Remaining hit points: {currentHitPoints}");

        // Play hit effect (use custom or default)
        GameObject effect = customSetting != null ? customSetting.hitEffect : defaultHitEffect;
        if (effect != null)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Debug.Log($"{gameObject.name}: Hit effect played.");
        }

        // Check if the object should be destroyed
        if (currentHitPoints <= 0)
        {
            DestroyObject(customSetting);
        }
    }

    private void DestroyObject(CustomCollisionSettings customSetting)
    {
        Debug.Log($"{gameObject.name} is destroyed!");

        // Play destruction effect (use custom or default)
        GameObject effect = customSetting != null ? customSetting.destructionEffect : defaultDestructionEffect;
        if (effect != null)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Debug.Log($"{gameObject.name}: Destruction effect played.");
        }

        // Destroy the object
        Destroy(gameObject);
    }

    private CustomCollisionSettings GetCustomSetting(GameObject collider)
    {
        // Find a matching custom setting based on the collided object's tag or name
        foreach (CustomCollisionSettings setting in customSettings)
        {
            if (collider.CompareTag(setting.colliderTag) || collider.name == setting.colliderName)
            {
                return setting;
            }
        }
        return null; // No custom setting found
    }

    private bool IsAllowedCollider(GameObject collider)
    {
        // Check if the collider is in the allowedColliders array
        foreach (GameObject allowed in allowedColliders)
        {
            if (allowed != null && collider == allowed)
            {
                return true;
            }
        }
        return false; // Not an allowed collider
    }

    // Continuous hit points addition (from ColliderHitPointAdder script)
    private void AddHitPoints(Collider other)
    {
        // Check if enough time has passed to add a hit point
        if (Time.time - lastHitTime >= hitPointIncreaseInterval)
        {
            lastHitTime = Time.time;

            // Add hit points
            TakeHit(other.gameObject); // Call TakeHit method to add hit points
            if (logContinuousHits)
            {
                Debug.Log($"{gameObject.name} is staying in the trigger of {other.gameObject.name}, hit points added.");
            }
        }
    }

    // Continuous hit points addition (from ColliderHitPointAdder script)
    private void AddHitPoints(Collision collision)
    {
        // Check if enough time has passed to add a hit point
        if (Time.time - lastHitTime >= hitPointIncreaseInterval)
        {
            lastHitTime = Time.time;

            // Add hit points
            TakeHit(collision.gameObject); // Call TakeHit method to add hit points
            if (logContinuousHits)
            {
                Debug.Log($"{gameObject.name} is still colliding with {collision.gameObject.name}, hit points added.");
            }
        }
    }

    // Collision events
    private void OnCollisionEnter(Collision collision)
    {
        if (IsAllowedCollider(collision.gameObject))
        {
            if (logCollisions)
            {
                Debug.Log($"{gameObject.name} collided with {collision.gameObject.name} at {collision.contacts[0].point}");
            }

            // Handle the hit logic
            TakeHit(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsAllowedCollider(other.gameObject))
        {
            if (logCollisions)
            {
                Debug.Log($"{gameObject.name} entered trigger of {other.gameObject.name}");
            }

            // Handle the hit logic
            TakeHit(other.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Add hit points while colliding
        if (IsAllowedCollider(collision.gameObject))
        {
            AddHitPoints(collision);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Add hit points while in trigger
        if (IsAllowedCollider(other.gameObject))
        {
            AddHitPoints(other);
        }
    }

    [System.Serializable]
    public class CustomCollisionSettings
    {
        public string colliderTag; // Tag to identify specific objects (e.g., "Enemy")
        public string colliderName; // Alternatively, use a specific name for unique objects
        public int hitPoints = 1; // Hit points to subtract when hit by this object
        public GameObject hitEffect; // Custom hit effect for this object
        public GameObject destructionEffect; // Custom destruction effect for this object
    }
}
