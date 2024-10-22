using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitConfig Config => config;
    [SerializeField] UnitConfig config;

    public UnitStateMachine UnitStateMachine => unitStateMachine;
    UnitStateMachine unitStateMachine;    
    public UnitWalkManager UnitWalkManager => unitWalkManager;
    UnitWalkManager unitWalkManager;
    public SplineManager SplineManager => splineManager;
    SplineManager splineManager;

    private void Awake()
    {
        unitStateMachine = GetComponent<UnitStateMachine>();
        unitWalkManager = GetComponent<UnitWalkManager>();
    }

    public void Initialize(SplineManager stageSpline,bool aggresive)
    {
        this.splineManager = stageSpline;
        unitWalkManager.SetSpline(stageSpline);
        unitStateMachine.Initialize(aggresive);
    }
}
