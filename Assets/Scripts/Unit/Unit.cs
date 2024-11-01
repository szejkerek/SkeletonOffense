using UnityEngine;

public class Unit : MonoBehaviour, IDamagable
{
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    bool isAlive = true;

    public UnitConfig Config => config;
    [SerializeField] UnitConfig config;
    public UnitStateMachine UnitStateMachine { get; private set; }
    public UnitWalkManager UnitWalkManager { get; private set; }
    public SplineManager SplineManager { get; private set; }
    public HealthManager HealthManager { get; private set; }
    public UnitAttackManager UnitAttackManager { get; private set; }
    public bool Agressive { get; set; }


    public void Initialize(SplineManager stageSpline,bool agressive)
    {
        UnitAttackManager = GetComponent<UnitAttackManager>();
        UnitStateMachine = GetComponent<UnitStateMachine>();
        UnitWalkManager = GetComponent<UnitWalkManager>();
        HealthManager = GetComponent<HealthManager>();
        this.SplineManager = stageSpline;
        this.Agressive = agressive;
        UnitWalkManager.SetSpline(SplineManager);
        UnitAttackManager.Init(this);
    }

    public void TakeDamage(int damage)
    {
        HealthManager.TakeDamage(damage);
    }


}
