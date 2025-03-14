using UnityEngine;
using UnityEngine.EventSystems;

public class Camera_Look : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform; // Assign your camera in inspector

    [Header("Settings")]
    public float horizontalSensitivity = 0.4f;
    public float verticalSensitivity = 0.4f;
    public float minVerticalAngle = -80f;
    public float maxVerticalAngle = 80f;

    private float xRotation; // Vertical camera rotation
    private float yRotation; // Horizontal player rotation
    private bool isDragging = false; // Track if user is actively dragging

    void Start()
    {
        // Initialize rotations with current values
        yRotation = transform.eulerAngles.y;
        xRotation = cameraTransform.localEulerAngles.x;

        // Convert initial camera rotation to -180/180 range
        xRotation = NormalizeAngle(xRotation);
    }

    // These public wrapper methods are now visible in the Inspector.
    public void HandlePointerDown(BaseEventData data)
    {
        PointerEventData eventData = data as PointerEventData;
        if (eventData != null)
        {
            isDragging = true;
        }
    }

    public void HandlePointerUp(BaseEventData data)
    {
        PointerEventData eventData = data as PointerEventData;
        if (eventData != null)
        {
            isDragging = false;
        }
    }

    public void HandleDrag(BaseEventData data)
    {
        PointerEventData eventData = data as PointerEventData;
        if (eventData != null && isDragging)
        {
            // Get touch delta
            Vector2 touchDelta = eventData.delta;

            // Horizontal rotation (player)
            yRotation += touchDelta.x * horizontalSensitivity;
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            // Vertical rotation (camera)
            xRotation -= touchDelta.y * verticalSensitivity;
            xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360;
        if (angle > 180) angle -= 360;
        return angle;
    }
}
