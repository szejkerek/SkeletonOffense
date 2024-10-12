using UnityEngine;
using UnityEngine.Splines;

public class UnitStateMachine : MonoBehaviour
{
    UnitState currentState;

    #region Managers
    public UnitWalkManager WalkManager => walkManager;
    UnitWalkManager walkManager;
    #endregion

    private void Awake()
    {
        walkManager = GetComponent<UnitWalkManager>();
        walkManager.SetSpline(FindFirstObjectByType<SplineContainer>()); //TODO: Przenieść do StageManagera i ustawiać podczas respu jednostki.
    }

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
