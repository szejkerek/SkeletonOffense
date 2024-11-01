using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Image mainHealth;
    [SerializeField] private Image secondHealth;
    [SerializeField] private float updateDelay = 0.2f;
    [SerializeField] private float updateDuration = 0.4f;


    [SerializeField] private bool autoRotateTowardsCamera = true;
    Camera targetCamera;

    void Start()
    {
        targetCamera = Camera.main;

        if (targetCamera == null)
        {
            Debug.LogError("Main Camera not found. Please tag the main camera.");
            autoRotateTowardsCamera = false;
        }
    }

    void Update()
    {
        if(!transform.parent.gameObject.activeSelf)
        
        if (autoRotateTowardsCamera && targetCamera != null)
        {
            transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward, targetCamera.transform.rotation * Vector3.up);
        }
    }

    public void UpdateHealth(float health, float maxHealth)
    {
        float currentHealth = Mathf.Clamp(health, 0, maxHealth); 

        mainHealth.fillAmount = currentHealth / maxHealth;

        if (health == 0)
        {
            secondHealth.DOFillAmount(0, 0.1f)
                .SetEase(Ease.InQuad)
                .OnComplete(() => DOTween.Kill(secondHealth));
            return;
        }

        secondHealth.DOFillAmount(currentHealth / maxHealth, updateDuration)
            .SetDelay(updateDelay)
            .SetEase(Ease.InQuad);
    }

}
