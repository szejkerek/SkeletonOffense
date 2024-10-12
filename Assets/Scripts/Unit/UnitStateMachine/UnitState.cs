using UnityEngine;

public abstract class UnitState
{
    public string StateName = "DefaultName"; 
    public Color StateColor = Color.gray; 

    protected UnitStateMachine stateMachine;

    public UnitState(UnitStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public abstract void Enter();     
    public abstract void LogicUpdate(); 
    public abstract void Exit();  
}
