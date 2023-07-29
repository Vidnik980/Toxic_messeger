using UnityEngine;

public class GridButton : MonoBehaviour
{
    private float xLength;
    private float yLength;
    [SerializeField] private float spaceWallPercent;
    [SerializeField] private float coeffButton;
    [SerializeField] private float numberElements;
    private float space;

    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        xLength = rectTransform.rect.width;
        yLength = rectTransform.rect.height * coeffButton;
        spaceWallPercent = Screen.width / 100f * spaceWallPercent;
        space = (Screen.width - (spaceWallPercent * 2) - yLength * 4)/3;
        for (int i = 0; i < numberElements; i++)
        {
            RectTransform image = transform.GetChild(i).GetComponent<RectTransform>();
            image.sizeDelta = new Vector2(yLength, yLength);
            image.anchoredPosition = new Vector2(spaceWallPercent + yLength/2 + i*(yLength +space), 0);
        }
    }
}
