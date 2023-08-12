using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanelSettings : MonoBehaviour
{
    private float yLengthCanvas;
    [SerializeField] private float percentY;
    private bool isActive;
    private RectTransform rect;
    private float maxLengthImage;
    [SerializeField] private float speedPercent;
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private GameObject panelClose;
    private void Start()
    {
        yLengthCanvas = transform.parent.GetComponent<RectTransform>().rect.height;
        maxLengthImage = yLengthCanvas * (percentY * 2 / 100f);
        rect = panelSettings.GetComponent<RectTransform>();
        speedPercent = yLengthCanvas / 100 * speedPercent;
    }

    private void Update()
    {
        if(isActive == true && rect.sizeDelta.y < maxLengthImage)
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + speedPercent);
        }
        if (isActive == false && rect.sizeDelta.y > 0)
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y - speedPercent);
        }
    }
    public void CloseSetting()
    {
        isActive = false;
        panelClose.SetActive(false);
    }
    public void OpenSetting()
    {
        isActive = true;
        panelClose.SetActive(true);
    }
    public void ChangeAccount()
    {
        RegistrationData Data = new RegistrationData();
        SaveData.RegistrationData(Data);
        SceneLoad.ChangeScene(0);
    }
}
