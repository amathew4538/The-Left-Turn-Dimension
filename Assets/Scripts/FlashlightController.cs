using Unity.Mathematics;
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

    [Header("Offsets")]
    public float rightOffset = 0.2f;
    public float forwardOffset = 0.15f;
    public float upOffset = -0.1f;

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
            float xDegrees = cameraTransform.localEulerAngles.x;
            float yDegrees = playerTransform.eulerAngles.y;

            Quaternion targetRotation = Quaternion.Euler(xDegrees, yDegrees, 0f);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1.0f - Mathf.Exp(-smoothSpeed * Time.deltaTime));

            Vector3 localOffset = new(rightOffset, upOffset, forwardOffset);

            Vector3 targetPosition = cameraTransform.position + (transform.rotation * localOffset);
            transform.position = targetPosition;
        }
    }
}
