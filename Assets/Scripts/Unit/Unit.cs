using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitConfig Config => config;
    [SerializeField] UnitConfig config;

    public UnitStateMachine UnitStateMachine => unitStateMachine;
    UnitStateMachine unitStateMachine;    
    public UnitWalkManager UnitWalkManager => unitWalkManager;
    UnitWalkManager unitWalkManager;

    private void Awake()
    {
        unitStateMachine = GetComponent<UnitStateMachine>();
        unitWalkManager = GetComponent<UnitWalkManager>();
    }

    public void Initialize(SplineManager stageSpline)
    {
        unitWalkManager.SetSpline(stageSpline);
        unitStateMachine.Initialize();
    }
}
