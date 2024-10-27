using System.Collections;
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
        StartCoroutine(AttackRoutine());
    }
    
    void Update()
    {
        if (timeSinceLastUpdate >= updateInterval)
        {
            FindUnitsInRange();
            UpdateTarget();
            timeSinceLastUpdate = 0f;
        }
        timeSinceLastUpdate += Time.deltaTime;
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (currentTarget != null)
            {
                ShootProjectile();
            }
            yield return new WaitForSeconds(config.reloadTime);
        }
    }

    private void ShootProjectile()
    {
        if (config.projectile == null)
            return;

        Projectile projectile = Instantiate(config.projectile, shootingPoint.position, Quaternion.identity);
        projectile.Initialize(currentTarget, config.damage, config.projectileSpeed);
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

    private void OnDrawGizmos()
    {
        if(config == null)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, config.range);
    }
}
