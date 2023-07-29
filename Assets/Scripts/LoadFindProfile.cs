using Newtonsoft.Json;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
public class SearchDTO
{
    public int idProfile;
    public String profileName;
    public byte[] lastCirclePhoto;
}

public class LoadFindProfile : MonoBehaviour
{
    private SearchDTO[] searchDTO;
    [SerializeField] private TMP_Text nameText;
    ProfileUserEntity[] profile;
    private string URL;
    public string[] nickNameProfile;
    public double[] ratingProfile;
    public long[] topProfile;
    public GridFind gridFindScr;
    public Texture2D[] texturePhoto = new Texture2D[100];

    private void Start()
    {
        URL = LoadData.LoadDataName(DataName.url);
    }
    public void StartFind()
    {
        StartCoroutine(StartFindEnum());
    }
    private IEnumerator StartFindEnum()
    {
        yield return StartCoroutine(GetProfile());
        for (int number = 0; number < searchDTO.Length; number++)
        {
            Texture2D texturePhoto = new Texture2D(1, 1);
            texturePhoto.LoadImage(searchDTO[number].lastCirclePhoto);
            gridFindScr.LoadDataProfile(texturePhoto, searchDTO[number].profileName);
        }
    }
    private IEnumerator GetProfile()
    {
        name = nameText.text.Remove(nameText.text.Length - 1);
        string url = URL + $"/dto/search/{name}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            searchDTO = JsonConvert.DeserializeObject<SearchDTO[]>(jsonResponse);
            gridFindScr.LoadImagefromInternet(searchDTO.Length);
        }
        else
        {
            Debug.LogError("Failed to retrieve profile: " + request.error);
        }
    }
}
