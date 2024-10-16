using UnityEngine;

public class DraggableUnit : MonoBehaviour
{
    private Camera mainCamera;  
    private Vector3 offset;     
    private bool isDragging = false;
    private Vector3 originalPosition;
    

    public CampBasicSlot currentSlot;    
    public float fixedHeight = 0.5f;
    public int updateCost = 50;

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

            CampBasicSlot validSlot = GetSlotUnderUnit();
            if (validSlot != null)
            {
                if(!validSlot.IsSlotOccupied())
                {
                    originalPosition = validSlot.snapPoint.position;
                    MoveToSlotPosition();


                    if (currentSlot != null)
                    {
                        currentSlot.SetUnitOnSlot(null);
                        currentSlot = null;
                    }

                    currentSlot = validSlot;
                    currentSlot.SetUnitOnSlot(this);
                }
                else
                {
                    //zamiana
                    DraggableUnit unitToChangeWith = validSlot.unitOnSlot;
                    currentSlot.SetUnitOnSlot(unitToChangeWith);
                    unitToChangeWith.originalPosition = currentSlot.snapPoint.position;
                    unitToChangeWith.MoveToSlotPosition();

                    currentSlot = validSlot;
                    currentSlot.SetUnitOnSlot(this);

                    originalPosition = currentSlot.snapPoint.position;
                    MoveToSlotPosition();
                }

                
            }
            else
            {
                MoveToSlotPosition();

            }
        }
    }

    public void MoveToSlotPosition()
    {
        transform.position = originalPosition;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }


    private CampBasicSlot GetSlotUnderUnit()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position , Vector3.down);

        if (Physics.Raycast(ray, out hit, 20f))
        {
            CampBasicSlot slot = hit.collider.GetComponent<CampBasicSlot>();
            if (slot != null)
            {
                return slot;
            }
        }
        return null;
    }
}
