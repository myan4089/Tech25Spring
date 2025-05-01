using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    public float bobSpeed = 10f;
    public float bobAmount = 0.05f;

    private CharacterController controller;
    private Camera cam;
    private float pitch = 0f;
    private Vector3 velocity;
    private float defaultCamY;
    private float bobTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        defaultCamY = cam.transform.localPosition.y;
    }

    void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -89f, 89f);
        cam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // WASD movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Gravity & jump
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            velocity.y = jumpForce;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Head bob
        HandleHeadBob(move);
    }

    void HandleHeadBob(Vector3 move)
    {
        if (controller.isGrounded && move.magnitude > 0.1f)
        {
            bobTimer += Time.deltaTime * bobSpeed;
            float bobOffset = Mathf.Sin(bobTimer) * bobAmount;
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, defaultCamY + bobOffset, cam.transform.localPosition.z);
        }
        else
        {
            Vector3 camPos = cam.transform.localPosition;
            camPos.y = Mathf.Lerp(camPos.y, defaultCamY, Time.deltaTime * bobSpeed);
            cam.transform.localPosition = camPos;
            bobTimer = 0f;
        }
    }
}