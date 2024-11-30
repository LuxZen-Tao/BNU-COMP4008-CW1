using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;       // Normal movement speed
    public float boostMultiplier = 2f; // Speed multiplier during boost

    [Header("Jump Settings")]
    public float jumpForce = 15f;      // Force applied when jumping
    public bool canJump = true;        // Allow/disallow jumping

    private Rigidbody rb;              // Reference to the Rigidbody component
    private bool isBoosting = false;   // Flag to track boost state

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
    }

    void HandleMovement()
    {
        // Get player input for X and Z axes
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // Determine the current speed
        float currentSpeed = isBoosting ? moveSpeed * boostMultiplier : moveSpeed;

        // Apply linear velocity for movement
        rb.velocity = new Vector3(
            moveDirection.x * currentSpeed, 
            rb.velocity.y, 
            moveDirection.z * currentSpeed
        );
    }

    void HandleJump()
    {
        // Jump when Space key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
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
