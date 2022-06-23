using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleImage : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _on;
    [SerializeField] private Sprite _off;
    [Space]
    [SerializeField] private bool _defaultStateIsTrue;

    public Action<bool> OnChanged = null;

    private bool lastState = false;

    public void ChangeState() 
    {
        _defaultStateIsTrue = !_defaultStateIsTrue;
        OnChanged?.Invoke(_defaultStateIsTrue);
    }

    public void SetState(bool newState) 
    {
        _defaultStateIsTrue = newState;
        OnChanged?.Invoke(_defaultStateIsTrue);
    }

    void Update()
    {
        if (_defaultStateIsTrue != lastState)
        {
            if (_defaultStateIsTrue)
            {
                _image.sprite = _on;
            }
            else
            {
                _image.sprite = _off;
            }
            lastState = _defaultStateIsTrue;
        }
    }
}
