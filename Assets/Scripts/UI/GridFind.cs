using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridFind : MonoBehaviour
{
    [SerializeField] private float coeff;
    [SerializeField] private float imageX;
    [SerializeField] private float imageY;
    [SerializeField] private float spaceBeetwinImageY;
    public float distansNextImage;
    [SerializeField] private GameObject imagePref;
    [SerializeField] private ImageCropperDemo ImageCropperScr;
    [SerializeField] private RawImage imageCircle;
    public List<RectTransform> photo = new List<RectTransform>();
    public int number;

    private void Start()
    {
        imageX = Screen.width;
        imageY = imageX * coeff;
    }
    public void AddImage()
    {
        CreatePhoto();
        ImageCropperScr.CropCircle(imageCircle, photo[number].GetComponent<RawImage>());
        number++;
    }
    public void RemoveImage()
    {
        for (int i = 0; i < number; i++)
        {
            Destroy(photo[0].gameObject);
            photo.RemoveAt(0);
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
    public void LoadDataProfile(Texture2D image, string name)
    {
        photo[number].GetComponent<SettingPanelTop>().AddData(image, name);
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
