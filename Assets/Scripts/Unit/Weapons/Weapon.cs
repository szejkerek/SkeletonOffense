using UnityEngine;

public enum WeaponType
{
    Melee,
    Ranged,
}

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

    public bool IsReadyToAttack()
    {
        return Time.time >= lastAttackTime + config.cooldown;
    }

    public void Attack(Tower target, int damage)
    {
        if (target == null || !IsReadyToAttack() || !IsTargetInRange(target))
            return;

        lastAttackTime = Time.time;

        if (weaponType == WeaponType.Melee)
        {
            DealDamage(target, damage);
        }
        else if (weaponType == WeaponType.Ranged)
        {
            ShootProjectile(target, damage);
        }
    }

    bool IsTargetInRange(Tower target)
    {
        float distanceToTarget = Vector3.Distance(shootingPoint.position, target.transform.position);
        return distanceToTarget <= config.range;
    }

    void DealDamage(Tower target, int damage)
    {
        target.TowerHealth.TakeDamage(damage);
    }

    private void ShootProjectile(Tower target, int damage)
    {
        if (projectilePrefab == null || shootingPoint == null)
            return;

        Projectile projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
        projectile.Initialize(target, damage);         
    }
}
