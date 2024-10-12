using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Image mainHealth; // Red health bar
    [SerializeField] private Image secondHealth; // Orange health bar
    [SerializeField] private float updateDelay = 0.2f; // Delay before the second bar starts moving
    [SerializeField] private float updateDuration = 0.4f; // Duration for the second bar to catch up


    [SerializeField] private bool autoRotateTowardsCamera = true;
    private Camera targetCamera;

    void Start()
    {
        targetCamera = Camera.main;

        if (targetCamera == null)
        {
            Debug.LogError("Main Camera not found. Please tag the main camera.");
            autoRotateTowardsCamera = false;
        }
    }

    private void Update()
    {
        if (autoRotateTowardsCamera && targetCamera != null)
        {
            transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward, targetCamera.transform.rotation * Vector3.up);
        }
    }

    public void UpdateHealth(float health, float maxHealth)
    {
        float currentHealth = Mathf.Clamp(health, 0, maxHealth); 

        mainHealth.fillAmount = currentHealth / maxHealth;


        secondHealth.DOFillAmount(currentHealth / maxHealth, updateDuration)
            .SetDelay(updateDelay)
            .SetEase(Ease.InQuad);
    }

    private void UpdateHealthDisplay(float currentHealth, float maxHealth)
    {
        mainHealth.fillAmount = currentHealth / maxHealth;
        secondHealth.fillAmount = currentHealth / maxHealth;
    }
}
