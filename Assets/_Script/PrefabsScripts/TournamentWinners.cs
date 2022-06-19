using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TournamentWinners : MonoBehaviour
{


    #region PUBLIC_VARIABLES

    //[Header ("Gamobjects")]

    //[Header ("Transforms")]


    //[Header ("ScriptableObjects")]


    //[Header ("DropDowns")]


    //[Header ("Images")]


    [Header("Text")]
    public Text WinnerName;
    public TextMeshProUGUI WinnerPrice;
    public Image Profile;


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
        Clear();
    }
    // Update is called once per frame
    void Update()
    {

    }
    #endregion


    #region DELEGATE_CALLBACKS

    #endregion


    #region PUBLIC_METHODS
    public void Clear()
    {
        WinnerName.text = "";
        WinnerPrice.text = "";
        Profile.sprite = null;
    }
    #endregion

    #region PRIVATE_METHODS

    #endregion


    #region COROUTINES

    #endregion


    #region GETTER_SETTER

    #endregion



}
