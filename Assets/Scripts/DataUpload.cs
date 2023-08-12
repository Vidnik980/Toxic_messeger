using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUpload : MonoBehaviour
{
    [SerializeField] private LoadYourProfile loadYourProfile;
    void Start()
    {
        loadYourProfile.LoadProfile();
    }
}
