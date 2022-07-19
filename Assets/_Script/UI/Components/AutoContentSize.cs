using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoContentSize : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private RectTransform textRectTransform;
    [SerializeField] private RectTransform content;
    
    private Vector2 contentSize;
    private Vector2 textSize;
    private int deltaSize = 10;

    // Start is called before the first frame update
    void Start()
    {
        textSize = textRectTransform.sizeDelta;
        contentSize = content.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        if (text != null && text.isTextOverflowing)
        {
            contentSize.y += deltaSize;
            content.sizeDelta = contentSize;

            textSize.y += deltaSize;
            textRectTransform.sizeDelta = textSize;
        }
    }
}
