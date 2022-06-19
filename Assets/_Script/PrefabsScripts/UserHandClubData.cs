using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UserHandClubData : MonoBehaviour
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

    public GamesHistoryListItem data;
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
    public void ButtonHold(Animator a)
    {
        a.SetBool("IsJoin", true);
    }

    public void ButtonRelease(Animator a)
    {
        a.SetBool("IsJoin", false);
    }

    public void SetData(GamesHistoryListItem Playerdata)// (RoomsListing.Room data, int i)
    {
        this.data = Playerdata;
        Postion.text = Playerdata.dateTime.ToString();
        Player.text = Playerdata.gameName;

        this.Open();

    }
    public void FullGameHistoryCall()
    {

        //UIManager.Instance.GameScreeen.OnPreviousHandButtonTap(UIManager.Instance.GameScreeen.PreviousGameId);
        UIManager.Instance.GameScreeen.OnPreviousHandButtonTap(this.data.gameId);

    }

    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion
}
