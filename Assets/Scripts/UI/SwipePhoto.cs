using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SwipePhoto : MonoBehaviour
{
    private Vector2 tapPosition;
    private Vector2 swipeDelta;
    [SerializeField] private float swipeDuration = 80;
    [SerializeField] private ActionPhoto[] actionPhoto;
    [SerializeField] private float timer;
    public int number;
    public bool delay = true;
    [SerializeField] private Slider sliderRating;
    private ImageLoad imageLoad;
    [SerializeField] private float percentUpSwipe;
    [SerializeField] private float percentDownSwipe;
    private void Start()
    {
        imageLoad = ImageLoad.imageLoad;
        percentUpSwipe = transform.parent.GetComponent<RectTransform>().rect.height / 100f * percentUpSwipe;
        percentDownSwipe = transform.parent.GetComponent<RectTransform>().rect.height / 100f * percentDownSwipe;
    }
    public int Number
    {
        get
        {
            return number;
        }
        set
        {
            number = value;
            if (number == 2)
                number = 0;
        }
    }
    private void Update()
    {
        if (Input.mousePosition.y <= percentUpSwipe && Input.mousePosition.y >= percentDownSwipe)
        {
            if (Input.GetMouseButtonDown(0))
            {
                print(Input.mousePosition.y + " mouse");
                tapPosition = Input.mousePosition;

            }
            else if (Input.GetMouseButtonUp(0) && tapPosition.y != 0)
            {
                CheckSwipe();
                ResetSwipe();
            }
        }
    }
    private void CheckSwipe()
    {
        if(delay == true)
        {
            swipeDelta = Vector2.zero;
            swipeDelta = (Vector2)Input.mousePosition - tapPosition;
            if (swipeDelta.magnitude > swipeDuration)
            {
                delay = false;
                Number++;
                imageLoad.StartPostRating(sliderRating.value, actionPhoto[Number].NowIDPhoto);
                actionPhoto[Number].NextPhoto();
                StartCoroutine(Timer());
            }
        }
    }
    private void ResetSwipe()
    {
        tapPosition = Vector2.zero;
        swipeDelta = Vector2.zero;
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        delay = true;
    }
}
