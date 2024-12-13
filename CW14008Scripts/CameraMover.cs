using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition; // The target position for the camera
    [SerializeField] private Vector3 targetRotation; // The target rotation (Euler angles) for the camera
    [SerializeField] private float targetFieldOfView = 60f; // The target field of view for the camera
    [SerializeField] private float transitionDuration = 2f; // Time to complete the move, rotation, and FOV change
    [SerializeField] private float moveStartTime = 5f; // Time (in seconds) after which the movement starts

    private Vector3 initialPosition; // Starting position of the camera
    private Quaternion initialRotation; // Starting rotation of the camera
    private Quaternion targetQuaternion; // Target rotation as a Quaternion
    private float initialFieldOfView; // Initial field of view of the camera
    private bool isMoving = false; // Whether the camera is currently moving
    private float elapsedTime = 0f; // Timer for smooth transition

    private Camera cameraComponent; // Reference to the Camera component

    void Start()
    {
        // Store the initial position, rotation, and field of view of the camera
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Convert target rotation from Euler angles to Quaternion
        targetQuaternion = Quaternion.Euler(targetRotation);

        // Get the Camera component
        cameraComponent = GetComponent<Camera>();
        if (cameraComponent != null)
        {
            initialFieldOfView = cameraComponent.fieldOfView;
        }

        // Start the movement after the specified delay
        Invoke("StartMovement", moveStartTime);
    }

    void StartMovement()
    {
        isMoving = true;
        elapsedTime = 0f; // Reset the timer
    }

    void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration); // Normalized time (0 to 1)

            // Smoothly interpolate position and rotation
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            transform.rotation = Quaternion.Lerp(initialRotation, targetQuaternion, t);

            // Smoothly interpolate the field of view if a Camera component is attached
            if (cameraComponent != null)
            {
                cameraComponent.fieldOfView = Mathf.Lerp(initialFieldOfView, targetFieldOfView, t);
            }

            // Stop moving once the transition is complete
            if (t >= 1f)
            {
                isMoving = false;
            }
        }
    }
}
