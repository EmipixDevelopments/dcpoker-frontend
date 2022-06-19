#if UNITY_WEBGL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
using System;
using TMPro;

public class keyboardClass : MonoBehaviour, ISelectHandler
{

    [DllImport("__Internal")]
    private static extern void focusHandleAction(string _name, string _str);

    public void ReceiveInputData(string value)
    {
        gameObject.GetComponent<TMP_InputField>().text = value;
        //	gameObject.GetComponent<TMP_InputField>().contentType = TMP_InputField.ContentType.IntegerNumber;
    }

    public void OnSelect(BaseEventData data)
    {
        try
        {
            focusHandleAction(gameObject.name, gameObject.GetComponent<TMP_InputField>().text);
            UIManager.Instance.MainHomeScreen.loginPageFocusCounter = 6;
        }
        catch (Exception error)
        {
            Debug.Log(error.ToString());
        }
    }
}
#endif