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
            Context.ChangeState(new UnitComeToTower(Context, targetInfo));
            return;
        }

        WalkManager.WalkAlongSpline();  
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
                if (Mathf.Abs(targetInfo.nearestWaypoint.percentage - WalkManager.splinePosition) <= 0.05f)
                {
                    this.targetInfo = targetInfo;
                    return true;
                }
            }
        }

        return false;
    }
}
