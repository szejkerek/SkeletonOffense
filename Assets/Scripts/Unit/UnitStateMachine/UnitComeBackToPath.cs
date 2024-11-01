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
        if(WalkManager == null || !WalkManager.UnitOnPath())
        {
            Context.ChangeState(new UnitIdleState(Context));
            return; 
        }
        
        nextWaypoint = SplineManager.GetNext(WalkManager.SplinePosition);
        WalkManager.MoveToPoint(nextWaypoint.position);      
        
    }

    public override void UpdateState()
    {
        if (WalkManager.HasReachedDestination())
        {
            Context.ChangeState(new UnitWalkOnPathState(Context));
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
        WalkManager.SetSpliePosition(nextWaypoint.percentage);
    }
}