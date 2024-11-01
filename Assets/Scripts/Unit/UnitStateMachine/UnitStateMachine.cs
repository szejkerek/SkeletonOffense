using UnityEngine;
using UnityEngine.Splines;

public class UnitStateMachine : MonoBehaviour
{
    public Unit Unit { get; private set; }

    [SerializeField] DebugText unitDebugText;

    UnitState currentState;

    void Awake()
    {
        Unit = GetComponent<Unit>();
        ChangeState(new UnitIdleState(this));
    }

    void Update()
    {
        currentState.UpdateState();
    }
    
    public void ChangeState(UnitState newState)
    {   
        if(Unit == null)
            return;
        
        currentState?.ExitState();   
        currentState = newState; 
        newState.EnterState();

        unitDebugText?.SetText(currentState.StateName, currentState.StateColor);
    }

}
