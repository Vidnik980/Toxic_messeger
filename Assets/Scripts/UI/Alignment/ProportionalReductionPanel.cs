using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ProportionalReductionPanel : MonoBehaviour
{
    private RectTransform parentPanel;
    private float xLengthParent;
    private float yLengthParent;
    private RectTransform panel;
    private float xLengthPanel;
    private float yLengthPanel;
    [SerializeField] private float percent;
    [SerializeField] private GameObject childActivate;
    private void OnEnable() //срабатывает раньше родительской панели, исправить
    {
        parentPanel = transform.parent.GetComponent<RectTransform>();
        xLengthParent = parentPanel.rect.width;
        yLengthParent = parentPanel.rect.height;
        
        panel = transform.GetComponent<RectTransform>();
        xLengthPanel = panel.rect.width;
        yLengthPanel = panel.rect.height;
        float coefficient1 = xLengthParent / xLengthPanel;
        float coefficient2 = yLengthParent / yLengthPanel;
        float coefficient;
        if (coefficient1 < coefficient2)
        {
            coefficient = (xLengthParent * (percent / 100f)) /(xLengthPanel);
            
        }
        else
        {
            coefficient = (yLengthParent * (percent / 100f)) / yLengthPanel;
        }
        panel.sizeDelta = new Vector2(xLengthPanel, yLengthPanel) * coefficient;
        if(childActivate != null)
        childActivate.SetActive(true);
    }
}
