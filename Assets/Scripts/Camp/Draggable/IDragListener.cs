using UnityEngine;

public interface IDragListener
{
    void OnDragStart(UnitDraggingManager unit);  // Wywoływana przy rozpoczęciu przeciągania
    void OnDragEnd(UnitDraggingManager unit);    // Wywoływana przy zakończeniu przeciągania
}
