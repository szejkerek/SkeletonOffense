using TMPro;
using UnityEngine;

public class UnitAttackTower : UnitState
{
    TargetInfo TargetInfo;
    public UnitAttackTower(UnitStateMachine context, TargetInfo target) : base(context)
    {
        StateName = "Attack Tower";
        StateColor = Color.red;
        this.TargetInfo = target;
    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        if (TargetInfo.target == null || !TargetInfo.target.IsAlive)
        {
            context.ChangeState(new UnitComeBackToPath(context));
            return;
        }    
       
        Weapon.Attack(TargetInfo.target, Unit.Config.damage);
            
    }

    public override void ExitState()
    {

    }
}