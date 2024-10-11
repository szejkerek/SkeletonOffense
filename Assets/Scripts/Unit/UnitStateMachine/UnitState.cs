public abstract class UnitState
{
    public string StateName = "DefaultName"; 
    protected UnitStateMachine stateMachine;

    public UnitState(UnitStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public abstract void Enter();     
    public abstract void LogicUpdate(); 
    public abstract void Exit();  
}
