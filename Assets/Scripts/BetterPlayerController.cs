using UnityEngine;
using UnityEngine.InputSystem;

public class BetterPlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveInput;

    [Header("Movement")]
    public float moveSpeed = 8f;
    public float jumpForce = 6f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.6f;

    [Header("Glide")] // Add all of our glide variables
    public InputAction glideButton;
    public float glideTimer = 2f;
    private float _glideTime = 0f;
    public bool gliding;
    public AnimationCurve gravityFalloff;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Enable your glide button
        glideButton.Enable();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = Vector3.zero;
        // Move your duck with respect to the camera
        targetVelocity = moveInput.y * moveSpeed * transform.forward + moveInput.x * moveSpeed * transform.right;


        // Glide your duck
        if (glideButton.IsPressed() && !IsGrounded() && rb.linearVelocity.y < 0)
        {
            // We are gliding
            if (!gliding)
            {
                gliding = true;
                rb.useGravity = false;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            }

            targetVelocity = GlideVelocity();
        }
        else
        {
            gliding = false;
            rb.useGravity = true;
            targetVelocity.y = rb.linearVelocity.y;
            _glideTime = 0;
        }


        rb.linearVelocity = targetVelocity;
    }

    Vector3 GlideVelocity()
    {
        Vector3 result = Vector3.zero;

        // Count down our glide timer
        if (_glideTime < glideTimer)
        {
            _glideTime += Time.fixedDeltaTime;
        }

        // Implement our glide
        result = moveInput.y * moveSpeed * transform.forward + moveInput.x * moveSpeed * transform.right;

        result.y = rb.linearVelocity.y + gravityFalloff.Evaluate(_glideTime / glideTimer) * Physics.gravity.y * Time.fixedDeltaTime;

        return result;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }
    }
}