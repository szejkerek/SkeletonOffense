using UnityEngine;

public class UnitComeBackToPath : UnitState
{
    UnitWalkManager walkManager;
    public UnitComeBackToPath(UnitStateMachine context) : base(context)
    {
        StateName = "ComeBackToPath";
        StateColor = Color.green;

        walkManager = context.WalkManager;
    }

    public override void EnterState()
    {
        if (!walkManager.MoveToPoint(walkManager.GetOffsetSplinePositionByLength()))
            Debug.LogWarning("Unit cannot find path to spline");
    }

    public override void UpdateState()
    {
        if (walkManager.HasReachedDestination())
            context.ChangeState(new UnitWalkOnPathState(context));
    }

    public override void ExitState()
    {
        walkManager.StopNavMeshMovement();
    }
}
