using System;
using UnityEngine;

public class UnityDraggingManager : MonoBehaviour
{
    public static UnityDraggingManager Instance; 

    public event Action<UnitDraggingManager> OnDragStart;
    public event Action<UnitDraggingManager> OnDragEnd;

    private UnitDraggingManager currentDraggedUnit; 

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

    public void StartDragging(UnitDraggingManager draggable)
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

    public UnitDraggingManager GetCurrentDraggedObject()
    {
        return currentDraggedUnit;
    }
}
