using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Unit : MonoBehaviour, IDamagable
{
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    bool isAlive = true;

    public UnitConfig Config => config;
    [SerializeField] UnitConfig config;
    public UnitStateMachine UnitStateMachine => unitStateMachine;
    UnitStateMachine unitStateMachine;    
    public UnitWalkManager UnitWalkManager => unitWalkManager;
    UnitWalkManager unitWalkManager;
    public SplineManager SplineManager => splineManager;
    SplineManager splineManager;
    public HealthManager HealthManager => healthManager;
    HealthManager healthManager;
    public Weapon Weapon => weapon;
    Weapon weapon;

    public List<TargetInfo> targets = new();

    public bool Agressive => aggresive;


    public bool aggresive;

    public void Initialize(SplineManager stageSpline,bool aggresive)
    {
        weapon = GetComponentInChildren<Weapon>();
        unitStateMachine = GetComponent<UnitStateMachine>();
        unitWalkManager = GetComponent<UnitWalkManager>();
        healthManager = GetComponent<HealthManager>();
        this.splineManager = stageSpline;
        this.aggresive = aggresive;
        unitWalkManager.SetSpline(splineManager);
        weapon.Init(this);
        SetUpTargets();
    }

    private void SetUpTargets()
    {
        Tower[] towers = FindObjectsByType<Tower>(FindObjectsSortMode.None);
        foreach (var tower in towers)
        {
            TargetInfo targetInfo = new();
            Waypoint nearestWaypoint = new(percentage: Mathf.Infinity, Vector3.zero);

            for (int i = 0; i < 200; i++)
            {
                Vector3 randomPoint = tower.transform.position + Random.insideUnitSphere * config.range;
                //Large range may produce lag
                if (!NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, config.height * 2, NavMesh.AllAreas) ||
                    Vector3.Distance(hit.position, tower.transform.position) >= config.range)
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

            if (!float.IsPositiveInfinity(nearestWaypoint.percentage))
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
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, config.range);

        foreach (var target in targets)
        {
            Gizmos.DrawSphere(target.sampledStandPosition.Add(y: 1), 1f);
        }
    }

    public void TakeDamage(float damage)
    {
        healthManager.TakeDamage(damage);
    }
}
