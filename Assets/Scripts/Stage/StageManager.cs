using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public StageConfig config;
    public EnemyBase EnemyBase;
    public List<Tower> Towers;
    public Transform spawnPoint;
    public SplineManager SplineManager;

    public Unit UnitPrefab;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnUnit();
            Debug.Log("Spawned");
        }
    }

    public void SpawnUnit()
    {
        var spawned = Instantiate(UnitPrefab, spawnPoint.transform.position, Quaternion.identity);
        spawned.Initialize(SplineManager, aggresive: true);
    }
}
