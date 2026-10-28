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

    //[Header("Glide")] // Add all of our glide variables

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Enable your glide button
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

        
        // Glide your duck
        

        rb.linearVelocity = targetVelocity;
    }

    Vector3 GlideVelocity()
    {
        Vector3 result = Vector3.zero;

        // Count down our glide timer


        // Implement our glide


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
