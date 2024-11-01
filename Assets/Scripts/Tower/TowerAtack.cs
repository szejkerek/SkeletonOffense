using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] Weapon weapon;

    const float UpdateInterval = 0.25f;
    const int MaxColliders = 50;
    
    Unit currentTarget;
    readonly List<Unit> unitsInRange = new();
    readonly Collider[] hitColliders = new Collider[MaxColliders]; 
    
    void Awake()
    {
        InvokeRepeating(nameof(UpdateTargetAndAttack), 0f, UpdateInterval);
    }

    void UpdateTargetAndAttack()
    {
        currentTarget = null;
        FindUnitsInRange();
        if (unitsInRange.Count <= 0)
        {
            return;
        }

        currentTarget = unitsInRange.OrderBy(unit => (
            unit.transform.position - transform.position).sqrMagnitude
        ).FirstOrDefault();

        weapon.Attack(currentTarget);
    }

    void FindUnitsInRange()
    {
        unitsInRange.Clear();
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, weapon.Range, hitColliders);

        for (int i = 0; i < hitCount; i++)
        {
            if (hitColliders[i].TryGetComponent(out Unit unit) && unit.IsAlive)
            {
                unitsInRange.Add(unit);
            }
        }
    }
}