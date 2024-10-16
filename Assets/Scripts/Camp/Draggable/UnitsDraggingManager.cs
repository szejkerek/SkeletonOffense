using System;
using UnityEngine;

public class UnityDraggingManager : MonoBehaviour
{
    public static UnityDraggingManager Instance;  // Singleton, aby mieæ globalny dostêp

  
    public event Action<DraggableUnit> OnDragStart;
    public event Action<DraggableUnit> OnDragEnd;

    private DraggableUnit currentDraggedUnit;  // Obiekt, który jest aktualnie przeci¹gany

    private void Awake()
    {
        // Upewniamy siê, ¿e mamy tylko jedn¹ instancjê UnityDraggingManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Sprawdza, czy jakiœ obiekt jest aktualnie przeci¹gany
    public bool IsDragging()
    {
        return currentDraggedUnit != null;
    }

    // Zaczyna przeci¹ganie obiektu
    public void StartDragging(DraggableUnit draggable)
    {
        if (!IsDragging())
        {
            currentDraggedUnit = draggable;
            OnDragStart?.Invoke(draggable);
        }
    }

    // Koñczy przeci¹ganie obiektu
    public void StopDragging()
    {
        if (IsDragging())
        {
            OnDragEnd?.Invoke(currentDraggedUnit);
            currentDraggedUnit = null;
        }
    }

    // Zwraca obiekt, który jest przeci¹gany
    public DraggableUnit GetCurrentDraggedObject()
    {
        return currentDraggedUnit;
    }
}
