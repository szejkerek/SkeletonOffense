using UnityEngine;
using DG.Tweening;
public class CameraManager : MonoBehaviour
{
    CameraConfig currentCameraConfig;
    float targetZoom;
    public float zoomSpeed = 10f;
    public float panSpeed = 5f;

    bool changing;
    
    public void ApplyConfig(CameraConfig cameraConfig)
    {
        changing = true;
        currentCameraConfig = cameraConfig;
        targetZoom = (currentCameraConfig.minZoom + currentCameraConfig.maxZoom) / 2;
        
        Vector3 target = new Vector3(
            currentCameraConfig.cameraStart.position.x,
            targetZoom,
            currentCameraConfig.cameraStart.position.z
        );
        
        transform.DOMove(target, 1f)
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

    void HandlePan()
    {
        float moveZ = Input.GetAxis("Vertical") * panSpeed * Time.deltaTime;
        float moveX = Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position;
        newPosition.z += moveZ;
        
        newPosition.z = Mathf.Clamp(newPosition.z + moveZ, 
            currentCameraConfig.cameraStart.position.z,
            currentCameraConfig.cameraEnd.position.z);;
        
        newPosition.x = Mathf.Clamp(newPosition.x + moveX, 
            -currentCameraConfig.xConstrains, 
            currentCameraConfig.xConstrains);

        transform.position = newPosition;
    }
}
