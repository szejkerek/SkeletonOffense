using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public CameraConfig CameraConfig => cameraConfig;
    [SerializeField] CameraConfig cameraConfig;    
    
    [SerializeField] StageConfig config;
    [SerializeField] Transform spawnPoint;
    [SerializeField] SplineManager SplineManager;
    [SerializeField] EnemyBase enemyBase;
    [Header("Spawning times")]
    [SerializeField] float initialWaitTime;
    [SerializeField] float waitBetweenUnit;
    [SerializeField] float bulkWait;
    [SerializeField] Button NextRoundButton;
    [Space]
    [SerializeField] List<UnitBlueprint> UnitBlueprints;
    
    List<Unit> SpawnedUnits = new();
    bool allSpawned = false;

    void Start()
    {
        Initialize();
    }

    public void StartRound(List<UnitBlueprint> UnitBlueprints)
    {
        NextRoundButton.interactable = false;
        allSpawned = false;
        SpawnedUnits.Clear();
        StartCoroutine(SpawningRoutine());
    }

    void OnRoundEnded()
    {
        StopAllCoroutines();
        NextRoundButton.interactable = true;
        
        Debug.Log("Round ended");
    }

    void Initialize()
    {
        NextRoundButton.onClick.AddListener(() => StartRound(UnitBlueprints: null));
        Unit.OnDeath += CheckForRoundEnd;
        //enemyBase.Initialize(Config.enemyBaseHP, CheckForRoundEnd);
    }

    void OnStageCompleted()
    {
        StopAllCoroutines();
        NextRoundButton.onClick.RemoveAllListeners();
        Unit.OnDeath -= CheckForRoundEnd;
        
        Debug.Log("Stage completed");
    }
    
    
    void CheckForRoundEnd()
    {
        if (!allSpawned)
        {
            return;
        }

        if (!enemyBase.IsAlive)
        {
            OnStageCompleted();
            return;
        }
        bool allUnitsDead = SpawnedUnits.All(unit => !unit.IsAlive);
    
        if (allUnitsDead)
        {
            OnRoundEnded();
        }
        
    }

    IEnumerator SpawningRoutine()
    {
        yield return new WaitForSeconds(initialWaitTime);

        foreach (var unit in UnitBlueprints)
        {
            if (unit.Config.BulkSpawnCount > 1)
            {
                for (int i = 0; i < unit.Config.BulkSpawnCount; i++)
                {
                    SpawnUnit(unit);
                    yield return new WaitForSeconds(bulkWait);
                }
                
            }
            else
            {
                SpawnUnit(unit);
            }
            yield return new WaitForSeconds(waitBetweenUnit);
        }

        allSpawned = true;
    }

    void SpawnUnit(UnitBlueprint unitBlueprint)
    {
        //Choose model/prefab based on Tier/lvl in blueprint
        int tier = unitBlueprint.Tier;
        var config = unitBlueprint.Config;
        var modelPrefab = tier == 1 ? config.UnitModelTier1 : tier == 2 ? config.UnitModelTier2 : config.UnitModelTier3; 
        var model = Instantiate(modelPrefab, spawnPoint.transform.position, Quaternion.identity);

        if (model.TryGetComponent(out Unit spawnedUnit))
        {
            spawnedUnit.PlaceOnStage(unitBlueprint, SplineManager);
            SpawnedUnits.Add(spawnedUnit);
        }
    }
}
