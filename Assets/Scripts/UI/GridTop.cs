using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridTop : MonoBehaviour
{
    [SerializeField] private float coeff;
    [SerializeField] private float imageX;
    [SerializeField] private float imageY;
    [SerializeField] private float spaceBeetwinImageY;
    public float distansNextImage;
    [SerializeField] private GameObject imagePref;
    public List<RectTransform> photo = new List<RectTransform>();
    public int number;
    public LoadTopList loadTopList;
    [SerializeField] private ButtonManagerMain buttonManager;
    
    private void OnEnable()
    {
        number = 0;
        imageX = Screen.width;
        imageY = imageX * coeff;
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imageX, 0);
        loadTopList.LoadTop(this);
    }
    public void AddProfile(Texture2D texturePhoto,double ratingProfile,string nickNameProfile, int idProfile)
    {
        //CreatePhoto();
        photo[number].GetComponent<SettingPanelTop>().AddData(texturePhoto, nickNameProfile, ratingProfile, number + 1);
        photo[number].gameObject.GetComponent<Button>().onClick.AddListener(() => buttonManager.OpenSelectProfile(idProfile));
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
        number = 0;
    }
    private void CreatePhoto()
    {
        photo.Add(Instantiate(imagePref).GetComponent<RectTransform>());
        photo[number].SetParent(transform);
        photo[number].sizeDelta = new Vector2(imageX, imageY);
        photo[number].anchoredPosition = new Vector2(0, -distansNextImage - imageY/2);
        distansNextImage += spaceBeetwinImageY + imageY;
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imageX, distansNextImage);
        
    }
}
