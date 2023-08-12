using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelTop : MonoBehaviour
{
    [SerializeField] private RawImage photoImage;
    [SerializeField] private TMP_Text ratingText;
    [SerializeField] private TMP_Text nickNameText;
    [SerializeField] private TMP_Text topText;
    public int IdProfile;
    public void AddData(Texture2D texturePhoto, string nickNameProfile, double ratingProfile = 0, long top = 0, int idProfile = 0)
    {
        if (photoImage != null)
            photoImage.texture = texturePhoto;
        if (ratingText != null)
            ratingText.text = ratingProfile.ToString();
        if (nickNameText != null)
            nickNameText.text = nickNameProfile;
        if (topText != null)
            topText.text = top.ToString();
        IdProfile = idProfile;
    }
}
