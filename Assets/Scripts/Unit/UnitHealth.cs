using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] HealthDisplay healthDisplay;
    UnitConfig config;

    float maxHealth;
    float currentHealth;

    private void Awake()
    {
        config = GetComponent<Unit>().Config;
        maxHealth = currentHealth = config.health;
    }

    public void DealDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthDisplay.UpdateHealth(currentHealth, maxHealth);
    }
}
