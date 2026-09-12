using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;
    public Transform playerTransform;
    public GameObject lightEmitter;

    [Header("Actions")]
    public InputAction toggleFlashlight;

    [Header("Settings")]
    public float smoothSpeed = 7.5f;

    [Header("Debug")]
    [SerializeField] private Light light;

    void OnEnable()
    {
        toggleFlashlight.Enable();
    }

    void OnDisable()
    {
        toggleFlashlight.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (lightEmitter != null) {
            light = lightEmitter.GetComponent<Light>();
        }
        else
        {
            light = GetComponent<Light>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleFlashlight.WasPressedThisFrame())
        {
            light.enabled = !light.enabled;
        }
    }

    void LateUpdate()
    {
        if (cameraTransform != null && playerTransform != null)
        {
            transform.position = cameraTransform.position + (cameraTransform.right * 0.2f) + (cameraTransform.forward * 0.15f) - (cameraTransform.up * 0.1f);

            float xDegrees = cameraTransform.localEulerAngles.x;
            float yDegrees = playerTransform.eulerAngles.y;

            Quaternion targetRotation = Quaternion.Euler(xDegrees, yDegrees, 0f);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.fixedDeltaTime);
        }
    }
}
