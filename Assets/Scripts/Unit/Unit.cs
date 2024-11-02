using System;
using UnityEngine;

public class Unit : MonoBehaviour, IDamagable
{
    public static Action<Unit> OnDeath;
    public bool IsAlive { get; set; }
    public bool Agressive { get; private set; }

    public UnitConfig Config { get; private set; }
    public UnitStateMachine UnitStateMachine { get; private set; }
    public UnitSplineWalker UnitSplineWalker { get; private set; }
    public UnitNavMeshWalker UnitNavMeshWalker { get; private set; }
    public SplineManager SplineManager { get; private set; }
    public HealthManager HealthManager { get; private set; }
    public UnitAttackManager UnitAttackManager { get; private set; }
    public DraggableUnit UnitDraggingManager { get; private set; }
    
    void Initialize()
    {
        UnitAttackManager = GetComponent<UnitAttackManager>();
        UnitStateMachine = GetComponent<UnitStateMachine>();
        UnitSplineWalker = GetComponent<UnitSplineWalker>();
        UnitNavMeshWalker = GetComponent<UnitNavMeshWalker>();
        HealthManager = GetComponent<HealthManager>();
        UnitDraggingManager = GetComponent<DraggableUnit>();
    }
    
    void OnUnitDeath()
    {
        IsAlive = false;
        UnitStateMachine.ChangeState(new UnitDyingState(UnitStateMachine));
        OnDeath?.Invoke(this);
    }

    public void PlaceOnStage(UnitBlueprint blueprint, SplineManager stageSpline)
    {
        Initialize();
        
        SplineManager = stageSpline;
        Agressive = blueprint.AgressiveMode;
        Config = blueprint.Config;
        UnitSplineWalker.Initialize(stageSpline, Config);
        UnitNavMeshWalker.Initialize(Config);
        
        IsAlive = true;

        UnitAttackManager.Init(this);
        HealthManager.Init(Config.health, OnUnitDeath);
        
        UnitStateMachine.ChangeState(new UnitComeBackToPath(UnitStateMachine));
    }
    
    public void PlaceInCamp(UnitConfig config, CampBasicSlot slot, int tier = 1)
    {
        Initialize();
        Config = config;
        UnitDraggingManager.GetUnitBlueprint().Config = config;
        UnitDraggingManager.GetUnitBlueprint().Tier = tier;
        UnitDraggingManager.SetCurrentSlot(slot);
        UnitDraggingManager.MoveToSlotPosition();

        UnitStateMachine.ChangeState(new UnitDraggableReady(UnitStateMachine));
    }
    
    public void DestroyUnit(float delay = 0)
    {
        Destroy(gameObject, delay);
    }

    public void TakeDamage(int damage)
    {
        HealthManager.TakeDamage(damage);
    }

}
