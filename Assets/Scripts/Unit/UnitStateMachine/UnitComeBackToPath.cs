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
        walkManager.MoveToPoint(walkManager.GetOffsetSplinePositionByLength());      
    }

    public override void UpdateState()
    {
        if (walkManager.HasReachedDestination())
        {
            context.ChangeState(new UnitWalkOnPathState(context));
            return;
        }
    }

    public override void ExitState()
    {
        walkManager.StopNavMeshMovement();
    }
}

public class UnitComeToTower : UnitState
{
    UnitWalkManager walkManager;
    public UnitComeToTower(UnitStateMachine context) : base(context)
    {
        StateName = "ComeToTower";
        StateColor = Color.magenta;

        walkManager = context.WalkManager;
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