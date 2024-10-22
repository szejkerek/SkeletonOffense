using UnityEngine;
using UnityEngine.Splines;

public class UnitStateMachine : MonoBehaviour
{
    public Unit Unit => unit;
    Unit unit;

    [SerializeField] DebugText UnitDebugText;

    bool aggresive;
    UnitState currentState;

    public void Initialize(bool aggresive)
    {
        this.aggresive = aggresive;
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
