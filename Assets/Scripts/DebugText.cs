using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    [SerializeField] private GameObject textCanvas;
    [SerializeField] private TMP_Text mainText;
    [SerializeField] private bool autoRotateTowardsCamera = true;
    private Camera targetCamera;

    private float fadeDuration = 0;
    private bool fadingOut = false;

    private void Start()
    {
        targetCamera = Camera.main;

        if (targetCamera == null)
        {
            Debug.LogError("Main Camera not found. Please tag the main camera.");
            autoRotateTowardsCamera = false;
        }
        textCanvas.SetActive(true);
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
            fadeDuration = Mathf.Max(fadeDuration, 0);

            if (fadeDuration <= 0)
            {
                textCanvas.SetActive(false);
                fadingOut = false;
            }
        }
    }

    public void SetText(string text, Color? color = null, float duration = 0)
    {
        if (!string.IsNullOrEmpty(text))
        {
            mainText.text = text;
            mainText.color = color ?? Color.white;
            textCanvas.SetActive(true);

            fadeDuration = duration > 0 ? duration : 0;
            fadingOut = fadeDuration > 0;
        }
        else
        {
            ResetText();
        }
    }

    public void ResetText()
    {
        mainText.text = "";
        mainText.color = Color.white;
        textCanvas.SetActive(false);

        fadeDuration = 0;
        fadingOut = false;
    }
}
