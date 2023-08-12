using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class GridGallery : MonoBehaviour
{
    [SerializeField] private float coeff;
    [SerializeField] private float mainPanelY;
    [SerializeField] private float imageX;
    [SerializeField] private float imageY;
    [SerializeField] private float spaceBeetwinImageY;
    public float distansNextImage;
    [SerializeField] private GameObject imagePref;
    [SerializeField] private ImageCropperDemo ImageCropperScr;
    [SerializeField] private RawImage imageCircle;
    public List<RectTransform> photo = new List<RectTransform>();
    public int number;
    public GridGallery gridGallery;
    private void Start()
    {
        gridGallery = this;
        distansNextImage += mainPanelY;
        imageX = Screen.width;
        imageY = imageX * coeff;
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imageX, mainPanelY);
    }
    public void AddImage()
    {
        CreatePhoto();
        ImageCropperScr.CropCircle(imageCircle, photo[number].GetComponent<RawImage>());
        number++;
    }
    public void RemoveImage()
    {
        for (int i = number; i > 0; i--)
        {
            Destroy(photo[number].gameObject);
            photo.RemoveAt(number);
            distansNextImage -= spaceBeetwinImageY + imageY;
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imageX, distansNextImage);
        }
        number = 0;
    }
    public void LoadImagefromInternet(int cout)
    {
        RemoveImage();
        for (int i = 0; i < cout; i++)
        {
            CreatePhoto();
            number++;
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imageX, distansNextImage);
        }
    }
    public void CreatePhoto()
    {
        photo.Add(Instantiate(imagePref).GetComponent<RectTransform>());
        photo[number].SetParent(transform);
        photo[number].sizeDelta = new Vector2(imageX, imageY);
        photo[number].anchoredPosition = new Vector2(0, distansNextImage);
        distansNextImage += spaceBeetwinImageY + imageY;
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imageX, distansNextImage);
        
    }
}
