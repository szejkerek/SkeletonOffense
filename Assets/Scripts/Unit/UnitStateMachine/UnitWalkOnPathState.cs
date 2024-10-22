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
        if(Unit.aggresive && IsOnAggroWaypoint()) 
        {
            context.ChangeState(new UnitComeToTower(context, targetInfo));
            return;
        }

        WalkManager.WalkAlongSpline();  
    }

    public override void ExitState()
    {
    }

    bool IsOnAggroWaypoint()
    {
        foreach(var targetInfo in Unit.targets)
        {
            if (targetInfo.target != null)
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
