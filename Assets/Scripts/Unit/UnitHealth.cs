using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] HealthDisplay healthDisplay;
    Unit unit;

    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }

    private void Awake()
    {
        unit = GetComponent<Unit>();
        maxHealth = currentHealth = unit.Config.health;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthDisplay.UpdateHealth(currentHealth, maxHealth);

        if(currentHealth <= 0)
        {
            unit.isAlive = false;
            Destroy(unit, 3f);
        }
    }
}
