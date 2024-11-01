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
        if(target == null)
        {
            return;
        }

        bool success = false;

        switch (weaponType)
        {
            case WeaponType.Melee:
                success = AttackMelee(target, additionalDamage, IsReadyToAttack(), IsTargetInRange(target));
                break;
            case WeaponType.Ranged:
                success = ShootProjectile(target, damage + additionalDamage, IsReadyToAttack(), IsTargetInRange(target));
                break;
            default:
                Debug.LogError("Type not specified");
                break;
        }
        
        if(success)
        {
            lastAttackTime = Time.time;
        }
    }

    protected virtual bool AttackMelee(IDamagable target, int additionalDamage, bool readyToAttack, bool targetInRange)
    {
        if (!readyToAttack || !targetInRange)
        {
            return false;
        }
        
        target.TakeDamage(damage + additionalDamage);
        return true;
    }
    
    protected virtual bool ShootProjectile(IDamagable target, int dmg, bool readyToAttack, bool targetInRange)
    {
        if (projectilePrefab == null || !readyToAttack || !targetInRange)
        {
            return false;
        }

        Projectile projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
        projectile.Initialize(target, dmg, speed);
        return true;
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
