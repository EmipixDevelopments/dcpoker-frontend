using System;
using UnityEngine;

public class OnChangeCanvasEvent : MonoBehaviour
{
    private Action _onChangeSizeCanvasAction;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnRectTransformDimensionsChange()
    {
        _onChangeSizeCanvasAction?.Invoke();
    }

    public void AddListener(out RectTransform rectTransform, Action onChangeSizeCanvasAction)
    {
        _onChangeSizeCanvasAction += onChangeSizeCanvasAction;
        rectTransform = _rectTransform;
    }

    public void RemoveListener(Action onChangeSizeCanvasAction)
    {
        _onChangeSizeCanvasAction -= onChangeSizeCanvasAction;
    }
    
}
