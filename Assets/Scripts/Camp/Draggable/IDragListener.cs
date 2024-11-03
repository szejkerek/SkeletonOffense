using UnityEngine;

public interface IDragListener
{
    void OnDragStart(UnitDraggingManager unit);  // Wywo�ywana przy rozpocz�ciu przeci�gania
    void OnDragEnd(UnitDraggingManager unit);    // Wywo�ywana przy zako�czeniu przeci�gania
}
