using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private WeaponType weaponType;
    [Header("IF RANGED ASSIGN PROJECTILE")]
    [SerializeField] private Projectile projectilePrefab; 

    float lastAttackTime = 0f;
    UnitConfig config;
    public void Init(Unit unit)
    {
        this.config = unit.Config;
    }

    bool IsReadyToAttack()
    {
        return Time.time >= lastAttackTime + config.cooldown;
    }

    public void Attack(IDamagable target, int damage)
    {
        if (target == null || !IsReadyToAttack() || !IsTargetInRange(target))
        {
            return;
        }

        lastAttackTime = Time.time;

        switch (weaponType)
        {
            case WeaponType.Melee:
                DealDamage(target, damage);
                break;
            case WeaponType.Ranged:
                ShootProjectile(target, damage);
                break;
            default:
                Debug.LogError("Type not specified");
                break;
        }
    }

    bool IsTargetInRange(IDamagable target)
    {
        float distanceToTarget = Vector3.Distance(shootingPoint.position, target.transform.position);
        return distanceToTarget <= config.range;
    }

    void DealDamage(IDamagable target, int damage)
    {
        target.TakeDamage(damage);
    }

    private void ShootProjectile(IDamagable target, int damage)
    {
        if (projectilePrefab == null || shootingPoint == null)
        {
            return;
        }

        Projectile projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
        projectile.Initialize(target, damage);         
    }
}
