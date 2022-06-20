using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DetailsTournament : MonoBehaviour
{


    #region PUBLIC_VARIABLES

    [Header("Gamobjects")]
    public GameObject[] SelectedGame;
    public GameObject[] SelectedGameHeader;
    public GameObject[] SelectedGameTableList;
    public GameObject RegisterButton;
    public GameObject UnRegisterButton;

    //[Header ("Transforms")]


    [Header("ScriptableObjects")]
    public PlayerDetailsTorunament PlayerDetailsTorunamentpanel;
    public TableDetails TournamentTableDetails;
    public payoutDetails payoutDetailsPanel;
    public payoutDetails bountypayoutsPanel;
    public BlindDetails BlindDetailsPanel;

    //[Header ("DropDowns")]



    [Header("Buttons")]

    public Button RegisterButton1;
    public Button UnRegisterButton1;
    public Button ReBuyButton;

    [Header("Text")]
    public Text TournamentName;
    public TextMeshProUGUI Stackes;
    public TextMeshProUGUI AllPlayers;
    public TextMeshProUGUI Status;
    public TextMeshProUGUI RegStatus;
    public TextMeshProUGUI BuyIn;
    public TextMeshProUGUI Players;
    public TextMeshProUGUI GameTypes;
    public TextMeshProUGUI PrizePool;
    public TextMeshProUGUI TournamentId;
    public TextMeshProUGUI MinPlayers;
    public TextMeshProUGUI MaxPlayers;
    public TextMeshProUGUI dateTime;
    public TextMeshProUGUI rebuyLevel;
    public TextMeshProUGUI rebuyLimit;
    public TextMeshProUGUI rebuy;
    public TextMeshProUGUI txtAddOn;
    public TextMeshProUGUI txtReBuyTime;

    //[Header ("Prefabs")]

    //[Header ("Enums")]


    [Header("Variables")]
    public int SelectedTab = 0;
    public string TournamentDetailsId = "";
    public string pokerGameType = "";
    #endregion

    #region PRIVATE_VARIABLES
    public string TID = "";
    public string nameSpaceStr = "";
    public long RebuyAmount;
    private double totalSeconds;
    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    void OnEnable()
    {
        PlayerDetailsTorunamentpanel.TournamentDetailsId = TournamentDetailsId;
        /*	TournamentTableDetails.TournamentDetailsId = TournamentDetailsId; 
			payoutDetailsPanel.TournamentDetailsId = TournamentDetailsId;
            bountypayoutsPanel.TournamentDetailsId = TournamentDetailsId;
			BlindDetailsPanel.TournamentDetailsId = TournamentDetailsId; */
        //if (TournamentDetailsId != "")
        //{
        //    if (UIManager.Instance.gameType == GameType.Touranment)
        //    {
        //        RegularTournamentEventCall();
        //    }
        //    if (UIManager.Instance.gameType == GameType.sng)
        //    {
        //        SngTournamentEventCall();
        //    }
        //}
        TournamentEventCall();

        SelectedTab = 0;
        ResetSelectedgameButtons(SelectedTab);
        dateTime.text = "";

    }

    void OnDisable()
    {
        TournamentDetailsId = "";
        PlayerDetailsTorunamentpanel.TournamentDetailsId = TournamentDetailsId;
        TournamentTableDetails.TournamentDetailsId = TournamentDetailsId;
        payoutDetailsPanel.TournamentDetailsId = TournamentDetailsId;
        bountypayoutsPanel.TournamentDetailsId = TournamentDetailsId;
        BlindDetailsPanel.TournamentDetailsId = TournamentDetailsId;
        RegisterButton1.interactable = true;
        UnRegisterButton1.interactable = true;
        totalSeconds = 0;
        CancelInvoke();
    }
    // Update is called once per frame
    void Update()
    {
        if (totalSeconds > 0)
        {
            totalSeconds -= 1 * Time.deltaTime;
            txtReBuyTime.Open();
            txtReBuyTime.text = "Remaining Time : " + string.Format("{1:D2}:{2:D2}", TimeSpan.FromSeconds(totalSeconds).Hours, TimeSpan.FromSeconds(totalSeconds).Minutes, TimeSpan.FromSeconds(totalSeconds).Seconds);
        }
        else
        {
            totalSeconds = 0;
            txtReBuyTime.text = "";
            txtReBuyTime.Close();
            //isBonusReady = true;
            //txtDailyBonus.text = "Bonus Ready!";
        }
    }
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void GetDetailsTournamentButtonTap(string TournamentId, string pokerGameType)
    {
        //UIManager.Instance.LobbyScreeen.LobbyTableList.SetActive (false);
        TournamentDetailsId = TournamentId;
        Constants.Poker.TournamentId = TournamentDetailsId;
        this.pokerGameType = pokerGameType;
        this.Open();
    }
    public void OnPlayersButtonTap()
    {
        SelectedTab = 0;
        UIManager.Instance.SoundManager.OnButtonClick();
        ResetSelectedgameButtons(SelectedTab);

    }
    public void OnTablesButtonTap()
    {
        SelectedTab = 1;
        UIManager.Instance.SoundManager.OnButtonClick();
        ResetSelectedgameButtons(SelectedTab);

    }
    public void OnPayOutsButtonTap()
    {
        SelectedTab = 2;
        UIManager.Instance.SoundManager.OnButtonClick();
        ResetSelectedgameButtons(SelectedTab);
    }
    public void bountypayoutsButtonTap()
    {
        SelectedTab = 3;
        UIManager.Instance.SoundManager.OnButtonClick();
        ResetSelectedgameButtons(SelectedTab);
    }
    public void OnBlindsButtonTap()
    {
        SelectedTab = 4;
        UIManager.Instance.SoundManager.OnButtonClick();
        ResetSelectedgameButtons(SelectedTab);
    }
    public void RebuyButtonaTap()
    {
        Game.Lobby.SetSocketNamespace = nameSpaceStr;
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.DisplayRebuyinConfirmationPanel("Do you want to Rebuy " /*+ response.result.buyInChips + " from " */+ RebuyAmount + " chips?", "Rebuy", "Cancel", () =>
        {
            UIManager.Instance.DisplayLoader("Please wait...");
            UIManager.Instance.RebuyInMessagePanel.btnAffirmativeAction.interactable = false;
            UIManager.Instance.SoundManager.OnButtonClick();
            UIManager.Instance.SocketGameManager.PlayerReBuyIn("", TID, (socket, packet, args) =>
            {
                Debug.Log(Constants.PokerEvents.PlayerReBuyIn + " Response : " + packet.ToString());
                UIManager.Instance.HidePopup();
                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;
                PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);
                UIManager.Instance.HideLoader();
                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    TournamentEventCall();
                    UIManager.Instance.DisplayMessagePanel(resp.message, null);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message, null);
                }

            });
        }, () =>
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            UIManager.Instance.SocketGameManager.DeclinedReBuyIn("", TID, (socket, packet, args) =>
            {
                Debug.Log(Constants.PokerEvents.DeclinedReBuyIn + " Response : " + packet.ToString());
                UIManager.Instance.HidePopup();
            });
            //UIManager.Instance.HidePopup();
        });
    }
    public void RegisterButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.gameType == GameType.Touranment)
        {
            UIManager.Instance.SocketGameManager.getRegisterTournament(TournamentDetailsId, pokerGameType, (socket, packet, args) =>
            {

                Debug.Log("getRegisterTournament  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(resp1);
                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                    RegisterButton.SetActive(false);
                    UnRegisterButton.SetActive(true);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }
            });
        }
        if (UIManager.Instance.gameType == GameType.sng)
        {
            UIManager.Instance.SocketGameManager.getRegisterSngTournament(TournamentDetailsId, pokerGameType, (socket, packet, args) =>
            {
                Debug.Log("RegisterSngTournament  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(resp1);


                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                    RegisterButton.SetActive(false);
                    UnRegisterButton.SetActive(true);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }

            });
        }
    }
    public void UnRegisterButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.gameType == GameType.Touranment)
        {
            UIManager.Instance.SocketGameManager.getUnRegisterTournament(TournamentDetailsId, pokerGameType, (socket, packet, args) =>
            {

                Debug.Log("getRegisterTournament  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(resp1);

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                    RegisterButton.SetActive(true);
                    UnRegisterButton.SetActive(false);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }
            });
        }
        if (UIManager.Instance.gameType == GameType.sng)
        {
            UIManager.Instance.SocketGameManager.getUnRegisterSngTournament(TournamentDetailsId, pokerGameType, (socket, packet, args) =>
            {
                Debug.Log("getUnRegisterSngTournament  : " + packet.ToString());
                UIManager.Instance.HideLoader();
                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(resp1);
                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                    RegisterButton.SetActive(true);
                    UnRegisterButton.SetActive(false);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }
            });
        }
    }
    #endregion

    #region PRIVATE_METHODS
    private void TournamentEventCall()
    {
        CancelInvoke();
        if (TournamentDetailsId != "")
        {
            if (UIManager.Instance.gameType == GameType.Touranment)
            {
                RegularTournamentEventCall();
            }
            if (UIManager.Instance.gameType == GameType.sng)
            {
                SngTournamentEventCall();
            }
        }
    }
    private void RegularTournamentEventCall()
    {
        if (TournamentDetailsId != "")
        {
            UIManager.Instance.SocketGameManager.getTournamentInfo(TournamentDetailsId, pokerGameType, (socket, packet, args) =>
{

    Debug.Log("getTournamentInfo  : " + packet.ToString());
    UIManager.Instance.HideLoader();
    JSONArray arr = new JSONArray(packet.ToString());
    string Source;
    Source = arr.getString(arr.length() - 1);
    var resp1 = Source;
    JSON_Object TournamentEventObj = new JSON_Object(resp1.ToString());
    PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(resp1);

    if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
    {
        Status.text = resp.result.status;
        RegStatus.text = resp.result.registrationStatus;
        BuyIn.text = resp.result.buyIn;
        Players.text = resp.result.players.ToString();
        GameTypes.text = resp.result.gameType;
        PrizePool.text = resp.result.prizePool.ConvertToCommaSeparatedValue();
        TournamentId.text = resp.result.id;
        MinPlayers.text = resp.result.min_players.ToString();
        MaxPlayers.text = resp.result.max_players.ToString();
        rebuy.text = resp.result.reBuy.ToString();
        rebuy.transform.parent.gameObject.SetActive(true);
        txtAddOn.text = resp.result.addOn.ToString();
        txtAddOn.transform.parent.gameObject.SetActive(true);

        //rebuyLevel.text = resp.result.reBuyEnds;
        //rebuyLimit.text = resp.result.reBuyLimit.ToString();
        //TournamentName.text = resp.result.name;
        Utility.Instance.CheckHebrewOwn(TournamentName, resp.result.name);
        Stackes.text = resp.result.buyIn;
        AllPlayers.text = resp.result.players.ToString();
        nameSpaceStr = resp.result.namespaceString;
        TID = resp.result.id;
        RebuyAmount = resp.result.rebuyAmount;
        dateTime.transform.parent.gameObject.SetActive(true);

        if (resp.result.allowRebuy)
        {
            RegisterButton.SetActive(false);
            UnRegisterButton.SetActive(false);
            ReBuyButton.Open();
            totalSeconds = resp.result.remainRebuySec;
        }
        else
        {
            RegisterButton.SetActive(!resp.result.isRegistered);
            UnRegisterButton.SetActive(resp.result.isRegistered);
            ReBuyButton.Close();
        }
        if (resp.result.registrationStatus.Equals("open") || resp.result.registrationStatus.Equals("Open"))
        {
            RegisterButton1.interactable = true;
            //UnRegisterButton1.interactable = true;
        }
        else
        {
            RegisterButton1.interactable = false;
            UnRegisterButton1.interactable = false;
            //UnRegisterButton.SetActive(false);
        }
        /*  if (resp.result.isRegistered)
          {

              if (resp.result.registrationStatus.Equals("open") || resp.result.registrationStatus.Equals("Open"))
              {
                  UnRegisterButton1.interactable = true;
              }
          }*/



        if (resp.result.dateTime == "")
        {
            dateTime.text = "-";
        }
        else
        {
            dateTime.text = resp.result.dateTime;
        }

        //if(resp.result.dateTime != "")
        //	dateTime.text = resp.result.dateTime.UTCTimeStringToLocalTime();
        //else
        //	dateTime.text = "-";
        Invoke("RegularTournamentEventCall", 5);
    }
    else
    {
        UIManager.Instance.DisplayMessagePanel(resp.message);
    }
});
        }
    }

    private void SngTournamentEventCall()
    {
        if (TournamentDetailsId != "")
        {
            UIManager.Instance.SocketGameManager.getSngTournamentInfo(TournamentDetailsId, pokerGameType, (socket, packet, args) =>
        {
            Debug.Log("getSngTournamentInfo  : " + packet.ToString());
            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;
            JSON_Object TournamentEventObj = new JSON_Object(resp1.ToString());
            PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                Status.text = resp.result.status;
                RegStatus.text = resp.result.registrationStatus;
                BuyIn.text = resp.result.buyIn;
                Players.text = resp.result.players.ToString();
                GameTypes.text = resp.result.gameType;
                PrizePool.text = resp.result.prizePool.ConvertToCommaSeparatedValue();
                TournamentId.text = resp.result.id;
                MinPlayers.text = resp.result.min_players.ToString();
                MaxPlayers.text = resp.result.max_players.ToString();
                rebuy.text = resp.result.reBuy.ToString();
                if (TournamentEventObj.has("reBuy"))
                {
                    txtAddOn.text = resp.result.reBuy.ToString();
                    rebuy.transform.parent.gameObject.SetActive(true);
                }
                else
                {
                    rebuy.transform.parent.gameObject.SetActive(false);
                }
                if (TournamentEventObj.has("addOn"))
                {
                    txtAddOn.text = resp.result.addOn.ToString();
                    txtAddOn.transform.parent.gameObject.SetActive(true);
                }
                else
                {
                    txtAddOn.transform.parent.gameObject.SetActive(false);
                }
                //rebuyLevel.text = resp.result.reBuyEnds;
                //rebuyLimit.text = resp.result.reBuyLimit.ToString();
                //TournamentName.text = resp.result.name;
                Utility.Instance.CheckHebrewOwn(TournamentName, resp.result.name);
                Stackes.text = resp.result.buyIn;
                AllPlayers.text = resp.result.players.ToString();
                nameSpaceStr = resp.result.namespaceString;
                TID = resp.result.id;
                RebuyAmount = resp.result.rebuyAmount;
                dateTime.transform.parent.gameObject.SetActive(false);
                if (resp.result.allowRebuy)
                {
                    RegisterButton.SetActive(false);
                    UnRegisterButton.SetActive(false);
                    ReBuyButton.Open();
                    totalSeconds = resp.result.remainRebuySec;
                }
                else
                {
                    RegisterButton.SetActive(!resp.result.isRegistered);
                    UnRegisterButton.SetActive(resp.result.isRegistered);
                    ReBuyButton.Close();
                }
                if (resp.result.registrationStatus.Equals("open") || resp.result.registrationStatus.Equals("Open"))
                {
                    RegisterButton1.interactable = true;
                }
                else
                {
                    RegisterButton1.interactable = false;
                }
                Invoke("SngTournamentEventCall", 5);
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
        }
    }

    void ResetSelectedgameButtons(int GameSelect)
    {
        foreach (GameObject Obj in SelectedGame)
        {
            Obj.SetActive(false);
        }
        foreach (GameObject Obj in SelectedGameTableList)
        {
            Obj.SetActive(false);
        }
        foreach (GameObject Obj in SelectedGameHeader)
        {
            Obj.SetActive(false);
        }
        SelectedGameHeader[GameSelect].SetActive(true);
        SelectedGame[GameSelect].SetActive(true);
        SelectedGameTableList[GameSelect].SetActive(true);
    }
    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion



}
