using UnityEngine;

public class UnitWalkOnPathState : UnitState
{
    TargetInfo targetInfo;
    public UnitWalkOnPathState(UnitStateMachine stateMachine) : base(stateMachine) 
    {
        StateName = "Walking on path";
        StateColor = Color.blue;
    }

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        if(Unit.Agressive && IsOnAggroWaypoint()) 
        {
            Context.ChangeState(new UnitComeToTarget(Context, targetInfo));
            return;
        }

        UnitSplineWalker.WalkAlongSpline();  
    }

    public override void ExitState()
    {
    }

    bool IsOnAggroWaypoint()
    {
        foreach(var targetInfo in UnitAttackManager.targets)
        {
            if (targetInfo.target != null && targetInfo.target.IsAlive)
            {
                if (Mathf.Abs(targetInfo.nearestWaypoint.percentage - UnitSplineWalker.SplinePosition) <= 0.05f)
                {
                    this.targetInfo = targetInfo;
                    return true;
                }
            }
        }

        return false;
    }
}
