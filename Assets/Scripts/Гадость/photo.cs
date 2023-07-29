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
        // �������� ����������� 
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to load image: " + request.error);
            yield break;
        }

        // �������� �������� � ����������� ��������� �������� 
        texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        Color[] pixels = texture.GetPixels();

        for (int i = 1; i <= 10; i++) // ������������� ������� ��� ��������� �������� 
        {
            Texture2D tempTexture = new Texture2D(texture.width, texture.height);
            tempTexture.SetPixels(pixels);
            tempTexture.Apply();

            // �������� �� ������ �������, ����� �������� ����������� � ������� ��������� 
            yield return new WaitForSeconds(0.5f);

            // �������� ������� ������ �������� 
            byte[] bytes = tempTexture.EncodeToJPG();
            texture.LoadImage(bytes);
            pixels = texture.GetPixels();
        }

        // ���������� ������������� ������ �������� � ��������� 
        imageRenderer.material.mainTexture = texture;
    }
}