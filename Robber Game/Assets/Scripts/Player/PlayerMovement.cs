using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 5f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f;
    public Transform cameraHolder; // Camera assign garannu parcha

    [Header("Ground Check")]
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private float xRotation = 0f;
    private bool isGrounded;

    private float moveX;
    private float moveZ;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // rotation freeze garne, physics le rotate nagaros vanera yk collision n stuff
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // fps ma cursor lock garne
    }

    void Update()
    {
        HandleMouseLook();
        HandleInput();
        CheckGround();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    // ---------------------------------
    // INPUT
    // ---------------------------------

    void HandleInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    // ---------------------------------
    // MOVEMENT (Physics based)
    // ---------------------------------

    void HandleMovement()
    {
        // Calculate movement direction relative to player (bujhyeu ni?)
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        // Keep current Y velocity (important for gravity) (newtoooon)
        Vector3 velocity = moveDirection.normalized * moveSpeed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }

    void Jump()
    {
        // Reset vertical velocity before jump (cleaner jump) (yo le jump ko height consistent banauxa, otherwise jump force apply garda velocity already downwards cha bhane jump height kam hunchha)
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // ---------------------------------
    // MOUSE LOOK
    // ---------------------------------

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical camera rotation (yo le camera lai up down ghumauxa, player body lai ghumauxa ni, but camera holder lai matra up down ghumauxa)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal player rotation (yo le player body lai left right ghumauxa, camera holder lai ghumauxa ni, but camera holder lai matra up down ghumauxa)
        transform.Rotate(Vector3.up * mouseX);
    }

    // ---------------------------------
    // GROUND CHECK
    // ---------------------------------

    void CheckGround()
    {
        // Raycast downward to check if grounded (yo le player ko position bata thap distance samma raycast garera ground layer ma hit cha ki nai check garauxa)
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundCheckDistance + 0.1f,
            groundLayer
        );
    }
}