using UnityEngine;

public class UnitComeToTower : UnitState
{
    UnitWalkManager walkManager;
    public UnitComeToTower(UnitStateMachine context) : base(context)
    {
        StateName = "ComeToTower";
        StateColor = Color.magenta;
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