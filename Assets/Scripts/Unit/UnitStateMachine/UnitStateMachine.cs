using UnityEngine;
using UnityEngine.Splines;

public class UnitStateMachine : MonoBehaviour
{
    public Unit Unit { get; private set; }

    [SerializeField] DebugText unitDebugText;

    UnitState currentState;

    void Start()
    {
        Unit = GetComponent<Unit>();
        ChangeState(new UnitComeBackToPath(this));
    }

    void Update()
    {
        currentState.UpdateState();
    }
    
    public void ChangeState(UnitState newState)
    {
        currentState?.ExitState();   
        currentState = newState; 
        newState.EnterState();

        unitDebugText?.SetText(currentState.StateName, currentState.StateColor);
    }

}
