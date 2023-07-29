using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManagerMain : MonoBehaviour
{
    [SerializeField] private GameObject canvas1Grade;
    [SerializeField] private GameObject canvas2Top;
    [SerializeField] private GameObject canvas3Reseach;
    [SerializeField] private GameObject canvas4Profile;
    private GameObject canvasActive;
    private void Start()
    {
        canvasActive = canvas1Grade;
    }
    public void OpenCanvasGrade()
    {
        OpenCanvas(canvas1Grade);
    }
    public void OpenCanvasTop()
    {
        OpenCanvas(canvas2Top);
    }
    public void OpenCanvasReseach()
    {
        OpenCanvas(canvas3Reseach);
    }
    public void OpenCanvasProfile()
    {
        OpenCanvas(canvas4Profile);
    }
    private void OpenCanvas(GameObject canvas)
    {
        if (canvas != canvasActive)
        {
            canvas.SetActive(true);
            canvasActive.SetActive(false);
            canvasActive = canvas;
        }
    }
}
