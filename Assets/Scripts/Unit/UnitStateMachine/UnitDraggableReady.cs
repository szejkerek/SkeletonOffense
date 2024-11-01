using UnityEngine;

public class UnitDraggableReady : UnitState
{
    public UnitDraggableReady(UnitStateMachine context) : base(context)
    {
        StateName = "Draggable Ready";
        StateColor = Color.green;
    }

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {

    }
}