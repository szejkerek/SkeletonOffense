using UnityEngine;

public abstract class UnitState
{
    public string StateName = "DefaultName"; 
    public Color StateColor = Color.gray; 

    protected UnitStateMachine context;
    protected Unit Unit;
    protected UnitWalkManager WalkManager;
    protected SplineManager SplineManager;
    protected Weapon Weapon;

    public UnitState(UnitStateMachine context)
    {
        this.context = context;
        this.Unit = context.Unit;
        this.WalkManager = context.Unit.UnitWalkManager;
        this.SplineManager = context.Unit.SplineManager;
        this.Weapon = context.Unit.GetComponentInChildren<Weapon>();
    }
    public abstract void EnterState();     
    public abstract void UpdateState(); 
    public abstract void ExitState();  
}
