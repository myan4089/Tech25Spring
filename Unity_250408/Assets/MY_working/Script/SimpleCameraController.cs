using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;

    float pitch = 0f;
    float yaw = 0f;

    void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void Update()
    {
        // Mouse Look
        yaw += Input.GetAxis("Mouse X") * lookSpeed;
        pitch -= Input.GetAxis("Mouse Y") * lookSpeed;
        pitch = Mathf.Clamp(pitch, -89f, 89f);
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);

        // WASD Movement
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) move += forward;
        if (Input.GetKey(KeyCode.S)) move -= forward;
        if (Input.GetKey(KeyCode.A)) move -= right;
        if (Input.GetKey(KeyCode.D)) move += right;

        transform.position += move * moveSpeed * Time.deltaTime;
    }
}