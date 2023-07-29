using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PercentageSizePanel : MonoBehaviour
{
    private float xLengthCanvas;
    private float yLengthCanvas;
    [SerializeField] private float percentX;
    [SerializeField] private float percentY;
    [SerializeField] private GameObject childActivate;
    [SerializeField] private float shiftPercentX;
    [SerializeField] private float shiftPercentY;
    private void Awake()
    {
        xLengthCanvas = transform.parent.GetComponent<RectTransform>().rect.width;
        yLengthCanvas = transform.parent.GetComponent<RectTransform>().rect.height;
        shiftPercentX =  shiftPercentX / 100 * xLengthCanvas;
        shiftPercentY =  shiftPercentY / 100 * yLengthCanvas;
        RectTransform panelObj = transform.GetComponent<RectTransform>();
        float xLengthImage = xLengthCanvas * (percentX / 100f);
        float yLengthImage = yLengthCanvas * (percentY / 100f);
        panelObj.sizeDelta = new Vector2(xLengthImage, yLengthImage);
        if (shiftPercentX == 0)
            panelObj.anchoredPosition = new Vector2(0, yLengthImage / 2 + shiftPercentY);
        else
            panelObj.anchoredPosition = new Vector2(shiftPercentX, yLengthImage / 2 + shiftPercentY);
        if (childActivate != null)
            childActivate.SetActive(true);
    }
}
