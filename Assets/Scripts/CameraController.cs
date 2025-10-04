using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target;
    public float followSpeed = 5f;
    public float rotationSpeed = 2f;
    public Vector3 offset = new Vector3(0, 10, -10);
    
    [Header("Zoom Settings")]
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float zoomSpeed = 2f;
    
    [Header("Board Focus")]
    public Transform boardCenter;
    public bool focusOnBoard = true;
    public float boardViewHeight = 12f;
    
    private Camera cam;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        
        if (focusOnBoard && boardCenter != null)
        {
            FocusOnBoard();
        }
    }
    
    void LateUpdate()
    {
        HandleInput();
        
        if (target != null && !focusOnBoard)
        {
            FollowTarget();
        }
    }
    
    void HandleInput()
    {
        // Mouse scroll for zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            ZoomCamera(-scroll * zoomSpeed);
        }
        
        // Right mouse button for camera rotation
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
            
            transform.RotateAround(boardCenter.position, Vector3.up, mouseX);
            transform.RotateAround(boardCenter.position, transform.right, -mouseY);
        }
        
        // Middle mouse button to reset camera
        if (Input.GetMouseButtonDown(2))
        {
            ResetCamera();
        }
        
        // F key to focus on board
        if (Input.GetKeyDown(KeyCode.F))
        {
            FocusOnBoard();
        }
    }
    
    void FollowTarget()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        
        transform.LookAt(target);
    }
    
    void ZoomCamera(float zoomAmount)
    {
        if (cam.orthographic)
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + zoomAmount, minZoom, maxZoom);
        }
        else
        {
            Vector3 newPosition = transform.position + transform.forward * zoomAmount;
            transform.position = newPosition;
        }
    }
    
    public void FocusOnBoard()
    {
        if (boardCenter == null) return;
        
        focusOnBoard = true;
        
        // Position camera to view entire board
        Vector3 boardPosition = boardCenter.position;
        Vector3 cameraPosition = boardPosition + new Vector3(0, boardViewHeight, -8);
        
        transform.position = cameraPosition;
        transform.LookAt(boardPosition);
        
        // Adjust camera settings for board view
        if (cam.orthographic)
        {
            cam.orthographicSize = 8f;
        }
    }
    
    public void FollowPlayer(Transform playerTransform)
    {
        target = playerTransform;
        focusOnBoard = false;
    }
    
    public void ResetCamera()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        
        if (cam.orthographic)
        {
            cam.orthographicSize = 10f;
        }
        
        focusOnBoard = true;
    }
    
    public void SetCameraMode(bool orthographic)
    {
        cam.orthographic = orthographic;
        
        if (orthographic)
        {
            cam.orthographicSize = 10f;
        }
        else
        {
            cam.fieldOfView = 60f;
        }
    }
    
    public void SmoothTransitionTo(Vector3 targetPosition, Quaternion targetRotation, float duration = 1f)
    {
        StartCoroutine(SmoothTransition(targetPosition, targetRotation, duration));
    }
    
    System.Collections.IEnumerator SmoothTransition(Vector3 targetPos, Quaternion targetRot, float duration)
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            // Smooth curve
            t = t * t * (3f - 2f * t);
            
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Lerp(startRot, targetRot, t);
            
            yield return null;
        }
        
        transform.position = targetPos;
        transform.rotation = targetRot;
    }
}