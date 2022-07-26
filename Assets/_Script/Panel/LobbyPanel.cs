using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BestHTTP.SocketIO;
using System.Linq;
using System;

public class LobbyPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    public Image mainBG;
    public Image currentBG;

    [Header("Gamobjects")]
    public GameObject homePangelObj;
    public GameObject[] SelectedGame;
    public GameObject[] SelectedGameTableList;
    public GameObject[] SelectedGameTableBannerDisplay;
    public GameObject[] SelectedGameTableBannerDisplayRight;
    public GameObject LobbyTableList;
    public GameObject HomeButton;
    public GameObject timerSide;
    public GameObject TournamentListPopup;
    public GameObject Gameoptions;


    [Header("Transforms")]
    public Transform[] SelectedGameParent;
    public Transform RegistredTableDataParent;

    [Header("ScriptableObjects")]
    public PanelProfile ProfileScreen;
    public DetailsTournament TournamentDetailsScreen;

    [Header("DropDowns")]
    public Dropdown SetGameSpeed;
    public Dropdown Limit;
    public Dropdown stakes;
    public Dropdown PlayerPerTable;
    public Dropdown dropDownPokerGameType;

    [Header("Panels")]
    public PrivateTablePasswordPopup privateTablePasswordPopup;

    [Header("Images")]
    public Image profilePicLeft;
    public Image MiniTable;
    public Sprite[] SoundOnoff;
    public Image SoundOnoffMain;
    public Image SoundOnoffMain2;
    public ScrollRect HomepageScrollRect;

    [Header("Text")]
    public TextMeshProUGUI txtTimer;
    public TextMeshProUGUI txtDate;
    public TextMeshProUGUI txtDisplayTournamentDate1;
    public TextMeshProUGUI txtDisplayTournamentDate2;
    public TextMeshProUGUI txtDisplayTournamentAmount1;
    public TextMeshProUGUI txtDisplayTournamentAmount2;
    public TextMeshProUGUI txtDisplayTournamentname1;
    public TextMeshProUGUI txtDisplayTournamentname2;
    public Text txtuesrname;
    public TextMeshProUGUI txtChips;
    public TextMeshProUGUI txtGameName;
    public TextMeshProUGUI txtTournamentRequestmessage;
    public TextMeshProUGUI txtTournamentRemainingTime1;
    public TextMeshProUGUI txtTournamentRemainingTime2;
    [Header("Prefabs")]
    public TableData tables;
    public List<TableData> TableDataObjectList;
    public TournaryAllTableList TournamentData;
    public List<TournaryAllTableList> TableTournaryAllTableList;
    public TournaryRegTableList TournaryRegTableData;
    public List<TournaryRegTableList> TournaryRegTableDataList;

    public RegistredTableData RegistredTableDataObj;
    public List<RegistredTableData> RegistredTableDataList;

    public StacksUpdate stackData;

    [Header("Enums")]
    public LimitType limitType = LimitType.All;
    public PokerGameType pokerGameType = PokerGameType.all;
    public PokerGameType tournamentGameType = PokerGameType.all;

    [Header("Variables")]
    public string gametype;
    public string SelectedPlayerPerTable;
    public string SelectedStack;
    public string tournamentPokerType;
    public int SelectedGames = 0;

    public List<string> TwoToNineList = new List<string> { "2", "3", "4", "5", "6", "7", "8", "9" };
    public List<string> TwoToSixList = new List<string> { "2", "3", "4", "5", "6" };
    public double totalRemainTournamentTime1;
    public double totalRemainTournamentTime2;
    #endregion

    #region PRIVATE_VARIABLES
    public void bgMusicOnOffBool(int OnOff)
    {
        SoundOnoffMain.sprite = SoundOnoff[OnOff];
    }
    public void SoundOnOffBool(int OnOff)
    {
        SoundOnoffMain2.sprite = SoundOnoff[OnOff];
    }
    #endregion

    #region UNITY_CALLBACKS

    void OnEnable()
    {
        mainBG.sprite = currentBG.sprite;
        Gameoptions.SetActive(true);
        UIManager.Instance.TandCondPopup.Close();
        UIManager.Instance.SoundManager.PlayBgSound();
        GetStaticTournamentData();
        RegistredTableDataList.Clear();
        TournamentPopupCall(false);
        Reset(SelectedGames);
        ResetAllPanels();
        LobbyTableList.SetActive(true);
        HomeButton.SetActive(false);
        timerSide.SetActive(true);
        txtGameName.text = "";

        txtuesrname.text = UIManager.Instance.assetOfGame.SavedLoginData.Username;
        //   txtChips.text = "Loading...";
        //txtChips.text = UIManager.Instance.assetOfGame.SavedLoginData.chips.ConvertToCommaSeparatedValue();
        txtDate.text = System.DateTime.Today.ToString("dd/MM/yyyy");  //.ToLongTimeString ();
        txtTournamentRequestmessage.text = "";
        MiniTable.gameObject.SetActive(false);
        //StopCoroutine ("RefreshRunningTableOnInterval"); false call
        //StartCoroutine ("RefreshRunningTableOnInterval"); false call
        //RefreshRunningTable (true); false call
        UIManager.Instance.HideLoader();
        GetProfileEventCall();
        GetRunningGameList();
        GetStacksData();
        GameTypeDropDown(0);
        //UIManager.Instance.tableManager.MiniTablePosition(224);
        UIManager.Instance.tableManager.MiniTablePosition(15);
        UIManager.Instance.tableManager.HideAddTableButton();
        bgMusicOnOffBool(PlayerPrefs.GetInt("bgMusic"));
        SoundOnOffBool(PlayerPrefs.GetInt("Sound"));
    }

    void OnDisable()
    {
        //StopCoroutine ("RefreshRunningTableOnInterval"); false call
        ResetAllPanels();
        ClearObjects();
        //SelectedGames = 0;
    }

    void Update()
    {
        //txtTimer.text = System.DateTime.Now.ToLongTimeString();
        if (totalRemainTournamentTime1 > 0)
        {
            totalRemainTournamentTime1 -= 1 * Time.deltaTime;
            txtTournamentRemainingTime1.Open();

            if (TimeSpan.FromSeconds(totalRemainTournamentTime1).Days > 0)
            {
                txtTournamentRemainingTime1.text = "" + string.Format("{0:0} day(s)\n{1:00}H:{2:00}M:{3:00}S", TimeSpan.FromSeconds(totalRemainTournamentTime1).Days, TimeSpan.FromSeconds(totalRemainTournamentTime1).Hours, TimeSpan.FromSeconds(totalRemainTournamentTime1).Minutes, TimeSpan.FromSeconds(totalRemainTournamentTime1).Seconds);

            }
            else if (TimeSpan.FromSeconds(totalRemainTournamentTime1).TotalHours < 0)
            {
                txtTournamentRemainingTime1.text = "" + string.Format("{0:00}M:{1:00}S", TimeSpan.FromSeconds(totalRemainTournamentTime1).Minutes, TimeSpan.FromSeconds(totalRemainTournamentTime1).Seconds);

            }
            else
            {
                txtTournamentRemainingTime1.text = "" + string.Format("{0:00}H:{1:00}M:{2:00}S", TimeSpan.FromSeconds(totalRemainTournamentTime1).TotalHours, TimeSpan.FromSeconds(totalRemainTournamentTime1).Minutes, TimeSpan.FromSeconds(totalRemainTournamentTime1).Seconds);
            }


            //txtTournamentRemainingTime.text = "" + string.Format("{0:00}D\n {1:00}H:{2:00}M:{3:00}S", TimeSpan.FromSeconds(totalRemainTournamentTime).Days, TimeSpan.FromSeconds(totalRemainTournamentTime).Hours, TimeSpan.FromSeconds(totalRemainTournamentTime).Minutes, TimeSpan.FromSeconds(totalRemainTournamentTime).Seconds);
        }
        else
        {
            totalRemainTournamentTime1 = 0;
            txtTournamentRemainingTime1.text = "";
            txtTournamentRemainingTime1.Close();
        }

        if (totalRemainTournamentTime2 > 0)
        {
            totalRemainTournamentTime2 -= 1 * Time.deltaTime;
            txtTournamentRemainingTime2.Open();

            if (TimeSpan.FromSeconds(totalRemainTournamentTime2).Days > 0)
            {
                txtTournamentRemainingTime2.text = "" + string.Format("{0:0} day(s)\n{1:00}H:{2:00}M:{3:00}S", TimeSpan.FromSeconds(totalRemainTournamentTime2).Days, TimeSpan.FromSeconds(totalRemainTournamentTime2).Hours, TimeSpan.FromSeconds(totalRemainTournamentTime2).Minutes, TimeSpan.FromSeconds(totalRemainTournamentTime2).Seconds);

            }
            else if (TimeSpan.FromSeconds(totalRemainTournamentTime2).TotalHours < 0)
            {
                txtTournamentRemainingTime2.text = "" + string.Format("{0:00}M:{1:00}S", TimeSpan.FromSeconds(totalRemainTournamentTime2).Minutes, TimeSpan.FromSeconds(totalRemainTournamentTime2).Seconds);

            }
            else
            {
                txtTournamentRemainingTime2.text = "" + string.Format("{0:00}H:{1:00}M:{2:00}S", TimeSpan.FromSeconds(totalRemainTournamentTime2).TotalHours, TimeSpan.FromSeconds(totalRemainTournamentTime2).Minutes, TimeSpan.FromSeconds(totalRemainTournamentTime2).Seconds);
            }


            //txtTournamentRemainingTime.text = "" + string.Format("{0:00}D\n {1:00}H:{2:00}M:{3:00}S", TimeSpan.FromSeconds(totalRemainTournamentTime).Days, TimeSpan.FromSeconds(totalRemainTournamentTime).Hours, TimeSpan.FromSeconds(totalRemainTournamentTime).Minutes, TimeSpan.FromSeconds(totalRemainTournamentTime).Seconds);
        }
        else
        {
            totalRemainTournamentTime2 = 0;
            txtTournamentRemainingTime2.text = "";
            txtTournamentRemainingTime2.Close();
        }
    }
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void bgmusicSFXButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (PlayerPrefs.GetInt("bgMusic") == 1)
        {
            PlayerPrefs.SetInt("bgMusic", 0);
            PlayerPrefs.SetInt("Sound", 0);
            UIManager.Instance.SoundManager.stopBgSound();
        }
        else
        {
            PlayerPrefs.SetInt("bgMusic", 1);
            PlayerPrefs.SetInt("Sound", 1);
            UIManager.Instance.SoundManager.PlayBgSound();
        }
        bgMusicOnOffBool(PlayerPrefs.GetInt("bgMusic"));
        SoundOnOffBool(PlayerPrefs.GetInt("Sound"));
        UIManager.Instance.LobbyScreeen.ProfileScreen.PanelSettings.bgMusicOnOffBool(PlayerPrefs.GetInt("bgMusic"));
        UIManager.Instance.LobbyScreeen.ProfileScreen.PanelSettings.SoundOnOffBool(PlayerPrefs.GetInt("Sound"));
    }
    /*
	 * Summarry 
	 * this Below methods are to manage Lobby Table listing with Different games and all DropDown Functions
	 * Gameobject name = LobbyTable_Listing.
	 * */
    public void OnHomeGameButtonTap()
    {
        //homePangelObj.SetActive(true);
        UIManager.Instance.SoundManager.OnButtonClick();
        Gameoptions.SetActive(true);
        SelectedGames = 0;
        GetStaticTournamentData();
        ResetSelectedgameButtons(SelectedGames);
        PlayerPerTableValue(1);
        ProfileScreen.Close();
        StartCoroutine(ForceScrollDownHomePage());
    }
    public void OnOmahaGameButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        SelectedGames = 1;
        ResetSelectedgameButtons(SelectedGames);
        PlayerPerTableValue(1);
    }

    public void OnTexassButtonTap()
    {

        UIManager.Instance.SoundManager.OnButtonClick();
        SelectedGames = 2;
        ResetSelectedgameButtons(SelectedGames);
        PlayerPerTableValue(1);
    }

    public void OnTournamentButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        SelectedGames = 3;
        tournamentPokerType = "all";
        GameTypeDropDown(0);
        ResetSelectedgameButtons(SelectedGames);
        PlayerPerTableValue(1);
    }

    public void OnSitNGoButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        SelectedGames = 4;
        tournamentPokerType = "all";
        GameTypeDropDown(0);
        ResetSelectedgameButtons(SelectedGames);
        PlayerPerTableValue(1);
    }
    public void OnploFiveButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        SelectedGames = 5;
        ResetSelectedgameButtons(SelectedGames);
        PlayerPerTableValue(0);

    }
    public void StaticTournamentButtonTap(int Id)
    {

        UIManager.Instance.SoundManager.OnButtonClick();
        if (StaticTournamentDisplay[Id].tournamentId.Equals(""))
        {
            OnTournamentButtonTap();
        }
        else
        {
            UIManager.Instance.gameType = GameType.Touranment;
            TournamentDetailsScreen.GetDetailsTournamentButtonTap(StaticTournamentDisplay[Id].tournamentId, StaticTournamentDisplay[Id].pokerGameType);
        }
    }

    public void onStaticBannersButtonTap(string s)
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        print("GetProfileEventCall => " + s);
        UIManager.Instance.SocketGameManager.StaticBanners(s, (socket, packet, args) =>
         {
             print("StaticBanners: " + packet.ToString());
             stackData = new StacksUpdate();
             JSONArray arr = new JSONArray(packet.ToString());
             var resp = arr.getString(arr.length() - 1);
             PokerEventResponse<RoomsListing.Room> StaticBannersResponse = JsonUtility.FromJson<PokerEventResponse<RoomsListing.Room>>(resp);

             if (StaticBannersResponse.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
             {
                 if (StaticBannersResponse.result.isTournament)
                 {
                     if (StaticBannersResponse.result.tournamentId.Equals(""))
                     {
                         if (StaticBannersResponse.result.namespaceString.Contains("sng") || StaticBannersResponse.result.namespaceString.Contains("SNG"))
                         {
                             OnSitNGoButtonTap();
                         }
                         else
                         {
                             OnTournamentButtonTap();
                         }
                     }
                     else
                     {
                         //UIManager.Instance.gameType = GameType.Touranment;
                         if (StaticBannersResponse.result.namespaceString.Contains("sng") || StaticBannersResponse.result.namespaceString.Contains("SNG"))
                         {
                             UIManager.Instance.gameType = GameType.sng;
                             TournamentDetailsScreen.GetDetailsTournamentButtonTap(StaticBannersResponse.result.id, StaticBannersResponse.result.pokerGameType);

                         }
                         else
                         {
                             UIManager.Instance.gameType = GameType.Touranment;
                             TournamentDetailsScreen.GetDetailsTournamentButtonTap(StaticBannersResponse.result.tournamentId, StaticBannersResponse.result.pokerGameType);
                         }

                     }
                 }
                 else
                 {
                     UIManager.Instance.GameScreeen.SetRoomDataAndPlay(StaticBannersResponse.result);
                     this.Close();
                     UIManager.Instance.GameScreeen.Open();
                 }

             }
             else
             {
                 //UIManager.Instance.DisplayMessagePanel(StaticTournamentResponse.message);
             }
         });
    }


    public void LogOutButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.IsWebGLAffiliat)
#if UNITY_EDITOR
            UIManager.Instance.DisplayConfirmationPanel("Are you sure you want to exit? ", OnLogOutDone);
#else
            OnLogOutDone();
#endif
        else
            UIManager.Instance.DisplayConfirmationPanel("Are you sure you want to Logout? ", OnLogOutDone);
    }

    public void OnLogOutDone()
    {
        UIManager.Instance.SocketGameManager.LogOutPlayer((socket, packet, args) =>
        {
            Debug.Log("LogOutPlayer  : " + packet.ToString());
            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                if (UIManager.Instance.IsWebGLAffiliat)
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    ExternalCallClass.Instance.ExitGame();
                }
                else
                {
                    StopCoroutine("LogoutFunction");
                    StartCoroutine(LogoutFunction(0f));
                }
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }

    public void CallTableListAPI()
    {
        string SelectedLimitType = limitType.ToString();
        string SelectedGameSpeed = UIManager.Instance.selectedGameSpeed.ToString();
        if (SelectedPlayerPerTable == "" || SelectedPlayerPerTable.Equals("All"))
        {
            SelectedPlayerPerTable = "all";
        }
        if (SelectedStack == "" || SelectedStack.Equals("All") || SelectedStack.Equals("ALL"))
        {
            SelectedStack = "all";
        }
        if (SelectedLimitType == "" || SelectedLimitType.Equals("All") || SelectedLimitType.Equals("ALL"))
        {
            SelectedLimitType = "all";
        }
        //		Debug.Log ("SelectedStack => " + SelectedStack);
        //		Debug.Log ("SelectedLimitType => " + SelectedLimitType);
        bool isLimitSelected = false;
        if (SelectedGames.Equals(1))
        {
            dropDownPokerGameType.transform.parent.gameObject.SetActive(false);
            SetGameSpeed.transform.parent.gameObject.SetActive(false);
            Limit.transform.parent.gameObject.SetActive(true);
            PlayerPerTable.transform.parent.gameObject.SetActive(true);
            //	UIManager.Instance.gameType = GameType.omaha;
            //    gametype = "omaha";
            pokerGameType = PokerGameType.omaha;
            UIManager.Instance.selectedGameType = GameType.cash;
            string currencyType = UIManager.Instance.currencyType.ToString();
            UIManager.Instance.SocketGameManager.SearchLobby(pokerGameType.ToString(), UIManager.Instance.selectedGameSpeed, "", isLimitSelected, gametype, SelectedLimitType, SelectedStack, SelectedPlayerPerTable, currencyType, OnTableListReceived);
        }
        if (SelectedGames.Equals(2))
        {
            dropDownPokerGameType.transform.parent.gameObject.SetActive(false);
            SetGameSpeed.transform.parent.gameObject.SetActive(false);
            Limit.transform.parent.gameObject.SetActive(true);
            PlayerPerTable.transform.parent.gameObject.SetActive(true);
            // gametype = "texas";
            pokerGameType = PokerGameType.texas;
            UIManager.Instance.gameType = GameType.texas;
            //UIManager.Instance.gameType = GameType.Touranment;
            UIManager.Instance.selectedGameType = GameType.cash;
            string currencyType = UIManager.Instance.currencyType.ToString();

            UIManager.Instance.SocketGameManager.SearchLobby(pokerGameType.ToString(), UIManager.Instance.selectedGameSpeed, "", isLimitSelected, gametype, SelectedLimitType, SelectedStack, SelectedPlayerPerTable, currencyType, OnTableListReceived);

            //     UIManager.Instance.SocketGameManager.SearchTournamentLobby (pokerGameType.ToString(), SelectedGameSpeed, "", isLimitSelected, gametype, SelectedLimitType, SelectedStack, SelectedPlayerPerTable, OnRegularTableListReceived);
        }
        if (SelectedGames.Equals(3))
        {
            dropDownPokerGameType.transform.parent.gameObject.SetActive(true);

            SetGameSpeed.transform.parent.gameObject.SetActive(false);
            Limit.transform.parent.gameObject.SetActive(true);
            PlayerPerTable.transform.parent.gameObject.SetActive(true);
            UIManager.Instance.gameType = GameType.Touranment;
            gametype = "Touranment";

            //UIManager.Instance.gameType = GameType.sng;
            //UIManager.Instance.selectedGameType = GameType.sng;
            UIManager.Instance.selectedGameType = GameType.Touranment;
            //Debug.Log("Touranment game type : " + tournamentGameType.ToString());
            Debug.Log("Touranment game type : " + tournamentPokerType);
            if (tournamentPokerType.Equals(""))
            {
                //GameTypeDropDown(0);
                tournamentPokerType = "all";
            }

            UIManager.Instance.SocketGameManager.SearchTournamentLobby(tournamentPokerType, SelectedGameSpeed, "", isLimitSelected, gametype, SelectedLimitType, SelectedStack, SelectedPlayerPerTable, OnRegularTableListReceived);

            //   UIManager.Instance.SocketGameManager.SearchSngLobby (pokerGameType.ToString(), UIManager.Instance.selectedGameSpeed, "", isLimitSelected, gametype, SelectedLimitType, SelectedStack, SelectedPlayerPerTable, OnSNGTableListReceived);
        }
        if (SelectedGames.Equals(4))
        {
            dropDownPokerGameType.transform.parent.gameObject.SetActive(true);

            SetGameSpeed.transform.parent.gameObject.SetActive(false);
            Limit.transform.parent.gameObject.SetActive(true);
            PlayerPerTable.transform.parent.gameObject.SetActive(true);
            UIManager.Instance.gameType = GameType.sng;
            UIManager.Instance.selectedGameType = GameType.sng;
            Debug.Log("Touranment game type : " + tournamentPokerType);
            gametype = "sng";

            if (tournamentPokerType.Equals(""))
            {
                //GameTypeDropDown(1);
                tournamentPokerType = "all";
            }
            UIManager.Instance.SocketGameManager.SearchSngLobby(tournamentPokerType, UIManager.Instance.selectedGameSpeed, "", isLimitSelected, gametype, SelectedLimitType, SelectedStack, SelectedPlayerPerTable, OnSNGTableListReceived);
            /*
                        dropDownPokerGameType.transform.parent.gameObject.SetActive(true);

                        SetGameSpeed.transform.parent.gameObject.SetActive(false);
                        Limit.transform.parent.gameObject.SetActive(true);
                        PlayerPerTable.transform.parent.gameObject.SetActive(true);
                        UIManager.Instance.gameType = GameType.sng;
                        gametype = "sng";

                        //UIManager.Instance.gameType = GameType.sng;
                        //UIManager.Instance.selectedGameType = GameType.sng;
                        UIManager.Instance.selectedGameType = GameType.sng;
                        Debug.Log("Touranment game type : " + tournamentGameType.ToString());
                       UIManager.Instance.SocketGameManager.SearchSngLobby (pokerGameType.ToString(), UIManager.Instance.selectedGameSpeed, "", isLimitSelected, gametype, SelectedLimitType, SelectedStack, SelectedPlayerPerTable, OnSNGTableListReceived);
                    */
        }
        //GetProfileEventCall (); false call

        if (SelectedGames.Equals(5))
        {
            dropDownPokerGameType.transform.parent.gameObject.SetActive(false);
            SetGameSpeed.transform.parent.gameObject.SetActive(false);
            Limit.transform.parent.gameObject.SetActive(true);
            PlayerPerTable.transform.parent.gameObject.SetActive(true);
            // gametype = "texas";
            pokerGameType = PokerGameType.PLO5;
            UIManager.Instance.gameType = GameType.PLO5;
            //UIManager.Instance.gameType = GameType.Touranment;
            UIManager.Instance.selectedGameType = GameType.PLO5;
            string currencyType = UIManager.Instance.currencyType.ToString();

            UIManager.Instance.SocketGameManager.SearchLobby(pokerGameType.ToString(), UIManager.Instance.selectedGameSpeed, "", isLimitSelected, gametype, SelectedLimitType, SelectedStack, SelectedPlayerPerTable, currencyType, OnTableListReceived);

            //     UIManager.Instance.SocketGameManager.SearchTournamentLobby (pokerGameType.ToString(), SelectedGameSpeed, "", isLimitSelected, gametype, SelectedLimitType, SelectedStack, SelectedPlayerPerTable, OnRegularTableListReceived);
        }
    }

    public void RefreshTable(bool displayLoader = false)
    {
        if (displayLoader)
        {
            DestroyAllTournamentTables();
            //loadingPanel.Open ();
        }
        CallTableListAPI();
    }

    public void GameSpeedValueChanged()
    {
        if (SetGameSpeed.value == 0)
            UIManager.Instance.selectedGameSpeed = GameSpeed.regular;
        else if (SetGameSpeed.value == 1)
            UIManager.Instance.selectedGameSpeed = GameSpeed.turbo;
        else if (SetGameSpeed.value == 2)
            UIManager.Instance.selectedGameSpeed = GameSpeed.hyper_turbo;
        CallTableListAPI();
    }

    public void LimitValueChanged()
    {
        Debug.Log("Limit Value Updated =>" + Limit.options[Limit.value].text);

        if (Limit.value == 0)
            limitType = LimitType.All;
        else if (Limit.value == 1)
            limitType = LimitType.Limit;
        else if (Limit.value == 2)
            limitType = LimitType.No_Limit;
        else if (Limit.value == 3)
            limitType = LimitType.Pot_Limit;

        CallTableListAPI();
    }

    public void StakeValueChanged()
    {
        Debug.Log("Stake Value Updated =>" + stakes.options[stakes.value].text);
        string SelectedStackvalue = stakes.options[stakes.value].text.ToString().Trim();
        SelectedStack = SelectedStackvalue;
        CallTableListAPI();
    }

    public void playerPerTableValueChanged()
    {
        string SelectedPlayerPerTablevalue = PlayerPerTable.options[PlayerPerTable.value].text.ToString().Trim();
        SelectedPlayerPerTable = SelectedPlayerPerTablevalue;
        CallTableListAPI();
    }

    public void PokerGameTypeValueChanged()
    {
        if (dropDownPokerGameType.value == 0)
            pokerGameType = PokerGameType.all;
        else if (dropDownPokerGameType.value == 1)
            pokerGameType = PokerGameType.texas;
        else if (dropDownPokerGameType.value == 2)
            pokerGameType = PokerGameType.omaha;

        CallTableListAPI();
    }

    public void TournamentGameTypeValueChanged()
    {
        /*  if (dropDownPokerGameType.value == 0)
			  tournamentGameType = PokerGameType.all;
		  else if (dropDownPokerGameType.value == 1)
			  tournamentGameType = PokerGameType.texas;
		  else if (dropDownPokerGameType.value == 2)
			  tournamentGameType = PokerGameType.omaha;
		  else if (dropDownPokerGameType.value == 3)
			  tournamentGameType = PokerGameType.PLO5*/
        Debug.Log("dropDownPokerGameType Updated =>" + dropDownPokerGameType.options[dropDownPokerGameType.value].text);
        string SelectedStackvalue = dropDownPokerGameType.options[dropDownPokerGameType.value].text.ToString().Trim();
        tournamentPokerType = SelectedStackvalue;

        CallTableListAPI();
    }

    /*
	 * Summarry 
	 * this Below methods are to manage Lobby Myaccount data in Perticular Myaccount panel.
	 * Gameobject name = PanelMyAccount.
	 * */
    public void ProfileScreenButtonTap()
    {
        print("call");
        ResetAllPanels();
        HomeButtonTap();

        ProfileScreen.Open();
        Gameoptions.SetActive(false);
        UIManager.Instance.SoundManager.OnButtonClick();
    }

    public void MyaccountPanelButtonTap()
    {
        ResetAllPanels();
        //PanelMyAccount.Open();
        txtGameName.text = "My Account";
        UIManager.Instance.SoundManager.OnButtonClick();
    }

    public void PanelNewsButtonTap()
    {
        ResetAllPanels();
        //PanelNews.Open();
        txtGameName.text = "News";
        UIManager.Instance.SoundManager.OnButtonClick();
    }

    public void PanelSettingsButtonTap()
    {
        ResetAllPanels();
        //PanelSettings.Open();
        txtGameName.text = "My Settings";
        UIManager.Instance.SoundManager.OnButtonClick();
    }

    public void PanelMyTournaryButtonTap()
    {
        ResetAllPanels();
        //PanelMyTournary.Open();
        txtGameName.text = "My Tourneys";
        UIManager.Instance.SoundManager.OnButtonClick();
    }

    public void HomeButtonTap()
    {
        Gameoptions.SetActive(true);
        txtGameName.text = "";
        ResetAllPanels();
        ProfileScreen.Close();
        LobbyTableList.SetActive(true);
        timerSide.SetActive(true);
        HomeButton.SetActive(false);
        StopCoroutine("RefreshTableOnInterval");
        StartCoroutine("RefreshTableOnInterval");
        RefreshTable();
        OnHomeGameButtonTap();
        UIManager.Instance.SoundManager.OnButtonClick();
    }

    public void BackToLobbyButtonTap()
    {
        TournamentDetailsScreen.Close();
        LobbyTableList.SetActive(true);
        UIManager.Instance.SoundManager.OnButtonClick();
        StopCoroutine("RefreshTableOnInterval");
        StartCoroutine("RefreshTableOnInterval");
        //StartCoroutine ("RefreshRunningTableOnInterval"); false call
        //RefreshRunningTable (); false call
        RefreshTable();
    }

    public void RefreshRunningTable(bool displayLoader = false)
    {
        if (displayLoader)
        {
            DestroyAllRunningTournamentTables();
        }
        GetCheckRunningGameCall();
    }

    public void TournamentPopupCall(bool isOpen)
    {
        if (isOpen)
        {
            if (txtTournamentRequestmessage.text != "")
            {

                TournamentListPopup.SetActive(isOpen);
                StopCoroutine("RefreshTableOnInterval");
            }
        }
        else
        {
            TournamentListPopup.SetActive(isOpen);
            StopCoroutine("RefreshTableOnInterval");
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine("RefreshTableOnInterval");
            }
        }
    }

    public void GetRunningGameList()
    {
        if (!UIManager.Instance.IsMultipleTableAllowed || UIManager.Instance.MainHomeScreen.isActiveAndEnabled)
            return;

        UIManager.Instance.SocketGameManager.GetRunningGameList((socket, packet, args) =>
        {
            print("GetRunningGameList response: " + packet.ToString());
            RoomsListing roomsResp = JsonUtility.FromJson<RoomsListing>(Utility.Instance.GetPacketString(packet));

            if (roomsResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {

                if (UIManager.Instance.MainHomeScreen.isActiveAndEnabled)
                    return;

                foreach (RoomsListing.Room room in roomsResp.result)
                {
                    if (!UIManager.Instance.tableManager.playingTableList.Contains(room.roomId))
                    {
                        UIManager.Instance.tableManager.AddMiniTable(room);
                    }
                }
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(roomsResp.message, null);
            }
        });
    }
    #endregion

    #region PRIVATE_METHODS
    void PlayerPerTableValue(int flag)
    {
        PlayerPerTable.options.Clear();
        PlayerPerTable.options.Add(new Dropdown.OptionData("All"));
        if (flag == 0)
        {
            foreach (string i in TwoToSixList)
            {
                PlayerPerTable.options.Add(new Dropdown.OptionData(i));
            }
        }
        else
        {
            foreach (string i in TwoToNineList)
            {
                PlayerPerTable.options.Add(new Dropdown.OptionData(i));
            }
        }
    }
    void ResetAllPanels()
    {
        LobbyTableList.SetActive(false);
        timerSide.SetActive(false);
        HomeButton.SetActive(true);
        ProfileScreen.Close();
        TournamentDetailsScreen.Close();
        privateTablePasswordPopup.Close();
        StopCoroutine("RefreshTableOnInterval");
        //StopCoroutine ("RefreshRunningTableOnInterval"); false call
    }

    void Reset(int SelectedGamesss = 0)
    {
        foreach (Transform Obj in SelectedGameParent)
        {
            foreach (Transform go in Obj)
            {
                Destroy(go.gameObject);
            }

            DestroyAllTournamentTables();
            StopCoroutine("RefreshTableOnInterval");
            StartCoroutine("RefreshTableOnInterval");
        }
        //StopCoroutine ("RefreshRunningTableOnInterval"); false call
        //StartCoroutine ("RefreshRunningTableOnInterval"); false call

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
        foreach (GameObject Obj in SelectedGameTableBannerDisplay)
        {
            Obj.SetActive(false);
        }
        foreach (GameObject Obj in SelectedGameTableBannerDisplayRight)
        {
            Obj.SetActive(false);
        }
        Reset(GameSelect);
        homePangelObj.SetActive(false);
        SelectedGame[GameSelect].SetActive(true);
        SelectedGameTableList[GameSelect].SetActive(true);

        if (!GameSelect.Equals(0))
        {
            SelectedGameTableBannerDisplay[GameSelect - 1].SetActive(true);
            SelectedGameTableBannerDisplayRight[GameSelect - 1].SetActive(true);
        }


        CallTableListAPI();
    }


    //Hold'em TEXAS Tabel List Received
    private void OnTableListReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        //		UIManager.Instance.DisplayLoader (Constants.Messages.PleaseWait);
        Debug.Log("OnTableListReceived : " + packet.ToString());

        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        RoomsListing roomsResp = JsonUtility.FromJson<RoomsListing>(resp);

        if (!roomsResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(roomsResp.message, null);
            return;
        }

        if (roomsResp.result != null)
        {
            //the following if else condition will delete all object & regenerate all tables when table added or removed from admin side.
            if (TableDataObjectList.Count > 0 && TableDataObjectList.Count != roomsResp.result.Count)
            {
                DestroyAllTournamentTables();
            }
            else
            {
                for (int i = 0; i < roomsResp.result.Count; i++)
                {
                    TableData obj = GetTournamentObjIfAlreadyCreated(roomsResp.result[i].roomId);
                    if (obj == null)
                    {
                        DestroyAllTournamentTables();
                        break;
                    }
                }
            }
            for (int i = 0; i < roomsResp.result.Count; i++)
            {
                TableData obj = GetTournamentObjIfAlreadyCreated(roomsResp.result[i].roomId);
                if (obj != null)
                {
                    obj.SetData(roomsResp.result[i], i);
                }
                else
                {
                    TableData tableDestails = Instantiate(tables) as TableData;
                    tableDestails.SetData(roomsResp.result[i], i);
                    tableDestails.transform.SetParent(SelectedGameParent[SelectedGames], false);
                    TableDataObjectList.Add(tableDestails);
                }
            }
        }
        //		UIManager.Instance.HideLoader ();
        RemoveOtherTournaments(roomsResp.result);
    }

    private void OnSNGTableListReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        //UIManager.Instance.DisplayLoader (Constants.Messages.PleaseWait);
        Debug.Log("OnSNGTableListReceived : " + packet.ToString());

        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;


        TournamentRoomObject roomsResp = JsonUtility.FromJson<TournamentRoomObject>(resp);
        //if (roomsResp.Equals(null))
        //{
        if (!roomsResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(roomsResp.message, null);
            return;
        }

        if (roomsResp.result != null)
        {
            //the following if else condition will delete all object & regenerate all tables when table added or removed from admin side.
            if (TableTournaryAllTableList.Count > 0 && TableTournaryAllTableList.Count != roomsResp.result.Count)
            {
                DestroyAllTournamentTables();
            }
            else
            {
                for (int i = 0; i < roomsResp.result.Count; i++)
                {
                    TournaryAllTableList obj = GetSNGTournamentObjIfAlreadyCreated(roomsResp.result[i].id);
                    if (obj == null)
                    {
                        DestroyAllTournamentTables();
                        break;
                    }
                }
            }

            for (int i = 0; i < roomsResp.result.Count; i++)
            {
                TournaryAllTableList obj = GetSNGTournamentObjIfAlreadyCreated(roomsResp.result[i].id);
                if (obj != null)
                {
                    obj.SetSngData(roomsResp.result[i], i);
                }
                else
                {
                    TournaryAllTableList tableDestails = Instantiate(TournamentData) as TournaryAllTableList;
                    tableDestails.SetSngData(roomsResp.result[i], i);
                    tableDestails.transform.SetParent(SelectedGameParent[SelectedGames], false);
                    TableTournaryAllTableList.Add(tableDestails);
                }
            }
        }
        //		UIManager.Instance.HideLoader ();
        RemoveSngOtherTournaments(roomsResp.result);
        //}
    }

    private void OnRegularTableListReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        //UIManager.Instance.DisplayLoader (Constants.Messages.PleaseWait);
        Debug.Log("OnRegularTableListReceived : " + packet.ToString());

        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;


        NormalTournamentDetails roomsResp = JsonUtility.FromJson<NormalTournamentDetails>(resp);

        if (!roomsResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(roomsResp.message, null);
            return;
        }

        if (roomsResp.result != null)
        {
            //the following if else condition will delete all object & regenerate all tables when table added or removed from admin side.
            if (TournaryRegTableDataList.Count > 0 && TournaryRegTableDataList.Count != roomsResp.result.Count)
            {
                DestroyAllTournamentTables();
            }
            else
            {
                for (int i = 0; i < roomsResp.result.Count; i++)
                {
                    TournaryRegTableList obj = GetRegTournamentObjIfAlreadyCreated(roomsResp.result[i].tournamentId);
                    if (obj == null)
                    {
                        DestroyAllTournamentTables();
                        break;
                    }
                }
            }

            for (int i = 0; i < roomsResp.result.Count; i++)
            {
                TournaryRegTableList obj = GetRegTournamentObjIfAlreadyCreated(roomsResp.result[i].tournamentId);
                if (obj != null)
                {
                    obj.SetSngData(roomsResp.result[i], i);
                }
                else
                {
                    TournaryRegTableList tableDestails = Instantiate(TournaryRegTableData) as TournaryRegTableList;
                    tableDestails.SetSngData(roomsResp.result[i], i);
                    tableDestails.transform.SetParent(SelectedGameParent[SelectedGames], false);
                    TournaryRegTableDataList.Add(tableDestails);
                }
            }
        }
        //		UIManager.Instance.HideLoader ();
        RemoveRegularOtherTournaments(roomsResp.result);
    }

    private void GetProfileEventCall()
    {
        UIManager.Instance.SocketGameManager.GetProfile((socket, packet, args) =>
        {

            Debug.Log("GetProfile  : " + packet.ToString());

            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse<Profile> resp = JsonUtility.FromJson<PokerEventResponse<Profile>>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                UIManager.Instance.assetOfGame.SavedLoginData.chips = resp.result.chips;
                txtChips.text = UIManager.Instance.assetOfGame.SavedLoginData.chips.ConvertToCommaSeparatedValueColor();
            }
            else
            {
                //UIManager.Instance.DisplayMessagePanel (resp.message);
            }
        });
    }

    private void DestroyAllTournamentTables()
    {
        if (TableDataObjectList == null)
            if (TableTournaryAllTableList == null)
                if (TournaryRegTableDataList == null)
                    return;

        if (TableDataObjectList != null)
        {
            foreach (TableData go in TableDataObjectList)
            {
                Destroy(go.gameObject);
            }
            TableDataObjectList = new List<TableData>();
        }
        if (TableTournaryAllTableList != null)
        {
            foreach (TournaryAllTableList go in TableTournaryAllTableList)
            {
                Destroy(go.gameObject);
            }
            TableTournaryAllTableList = new List<TournaryAllTableList>();
        }

        if (TournaryRegTableDataList != null)
        {
            foreach (TournaryRegTableList go in TournaryRegTableDataList)
            {
                Destroy(go.gameObject);
            }
            TournaryRegTableDataList = new List<TournaryRegTableList>();
        }
    }

    private void RemoveOtherTournaments(List<RoomsListing.Room> roomsList)
    {
        if (TableDataObjectList != null)
        {
            if (roomsList == null || roomsList.Count == 0)
            {
                foreach (TableData tro in TableDataObjectList.ToArray())
                {
                    TableDataObjectList.Remove(tro);
                    Destroy(tro.gameObject);
                }
            }
            else
            {
                List<string> roomIdsList = roomsList.Select(o => o.roomId).ToList();

                foreach (TableData tro in TableDataObjectList.ToArray())
                {
                    for (int i = 0; i < roomsList.Count; i++)
                    {
                        if (roomsList == null || !roomIdsList.Contains(tro.data.roomId))
                        {
                            TableDataObjectList.Remove(tro);
                            Destroy(tro.gameObject);
                        }
                    }
                }
            }
        }
    }

    private TableData GetTournamentObjIfAlreadyCreated(string tableID)
    {
        if (TableDataObjectList != null)
        {
            for (int i = 0; i < TableDataObjectList.Count; i++)
            {
                if (tableID.Equals(TableDataObjectList[i].data.roomId))
                {
                    return TableDataObjectList[i];
                }
            }
        }
        return null;
    }



    private void RemoveSngOtherTournaments(List<TournamentRoomObject.TournamentRoom> roomsList)
    {
        if (TableTournaryAllTableList != null)
        {
            if (roomsList == null || roomsList.Count == 0)
            {
                foreach (TournaryAllTableList tro in TableTournaryAllTableList.ToArray())
                {
                    TableTournaryAllTableList.Remove(tro);
                    Destroy(tro.gameObject);
                }
            }
            else
            {
                List<string> roomIdsList = roomsList.Select(o => o.id).ToList();

                foreach (TournaryAllTableList tro in TableTournaryAllTableList.ToArray())
                {
                    for (int i = 0; i < roomsList.Count; i++)
                    {
                        if (roomsList == null || !roomIdsList.Contains(tro.TournamentTableId))
                        {
                            TableTournaryAllTableList.Remove(tro);
                            Destroy(tro.gameObject);
                        }
                    }
                }
            }
        }
    }

    private TournaryAllTableList GetSNGTournamentObjIfAlreadyCreated(string tableID)
    {
        if (TableTournaryAllTableList != null)
        {
            for (int i = 0; i < TableTournaryAllTableList.Count; i++)
            {
                if (tableID.Equals(TableTournaryAllTableList[i].TournamentTableId))
                {
                    return TableTournaryAllTableList[i];
                }
            }
        }
        return null;
    }

    private void RemoveRegularOtherTournaments(List<NormalTournamentDetails.NormalTournamentData> roomsList)
    {
        if (TournaryRegTableDataList != null)
        {
            if (roomsList == null || roomsList.Count == 0)
            {
                foreach (TournaryRegTableList tro in TournaryRegTableDataList.ToArray())
                {
                    TournaryRegTableDataList.Remove(tro);
                    Destroy(tro.gameObject);
                }
            }
            else
            {
                List<string> roomIdsList = roomsList.Select(o => o.tournamentId).ToList();

                foreach (TournaryRegTableList tro in TournaryRegTableDataList.ToArray())
                {
                    for (int i = 0; i < roomsList.Count; i++)
                    {
                        if (roomsList == null || !roomIdsList.Contains(tro.TournamentTableId))
                        {
                            TournaryRegTableDataList.Remove(tro);
                            Destroy(tro.gameObject);
                        }
                    }
                }
            }
        }
    }

    private TournaryRegTableList GetRegTournamentObjIfAlreadyCreated(string tableID)
    {
        if (TournaryRegTableDataList != null)
        {
            for (int i = 0; i < TournaryRegTableDataList.Count; i++)
            {
                if (tableID.Equals(TournaryRegTableDataList[i].TournamentTableId))
                {
                    return TournaryRegTableDataList[i];
                }
            }
        }
        return null;
    }
    private void GameTypeDropDown(int Id)
    {
        dropDownPokerGameType.ClearOptions();

        List<string> m_DropOptionsStakes = new List<string>();

        m_DropOptionsStakes.Add("All");
        m_DropOptionsStakes.Add("texas");
        m_DropOptionsStakes.Add("omaha");

        if (Id.Equals(0))
        {
            m_DropOptionsStakes.Add("PLO5");
        }
        dropDownPokerGameType.AddOptions(m_DropOptionsStakes);
    }
    StacksUpdateResult StacksUpdateResultdata;
    private void GetStacksData()
    {
        UIManager.Instance.SocketGameManager.GetStacks((socket, packet, args) =>
        {
            print("GetStacks: " + packet.ToString());
            stackData = new StacksUpdate();
            JSONArray arr = new JSONArray(packet.ToString());
            var resp = arr.getString(arr.length() - 1);
            StacksUpdate lobbyFilterResponse = JsonUtility.FromJson<StacksUpdate>(resp);

            if (lobbyFilterResponse.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                stakes.ClearOptions();

                List<string> m_DropOptionsStakes = new List<string>();

                //StacksUpdateResultdata._id  ="all";

                ///stackData.result.Add(StacksUpdateResultdata);
                //				stackData.result = lobbyFilterResponse.result;

                m_DropOptionsStakes.Add("All");
                foreach (StacksUpdateResult stacks in lobbyFilterResponse.result)
                {
                    m_DropOptionsStakes.Add(stacks.minStack + "/" + stacks.maxStack);
                    //				print("GetStacks: " + stacks.minStack +"/"+ stacks.maxStack);
                }
                stakes.ClearOptions();
                stakes.AddOptions(m_DropOptionsStakes);
                //stakes.value = 1;
                SelectedStack = "all";
                ResetSelectedgameButtons(SelectedGames);
                StopCoroutine("RefreshTableOnInterval");
                StartCoroutine("RefreshTableOnInterval");
                RefreshTable(true);
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(lobbyFilterResponse.message, GetStacksData);
            }
        });
    }
    public List<NormalTournamentDetails.NormalTournamentData> StaticTournamentDisplay;
    private void GetStaticTournamentData()
    {
        UIManager.Instance.SocketGameManager.StaticTournament((socket, packet, args) =>
        {
            print("StaticTournament: " + packet.ToString());
            stackData = new StacksUpdate();
            JSONArray arr = new JSONArray(packet.ToString());
            var resp = arr.getString(arr.length() - 1);
            PokerEventListResponse<NormalTournamentDetails.NormalTournamentData> StaticTournamentResponse = JsonUtility.FromJson<PokerEventListResponse<NormalTournamentDetails.NormalTournamentData>>(resp);

            if (StaticTournamentResponse.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                StaticTournamentDisplay = StaticTournamentResponse.result;

                if (StaticTournamentDisplay.Count <= 0)
                {
                    return;
                }
                for (int i = 0; i < StaticTournamentResponse.result.Count; i++)
                {
                    if (StaticTournamentResponse.result[i].timerDueSeconds <= StaticTournamentResponse.result[i].timerDisplayWhen)
                    {
                        tournamentRemainingTime(i, StaticTournamentResponse.result[i].timerDueSeconds, StaticTournamentResponse.result[i].displayDateTime,
                            StaticTournamentResponse.result[i].name, StaticTournamentResponse.result[i].prize);

                    }
                    else
                    {
                        tournamentRemainingTime(i, 0, StaticTournamentResponse.result[i].displayDateTime,
                            StaticTournamentResponse.result[i].name, StaticTournamentResponse.result[i].prize);
                    }
                }
            }
            else
            {
                totalRemainTournamentTime1 = 0;
                txtDisplayTournamentDate1.text = "";
                txtDisplayTournamentname1.text = "Coming Soon...";
                txtDisplayTournamentAmount1.text = "";
                totalRemainTournamentTime2 = 0;
                txtDisplayTournamentDate2.text = "";
                txtDisplayTournamentname2.text = "Coming Soon...";
                txtDisplayTournamentAmount2.text = "";
                //UIManager.Instance.DisplayMessagePanel(StaticTournamentResponse.message);
            }
        });
    }

    void tournamentRemainingTime(int Id, int startTime, string displayDateTime, string Tname, string Prize)
    {
        if (Id.Equals(0))
        {
            totalRemainTournamentTime1 = startTime;
            txtDisplayTournamentDate1.text = displayDateTime;
            txtDisplayTournamentname1.text = Tname;
            txtDisplayTournamentAmount1.text = Prize;
        }
        else
        {
            totalRemainTournamentTime2 = startTime;
            txtDisplayTournamentDate2.text = displayDateTime;
            txtDisplayTournamentname2.text = Tname;
            txtDisplayTournamentAmount2.text = Prize;
        }
    }
    public void ComingSoonbuttonTap()
    {
        UIManager.Instance.DisplayMessagePanel("Coming Soon !");
    }
    void GetCheckRunningGameCall()
    {
        UIManager.Instance.SocketGameManager.GetCheckRunningGame((socket, packet, args) =>
        {
            Debug.Log("GetCheckRunningGame  : " + packet.ToString());

            try
            {
                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;
                PokerEventListResponse<RoomsListing.Room> resp = JsonUtility.FromJson<PokerEventListResponse<RoomsListing.Room>>(resp1);

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    txtTournamentRequestmessage.text = Constants.Messages.RegistredTournamentmessage;
                    MiniTable.gameObject.SetActive(true);
                    for (int i = 0; i < resp.result.Count; i++)
                    {
                        RegistredTableData obj = GetTableObjIfAlreadyCreated(resp.result[i].roomId);
                        if (obj != null)
                        {
                            obj.SetData(resp.result[i]);
                        }
                        else
                        {
                            RegistredTableData RegistredTableDataDetails = Instantiate(RegistredTableDataObj) as RegistredTableData;
                            RegistredTableDataDetails.SetData(resp.result[i]);
                            RegistredTableDataDetails.transform.SetParent(RegistredTableDataParent, false);
                            RegistredTableDataList.Add(RegistredTableDataDetails);
                        }
                    }
                    RemoveOtherPlayers(resp.result);
                }
                else
                {
                    txtTournamentRequestmessage.text = "";
                    MiniTable.gameObject.SetActive(false);
                    RegistredTableDataList.Clear();
                    foreach (Transform Data in RegistredTableDataParent)
                    {
                        Destroy(Data.gameObject);
                    }
                }

            }
            catch (System.Exception e)
            {
                UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
                Debug.LogError("exception  : " + e);
            }
        });
    }

    private void RemoveOtherPlayers(List<RoomsListing.Room> roomsList)
    {
        if (RegistredTableDataList != null)
        {
            if (roomsList == null || roomsList.Count == 0)
            {
                foreach (RegistredTableData tro in RegistredTableDataList.ToArray())
                {
                    RegistredTableDataList.Remove(tro);
                    Destroy(tro.gameObject);
                }
            }
            else
            {
                List<string> roomIdsList = roomsList.Select(o => o.roomId).ToList();

                foreach (RegistredTableData tro in RegistredTableDataList.ToArray())
                {

                    for (int i = 0; i < roomsList.Count; i++)
                    {
                        if (roomsList == null || !roomIdsList.Contains(tro.Roomid))
                        {
                            RegistredTableDataList.Remove(tro);
                            Destroy(tro.gameObject);
                        }
                    }
                }
            }
        }
    }

    private RegistredTableData GetTableObjIfAlreadyCreated(string tableID)
    {
        if (RegistredTableDataList != null)
        {
            for (int i = 0; i < RegistredTableDataList.Count; i++)
            {
                string Ranker = RegistredTableDataList[i].Roomid;
                if (tableID.Equals(Ranker))
                {
                    return RegistredTableDataList[i];
                }
            }
        }
        return null;
    }

    private void DestroyAllRunningTournamentTables()
    {
        if (RegistredTableDataList == null)
            return;

        if (RegistredTableDataList != null)
        {
            foreach (RegistredTableData go in RegistredTableDataList)
            {
                Destroy(go.gameObject);
            }
            RegistredTableDataList = new List<RegistredTableData>();
        }
    }

    private void ClearObjects()
    {
        TournamentPopupCall(false);
        RegistredTableDataList.Clear();
        foreach (Transform Data in RegistredTableDataParent)
        {
            Destroy(Data.gameObject);
        }
    }

    #endregion

    #region COROUTINES
    IEnumerator PreviousScreen(float timer)
    {
        UIManager.Instance.DisplayLoader("");
        yield return new WaitForSeconds(timer);
        UIManager.Instance.LobbyPanelNew.Close(); // LobbyScreeen not used more
    }

    IEnumerator LogoutFunction(float timer)
    {
        //UIManager.Instance.assetOfGame.SavedLoginData.Username = "";
        //UIManager.Instance.assetOfGame.SavedLoginData.password = "";
        //UIManager.Instance.assetOfGame.SavedLoginData.PlayerId = "";
        //UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe = false;
        //SaveLoad.SaveGame();
        //SaveLoad.DeleteFile();
        UIManager.Instance.isLogOut = true;

        //Utility.Instance.ClearLoginData();

        Game.Lobby.socketManager.Close();
        UIManager.Instance.DisplayLoader("");
        Game.Lobby.socketManager.Open();
        Game.Lobby.ConnectToSocket();
        UIManager.Instance.tableManager.RemoveAllMiniTableData();
        UIManager.Instance.SoundManager.stopBgSound();
        yield return new WaitForSeconds(timer);
        UIManager.Instance.Reset(false);
    }

    private IEnumerator RefreshTableOnInterval()
    {
        while (true)
        {
            if (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(Constants.Poker.RefreshTableInterval);
                RefreshTable();
            }
            if (TournamentDetailsScreen.gameObject.activeSelf)
            {
                StopCoroutine("RefreshTableOnInterval");
            }
        }
    }
    private IEnumerator RefreshRunningTableOnInterval()
    {
        while (true)
        {
            if (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(Constants.Poker.RefreshRegistredTableInterval);
                //RefreshRunningTable (); false call
            }

            if (TournamentDetailsScreen.gameObject.activeSelf)
            {
                StopCoroutine("RefreshRunningTableOnInterval");
            }
        }
    }
    IEnumerator ForceScrollDownHomePage()
    {
        // Wait for end of frame AND force update all canvases before setting to bottom.
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
        HomepageScrollRect.verticalScrollbar.value = 1;
        Canvas.ForceUpdateCanvases();
    }


    #endregion


    #region GETTER_SETTER
    public double Chips
    {
        set
        {
            txtChips.text = value.ConvertToCommaSeparatedValueColor();
        }
    }

    public string Username
    {
        set
        {
            txtuesrname.text = value;
        }
    }
    #endregion

}
