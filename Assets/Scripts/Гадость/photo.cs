using UnityEngine; 
using UnityEngine.Networking; 
using System.Collections; 
 
public class ProgressiveImageLoader : MonoBehaviour
{
    public string imageUrl;
    public Renderer imageRenderer;

    private Texture2D texture;

    private IEnumerator Start()
    {
        // «агрузка изображени€ 
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to load image: " + request.error);
            yield break;
        }

        // —оздание текстуры и постепенное улучшение качества 
        texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        Color[] pixels = texture.GetPixels();

        for (int i = 1; i <= 10; i++) // ѕрогрессивные проходы дл€ улучшени€ качества 
        {
            Texture2D tempTexture = new Texture2D(texture.width, texture.height);
            tempTexture.SetPixels(pixels);
            tempTexture.Apply();

            // ќжидание на каждом проходе, чтобы обновить отображение с текущим качеством 
            yield return new WaitForSeconds(0.5f);

            // «агрузка текущей версии текстуры 
            byte[] bytes = tempTexture.EncodeToJPG();
            texture.LoadImage(bytes);
            pixels = texture.GetPixels();
        }

        // ѕрименение окончательной версии текстуры к рендереру 
        imageRenderer.material.mainTexture = texture;
    }
}