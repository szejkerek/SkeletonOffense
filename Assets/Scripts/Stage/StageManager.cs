using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] StageConfig config;
    public EnemyBase EnemyBase;
    public Tower[] Towers;
    public Transform spawnPoint;
    public SplineManager SplineManager;

    public Unit UnitPrefab;

    public List<Unit> UnitsToBeSpawned;
    public List<Unit> SpawnedUnits;
    
    
    void InitializeStage(List<Unit> unitsToBeSpawned)
    {
        Towers = GetComponentsInChildren<Tower>();
        EnemyBase = GetComponentInChildren<EnemyBase>();
        UnitsToBeSpawned = unitsToBeSpawned;
    }
    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnUnit();
        }
    }

    void SpawnUnit()
    {
        var spawned = Instantiate(UnitPrefab, spawnPoint.transform.position, Quaternion.identity);
        spawned.Initialize(SplineManager, true);
    }
}
