using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float acceleration = 10f;
    public float groundDamping = 5f;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float staminaDrainRate = 15f;
    public float staminaRegenRate = 10f;
    private float currentStamina;

    [Header("Look & Camera")]
    public Transform cameraRoot;
    public float mouseSensitivity = 2f;
    private float xRotation = 0f;

    [Header("Interaction (Future Proofing)")]
    public float interactRange = 3f;
    public LayerMask interactableLayer;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isSprinting;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentStamina = maxStamina;

        // Lock cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleLook();
        HandleInput();
        HandleStamina();
        // CheckInteraction();
    }

    void FixedUpdate()
    {
        MovePlayer();
        // Unity 6 uses linearDamping instead of drag
        rb.linearDamping = groundDamping; 
    }

    private void HandleLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        // Vertical look (Pitch) - applied to the CameraRoot
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal look (Yaw) - applied to the whole Player body
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleInput()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // Allow sprinting only if we have stamina and are moving forward
        isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f && moveInput.y > 0f;
    }

    private void HandleStamina()
    {
        if (isSprinting && moveInput.magnitude > 0.1f)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Max(currentStamina, 0f);
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        Vector3 targetVelocity = moveDir.normalized * currentSpeed;

        // Unity 6 uses linearVelocity instead of velocity
        Vector3 velocityDiff = targetVelocity - new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        
        // Smoothly accelerate using ForceMode.VelocityChange
        rb.AddForce(velocityDiff * acceleration * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    // private void CheckInteraction()
    // {
    //     // Fire a raycast from the camera when 'E' is pressed
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            
    //         if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
    //         {
    //             // Look for the IInteractable interface
    //             IInteractable interactable = hit.collider.GetComponent<IInteractable>();
    //             if (interactable != null)
    //             {
    //                 interactable.Interact();
    //             }
    //         }
    //     }
    // }
}