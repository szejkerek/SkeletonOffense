using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    TowerConfig config;

    [SerializeField] float updateInterval = 0.25f;
    public Transform shootingPoint;

    Unit currentTarget;
    List<Unit> unitsInRange = new();
    float timeSinceLastUpdate = 0f;

    void Awake()
    {
        config = GetComponent<Tower>().Config;
        StartCoroutine(AttackRoutine());
    }


    private void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= updateInterval)
        {
            timeSinceLastUpdate = 0f;
            FindUnitsInRange();
            UpdateTarget();
        }
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

        var projectile = Instantiate(config.projectile, shootingPoint.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.Initialize(currentTarget, config.damage);
    }

    private void FindUnitsInRange()
    {
        unitsInRange.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, config.range);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Unit unit) && unit.isAlive)
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
        if(config == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, config.range);
    }
}
