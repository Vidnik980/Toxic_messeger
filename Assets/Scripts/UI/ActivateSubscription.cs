using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ActivateSubscription : MonoBehaviour
{
    [SerializeField] private TMP_Text inscriptionText;
    private RawImage image;
    [SerializeField] private Color colorActive;
    [SerializeField] private Color isColorActive;
    private bool isActive;
    [SerializeField] private float timer;
    private void Start()
    {
        image = GetComponent<RawImage>();
    }
    public void Subscription()
    {
        inscriptionText.gameObject.SetActive(true);
        if (isActive == false)
        {
            image.color = colorActive;
            isActive = true;
            inscriptionText.text = "subscribed";
            StartCoroutine(TimerText());
        }
        else
        {
            image.color = isColorActive;
            isActive = false;
            inscriptionText.text = "unsubscribed";
            StartCoroutine(TimerText());
        }
        
    }
    private IEnumerator TimerText()
    {
        yield return new WaitForSeconds(timer);
        inscriptionText.gameObject.SetActive(false);
    }

}
