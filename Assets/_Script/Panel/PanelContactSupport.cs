using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class PanelContactSupport : MonoBehaviour
{
    [SerializeField] private TMP_InputField _messageInputField;
    [SerializeField] private TextMeshProUGUI _charCounterText;

    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _sendButton;


    // Start is called before the first frame update
    void Start()
    {
        _closeButton.onClick.RemoveAllListeners();
        _sendButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(OnClickCloseButton);
        _sendButton.onClick.AddListener(OnClickSendButton);

        _messageInputField.onValueChanged.RemoveAllListeners();
        _messageInputField.onValueChanged.AddListener(InputFieldValueChanged);
    }

    private void InputFieldValueChanged(string arg0)
    {
        _charCounterText.text = $"{_messageInputField.text.Length}/{_messageInputField.characterLimit}";
    }

    private void OnClickSendButton()
    {
        Debug.Log($"Sent to report: {_messageInputField.text}");
    }

    private void OnClickCloseButton()
    {
        gameObject.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
