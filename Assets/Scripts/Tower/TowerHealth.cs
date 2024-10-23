using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    [SerializeField] HealthDisplay healthDisplay;
    TowerConfig config;

    float maxHealth;
    float currentHealth;

    private void Awake()
    {
        config = GetComponent<Tower>().Config;
        maxHealth = currentHealth = config.health;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthDisplay.UpdateHealth(currentHealth, maxHealth);
    }
}
