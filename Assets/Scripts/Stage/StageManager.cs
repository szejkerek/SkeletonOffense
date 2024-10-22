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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnUnit();
        }
    }

    public void SpawnUnit()
    {
        var spawned = Instantiate(UnitPrefab, spawnPoint.transform.position, Quaternion.identity);
        spawned.GetComponent<UnitWalkManager>().SetSpline(SplineManager.Spline);
        spawned.UnitStateMachine.Initialize();
    }
}
