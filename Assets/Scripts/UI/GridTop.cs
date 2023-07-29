using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
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
    public Texture2D[] izi = new Texture2D[5];
    
    private void OnEnable()
    {
        number = 0;
        imageX = Screen.width;
        imageY = imageX * coeff;
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imageX, 0);
        loadTopList.LoadTop(this);
    }
    public void AddProfile(Texture2D texturePhoto,double ratingProfile,string nickNameProfile)
    {
       // CreatePhoto();
        photo[number].GetComponent<SettingPanelTop>().AddData(texturePhoto, nickNameProfile, ratingProfile, number + 1);
        izi[number] = texturePhoto;
        number++;
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
