using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttackManager : MonoBehaviour
{
    Unit unit;
    public List<TargetInfo> targets = new();
    [SerializeField] Weapon weapon;

    public void Init(Unit unit)
    {
        this.unit = unit;
        SetUpTargets();
    }

    public void Attack(IDamagable target)
    {
        weapon.Attack(target, CalculateAdditionalDamage());
    }
    
    void SetUpTargets()
    {
        Tower[] towers = FindObjectsByType<Tower>(FindObjectsSortMode.None);
        foreach (var tower in towers)
        {
            TargetInfo targetInfo = new();
            Waypoint nearestWaypoint = new(percentage: Mathf.Infinity, Vector3.zero);

            for (int i = 0; i < 200; i++)
            {
                Vector3 randomPoint = tower.transform.position + Random.insideUnitSphere * weapon.Range;
                //Large range may produce lag
                if (!NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, unit.Blueprint.Config.height * 2, NavMesh.AllAreas) ||
                    Vector3.Distance(hit.position, tower.transform.position) > weapon.Range)
                {
                    continue;
                }
                
                var waypoint = unit.SplineManager.GetClosest(hit.position);
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
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var target in targets)
        {
            Gizmos.DrawSphere(target.sampledStandPosition.Add(y: 1), 1f);
        }
    }
    
    int CalculateAdditionalDamage()
    {
        return 0;
    }
}
