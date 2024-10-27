using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Range => range;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private WeaponType weaponType;

    [SerializeField] int damage = 0;
    [SerializeField] float speed = 0;
    [SerializeField] Projectile projectilePrefab = null;  
    [SerializeField] float cooldown = 0;  
    [SerializeField] float range = 0;  
    
    float lastAttackTime = 0f;
    bool IsReadyToAttack()
    {
        return Time.time >= lastAttackTime + cooldown;
    }

    public void Attack(IDamagable target, int additionalDamage = 0)
    {
        lastAttackTime = Time.time;

        switch (weaponType)
        {
            case WeaponType.Melee:
                AttackMelee(target, additionalDamage, IsReadyToAttack(), IsTargetInRange(target));
                break;
            case WeaponType.Ranged:
                ShootProjectile(target, damage + additionalDamage, IsReadyToAttack(), IsTargetInRange(target));
                break;
            default:
                Debug.LogError("Type not specified");
                break;
        }
    }

    protected virtual void AttackMelee(IDamagable target, int additionalDamage, bool readyToAttack, bool targetInRange)
    {
        if (target == null || !readyToAttack || !targetInRange)
        {
            return;
        }
        
        target.TakeDamage(damage + additionalDamage);
    }
    
    protected virtual void ShootProjectile(IDamagable target, int dmg, bool readyToAttack, bool targetInRange)
    {
        if (target == null || projectilePrefab == null || !readyToAttack || !targetInRange)
        {
            return;
        }

        Projectile projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
        projectile.Initialize(target, dmg, speed);         
    }
    bool IsTargetInRange(IDamagable target)
    {
        float distanceToTarget = Vector3.Distance(shootingPoint.position, target.transform.position);
        return distanceToTarget <= range;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.parent.position, range);
    }
}
