using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleTMProHelper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color _selectColor;
    [SerializeField] private Color _unSelectColor;

    private Toggle _toggle = null;
    private bool lastState = false;

    void Start()
    {
        _toggle = GetComponent<Toggle>();
    }

    void Update()
    {
        if (_toggle != null)
        {
            if (_toggle.isOn != lastState)
            {
                lastState = _toggle.isOn;
                if (lastState)
                {
                    _text.color = _selectColor;
                }
                else
                {
                    _text.color = _unSelectColor;
                }
            }
        }
    }
}
