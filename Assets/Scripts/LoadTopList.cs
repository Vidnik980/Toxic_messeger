using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class TopDTO
{
    public long idProfile;
    public String profileName;
    public double ratingUser;
    public long topUser;
    public byte[] lastCirclePhoto;
}
public class LoadTopList : MonoBehaviour
{
    private TopDTO[] topDTO;
    private string URL;
    private string[] nickNameProfile = new string[50];
    private double[] ratingProfile = new double[50];
    private GridTop gridTopScr;
    private Texture2D[] texturePhoto = new Texture2D[50];
    public List<long> topList;
    private float timer;
    private bool isTimer = false;
    public void FixedUpdate()
    {
        if (isTimer)
        {
            timer += Time.deltaTime;
        }
    }
    private void Awake()
    {
        URL = LoadData.LoadDataName(DataName.url);
    }
    public void LoadTop(GridTop gridTop)
    {
        gridTopScr = gridTop;
        StartCoroutine(StartLoadingTop());
    }
    public void ReturnDataProfile(long number)
    {
        texturePhoto[number] = new Texture2D(1, 1);
        texturePhoto[number].LoadImage(topDTO[number].lastCirclePhoto);
        ratingProfile[number] = Math.Round(topDTO[number].ratingUser, 2);
        int idProfile = (int)topDTO[number].idProfile;
        gridTopScr.AddProfile(texturePhoto[number], ratingProfile[number], topDTO[number].profileName, idProfile);
    }
    private IEnumerator StartLoadingTop()
    {
        yield return StartCoroutine(GetTopFirst50());
        gridTopScr.LoadImagefromInternet(topDTO.Length);
        for (int number = 0; number < topDTO.Length; number++)
        {
            ReturnDataProfile(number);
        }
    }
    private IEnumerator GetTopFirst50()
    {
        string url = URL + "/dto/top";
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
                topDTO = JsonConvert.DeserializeObject<TopDTO[]>(jsonResponse);
            }
        }
    }
}
