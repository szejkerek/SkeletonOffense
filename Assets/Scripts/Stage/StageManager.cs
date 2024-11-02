using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] StageConfig config;
    [SerializeField] Transform spawnPoint;
    [SerializeField] SplineManager SplineManager;
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
        StartCoroutine(SpawningRoutine());
    }

    public void OnRoundEnded()
    {
        StopAllCoroutines();
        NextRoundButton.interactable = true;
    }

    void Initialize()
    {
        NextRoundButton.onClick.AddListener(() => StartRound(UnitBlueprints: null));
        Unit.OnDeath += CheckForRoundEnd;
    }

    void OnStageCompleted()
    {
        NextRoundButton.onClick.RemoveAllListeners();
        Unit.OnDeath -= CheckForRoundEnd;
    }
    
    
    void CheckForRoundEnd(Unit _)
    {
        if (!allSpawned)
        {
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
        //Choose model/prefab based on tier/lvl in blueprint
        var model = Instantiate(unitBlueprint.Config.UnitModelTier1, spawnPoint.transform.position, Quaternion.identity);

        if (model.TryGetComponent(out Unit spawnedUnit))
        {
            spawnedUnit.PlaceOnStage(unitBlueprint, SplineManager);
            SpawnedUnits.Add(spawnedUnit);
        }
    }
}
