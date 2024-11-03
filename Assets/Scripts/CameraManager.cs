using UnityEngine;
using DG.Tweening;
public class CameraManager : MonoBehaviour
{
    CameraConfig currentCameraConfig;
    float targetZoom;
    float currentZoom;
    public float zoomSpeed = 10f;
    public float panSpeed = 5f;

    bool changing;
    
    public void ApplyConfig(CameraConfig cameraConfig)
    {
        currentCameraConfig = cameraConfig;
        targetZoom = (currentCameraConfig.minZoom + currentCameraConfig.maxZoom) / 2;
        currentZoom = targetZoom;
        
        transform.position = new Vector3(
            currentCameraConfig.cameraStart.position.x,
            targetZoom,
            currentCameraConfig.cameraStart.position.z
        );
        
        changing = true;
        transform.DOMove(transform.position, 1f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => changing = false);

    }

    void Update()
    {
        if(changing)
        {
            return;
        }

        HandleZoom();
        HandlePan();
    }
    void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float newHeight = Mathf.Clamp(transform.position.y - scrollInput * zoomSpeed, currentCameraConfig.minZoom, currentCameraConfig.maxZoom);
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }

    private void HandlePan()
    {
        float moveZ = Input.GetAxis("Vertical") * panSpeed * Time.deltaTime;
        float moveX = Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position;
        newPosition.z += moveZ;
        newPosition.x = Mathf.Clamp(newPosition.x + moveX, -currentCameraConfig.xConstrains, currentCameraConfig.xConstrains);

        transform.position = newPosition;
    }
}
