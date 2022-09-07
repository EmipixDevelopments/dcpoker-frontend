using UnityEngine;
using UnityEngine.UI;

public class LayoutElementMinSizeHeighAsCanvasSetter : LayoutElement
{
    private RectTransform _rectTransform;
    private OnChangeCanvasEvent _onChangeCanvasEvent;

    protected override void Start()
    {
        base.Start();
        
        var canvas = StaticUiCommon.GetCanvas(GetComponent<RectTransform>());

        _onChangeCanvasEvent = canvas.GetComponent<OnChangeCanvasEvent>();
        if (canvas.gameObject.TryGetComponent(out _onChangeCanvasEvent))
        {
            _onChangeCanvasEvent.AddListener(out _rectTransform,OnChangeRectTransformCanvas);
            minHeight = _rectTransform.rect.height;
            return;
        }
        
        Debug.LogError("Not find OnChangeCanvasEvent on Canvas!");
    }
    
    private void OnChangeRectTransformCanvas()
    {
        minHeight = _rectTransform.rect.height;
    }
}
