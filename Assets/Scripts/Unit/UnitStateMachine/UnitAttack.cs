using TMPro;
using UnityEngine;

public class UnitAttack : UnitState
{
    readonly TargetInfo targetInfo;
    public UnitAttack(UnitStateMachine context, TargetInfo target) : base(context)
    {
        StateName = "Attack target";
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
       
        UnitAttackManager.Attack(targetInfo.target);
    }
    
    public override void ExitState()
    {

    }
}