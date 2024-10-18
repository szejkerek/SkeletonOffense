using UnityEngine;

public abstract class UnitState
{
    public string StateName = "DefaultName"; 
    public Color StateColor = Color.gray; 

    protected UnitStateMachine context;

    public UnitState(UnitStateMachine context)
    {
        this.context = context;
    }
    public abstract void EnterState();     
    public abstract void UpdateState(); 
    public abstract void ExitState();  
}
