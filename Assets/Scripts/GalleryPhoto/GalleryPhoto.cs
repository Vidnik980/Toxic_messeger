using ImageCropperNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryPhoto : MonoBehaviour
{
    [SerializeField] private RawImage imageCut;
    [SerializeField] private GameObject panelCut;
    [SerializeField] private ImageCropperDemo imageCropeScr;
    [SerializeField] private GridGallery gridGallery;
    public void OpenGallery()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                Texture2D texture = NativeGallery.LoadImageAtPath(path);
                if (texture != null)
                {
                    // Отобразить загруженное изображение на экране
                    panelCut.SetActive(true);
                    imageCut.texture = texture;
                    imageCut.GetComponent<ProportiontalPhoto>().Change();
                    gridGallery.AddImage();
                    
                }
            }
        }, "Выберите изображение", "image/*");
    }
}
