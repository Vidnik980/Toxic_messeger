using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManagerMain : MonoBehaviour
{
    [SerializeField] private GameObject canvas1Grade;
    [SerializeField] private GameObject canvas2Top;
    [SerializeField] private GameObject canvas3Reseach;
    [SerializeField] private GameObject canvas4Profile;
    [SerializeField] private GameObject canvas5SelectProfile;
    [SerializeField] private LoadYourProfile loadYourProfile;
    private GameObject canvasActive;
    private GameObject lastCanvas;
    private void Start()
    {
        canvasActive = canvas4Profile;
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
    public void OpenSelectProfile(int idProfile)
    {
        OpenCanvas(canvas5SelectProfile);
        loadYourProfile.LoadProfile(idProfile);
    }
    public void ReturnCanvas()
    {
        OpenCanvas(lastCanvas);
    }
    private void OpenCanvas(GameObject canvas)
    {
        if (canvas != canvasActive)
        {
            lastCanvas = canvasActive;
            canvas.SetActive(true);
            canvasActive.SetActive(false);
            canvasActive = canvas;
        }
    }
}
