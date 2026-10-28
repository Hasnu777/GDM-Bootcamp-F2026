using UnityEngine;
using UnityEngine.InputSystem;

public class BetterCameraController : MonoBehaviour
{
    // Store a reference to the camera and pivot Transform.
    public Transform cameraPivot;

    // Create Rotation, Sensitivity, MinMax variables
    public Vector2 rotation;
    public Vector2 sensitivity = new Vector2(0.2f, 0.2f);
    public Vector2 minMax = new Vector2(-40, 40);

    // Start is called before the first frame update.
    void Start()
    {
        // Lock Cursor here
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnLook(InputValue value)
    {
        Vector2 delta = value.Get<Vector2>();
        rotation += Vector2.Scale(delta, sensitivity);
    }

    void FixedUpdate()
    {
        // Clamp Rotation
        rotation.y = Mathf.Clamp(rotation.y, minMax.x, minMax.y);

        // Apply Rotation
        cameraPivot.localRotation = Quaternion.Euler(-rotation.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, rotation.x, 0);
    }
}