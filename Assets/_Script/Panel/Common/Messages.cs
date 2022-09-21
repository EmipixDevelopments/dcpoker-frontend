using System;
using System.Collections;
using System.Collections.Generic;
using BestHTTP.SocketIO;
using TMPro;
using UnityEngine;

public class Messages : MonoBehaviour
{
    [SerializeField] private GameObject _notificationBubbleGameObject;
    [SerializeField] private TextMeshProUGUI _notificationBubbleText;
    
    private List<MessageData> _messages;
    private Coroutine _messageUpdateCoroutine;
    private Action _onUpdateMessagesAction;

    private void Start()
    {
        _messages = new List<MessageData>();
    }

    private IEnumerator MessageUpdateEnumerator()
    {
        while (true)
        {
            CheckMessage();
            yield return new WaitForSeconds(30);  
        }
    }

    private void OnEnable()
    {
        _messageUpdateCoroutine = StartCoroutine(MessageUpdateEnumerator());
    }

    private void OnDisable()
    {
        StopCoroutine(_messageUpdateCoroutine);
    }

    private void CheckMessage()
    {
        var uiManager = UIManager.Instance;
        
        if (uiManager != null)
        {
            uiManager.SocketGameManager.ContactUs(OnMessageReceived);
        }
    }
    
    private void OnMessageReceived(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("OnMessagesReceived : " + packet.ToString());

        var arr = new JSONArray(packet.ToString());
        var source = arr.getString(arr.length() - 1);

        var messagesDetails = JsonUtility.FromJson<MessagesDetails>(source);
        
        if(messagesDetails == null)
            return;
        if(messagesDetails.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UpdateMessageInfo(messagesDetails);
        }
    }

    private void UpdateMessageInfo(MessagesDetails messagesDetails)
    {
        var amount = 0;
        _messages.Clear();
        
        foreach (var t in messagesDetails.result)
        {
            if (t.read || t.userId!= null && t.userId._id == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
                continue;
            
            _messages.Add(new MessageData{Message = t.message, ID = t._id});
        }
        //_messages.Reverse();
        _onUpdateMessagesAction?.Invoke();
        UpdateNotificationBubble();
    }

    private void UpdateNotificationBubble()
    {
        var isNeedUpdate = _messages.Count > 0;
        
        _notificationBubbleGameObject.SetActive(isNeedUpdate);
        
        if (isNeedUpdate)
        {
            _notificationBubbleText.text = _messages.Count.ToString();
        }
    }

    public void AddReedMessage(MessageData messageData)
    {
        var message = _messages.Find(messagePredicate => messagePredicate.ID == messageData.ID);
        if(message == null)
            return;
        
        UIManager.Instance.SocketGameManager.ReadContactUs(message.ID,(socket, packet, args) =>
        {
            //add check status
            _messages.Remove(message);
            
            _onUpdateMessagesAction?.Invoke();
            UpdateNotificationBubble();
        });
    }

    public void AddUpdateListener(out List<MessageData> messages, Action action)
    {
        messages = _messages;
        _onUpdateMessagesAction += action;
    }

    public void RemoveUpdateListener(Action action)
    {
        _onUpdateMessagesAction -= action;
    }
    
}

public class MessageData
{
    public string ID;
    public string Message;
}
