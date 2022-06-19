using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderBoardListData : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    [Header("Images")]
    public Image BarMain;

    [Header("Colors")]
    public Sprite[] Colors;

    //[Header("Gamobjects")]

    /*[Header ("Transforms")]
	 

	[Header ("ScriptableObjects")]
	 

	[Header ("DropDowns")]*/


    //[Header ("Images")]


    //	[Header("InputField")] 


    [Header("Text")]
    public TextMeshProUGUI Postion;
    public Text Player;
    public TextMeshProUGUI Amount;
    public TextMeshProUGUI Payouts;

    /*[Header ("Prefabs")]
	 
	[Header ("Enums")]
	 

	[Header ("Variables")]
*/
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
    public void SetData(TopPlayer Playerdata, int i)// (RoomsListing.Room data, int i)
    {
        Postion.text = Playerdata.position.ToString();
        Player.text = Playerdata.player;
        Amount.text = Playerdata.winRate.ToString();
        //	Amount.text = Playerdata.amount.ConvertToCommaSeparatedValue();
        //		Payouts.text = "1000";

        if (i % 2 == 0)
        {
            BarMain.sprite = Colors[0];
        }
        else
        {
            BarMain.sprite = Colors[1];
        }

        this.Open();

    }



    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion
}
