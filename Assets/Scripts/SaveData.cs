using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DBManager;

public static class SaveData
{
    public static void RegistrationData(RegistrationData data)
    {
        PlayerPrefs.SetString("userName", data.userName);
        PlayerPrefs.SetString("email", data.email);
        PlayerPrefs.SetString("password", data.password);
        PlayerPrefs.Save();
    }
    public static void TokenData(string token)
    {
        PlayerPrefs.SetString("token", token);
        PlayerPrefs.Save();
    }
    public static void IDProfile(string id)
    {
        PlayerPrefs.SetString("idProfile", id);
        PlayerPrefs.Save();
    }
    public static void URL(string url)
    {
        PlayerPrefs.SetString("url", url);
        PlayerPrefs.Save();
    }
}
