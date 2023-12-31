using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class LoadYourProfile : MonoBehaviour
{
    [SerializeField] private RawImage circleImageProfile;
    [SerializeField] private TMP_Text subscrip; // ����������� �����������
    [SerializeField] private TMP_Text subscripPeople; // ����������� ��������
    [SerializeField] private TMP_Text nowRating;
    [SerializeField] private TMP_Text nowTop;
    [SerializeField] private TMP_Text nickName; 
    private string initialTextRating;
    private string initialTextTop;
    [SerializeField] private LoadProfile loadProfile;
    [SerializeField] private GridGallery gridGallery;
    public int IdProfile;
    private void OnEnable()
    {
        initialTextRating = nowRating.text;
        initialTextTop = nowTop.text;
    }
    public void LoadProfile(int id = 0)
    {
        if (id == 0)
            IdProfile = int.Parse(LoadData.LoadDataName(DataName.idProfile));
        else
            IdProfile = id;
        loadProfile.LoadMainPanel(this, gridGallery, IdProfile);
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
