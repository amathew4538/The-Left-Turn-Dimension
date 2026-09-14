using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform playerBody;
    
    [Header("Actions")]
    public InputAction lookAction;
    public InputAction unlockCursorAction;
    public InputAction lockCursorAction;

    [Header("Settings")]
    public float sensitivity = 0.1f;
    public float upperLookLimit = 80f;
    public float lowerLookLimit = -80f;

    private float xRotation = 0f;

    void OnEnable()
    {
        lookAction.Enable();
        unlockCursorAction.Enable();
        lockCursorAction.Enable();
    }

    void OnDisable()
    {
        lookAction.Disable();
        unlockCursorAction.Disable();
        lockCursorAction.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 currentRotation = transform.localEulerAngles;
        xRotation = currentRotation.x;
        if (xRotation > 180) xRotation -= 360f;
    }

    void Update()
    {
        HandleCursorLock();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Vector2 mouseDelta = lookAction.ReadValue<Vector2>();

            float mouseX = mouseDelta.x * sensitivity * Time.unscaledDeltaTime * 100f;
            float mouseY = mouseDelta.y * sensitivity * Time.unscaledDeltaTime * 100f;

            // Rotate player body left/right
            playerBody.Rotate(Vector3.up * mouseX);

            // Rotate camera up/down
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, lowerLookLimit, upperLookLimit);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    private void HandleCursorLock()
    {
        if (unlockCursorAction.WasPressedThisFrame())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (lockCursorAction.WasPressedThisFrame() && Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
