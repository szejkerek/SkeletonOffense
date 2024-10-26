using UnityEngine;

public class DraggableUnit : MonoBehaviour
{
    private Camera mainCamera;  
    private Vector3 offset;     
    private bool isDragging = false;
    private Vector3 originalPosition;
    
    public UnitBlueprint unitBlueprint;
    public CampBasicSlot currentSlot;    
    public float fixedHeight = 0.5f;

    void Start()
    {
        mainCamera = Camera.main;
        if (currentSlot != null)
        {
            originalPosition = currentSlot.snapPoint.position;
            currentSlot.SetUnitOnSlot(this);
            MoveToSlotPosition();
        }
        else
        {
            originalPosition = transform.position;
        }
    }

    void OnMouseDown()
    {
        if (!UnityDraggingManager.Instance.IsDragging())
        {
            UnityDraggingManager.Instance.StartDragging(this);

            offset = gameObject.transform.position - GetMouseWorldPos();
            isDragging = true;
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = GetMouseWorldPos() + offset;
            newPosition.y = fixedHeight;
            transform.position = newPosition;
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;

            UnityDraggingManager.Instance.StopDragging();
            IDragPutTarget putTarget = GetPutTargetUnderUnit();
            if (putTarget != null)
            {
                putTarget.PutUnit(this);
            }
            else
            {
                MoveToSlotPosition();
            }
        }
    }

    public void SetCurrentSlot(CampBasicSlot slot)
    {
        originalPosition = slot.snapPoint.position;
        currentSlot = slot;
    }
    public void MoveToSlotPosition()
    {
        transform.position = originalPosition;
    }

    public UnitBlueprint GetUnitBlueprint()
    {
        return unitBlueprint;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private IDragPutTarget GetPutTargetUnderUnit()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position , Vector3.down);

        if (Physics.Raycast(ray, out hit, 20f))
        {
            IDragPutTarget putTarget = hit.collider.GetComponent<IDragPutTarget>();
            if (putTarget != null)
            {
                return putTarget;
            }
        }
        return null;
    }
}
