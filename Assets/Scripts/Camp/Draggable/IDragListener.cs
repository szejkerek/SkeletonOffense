using UnityEngine;

public interface IDragListener
{
    void OnDragStart(DraggableUnit unit);  // Wywo³ywana przy rozpoczêciu przeci¹gania
    void OnDragEnd(DraggableUnit unit);    // Wywo³ywana przy zakoñczeniu przeci¹gania
}
