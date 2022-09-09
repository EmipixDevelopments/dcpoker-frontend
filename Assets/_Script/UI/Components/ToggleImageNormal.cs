using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleImageNormal : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _on;
    [SerializeField] private Sprite _off;

    private Toggle _toggle;

    private void Start()
    {
        _toggle = GetComponent<Toggle>();
        if (_toggle == null)
        {
            Debug.LogError("Toggle not find in : " + gameObject.name);
            return;
        }
        
        _toggle.onValueChanged.AddListener(SetSprite);
        SetSprite(_toggle.isOn);
    }
    
    private void SetSprite(bool active) =>
        _image.sprite = active ? _on : _off;

    public void AddListener(UnityAction<bool> action)
    {
        _toggle.onValueChanged.AddListener(action);
        action.Invoke(_toggle.isOn);
    }

    public void RemoveListener(UnityAction<bool> action)
        => _toggle.onValueChanged.RemoveListener(action);

    public void SetActive(bool active)
    {
        _toggle.isOn = active;
    }
}

