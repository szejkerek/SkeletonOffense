using UnityEngine;
using UnityEngine.Splines;

public class UnitStateMachine : MonoBehaviour
{
    #region Managers
    public UnitWalkManager WalkManager => walkManager;
    UnitWalkManager walkManager;    
    #endregion

    DebugText UnitDebugText;
    UnitState currentState;

    private void Awake()
    {
        walkManager = GetComponent<UnitWalkManager>();
        UnitDebugText = GetComponentInChildren<DebugText>();
    }

    public void Initialize()
    {
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
