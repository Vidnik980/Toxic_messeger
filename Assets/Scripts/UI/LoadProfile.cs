using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class ProfileUserEntity
{
    public long id;
    public string profileName;
    public double ratingUser;
    public long topUser;
    public int[] subscriberId;
    public int[] subscriptionId;
    public int[] allIdCirclePhotoUser;
    public int[] allIdRectanglePhotoUser;
}
[System.Serializable]
public class UserPhotoEntityDemo
{
    public long id;
    public byte[] photoRectangle;
    public byte[] photoCircle;
    public int countMark;
    public double sumMark;
    public double ratingPhoto;
    public long topPhoto;
}
[System.Serializable]
public class UserPhotoDTO
{
    public string photoRectangle;
    public long idPhoto;
    public int countMark;
    public double sumMark;
    public double ratingPhoto;
    public long topPhoto;
}
[System.Serializable]
public class ProfileUserDTO
{
    public string profileName;
    public double ratingUser;
    public long topUser;
    public string lastCirclePhoto;
    public List<UserPhotoDTO> userPhotos;
}
public class LoadProfile : MonoBehaviour
{
    ProfileUserDTO profile;
    public RawImage aa;
    public Texture2D aaa;
    public string URL;
    public int idProfile;
    public Texture2D texturePhoto;
    public double ratingRandomPhoto;
    public long topRandomPhoto;
    public string nickNameRandomProfile;
    public int sub1;
    public int sub2;
    public int[] CirclePhotoUser = new int[10];
    public int[] RectanglePhotoUser = new int[10];
    public LoadYourProfile profileScr;
    [SerializeField] private GridGallery gridGallery;
    private int number1;



    bool a = false;
    float timer;
    private void OnEnable()
    {
        URL = LoadData.LoadDataName(DataName.url);
    }
    public void LoadMainPanel(LoadYourProfile thisScr, int id)
    {
        
        idProfile = id;
        StartCoroutine(StartLoadingTexture(thisScr));
    }
    public void ReturnDataProfile()
    {
        profileScr.DataLoad(texturePhoto, ratingRandomPhoto, topRandomPhoto, sub1, sub2, nickNameRandomProfile);
        print(3);
    }
    private IEnumerator StartLoadingTexture(LoadYourProfile thisScr)
    {
        profileScr = thisScr;
        yield return StartCoroutine(GetProfile(idProfile));
        gridGallery.LoadImagefromInternet(profile.userPhotos.Count);
        CircleImage();
        for (int number = 0; number < profile.userPhotos.Count; number++)
        {
            yield return ImageInformation(number);
        }
        ReturnDataProfile();
        print(3);
    }
    private IEnumerator GetProfile(long idProfile)
    {
        a = true;
        string url = URL + $"/dto/{idProfile}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            profile = JsonUtility.FromJson<ProfileUserDTO>(jsonResponse);   
            ratingRandomPhoto = profile.ratingUser;
            ratingRandomPhoto = Math.Round(ratingRandomPhoto, 2);
            topRandomPhoto = profile.topUser;
            nickNameRandomProfile = profile.profileName;
            print("profile" + timer);
            a = false;
        }
        else
        {
            Debug.LogError("Failed to retrieve profile: " + request.error);
        }
    }
    private IEnumerator ImageInformation(int number)
    {
        number1++;
        byte[] bytes = Convert.FromBase64String(profile.userPhotos[profile.userPhotos.Count - number - 1].photoRectangle);
        //byte[] decompressedImageBytes = ImageCompressionUtility.DecompressImage(bytes);
        aaa = new Texture2D(1, 1);
        aaa.LoadImage(bytes);
        yield return gridGallery.photo[number].GetComponent<RawImage>().texture = aaa;
        gridGallery.photo[number].GetComponent<DeletePhotoButton>().idPhoto = profile.userPhotos[profile.userPhotos.Count - number - 1].idPhoto;
    }
    private void CircleImage()
    {
        byte[] bytes = Convert.FromBase64String(profile.lastCirclePhoto);
        // Сохраняем массив байтов в файл
        //System.IO.File.WriteAllBytes("Assets/SavedTextures/name", bytes);
        texturePhoto = new Texture2D(1, 1);
        texturePhoto.LoadImage(bytes);
    }
    private void Update()
    {
        if (a == true)
            timer += Time.deltaTime;
    }
}