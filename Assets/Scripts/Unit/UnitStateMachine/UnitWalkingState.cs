using UnityEngine;

public class UnitWalkingState : UnitState
{
    UnitWalkManager walkManager;
    public UnitWalkingState(UnitStateMachine stateMachine) : base(stateMachine) 
    {
        StateName = "Walking";
        StateColor = Color.blue;

        walkManager = stateMachine.WalkManager;
    }

    public override void Enter()
    {
        //Debug.Log("Entering Walking State");
    }

    public override void LogicUpdate()
    {
        walkManager.WalkAlongSpline();  
    }

    public override void Exit()
    {
        //Debug.Log("Exiting Walking State");
    }
}
