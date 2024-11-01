using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] StageConfig config;
    public Transform spawnPoint;
    public SplineManager SplineManager;
    public UnitBlueprint UnitBlueprint;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnUnit();
        }
    }

    void SpawnUnit()
    {
        var spawned = Instantiate(UnitBlueprint.Config.UnitModel, spawnPoint.transform.position, Quaternion.identity);
        spawned.GetComponent<Unit>().PlaceOnStage(UnitBlueprint, SplineManager);
    }
}
