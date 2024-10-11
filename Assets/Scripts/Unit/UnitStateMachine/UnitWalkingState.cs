using UnityEngine;

public class UnitWalkingState : UnitState
{
    UnitWalkManager walkManager;
    public UnitWalkingState(UnitStateMachine stateMachine) : base(stateMachine) 
    {
        walkManager = stateMachine.GetComponent<UnitWalkManager>();
    }

    public override void Enter()
    {
        Debug.Log("Entering Walking State");
    }

    public override void LogicUpdate()
    {
        Debug.Log("Walk");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Walking State");
    }
}
