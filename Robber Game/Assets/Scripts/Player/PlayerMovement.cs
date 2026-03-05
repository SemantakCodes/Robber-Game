using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Sprint")]
    public float sprintSpeed = 10f;
    public float maxSprintStamina = 10f;
    public float currentSprintStamina;
    public float sprintDrainRate = 2f;
    public float sprintRegenRate = 1f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        currentSprintStamina = maxSprintStamina;
    }

    void Update()
    {
        print("is grounded" + controller.isGrounded);
        print("pressed" + Input.GetKeyDown(KeyCode.Space));
        
        Move();
        MouseLook();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Check if grounded
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;

        // Sprint handling
        float currentMoveSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && currentSprintStamina > 0)
        {
            currentSprintStamina -= sprintDrainRate * Time.deltaTime;
            currentMoveSpeed = sprintSpeed;
        }
        else
        {
            currentSprintStamina = Mathf.Min(currentSprintStamina + sprintRegenRate * Time.deltaTime, maxSprintStamina);
        }

        // Combine horizontal and vertical movement in ONE call
        Vector3 move = (transform.right * x + transform.forward * z) * currentMoveSpeed;
        move += Vector3.up * velocity.y;
        controller.Move(move * Time.deltaTime);
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}