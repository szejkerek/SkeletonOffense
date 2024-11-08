using UnityEngine;
using UnityEngine.UI; // Do obs�ugi UI w edytorze

public class CaptureIcon : MonoBehaviour
{
    public Camera iconCamera; // Kamera, kt�ra robi zdj�cie
    public RenderTexture renderTexture; // Tekstura, na kt�r� kamera b�dzie renderowa�
    public string filePath = "Assets/icon.png"; // �cie�ka zapisu pliku

    public GameObject background; // T�o ikony
    public Button captureButton; // Przycisk uruchamiaj�cy generacj�

    void Start()
    {
        // Upewnij si�, �e przycisk jest przypisany i nas�uchuje na klikni�cie
        if (captureButton != null)
        {
            captureButton.onClick.AddListener(CaptureScreenshot);
        }
    }

    // Metoda do wykonania zrzutu ekranu
    public void CaptureScreenshot()
    {
        // W��cz t�o, je�li jest wy��czone
        if (background != null)
        {
            background.SetActive(true);
        }

        iconCamera.targetTexture = renderTexture;
        RenderTexture.active = renderTexture;
        iconCamera.Render();

        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBAFloat , false);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(filePath, bytes);

        RenderTexture.active = null;
        iconCamera.targetTexture = null;

        Debug.Log("Ikona zapisana w: " + filePath);

        // Wy��cz t�o po renderowaniu, je�li chcesz, aby by�o widoczne tylko podczas renderowania
        if (background != null)
        {
            background.SetActive(false);
        }
    }
}
