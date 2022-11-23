using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBubble : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _message;
    [SerializeField] private Button _readButton;

    //private MessagesDetails.Result _data;
    private MessageData _messageData;
    private PanelProfileNew _panelProfileNew;

    private void Start()
    {
        _readButton.onClick.AddListener(OnClickReadButton);
    }

    public void Init(PanelProfileNew panelProfileNew)
    {
        _panelProfileNew = panelProfileNew;
    }

    public void SetData(MessageData messageData)
    {
        _messageData = messageData;
        SetMessage();
    }

    private void SetMessage()
    {
        if (_messageData.Message.Length > 50)
        {
            var text = _messageData.Message.Substring(0,50) + "...";
            _message.text = text;
        }
        else
        {
            _message.text = _messageData.Message;
        }
    }

    private void OnClickReadButton()
    {
        var uiManager = UIManager.Instance;
        uiManager.PanelMessageContactSupport.Open(_messageData);
        _panelProfileNew.AddMessageToRead(_messageData);
        //_panelProfileNew.UpdateMessages();
    }

    //public string GetMessageId() => _data._id;
    //public bool IsRead() => _data != null && _data.read;

    /*public bool IsEqual(MessageBubble messageBubble)
    {
        if (_data == null || messageBubble._data == null)
            return false;
        
        return _data._id == messageBubble._data._id;
    }

    public MessagesDetails.Result GetData() => _data;*/
}
