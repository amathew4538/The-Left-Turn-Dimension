using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Transform playerFlashlight;

    [Header("Movement Actions")]
    public InputAction moveAction;
    public InputAction jumpAction;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpHeight = 10f;
    public float gravity = -9.81f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Debug")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Vector2 inputVector;
    [SerializeField] private Vector3 currentVelocity;

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    void OnDisable()
    { 
        moveAction.Disable(); 
        jumpAction.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && currentVelocity.y < 0)
        {
            currentVelocity.y = -2f;
        }

        inputVector = moveAction.ReadValue<Vector2>();

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * inputVector.y) + (right * inputVector.x);

        currentVelocity.x = moveDirection.x * speed;
        currentVelocity.z = moveDirection.z * speed;

        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            currentVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        currentVelocity.y += gravity * Time.deltaTime;

        characterController.Move(currentVelocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a gizmo to visualize the ground check area
        bool checkGrounded = Application.isPlaying ? isGrounded : Physics.CheckSphere(groundCheck.transform.position, groundCheckRadius, groundLayer);

        // Set the gizmo color based on whether the player is grounded or not
        Gizmos.color = checkGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }
}
