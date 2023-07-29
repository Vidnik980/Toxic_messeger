using TMPro;
using UnityEngine;

public class FontSize : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;
    [SerializeField] private float coefficient = 1f;
    private void Start()
    {
        if(m_Text != null)
        GetComponent<TMP_Text>().fontSize = m_Text.fontSize * coefficient;
    }
}