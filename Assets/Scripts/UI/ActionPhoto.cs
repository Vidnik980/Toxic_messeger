
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionPhoto : MonoBehaviour
{
    [SerializeField] private bool firstPhoto;
    private RawImage image;
    public bool isTransparent;
    public bool isVisible;
    public float nowTransparent;
    public float speedTr = 0.1f;
    [SerializeField] ActionPhoto actionPhoto2;
    [SerializeField] private ImageLoad imageLoad;
    [SerializeField] private TMP_Text NickNameText;
    [SerializeField] private TMP_Text ratingText;
    private string initialTextRating;
    [SerializeField] private TMP_Text topText;
    private string initialTextTop;
    public long NowIDPhoto;
    public LoadRandomProfile loadRandomProfile;
    [SerializeField] private RawImage backGround;

    private void Start()
    {
        initialTextRating = ratingText.text;
        initialTextTop = topText.text;
        image = transform.GetComponent<RawImage>();
        nowTransparent = image.color.a;
        //imageLoad.GetRandomPhoto(image);
        //loadRandomProfille = new LoadRandomProfille();
    }
    public void NextPhoto()
    {
        isTransparent = true;
        isVisible = false;
        actionPhoto2.OpenPhoto();
        //imageLoad.GetRandomPhoto(image);
        loadRandomProfile.LoadRandomPhoto(this);
    }
    public IEnumerator DataLoad(Texture2D texture, double rating, long top, long idPhoto, string nickname)
    {
        yield return new WaitForSeconds(0.3f);
        image.texture = texture;
        NickNameText.text = nickname;
        ratingText.text = initialTextRating + rating.ToString();
        topText.text = initialTextTop + top.ToString();
        NowIDPhoto = idPhoto;
    }
    private void OpenPhoto()
    {
        isTransparent = false;
        isVisible = true;
    }
    private void Update()
    {
        if (isTransparent == true && nowTransparent > 0)
        {
            nowTransparent -= Time.deltaTime * speedTr;
            image.color = new Color(1, 1, 1, nowTransparent);
            backGround.color = new Color(1,1,1,nowTransparent);
            ratingText.color = new Color(1, 1, 1, nowTransparent);
            topText.color = new Color(1, 1, 1, nowTransparent);
            NickNameText.color = new Color(1, 1, 1, nowTransparent);
            if (nowTransparent <= 0)
            {
                isTransparent = false;
            }
        }
        if (isVisible == true && nowTransparent < 1)
        {
            nowTransparent += Time.deltaTime * speedTr;
            image.color = new Color(1, 1, 1, nowTransparent);
            backGround.color = new Color(1, 1, 1, nowTransparent);
            ratingText.color = new Color(1, 1, 1, nowTransparent);
            topText.color = new Color(1, 1, 1, nowTransparent);
            NickNameText.color = new Color(1, 1, 1, nowTransparent);
            if (nowTransparent >= 1)
            {
                isVisible = false;
            }
        }

    }
}