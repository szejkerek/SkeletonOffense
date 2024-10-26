using UnityEngine;

public class UnitIdleState : UnitState
{
    public UnitIdleState(UnitStateMachine context) : base(context)
    {
        StateName = "Idle";
        StateColor = Color.gray;
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