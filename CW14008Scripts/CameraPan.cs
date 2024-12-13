using UnityEngine;

public class CameraPan : MonoBehaviour
{
    [SerializeField] private Vector3 panDirection = Vector3.right; // Direction to pan (e.g., right)
    [SerializeField] private float panSpeed = 5f; // Speed of the pan
    [SerializeField] private float stopTime = 5f; // Time (in seconds) after which the camera stops moving

    private bool isPanning = true; // Whether the camera is currently panning
    private float elapsedTime = 0f; // Timer to track how long the camera has been panning

    void Update()
    {
        if (isPanning)
        {
            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            // Move the camera in the specified direction at the specified speed
            transform.position += panDirection * panSpeed * Time.deltaTime;

            // Stop panning once the elapsed time exceeds the stop time
            if (elapsedTime >= stopTime)
            {
                isPanning = false;
            }
        }
    }
}
