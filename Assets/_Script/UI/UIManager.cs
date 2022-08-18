using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;
using BestHTTP.SocketIO;
using BestHTTP.SocketIO.Events;
using UnityEngine.Events;
using System;
using BestHTTP;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour
{
    #region PUBLIC_VARIABLES

    public SERVER server = SERVER.Club;
    public enum CurrencyType
    {
        cash,
        coin,
        count
    };

    public SafeArea safeArea;
    public static UIManager Instance;
    public AssetOfGame assetOfGame;
    public Canvas main;
    public CanvasScaler mainscale;
    public GameObject BackgroundImage;
    public SplashPanel splashScreen;
    public MainHomePageScreen MainHomeScreen;
    public WebGLAffiliatePanel webGLAffiliatePanel;
    public LobbyPanel LobbyScreeen; // not need used
    public LobbyPanelNew LobbyPanelNew;
    public DetailsTournament DetailsTournament;
    public GamePanel GameScreeen;
    public TournamentWinnerPanel TournamentWinnerPanel;
    public HistoryPanel historyPanel;
    public BackgroundEventManager backgroundEventManager;
    public TableManager tableManager;
    public SoundManager SoundManager;
    public Loader loader;
    public GameObject downloadImageLoaderPrefab;
    public SocketManager socketManager;
    public SocketGamemanager SocketGameManager;
    public UnityIAPManager panelInApp;
    public IPLocationService ipLocationService;

    [Header("Utility Panels")]
    public UtilityMessagePanel messagePanel;
    public UtilityMessagePanel messagePanelInfo;
    public UtilityMessagePanel messagePanelJoinTable;
    public tournamentMessagePanel TournamentRequestPanel;
    public UtilityMessagePanel RebuyInMessagePanel;
    public termsAndConditonspanel TandCondPopup;
    public PanelDeleteAccount DeleteAccountPopup;
    public PanelInformationAboutMoneyOnAccountPopup PanelInformationAboutMoneyOnAccountPopup;
    public PanelContactSupportPopup PanelContactSupportPopup;

    [Header("Public Variables")]
    public bool isLogAllEnabled = false;
    public bool IsMultipleTableAllowed;
    public bool isChipsTransferAllowed;
    public bool MySuperPlayer = false;
    public bool isLogOut = false;
    public bool isWebGLAffiliateBuild = false;
    public bool AbsolutePlayer = false;
    public string SelectedPlayerPerTable;
    public bool isGalleryOpen = false;

    [Header("Currency Related")]
    public Image[] currencyContainingImages;
    public Image[] sidePotCurrencyImages;
    public Image mainPotImage;
    public Sprite coinCurrencySprite;
    public Sprite cashCurrencySprite;
    public Sprite[] bgSprites;
    public float assetsLoadedPercentage = 0;
    public int totalIconsToDownload = 0;

    [Header("Enum")]
    public CurrencyType currencyType;
    public GameType gameType = GameType.cash;
    public GameType selectedGameType = GameType.cash;
    public GameSpeed selectedGameSpeed = GameSpeed.regular;

    [Header("Header")]
    public Reporter reporter;
    #endregion

    #region PRIVATE_VARIABLES
    public string CustormUrl;
    public string StoreDetails;
    private int _profilePic;
    public string webglToken;
    #endregion

    #region UNITY_CALLBACKS

    void Awake()
    {
        Debug.Log("Application.persistentDataPath = > " + Application.persistentDataPath);
        Instance = this;
        SaveLoad.LoadGame();
    }
    // Use this for initialization
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.runInBackground = true;
        //print("width =>" + Screen.width);
        //print("height =>" + Screen.height);
#if UNITY_IOS && !UNITY_EDITOR
        if (Screen.height > 1080)
        {
            mainscale.referenceResolution = new Vector2(Screen.width, Screen.height);
        }
        else
        {
            mainscale.referenceResolution = new Vector2(1920, 1080);
        }
#endif

        Game.Lobby.ConnectToSocket();
        Reset(true);

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX
        Debug.unityLogger.logEnabled = true;
        reporter.gameObject.SetActive(true);
#else
        Debug.unityLogger.logEnabled = isLogAllEnabled;
        reporter.gameObject.SetActive(isLogAllEnabled);
#endif

        DateTime dateTime = DateTime.Parse("2019-09-21T09:27:57.000Z");
        print(">>>>> " + dateTime.ToString());
    }

    /*void TestServer(string newUrl)
	{
		try
		{
			HTTPRequest request = new HTTPRequest(new Uri(newUrl), (HTTPRequest req, HTTPResponse res) =>
			{
				if (res.IsSuccess)
				{
					Debug.Log(newUrl + " URL is working...");
				}
				else
				{
					Debug.Log(newUrl + " URL is not working...");
				}
			});
			request.Send();
		}
		catch (Exception e)
		{
			Debug.Log("url exception " + e);
		}
	}*/


    //	void OnEnable ()
    //	{
    ////		connectToSocket ();
    //	}

    void OnDisable()
    {
        Game.Lobby.socketManager.Close();
    }



    #endregion

    #region DELEGATE_CALLBACKS

    private static void OnTournamentStart(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("-- OnTournamentStart --" + packet.ToString());

        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        TournamentStartData TournamentStartDetails = JsonUtility.FromJson<TournamentStartData>(resp);
        UIManager.Instance.HideTournamentRequestPopup();
        UIManager.Instance.DisplayTorurnamentConfirmationPanel(TournamentStartDetails.message, TournamentStartDetails.timer, () =>
        {
            AcceptTournament(TournamentStartDetails.tournamentId, TournamentStartDetails.roomId, TournamentStartDetails.type, 0);
        }, () =>
        {
            UIManager.Instance.getRejectTournamentCall(TournamentStartDetails.tournamentId);
        });

        if (!Instance.tableManager.IsMiniTableTournamentExisted(TournamentStartDetails.tournamentId))
            UIManager.Instance.LobbyScreeen.GetRunningGameList();
    }

    private static void OnSngTournamentStart(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("-- OnSngTournamentStart --" + packet.ToString());

        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        TournamentStartData TournamentStartDetails = JsonUtility.FromJson<TournamentStartData>(resp);
        UIManager.Instance.HideTournamentRequestPopup();
        UIManager.Instance.DisplayTorurnamentConfirmationPanel(TournamentStartDetails.message, TournamentStartDetails.timer, () =>
        {
            AcceptTournament(TournamentStartDetails.tournamentId, TournamentStartDetails.roomId, TournamentStartDetails.type, 1);
        }, () =>
        {
            UIManager.Instance.getRejectTournamentCall(TournamentStartDetails.tournamentId);
        });

        if (!Instance.tableManager.IsMiniTableTournamentExisted(TournamentStartDetails.tournamentId))
            UIManager.Instance.LobbyScreeen.GetRunningGameList();
    }

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
    public void SocketOn()
    {
        Game.Lobby.socketManager.Socket.On(Constants.PokerEvents.SOCKET_EVENT_OnTournamentStart, OnTournamentStart);
        Game.Lobby.socketManager.Socket.On(Constants.PokerEvents.SOCKET_EVENT_OnSngTournamentStart, OnSngTournamentStart);
        Game.Lobby.socketManager.Socket.On(Constants.PokerEvents.LateJoinTournament, OnLateJoinTournament);
    }

    public void DisplayConfirmationPanel(string message, UnityAction affirmativeAction, UnityAction negativeAction = null)
    {
        //		messagePanel.MainImage.sprite = messagePanel.Options[1];
        messagePanel.txtTitle.text = message;
        messagePanel.txtAffirmativeButton.text = Constants.MessageTitle.DefaultAffirmativeButtonTitle;
        messagePanel.txtNegativeButton.text = Constants.MessageTitle.DefaultNegativeButtonTitle;

        messagePanel.btnAffirmativeAction.onClick.RemoveAllListeners();
        messagePanel.btnNegativeAction.onClick.RemoveAllListeners();
        messagePanel.btnAffirmativeAction.onClick.AddListener(affirmativeAction);
        messagePanel.btnAffirmativeAction.onClick.AddListener(() =>
        {
            HidePopup();
            UIManager.Instance.SoundManager.OnButtonClick();
        });
        if (negativeAction != null)
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            messagePanel.btnNegativeAction.onClick.AddListener(SoundManager.OnButtonClick);
            messagePanel.btnNegativeAction.onClick.AddListener(negativeAction);
        }
        else
        {
            messagePanel.btnNegativeAction.onClick.AddListener(SoundManager.OnButtonClick);
            messagePanel.btnNegativeAction.onClick.AddListener(HidePopup);
        }
        messagePanel.btnAffirmativeAction.gameObject.SetActive(true);
        messagePanel.btnNegativeAction.gameObject.SetActive(true);
        messagePanel.btnOK.gameObject.SetActive(false);
        messagePanel.Open();
    }

    /// <summary>
    /// Displays the confirmation panel with button titles.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="positiveButtonTitle">Positive button title.</param>
    /// <param name="negativeButtonTitle">Negative button title.</param>
    /// <param name="affirmativeAction">Affirmative action.</param>
    /// <param name="negativeAction">Negative action.</param>
    public void DisplayConfirmationPanel(string message, string positiveButtonTitle, string negativeButtonTitle, UnityAction affirmativeAction, UnityAction negativeAction)
    {
        messagePanel.txtTitle.text = message;

        messagePanel.txtAffirmativeButton.text = positiveButtonTitle;
        messagePanel.txtNegativeButton.text = negativeButtonTitle;

        messagePanel.btnAffirmativeAction.onClick.RemoveAllListeners();
        messagePanel.btnNegativeAction.onClick.RemoveAllListeners();
        messagePanel.btnAffirmativeAction.onClick.AddListener(affirmativeAction);
        messagePanel.btnNegativeAction.onClick.AddListener(negativeAction);

        messagePanel.btnAffirmativeAction.gameObject.SetActive(true);
        messagePanel.btnNegativeAction.gameObject.SetActive(true);
        messagePanel.btnOK.gameObject.SetActive(false);

        messagePanel.Open();
    }
    public void DisplayRebuyinConfirmationPanel(string message, string positiveButtonTitle, string negativeButtonTitle, UnityAction affirmativeAction, UnityAction negativeAction)
    {
        RebuyInMessagePanel.txtTitle.text = message;

        RebuyInMessagePanel.txtAffirmativeButton.text = positiveButtonTitle;
        RebuyInMessagePanel.txtNegativeButton.text = negativeButtonTitle;

        RebuyInMessagePanel.btnAffirmativeAction.onClick.RemoveAllListeners();
        RebuyInMessagePanel.btnNegativeAction.onClick.RemoveAllListeners();
        RebuyInMessagePanel.btnAffirmativeAction.interactable = true;
        RebuyInMessagePanel.btnAffirmativeAction.onClick.AddListener(affirmativeAction);
        RebuyInMessagePanel.btnNegativeAction.onClick.AddListener(negativeAction);

        RebuyInMessagePanel.btnAffirmativeAction.gameObject.SetActive(true);
        RebuyInMessagePanel.btnNegativeAction.gameObject.SetActive(true);
        RebuyInMessagePanel.btnOK.gameObject.SetActive(false);

        RebuyInMessagePanel.Open();
    }
    public void DisplayMessagePanel(string message, UnityAction playerAction = null)
    {
        if (message == "")
            return;

        messagePanel.txtTitle.text = message;
        //messagePanel.MainImage.sprite = messagePanel.Options[0];
        messagePanel.btnOK.onClick.RemoveAllListeners();
        if (playerAction != null)
        {
            messagePanel.btnOK.onClick.AddListener(playerAction);
            UIManager.Instance.SoundManager.OnButtonClick();
        }
        else
        {
            messagePanel.btnOK.onClick.AddListener(HidePopup);
            UIManager.Instance.SoundManager.OnButtonClick();
        }
        messagePanel.btnAffirmativeAction.gameObject.SetActive(false);
        messagePanel.btnNegativeAction.gameObject.SetActive(false);
        messagePanel.btnOK.gameObject.SetActive(true);
        messagePanel.txtOKButton.text = Constants.MessageTitle.DefaultOkButtonTitle;
        messagePanel.gameObject.SetActive(true);
    }

    public void DisplayMessagePanelOnly(string message)
    {
        messagePanelInfo.txtTitle.text = message;
        //messagePanel.MainImage.sprite = messagePanel.Options[0];
        messagePanelInfo.btnOK.onClick.RemoveAllListeners();
        messagePanelInfo.btnAffirmativeAction.onClick.RemoveAllListeners();
        messagePanelInfo.btnNegativeAction.onClick.RemoveAllListeners();
        messagePanelInfo.btnAffirmativeAction.gameObject.SetActive(false);
        messagePanelInfo.btnNegativeAction.gameObject.SetActive(false);
        messagePanelInfo.btnOK.gameObject.SetActive(false);

        messagePanelInfo.gameObject.SetActive(true);
    }

    /// <summary>
    /// Displays the confirmation panel.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="affirmativeAction">Affirmative action.</param>
    /// <param name="negativeAction">Negative action.</param>

    public void DisplayTorurnamentConfirmationPanel(string message, int timer, UnityAction affirmativeAction, UnityAction negativeAction = null)
    {
        //		messagePanel.MainImage.sprite = messagePanel.Options[1];

        TournamentRequestPanel.txtTitle.text = message;
        TournamentRequestPanel.txtTimer.text = timer.ToString();

        TournamentRequestPanel.btnAffirmativeAction.onClick.RemoveAllListeners();
        TournamentRequestPanel.btnNegativeAction.onClick.RemoveAllListeners();
        TournamentRequestPanel.btnAffirmativeAction.onClick.AddListener(affirmativeAction);
        TournamentRequestPanel.btnAffirmativeAction.onClick.AddListener(() =>
        {
            HidePopup();
            UIManager.Instance.SoundManager.OnButtonClick();
        });
        if (negativeAction != null)
        {
            TournamentRequestPanel.btnNegativeAction.onClick.AddListener(negativeAction);
            UIManager.Instance.SoundManager.OnButtonClick();
        }
        else
        {
            TournamentRequestPanel.btnNegativeAction.onClick.AddListener(HidePopup);
            UIManager.Instance.SoundManager.OnButtonClick();
        }
        TournamentRequestPanel.btnAffirmativeAction.gameObject.SetActive(true);
        TournamentRequestPanel.btnNegativeAction.gameObject.SetActive(true);

        TournamentRequestPanel.Open();
        if (timer.Equals(0))
        {
            Invoke("HideTournamentRequestPopup", 1f);
        }
    }
    public void DisplayJoinConfirmationPanel(string message, string positiveButtonTitle, string negativeButtonTitle, UnityAction affirmativeAction, UnityAction negativeAction)
    {
        //messagePanel.txtTitle.text = ArabicFixer.Fix(message, false, false); ;
        messagePanelJoinTable.txtTitle.text = message;
        messagePanelJoinTable.txtAffirmativeButton.text = positiveButtonTitle;
        messagePanelJoinTable.txtNegativeButton.text = negativeButtonTitle;
        messagePanelJoinTable.btnAffirmativeAction.onClick.RemoveAllListeners();
        messagePanelJoinTable.btnNegativeAction.onClick.RemoveAllListeners();
        messagePanelJoinTable.btnAffirmativeAction.interactable = true;
        messagePanelJoinTable.btnAffirmativeAction.onClick.AddListener(affirmativeAction);
        messagePanelJoinTable.btnNegativeAction.onClick.AddListener(negativeAction);
        messagePanelJoinTable.btnAffirmativeAction.gameObject.SetActive(true);
        messagePanelJoinTable.btnNegativeAction.gameObject.SetActive(true);
        messagePanelJoinTable.btnOK.gameObject.SetActive(false);
        messagePanelJoinTable.Open();
    }

    public void DisplayLoader(string message = "")
    {
        loader.txtMessage.text = message;
        loader.gameObject.SetActive(true);
        //StopCoroutine ("WaitingProcess");
        StartCoroutine("WaitingProcess");
    }


    public void HideLoader()
    {
        StopCoroutine("WaitingProcess");
        if (loader.gameObject.activeInHierarchy)
        {
            loader.Close();
        }

    }

    public void HidePopup()
    {
        messagePanel.Close();
        RebuyInMessagePanel.Close();
    }

    public void HidemessagePanelInfoPopup()
    {
        messagePanelInfo.Close();
    }

    public void HideTournamentRequestPopup()
    {
        TournamentRequestPanel.Close();
        CancelInvoke("HideTournamentRequestPopup");
    }
    public void SetPlayerLoginType(int no)
    {
        if (no.Equals(1))
        {
            assetOfGame.SavedLoginData.IsLogin = true;
        }
        else
        {
            assetOfGame.SavedLoginData.IsLogin = false;
        }
        PlayerPrefs.SetInt("PokerBetAutoLogin", no);
        //Debug.Log("set => " + PlayerPrefs.GetInt("PokerBetAutoLogin"));
    }
    #endregion

    #region PRIVATE_METHODS

    public void SetCurrencyImages()
    {
        Sprite currentSprite = null;
        switch (currencyType)
        {
            case CurrencyType.cash:
                currentSprite = cashCurrencySprite;
                break;
            case CurrencyType.coin:
                currentSprite = coinCurrencySprite;
                break;
        }
        for (int i = 0; i < currencyContainingImages.Length; i++)
        {
            currencyContainingImages[i].sprite = currentSprite;
        }

        for (int i = 0; i < sidePotCurrencyImages.Length; i++)
        {
            sidePotCurrencyImages[i].sprite = currentSprite;
        }

        mainPotImage.sprite = currentSprite;
    }

    void RejoinUser()
    {
        Game.Lobby.socketManager.Close();
        Reset(false);
        Game.Lobby.socketManager.Open();
    }

    public void Reset(bool isFirstTime)
    {
        SetSecurityData();
        foreach (Transform panels in safeArea.gameObject.transform)
        {
            panels.gameObject.SetActive(false);
        }

        foreach (Transform panels in main.transform)
        {
            panels.gameObject.SetActive(false);
        }


        if (isFirstTime)
        {
            if (IsWebGLAffiliat)
            {
                //webGLAffiliatePanel.Open();
                splashScreen.Open();
            }
            else
            {
                splashScreen.Open();
            }

        }
        else
        {
            if (IsWebGLAffiliat)
            {
                //webGLAffiliatePanel.Open();
                splashScreen.Open();
            }
            else
            {
                MainHomeScreen.Open();
            }
        }

        safeArea.gameObject.SetActive(true);
        BackgroundImage.SetActive(true);
        isLogOut = false;
    }

    public void TournamentScreenCall()
    {
        //foreach (Transform panels in main.transform) {
        /*foreach (Transform panels in safeArea.transform) {
            panels.gameObject.SetActive (false);
        }*/
        UIManager.Instance.DisplayLoader("");
        UIManager.Instance.LobbyPanelNew.Close(); // LobbyScreeen not used more
        UIManager.Instance.GameScreeen.Open();
    }

    public void InappButtonTap()
    {
        panelInApp.OpenInappPanel();
    }

    public void ReceiveGameData(string data)
    {
        //Debug.unityLogger.logEnabled = true;
        print("UNITY ReceiveGameData string: " + data);
        ExternalCallClass.Instance.ReceiveGameData(data);
        //Debug.unityLogger.logEnabled = UIManager.Instance.isLogAllEnabled;
    }
    public void ReceiveDepositBase64Data(string data)
    {
        print("UNITY ReceiveDepositBase64Data string: " + data);
        ExternalCallClass.Instance.ReceiveDepositBase64Data(data);
    }

    static void AcceptTournament(string TournamentId, string RoomId, string tournamentType, int ID)
    {
        UIManager.Instance.SocketGameManager.getJoinTournament(ID, TournamentId, tournamentType, (socket, packet, args) =>
        {
            Debug.Log("getJoinTournament  response: " + packet.ToString());
            UIManager.Instance.HideLoader();
            try
            {
                PokerEventResponse<RoomsListing.Room> resp = JsonUtility.FromJson<PokerEventResponse<RoomsListing.Room>>(Utility.Instance.GetPacketString(packet));

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.GameScreeen.Close();
                    UIManager.Instance.LobbyPanelNew.Open(); // LobbyScreeen not used more
                    UIManager.Instance.HideTournamentRequestPopup();
                    //UIManager.Instance.GameScreeen.currentRoomData.roomId = resp.result.roomId;
                    //Constants.Poker.TableId = resp.result.roomId;
                    //UIManager.Instance.GameScreeen.SetRoomDataAndPlay(resp.result);
                    //UIManager.Instance.getRejectTournamentCall (TournamentId);

                    if (UIManager.Instance.IsMultipleTableAllowed && !UIManager.Instance.tableManager.playingTableList.Contains(resp.result.roomId))
                    {
                        UIManager.Instance.tableManager.DeselectAllTableSelection();
                        UIManager.Instance.tableManager.AddMiniTable(resp.result);
                    }
                    UIManager.Instance.tableManager.DeselectAllTableSelection();

                    MiniTable miniTable = UIManager.Instance.tableManager.GetMiniTable(resp.result.roomId);
                    if (miniTable != null)
                    {
                        miniTable.OnMiniTableButtonTap();
                    }
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }
            }
            catch (System.Exception e)
            {
                UIManager.Instance.DisplayMessagePanel("Something went wrong. Try again.");
                Debug.LogError("exception  : " + e);
            }
        });
        /*
        if (ID.Equals (0)) {
            UIManager.Instance.SocketGameManager.getJoinTournament (TournamentId, (socket, packet, args) => {
                Debug.Log ("getJoinTournament  response: " + packet.ToString ());
                UIManager.Instance.HideLoader ();
                try {
                    JSONArray arr = new JSONArray (packet.ToString ());
                    string Source;
                    Source = arr.getString (arr.length () - 1);
                    var resp1 = Source;
                    PokerEventResponse<RegularTournament> resp = JsonUtility.FromJson<PokerEventResponse<RegularTournament>> (resp1);

                    if (resp.status.Equals (Constants.PokerAPI.KeyStatusSuccess)) {
                        UIManager.Instance.GameScreeen.currentRoomData.roomId = resp.result.roomId;

                        UIManager.Instance.gameType = GameType.Touranment;
                        UIManager.Instance.selectedGameType = GameType.Touranment;
                        Constants.Poker.TableId = resp.result.roomId;
                        UIManager.Instance.GameScreeen.SetRoomDataAndPlay(resp.result.roomId, resp.result.namespaceString, resp.result.pokerGameType, resp.result.gameFormat);
                        UIManager.Instance.TournamentScreenCall ();
                        UIManager.Instance.getRejectTournamentCall (TournamentId);
                    } else {
                        UIManager.Instance.DisplayMessagePanel (resp.message);
                    }

                } catch (System.Exception e) {
                    UIManager.Instance.DisplayMessagePanel (Constants.Messages.SomethingWentWrong);
                    Debug.LogError ("exception  : " + e);
                }
            });
        } else {
            UIManager.Instance.gameType = GameType.sng;
            UIManager.Instance.selectedGameType = GameType.sng;
            Constants.Poker.TableId = RoomId;
            UIManager.Instance.GameScreeen.SetRoomDataAndPlay(RoomId);
            UIManager.Instance.TournamentScreenCall ();
            UIManager.Instance.getRejectTournamentCall (TournamentId);
        }
        */
    }

    void getRejectTournamentCall(string TournamentId)
    {
        UIManager.Instance.SocketGameManager.getRejectTournament(TournamentId, (socket, packet, args) =>
        {
            Debug.Log("getRejectTournament  : " + packet.ToString());
            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;
            PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                UIManager.Instance.HideTournamentRequestPopup();
            }
            else
            {

                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
        HidePopup();
    }


    private static void OnLateJoinTournament(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("-- OnLateJoinTournament --" + packet.ToString());

        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        RoomsListing.Room lateJoinTournament = JsonUtility.FromJson<RoomsListing.Room>(resp);

        if (!Instance.tableManager.IsMiniTableTournamentExisted(lateJoinTournament.tournamentId))
            UIManager.Instance.LobbyScreeen.GetRunningGameList();

        if (UIManager.Instance.IsMultipleTableAllowed && !UIManager.Instance.tableManager.playingTableList.Contains(lateJoinTournament.roomId))
        {
            UIManager.Instance.tableManager.DeselectAllTableSelection();
            UIManager.Instance.tableManager.AddMiniTable(lateJoinTournament);
        }
        UIManager.Instance.tableManager.DeselectAllTableSelection();

        MiniTable miniTable = UIManager.Instance.tableManager.GetMiniTable(lateJoinTournament.roomId);
        if (miniTable != null)
        {
            UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.Close();
            miniTable.MiniTableButtonTap();
        }

    }

    public void SetSecurityData()
    {
        string ss = Constants.Constants.CountryCodeUrl;
        StartCoroutine(GetCountryDetails(ss));
    }

    #endregion

    #region COROUTINES
    private IEnumerator GetCountryDetails(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        yield return www;

        yield return www;
        if (www.error == null && www.downloadHandler.text != null)
        {
            StoreDetails = www.downloadHandler.text;
        }
        else
        {
            SetSecurityData();
        }
    }
    public IEnumerator WaitingProcess()
    {
        yield return new WaitForSeconds(60);

        if (!SocketGameManager.HasInternetConnection())
        {
            HideLoader();
            DisplayMessagePanel("Internet connection not available.", RejoinUser);
        }
        else
        {
            HideLoader();
            DisplayMessagePanel("Internet Connection is Low Or Server TimerOut", RejoinUser);
        }
    }

    public IEnumerator SwitchGameTable(RoomsListing.Room miniTableRoomData, bool isWaitingPlayer, int seatIndex)
    {
        foreach (MiniTable tables in tableManager.MiniTables)
        {

            tables.imgSelected.SetActive(false);
        }
        UIManager.Instance.DisplayLoader(Constants.Messages.PleaseWait);
        UIManager.Instance.GameScreeen.Close();
        yield return new WaitForSeconds(0.5f);
        Constants.Poker.TableId = miniTableRoomData.roomId;
        UIManager.Instance.GameScreeen.currentRoomData = new RoomsListing.Room();
        foreach (MiniTable tables in tableManager.MiniTables)
        {
            if (miniTableRoomData.roomId.Equals(tables.roomId))
            {
                tables.imgSelected.SetActive(true);
            }
            else
            {
                tables.imgSelected.SetActive(false);

            }
        }
        UIManager.Instance.GameScreeen.SetRoomDataAndPlay(miniTableRoomData);
        yield return new WaitForSeconds(1f);
        UIManager.Instance.GameScreeen.Open();

        if (isWaitingPlayer)
        {
            UIManager.Instance.messagePanelJoinTable.Close();
            GameScreeen.OnSeatButtonTap(seatIndex);
        }
    }

    #endregion

    #region GETTER_SETTER
    public string tokenHack()
    {
        string syndicate = null;
        rootHack root = new rootHack();
        root.userId = UIManager.Instance.assetOfGame.SavedLoginData.PlayerId;
        root.timestamp = DateTimeToUnix(DateTime.UtcNow);
        string json = JsonUtility.ToJson(root);
        //Debug.Log(json);
        string encryptedJson = tableManager.AESEncryption(json);
        syndicate = encryptedJson;
        //syndicate = System.Uri.EscapeDataString(syndicate);
        //print(syndicate);
        return syndicate;
    }
    public long DateTimeToUnix(DateTime MyDateTime)
    {
        TimeSpan timeSpan = MyDateTime - new DateTime(1970, 1, 1, 0, 0, 0);
        return (long)timeSpan.TotalSeconds;
    }
    public int ProfilePic
    {
        get
        {
            return _profilePic;
        }
        set
        {
            _profilePic = value;

            assetOfGame.SavedLoginData.SelectedAvatar = _profilePic;
            LobbyScreeen.ProfileScreen.PanelMyAccount.ProfilePanel.PlayerProfile.sprite = assetOfGame.profileAvatarList.profileAvatarSprite[_profilePic];
            LobbyScreeen.profilePicLeft.sprite = assetOfGame.profileAvatarList.profileAvatarSprite[_profilePic];
        }
    }

    public bool IsWebGLAffiliat
    {
        get
        {
#if UNITY_WEBGL
            if (isWebGLAffiliateBuild)
                return true;
            else
                return false;
#else
            return false;
#endif
        }
    }
    #endregion
}
