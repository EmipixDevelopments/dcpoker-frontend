using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCMManager : MonoBehaviour
{
    /*
    // Use this for initialization
#if !UNITY_WEBGL
    void Start()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
        UIManager.Instance.assetOfGame.SavedLoginData.fcmRegistrationToken = token.Token;
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);
        Debug.Log("Received a new Data : " + e.Message.Data.ToString());
        Debug.Log("Received a new RawData : " + e.Message.RawData.ToString());
        Debug.Log("Received a new Body : " + e.Message.Notification.Body);
        Debug.Log("Received a new Title : " + e.Message.Notification.Title);

        //			UIManager.Instance.messagePanel. (e.Message.Notification.Body);
        UIManager.Instance.DisplayMessagePanel(e.Message.Notification.Body);
    }
#endif

    */
}
