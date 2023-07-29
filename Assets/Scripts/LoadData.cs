using UnityEngine;
public enum DataName
{
    userName,
    email,
    password,
    idProfile,
    url
}
public static class LoadData
{
    public static string LoadDataName(DataName dataName)
    {
        string name = dataName.ToString();
        if (PlayerPrefs.HasKey(name))
        {
            string data = PlayerPrefs.GetString(name);
            return data;
        }
        return name;
    }
}
