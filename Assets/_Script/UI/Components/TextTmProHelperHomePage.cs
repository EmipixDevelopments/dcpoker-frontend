using System;
using TMPro;
using UnityEngine;


public class TextTmProHelperHomePage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color _selectColor;
    [SerializeField] private Color _unSelectColor;

    private void OnEnable()
    {
        if (_text != null)
            _text.color = _selectColor;
    }

    private void OnDisable()
    {
        if (_text != null)
            _text.color = _unSelectColor;
    }
}
