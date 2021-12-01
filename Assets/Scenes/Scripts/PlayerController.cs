using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 velocity;

    public bool isMoving;
    public bool isJumping;
    public bool isFalling;
    public bool isGrounded;

    public Vector2 inputVector;
    public Vector3 direction;

    [SerializeField]
    private float moveSpeed = 100f;

    [SerializeField]
    private float maxSpeed = 10f;

    [SerializeField]
    private float jumpForce = 100f;
    private bool jumpKeyHeld;

    [SerializeField]
    private float playerGravity = 5f;
    [SerializeField]
    private float sphereRadius = 0.2f;

    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float groundCheckOffset = 2f;
    private Vector3 checkOrigin;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        velocity = rb.velocity;
        HandleInput();
        HandleDirection();
    }

    private void FixedUpdate()
    {
        HandleJumping();
        HandleGravity();
        HandleMovement();
        HandleSpeedClamping();
    }

    private void HandleGravity()
    {
        rb.AddForce(Vector3.down * playerGravity);
    }

    private void HandleMovement()
    {
        float movementVelocity = direction.x * moveSpeed;
        rb.velocity = new Vector3(movementVelocity, rb.velocity.y, rb.velocity.z);
    }

    private void HandleJumping()
    {
        isGrounded = CheckGrounded();


        if (isGrounded && jumpKeyHeld)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool CheckGrounded()
    {
        checkOrigin = transform.position;
        checkOrigin.y = transform.position.y + groundCheckOffset;

        if (Physics.CheckSphere(checkOrigin, sphereRadius, groundLayer))
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(checkOrigin, sphereRadius);
    }

    private void HandleSpeedClamping()
    {
        float currentVelocity = rb.velocity.x;
        float absVelocity = Mathf.Abs(currentVelocity);

        if (absVelocity > maxSpeed)
        {
            float clampedVelocity = Mathf.Clamp(absVelocity, 0f, maxSpeed);

            // restore velocity direction
            if (currentVelocity < 1)
            {
                clampedVelocity *= -1;
            }

            // apply clamped velocity
            rb.velocity = new Vector3(clampedVelocity, rb.velocity.y, rb.velocity.z);
        }
    }

    private void HandleDirection()
    {
        direction = new Vector3(inputVector.x, inputVector.y, 0f);
    }

    private void HandleInput()
    {
        jumpKeyHeld = false;

        inputVector.x = Input.GetAxisRaw("Horizontal");
        // inputVector.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyHeld = true;
        }
    }
}