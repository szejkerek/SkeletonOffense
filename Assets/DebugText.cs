using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    [SerializeField] GameObject textCanvas;
    [SerializeField] TMP_Text mainText;
    [SerializeField] bool autoRotateTowardsCamera = true;
    Camera targetCamera;

    private float fadeDuration = 0; 
    private Color defaultColor = Color.white;
    private bool fadingOut = false;

    private void Start()
    {
        targetCamera = Camera.main;
    }

    private void Update()
    {
        if (autoRotateTowardsCamera && targetCamera != null)
        {
            transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward, targetCamera.transform.rotation * Vector3.up);
        }

        if (fadingOut && fadeDuration > 0)
        {
            fadeDuration -= Time.deltaTime;
            if (fadeDuration <= 0)
            {
                textCanvas.SetActive(false);
            }
        }
    }

    public void SetText(string text, Color? color = null, float duration = 0)
    {
        mainText.text = text;
        mainText.color = color ?? defaultColor;
        textCanvas.SetActive(true);

        fadeDuration = duration > 0 ? duration : 0;
        fadingOut = fadeDuration > 0;
    }

    public void ResetText()
    {
        mainText.text = "";
        mainText.color = defaultColor;
        textCanvas.SetActive(false);
    }
}
