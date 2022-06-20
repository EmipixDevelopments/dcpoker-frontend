using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waitingTextPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    //[Header ("Gamobjects")]

    //[Header ("Transforms")]


    //[Header ("ScriptableObjects")]


    //[Header ("DropDowns")]


    //[Header ("Images")]


    [Header("Text")]
    public Text txtWaitingMessage;

    //[Header ("Prefabs")]

    //[Header ("Enums")]


    //[Header ("Variables")]

    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    void OnEnable()
    {


    }
    void OnDisable()
    {
        txtWaitingMessage.text = "";
    }

    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void SetData(string Message)
    {
        txtWaitingMessage.text = "";
        txtWaitingMessage.text = Message;
        this.Open();
    }
    public void closePanel()
    {
        this.Close();
    }

    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion



}
