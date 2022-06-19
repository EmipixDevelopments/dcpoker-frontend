using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamehistoryData : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    [Header("Gamobjects")]

    /*[Header ("Transforms")]
	 

	[Header ("ScriptableObjects")]
	 

	[Header ("DropDowns")]*/


    //[Header ("Images")]


    //	[Header("InputField")] 


    [Header("Text")]
    public Text Postion;
    public Text Player;
    public Text Amount;
    public Text Payouts;

    /*[Header ("Prefabs")]
	 
	[Header ("Enums")]
	 

*/
    [Header("Variables")]
    public ResultItem ResultHistory;
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

    }
    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void SetData(historyPlayer Playerdata)// (RoomsListing.Room data, int i)
    {
        Postion.text = Playerdata.position.ToString();
        Player.text = Playerdata.player;
        Amount.text = Playerdata.amount.ConvertToCommaSeparatedValue();
        //		Payouts.text = "1000";
        this.Open();

    }
    public void SetData(ResultItem Playerdata)// (RoomsListing.Room data, int i)
    {
        ResultHistory = Playerdata;
        Postion.text = Playerdata.dateTime.ToString();
        Player.text = Playerdata.gameType;
        Amount.text = Playerdata.amount;
        //		Payouts.text = "1000";
        this.Open();

    }
    public void FullGameHistoryCall()
    {
        if (ResultHistory.gameId.Equals(""))
        {
            UIManager.Instance.DisplayMessagePanel("Game Id is Null ");
            return;
        }
        else
        {
            //UIManager.Instance.LobbyScreeen.OnPreviousHandButtonTap(ResultHistory.gameId);
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
