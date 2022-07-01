using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleImage : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _on;
    [SerializeField] private Sprite _off;

    Toggle _toggle;
    private void OnEnable()
    {
        _toggle = GetComponent<Toggle>();
    }

    void Update()
    {
        if (_toggle != null)
        {
            if (_toggle.isOn)
            {
                _image.sprite = _on;
            }
            else
            {
                _image.sprite = _off;
            }
        }
    }
}
