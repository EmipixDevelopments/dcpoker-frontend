using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelMessageContactSupport : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _replyButton;
    [SerializeField] private TextMeshProUGUI _messageText;

    private string _id;

    private void Start()
    {
        _closeButton.onClick.AddListener(OnClickCloseButton);
        _replyButton.onClick.AddListener(OnClickReplyButton);
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(OnClickCloseButton);
        _replyButton.onClick.RemoveListener(OnClickReplyButton);
    }

    public void Open(MessageData messageData)
    {
        _messageText.text = messageData.Message;
        _id = messageData.ID;
        
        gameObject.SetActive(true);
    }

    private void OnClickReplyButton()
    {
        ClosePanel();
        UIManager.Instance.PanelContactSupportPopup.gameObject.SetActive(true);
    }
    
    private void OnClickCloseButton()
    {
        ClosePanel();
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    
}
