using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    TowerConfig config;

    [SerializeField] Weapon weapon;

    Unit currentTarget;
    readonly List<Unit> unitsInRange = new();
    
    [SerializeField] float updateInterval = 0.25f;
    float timeSinceLastUpdate = 0f;

    void Awake()
    {
        config = GetComponent<Tower>().Config;
    }
    
    void Update()
    {
        if (timeSinceLastUpdate >= updateInterval)
        {
            FindUnitsInRange();
            UpdateTarget();
            weapon.Attack(currentTarget);
            timeSinceLastUpdate = 0f;
        }
        timeSinceLastUpdate += Time.deltaTime;
    }
    

    private void FindUnitsInRange()
    {
        unitsInRange.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, config.range);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Unit unit) && unit.IsAlive)
            {
                unitsInRange.Add(unit);
            }
        }
    }

    private void UpdateTarget()
    {
        if (unitsInRange.Count == 0)
        {
            currentTarget = null;
            return;
        }
        currentTarget = unitsInRange.SelectRandomElement();
    }
    
}