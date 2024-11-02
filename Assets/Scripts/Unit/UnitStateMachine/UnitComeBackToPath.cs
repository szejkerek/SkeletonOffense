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
        if(UnitNavMeshWalker == null || !UnitNavMeshWalker.UnitOnPath())
        {
            Context.ChangeState(new UnitIdleState(Context));
            return; 
        }
        
        nextWaypoint = SplineManager.GetNext(UnitSplineWalker.SplinePosition);
        UnitNavMeshWalker.MoveToPoint(nextWaypoint.position);      
        
    }

    public override void UpdateState()
    {
        if (UnitNavMeshWalker.HasReachedDestination())
        {
            Context.ChangeState(new UnitWalkOnPathState(Context));
            return;
        }
    }

    public override void ExitState()
    {
        if(UnitNavMeshWalker == null)
        {
            return;
        }

        UnitNavMeshWalker.StopNavMeshMovement();
        UnitSplineWalker.SetSplinePosition(nextWaypoint.percentage);
    }
}