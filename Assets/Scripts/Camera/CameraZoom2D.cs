using UnityEngine;

using Unity.Cinemachine;

public class CameraZoom2D : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CinemachineCamera virtualCamera;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomInSize = 7f;   // kích thước khi zoom gần
    [SerializeField] private float zoomOutSize = 25f;  // kích thước khi zoom xa
    [SerializeField] private float zoomSpeed = 2f;    // tốc độ zoom

    private float targetSize;

    void Start()
    {
        // Gán kích thước ban đầu của camera
        targetSize = virtualCamera.Lens.OrthographicSize;
    }

    void Update()
    {
        // ✅ Ví dụ: khi nhấn chuột phải, đổi trạng thái zoom
        if (Input.GetMouseButtonDown(1))
        {
            if (Mathf.Abs(targetSize - zoomOutSize) < 0.1f)
                targetSize = zoomInSize;
            else
                targetSize = zoomOutSize;
        }

        // ✅ Zoom mượt mà theo thời gian
        float currentSize = virtualCamera.Lens.OrthographicSize;
        virtualCamera.Lens.OrthographicSize =
            Mathf.Lerp(currentSize, targetSize, Time.deltaTime * zoomSpeed);
    }
}
