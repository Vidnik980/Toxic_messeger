using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class AlignText : MonoBehaviour
{
    private float xLengthPanel;
    private float yLengthPanel;
    [SerializeField] private float percentX;
    [SerializeField] private float percentY;
    [SerializeField] private float yPosPercent;
    [SerializeField] private GameObject childActivate;
    private void Awake()
    {
        xLengthPanel = transform.parent.GetComponent<RectTransform>().rect.width;
        yLengthPanel = transform.parent.GetComponent<RectTransform>().rect.height;
        RectTransform panelObj = transform.GetComponent<RectTransform>();
        float xLengthImage = xLengthPanel * (percentX / 100f);
        float yLengthImage = yLengthPanel * (percentY / 100f);
        panelObj.sizeDelta = new Vector2(xLengthImage, yLengthImage);
        panelObj.anchoredPosition = new Vector2(0, yLengthImage / 2 + yLengthPanel * (yPosPercent / 100f));
        if (childActivate != null)
            childActivate.SetActive(true);
    }
}
