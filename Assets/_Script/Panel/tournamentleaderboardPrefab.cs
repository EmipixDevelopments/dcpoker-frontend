using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tournamentleaderboardPrefab : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    //[Header ("Gamobjects")]

    //[Header ("Transforms")]


    //[Header ("ScriptableObjects")]


    //[Header ("DropDowns")]


    //[Header ("Images")]


    [Header("Text")]
    public Text txtPosition;
    public Text txtPlayerName;
    public Text txtChips;
    public Text txtRebuys;
    public Text txtaddOn;


    //[Header ("Prefabs")]

    //[Header ("Enums")]


    [Header("Variables")]
    public TournamentLeaderBoardData stuff;
    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS


    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void SetData(TournamentLeaderBoardData Data)
    {
        stuff = Data;
        if (!string.IsNullOrEmpty(Data.position))
        {
            txtPosition.text = Data.position.ToString();
        }
        else
        {
            txtPosition.text = "-";
        }
        if (!string.IsNullOrEmpty(Data.username))
        {
            txtPlayerName.text = Data.username;
        }
        else
        {
            txtPlayerName.text = "-";
        }
        if (!string.IsNullOrEmpty(Data.chips))
        {
            txtChips.text = Data.chips;

        }
        else
        {
            txtChips.text = "-";
        }
        if (!string.IsNullOrEmpty(Data.rebuys))
        {
            txtRebuys.text = Data.rebuys.ToString();
        }
        else
        {
            txtRebuys.Close();
        }
        if (!string.IsNullOrEmpty(Data.addon))
        {
            txtaddOn.text = Data.addon.ToString();
        }
        else
        {
            txtaddOn.Close();
        }

    }


    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion



}
