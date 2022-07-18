using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoContentSize : MonoBehaviour
{
    private TextMeshProUGUI contentText;
    private RectTransform rectTransform;
    Vector2 size;
    int deltaSize = 10;

    // Start is called before the first frame update
    void Start()
    {
        contentText = GetComponent<TextMeshProUGUI>();
        rectTransform = contentText.GetComponent<RectTransform>();
        size = rectTransform.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        if (contentText.isTextOverflowing)
        {
            size.y += deltaSize;
            rectTransform.sizeDelta = size;
        }
    }
}
