using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamagable
{
    public bool IsAlive { get; set; } = true;
    public HealthManager HealthManager { get; private set; }
    public void Initialize(int health, Action checkForRoundEnd)
    {
        HealthManager = GetComponent<HealthManager>();
        HealthManager.Init(health, () =>
        {
            IsAlive = false;
            checkForRoundEnd?.Invoke();
        });
    }
    public void TakeDamage(int damage)
    {
        HealthManager.TakeDamage(damage);
    }
}
