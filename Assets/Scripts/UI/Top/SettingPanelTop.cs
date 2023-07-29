using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelTop : MonoBehaviour
{
    [SerializeField] private RawImage photoImage;
    [SerializeField] private TMP_Text ratingText;
    [SerializeField] private TMP_Text nickNameText;
    [SerializeField] private TMP_Text topText;
    public void AddData(Texture2D texturePhoto, string nickNameProfile, double ratingProfile = 0, long top = 0)
    {
        photoImage.texture = texturePhoto;
        ratingText.text = ratingProfile.ToString();
        nickNameText.text = nickNameProfile;
        topText.text = top.ToString();
    }
}
