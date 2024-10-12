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
        walkManager.SetSpline(FindFirstObjectByType<SplineContainer>()); //TODO: Przenieść do StageManagera i ustawiać podczas respu jednostki.

        UnitDebugText = GetComponentInChildren<DebugText>();
    }

    private void Start()
    {
        ChangeState(new UnitWalkingState(this));
    }

    private void Update()
    {
        currentState.LogicUpdate();
    }

    public void ChangeState(UnitState newState)
    {
        currentState?.Exit();   
        currentState = newState; 
        currentState.Enter();

        UnitDebugText?.SetText(currentState.StateName, currentState.StateColor);
    }

}
