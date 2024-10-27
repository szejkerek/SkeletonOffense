using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    
    Unit unit;
    Action onDeath;
    Action onHit;
    HealthDisplay healthDisplay;

    public void Init(int maxHealth, Action onDeath = null, Action onHit = null)
    {
        healthDisplay = GetComponentInChildren<HealthDisplay>();
        unit = GetComponent<Unit>();
        this.onDeath = onDeath;
        this.onHit = onHit;
        MaxHealth = CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        healthDisplay?.UpdateHealth(CurrentHealth, MaxHealth);
        
        if(CurrentHealth <= 0)
        {
            onDeath?.Invoke();
            return;
        }
        
        onHit?.Invoke();
    }
}
