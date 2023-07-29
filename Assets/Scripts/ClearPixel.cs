using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearPixel : MonoBehaviour
{
    [SerializeField] private Texture2D image;
    [SerializeField] private Texture2D image1;
    private int radius;

    private void Start()
    {
        // создание клона текстуры, чтобы не испортить оригинал
        Texture2D duplicateTexture = new Texture2D(image1.width, image1.height, image1.format, image1.mipmapCount > 1);
        duplicateTexture.filterMode = image1.filterMode;
        duplicateTexture.wrapMode = image1.wrapMode;
        Graphics.CopyTexture(image1, duplicateTexture);

        // обрезать фото по кругу
        image = duplicateTexture;
        radius = image.width;
        for (int x=0; x< image.width; x++)
        {
            for (int y = 0; y < image.height; y++)
            {
                if((x - radius / 2) * (x - radius / 2) + (y - radius / 2) * (y - radius / 2) >= (radius/2)* (radius / 2))
                {
                    
                    image.SetPixel(x, y, Color.clear);
                   
                }
            }
        }
        image.Apply(); // обязательная строчка для сохранения изменений
        GetComponent<RawImage>().texture = image;
        //
        image.SetPixel(5, 6, Color.clear); // удалить пиксель, прозрачный
        image.SetPixel(5, 6, Color.red); // закрасить пиксель
    }
}
