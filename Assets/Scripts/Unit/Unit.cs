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
    public UnitStateMachine UnitStateMachine { get; private set; }
    public UnitWalkManager UnitWalkManager { get; private set; }
    public SplineManager SplineManager { get; private set; }
    public HealthManager HealthManager { get; private set; }
    public Weapon Weapon { get; private set; }
    public bool Agressive { get; set; }
    
    public List<TargetInfo> targets = new();


    public void Initialize(SplineManager stageSpline,bool agressive)
    {
        Weapon = GetComponentInChildren<Weapon>();
        UnitStateMachine = GetComponent<UnitStateMachine>();
        UnitWalkManager = GetComponent<UnitWalkManager>();
        HealthManager = GetComponent<HealthManager>();
        this.SplineManager = stageSpline;
        this.Agressive = agressive;
        UnitWalkManager.SetSpline(SplineManager);
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
                Vector3 randomPoint = tower.transform.position + Random.insideUnitSphere * Weapon.Range;
                //Large range may produce lag
                if (!NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, config.height * 2, NavMesh.AllAreas) ||
                    Vector3.Distance(hit.position, tower.transform.position) > Weapon.Range)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var target in targets)
        {
            Gizmos.DrawSphere(target.sampledStandPosition.Add(y: 1), 1f);
        }
    }

    public void TakeDamage(int damage)
    {
        HealthManager.TakeDamage(damage);
    }

    public int CalculateAdditionalDamage()
    {
        return 0;
    }
}
