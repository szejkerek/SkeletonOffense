using TMPro;
using UnityEngine;

public class UnitAttackTower : UnitState
{
    readonly TargetInfo targetInfo;
    public UnitAttackTower(UnitStateMachine context, TargetInfo target) : base(context)
    {
        StateName = "Attack Tower";
        StateColor = Color.red;
        this.targetInfo = target;
    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        if (targetInfo.target == null || !targetInfo.target.IsAlive)
        {
            Context.ChangeState(new UnitComeBackToPath(Context));
            return;
        }    
       
        Weapon.Attack(targetInfo.target, Unit.CalculateAdditionalDamage());
    }
    
    public override void ExitState()
    {

    }
}