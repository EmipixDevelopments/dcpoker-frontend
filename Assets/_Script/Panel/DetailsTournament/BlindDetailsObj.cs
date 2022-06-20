using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlindDetailsObj : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    [Header("Gamobjects")]



    [Header("Images")]
    public Image BarMain;

    [Header("Colors")]
    public Sprite[] Colors;


    //	[Header("InputField")] 

    //	[Header ("Button")]


    [Header("Text")]
    public TextMeshProUGUI Rank;
    public TextMeshProUGUI Blinds;
    public TextMeshProUGUI Duration;



    /*[Header ("Prefabs")]
	 
	[Header ("Enums")]
	 */

    [Header("ScriptableObjects")]
    public GetBlindDetails data;

    //		[Header ("Variables")] 
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
    public void JoinTournamentTableButtonap()
    {

    }
    public void SetData(GetBlindDetails Data, int i)// (RoomsListing.Room data, int i)
    {
        this.data = Data;
        Rank.text = Data.index.ToString();
        Blinds.text = Data.blinds.ToString();
        Duration.text = Data.duaration.ToString();
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

    /*public void SetSngData (TournamentRoomObject.TournamentRoom data)
	{
		this.data = data;

		if (string.IsNullOrEmpty (data.type) == false) {
			Type.text = data.type;
		} else {
			Type.text = "---";
		}
		if (string.IsNullOrEmpty (data.name) == false) {
			GameName.text = data.name;
		} else {
			GameName.text = "---";
		}

		//		if (data.playerCount > 0) {
		Seats.text = data.playerCount + "/" + data.maxPlayers;
		//		} else {
		//			Seats.text = data.playerCount + "/9";
		//		}

		//if ( (data.blinds) == false) {

		Blind.text = data.blinds.ToString();
		//} else {
		//Blind.text = "---";
		//}

		if (string.IsNullOrEmpty (data.type) == false) {

			Type.text = data.type.ToUpper();
		} else {
			Type.text = "---";
		}

		if (float.IsNaN (data.minBuyIn) == false) {
			minBuyin.text = data.minBuyIn.ToString ();
		} else {
			minBuyin.text = "---";
		}

		if (string.IsNullOrEmpty (data.status) == false) {

			Status.text = data.status.ToUpper();
		} else {
			Status.text = "---";
		}

		TournamentTableId = data.roomId;
		//game.text = data.isLimitGame;
		//txtPlayers.text = data.maxPlayers;
		//		if (i % 2 == 0) {
		//			BarMain.sprite = Colors [0];
		//		} else {
		//			BarMain.sprite = Colors [1];
		//		}
		this.Open ();

	}*/

    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion


}
