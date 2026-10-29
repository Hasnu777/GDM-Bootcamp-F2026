using UnityEngine;
using UnityEngine.InputSystem;

public class BetterCameraController : MonoBehaviour
{
    // Store a reference to the camera and pivot Transform.
    public Transform cameraPivot;

    // Create Rotation, Sensitivity, MinMax variables
    public Vector2 rotation;
    public Vector2 sensitivity;
    public Vector2 MinMax = new Vector2(-40, 40);

    // Start is called before the first frame update.
    void Start()
    {
        // Lock Cursor here
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void OnLook(InputValue value)
    {
        rotation += Vector2.Scale(value.Get<Vector2>(), sensitivity);
    }

    void FixedUpdate()
    {
        // Clamp Rotation
        rotation.y = Mathf.Clamp(rotation.y, MinMax.x, MinMax.y);

        // Apply Rotation
        cameraPivot.localRotation = Quaternion.Euler(-rotation.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, rotation.x, 0);
    }
}