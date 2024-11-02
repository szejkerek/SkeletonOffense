using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] StageConfig config;
    [SerializeField] Transform spawnPoint;
    [SerializeField] SplineManager SplineManager;
    [SerializeField] List<UnitBlueprint> UnitBlueprints;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnUnit(UnitBlueprints[0]);
        }
    }

    void SpawnUnit(UnitBlueprint unitBlueprint)
    {
        var spawned = Instantiate(unitBlueprint.Config.UnitModel, spawnPoint.transform.position, Quaternion.identity);
        spawned.GetComponent<Unit>().PlaceOnStage(unitBlueprint, SplineManager);
    }
}
