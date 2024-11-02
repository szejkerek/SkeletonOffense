using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Action OnBaseDeath;
    public HealthManager HealthManager { get; private set; }
    public void Initialize(int health)
    {
        HealthManager = GetComponent<HealthManager>();
        HealthManager.Init(health, OnBaseDeath);
    }

}
