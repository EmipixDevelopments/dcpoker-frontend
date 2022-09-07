using UnityEngine;

public static class StaticUiCommon
{
    public static Canvas GetRootCanvas(RectTransform rectTransform)
    {
        var canvas = GetCanvas(rectTransform);
        return canvas.isRootCanvas ? canvas : canvas.rootCanvas;
    }
    
    public static Canvas GetCanvas(RectTransform rectTransform)
    {
        return rectTransform.GetComponentInParent<Canvas>();
    }
}
