using UnityEngine;

public class CubeController : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;
    private Plane quadPlane;
    private GameObject floor;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        mainCamera = Camera.main;
        floor = GameObject.Find("Floor");
        quadPlane = new Plane(Vector3.up, floor.transform.position);
    }

    private void Update()
    {
        HandleCubeMovement();
    }

    private void HandleCubeMovement()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    isDragging = true;
                    offset = transform.position - hit.point;
                }
            }
        }

        if (isDragging)
        {
            float distance;
            if (quadPlane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                transform.position = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
