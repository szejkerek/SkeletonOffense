using UnityEngine;

public class UnitWalkOnPathState : UnitState
{
    UnitWalkManager walkManager;
    public UnitWalkOnPathState(UnitStateMachine stateMachine) : base(stateMachine) 
    {
        StateName = "Walking on path";
        StateColor = Color.blue;

        walkManager = stateMachine.WalkManager;
    }

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        walkManager.WalkAlongSpline();  
    }

    public override void ExitState()
    {
        //Debug.Log("Exiting Walking State");
    }
}
