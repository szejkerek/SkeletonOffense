using System;
using UnityEngine;

public class UnityDraggingManager : MonoBehaviour
{
    public static UnityDraggingManager Instance; 

    public event Action<DraggableUnit> OnDragStart;
    public event Action<DraggableUnit> OnDragEnd;

    private DraggableUnit currentDraggedUnit; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsDragging()
    {
        return currentDraggedUnit != null;
    }

    public void StartDragging(DraggableUnit draggable)
    {
        if (!IsDragging())
        {
            currentDraggedUnit = draggable;
            OnDragStart?.Invoke(draggable);
        }
    }

    public void StopDragging()
    {
        if (IsDragging())
        {
            OnDragEnd?.Invoke(currentDraggedUnit);
            currentDraggedUnit = null;
        }
    }

    public DraggableUnit GetCurrentDraggedObject()
    {
        return currentDraggedUnit;
    }
}
