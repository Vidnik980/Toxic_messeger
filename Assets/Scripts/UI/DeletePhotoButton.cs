using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePhotoButton : MonoBehaviour
{
    [SerializeField] private GameObject buttonDelete;
    public long idPhoto;
    public void OpenButtonDelete()
    {
        buttonDelete.SetActive(true);
        StartCoroutine(CloseButtonDelete());
    }
    private IEnumerator CloseButtonDelete()
    {
        yield return new WaitForSeconds(1);
        buttonDelete.SetActive(false);
    }
    public void DeletePhoto()
    {

    }
}
