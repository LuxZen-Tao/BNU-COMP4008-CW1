using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Tooltip("Duration of a full day in seconds.")]
    public float dayDuration = 120f; // Default: 2 minutes for a full cycle

    private float rotationSpeed;

    void Start()
    {
        // Calculate the rotation speed based on the day duration
        rotationSpeed = 360f / dayDuration; // 360 degrees over the specified seconds
    }

    void Update()
    {
        // Rotate the light around the X-axis to simulate day passing
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
