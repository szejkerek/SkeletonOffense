using UnityEngine;

public class CampBasicSlot : MonoBehaviour, IDragListener, IDragPutTarget
{
    public Transform snapPoint;
    public DraggableUnit unitOnSlot;

    public Material slotEmptyMaterial;
    public Material slotFullMaterial;
    public Material slotHoverEmptyMaterial;
    public Material slotHoverFullMaterial;
    public MeshRenderer visualMeshRenderer;

    private void Start()
    {
        UnityDraggingManager.Instance.OnDragStart += OnDragStart;
        UnityDraggingManager.Instance.OnDragEnd += OnDragEnd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Draggable"))
        {
            DraggableUnit draggable = other.GetComponent<DraggableUnit>();
            if (UnityDraggingManager.Instance.IsDragging()) SetStateColor(draggable, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Draggable"))
        {
            DraggableUnit draggable = other.GetComponent<DraggableUnit>();
            if(UnityDraggingManager.Instance.IsDragging()) SetStateColor(draggable);
        }
    }

    public void SetUnitOnSlot(DraggableUnit unit)
    {
        unitOnSlot = unit;
        if(unit!=null) unit.SetCurrentSlot(this);
    }

    public bool IsSlotOccupied()
    {
        return unitOnSlot != null;
    }
    public virtual bool IsSlotUnLocked()
    {
        return true;
    }
    public DraggableUnit GetUnitOnSlot()
    {
        return unitOnSlot;
    }

    public void SetStateColor(DraggableUnit unit, bool inRange = false)
    {
        if (IsSlotUnLocked())
        {
            
            if (inRange)
            {
                if (unitOnSlot != null && unitOnSlot != unit)
                {
                    //naje�d�asz innym unitem i co� innego tu jest
                    visualMeshRenderer.material = slotHoverFullMaterial;
                }
                else
                {
                    //naje�d�asz unitem a jest pusto
                    visualMeshRenderer.material = slotHoverEmptyMaterial;
                }
            }
            else
            {
                if (unit != null)
                {
                    //podniesiony unit w oddali
                    visualMeshRenderer.material = slotFullMaterial;
                }
                else
                {
                    //zako�czenie nic, nie jest przenoszone
                    visualMeshRenderer.material = slotEmptyMaterial;
                }
            }

        }
        else
        {
            visualMeshRenderer.material = slotHoverFullMaterial;
        }
    }

    public void OnDragStart(DraggableUnit unit)
    {
        SetStateColor(unit);
    }

    public void OnDragEnd(DraggableUnit unit)
    {
        SetStateColor(null);
    }

    public bool PutUnit(DraggableUnit unit)
    {
        if (!IsSlotUnLocked()) return false;

        if (unit == null)
        {
            SetUnitOnSlot(unit);
            return true;
        }
        
        if (IsSlotOccupied()) //zamiana
        {
            //Move unit form this slot to slot of dragged unit
            CampBasicSlot otherSlot = unit.currentSlot;
            otherSlot.SetUnitOnSlot(unitOnSlot);
            unitOnSlot.MoveToSlotPosition();
        }
        else if (unit.currentSlot != null)
        {
            //If there is no unit on the slot that we drag to we can clear connection to our previos slot
            unit.currentSlot.SetUnitOnSlot(null);
        }

        //Move dragged unit to this slot
        SetUnitOnSlot(unit);
        unitOnSlot.MoveToSlotPosition();

        return true;

    }
}
