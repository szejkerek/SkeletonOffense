using TMPro;
using UnityEngine;

public class UnitComeToTarget : UnitState
{
    readonly TargetInfo targetInfo;
    public UnitComeToTarget(UnitStateMachine context, TargetInfo targetInfo) : base(context)
    {
        StateName = "Come To target";
        StateColor = Color.magenta;
        this.targetInfo = targetInfo;
    }

    public override void EnterState()
    {
        UnitNavMeshWalker.MoveToPoint(targetInfo.sampledStandPosition);
    }

    public override void UpdateState()
    {
        if (UnitNavMeshWalker.HasReachedDestination())
        {
            Context.ChangeState(new UnitAttack(Context, targetInfo));
            return;
        }
    }

    public override void ExitState()
    {

    }
}