using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class clubResultPrefab : MonoBehaviour
{

    #region PUBLIC_VARIABLES 
    //[Header ("Gamobjects")]

    //[Header ("Transforms")]


    //[Header ("ScriptableObjects")]


    //[Header ("DropDowns")]


    //[Header ("Images")]


    [Header("Text")]
    public Text txtRank;
    public Text txtPlayerName;
    public Text txtHands;
    public Text txtBuyIn;
    public Text txtProfit;


    //[Header ("Prefabs")]

    //[Header ("Enums")]


    [Header("Variables")]
    public ReportResultData stuff;
    #endregion 
    #region PRIVATE_VARIABLES 
    #endregion 
    #region UNITY_CALLBACKS 

    #endregion 
    #region DELEGATE_CALLBACKS 

    #endregion 
    #region PUBLIC_METHODS     public void SetData(ReportResultData Data)
    {
        stuff = Data;
        txtRank.text = Data.rank.ToString();
        txtPlayerName.text = Data.name;
        txtHands.text = Data.hand;
        txtBuyIn.text = Data.buyin.ToString();
        txtProfit.text = Data.profit.ToString();
    }


    #endregion 
    #region PRIVATE_METHODS 
    #endregion 
    #region COROUTINES 


    #endregion 

    #region GETTER_SETTER 

    #endregion 


}
