using UnityEngine;

public interface IDragListener
{
    void OnDragStart(UnitDraggingManager unit);  // Wywo³ywana przy rozpoczêciu przeci¹gania
    void OnDragEnd(UnitDraggingManager unit);    // Wywo³ywana przy zakoñczeniu przeci¹gania
}
