using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadYourProfile : MonoBehaviour
{
    [SerializeField] private RawImage circleImageProfile;
    [SerializeField] private TMP_Text subscrip; // колличество подписчиков
    [SerializeField] private TMP_Text subscripPeople; // колличество подписок
    [SerializeField] private TMP_Text nowRating;
    [SerializeField] private TMP_Text nowTop;
    [SerializeField] private TMP_Text nickName; 
    private string initialTextRating;
    private string initialTextTop;
    [SerializeField] private LoadProfile loadProfile;
    private void Start()
    {
        int id = int.Parse(LoadData.LoadDataName(DataName.idProfile));
        initialTextRating = nowRating.text;
        initialTextTop = nowTop.text;
        print("start rabota");
        loadProfile.LoadMainPanel(this, id);

    }
    public void DataLoad(Texture2D texture, double rating, long top, int sub1, int sub2, string nick)
    {
        circleImageProfile.texture = texture;
        nickName.text = nick;
        nowRating.text = initialTextRating + rating.ToString();
        nowTop.text = initialTextTop + top.ToString();
        subscrip.text = sub1.ToString();
        subscripPeople.text = sub2.ToString();
    }
}
