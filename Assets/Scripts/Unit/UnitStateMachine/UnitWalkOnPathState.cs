using UnityEngine;

public class UnitWalkOnPathState : UnitState
{
    public UnitWalkOnPathState(UnitStateMachine stateMachine) : base(stateMachine) 
    {
        StateName = "Walking on path";
        StateColor = Color.blue;
    }

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        WalkManager.WalkAlongSpline();  
    }

    public override void ExitState()
    {
    }
}
