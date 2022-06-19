using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TournaryAllTableList : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    [Header("Gamobjects")]

    /*[Header ("Transforms")]
	 

	[Header ("ScriptableObjects")]
	 

	[Header ("DropDowns")]*/


    [Header("Images")]
    public Image BarMain;

    //	[Header("InputField")] 

    [Header("Button")]
    public Button JoinButton;

    [Header("Text")]
    public TextMeshProUGUI Type;
    public Text GameName;
    public TextMeshProUGUI Seats;
    public TextMeshProUGUI Blind;
    public TextMeshProUGUI minBuyin;
    public TextMeshProUGUI Status;

    [Header("Colors")]
    public Sprite[] Colors;
    /*[Header ("Prefabs")]
	 
	[Header ("Enums")]
	 */

    [Header("ScriptableObjects")]
    public TournamentRoomObject.TournamentRoom data;

    [Header("Variables")]
    public string TournamentTableId;
    public string roomId;
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
    public void OnTournamentTableSelectButtonTap()
    {
        UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.TournamentDetailsId = TournamentTableId;
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.GetDetailsTournamentButtonTap(TournamentTableId, data.pokerGameType);
    }
    public void SetData()// (RoomsListing.Room data, int i)
    {
        /*Date.text = System.DateTime.Today.ToString("dd/MM/yyyy");
		Type.text = "Touranament";
		TOtalCoins.text = "1000";
		Amount.text = "10000";
		this.Open ();
		*/
    }

    public void SetSngData(TournamentRoomObject.TournamentRoom data, int i)
    {
        this.data = data;

        if (string.IsNullOrEmpty(data.type) == false)
        {
            Type.text = data.type;
        }
        else
        {
            Type.text = "---";
        }
        if (string.IsNullOrEmpty(data.name) == false)
        {
            //GameName.text = data.name;
            Utility.Instance.CheckHebrewOwn(GameName, data.name);
        }
        else
        {
            GameName.text = "---";
        }

        //		if (data.playerCount > 0) {
        Seats.text = data.seat;//data.playerCount + "/" + data.maxPlayers;
                               //		} else {
                               //			Seats.text = data.playerCount + "/9";
                               //		}

        //if ( (data.blinds) == false) {

        Blind.text = data.blinds.ToString();
        //} else {
        //Blind.text = "---";
        //}
        if (i % 2 == 0)
        {
            BarMain.sprite = Colors[0];
        }
        else
        {
            BarMain.sprite = Colors[1];
        }
        if (string.IsNullOrEmpty(data.type) == false)
        {

            Type.text = data.type.ToUpper();
        }
        else
        {
            Type.text = "---";
        }

        if (string.IsNullOrEmpty(data.buyIn) == false)
        {
            minBuyin.text = data.buyIn.ToString();
        }
        else
        {
            minBuyin.text = "---";
        }

        if (string.IsNullOrEmpty(data.status) == false)
        {

            Status.text = data.status.ToUpper();
        }
        else
        {
            Status.text = "---";
        }

        TournamentTableId = data.id;
        //game.text = data.isLimitGame;
        //txtPlayers.text = data.maxPlayers;
        //		if (i % 2 == 0) {
        //			BarMain.sprite = Colors [0];
        //		} else {
        //			BarMain.sprite = Colors [1];
        //		}
        this.Open();

    }

    public void JoinButtonTap()
    {
        UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.Open();
    }

    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion
}
