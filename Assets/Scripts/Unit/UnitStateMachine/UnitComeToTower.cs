using TMPro;
using UnityEngine;

public class UnitComeToTower : UnitState
{
    TargetInfo targetInfo;
    public UnitComeToTower(UnitStateMachine context, TargetInfo targetInfo) : base(context)
    {
        StateName = "Come To Tower";
        StateColor = Color.magenta;
        this.targetInfo = targetInfo;
    }

    public override void EnterState()
    {
        WalkManager.MoveToPoint(targetInfo.sampledStandPosition);
    }

    public override void UpdateState()
    {
        if (WalkManager.HasReachedDestination())
        {
            return;
        }
    }

    public override void ExitState()
    {

    }
}