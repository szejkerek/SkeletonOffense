using System;
using UnityEngine;

public class Unit : MonoBehaviour, IDamagable
{
    public static Action OnDeath;
    public bool IsAlive { get; set; }

    public UnitBlueprint Blueprint { get; private set; }
    public UnitStateMachine UnitStateMachine { get; private set; }
    public UnitSplineWalker UnitSplineWalker { get; private set; }
    public UnitNavMeshWalker UnitNavMeshWalker { get; private set; }
    public SplineManager SplineManager { get; private set; }
    public HealthManager HealthManager { get; private set; }
    public UnitAttackManager UnitAttackManager { get; private set; }
    public UnitDraggingManager UnitDraggingManager { get; private set; }
    
    void Initialize()
    {
        UnitAttackManager = GetComponent<UnitAttackManager>();
        UnitStateMachine = GetComponent<UnitStateMachine>();
        UnitSplineWalker = GetComponent<UnitSplineWalker>();
        UnitNavMeshWalker = GetComponent<UnitNavMeshWalker>();
        HealthManager = GetComponent<HealthManager>();
        UnitDraggingManager = GetComponent<UnitDraggingManager>();
        Blueprint = new UnitBlueprint();
    }
    
    void OnUnitDeath()
    {
        IsAlive = false;
        UnitStateMachine.ChangeState(new UnitDyingState(UnitStateMachine));
        OnDeath?.Invoke();
    }

    public void PlaceOnStage(UnitBlueprint blueprint, SplineManager stageSpline)
    {
        Initialize();
        
        SplineManager = stageSpline;
        Blueprint = blueprint;
        UnitSplineWalker.Initialize(stageSpline, Blueprint.Config);
        UnitNavMeshWalker.Initialize(Blueprint.Config);
        
        IsAlive = true;

        UnitAttackManager.Init(this);
        HealthManager.Init(Blueprint.Config.health, OnUnitDeath);
        
        UnitStateMachine.ChangeState(new UnitComeBackToPath(UnitStateMachine));
    }
    
    public void PlaceInCamp(UnitConfig config, CampBasicSlot slot, int tier = 1)
    {
        Initialize();
        Blueprint.Config = config;
        Blueprint.Tier = tier;
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
