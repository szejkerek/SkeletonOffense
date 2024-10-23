using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Unit : MonoBehaviour
{
    public bool isAlive = true;
    public UnitConfig Config => config;
    [SerializeField] UnitConfig config;
    public UnitStateMachine UnitStateMachine => unitStateMachine;
    UnitStateMachine unitStateMachine;    
    public UnitWalkManager UnitWalkManager => unitWalkManager;
    UnitWalkManager unitWalkManager;
    public SplineManager SplineManager => splineManager;
    SplineManager splineManager;
    public UnitHealth UnitHealth => unitHealth;
    UnitHealth unitHealth;

    public List<TargetInfo> targets = new();

    public bool Agressive => aggresive;
    public bool aggresive;

    public void Initialize(SplineManager stageSpline,bool aggresive)
    {
        unitStateMachine = GetComponent<UnitStateMachine>();
        unitWalkManager = GetComponent<UnitWalkManager>();
        unitHealth = GetComponent<UnitHealth>();
        this.splineManager = stageSpline;
        this.aggresive = aggresive;
        unitWalkManager.SetSpline(stageSpline);
        unitStateMachine.Initialize();
        SetUpTargets();
    }

    private void SetUpTargets()
    {
        var towers = FindObjectsByType<Tower>(FindObjectsSortMode.None).ToList();
        foreach (var tower in towers)
        {
            TargetInfo targetInfo = new TargetInfo();
            Waypoint nearestWaypoint = new(percentage: Mathf.Infinity, Vector3.zero);

            for (int i = 0; i < 200; i++)
            {
                Vector3 randomPoint = tower.transform.position + Random.insideUnitSphere * config.range;
                //Large range may produce lag
                if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, config.height * 2, NavMesh.AllAreas))
                {
                    if (Vector3.Distance(hit.position, tower.transform.position) >= config.range)
                    {
                        continue;
                    }

                    var waypoint = SplineManager.GetClosest(hit.position);

                    if (waypoint.percentage < nearestWaypoint.percentage)
                    {
                        targetInfo.SetPosition(hit.position);
                        nearestWaypoint = waypoint;
                    }
                }
            }

            if (nearestWaypoint.percentage != Mathf.Infinity)
            {
                targetInfo.SetTarget(tower);
                targetInfo.SetWaypoint(nearestWaypoint);
            }

            targets.Add(targetInfo);
        }
    }

    private void OnDrawGizmos()
    {
        if (config.range <= 0)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, config.range);

        foreach (var target in targets)
        {
            Gizmos.DrawSphere(target.sampledStandPosition.Add(y: 1), 1f);
        }
    }
}
