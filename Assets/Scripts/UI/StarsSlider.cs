using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsSlider : MonoBehaviour
{
    private float maxSize;
    private RectMask2D rm2D;
    private float nowValue;
    [SerializeField] private Slider slider;
    private void Start()
    {
        rm2D = GetComponent<RectMask2D>();
        maxSize = GetComponent<RectTransform>().rect.width;
    }
    public bool AddValue(float value)
    {
        nowValue += value;
        if (nowValue < maxSize)
        {
            rm2D.padding = new Vector4(0, 0, 0, maxSize - nowValue);
            return true;
        }
        else
            return false;
    }
    private void Update()
    {
        nowValue = (slider.value / slider.maxValue) * maxSize;
        rm2D.padding = new Vector4(0, 0, maxSize - nowValue, 0);
    }
}
