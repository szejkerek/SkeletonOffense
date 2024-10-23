using UnityEngine;

public class Tower : MonoBehaviour, IDamagable
{
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    bool isAlive;

    public TowerConfig Config;
    public TowerHealth TowerHealth {  get; private set; }

    public void TakeDamage(float damage)
    {
        TowerHealth.TakeDamage(damage);
    }

    private void Awake()
    {
        TowerHealth = GetComponent<TowerHealth>();
    }
}
