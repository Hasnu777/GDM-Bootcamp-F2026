using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveInput;

    [Header("Movement")]
    public float moveSpeed = 8f;
    public float jumpForce = 6f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.6f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        Vector3 targetVelocity = new Vector3(moveInput.x * moveSpeed, rb.linearVelocity.y, moveInput.y * moveSpeed);
        rb.linearVelocity = targetVelocity;
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