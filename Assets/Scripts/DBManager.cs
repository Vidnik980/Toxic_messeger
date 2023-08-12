using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TMP_Text consoleLogin;
    [SerializeField] private TMP_Text consoleRegistration;
    public void Awake()
    {

        dbManager = this;
        SaveData.URL(URL);
        print(LoadData.LoadDataName(DataName.url));
        Autologin();
    }
    private void Autologin()
    {
        string userName = LoadData.LoadDataName(DataName.userName);
        string password = LoadData.LoadDataName(DataName.password);
        if (userName.Length > 0 && password.Length > 0)
        {
            Login(userName, password);
        }
    }
    public void Register(RegistrationData data)
    {
        StartCoroutine(Registration(data));
    }
    public void Login(string userName, string password)
    {
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
            consoleRegistration.text = "Error " + www.error;
            yield break;
        }
        SaveData.RegistrationData(data);
        SaveData.TokenData(www.text);
        consoleRegistration.text =  "Server tolk: " + "Check your email";
    }
    private IEnumerator LogIn(string userName, string password)
    {        
        string url = URL + $"/api/v1/login?userName={userName}&password={password}";
        print(url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            consoleLogin.text = "Error " + www.error;
            yield break;
        }
        consoleLogin.text = "Server response: " + www.downloadHandler.text;
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
