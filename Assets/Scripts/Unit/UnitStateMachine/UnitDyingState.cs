using UnityEngine;

public class UnitDyingState : UnitState
{

    public UnitDyingState(UnitStateMachine context) : base(context)
    {
        StateName = "Dying";
        StateColor = Color.gray;
    }

    public override void EnterState()
    {
        UnitNavMeshWalker.StopNavMeshMovement();    
        Object.Destroy(Context.Unit.gameObject, 3f);
    }

    public override void UpdateState()
    {
        //Animate death
    }
    
    public override void ExitState()
    {

    }
}