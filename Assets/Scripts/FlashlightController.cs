using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    [Header("References")]
    public GameObject lightEmitter;

    [Header("Actions")]
    public InputAction toggleFlashlight;

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
}
