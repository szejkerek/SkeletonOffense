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
        nextWaypoint = SplineManager.GetNext(WalkManager.splinePosition);
        WalkManager.MoveToPoint(nextWaypoint);      
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
        WalkManager.StopNavMeshMovement();
        WalkManager.splinePosition = nextWaypoint.percentage;
    }
}
