using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private Vector3 targetRotation; // Target rotation in Euler angles
    [SerializeField] private float delayTime = 3f; // Delay in seconds before rotation starts
    [SerializeField] private float rotationDuration = 2f; // Time to complete the rotation
    [SerializeField] private float initialFOV = 60f; // Starting Field of View
    [SerializeField] private float targetFOV = 90f; // Target Field of View during rotation

    private Quaternion initialRotation; // Camera's starting rotation
    private Quaternion targetQuaternion; // Target rotation as a Quaternion
    private bool isRotating = false; // Whether the camera is currently rotating
    private float elapsedTime = 0f; // Timer for smooth rotation
    private Camera cam; // Reference to the camera component

    void Start()
    {
        // Get the Camera component
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("No Camera component found on this GameObject.");
            return;
        }

        // Set the initial FOV
        cam.fieldOfView = initialFOV;

        // Store the initial rotation of the camera
        initialRotation = transform.rotation;

        // Convert target rotation from Euler angles to Quaternion
        targetQuaternion = Quaternion.Euler(targetRotation);

        // Start the rotation after the specified delay
        Invoke("StartRotation", delayTime);
    }

    void StartRotation()
    {
        isRotating = true;
        elapsedTime = 0f; // Reset the timer
    }

    void Update()
    {
        if (isRotating)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / rotationDuration); // Normalize time (0 to 1)

            // Smoothly interpolate rotation
            transform.rotation = Quaternion.Lerp(initialRotation, targetQuaternion, t);

            // Smoothly interpolate the Field of View
            cam.fieldOfView = Mathf.Lerp(initialFOV, targetFOV, t);

            // Stop rotating once the transition is complete
            if (t >= 1f)
            {
                isRotating = false;
            }
        }
    }
}
