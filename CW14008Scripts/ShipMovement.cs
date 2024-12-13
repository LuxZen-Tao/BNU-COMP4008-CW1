using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;       // Normal movement speed
    public float boostMultiplier = 2f; // Speed multiplier during boost

    [Header("Jump Settings")]
    public float jumpForce = 15f;       // Force applied when jumping
    public bool canJump = true;         // Allow/disallow jumping
    public float jumpCooldown = 2f;     // Cooldown time before the ship can jump again
    private float jumpCooldownTimer = 0f; // Timer to track cooldown

    private Rigidbody rb;               // Reference to the Rigidbody component
    private bool isBoosting = false;    // Flag to track boost state

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Freeze rotation on all axes
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleBoost();

        // Update cooldown timer if necessary
        if (jumpCooldownTimer > 0)
        {
            jumpCooldownTimer -= Time.deltaTime; // Decrease the timer over time
        }
    }

    void HandleMovement()
    {
        // Get player input for X and Z axes
        float moveX = -Input.GetAxis("Horizontal"); // Inverted horizontal input
        float moveZ = -Input.GetAxis("Vertical");   // Inverted vertical input

        // Combine inputs into a movement direction
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);

        // Normalize to ensure diagonal movement isn't faster
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        // Determine the current speed
        float currentSpeed = isBoosting ? moveSpeed * boostMultiplier : moveSpeed;

        // Apply movement to the Rigidbody
        rb.velocity = new Vector3(
            moveDirection.x * currentSpeed,
            rb.velocity.y, // Keep vertical velocity (e.g., during jumps)
            moveDirection.z * currentSpeed
        );
    }

    void HandleJump()
    {
        // Jump when Space key is pressed and cooldown timer is finished
        if (Input.GetKeyDown(KeyCode.Space) && canJump && jumpCooldownTimer <= 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

            // Reset the cooldown timer after a jump
            jumpCooldownTimer = jumpCooldown;
        }
    }

    void HandleBoost()
    {
        // Activate boost when holding Left Shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isBoosting = true;
        }

        // Deactivate boost when Left Shift is released
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isBoosting = false;
        }
    }
}
