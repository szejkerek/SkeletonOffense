using UnityEngine;

public class Tower : MonoBehaviour, IDamagable
{
    public bool IsAlive { get; set; } = true;

    public TowerConfig Config;
    HealthManager health;

    private void Awake()
    {
        health = GetComponent<HealthManager>();
        health.Init(Config.health, OnTowerDeath);
    }

    void OnTowerDeath()
    {
        Debug.Log($"Tower {name} is dead");
        IsAlive = false;
        gameObject.SetActive(false);    
    }

    public void TakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }
    
    

}
