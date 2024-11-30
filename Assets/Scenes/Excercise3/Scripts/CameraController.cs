using UnityEngine;

public class CameraController : MonoBehaviour
{
    // nếu  muốn di chuyển camera 1 độ tương ứng với khi di chuyển chuột 1 pixel thì mình cho rotationSpeed = 1;
    private float rotationSpeed = 5f;
    private float minAngle = -50f;
    private float maxAngle = 50f;

    private float currentAngle = 0f;
    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void Update()
    {
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotationAmount = delta.x * rotationSpeed * Time.deltaTime;
            currentAngle += rotationAmount;
            currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);
            transform.rotation = Quaternion.Euler(0, -currentAngle, 0);
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
