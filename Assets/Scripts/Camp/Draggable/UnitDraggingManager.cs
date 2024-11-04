using System;
using UnityEngine;

public class UnitDraggingManager : MonoBehaviour
{
    public static event Action<UnitDraggingManager> OnDragStart;
    public static event Action<UnitDraggingManager> OnDragEnd;
    public static bool IsSthDragged = false;

    public CampBasicSlot currentSlot; 
    
    private Camera mainCamera;  
    private Vector3 offset;     
    private bool isDragged = false;
    private Vector3 originalPosition;

    private Animator animator;
    private const string DRAGGING = "Dragging";
    //TODO more interesting way of keeping unit in set height
    public float fixedHeight = 0.5f;

    public void Init(UnitConfig config,int tier, CampBasicSlot slot,Animator animator)
    {
        GetUnitBlueprint().Config = config;
        GetUnitBlueprint().Tier = tier;
        slot?.PutUnit(this);
        this.animator = animator;
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        if (!IsSthDragged)
        {
            IsSthDragged = true;
            isDragged = true;
            OnDragStart?.Invoke(this);
            offset = gameObject.transform.position - GetMouseWorldPos();
            animator.SetBool(DRAGGING, true);
        }
    }

    void OnMouseDrag()
    {
        if (isDragged)
        {
            Vector3 newPosition = GetMouseWorldPos() + offset;
            newPosition.y = fixedHeight;
            transform.position = newPosition;
        }
    }

    void OnMouseUp()
    {
        if (isDragged)
        {
            isDragged = false;
            IsSthDragged = false;
            OnDragEnd?.Invoke(this);
            IDragPutTarget putTarget = GetPutTargetUnderUnit();
            if (putTarget != null)
            {
                putTarget.PutUnit(this);
            }
            else
            {
                MoveToSlotPosition();
            }
            animator.SetBool(DRAGGING, false);
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
        return GetComponent<Unit>().Blueprint;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private IDragPutTarget GetPutTargetUnderUnit()
    {
        LayerMask dragPutTargetLayer = 1 << 7;
        Vector3 rayOrigin = transform.position + new Vector3(0,50,0);
        Ray ray = new Ray(rayOrigin, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, dragPutTargetLayer))
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
