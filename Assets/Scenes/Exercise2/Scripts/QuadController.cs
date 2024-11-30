using UnityEngine;

public class QuadController : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 lastMousePosition;
    private Vector3 center;
    private float initialAngle;

    private void Awake()
    {
        Initialize();
    }

    void Update()
    {
        HandleQuadRotation();
    }

    private void Initialize()
    {
        mainCamera = Camera.main;
        center = transform.position;
    }

    private void HandleQuadRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverQuad())
            {
                isDragging = true;
                lastMousePosition = Input.mousePosition;
                initialAngle = GetAngleFromCenter(lastMousePosition);
            }
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float currentAngle = GetAngleFromCenter(currentMousePosition);
            float angleDelta = currentAngle - initialAngle;
            transform.Rotate(0, 0, angleDelta);
            lastMousePosition = currentMousePosition;
            initialAngle = currentAngle;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private bool IsMouseOverQuad()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform == transform;
        }

        return false;
    }

    private float GetAngleFromCenter(Vector3 mousePosition)
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));
        Vector3 direction = mouseWorldPosition - center;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}
