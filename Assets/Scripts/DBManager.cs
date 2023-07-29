using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RegistrationData
{
    public string userName;
    public string email;
    public string password;
}
public class DBManager : MonoBehaviour
{
    public string URL;
    public DBManager dbManager;
    public void Awake()
    {
        dbManager = this;
        URL = LoadData.LoadDataName(DataName.url);
        print(LoadData.LoadDataName(DataName.url));
        RegistrationData data = new RegistrationData
        {
            userName = "vlad",
            email = "vidnikevich-vlad@mail.ru",
            password = "1234",

        };
       // StartCoroutine(PhotoEvaluation());
    }
    public void Register(RegistrationData data)
    {
        URL = LoadData.LoadDataName(DataName.url);
        StartCoroutine(Registration(data));

    }
    public void Login(string userName, string password)
    {
        URL = LoadData.LoadDataName(DataName.url);
        StartCoroutine(LogIn(userName, password));
        //StartCoroutine(GetID(userName, password));
    }
    private IEnumerator Registration(RegistrationData data)
    {
        print(URL);
        print(data);
        string url = URL + "/api/v1/registration";
        string json = JsonUtility.ToJson(data); ;
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        WWW www = new WWW(url, postData, headers);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("Error " + www.error);
            yield break;
        }
        SaveData.RegistrationData(data);
        SaveData.TokenData(www.text);
        Debug.Log("Server tolk: " + "Check your email");
    }
    private IEnumerator LogIn(string userName, string password)
    {
        print(userName);
        if (userName == null)
            userName = LoadData.LoadDataName(DataName.userName);
        if (password == null)
            password = LoadData.LoadDataName(DataName.password);
        
        string url = URL + $"/api/v1/login?userName={userName}&password={password}";
        print(url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error " + www.error);
            yield break;
        }
        Debug.Log("Server response: " + www.downloadHandler.text);
        if (www.downloadHandler.text == "Login successful!")
        {
            yield return StartCoroutine(GetID(userName, password));
            SceneLoad.ChangeScene(1);
        }
    }
    private IEnumerator GetID(string userName, string password)
    {
        string url = URL + $"/users/id/{userName}";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error " + www.error);
            yield break;
        }
        Debug.Log("Server response: " + www.downloadHandler.text);
        SaveData.IDProfile(www.downloadHandler.text);
    }
    
}
