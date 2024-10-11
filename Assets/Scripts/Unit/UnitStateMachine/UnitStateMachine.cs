using UnityEngine;

public class UnitStateMachine : MonoBehaviour
{
    UnitState currentState;

    private void Start()
    {
        currentState = new UnitWalkingState(this);
        currentState.Enter();
    }

    private void Update()
    {
        currentState.LogicUpdate();
    }

    public void ChangeState(UnitState newState)
    {
        if(currentState != null) 
            currentState.Exit();
        
        currentState = newState; 
        currentState.Enter();
    }

}
