using TMPro;
using UnityEngine;

public class ButtonManagerLogin : MonoBehaviour
{
    [Header("URL")]
    [SerializeField] private TMP_Text URL;

    [SerializeField] private GameObject registrationPanel;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject ImagePanel;
    [Header ("Registration")]
    [SerializeField] private TMP_Text userName;
    [SerializeField] private TMP_Text mail;
    [SerializeField] private TMP_Text password;
    [Header ("Login")]
    [SerializeField] private TMP_Text userLog;
    [SerializeField] private TMP_Text passwordLog;
    [Header("Image")]
    [SerializeField] private TMP_Text ID;

    [SerializeField] private DBManager DBscr;
    [SerializeField] private ImageLoad imageLoad;

    public void SaveURL()
    {
        SaveData.URL(URL.text.Remove(URL.text.Length - 1));
    }
    public void OpenRegistrationPanel()
    {
        registrationPanel.SetActive(true);
    }
    public void CloseRegistrationPanel()
    {
        registrationPanel.SetActive(false);
    }
    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
    }
    public void CloseLoginPanel()
    {
        loginPanel.SetActive(false);
    }
    public void OpenImagePanel()
    {
        ImagePanel.SetActive(true);
    }
    public void CloseImagePanel()
    {
        ImagePanel.SetActive(false);
    }
    public void Registration() 
    {
        RegistrationData data = new RegistrationData
        {
            userName = userName.text.Remove(userName.text.Length - 1), //удаление лишнего(пустого) символа
            email = mail.text.Remove(mail.text.Length - 1),
            password = password.text.Remove(password.text.Length - 1)
        };
        DBscr.Register(data);
    }
    public void Login()
    {
        string userNameStr = userLog.text.Remove(userLog.text.Length - 1);
        string passwordStr = passwordLog.text.Remove(passwordLog.text.Length - 1);
        DBscr.Login(userNameStr, passwordStr);
    }
}
