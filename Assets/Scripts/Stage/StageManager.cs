using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] StageConfig config;
    [SerializeField] Transform spawnPoint;
    [SerializeField] SplineManager SplineManager;
    [SerializeField] List<UnitBlueprint> UnitBlueprints;
    [Header("Spawning times")]
    [SerializeField] float initialWaitTime;
    [SerializeField] float waitBetweenUnit;
    [SerializeField] float bulkWait;
    
    List<Unit> SpawnedUnits = new List<Unit>();

    void Start()
    {
        Initialize(UnitBlueprints: null);
    }

    public void Initialize(List<UnitBlueprint> UnitBlueprints)
    {
        SpawnedUnits.Clear();
        StartCoroutine(SpawningRoutine());
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
    }

    void SpawnUnit(UnitBlueprint unitBlueprint)
    {
        var model = Instantiate(unitBlueprint.Config.UnitModel, spawnPoint.transform.position, Quaternion.identity);
        model.GetComponent<Unit>().PlaceOnStage(unitBlueprint, SplineManager);
        
        if(model.TryGetComponent(out Unit spawnedUnit))
        {
            spawnedUnit.PlaceOnStage(unitBlueprint, SplineManager);
            SpawnedUnits.Add(spawnedUnit);
        }
    }
}
