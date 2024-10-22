using UnityEngine;
using UnityEngine.Splines;

public class UnitStateMachine : MonoBehaviour
{
    public Unit Unit => unit;
    Unit unit;

    [SerializeField] DebugText UnitDebugText;

    UnitState currentState;

    public void Initialize()
    {
        unit = GetComponent<Unit>();
        ChangeState(new UnitComeBackToPath(this));
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    public void ChangeState(UnitState newState)
    {
        currentState?.ExitState();   
        currentState = newState; 
        currentState.EnterState();

        UnitDebugText?.SetText(currentState.StateName, currentState.StateColor);
    }

}
