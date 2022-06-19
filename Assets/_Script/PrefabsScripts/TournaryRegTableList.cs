using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TournaryRegTableList : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    [Header("Gamobjects")]

    /*[Header ("Transforms")]
	 

	[Header ("ScriptableObjects")]
	 

	[Header ("DropDowns")]*/


    [Header("Images")]
    public Image BarMain;
    [Header("Colors")]
    public Sprite[] Colors;

    //	[Header("InputField")] 

    [Header("Button")]
    public Button JoinButton;

    [Header("Text")]
    public TextMeshProUGUI Type;
    public Text GameName;
    public TextMeshProUGUI buyIn;
    public TextMeshProUGUI dateTime;
    public TextMeshProUGUI players;
    public TextMeshProUGUI status;

    /*[Header ("Prefabs")]
	 
	[Header ("Enums")]
	 */

    [Header("ScriptableObjects")]
    public NormalTournamentDetails.NormalTournamentData data;

    [Header("Variables")]
    public string TournamentTableId;
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

    public void SetSngData(NormalTournamentDetails.NormalTournamentData data, int i)
    {
        this.data = data;
        JoinButton.Close();
        status.Close();
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

        players.text = data.players.ToString();
        //	dateTime.text = data.dateTime;
        if (string.IsNullOrEmpty(data.dateTime) == false)
        {
            dateTime.text = data.dateTime;
        }
        else
        {
            dateTime.text = "---";
        }
        buyIn.text = data.buyIn.ToString();
        if (string.IsNullOrEmpty(data.type) == false)
        {

            Type.text = data.type.ToUpper();
        }
        else
        {
            Type.text = "---";
        }

        if (string.IsNullOrEmpty(data.status) == false)
        {

            status.text = data.status.ToUpper();
            status.Open();
        }
        else
        {
            status.text = "---";
            status.Open();
        }
        if ((data.status.Equals("Running") || data.status.Equals("running") || data.status.Equals("RUNNING")) && data.isJoinable)
        {
            status.Close();
            JoinButton.Open();
        }
        else
        {
            JoinButton.Close();
            status.text = data.status.ToUpper();
            status.Open();
        }
        if (i % 2 == 0)
        {
            BarMain.sprite = Colors[0];
        }
        else
        {
            BarMain.sprite = Colors[1];
        }
        TournamentTableId = data.tournamentId;

        this.Open();

    }

    public void JoinButtonTap()
    {
        UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.Open();
    }
    public void onjoinTournamentRoomButtonTap()
    {
        UIManager.Instance.SocketGameManager.JoinTournamentRoom(this.data.tournamentId, (socket, packet, args) =>
        {
            print("JoinTournamentRoom response: " + packet.ToString());

            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse<RoomsListing.Room> JoinTournamentRoomResp = JsonUtility.FromJson<PokerEventResponse<RoomsListing.Room>>(resp1);
            if (JoinTournamentRoomResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                if (!UIManager.Instance.tableManager.IsMiniTableTournamentExisted(JoinTournamentRoomResp.result.tournamentId))
                    UIManager.Instance.LobbyScreeen.GetRunningGameList();

                if (UIManager.Instance.IsMultipleTableAllowed && !UIManager.Instance.tableManager.playingTableList.Contains(JoinTournamentRoomResp.result.roomId))
                {
                    UIManager.Instance.tableManager.DeselectAllTableSelection();
                    UIManager.Instance.tableManager.AddMiniTable(JoinTournamentRoomResp.result);
                }
                UIManager.Instance.tableManager.DeselectAllTableSelection();

                MiniTable miniTable = UIManager.Instance.tableManager.GetMiniTable(JoinTournamentRoomResp.result.roomId);
                if (miniTable != null)
                {
                    UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.Close();
                    miniTable.MiniTableButtonTap();
                }

            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(JoinTournamentRoomResp.message);
            }
        });
    }
    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion
}
