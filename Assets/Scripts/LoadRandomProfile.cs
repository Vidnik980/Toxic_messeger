using Newtonsoft.Json;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Profiling;

public class RatingDTO
{
    public string profileName;
    public double ratingUser;
    public long topUser;
    public long idPhoto;
    public long idProfile;
    public string photoRectangle;
}
public class LoadRandomProfile : MonoBehaviour
{
    private RatingDTO[] ratingDTO = new RatingDTO[2];
    private RatingDTO[] ratingReserveDTO = new RatingDTO[2];
    private string URL;
    private ActionPhoto actionPhoto;
    private ProfileUserEntity[] profileUserEntity;
    public long[] idRandomPhoto;
    private Texture2D textureRandomPhoto;
    private double ratingRandomPhoto;
    private long topRandomPhoto;
    private string nickNameRandomProfile;
    [SerializeField] ActionPhoto actionPhoto1;
    [SerializeField] ActionPhoto actionPhoto2;
    public int number;
    private void Awake()
    {
        URL = LoadData.LoadDataName(DataName.url);
    }
    private void Start()
    {
        StartCoroutine(BeginerLoadPhoto());
    }
    private IEnumerator BeginerLoadPhoto()
    {
        yield return StartCoroutine(GetRandomIDPhoto(2));
        ratingDTO = ratingReserveDTO;
        StartLoadingTexture(actionPhoto1);
        StartLoadingTexture(actionPhoto2);
        StartCoroutine(GetRandomIDPhoto(2));
    }
    public void LoadRandomPhoto(ActionPhoto thisScr)
    {
        StartLoadingTexture(thisScr);
    }
    public IEnumerator ReturnDataProfile()
    {
        topRandomPhoto = ratingDTO[number].topUser;
        ratingRandomPhoto = Math.Round(ratingDTO[number].ratingUser, 2);
        nickNameRandomProfile = ratingDTO[number].profileName;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(actionPhoto.DataLoad(textureRandomPhoto, ratingRandomPhoto, topRandomPhoto, ratingDTO[number].idPhoto, nickNameRandomProfile));
    }
    private void StartLoadingTexture(ActionPhoto thisScr)
    {
        actionPhoto = thisScr;
        if (ratingDTO.Length - 1 < number)
        {
            print("null");
            number = 0;
            ratingDTO = ratingReserveDTO;
            ratingReserveDTO = new RatingDTO[2];
            StartCoroutine(GetRandomIDPhoto(2));
        }
        if(ratingDTO[number].photoRectangle != null)
        {
            print(ratingDTO.Length);
            textureRandomPhoto = new Texture2D(1, 1);
            byte[] bytesImage = Convert.FromBase64String(ratingDTO[number].photoRectangle);
            textureRandomPhoto.LoadImage(bytesImage);
            ReturnDataProfile();
            number++;
        }

    }
    private IEnumerator GetRandomIDPhoto(int count)
    {
        string url = URL + $"/dto/random/{count}";
        print(url);
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
                string jsonResponse = request.downloadHandler.text;
                print(jsonResponse);

                ratingReserveDTO = JsonConvert.DeserializeObject<RatingDTO[]>(jsonResponse);
                print("reserve" + ratingReserveDTO.Length);
            }
        }
    }
}
