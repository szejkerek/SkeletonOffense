using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CampBasicSlot : MonoBehaviour, IDragListener, IDragPutTarget
{
    public Transform snapPoint;
    public UnitDraggingManager unitOnSlot;


    public MeshRenderer visualMeshRenderer;
    public List<ParticleSystem> ViusalEffects = new List<ParticleSystem>();


    private void Start()
    {
        
        UnitDraggingManager.OnDragStart += OnDragStart;
        UnitDraggingManager.OnDragEnd += OnDragEnd;
        ViusalEffects = GetComponentsInChildren<ParticleSystem>().ToList();

        foreach (ParticleSystem p in ViusalEffects)
        {
            p.Pause();
            p.Clear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Draggable"))
        {
            UnitDraggingManager draggable = other.GetComponent<UnitDraggingManager>();
            if (UnitDraggingManager.IsSthDragged) SetStateColor(draggable, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Draggable"))
        {
            UnitDraggingManager draggable = other.GetComponent<UnitDraggingManager>();
            if(UnitDraggingManager.IsSthDragged) SetStateColor(draggable);
        }
    }

    public void SetUnitOnSlot(UnitDraggingManager unit)
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
    public UnitDraggingManager GetUnitOnSlot()
    {
        return unitOnSlot;
    }

    public void SetStateColor(UnitDraggingManager unit, bool inRange = false)
    {
        if (IsSlotUnLocked())
        {
            foreach (ParticleSystem p in ViusalEffects)
            {
                p.Pause();
                p.Clear();
            }

            if (inRange)
            {
                if (unitOnSlot != null && unitOnSlot != unit)
                {
                    //naje�d�asz innym unitem i co� innego tu jest
                    //visualMeshRenderer.material = slotHoverFullMaterial;
                    foreach (ParticleSystem p in ViusalEffects)
                    {
                        var main = p.main;
                        main.startColor = new Color(255, 255, 0);
                        p.Emit(1);
                        p.Play();
                    }
                }
                else
                {
                    //naje�d�asz unitem a jest pusto
                    //visualMeshRenderer.material = slotHoverEmptyMaterial;
                    foreach (ParticleSystem p in ViusalEffects)
                    {
                        var main = p.main;
                        main.startColor = new Color(0, 255, 255);
                        p.Emit(1);
                        p.Play();
                    }
                }
            }
            else
            {
                if (unit != null && !IsSlotOccupied())
                {
                    //podniesiony unit w oddali
                    //visualMeshRenderer.material = slotFullMaterial;
                    foreach (ParticleSystem p in ViusalEffects)
                    {
                        var main = p.main;
                        main.startColor = new Color(0, 255, 0);
                        p.Emit(1);
                        p.Play();
                    }
                }
                else
                {
                    //zako�czenie nic, nie jest przenoszone
                    //visualMeshRenderer.material = slotEmptyMaterial;
                }
            }

        }
        else
        {
            //visualMeshRenderer.material = slotHoverFullMaterial;
        }
    }

    public void OnDragStart(UnitDraggingManager unit)
    {
        SetStateColor(unit);
    }

    public void OnDragEnd(UnitDraggingManager unit)
    {
        SetStateColor(null);
    }

    public bool PutUnit(UnitDraggingManager unit)
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

    private void OnDestroy()
    {
        UnitDraggingManager.OnDragStart -= OnDragStart;
        UnitDraggingManager.OnDragEnd -= OnDragEnd;
    }
}
