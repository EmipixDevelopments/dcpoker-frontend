using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestHTTP.SocketIO;
using TMPro;
using UnityEngine;

public class Messages : MonoBehaviour
{
    [SerializeField] private GameObject _notificationBubbleGameObject;
    [SerializeField] private TextMeshProUGUI _notificationBubbleText;

    private MessagesDetails _messagesDetails;
    private List<string> _messagesReadId;

    private int _oldMessageAmount;

    public MessagesDetails GetMessagesDetails() 
        => _messagesDetails;
    
    private void OnEnable()
    {
        CheckMessage();
    }

    public void CheckMessage()
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
        
        _messagesDetails = JsonUtility.FromJson<MessagesDetails>(source);
        if(_messagesDetails.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UpdateMessageInfo();
        }
    }

    public void SetMessagesReadId(List<string> messagesReadId)
    {
        _messagesReadId = messagesReadId;
    }

    private void UpdateMessageInfo()
    {
        if(_messagesDetails == null)
            return;
        
        var amount = _messagesDetails.result.Count(result => !result.read && (_messagesReadId == null || !_messagesReadId.Contains(result._id)));
        var isNeedUpdate = amount > 0;
        
        _notificationBubbleGameObject.SetActive(isNeedUpdate);
        if (isNeedUpdate)
        {
            _notificationBubbleText.text = amount.ToString();
        }
    }
}
