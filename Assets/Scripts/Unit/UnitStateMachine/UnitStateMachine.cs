using UnityEngine;
using UnityEngine.Splines;

public class UnitStateMachine : MonoBehaviour
{
    public Unit Unit { get; private set; }

    [SerializeField] DebugText unitDebugText;

    UnitState currentState;

    public void Initialize()
    {
        Unit = GetComponent<Unit>();
        ChangeState(new UnitComeBackToPath(this));
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void ChangeState(UnitState newState)
    {
        currentState?.ExitState();   
        currentState = newState; 
        currentState.EnterState();

        unitDebugText?.SetText(currentState.StateName, currentState.StateColor);
    }

}
