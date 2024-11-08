using UnityEngine;
using UnityEngine.UI; // Do obs³ugi UI w edytorze

public class CaptureIcon : MonoBehaviour
{
    public Camera iconCamera; // Kamera, która robi zdjêcie
    public RenderTexture renderTexture; // Tekstura, na któr¹ kamera bêdzie renderowaæ
    public string filePath = "Assets/icon.png"; // Œcie¿ka zapisu pliku

    public GameObject background; // T³o ikony
    public Button captureButton; // Przycisk uruchamiaj¹cy generacjê

    void Start()
    {
        // Upewnij siê, ¿e przycisk jest przypisany i nas³uchuje na klikniêcie
        if (captureButton != null)
        {
            captureButton.onClick.AddListener(CaptureScreenshot);
        }
    }

    // Metoda do wykonania zrzutu ekranu
    public void CaptureScreenshot()
    {
        // W³¹cz t³o, jeœli jest wy³¹czone
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

        // Wy³¹cz t³o po renderowaniu, jeœli chcesz, aby by³o widoczne tylko podczas renderowania
        if (background != null)
        {
            background.SetActive(false);
        }
    }
}
