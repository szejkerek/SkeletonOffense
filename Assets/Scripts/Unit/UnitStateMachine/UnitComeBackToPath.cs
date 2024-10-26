using UnityEngine;

public class UnitComeBackToPath : UnitState
{
    Waypoint nextWaypoint;
    public UnitComeBackToPath(UnitStateMachine context) : base(context)
    {
        StateName = "ComeBackToPath";
        StateColor = Color.green;
    }

    public override void EnterState()
    {
        if(WalkManager == null || WalkManager.UnitOnPath())
        {
            context.ChangeState(new UnitIdleState(context));
            return; 
        }
        
        nextWaypoint = SplineManager.GetNext(WalkManager.splinePosition);
        WalkManager.MoveToPoint(nextWaypoint.position);      
        
    }

    public override void UpdateState()
    {
        if (WalkManager.HasReachedDestination())
        {
            context.ChangeState(new UnitWalkOnPathState(context));
            return;
        }
    }

    public override void ExitState()
    {
        if(WalkManager == null)
        {
            return;
        }

        WalkManager.StopNavMeshMovement();
        WalkManager.splinePosition = nextWaypoint.percentage;
    }
}