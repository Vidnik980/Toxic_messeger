using UnityEngine;
using UnityEngine.UI;

public class CirclePhotoAlignment : MonoBehaviour
{
    private float xBias;
    private float yBias;
    private float coeffScale;
    private void Start()
    {
        Transform image = transform.GetChild(0);
        coeffScale = transform.GetComponent<RectTransform>().rect.width / image.GetComponent<RawImage>().texture.width;
        float xScale = image.GetComponent<RawImage>().texture.width * coeffScale;
        float yScale = image.GetComponent<RawImage>().texture.height * coeffScale;
        image.transform.localPosition = new Vector2(xBias, yBias);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(xScale, yScale);
    }
}
