using UnityEngine;

public abstract class UnitState
{
    public string StateName = "DefaultName"; 
    public Color StateColor = Color.gray; 

    protected readonly UnitStateMachine Context;
    protected readonly Unit Unit;
    protected readonly UnitWalkManager WalkManager;
    protected readonly SplineManager SplineManager;
    protected readonly Weapon Weapon;

    protected UnitState(UnitStateMachine context)
    {
        this.Context = context;
        this.Unit = context.Unit;
        this.WalkManager = context.Unit.UnitWalkManager;
        this.SplineManager = context.Unit.SplineManager;
        this.Weapon = context.Unit.Weapon;
    }
    public abstract void EnterState();     
    public abstract void UpdateState(); 
    public abstract void ExitState();  
}
