using UnityEngine;

public abstract class UnitState
{
    public string StateName = "DefaultName"; 
    public Color StateColor = Color.gray; 

    protected readonly UnitStateMachine Context;
    protected readonly Unit Unit;
    protected readonly UnitNavMeshWalker UnitNavMeshWalker;
    protected readonly UnitSplineWalker UnitSplineWalker;
    protected readonly SplineManager SplineManager;
    protected readonly UnitAttackManager UnitAttackManager;

    protected UnitState(UnitStateMachine context)
    {
        this.Context = context;
        this.Unit = context.Unit;
        this.UnitNavMeshWalker = context.Unit.UnitNavMeshWalker;
        this.UnitSplineWalker = context.Unit.UnitSplineWalker;
        this.SplineManager = context.Unit.SplineManager;
        this.UnitAttackManager = context.Unit.UnitAttackManager;
    }
    public abstract void EnterState();     
    public abstract void UpdateState(); 
    public abstract void ExitState();  
}
