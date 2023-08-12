using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInTime : MonoBehaviour
{
    [SerializeField] private float timer = 1;
    private void OnEnable()
    {
        StartCoroutine(TimerForClose());
    }
    private IEnumerator TimerForClose()
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}
