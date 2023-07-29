using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Profiling;
using UnityEngine.UI;
using UnityEngine.XR;
using Color = UnityEngine.Color;

public class ImageLoad : MonoBehaviour
{
    public string URL;
    public Texture2D image1;
    public RawImage[] image2;
    public int number;
    public float timer;
    public bool a = false;
    public static ImageLoad imageLoad;
    public GridGallery gridGallery;
    [SerializeField] private Slider sliderRating;
    private long[] arrayRandomID = new long[2];
    private long _randomID
    {
        get 
        {
            return arrayRandomID[1];
        }
        set 
        {
            arrayRandomID[1] = arrayRandomID[0];
            arrayRandomID[0] = value;
        }
    }
    public void Awake()
    {
        URL = LoadData.LoadDataName(DataName.url);
        imageLoad = this;
    }
    public void UploadCirclePhoto(Texture2D textureCircle, Texture2D textureRectangle)
    {
        print("photo");
        StartCoroutine(UploadPhotoRequest(textureCircle, textureRectangle));
    }
    public void StartPostRating(double rating, long idPhoto)
    {
        print("rating-" + rating);
        StartCoroutine(PostRating(rating, idPhoto));
    }
    Texture2D duplicateTexture(Texture2D source) // перезаписывает текстуру с режимом чтения
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
    private IEnumerator UploadPhotoRequest(Texture2D textureCircle, Texture2D textureRectangle)//загрузить фото в базу
    {
        textureCircle = duplicateTexture(textureCircle);
        textureRectangle = duplicateTexture(textureRectangle);
        string url = URL + "/photo/upload?";
        byte[] imageBytes1 = textureCircle.EncodeToJPG();
        byte[] imageBytes2 = textureRectangle.EncodeToJPG();
        WWWForm form = new WWWForm();
        form.AddBinaryData("circle", imageBytes1, "photocircle" + UnityEngine.Random.Range(1, 9999) + ".jpg", "image/jpeg");
        form.AddBinaryData("rectangle", imageBytes2, "photorectangle" + UnityEngine.Random.Range(1, 9999) + ".jpg", "image/jpeg");
        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error uploading photo: {request.error}");
            }
            else
            {
                Debug.Log("Photo uploaded successfully!");
            }
        }
    }
    private IEnumerator GetIdYourPhotoCircle() // скачать id твоих фоток
    {
        long[] numbers = new long[100];
        int id = int.Parse(LoadData.LoadDataName(DataName.idProfile));
        print(id);
        string url = URL + $"/images/circle/{id}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error getting photo: {request.error}");
            }
            else
            {
                long idPhoto = long.Parse(request.downloadHandler.text);
                number = numbers.Length - 1;
                StartCoroutine(GetPhotoCircleByIdRequest(idPhoto));
            }
        }
        
    }
    private IEnumerator GetPhotoByIdRequest(long[] array) // скачать фотки по id
    {
        long idPhoto = array[number];
        string url = URL + $"/image/get/{idPhoto}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.downloadHandler = new DownloadHandlerTexture();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error getting photo: {request.error}");
            }
            else
            {
                    gridGallery.photo[number].GetComponent<RawImage>().texture = DownloadHandlerTexture.GetContent(request);
                    a = false;
                    number--;
                    StartCoroutine(GetPhotoByIdRequest(array));
            }
        }
    }
    private IEnumerator GetPhotoCircleByIdRequest(long idPhoto) // скачать фотки по id
    {
        string url = URL + $"/image/get/{idPhoto}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.downloadHandler = new DownloadHandlerTexture();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error getting photo: {request.error}");
            }
            else
            {
                gridGallery.photo[number].GetComponent<RawImage>().texture = DownloadHandlerTexture.GetContent(request);
            }
        }
    }
    IEnumerator PostRating(double rating, long idPhoto)
    {
        int c = (int)(rating * 10);
        double a = c / 10;
        double b = c % 10;
        // Формируем URL-адрес с параметрами запроса 
        string requestUrl = URL + $"/rating/{idPhoto}?rating={a}.{b}";
        print(requestUrl);
        using (UnityWebRequest www = UnityWebRequest.Post(requestUrl, ""))
        {
            // Отправляем запрос и ждем ответа 
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Получаем ответ от сервера 
                string response = www.downloadHandler.text;
                print(response);

                // Обрабатываем полученные данные 
                //Debug.Log("Рейтинг: " + ratingResult);
            }
            else
            {
                // В случае ошибки выводим сообщение 
                Debug.LogError("Ошибка отправки запроса: " + www.error);
            }
        }
    }
    private IEnumerator GetRatingIdPhoto(long photoId)
    {
        print(photoId);
        string url = URL + $"/rating/{photoId}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error getting photo: {request.error}");
            }
            else
            {
                print(request.downloadHandler.text);
                double rating = Double.Parse(request.downloadHandler.text, CultureInfo.InvariantCulture);
               
            }
        }
        
    }
    public class RegistrationDataImage
    {
        public static string userName = "vlad";
        public static string email = "vidnikevich-vlad@mail.ru";
        public static string password = "1234";
    }
    private void Update()
    {
        if (a == true)
            timer += Time.deltaTime;
    }
    public RawImage aaa;
    public Texture2D photoTexture2;
    public Texture2D[] photoTexture;
    private const string GetPhotoUrl = "/api/photos/";

    public void OnEnable()
    {

    }

    private IEnumerator GetPhotoRequest(long photoId)
    {
        
        string url = URL + $"/photo/rectangle/{photoId}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                byte[] photoBytes = request.downloadHandler.data;
            byte[] decompressedImageBytes = ImageCompressionUtility.DecompressImage(photoBytes);
            print(decompressedImageBytes.Length);
            photoTexture[photoId] = new Texture2D(1, 1);
            photoTexture[photoId].LoadImage(decompressedImageBytes);
            imageRenderer.texture = photoTexture[photoId];

            // Используйте photoTexture как необходимо (например, отобразить на объекте) 

            print("photo1 " + timer);
            }
            else
            {
                Debug.LogError("Failed to retrieve photo: " + request.error);
            }
    }
    private IEnumerator PostPhotoRequest()
    {
        photoTexture2 = duplicateTexture(photoTexture2);
        byte[] photoBytes = photoTexture2.EncodeToPNG();
        string url = URL + $"/api/photos/upload";
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", photoBytes, "photo.png", "image/png");

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Photo uploaded successfully");
            }
            else
            {
                Debug.LogError("Failed to upload photo: " + www.error);
            }
        }
    }



    public RawImage imageRenderer;

    private Texture2D texture;

    private IEnumerator Start1(int photoId)
    {
        print("start  " + timer);
        string url = URL + $"/photo/rectangle/{photoId}";
        // Загрузка изображения 
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to load image: " + request.error);
            yield break;
        }
        print("serverTolk  " + timer);
        // Создание текстуры и постепенное улучшение качества 
        texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        Color[] pixels = texture.GetPixels();
        print(timer);
        for (int i = 1; i <= 10; i++) // Прогрессивные проходы для улучшения качества 
        {
            Texture2D tempTexture = new Texture2D(texture.width, texture.height);
            tempTexture.SetPixels(pixels);
            tempTexture.Apply();
            print("1  " + timer);
            imageRenderer.texture = texture;
            // Ожидание на каждом проходе, чтобы обновить отображение с текущим качеством 
            yield return new WaitForSeconds(0.5f);
            print("2  " + timer);
            // Загрузка текущей версии текстуры 
            byte[] bytes = tempTexture.EncodeToJPG();
            texture.LoadImage(bytes);
            pixels = texture.GetPixels();
            
        }

        // Применение окончательной версии текстуры к рендереру 
        imageRenderer.texture = texture;
        print("end  " + timer);
    }





}


public class ImageCompressionUtility
{
    public static byte[] DecompressImage(byte[] compressedImageBytes)
    {
        // Создайте поток для сжатого изображения 
        MemoryStream compressedStream = new MemoryStream(compressedImageBytes);

        // Прочитайте сжатое изображение в объект Texture2D 
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(compressedImageBytes);

        // Создайте буфер для разжатого изображения 
        MemoryStream decompressedStream = new MemoryStream();

        // Сохраните разжатое изображение в формате JPEG в буфер 
        byte[] decompressedImageBytes = texture.EncodeToJPG();
        decompressedStream.Write(decompressedImageBytes, 0, decompressedImageBytes.Length);

        // Получите разжатое изображение в виде байтового массива 
        byte[] result = decompressedStream.ToArray();

        // Освободите ресурсы 
        compressedStream.Close();
        decompressedStream.Close();
       // Object.Destroy(texture);

        return result;
    }
} // compressedImageBytes - сжатое изображение, полученное с сервера 

