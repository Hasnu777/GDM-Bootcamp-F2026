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
    private float glideTime;
    public float glideSpeed = 6f;
    public bool gliding;
    public AnimationCurve gravityFalloff;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Emable your glide button
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
        // Move your duck with respect to the camera
        Vector3 targetVelocity = moveInput.y * moveSpeed * transform.forward + moveInput.x * moveSpeed * transform.right;
        
        // Glide your duck
        if(!IsGrounded() && rb.linearVelocity.y < 0 && glideButton.IsPressed())
        {
            if (!gliding)
            {
                rb.useGravity = false;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                gliding = true;
            }
            targetVelocity = GlideVelocity();
        } else
        {
            glideTime = 0;
            rb.useGravity = true;
            targetVelocity.y = rb.linearVelocity.y;
            gliding = false;
        }

        rb.linearVelocity = targetVelocity;
    }

    Vector3 GlideVelocity()
    {
        Vector3 result = Vector3.zero;

        if(glideTime < glideTimer)
        {
            glideTime += Time.fixedDeltaTime;
        }

        // Implement our glide
        result = moveInput.y * glideSpeed * transform.forward + moveInput.x * glideSpeed * transform.right;
        
        result.y = rb.linearVelocity.y + gravityFalloff.Evaluate(glideTime/glideTimer) * Physics.gravity.y * Time.deltaTime;

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
