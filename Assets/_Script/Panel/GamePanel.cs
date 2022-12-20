using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BestHTTP.SocketIO;
using System;
using System.Text.RegularExpressions;
using System.Linq;

public class GamePanel : MonoBehaviour
{
    private RoomData _currentRoomData;

    [SerializeField] private PockerRoomCustomization _pockerRoomCustomization;
    [SerializeField] private CustomizationPopup _customizationPopup;

    [SerializeField] private Button _gameCustomizationButton;

    [SerializeField] private PlayerContainer _playerContainer;

    private List<PlayerPlace> _playerPlaces;
    private List<PlayerPlace> _orderPlayerPlaces;

    [SerializeField] private Button _lobbyButton;

    [Space] [Space]

    #region PUBLIC_VARIABLES

    ///public Image mainBG;
    //public Image currentBG;
    //public Image imgTable;
    //public Image imgTableLogoMain;
    public GameObject currentTurnEffect;

    [Header("Gamobjects")]
    //public GameObject[] SelectedGame;
    public bool showBetPanel = false;

    private List<GameObject> instantiatedObjList = new List<GameObject>();

    [Header("Transforms")] public Transform cardGeneratePosition;
    public Transform ChatParent;
    public GameObject chatPanel;
    public Transform cardsParentForInstantiatedCards;

    [Header("ScriptableObjects")] public PokerCard[] TableCards;

    public PokerCard[] TableExtraCards;

    //public PokerPlayer[] GamePlayers;
    //public Image[] SeatPositionCups;
    //public Image[] SeatPositionCupsByOrder;
    public RoomsListing.Room currentRoomData;
    public PlayerInfoList AllPokerPlayerInfo;
    public WaitingListPanel waitingListPanel;
    public BuyinPanel BuyInAmountPanel;
    public BetPanel Bet;
    public PreBetButtonsPanel preBetButtonsPanel;
    public PokerTableCards tableCardsData;
    public BreakTIme BreakTimerPanel;
    public ScrollRect chatHistoryScrollRect;
    public TournamentLoserPanel tournamentLoserPanel;
    public panelAllin allInPanel;
    public panelRaise RaisePanel;
    public UserClubGameHistoryPanel HistoryScreen;
    public RankingPanel RankingScreen;
    public tournamentLeaderBoardPanel tournamentLeaderBoardScreen;
    public waitingTextPanel waitingTextScreen;
    [Header("Panels")] public FullGameHistoryPanel fullGameHistoryPanel;

    [Header("Images")] public Image imgDealer;
    public Image imgstraddleTimer;

    public Image imgRunItTwiceTimer;
    //public Sprite[] imgTables;
    //public Sprite[] imgBackgrounds;
    //public Sprite[] imgTableLogo;


    [Header("Buttons")]
    //public Button[] Seats;
    //public Button[] SeatsByOrder;
    public Button btnChat;

    public Button btnRebuyinTournament;

    //public Button btnRebuyin;
    public Button btnAddOnTournament;
    public Button btnIAmBack;
    public Button btnShowCards;
    public Button btnMinimize;
    public Button btnStandup;
    public Button btnExitToLobby;
    public Button btnStraddleCards;
    public Button btnStraddleCardsCheck;
    public Button btntwiceCards;
    public Button btnHistoryOpen;
    public Button btnRankingOpen;
    public Button btnClockOpen;
    [SerializeField] private RebuyinButtons _rebuyinButtons;

    //[Header("Two Player")]
    //public PokerPlayer[] twoByTwoGamePlayers;
    //public Button[] TwoByTwoSeats;
    //public Button[] TwoByTwoSeatsByOrder;
    //public Image[] TwoByTwoSeatPositionCupsByOrder;

    //[Header("Three Player")]
    //public PokerPlayer[] threeByThreeGamePlayers;
    //public Button[] ThreeByThreeSeats;
    //public Button[] ThreeByThreeSeatsByOrder;
    //public Image[] ThreeByThreeSeatPositionCupsByOrder;

    //[Header("Four Player")]
    //public PokerPlayer[] fourByfourGamePlayers;//new
    //public Button[] fourByfourSeats;
    //public Button[] fourByfourSeatsByOrder;
    //public Image[] fourByfourSeatPositionCupsByOrder;

    //[Header("Five Player")]
    //public PokerPlayer[] FiveByFiveGamePlayers;
    //public Button[] FiveByFiveSeats;
    //public Button[] FiveByFiveSeatsByOrder;
    //public Image[] FiveByFiveSeatPositionCupsByOrder;

    //[Header("Six Player")]
    //public PokerPlayer[] sixBysixGamePlayers;//new
    //public Button[] sixBysixSeats;
    //public Button[] sixBysixSeatsByOrder;
    //public Image[] sixBysixSeatPositionCupsByOrder;

    //[Header("Seven Player")]
    //public PokerPlayer[] sevenBysevenGamePlayers;//new
    //public Button[] sevenBysevenSeats;
    //public Button[] sevenBysevenSeatsByOrder;
    //public Image[] sevenBysevenSeatPositionCupsByOrder;

    //[Header("Eight Player")]
    //public PokerPlayer[] eightByeightGamePlayers;//new
    //public Button[] eightByeightSeats;
    //public Button[] eightByeightSeatsByOrder;
    //public Image[] eightByeightSeatPositionCupsByOrder;

    //[Header("Nine Player")]
    //public PokerPlayer[] NineByNineGamePlayers;
    //public Button[] NineByNineSeats;
    //public Button[] NineByNineSeatsByOrder;
    //public Image[] NineByNineSeatPositionCupsByOrder;


    [Header("Text")] public TextMeshProUGUI txtPotAmount;
    public TextMeshProUGUI txtTotalTablePotAmount;
    public TextMeshProUGUI txtBlindAmountforTournament;
    public TextMeshProUGUI txtBlindAmountforTournamentTimer;
    public TextMeshProUGUI txtbreakTournamentTimer;
    public TextMeshProUGUI txtplayersPlayingTournament;
    public Text txtGameName;
    public InputField inputfieldChat;
    public InputField inputfieldChatHebrew;
    public TMP_InputField inputfieldChatmesh;
    public TextMeshProUGUI txtPreviousGameNumber;
    public TextMeshProUGUI txtBlindLevel;
    public TextMeshProUGUI txtAppVersionOnTable;
    public TextMeshProUGUI txtTournamentBreakTableMessage;

    public Text txtChatPanel;

    //public TextMeshProUGUI txtTotalChips;
    public Text txtTwiceTimerText;
    public Text txtStraddleTimerText;
    public Text txtClockTimerText;
    public Text txtRebuyRemainingTime;
    public Text txtIAmBackRemainingTime;
    public Text txtSidePot1;
    public Text txtSidePot2;
    public Text txtMainPot1;
    public Text txtMainPot2;
    public Text txtAddOnTimer;
    public flipfont ffText;
    [Header("Toggle")] public Toggle toggleSitOutNextHand;
    public Toggle toggleSitOutNextBigBlind;
    public Toggle toggleWaitForBigBlind;
    public Toggle EnglishLang;
    public Toggle HebrewLang;

    [Header("Prefabs")] public Text TxtChatPrefab;
    public GameObject cardPrefab;
    [Header("Booleans")] public bool HasJoin;
    public bool Switchingtable;
    public bool isOmahaPokerGame;
    public double _PotAmount;
    private string _BlindLevel = "";
    private string _BreakLevel = "";
    private string _tournamentPlayers = "";
    public int _turnTime = 10;
    public int ownPlayerSeatIndex = 0;
    public PlayerSidePot _sidePotAmountListNew;
    public List<string> tableCardsListData;

    string individualLine = ""; //Control individual line in the multi-line text component.

    int numberOfAlphabetsInSingleLine = 20;

    #endregion

    public bool IsCash() => UIManager.Instance.assetOfGame.SavedLoginData.isCash; //rewrite

    #region PRIVATE_VARIABLES

    System.Diagnostics.StackTrace stackTrace;

    private PokerGameRound _currentRound;
    private double _totalTablePotAmount;
    private double _oldPlayerBuyIn = 0;

    private string _gameId = "";
    private string _previousGameId = "";
    private string _previousGameNumber = "";
    TimeSpan oneDay = new TimeSpan(00, 10, 00);

    private float minutesInSeconds;
    private float hoursInSeconds;
    private float secondsInSeconds; //don't really need this.
    private double totalSeconds;
    private double totalRebuySeconds;
    private string[] remainingBonusTime;
    string chatMessage = "";

    TimeSpan clockDay = new TimeSpan(00, 15, 00);

    private float clockminutesInSeconds;
    private float clockhoursInSeconds;
    private float clocksecondsInSeconds; //don't really need this.
    private double clocktotalSeconds;

    public List<string> roomIds;
    private string[] remainingclockTime;
    public clockResult ClockData;

    private int _bankTimer;
    public int GetBankTimer() => _bankTimer;

    #endregion

    #region UNITY_CALLBACKS

    void Start()
    {
        txtAppVersionOnTable.text = Utility.Instance.GetApplicationVersionWithOS();

        _gameCustomizationButton.onClick.AddListener(_customizationPopup.OpenClose);
        _lobbyButton.onClick.AddListener(BackButtonTap);
    }

    public void Init()
    {
        _currentRoomData = new RoomData();

        _playerContainer.Init();

        _rebuyinButtons.Init(this);
        _pockerRoomCustomization.Init();
        _customizationPopup.Init(_pockerRoomCustomization);
        Bet.Init(this);

        _playerPlaces = _playerContainer.GetPlayerPaces();
        _orderPlayerPlaces = _playerContainer.GetOrderPlayerPaces();
    }

    /*public void SetActiveOpenSeatButton(int index, bool active)
    {
        //todo: Set active Open Seat or place?
        _playerPlaces[index].openSeatButton.gameObject.SetActive(active);
        //_playerPlaces[index].gameObject.SetActive(active);
    }*/

    public void UpdateOpenSeatButton()
    {
        /*for (int i = 0; i < _playerPlaces.Count; i++)
        {
            if (!_playerPlaces[i].pokerPlayer.gameObject.activeInHierarchy)
            {
                UIManager.Instance.GameScreeen.SetActiveOpenSeatButton(i, true);
            }
            else
            {
                UIManager.Instance.GameScreeen.SetActiveOpenSeatButton(i, false);
            }
        }*/

        foreach (var playerPlace in _playerPlaces)
        {
            playerPlace.openSeatButton.gameObject.SetActive(
                !playerPlace.pokerPlayer.gameObject.activeInHierarchy);
        }
    }

    void OnEnable()
    {
        //_playerContainer.Init();

        UIManager.Instance.SoundManager.stopBgSound();
        tableCardsListData.Clear();
        currentTurnEffect.gameObject.GetComponent<Image>().enabled = false;
        BreakLevelRank = "";
        BlindLevelRank = "";
        freeze = false;
        DestroyInstantiatedObjects();
        CancelInvoke();
        StopAllCoroutines();
        SidePot1 = 0;
        SidePot2 = 0;
        MainPot1 = 0;
        MainPot2 = 0;
        //UIManager.Instance.GameScreeen.Chips = UIManager.Instance.assetOfGame.SavedLoginData.chips;
        //InvokeRepeating("GetPerfectClock", 0f, 1f);
        if (UIManager.Instance.IsMultipleTableAllowed)
        {
            btnMinimize.Open();
        }
        else
        {
            btnMinimize.Close();
        }

        //mainBG.sprite = currentBG.sprite;
        SidePotAmountNew = new PlayerSidePot();
        Switchingtable = false;
        if (UIManager.Instance.selectedGameType != GameType.cash)
        {
            txtBlindAmountforTournament.text = "";
            txtBlindAmountforTournamentTimer.text = "";
            txtplayersPlayingTournament.text = "";
            GameName = "";
        }
        else
        {
            txtBlindAmountforTournamentTimer.text = "";
            txtBlindAmountforTournament.text = "";
            txtplayersPlayingTournament.text = "";
            GameName = "";
        }

        HasJoin = false;
        ownPlayerSeatIndex = 0;
        txtAddOnTimer.Close();
        btnIAmBack.gameObject.SetActive(false);
        btnStandup.gameObject.SetActive(false);
        btnHistoryOpen.Close();

        btnClockOpen.Close();
        btnRankingOpen.Close();
        ResetData(false);
        ResetTableCards();
        DestoryStuff();
        //StartCoroutine ("SitPlayersAll");

        txtChatPanel.text = "";
        chatPanel.gameObject.SetActive(false);
        btnChat.interactable = false;
        btnRebuyinTournament.Close();
        _rebuyinButtons.Close();
        btnAddOnTournament.Close();
        btnShowCards.Close();
        btntwiceCards.Close();
        btnStraddleCards.Close();
        btnStraddleCardsCheck.Close();
        btnHistoryOpen.interactable = false;
        btnClockOpen.interactable = false;
        //Debug.Log("here");
        waitingTextScreen.closePanel();
        BuyInAmountPanel.Close();
        Bet.Close();
        HistoryScreen.Close();
        RankingScreen.Close();
        tournamentLeaderBoardScreen.Close();
        preBetButtonsPanel.Close();
        tournamentLoserPanel.Close();
        waitingListPanel.btnJoinWaitingList.Close();
        waitingListPanel.btnLeaveWaitingList.Close();

        print("Namespace: " + Game.Lobby.CashSocket.Namespace);

        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnSubscribeRoom, OnSubscribeRoomReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.PlayerInfoList, OnPlayerInfoListReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.TableCards, OnTableCardsInfoReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.PlayerAction, OnPlayerActionReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.PlayerLeft, OnPlayerLeft);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.RoundStarted, OnRoundStarted);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.GameStarted, OnGameStarted);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.PlayerCards, OnPlayersCardsInfoReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.RoundComplete, OnRoundComplete);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.GameFinished, OnGameFinished);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.ResetGame, OnPokerResetGame);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnTurnTimer, OnTurnTimerRecieved);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.RevertPoint, OnRevertPointReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.RevertPointFolded, OnRevertPointFoldedReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnGameStartWait, OnGameStartWaitReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnSngTournamentFinished, OnSngTournamentFinishedReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnSwitchRoom, OnSwitchRoomReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.RegularTournamentFinished, OnRegularTournamentFinishedReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnBreak, OnBreakReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnTournamentTableBreakMessage, OnTournamentTableBreakMessage);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnBlindLevelsRaised, OnBlindLevelRaised);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnBlindLevels, OnBlindLevelsReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.Chat, OnChatMessageReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.IAmBack, OnIAmBackReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnGameBoot, OnGameBootReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnPlayerCards, OnPlayerCardsReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.superPlayerData, OnsuperPlayerDataReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnPlayersCardsDistribution, OnPlayersCardsDistributionReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.GameFinishedPlayersCards, OnGameFinishedPlayersCardsReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnSubscibePlayersCards, OnSubscibePlayersCardsReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnOpenBuyInPanel, OnOpenBuyInPanelReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.DisplayShowCardButton, OnDisplayShowCardButtonReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.ShowCardResult, OnShowCardResultReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.GamePopupNotification, OnGamePopupNotificationReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnReBuyIn, OnReBuyIn);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnCloseReBuyIn, OnCloseReBuyIn);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnTournamentPrize, OnTournamentPrize);

        Game.Lobby.CashSocket.On(Constants.PokerEvents.TurnOnRunItTwice, OnTurnOnRunItTwiceReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.RunItTwiceResponse, OnRunItTwiceResponseeReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.TurnOffRunItTwice, OnTurnOffRunItTwiceReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.cancelTwice, OncancelTwiceReceived);

        Game.Lobby.CashSocket.On(Constants.PokerEvents.TurnOnStraddle, OnTurnOnStraddleReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.TurnOffStraddle, OnTurnOffStraddleReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.StraddleTwiceTimer, OnStraddleTwiceTimerReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnReservedSeatList, OnReservedSeatList);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.onAbsolutePlayer, onAbsolutePlayereceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.TournamentBounty, OnbountyTournament);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.AddOnTimer, OnAddOnTimer);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.CancelTournament, OnCancelTournament);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.BanPlayerTournament, OnBanPlayerTournament);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.RoomDeleted, OnRoomDeleted);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.TableBalanceNotice, OnTableBalanceNotice);


        /*
		Game.Lobby.CashSocket.On(Constants.PokerEvents.PlayerHandInfo, OnPlayerHandInfoReceived);
		Game.Lobby.CashSocket.On(Constants.PokerEvents.InviteFriend, OnFriendInvitationReceived);
		Game.Lobby.CashSocket.On(Constants.PokerEvents.Gift, OnGiftReceived);
		Game.Lobby.CashSocket.On(Constants.PokerEvents.TipToDealer, OnPlayerGiveTipToDealer);
		Game.Lobby.CashSocket.On(Constants.PokerEvents.RoomToCloseOneHour, OnRoomToCloseOneHour);
		Game.Lobby.CashSocket.On(Constants.PokerEvents.RoomToCloseHalfHour,  OnRoomToCloseHalfHour);

		Game.Lobby.CashSocket.On(Constants.PokerEvents.PlayerSidePot, OnPlayerSidePotReceived);
		Game.Lobby.CashSocket.On(Constants.PokerEvents.TournamentFinished, OnTournamnetFinished);
		Game.Lobby.CashSocket.On(Constants.PokerEvents.BlindLevelUpdate, OnBlindLevelUpdated);
		*/

        UIManager.Instance.HideLoader();

        if (currentRoomData.isTournament)
        {
            _playerContainer.SetActivePlayerPlace(9);

            //GamePlayers = NineByNineGamePlayers;
            //Seats = NineByNineSeats;
            //SeatsByOrder = NineByNineSeatsByOrder;
            //SeatPositionCups = NineByNineSeatPositionCupsByOrder;
            //SeatPositionCupsByOrder = NineByNineSeatPositionCupsByOrder;
        }
        else
        {
            if (currentRoomData.maxPlayers == 2)
            {
                //GamePlayers = twoByTwoGamePlayers;
                _playerContainer.SetActivePlayerPlace(2);
                //Seats = TwoByTwoSeats;
                //SeatsByOrder = TwoByTwoSeatsByOrder;
                //SeatPositionCups = TwoByTwoSeatPositionCupsByOrder;
                //SeatPositionCupsByOrder = TwoByTwoSeatPositionCupsByOrder;
            }
            else if (currentRoomData.maxPlayers == 3)
            {
                //GamePlayers = threeByThreeGamePlayers;
                _playerContainer.SetActivePlayerPlace(3);

                //Seats = ThreeByThreeSeats;
                //SeatsByOrder = ThreeByThreeSeatsByOrder;
                //SeatPositionCups = ThreeByThreeSeatPositionCupsByOrder;
                //SeatPositionCupsByOrder = ThreeByThreeSeatPositionCupsByOrder;
            }
            else if (currentRoomData.maxPlayers == 4) //new
            {
                //GamePlayers = fourByfourGamePlayers;
                _playerContainer.SetActivePlayerPlace(4);

                //Seats = fourByfourSeats;
                //SeatsByOrder = fourByfourSeatsByOrder;
                //SeatPositionCups = fourByfourSeatPositionCupsByOrder;
                //SeatPositionCupsByOrder = fourByfourSeatPositionCupsByOrder;
            }
            else if (currentRoomData.maxPlayers == 5)
            {
                //GamePlayers = FiveByFiveGamePlayers;
                _playerContainer.SetActivePlayerPlace(5);

                //Seats = FiveByFiveSeats;
                //SeatsByOrder = FiveByFiveSeatsByOrder;
                //SeatPositionCups = FiveByFiveSeatPositionCupsByOrder;
                //SeatPositionCupsByOrder = FiveByFiveSeatPositionCupsByOrder;
            }
            else if (currentRoomData.maxPlayers == 6) //new
            {
                //GamePlayers = sixBysixGamePlayers;
                _playerContainer.SetActivePlayerPlace(6);
                //Seats = sixBysixSeats;
                //SeatsByOrder = sixBysixSeatsByOrder;
                //SeatPositionCups = sixBysixSeatPositionCupsByOrder;
                //SeatPositionCupsByOrder = sixBysixSeatPositionCupsByOrder;
            }
            else if (currentRoomData.maxPlayers == 7) //new
            {
                //GamePlayers = sevenBysevenGamePlayers;
                _playerContainer.SetActivePlayerPlace(7);
                //Seats = sevenBysevenSeats;
                //SeatsByOrder = sevenBysevenSeatsByOrder;
                //SeatPositionCups = sevenBysevenSeatPositionCupsByOrder;
                //SeatPositionCupsByOrder = sevenBysevenSeatPositionCupsByOrder;
            }
            else if (currentRoomData.maxPlayers == 8) //new
            {
                //GamePlayers = eightByeightGamePlayers;
                _playerContainer.SetActivePlayerPlace(8);

                //Seats = eightByeightSeats;
                //SeatsByOrder = eightByeightSeatsByOrder;
                //SeatPositionCups = eightByeightSeatPositionCupsByOrder;
                //SeatPositionCupsByOrder = eightByeightSeatPositionCupsByOrder;
            }
            else
            {
                //GamePlayers = NineByNineGamePlayers;
                _playerContainer.SetActivePlayerPlace(9);
                //Seats = NineByNineSeats;
                //SeatsByOrder = NineByNineSeatsByOrder;
                //SeatPositionCups = NineByNineSeatPositionCupsByOrder;
                //SeatPositionCupsByOrder = NineByNineSeatPositionCupsByOrder;
            }
        }

        /*for (int i = 0; i < SeatPositionCupsByOrder.Length; i++)
        {
            SeatPositionCupsByOrder[i].Close();
        }*/

        /*for (int i = 0; i < SeatPositionCupsByOrder.Length; i++)
        {
            int newSeatIndex = i;
            //SeatsByOrder[i].name = "Image-Seat-" + newSeatIndex;
            //SeatsByOrder[i].onClick.RemoveAllListeners();
            //SeatsByOrder[i].onClick.AddListener(() => { OnSeatButtonTap(newSeatIndex); });
            //SeatPositionCupsByOrder[i].Open();
        }*/

        //_playerContainer.InitOpenSeatButtons(OnSeatButtonTap);

        for (var i = 0; i < _orderPlayerPlaces.Count; i++)
        {
            var index = i;
            _orderPlayerPlaces[i].SetOpenSeat(() => OnSeatButtonTap(index));
        }

        /* for (int i = 0; i < SeatsByOrder.Length; i++)
         {
             int newSeatIndex = i;
             SeatsByOrder[i].name = "Image-Seat-" + newSeatIndex;
             SeatsByOrder[i].onClick.RemoveAllListeners();
             SeatsByOrder[i].onClick.AddListener(() => { OnSeatButtonTap(newSeatIndex); });
         }*/

        WaitForBigBlindCheckbox = false;
        preBetButtonsPanel.toggleSitOutNextBigBlind.Close();
        preBetButtonsPanel.toggleSitOutNextHand.Close();
        ;

        StopCoroutine(SubscribeEventEnum());
        StartCoroutine(SubscribeEventEnum());
        //Invoke ("SubscribeEventInvoke", 0.5f);
        //SubscribeEventInvoke();

        UIManager.Instance.tableManager.MiniTablePosition(0);
        UIManager.Instance.tableManager.ShowAddTableButton();
    }

    private void Update()
    {
        if (totalSeconds > 0 && btnIAmBack.gameObject.activeInHierarchy)
        {
            totalSeconds -= 1 * Time.deltaTime;
            txtIAmBackRemainingTime.Open();
            txtIAmBackRemainingTime.text = "Remaining Time : " + string.Format("{1:D2}:{2:D2}", TimeSpan.FromSeconds(totalSeconds).Hours, TimeSpan.FromSeconds(totalSeconds).Minutes, TimeSpan.FromSeconds(totalSeconds).Seconds);
            DisplayTime(totalSeconds);
            //Debug.Log("I Am Back Remaining : " + string.Format("{1:D2}:{2:D2}", TimeSpan.FromSeconds(totalSeconds).Hours, TimeSpan.FromSeconds(totalSeconds).Minutes, TimeSpan.FromSeconds(totalSeconds).Seconds));
        }
        else
        {
            totalSeconds = 0;
            txtIAmBackRemainingTime.text = "";
            txtIAmBackRemainingTime.Close();
            //isBonusReady = true;
            //txtDailyBonus.text = "Bonus Ready!";
        }

        if (clocktotalSeconds > 0 && UIManager.Instance.AbsolutePlayer)
        {
            clocktotalSeconds -= 1 * Time.deltaTime;
            txtClockTimerText.Open();
            txtClockTimerText.text = string.Format("{1:D2}:{2:D2}", TimeSpan.FromSeconds(clocktotalSeconds).Hours, TimeSpan.FromSeconds(clocktotalSeconds).Minutes, TimeSpan.FromSeconds(clocktotalSeconds).Seconds);

            //Debug.Log("I Am Back Remaining : " + string.Format("{1:D2}:{2:D2}", TimeSpan.FromSeconds(totalSeconds).Hours, TimeSpan.FromSeconds(totalSeconds).Minutes, TimeSpan.FromSeconds(totalSeconds).Seconds));
        }
        else
        {
            clocktotalSeconds = 0;
            txtClockTimerText.text = "";
            txtClockTimerText.Close();
        }

        if (totalRebuySeconds > 0)
        {
            totalRebuySeconds -= 1 * Time.deltaTime;
            txtRebuyRemainingTime.Open();
            txtRebuyRemainingTime.text = "Remaining Time : " + string.Format("{1:D2}:{2:D2}", TimeSpan.FromSeconds(totalRebuySeconds).Hours, TimeSpan.FromSeconds(totalRebuySeconds).Minutes, TimeSpan.FromSeconds(totalRebuySeconds).Seconds);
        }
        else
        {
            totalRebuySeconds = 0;
            txtRebuyRemainingTime.text = "";
            UIManager.Instance.RebuyInMessagePanel.Close();
            txtRebuyRemainingTime.Close();
        }
    }

    void SubscribeEventInvoke()
    {
        UIManager.Instance.SocketGameManager.SubscribeRoom(OnSubscribeRoomDone);
    }

    IEnumerator SubscribeEventEnum()
    {
        yield return new WaitForSeconds(0.25f);
        UIManager.Instance.SocketGameManager.SubscribeRoom(OnSubscribeRoomDone);
    }

    void OnDisable()
    {
        tableCardsListData.Clear();
        txtAddOnTimer.Close();
        btnIAmBack.gameObject.SetActive(false);
        freeze = false;
        //CancelInvoke("GetPerfectClock");
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnSubscribeRoom, OnSubscribeRoomReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.PlayerInfoList, OnPlayerInfoListReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.TableCards, OnTableCardsInfoReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.PlayerAction, OnPlayerActionReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.PlayerLeft, OnPlayerLeft);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.RoundStarted, OnRoundStarted);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.GameStarted, OnGameStarted);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.PlayerCards, OnPlayersCardsInfoReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.RoundComplete, OnRoundComplete);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.GameFinished, OnGameFinished);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.ResetGame, OnPokerResetGame);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnTurnTimer, OnTurnTimerRecieved);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.RevertPoint, OnRevertPointReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.RevertPointFolded, OnRevertPointFoldedReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnGameStartWait, OnGameStartWaitReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnSngTournamentFinished, OnSngTournamentFinishedReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnSwitchRoom, OnSwitchRoomReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.RegularTournamentFinished, OnRegularTournamentFinishedReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnBreak, OnBreakReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnTournamentTableBreakMessage, OnTournamentTableBreakMessage);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnBlindLevelsRaised, OnBlindLevelRaised);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnBlindLevels, OnBlindLevelsReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.Chat, OnChatMessageReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.IAmBack, OnIAmBackReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnGameBoot, OnGameBootReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnPlayerCards, OnPlayerCardsReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.superPlayerData, OnsuperPlayerDataReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnPlayersCardsDistribution, OnPlayersCardsDistributionReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.GameFinishedPlayersCards, OnGameFinishedPlayersCardsReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnSubscibePlayersCards, OnSubscibePlayersCardsReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnOpenBuyInPanel, OnOpenBuyInPanelReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.DisplayShowCardButton, OnDisplayShowCardButtonReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.ShowCardResult, OnShowCardResultReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.GamePopupNotification, OnGamePopupNotificationReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnReBuyIn, OnReBuyIn);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnCloseReBuyIn, OnCloseReBuyIn);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnTournamentPrize, OnTournamentPrize);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.TurnOnRunItTwice, OnTurnOnRunItTwiceReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.RunItTwiceResponse, OnRunItTwiceResponseeReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.TurnOffRunItTwice, OnTurnOffRunItTwiceReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.cancelTwice, OncancelTwiceReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.TurnOnStraddle, OnTurnOnStraddleReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.TurnOffStraddle, OnTurnOffStraddleReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.StraddleTwiceTimer, OnStraddleTwiceTimerReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnReservedSeatList, OnReservedSeatList);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.TournamentBounty, OnbountyTournament);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.AddOnTimer, OnAddOnTimer);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.CancelTournament, OnCancelTournament);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.BanPlayerTournament, OnBanPlayerTournament);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.RoomDeleted, OnRoomDeleted);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.TableBalanceNotice, OnTableBalanceNotice);
        //Game.Lobby.CashSocket.Off(Constants.PokerEvents.PlayerHandInfo, OnPlayerHandInfoReceived);
        //OnLobbyDone ();
        Bet.Close();
        preBetButtonsPanel.Close();
        Constants.Poker.GameId = Constants.Poker.TableId = "";
        Constants.Poker.GameNumber = Constants.Poker.TableNumber = "";
        fullGameHistoryPanel.Close();
        //		UIManager.Instance.historyPanel.OnGameDisable ();

        PokerPlayer ownPlayer = GetOwnPlayer();
        if (ownPlayer != null)
        {
            UIManager.Instance.assetOfGame.SavedLoginData.chips += ownPlayer.BuyInAmount;
            ownPlayer.BuyInAmount = 0;
        }

        /*for (int i = 0; i < Seats.Length; i++)
        {
            Seats[i].Close();
        }
        */
        foreach (var orderPlayerPlace in _orderPlayerPlaces)
        {
            orderPlayerPlace.openSeatButton.gameObject.SetActive(false);
        }
        //_playerContainer.SetActiveOpenSeatButtons(false);

        /*for (int i = 0; i < SeatPositionCupsByOrder.Length; i++)
        {
            SeatPositionCupsByOrder[i].Close();
        }
        for (int i = 0; i < SeatPositionCups.Length; i++)
        {
            SeatPositionCups[i].Close();
        }*/
        DestoryStuff();
        btnChat.interactable = HasJoinedRoom;

        BreakTimerPanel.Close();
        TotalTablePotAmount = 0;
        //		HistoryManager.GetInstance ().ResetHistory ();
        showBetPanel = false;
        DestroyInstantiatedObjects();
        UIManager.Instance.HidemessagePanelInfoPopup();
        Resources.UnloadUnusedAssets();
    }

    #endregion

    #region DELEGATE_CALLBACKS

    private void OnTurnOnRunItTwiceReceived(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("OnTurnOnRunItTwiceReceived Broadcast Response : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;


        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        RunItTwiceData RunItTwiceDataResp = JsonUtility.FromJson<RunItTwiceData>(resp);
        PokerPlayer myPlr = GetOwnPlayer();
        if (myPlr == null)
        {
            return;
        }

        if (myPlr.status == PlayerStatus.Waiting || myPlr.status == PlayerStatus.Ideal || myPlr.status == PlayerStatus.Fold)
        {
            return;
        }

        imgRunItTwiceTimer.fillAmount = 1f;
        btntwiceCards.Open();
    }

    private void OnRunItTwiceResponseeReceived(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("OnRunItTwiceResponseeReceived Broadcast Response : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;


        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        RunItTwiceRequestData RunItTwiceRequestDataResp = JsonUtility.FromJson<RunItTwiceRequestData>(resp);
        if (RunItTwiceRequestDataResp.roomId.Equals(Constants.Poker.TableId))
        {
            PokerPlayer ownPlayer = GetPlayerById(RunItTwiceRequestDataResp.playerId);
            ownPlayer.twiceIcon.Open();
        }
    }

    private void OnTurnOffRunItTwiceReceived(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("OnTurnOffRunItTwiceReceived Broadcast Response : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;


        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        StreddleData StreddleDataResp = JsonUtility.FromJson<StreddleData>(resp);
        if (StreddleDataResp.roomId.Equals(Constants.Poker.TableId))
        {
            UIManager.Instance.HidePopup();
            btntwiceCards.Close();
            StartCoroutine(popClose(2f, StreddleDataResp.message));
        }
    }

    private void OncancelTwiceReceived(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("OncancelTwiceReceived Broadcast Response : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;


        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        RunItTwiceRequestData RunItTwiceRequestDataResp = JsonUtility.FromJson<RunItTwiceRequestData>(resp);
        if (RunItTwiceRequestDataResp.roomId.Equals(Constants.Poker.TableId))
        {
            /*foreach (PokerPlayer plr in GamePlayers)
            {
                if (plr.gameObject.activeInHierarchy)
                {
                    plr.twiceIcon.Close();
                }
            }*/

            foreach (var playerPlace in _playerPlaces)
            {
                var pokerPlayer = playerPlace.pokerPlayer;
                if (pokerPlayer.gameObject.activeInHierarchy)
                {
                    pokerPlayer.twiceIcon.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTurnOnStraddleReceived(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("OnTurnOnStraddleReceived Broadcast Response : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;


        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        StreddleData StreddleDataResp = JsonUtility.FromJson<StreddleData>(resp);
        if (StreddleDataResp.roomId.Equals(Constants.Poker.TableId))
        {
            if (StreddleDataResp.playerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
            {
                imgstraddleTimer.fillAmount = 1f;
                btnStraddleCards.Open();
                btnStraddleCardsCheck.Open();
            }
            else
            {
                //txtWaitingText.text = StreddleDataResp.message + " " + StreddleDataResp.displayTime + " Seconds";
                //txtWaitingText.transform.parent.gameObject.SetActive(true);
            }

            btnExitToLobby.interactable = false;
            btnStandup.interactable = false;
        }
    }

    private void OnTurnOffStraddleReceived(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("OnTurnOffStraddleReceived Broadcast Response : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;


        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        StreddleData StreddleDataResp = JsonUtility.FromJson<StreddleData>(resp);
        if (StreddleDataResp.roomId.Equals(Constants.Poker.TableId))
        {
            if (StreddleDataResp.playerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
            {
                UIManager.Instance.HidePopup();
                btnStraddleCards.Close();
                btnStraddleCardsCheck.Close();
            }

            imgstraddleTimer.fillAmount = 1f;
            //else
            //{
            //	txtWaitingText.text ="";
            //	txtWaitingText.transform.parent.gameObject.SetActive(false);
            //}
        }
    }

    private void OnStraddleTwiceTimerReceived(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("OnStraddleTwiceTimerReceived Broadcast Response : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;


        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        OnStraddleTwiceTimerReceivedData StreddleDataResp = JsonUtility.FromJson<OnStraddleTwiceTimerReceivedData>(resp);
        if (StreddleDataResp.roomId.Equals(Constants.Poker.TableId))
        {
            //        foreach(PokerPlayer plr in GamePlayers)
            //        {
            //if (!plr.gameObject.activeInHierarchy || plr.status != PlayerStatus.Playing)
            //{
            //	return;
            //            }
            //        }
            PokerPlayer myPlr = GetOwnPlayer();
            if (myPlr == null)
            {
                return;
            }

            if (myPlr.status == PlayerStatus.Waiting || myPlr.status == PlayerStatus.Ideal || myPlr.status == PlayerStatus.Fold)
            {
                return;
            }

            Debug.Log("StreddleDataResp.isStraddle " + StreddleDataResp.isStraddle);
            if (StreddleDataResp.isStraddle)
            {
                Debug.Log("timer " + StreddleDataResp.currentTimer / StreddleDataResp.totalTimer);
                imgstraddleTimer.fillAmount = StreddleDataResp.currentTimer / StreddleDataResp.totalTimer;

                txtTwiceTimerText.text = StreddleDataResp.currentTimer + "";
                if (StreddleDataResp.playerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
                {
                    btnStraddleCards.Open();
                    btnStraddleCardsCheck.Open();
                }
                else
                {
                    Debug.Log("waiting timer " + StreddleDataResp.totalTimer);
                    //            txtWaitingText.text = StreddleDataResp.message + " " + StreddleDataResp.currentTimer + " Seconds";
                    //          txtWaitingText.transform.parent.gameObject.SetActive(true);
                }
            }

            if (StreddleDataResp.isRIT)
            {
                txtStraddleTimerText.text = StreddleDataResp.currentTimer + "";
                imgRunItTwiceTimer.fillAmount = StreddleDataResp.currentTimer / StreddleDataResp.totalTimer;
                btntwiceCards.Open();
            }


            if (StreddleDataResp.currentTimer <= 0)
            {
                btnStraddleCards.Close();
                btnStraddleCardsCheck.Close();
                btntwiceCards.Close();
                imgstraddleTimer.fillAmount = 1f;
                imgRunItTwiceTimer.fillAmount = 1f;
            }
        }
    }

    private void onAbsolutePlayereceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("onAbsolutePlayereceived  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        clockResult result = JsonUtility.FromJson<clockResult>(Utility.Instance.GetPacketString(packet));

        try
        {
            //#if UNITY_ANDROID || UNITY_IOS || UNITY_WEBGL //||UNITY_EDITOR
            if (result.playerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
            {
                UIManager.Instance.assetOfGame.SavedLoginData.isAbsolute = true;
                if (result.roomId.Equals(currentRoomData.roomId))
                {
                    ClockRemainingTime(result.dueSeconds, result.currentTime);
                }
            }

            //#endif
            //ClockData = new clockResult();
            //ClockData.startTime = result.startTime;
            //StoreTimeNow();
        }
        catch (System.Exception e)
        {
            Debug.LogError("onAbsolutePlayereceived -> Exception  : " + e);
        }
    }

    private void OnReservedSeatList(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnReservedSeatList  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        ReservedListResult result = JsonUtility.FromJson<ReservedListResult>(Utility.Instance.GetPacketString(packet));

        try
        {
            if (result.roomId == Constants.Poker.TableId || result.roomId == "")
            {
                waitingListPanel.WaitingListFunction(result);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("OnReservedSeatList -> Exception  : " + e);
        }
    }

    private void OnSubscribeRoomReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnSubscribeRoomReceived  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        showBetPanel = false;
        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;
        PokerGameHistory subscribeResp = JsonUtility.FromJson<PokerGameHistory>(resp);


        UIManager.Instance.HideLoader();
        TurnTime = subscribeResp.turnTime;
        currentRoomData.smallBlind = subscribeResp.smallBlindChips;
        currentRoomData.bigBlind = subscribeResp.bigBlindChips;

        if (subscribeResp == null || currentRoomData.roomId != subscribeResp.roomId)
            return;

        TournamentBreakTableMessage = subscribeResp.tournamentTableWaitMessage;
        Constants.Poker.roomId = subscribeResp.roomId;
        Constants.Poker.TableId = subscribeResp.roomId;
        GameId = subscribeResp.gameId;
        Constants.Poker.TableNumber = subscribeResp.tableNumber;
        GameName = subscribeResp.tableNumber;
        PotAmountValue = subscribeResp.gameHistory.potAmount;
        PreviousGameNumber = subscribeResp.previousGameNumber;
        PreviousGameId = subscribeResp.previousGameId;
        OldPlayerBuyIn = subscribeResp.oldPlayerBuyIn;
        SidePotAmountNew = subscribeResp.gameHistory.PlayerSidePot;
        //			txtPotAmount.text = "pot = " + PotAmountValue.ToString ();
        //txtTableId.text = "Table ID : " + Constants.Poker.TableNumber;   
        TotalTablePotAmount = subscribeResp.gameHistory.totalTablePotAmount;

        if (!string.IsNullOrEmpty(subscribeResp.gameHistory.currentRound))
            CurrentRound = subscribeResp.gameHistory.currentRound.ToEnum<PokerGameRound>();

        for (int i = 0; i < subscribeResp.gameHistory.cards.Count; i++)
        {
            //				Debug.Log ("-----456------");
            this.TableCards[i].gameObject.SetActive(true);
            this.TableCards[i].DisplayCardWithoutAnimation(subscribeResp.gameHistory.cards[i]);
            tableCardsListData.Add(subscribeResp.gameHistory.cards[i].ToString());
        }

        for (int i = 0; i < subscribeResp.gameHistory.extraCards.Count; i++)
        {
            this.TableExtraCards[i].gameObject.SetActive(true);
            this.TableExtraCards[i].DisplayCardWithoutAnimation(subscribeResp.gameHistory.extraCards[i]);
            Debug.Log("extra card > ");
        }

        tournamentPlayersNumbers = subscribeResp.blindLevelsData.players;
        SetBlindLevelData(subscribeResp.blindLevelsData);
        //Debug.Log("muck => " + currentRoomData.muck);
        preBetButtonsPanel.toggleAllowMuck.gameObject.SetActive(currentRoomData.muck);
        preBetButtonsPanel.SetPrebetOptions(subscribeResp.defaultButtons);

        if (subscribeResp.roomIdChanged)
        {
            UIManager.Instance.tableManager.ReplaceMiniTableRoomId(currentRoomData.roomId, subscribeResp.roomId);
            this.currentRoomData.roomId = subscribeResp.roomId;
            SubscribeEventInvoke();
            UIManager.Instance.tableManager.ReSubscribeMiniTables(this.currentRoomData.roomId);
        }
        /*foreach (PokerPlayer plr in GamePlayers)
        {
            if (subscribeResp.straddlePlayerId != null && subscribeResp.straddlePlayerId.Equals(plr.PlayerId))
            {
                plr.BetAmount = subscribeResp.straddleChips;
                plr.BuyInAmount = subscribeResp.straddlePlayerChips;
                plr.StraddleIcon.Open();
            }
        }*/

        foreach (var playerPlace in _playerPlaces)
        {
            var pokerPlayer = playerPlace.pokerPlayer;
            if (subscribeResp.straddlePlayerId != null && subscribeResp.straddlePlayerId.Equals(pokerPlayer.PlayerId))
            {
                pokerPlayer.BetAmount = subscribeResp.straddleChips;
                pokerPlayer.BuyInAmount = subscribeResp.straddlePlayerChips;
                pokerPlayer.StraddleIcon.Open();
            }
        }

        PokerPlayer ownPlayer = GetOwnPlayer();
        if (this.currentRoomData.isTournament && subscribeResp.isRebuyIn && ownPlayer == null)
        {
            btnRebuyinTournament.Open();
            RebuyRemainingTime(subscribeResp.remainRebuySec);
        }
        //foreach (PokerPlayer plr in GamePlayers)
        //{
        // if (!plr.PlayerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId) && plr.gameObject.activeInHierarchy)
        // {

        // if (UIManager.Instance.assetOfGame.SavedLoginData.isSuperPlayer)
        // {
        // GetSuperPlayerRequest();
        // }
        // }
        //}
        if (UIManager.Instance.MySuperPlayer && subscribeResp.gameId != "")
        {
            //Debug.Log("222222");
            GetSuperPlayerRequest();
        }

        /*foreach (PokerPlayer plr in GamePlayers)
        {
            if (!plr.PlayerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId) && plr.gameObject.activeInHierarchy)
            {

                if (UIManager.Instance.assetOfGame.SavedLoginData.isSuperPlayer)
                {
                    Debug.Log("222222");
                    GetSuperPlayerRequest();
                }
            }
        }*/
        UIManager.Instance.HideLoader();
    }

    private void OnPlayerInfoListReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnPlayerInfoListReceived  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        PlayerInfoList playerInfo = JsonUtility.FromJson<PlayerInfoList>(resp);

        if (playerInfo.roomId != currentRoomData.roomId && playerInfo.roomId.Length != 0)
            return;

        try
        {
            AllPokerPlayerInfo = playerInfo;
            GeneratePlayers(playerInfo);

            if (playerInfo.PlayerSidePot != null)
                SidePotAmountNew = playerInfo.PlayerSidePot;

            if (!HasJoin && !currentRoomData.isTournament)
            {
                /*for (int i = 0; i < GamePlayers.Length; i++)
                {
                    if (!GamePlayers[i].isActiveAndEnabled)
                    {
                        if (!currentRoomData.isTournament)
                        {
                            _playerPlaces[i].openSeatButton.gameObject.SetActive(true);
                            //_playerContainer.SetActiveOpenSeatButton(i, true);
                            //Seats[i].Open();
                        }
                    }
                }*/

                for (int i = 0; i < _playerPlaces.Count; i++)
                {
                    if (!_playerPlaces[i].pokerPlayer.isActiveAndEnabled)
                    {
                        if (!currentRoomData.isTournament)
                        {
                            _playerPlaces[i].openSeatButton.gameObject.SetActive(true);
                            //_playerContainer.SetActiveOpenSeatButton(i, true);
                            //Seats[i].Open();
                        }
                    }
                }
            }

            if (currentRoomData.maxPlayers == playerInfo.playerInfo.Count)
            {
                //Debug.Log("sub in ===");
                /*for (int i = 0; i < Seats.Length; i++)
                {
                    //Debug.Log("sub in ");
                    Seats[i].Close();
                }*/
                foreach (var orderPlayerPlace in _orderPlayerPlaces)
                {
                    orderPlayerPlace.openSeatButton.gameObject.SetActive(false);
                }

                //_playerContainer.SetActiveOpenSeatButtons(false);
            }

            foreach (var orderPlayerPlace in _orderPlayerPlaces)
            {
                if (orderPlayerPlace.pokerPlayer.playerInfo.id ==
                    UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
                {
                    orderPlayerPlace.SetIsBigPlayer(true);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("OnPlayerInfoReceived -> Exception  : " + e);
        }
    }

    private void OnTableCardsInfoReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        //		Debug.Log ("OnTableCardsInfoReceived  : " + packet.ToString ());

        if (!gameObject.activeSelf)
            return;

        JSON_Object cardsData = new JSON_Object(packet.ToString());

        if (cardsData.has("roomId") && cardsData.getString("roomId") != currentRoomData.roomId)
            return;

        tableCardsData = JsonUtility.FromJson<PokerTableCards>(cardsData.getString("cards"));
    }

    private void OnPlayerActionReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnPlayerActionReceived  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;
        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        //		string removedEvent = packet.RemoveEventName (true);
        PlayerAction action = JsonUtility.FromJson<PlayerAction>(resp);

        if (action.roomId != currentRoomData.roomId && action.roomId.Length != 0)
            return;

        showBetPanel = false;
        PerformPlayerAction(action);
        TotalTablePotAmount = action.totalTablePotAmount;
        /*if (HasJoinedRoom && ownPlayer != null && ownPlayer.status == PlayerStatus.Playing)
			SetPresetButtons(action);*/

        if (action.action.playerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
        {
            preBetButtonsPanel.ResetToggles(true);
        }
    }

    void OnPlayerLeft(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnPlayerLeft  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        JSONArray arr = new JSONArray(packet.ToString());

        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;
        JSON_Object playerObj = new JSON_Object(resp.ToString());

        if (playerObj.has("roomId") && playerObj.getString("roomId") != currentRoomData.roomId)
            return;

        RemovePlayer(playerObj.getString("playerId"));

        if (playerObj.getString("playerId") == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId && !currentRoomData.isTournament)
        {
            foreach (var playerPlace in _playerPlaces)
            {
                if (!playerPlace.pokerPlayer.isActiveAndEnabled)
                {
                    if (!currentRoomData.isTournament)
                    {
                        playerPlace.openSeatButton.gameObject.SetActive(true);
                        //_playerContainer.SetActiveOpenSeatButton(i, true);
                        //Seats[i].Open();
                    }
                }
            }

            /*for (int i = 0; i < GamePlayers.Length; i++)
            {
                if (!GamePlayers[i].isActiveAndEnabled)
                {
                    if (!currentRoomData.isTournament)
                    {
                        _playerPlaces[i].openSeatButton.gameObject.SetActive(true);
                        //_playerContainer.SetActiveOpenSeatButton(i, true);
                        //Seats[i].Open();
                    }
                }
            }*/
            _rebuyinButtons.Close();
            btnAddOnTournament.Close();
            btnRebuyinTournament.Close();
            preBetButtonsPanel.toggleSitOutNextHand.Close();
            preBetButtonsPanel.toggleSitOutNextBigBlind.Close();
            WaitForBigBlindCheckbox = false;
            btnStandup.gameObject.SetActive(false);
            //  btnHistoryOpen.gameObject.SetActive(false);
            btnHistoryOpen.interactable = false;
            //Debug.Log("**");
            btnClockOpen.interactable = false;
            //   btnClockOpen.Close();

            //if(!currentRoomData.isTournament)
            //{                
            //    HasJoinedRoom = false;
            //    StartCoroutine(NextScreen(0f));
            //}
        }
        else if (!HasJoin && !currentRoomData.isTournament)
        {
            foreach (var playerPlace in _playerPlaces)
            {
                if (!playerPlace.pokerPlayer.isActiveAndEnabled)
                {
                    if (!currentRoomData.isTournament)
                    {
                        playerPlace.openSeatButton.gameObject.SetActive(true);
                        //_playerContainer.SetActiveOpenSeatButton(i, true);
                        //Seats[i].Open();
                    }
                }
            }

            /*for (int i = 0; i < GamePlayers.Length; i++)
            {
                if (!GamePlayers[i].isActiveAndEnabled)
                {
                    if (!currentRoomData.isTournament)
                    {
                        _playerPlaces[i].openSeatButton.gameObject.SetActive(true);
                        //_playerContainer.SetActiveOpenSeatButton(i, true);
                        //Seats[i].Open();
                    }
                }
            }*/
        }
    }

    //private void OnPlayerLeft(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    //{
    //	Debug.Log("OnPlayerLeft  : " + packet.ToString());

    //	if (!gameObject.activeSelf)
    //		return;

    //	JSONArray arr = new JSONArray(packet.ToString());
    //	//		JSONArray 
    //	string Source;
    //	Source = arr.getString(arr.length() - 1);
    //	var resp = Source;
    //	JSON_Object playerObj = new JSON_Object(resp.ToString());

    //	if (playerObj.has("roomId") && playerObj.getString("roomId") != currentRoomData.roomId)
    //		return;

    //	RemovePlayer(playerObj.getString("playerId"));
    //	for (int j = 0; j < AllPokerPlayerInfo.playerInfo.Count; j++)
    //	{
    //		if (AllPokerPlayerInfo.playerInfo[j].id.Equals(playerObj.getString("playerId")))
    //		{
    //			Debug.Log("remove");
    //			AllPokerPlayerInfo.playerInfo.Remove(AllPokerPlayerInfo.playerInfo[j]);
    //		}
    //	}
    //	if (playerObj.getString("playerId") == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId && !currentRoomData.isTournament)
    //	{
    //		/*for (int i = 0; i < GamePlayers.Length; i++)
    //		{
    //			if (!GamePlayers[i].isActiveAndEnabled)
    //				Seats[i].Open();
    //		}*/
    //		btnRebuyin.Close();
    //		preBetButtonsPanel.toggleSitOutNextHand.Close();
    //		preBetButtonsPanel.toggleSitOutNextBigBlind.Close();
    //		WaitForBigBlindCheckbox = false;

    //		//if(!currentRoomData.isTournament)
    //		//{                
    //		//    HasJoinedRoom = false;
    //		//    StartCoroutine(NextScreen(0f));
    //		//}
    //	}

    //	if (currentRoomData.maxPlayers == AllPokerPlayerInfo.playerInfo.Count)
    //	{
    //		Debug.Log("sub in ===");
    //		for (int i = 0; i < Seats.Length; i++)
    //		{
    //			Seats[i].Close();
    //		}
    //	}
    //	else
    //	{
    //		Debug.Log("sub in =????  ");
    //		for (int i = 0; i < Seats.Length; i++)
    //		{
    //			if (!GamePlayers[i].gameObject.activeSelf)
    //			{
    //				Seats[i].Open();
    //			}
    //			else
    //			{
    //				Seats[i].Close();
    //			}
    //		}
    //	}
    //}

    private void OnRoundStarted(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        //Debug.Log ("PokerNewRoundStarted  : " + packet.ToString ());
    }

    private void OnGameStarted(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("PokerGameStarted  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        JSON_Object gameStartedObj = new JSON_Object(resp);

        if (gameStartedObj.has("roomId") && gameStartedObj.getString("roomId") != currentRoomData.roomId)
            return;

        if (gameStartedObj.has("gameId"))
        {
            Constants.Poker.GameId = gameStartedObj.getString("gameId");
            GameId = gameStartedObj.getString("gameId");
        }

        Constants.Poker.GameNumber = gameStartedObj.getString("gameNumber");
        showBetPanel = false;
        CurrentRound = PokerGameRound.Preflop;
        //if (UIManager.Instance.selectedGameType != GameType.texas) {
        GameName = Constants.Poker.GameNumber;
        btnExitToLobby.interactable = true;
        btnStandup.interactable = true;
        btnHistoryOpen.interactable = true;
        //Debug.Log("*****");
        btnClockOpen.interactable = true;
        waitingTextScreen.closePanel();
        freeze = false;

        if (gameStartedObj.has("previousGameId"))
        {
            PreviousGameId = gameStartedObj.getString("previousGameId");
        }

        if (gameStartedObj.has("previousGameNumber"))
        {
            PreviousGameNumber = gameStartedObj.getString("previousGameNumber");
        }


        /*  if (PreviousGameId.Equals(""))
          {
              btnHistoryOpen.interactable = false;
          }
          else
          {
              btnHistoryOpen.interactable = true;
          }
          */

        //} else {
        //	GameName.text = "";
        //}
    }

    private void OnPlayersCardsInfoReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        //		Debug.Log ("OnPlayersCardsInfoReceived  : " + packet.ToString ());

        //		StartCoroutine (DistributeCards ());
    }

    private void OnRoundComplete(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnRoundComplete  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        JSONArray arr = new JSONArray(packet.ToString());
        var resp = arr.getString(arr.length() - 1);
        RoundComplete round = JsonUtility.FromJson<RoundComplete>(resp);

        if (round.roomId != currentRoomData.roomId)
            return;

        preBetButtonsPanel.Close();

        if (round.cards.Count > 0)
        {
            StopCoroutine("DistributeTableCards");
            StartCoroutine(DistributeTableCards(round.cards));
        }

        if (round.cards.Count > 0 && round.extraCards.Count > 0)
        {
            //Debug.Log("in here");
            StopCoroutine("DistributeExtraTableCards");
            StartCoroutine(DistributeExtraTableCards(round.cards, round.extraCards, round.extraCardsPosition));
        }

        //StartCoroutine(DisplaySidePotAmount(round));
        CurrentRound = round.roundStarted.ToEnum<PokerGameRound>();

        for (int i = 0; i < round.cards.Count; i++)
        {
            TableCards[i].SetAlpha(2f);
            //Debug.Log("----i----");
        }

        showBetPanel = false;
        Bet.IsPreCallAnySelected = Bet.IsPreCheckSelected = Bet.IsPreFoldSelected = Bet.IsPreCallSelected = false;
        Bet.Close();
        preBetButtonsPanel.Close();
        if (!freeze)
        {
            StartCoroutine(ShowPotAfterSomeTime(round.PlayerSidePot));
        }
    }

    public bool freeze = false;

    private void OnGameFinished(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        if (!gameObject.activeSelf)
            return;

        JSONArray arr = new JSONArray(packet.ToString());
        var resp = arr.getString(arr.length() - 1);
        PokerGameWinner wnr = JsonUtility.FromJson<PokerGameWinner>(resp);
        currentTurnEffect.gameObject.GetComponent<Image>().enabled = false;
        if (wnr.roomId != currentRoomData.roomId && wnr.roomId.Length != 0)
            return;

        Debug.Log("OnGameFinished  : " + packet.ToString());
        Bet.Close();
        preBetButtonsPanel.Close();
        freeze = true;
        StartCoroutine(DisplayWinningAnimation(wnr));

        Bet.IsPreCallAnySelected = Bet.IsPreCallSelected = Bet.IsPreCheckSelected = Bet.IsPreFoldSelected = false;
        Bet.PreCallSelectedAmount = 0;
        //Debug.Log("****");
        btnClockOpen.interactable = false;
        //Debug.Log(".");
        PreviousGameId = wnr.previousGameId;
        PreviousGameNumber = wnr.previousGameNumber;
        /*foreach (PokerPlayer plr in GamePlayers)
        {
            if (plr.AllinObj.activeSelf)
            {
                plr.AllinObj.SetActive(false);
            }
            if (plr.FoldObj.activeSelf)
            {
                plr.FoldObj.SetActive(false);
            }
        }*/

        foreach (var playerPlace in _playerPlaces)
        {
            var pokerPlayer = playerPlace.pokerPlayer;
            pokerPlayer.ResetObjAnimation();
        }
    }

    private void OnPokerResetGame(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        if (!gameObject.activeSelf)
            return;
        try
        {
            ResetGame resetGame = JsonUtility.FromJson<ResetGame>(Utility.Instance.GetPacketString(packet));
            freeze = false;
            if (resetGame.roomId != currentRoomData.roomId && resetGame.roomId.Length != 0)
                return;

            //		UIManager.Instance.historyPanel.OnPokerResetGame();
            DestroyInstantiatedObjects();
            ResetTableCards();
            tableCardsListData.Clear();
            SidePotAmountNew = new PlayerSidePot();
            TotalTablePotAmount = 0;
            SidePot1 = 0;
            SidePot2 = 0;
            MainPot1 = 0;
            MainPot2 = 0;
            preBetButtonsPanel.ResetToggles(false);
            preBetButtonsPanel.Close();
            btnStraddleCards.Close();
            btnStraddleCardsCheck.Close();
            btntwiceCards.Close();
            currentTurnEffect.gameObject.GetComponent<Image>().enabled = false;
            //		ResetSidePots();
            //		Debug.Log("--------- Reset Game---------- " + packet.ToString());
            //		HistoryManager.GetInstance ().ResetHistory ();
        }
        catch (System.Exception e)
        {
        }
    }

    private void OnPlayerHandInfoReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        //print("OnPlayerHandInfoReceived  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;
        //		var resp = JSON.Parse (packet.ToString());
        /*JSONArray arr = new JSONArray(packet.ToString());

		string Source;
		Source = arr.getString (arr.length()-1);
		var resp = Source;

		PokerHandStrength handStrength = JsonUtility.FromJson<PokerHandStrength>(resp);

		PokerPlayer ownPlayer = GetOwnPlayer();
		if (ownPlayer != null)
		{
			//ownPlayer.SetStrengthMeter(handStrength.message);
		}*/
    }

    private void OnTurnTimerRecieved(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] arg)
    {
        if (!gameObject.activeSelf)
            return;

        Debug.Log("---------- OnTurnTimerRecieved ---------- " + packet.ToString());

        try
        {
            JSONArray arr = new JSONArray(packet.ToString());

            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            OnTurnTimer onTurnTimerCall = JsonUtility.FromJson<OnTurnTimer>(resp);

            if (onTurnTimerCall.roomId != currentRoomData.roomId && onTurnTimerCall.roomId.Length != 0)
                return;

            _bankTimer = (int) onTurnTimerCall.bankTimer; /// Bank Timer Logic
            PokerPlayer plr = GetPlayerById(onTurnTimerCall.playerId);

            if (plr == null)
                return;

            plr.TurnPlayer.Open();
            StartCoroutine(OnNextTurnHighlightsPlayerEffect(onTurnTimerCall.playerId));
            /*#if UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE || UNITY_EDITOR
                        StartCoroutine(OnNextTurnHighlightsPlayerEffect(onTurnTimerCall.playerId));
            #endif*/
            if (!btnExitToLobby.interactable && !btnStandup.interactable)
            {
                btnExitToLobby.interactable = true;
                btnStandup.interactable = true;
            }
            //float temp = onTurnTimerCall.timer - 1f;

            //plr.TurnPlayer.fillAmount = 1f - (onTurnTimerCall.timer / onTurnTimerCall.maxTimer);
            plr.StartTimer(onTurnTimerCall.timer, onTurnTimerCall.maxTimer);
            if (plr.PlayerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
            {
                if (waitingTextScreen.gameObject.activeInHierarchy)
                {
                    waitingTextScreen.closePanel();
                }

                if (onTurnTimerCall.timer <= 4)
                {
                    UIManager.Instance.SoundManager.AttentionSoundOnce();
                }

                if (!showBetPanel /* || !Bet.isActiveAndEnabled*/)
                {
                    Debug.Log("---------- OnTurnTimerRecieved ---------- " + packet.ToString());
                    showBetPanel = true;
                    Bet.OpenBetPanelTurn(onTurnTimerCall.buttonAction.callAmount, onTurnTimerCall.buttonAction.betAmount, onTurnTimerCall.buttonAction.minRaise, onTurnTimerCall.buttonAction.maxRaise, onTurnTimerCall.buttonAction.allInAmount,
                        onTurnTimerCall.buttonAction.call, onTurnTimerCall.buttonAction.bet, onTurnTimerCall.buttonAction.check, onTurnTimerCall.buttonAction.raise, onTurnTimerCall.buttonAction.allIn, onTurnTimerCall.timer,
                        onTurnTimerCall.isLimitGame);
                }
            }
            else
            {
                //plr.TurnPlayer.fillAmount =0;
                Bet.Close();
                PokerPlayer ownPlayer = GetOwnPlayer();
                if (ownPlayer != null && ownPlayer.status == PlayerStatus.Playing && !btnIAmBack.isActiveAndEnabled)
                {
                    preBetButtonsPanel.setOnData(onTurnTimerCall.buttonAction.callAny);
                }
            }

            if (HasJoinedRoom && !currentRoomData.isTournament && Bet.gameObject.activeInHierarchy == false && !WaitForBigBlindCheckbox)
            {
                preBetButtonsPanel.toggleSitOutNextBigBlind.Open();
                preBetButtonsPanel.toggleSitOutNextHand.Open();
            }
            else
            {
                preBetButtonsPanel.toggleSitOutNextBigBlind.Close();
                preBetButtonsPanel.toggleSitOutNextHand.Close();
            }

            //			if(GetOwnPlayer ().cards == null)
            //			{
            //				GetPlayerCards();
            //			}
        }
        catch (System.Exception e)
        {
            //			UIManager.Instance.DisplayMessagePanel (Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    //	void GetPlayerCards()
    //	{
    //		UIManager.Instance.SocketGamemanager.PlayersCards ( (socket, packet, args) => {
    //			Debug.Log ("GetPlayerCards  : " + packet.ToString ());
    //			JSONArray arr = new JSONArray(packet.ToString ());
    //			string Source;
    //			Source = arr.getString (arr.length()-1);
    //			var resp = Source;
    //			PokerEventResponse<OwnCards> GetPlayerCardsRes = JsonUtility.FromJson <PokerEventResponse<OwnCards>>(resp);
    //
    //			if (GetPlayerCardsRes.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
    //			{
    //				GetOwnPlayer ().cards = GetPlayerCardsRes.result.cards;
    //				GetOwnPlayer ().Card1.DisplayCardWithoutAnimation (GetOwnPlayer ().cards[0]);
    //				GetOwnPlayer ().Card2.DisplayCardWithoutAnimation (GetOwnPlayer ().cards[1]);
    //			}
    //		});
    //	}

    private void OnRevertPointReceived(Socket scoket, Packet packet, params object[] args)
    {
        UIManager.Instance.HideLoader();

        if (!gameObject.activeSelf)
            return;
        Debug.Log("OnRevertPointReceived : " + packet.ToString());
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            var resp = arr.getString(arr.length() - 1);

            PokerGameWinner.Wnr wnr = JsonUtility.FromJson<PokerGameWinner.Wnr>(resp);

            if (wnr.roomId != currentRoomData.roomId && wnr.roomId.Length != 0)
                return;

            StartCoroutine(DisplayRevertPointAnimation(wnr));
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnRevertPointFoldedReceived(Socket scoket, Packet packet, params object[] args)
    {
        UIManager.Instance.HideLoader();

        if (!gameObject.activeSelf)
            return;
        Debug.Log("OnRevertPointFoldedReceived : " + packet.ToString());
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            var resp = arr.getString(arr.length() - 1);
            PokerGameWinner.Wnr wnr = JsonUtility.FromJson<PokerGameWinner.Wnr>(resp);

            if (wnr.roomId != currentRoomData.roomId && wnr.roomId.Length != 0)
                return;

            StartCoroutine(DisplayRevertPointFoldedAnimation(wnr));
            TotalTablePotAmount = 0;
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnGameStartWaitReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnGameStartWaitReceived : " + packet.ToString());
    }

    private void OnSngTournamentFinishedReceived(Socket scoket, Packet packet, params object[] args)
    {
        UIManager.Instance.HideLoader();

        if (!gameObject.activeSelf)
            return;

        Debug.Log("OnSngTournamentFinishedReceived : " + packet.ToString());
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            SngTournamentFinishedData RegularTournamentFinishedDataResp = JsonUtility.FromJson<SngTournamentFinishedData>(resp);

            if (RegularTournamentFinishedDataResp.roomId != currentRoomData.roomId && RegularTournamentFinishedDataResp.roomId.Length != 0)
                return;

            UIManager.Instance.TournamentWinnerPanel.SNGTournamentData(RegularTournamentFinishedDataResp.winners);
            //UIManager.Instance.GameScreeen.Close();
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnSwitchRoomReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnSwitchRoomReceived : " + packet.ToString());
        UIManager.Instance.HideLoader();
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;
            RegularTournament resp = JsonUtility.FromJson<RegularTournament>(resp1);

            if (resp.roomId != currentRoomData.roomId && resp.roomId.Length != 0)
            {
                print(resp.roomId + " == " + currentRoomData.roomId);
                return;
            }

            RemoveAllPlayersFromTable();

            if (resp.roomId != "")
            {
                print("StartCoroutine (SwitchTable");
                StartCoroutine(SwitchTable(resp.newRoomId));
            }
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnRegularTournamentFinishedReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnRegularTournamentFinishedReceived : " + packet.ToString());
        UIManager.Instance.HideLoader();

        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            SngTournamentFinishedData RegularTournamentFinishedDataResp = JsonUtility.FromJson<SngTournamentFinishedData>(resp);

            if (RegularTournamentFinishedDataResp.roomId != currentRoomData.roomId && RegularTournamentFinishedDataResp.roomId.Length != 0)
                return;

            UIManager.Instance.HidePopup();
            btnRebuyinTournament.Close();
            UIManager.Instance.TournamentWinnerPanel.RegularTournamentData(RegularTournamentFinishedDataResp.winners);
            //  UIManager.Instance.GameScreeen.Close();
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnBreakReceived(Socket scoket, Packet packet, params object[] args)
    {
        //Debug.Log("OnBreakReceived : " + packet.ToString());
        UIManager.Instance.HideLoader();

        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            BreakTimerData BreakTimerDataResp = JsonUtility.FromJson<BreakTimerData>(resp);

            if (BreakTimerDataResp.roomId != currentRoomData.roomId && BreakTimerDataResp.roomId.Length != 0)
                return;

            TournamentBreakTableMessage = "";
            BreakTimerPanel.Close();
            BreakTimerPanel.SetmethodandTime(BreakTimerDataResp.timer);
            UIManager.Instance.HidePopup();
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
            BreakTimerPanel.Close();
        }
    }

    private void OnTournamentTableBreakMessage(Socket scoket, Packet packet, params object[] args)
    {
        UIManager.Instance.HideLoader();

        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            TournamentTableBreakMessage tournamentTableBreakMessage = JsonUtility.FromJson<TournamentTableBreakMessage>(resp);

            if (tournamentTableBreakMessage.roomId != currentRoomData.roomId && tournamentTableBreakMessage.roomId.Length != 0)
                return;

            UIManager.Instance.DisplayMessagePanel(tournamentTableBreakMessage.message);
            TournamentBreakTableMessage = tournamentTableBreakMessage.message;
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
            BreakTimerPanel.Close();
        }
    }

    private void OnBanPlayerTournament(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnBanPlayerTournament : " + packet.ToString());

        UIManager.Instance.HideLoader();
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            BanPlayerTournamentData BanPlayerTournamentDataResp = JsonUtility.FromJson<BanPlayerTournamentData>(resp);

            if (BanPlayerTournamentDataResp.roomId != currentRoomData.roomId && BanPlayerTournamentDataResp.roomId.Length != 0)
                return;

            if (BanPlayerTournamentDataResp.banPlayerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
            {
                string Msg = "You have " + BanPlayerTournamentDataResp.message;
                UIManager.Instance.DisplayMessagePanel(Msg, OnCancelTournamentDone);
            }
            else
            {
                string Msg = BanPlayerTournamentDataResp.banPlayerName + " has " + BanPlayerTournamentDataResp.message;
                //UIManager.Instance.DisplayMessagePanelOnly(Msg);
                StartCoroutine(popClose(2f, Msg));
            }
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnTableBalanceNotice(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnTableBalanceNotice : " + packet.ToString());

        UIManager.Instance.HideLoader();
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            TableBalanceNoticeResp OnTableBalanceNoticeResp = JsonUtility.FromJson<TableBalanceNoticeResp>(resp);

            if (OnTableBalanceNoticeResp.roomId != currentRoomData.roomId && OnTableBalanceNoticeResp.roomId.Length != 0)
                return;

            if (OnTableBalanceNoticeResp.isVisible)
            {
                int activeplayers = 0;
                /*foreach (PokerPlayer plr in GamePlayers)
                {
                    if (plr.gameObject.activeInHierarchy)
                    {
                        activeplayers++;
                    }
                }*/

                foreach (var playerPlace in _playerPlaces)
                {
                    if (playerPlace.pokerPlayer.gameObject.activeInHierarchy)
                    {
                        activeplayers++;
                    }
                }


                if (activeplayers <= 1)
                {
                    waitingTextScreen.SetData(OnTableBalanceNoticeResp.message);
                }
                else
                {
                    waitingTextScreen.closePanel();
                }
            }
            else
            {
                waitingTextScreen.closePanel();
            }
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnRoomDeleted(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnRoomDeleted : " + packet.ToString());

        UIManager.Instance.HideLoader();
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            CancelTournamentData OnCancelTournamentResp = JsonUtility.FromJson<CancelTournamentData>(resp);

            if (OnCancelTournamentResp.roomId != currentRoomData.roomId && OnCancelTournamentResp.roomId.Length != 0)
                return;
            UIManager.Instance.DisplayMessagePanel(OnCancelTournamentResp.message, OnCancelTournamentDone);
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnCancelTournament(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnCancelTournament : " + packet.ToString());

        UIManager.Instance.HideLoader();
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            CancelTournamentData OnCancelTournamentResp = JsonUtility.FromJson<CancelTournamentData>(resp);

            if (OnCancelTournamentResp.roomId != currentRoomData.roomId && OnCancelTournamentResp.roomId.Length != 0)
                return;
            UIManager.Instance.DisplayMessagePanel(OnCancelTournamentResp.message, OnCancelTournamentDone);
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnAddOnTimer(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnAddOnTimer : " + packet.ToString());

        UIManager.Instance.HideLoader();
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            AddOnTimeFinished AddOnTimeFinishedResp = JsonUtility.FromJson<AddOnTimeFinished>(resp);

            if (AddOnTimeFinishedResp.roomId != currentRoomData.roomId && AddOnTimeFinishedResp.roomId.Length != 0)
                return;

            if (AddOnTimeFinishedResp.eligiblePlayers.Contains(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId) && AddOnTimeFinishedResp.eligiblePlayers.Count > 0)
            {
                btnAddOnTournament.gameObject.SetActive(AddOnTimeFinishedResp.tournamentAddon);
                txtAddOnTimer.gameObject.SetActive(AddOnTimeFinishedResp.tournamentAddon);
                string remaingAddOnTimes = string.Format("{0:D2}:{1:D2}:{2:D2}", TimeSpan.FromSeconds(AddOnTimeFinishedResp.remaingAddOnTime).Hours, TimeSpan.FromSeconds(AddOnTimeFinishedResp.remaingAddOnTime).Minutes,
                    TimeSpan.FromSeconds(AddOnTimeFinishedResp.remaingAddOnTime).Seconds);
                txtAddOnTimer.text = remaingAddOnTimes + " Left";
            }
            else
            {
                btnAddOnTournament.gameObject.SetActive(false);
                txtAddOnTimer.gameObject.SetActive(false);
            }
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnbountyTournament(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("TournamentBounty : " + packet.ToString());

        UIManager.Instance.HideLoader();
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            BlindLevelRaised BlindLevelRaisedResp = JsonUtility.FromJson<BlindLevelRaised>(resp);

            if (BlindLevelRaisedResp.roomId != currentRoomData.roomId && BlindLevelRaisedResp.roomId.Length != 0)
                return;

            StopCoroutine("bountyTournamentDisplay");
            StartCoroutine(bountyTournamentDisplay(BlindLevelRaisedResp));
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnBlindLevelRaised(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnBlindLevelRaised : " + packet.ToString());

        UIManager.Instance.HideLoader();
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            BlindLevelRaised BlindLevelRaisedResp = JsonUtility.FromJson<BlindLevelRaised>(resp);

            if (BlindLevelRaisedResp.roomId != currentRoomData.roomId && BlindLevelRaisedResp.roomId.Length != 0)
                return;

            StopCoroutine("BLindValueRaised");
            StartCoroutine(BLindValueRaised(BlindLevelRaisedResp));
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnBlindLevelsReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnBlindLevelsReceived : " + packet.ToString());

        UIManager.Instance.HideLoader();
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            OnBlindLevelsData OnBlindLevelsDataResp = JsonUtility.FromJson<OnBlindLevelsData>(resp);

            if (OnBlindLevelsDataResp.roomId != currentRoomData.roomId && OnBlindLevelsDataResp.roomId.Length != 0)
                return;

            txtBlindAmountforTournamentTimer.text = "";
            txtBlindAmountforTournament.text = "";
            txtplayersPlayingTournament.text = "";
            SetBlindLevelData(OnBlindLevelsDataResp);
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void SetBlindLevelData(OnBlindLevelsData blindLevelData)
    {
        try
        {
            if (currentRoomData.isTournament && blindLevelData.current.smallBlind > 0)
            {
                string ShowMessage = "";
                BlindLevelRank = "#" + blindLevelData.current.level;
                BreakLevelRank = blindLevelData.breakLevel;
                ShowMessage = "BLINDS  : " + blindLevelData.current.smallBlind + "/" + blindLevelData.current.bigBlind
                              + " (" + +blindLevelData.next.smallBlind + "/" + blindLevelData.next.bigBlind + ")";

                txtBlindAmountforTournament.text = ShowMessage;
                System.TimeSpan t = System.TimeSpan.FromSeconds(blindLevelData.remain);
                txtBlindAmountforTournamentTimer.text = "NEXT BL : " + string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
                tournamentPlayersNumbers = blindLevelData.players;
            }
        }
        catch (Exception e)
        {
        }
    }

    private void OnGameFinishedPlayersCardsReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnGameFinishedPlayersCardsReceived : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            PlayerCardsAll onPlayerCardsResp = JsonUtility.FromJson<PlayerCardsAll>(resp);

            if (onPlayerCardsResp.roomId != currentRoomData.roomId && onPlayerCardsResp.roomId.Length != 0)
                return;

            PokerPlayer ownPlr = GetOwnPlayer();

            //			if (ownPlr != null && ownPlr.status == PlayerStatus.Fold) {
            //				return;
            //			}

            for (int i = 0; i < onPlayerCardsResp.playersCards.Length; i++)
            {
                PokerPlayer plr = GetPlayerById(onPlayerCardsResp.playersCards[i].playerId);
                if (plr != null)
                {
                    if (!plr.PlayerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
                    {
                        plr.cards = onPlayerCardsResp.playersCards[i].cards;

                        plr.CloseAllHiddenCards();

                        plr.Card1.Open();
                        plr.Card2.Open();

                        plr.Card1.PlayAnimation(plr.cards[0]);
                        plr.Card2.PlayAnimation(plr.cards[1]);

                        if (currentRoomData.pokerGameType == PokerGameType.omaha.ToString())
                        {
                            plr.Card3.Open();
                            plr.Card4.Open();

                            plr.Card3.PlayAnimation(plr.cards[2]);
                            plr.Card4.PlayAnimation(plr.cards[3]);
                        }
                        else if (currentRoomData.pokerGameType == PokerGameType.PLO5.ToString())
                        {
                            plr.Card3.Open();
                            plr.Card4.Open();
                            plr.Card5.Open();

                            plr.Card3.PlayAnimation(plr.cards[2]);
                            plr.Card4.PlayAnimation(plr.cards[3]);
                            plr.Card5.PlayAnimation(plr.cards[4]);
                        }
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnPlayerCardsReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnPlayerCardsReceived : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            PlayerCards onPlayerCardsResp = JsonUtility.FromJson<PlayerCards>(resp);

            if (onPlayerCardsResp.roomId != currentRoomData.roomId && onPlayerCardsResp.roomId.Length != 0)
                return;

            PokerPlayer plr = GetOwnPlayer();
            //			Debug.Log ("onPlayerCardsResp " + onPlayerCardsResp.playerId);
            //			Debug.Log ("plr " + plr.PlayerId);
            if (plr != null && onPlayerCardsResp.playerId.Equals(plr.PlayerId) && onPlayerCardsResp.cards != null)
            {
                Debug.Log("OnPlayerCardsReceived");

                plr.cards = onPlayerCardsResp.cards;

                plr.CloseAllHiddenCards();

                plr.Card1.Open();
                plr.Card2.Open();

                if (onPlayerCardsResp.flipAnimation)
                {
                    plr.Card1.PlayAnimation(plr.cards[0]);
                    plr.Card2.PlayAnimation(plr.cards[1]);
                }
                else
                {
                    plr.Card1.DisplayCardWithoutAnimation(plr.cards[0]);
                    plr.Card2.DisplayCardWithoutAnimation(plr.cards[1]);
                }

                if (currentRoomData.pokerGameType == PokerGameType.omaha.ToString())
                {
                    plr.Card3.Open();
                    plr.Card4.Open();

                    if (onPlayerCardsResp.flipAnimation)
                    {
                        plr.Card3.PlayAnimation(plr.cards[2]);
                        plr.Card4.PlayAnimation(plr.cards[3]);
                    }
                    else
                    {
                        plr.Card3.DisplayCardWithoutAnimation(plr.cards[2]);
                        plr.Card4.DisplayCardWithoutAnimation(plr.cards[3]);
                    }
                }

                if (currentRoomData.pokerGameType == PokerGameType.PLO5.ToString())
                {
                    plr.Card3.Open();
                    plr.Card4.Open();
                    plr.Card5.Open();

                    if (onPlayerCardsResp.flipAnimation)
                    {
                        plr.Card3.PlayAnimation(plr.cards[2]);
                        plr.Card4.PlayAnimation(plr.cards[3]);
                        plr.Card5.PlayAnimation(plr.cards[4]);
                    }
                    else
                    {
                        plr.Card3.DisplayCardWithoutAnimation(plr.cards[2]);
                        plr.Card4.DisplayCardWithoutAnimation(plr.cards[3]);
                        plr.Card5.DisplayCardWithoutAnimation(plr.cards[4]);
                    }
                }

                if (onPlayerCardsResp.showFoldedCards)
                {
                    print("alpha => ");
                    plr.Card1.SetAlpha(Constants.Constants.foldedCardsAlpha);
                    plr.Card2.SetAlpha(Constants.Constants.foldedCardsAlpha);
                    plr.Card3.SetAlpha(Constants.Constants.foldedCardsAlpha);
                    plr.Card4.SetAlpha(Constants.Constants.foldedCardsAlpha);
                    plr.Card5.SetAlpha(Constants.Constants.foldedCardsAlpha);
                }
                else
                {
                    print("else alpha => ");
                    plr.Card1.SetAlpha(2);
                    plr.Card2.SetAlpha(2);
                    plr.Card3.SetAlpha(2);
                    plr.Card4.SetAlpha(2);
                    plr.Card5.SetAlpha(2);
                }
            }
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnSubscibePlayersCardsReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnSubscibePlayersCardsReceived : " + packet.ToString());
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            PlayerCardsAll onPlayerCardsResp = JsonUtility.FromJson<PlayerCardsAll>(resp);

            if (onPlayerCardsResp.roomId != currentRoomData.roomId && onPlayerCardsResp.roomId.Length != 0)
                return;

            for (int i = 0; i < onPlayerCardsResp.playersCards.Length; i++)
            {
                PokerPlayer plr = GetPlayerById(onPlayerCardsResp.playersCards[i].playerId);
                if (plr != null)
                {
                    plr.status = PlayerStatus.Playing;
                    plr.playerInfo.status = PlayerStatus.Playing.ToString();
                    plr.cards = new List<string>();
                    plr.ResetImageAllHiddenCards();
                    plr.HiddenCard1.Open();
                    plr.HiddenCard2.Open();
                    if (currentRoomData.pokerGameType == PokerGameType.omaha.ToString())
                    {
                        plr.HiddenCard3.Open();
                        plr.HiddenCard4.Open();
                    }

                    if (currentRoomData.pokerGameType == PokerGameType.PLO5.ToString())
                    {
                        plr.HiddenCard3.Open();
                        plr.HiddenCard4.Open();
                        plr.HiddenCard5.Open();
                    }

                    plr.CloseAllOpenCards();
                }
            }
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnDisplayShowCardButtonReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnDisplayShowCardButtonReceived : " + packet.ToString());
        if (!gameObject.activeSelf)
            return;
        try
        {
            DisplayShowCardResult displayShowCardResult = JsonUtility.FromJson<DisplayShowCardResult>(Utility.Instance.GetPacketString(packet));

            if (displayShowCardResult.roomId != currentRoomData.roomId && displayShowCardResult.roomId.Length != 0)
                return;

            foreach (string playerId in displayShowCardResult.playerIdList)
            {
                if (playerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
                {
                    PreviousGameId = displayShowCardResult.gameId;
                    btnShowCards.Open();
                    btnShowCards.interactable = true;
                    Invoke("HideShowCard", displayShowCardResult.buttonActiveTime);
                    break;
                }
            }
        }
        catch (System.Exception e)
        {
            print(e);
        }
    }

    private void OnShowCardResultReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnShowCardResultReceived : " + packet.ToString());
        if (!gameObject.activeSelf)
            return;
        try
        {
            PlayerCards playerCards = JsonUtility.FromJson<PlayerCards>(Utility.Instance.GetPacketString(packet));

            if (playerCards.roomId != currentRoomData.roomId && playerCards.roomId.Length != 0)
                return;

            PokerPlayer pokerPlayer = GetPlayerById(playerCards.playerId);
            if (pokerPlayer != null)
            {
                pokerPlayer.OpenShowCards(playerCards.cards, playerCards.cardActiveTime);
            }
        }
        catch (System.Exception e)
        {
        }
    }

    private void OnGamePopupNotificationReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnGamePopNotificationReceived : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        try
        {
            GamePopupNotificationResponse notification = JsonUtility.FromJson<GamePopupNotificationResponse>(Utility.Instance.GetPacketString(packet));

            if (notification.roomId != currentRoomData.roomId && notification.roomId.Length != 0)
                return;

            UIManager.Instance.DisplayMessagePanel(notification.message);
        }
        catch (System.Exception e)
        {
        }
    }

    private void OnReBuyIn(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnReBuyIn : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        OnReBuyInResult result = JsonUtility.FromJson<OnReBuyInResult>(Utility.Instance.GetPacketString(packet));

        try
        {
            if (result.roomId != currentRoomData.roomId && result.roomId.Length != 0)
                return;
            /*
            if (result.playerId != UIManager.Instance.assetOfGame.SavedLoginData.PlayerId && result.playerId.Length != 0)
            {
                UIManager.Instance.DisplayLoader("Wait for other Players!!");
            }*/

            if (result.playerId != UIManager.Instance.assetOfGame.SavedLoginData.PlayerId && result.playerId.Length != 0)
            {
                UIManager.Instance.HideLoader();
                return;
            }


            UIManager.Instance.SocketGameManager.GetReBuyInChips(result.roomId, result.tournamentId, (socket2, packet2, args2) =>
            {
                Debug.Log(Constants.PokerEvents.GetReBuyInChips + " Response : " + packet2.ToString());

                PokerEventResponse<GetReBuyInChipsResponse> response = JsonUtility.FromJson<PokerEventResponse<GetReBuyInChipsResponse>>(Utility.Instance.GetPacketString(packet2));

                //Do you want to ReBuy 5000 from 10 chips?
                btnRebuyinTournament.Open();
                RebuyRemainingTime(result.remainRebuySec);
                UIManager.Instance.DisplayRebuyinConfirmationPanel("Do you want to Rebuy " /*+ response.result.buyInChips + " from " */ + response.result.buyIn + " chips?", "Rebuy", "Cancel", () =>
                {
                    UIManager.Instance.DisplayLoader("Please wait...");
                    UIManager.Instance.RebuyInMessagePanel.btnAffirmativeAction.interactable = false;
                    UIManager.Instance.SocketGameManager.PlayerReBuyIn(currentRoomData.roomId, result.tournamentId, (socket3, packet3, args3) =>
                    {
                        Debug.Log(Constants.PokerEvents.PlayerReBuyIn + " Response : " + packet3.ToString());
                        UIManager.Instance.HidePopup();
                        JSONArray arr = new JSONArray(packet3.ToString());
                        string Source;
                        Source = arr.getString(arr.length() - 1);
                        var resp1 = Source;
                        PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);

                        if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                        {
                            UIManager.Instance.HidePopup();
                            btnRebuyinTournament.Close();
                            UIManager.Instance.HideLoader();
                        }
                        else
                        {
                            UIManager.Instance.DisplayMessagePanel(resp.message, null);
                            UIManager.Instance.HideLoader();
                        }
                    });
                }, () =>
                {
                    UIManager.Instance.SocketGameManager.DeclinedReBuyIn(currentRoomData.roomId, result.tournamentId, (socket3, packet3, args3) =>
                    {
                        Debug.Log(Constants.PokerEvents.DeclinedReBuyIn + " Response : " + packet3.ToString());
                        UIManager.Instance.HidePopup();
                    });
                    UIManager.Instance.HideLoader();
                    //UIManager.Instance.HidePopup();
                });
            });
        }
        catch (System.Exception e)
        {
        }
    }

    /*private void OnReBuyIn(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnReBuyIn  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        OnReBuyInResult result = JsonUtility.FromJson<OnReBuyInResult>(Utility.Instance.GetPacketString(packet));

        try
        {
            if (result.roomId != currentRoomData.roomId && result.roomId.Length != 0)
                return;

            UIManager.Instance.SocketGameManager.GetReBuyInChips(result.roomId, result.tournamentId, (socket2, packet2, args2) =>
            {
                PokerEventResponse<GetReBuyInChipsResponse> response = JsonUtility.FromJson<PokerEventResponse<GetReBuyInChipsResponse>>(Utility.Instance.GetPacketString(packet));

                Debug.Log();
                UIManager.Instance.SocketGameManager.PlayerAddChips(selectedBuyinAmount, (socket, packet, args) =>

                    UIManager.Instance.DisplayRebuyinConfirmationPanel(LocalizationManager.GetTranslation("Dynamic/Do you want to Rebuy") + "  " + response.result.buyIn + " " + LocalizationManager.GetTranslation("Chips"), "Rebuy", "Cancel", () =>
                {
                    UIManager.Instance.SocketGameManager.PlayerReBuyIn(currentRoomData.roomId, result.tournamentId, (socket3, packet3, args3) =>
                    {
                        Debug.Log("here");
                        Debug.Log("PlayerReBuyIn response: " + packet3.ToString());
                        UIManager.Instance.HidePopup();
                    });
                }, () =>
                {
                    UIManager.Instance.HidePopup();
                });
            });
        }
        catch (System.Exception e)
        {

        }
    }
    */
    private void OnCloseReBuyIn(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnCloseReBuyIn  : " + packet.ToString());
        UIManager.Instance.HidePopup();
        UIManager.Instance.HideLoader();
    }

    private void OnTournamentPrize(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnTournamentPrize : " + packet.ToString());

        UIManager.Instance.HideLoader();
        if (!gameObject.activeSelf)
            return;
        try
        {
            OnTournamentPrizeResponse response = JsonUtility.FromJson<OnTournamentPrizeResponse>(Utility.Instance.GetPacketString(packet));

            if (response.roomId != currentRoomData.roomId && response.roomId.Length != 0)
                return;

            tournamentLoserPanel.SetData(response);
        }
        catch (System.Exception e)
        {
            Debug.Log("Error at !! OnTournamentPrize");
        }
    }

    private void OnOpenBuyInPanelReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnOpenBuyInPanelReceived : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        try
        {
            OpenBuyInPanelResult openBuyInPanel = JsonUtility.FromJson<OpenBuyInPanelResult>(Utility.Instance.GetPacketString(packet));

            if (openBuyInPanel.roomId != currentRoomData.roomId && openBuyInPanel.roomId.Length != 0)
                return;

            GetPlayerReBuyInChips();
        }
        catch (System.Exception e)
        {
        }
    }

    private void OnsuperPlayerDataReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnsuperPlayerDataReceived : " + packet.ToString());
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            superPlayerCard onSuperPlayerCardsResp = JsonUtility.FromJson<superPlayerCard>(Utility.Instance.GetPacketString(packet));

            if (onSuperPlayerCardsResp.roomId != currentRoomData.roomId && onSuperPlayerCardsResp.roomId.Length != 0)
                return;

            StartCoroutine(superplayercatdsopen(onSuperPlayerCardsResp));

            if (onSuperPlayerCardsResp.tableCards.Count > 0)
            {
                StopCoroutine("DistributeSuperPlayerTableCards");
                StartCoroutine(DistributeSuperPlayerTableCards(onSuperPlayerCardsResp.tableCards));
            }
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("exception  : " + e + " " + stackTrace.GetFrame(0).GetMethod().Name);
        }
    }

    private void OnPlayersCardsDistributionReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnPlayersCardsDistributionReceived : " + packet.ToString());
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            PlayerCardsAll onPlayerCardsResp = JsonUtility.FromJson<PlayerCardsAll>(resp);

            if (onPlayerCardsResp.roomId != currentRoomData.roomId && onPlayerCardsResp.roomId.Length != 0)
                return;

            for (int i = 0; i < onPlayerCardsResp.playersCards.Length; i++)
            {
                PokerPlayer plr = GetPlayerById(onPlayerCardsResp.playersCards[i].playerId);
                if (plr != null)
                {
                    plr.status = PlayerStatus.Playing;
                    plr.playerInfo.status = PlayerStatus.Playing.ToString();
                    plr.cards = new List<string>();
                    plr.cards = onPlayerCardsResp.playersCards[i].cards;

                    plr.ResetImageAllHiddenCards();
                    plr.CloseAllHiddenCards();
                    plr.CloseAllOpenCards();
                }
            }

            StartCoroutine(DistributeCards(onPlayerCardsResp.playersCards));
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("exception  : " + e + " " + stackTrace.GetFrame(0).GetMethod().Name);
        }
    }

    private void OnGameBootReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnGameBootReceived : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            GameBoot onGameBootResp = JsonUtility.FromJson<GameBoot>(resp);

            if (onGameBootResp.roomId != currentRoomData.roomId && onGameBootResp.roomId.Length != 0)
                return;

            TotalTablePotAmount = onGameBootResp.totalTablePotAmount;
            /*foreach (PokerPlayer plr in GamePlayers)
            {
                if (plr.PlayerId != null)
                {
                    plr.isDealer = plr.PlayerId.Equals(onGameBootResp.dealerPlayerId);
                    plr.Dealer.gameObject.SetActive(plr.isDealer);
                    if (onGameBootResp.smallBlindPlayerId.Equals(plr.PlayerId))
                    {
                        plr.BetAmount = onGameBootResp.smallBlindChips;
                        plr.BuyInAmount = onGameBootResp.smallBlindPlayerChips;
                    }
                    if (onGameBootResp.bigBlindPlayerId.Equals(plr.PlayerId))
                    {
                        plr.BetAmount = onGameBootResp.bigBlindChips;
                        plr.BuyInAmount = onGameBootResp.bigBlindPlayerChips;
                    }
                    if (onGameBootResp.straddlePlayerId != null && onGameBootResp.straddlePlayerId.Equals(plr.PlayerId))
                    {
                        plr.BetAmount = onGameBootResp.straddleChips;
                        plr.BuyInAmount = onGameBootResp.straddlePlayerChips;
                        plr.StraddleIcon.Open();
                    }
                }
            }*/

            foreach (var playerPlace in _playerPlaces)
            {
                var pokerPlayer = playerPlace.pokerPlayer;
                if (pokerPlayer.PlayerId != null)
                {
                    pokerPlayer.isDealer = pokerPlayer.PlayerId.Equals(onGameBootResp.dealerPlayerId);
                    pokerPlayer.Dealer.gameObject.SetActive(pokerPlayer.isDealer);
                    if (onGameBootResp.smallBlindPlayerId.Equals(pokerPlayer.PlayerId))
                    {
                        pokerPlayer.BetAmount = onGameBootResp.smallBlindChips;
                        pokerPlayer.BuyInAmount = onGameBootResp.smallBlindPlayerChips;
                    }

                    if (onGameBootResp.bigBlindPlayerId.Equals(pokerPlayer.PlayerId))
                    {
                        pokerPlayer.BetAmount = onGameBootResp.bigBlindChips;
                        pokerPlayer.BuyInAmount = onGameBootResp.bigBlindPlayerChips;
                    }

                    if (onGameBootResp.straddlePlayerId != null && onGameBootResp.straddlePlayerId.Equals(pokerPlayer.PlayerId))
                    {
                        pokerPlayer.BetAmount = onGameBootResp.straddleChips;
                        pokerPlayer.BuyInAmount = onGameBootResp.straddlePlayerChips;
                        pokerPlayer.StraddleIcon.Open();
                    }
                }
            }

            foreach (GameBoot.BlindPlayer bigBlindPlayerData in onGameBootResp.bigBlindPlayerList)
            {
                PokerPlayer pokerPlayer = GetPlayerById(bigBlindPlayerData.playerId);
                pokerPlayer.BetAmount = bigBlindPlayerData.chips;
                pokerPlayer.BuyInAmount = bigBlindPlayerData.playerChips;

                if (bigBlindPlayerData.playerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
                {
                    WaitForBigBlindCheckbox = false;
                }
            }
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnIAmBackReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnIAmBackReceived : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            IAmBack onIAmBackResp = JsonUtility.FromJson<IAmBack>(resp);

            if (onIAmBackResp.roomId != currentRoomData.roomId && onIAmBackResp.roomId.Length != 0)
                return;

            PokerPlayer plr = GetPlayerById(onIAmBackResp.playerId);
            if (plr != null)
            {
                print("onIAmBackResp.status: " + onIAmBackResp.status);
                if (onIAmBackResp.status)
                {
                    if (onIAmBackResp.playerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
                    {
                        btnIAmBack.gameObject.SetActive(true);

                        //#if UNITY_ANDROID || UNITY_IOS || UNITY_WEBGL || UNITY_EDITOR
                        IAmBackRemainingTime(onIAmBackResp.dueSeconds, onIAmBackResp.currentTime);
                        //#endif

                        BuyInAmountPanel.Close();
                        Bet.Close();
                        preBetButtonsPanel.Close();
                        preBetButtonsPanel.toggleSitOutNextHand.Close();
                        preBetButtonsPanel.toggleSitOutNextBigBlind.Close();
                    }

                    plr.canvasGroup.alpha = 0.4f;

                    if (onIAmBackResp.waitingGameChips > 0)
                    {
                        plr.BuyInAmount = onIAmBackResp.waitingGameChips;
                    }
                }
                else
                {
                    if (onIAmBackResp.playerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
                    {
                        Bet.Close();
                        btnIAmBack.gameObject.SetActive(false);
                        toggleSitOutNextHand.isOn = false;
                        toggleSitOutNextBigBlind.isOn = false;
                    }

                    plr.canvasGroup.alpha = 1f;
                }
            }
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    private void OnChatMessageReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OnChatMessageReceived : " + packet.ToString());

        UIManager.Instance.HideLoader();
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            Chatdata OnChatdataResp = JsonUtility.FromJson<Chatdata>(resp);

            if (OnChatdataResp.roomId != currentRoomData.roomId && OnChatdataResp.roomId.Length != 0)
                return;

            if (OnChatdataResp.roomId.Equals(Constants.Poker.TableId))
            {
                /*
                if (!chatPanel.activeSelf && HasJoin)
                {
                    chatPanel.SetActive(true);
#if UNITY_WEBGL
                    EnglishLang.Open();
                    HebrewLang.Open();
                    inputfieldChat.Open();
                    inputfieldChatHebrew.Close();
                    //inputfieldChat.Close();
                    //inputfieldChatHebrew.Open();

#else
                    EnglishLang.Close();
                    HebrewLang.Close();
                    inputfieldChat.Open();
                    inputfieldChatHebrew.Close();
#endif
                }
                */
                PokerPlayer plr = GetPlayerById(OnChatdataResp.playerId);
                if (plr != null)
                {
                    IsLetterValid(OnChatdataResp.message, OnChatdataResp.playerId);
                    plr.showChatBubble(OnChatdataResp.playerId, OnChatdataResp.message);
                }

                string senderName = "";
                if (OnChatdataResp.playerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
                    senderName = "You";
                else
                    senderName = GetPlayerNameById(OnChatdataResp.playerId);

                txtChatPanel.text += senderName + " : " + plr.txtChatMessage.text.ToString();
                //                txtChatPanel.text += senderName + ": " + OnChatdataResp.message + "\n";

                Debug.Log("length : " + plr.txtChatMessage.text.Length);
                if (plr.txtChatMessage.text.Length > 75)
                {
                    plr.txtChatMessage.text.Substring(0, 75);
                    //Debug.Log("====== " + plr.txtChatMessage.text.Substring(0, 75));
                    plr.txtChatMessage.text = plr.txtChatMessage.text.Substring(0, 75);
                }

                StartCoroutine("ForceScrollDownChat");
                //Text ChatText = Instantiate(TxtChatPrefab) as Text;
                //ChatText.text = "<b>" + GetPlayerNameById(OnChatdataResp.playerId) + "</b>" + " : " + "<i>" + OnChatdataResp.message + "</i>";
                //ChatText.transform.SetParent(ChatParent, false);
                //StartCoroutine("ForceScrollDownChat");

                //Text ChatText = Instantiate(TxtChatPrefab) as Text;
                //string senderName = "";
                //if (OnChatdataResp.playerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
                //    senderName = "You";
                //else
                //    senderName = GetPlayerNameById(OnChatdataResp.playerId);

                //ChatText.text = senderName + ": " + OnChatdataResp.message + "\n";
                //ChatText.transform.SetParent(ChatParent, false);
                //StartCoroutine("ForceScrollDownChat");
            }
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
        }
    }

    #endregion

    #region SocketEvent_callbacks

    public void OnSubscribeRoomDone(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnSubscribeRoomDone event received  : " + packet.ToString());

        if (currentRoomData.maxPlayers == AllPokerPlayerInfo.playerInfo.Count)
        {
            Debug.Log("sub in ===");
            /*for (int i = 0; i < Seats.Length; i++)
            {
                Seats[i].Close();
            }*/
            foreach (var orderPlayerPlace in _orderPlayerPlaces)
            {
                orderPlayerPlace.openSeatButton.gameObject.SetActive(false);
            }

            //_playerContainer.SetActiveOpenSeatButtons(false);
        }

        //  return; //OnSubscribeRoom broadcast integrated. dont need to run following lines.
        Debug.Log("OnSubscribeRoomDone received  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        showBetPanel = false;
        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;
        PokerEventResponse<PokerGameHistory> subscribeResp = JsonUtility.FromJson<PokerEventResponse<PokerGameHistory>>(resp);

        if (subscribeResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.HideLoader();
            TurnTime = subscribeResp.result.turnTime;
            currentRoomData.smallBlind = subscribeResp.result.smallBlindChips;
            currentRoomData.bigBlind = subscribeResp.result.bigBlindChips;

            if (subscribeResp.result == null)
                return;

            Constants.Poker.roomId = subscribeResp.result.roomId;
            Constants.Poker.TableId = subscribeResp.result.roomId;
            GameId = subscribeResp.result.gameId;
            Constants.Poker.TableNumber = subscribeResp.result.tableNumber;
            GameName = subscribeResp.result.tableNumber;
            PotAmountValue = subscribeResp.result.gameHistory.potAmount;
            PreviousGameNumber = subscribeResp.result.previousGameNumber;
            PreviousGameId = subscribeResp.result.previousGameId;
            OldPlayerBuyIn = subscribeResp.result.oldPlayerBuyIn;

            //			PotAmountValue = subscribeResp.result.gameHistory.potAmount.ConvertDoubleDecimals ();
            SidePotAmountNew = subscribeResp.result.gameHistory.PlayerSidePot;
            //			txtPotAmount.text = "pot = " + PotAmountValue.ToString ();
            //txtTableId.text = "Table ID : " + Constants.Poker.TableNumber;   
            TotalTablePotAmount = subscribeResp.result.gameHistory.totalTablePotAmount;

            if (!string.IsNullOrEmpty(subscribeResp.result.gameHistory.currentRound))
                CurrentRound = subscribeResp.result.gameHistory.currentRound.ToEnum<PokerGameRound>();

            for (int i = 0; i < subscribeResp.result.gameHistory.cards.Count; i++)
            {
                //				Debug.Log ("-----456------");
                this.TableCards[i].gameObject.SetActive(true);
                this.TableCards[i].DisplayCardWithoutAnimation(subscribeResp.result.gameHistory.cards[i]);
            }

            if (subscribeResp.result.gameStatus.Equals("Running"))
            {
                btnClockOpen.interactable = true;
            }
            else
            {
                btnClockOpen.interactable = false;
            }

            preBetButtonsPanel.SetPrebetOptions(subscribeResp.result.defaultButtons);

            if (subscribeResp.result.roomIdChanged)
            {
                UIManager.Instance.tableManager.ReplaceMiniTableRoomId(currentRoomData.roomId, subscribeResp.result.roomId);
                this.currentRoomData.roomId = subscribeResp.result.roomId;
                SubscribeEventInvoke();
                UIManager.Instance.tableManager.ReSubscribeMiniTables(this.currentRoomData.roomId);
            }
        }
        else
        {
            string message = subscribeResp.message;
            if (string.IsNullOrEmpty(message))
            {
                UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong, () =>
                {
                    this.Close();
                    UIManager.Instance.tableManager.RemoveMiniTable(this.currentRoomData.roomId);
                    UIManager.Instance.LobbyPanelNew.Open(); // LobbyScreeen not used more
                    UIManager.Instance.HidePopup();
                });
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(message, () =>
                {
                    this.Close();
                    UIManager.Instance.tableManager.RemoveMiniTable(this.currentRoomData.roomId);
                    UIManager.Instance.LobbyPanelNew.Open(); // LobbyScreeen not used more
                    UIManager.Instance.HidePopup();
                });
            }
        }

        UIManager.Instance.HideLoader();
    }

    private void JoinRoomResponse(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        UIManager.Instance.HideLoader();
        Debug.Log("JoinRoomResponse  : " + packet.ToString());

        PokerEventResponse response = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet));
        if (response.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
        }
        else
        {
            UIManager.Instance.DisplayMessagePanel(response.message, null);
        }
    }

    /*private void OnGetPlayerReBuyInChipsDone(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnGetPlayerReBuyInChipsDone  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        UIManager.Instance.HideLoader();
        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        PokerEventResponse<GetPlayerReBuyInChips> rebuyChipsRes = JsonUtility.FromJson<PokerEventResponse<GetPlayerReBuyInChips>>(resp);

        if (rebuyChipsRes.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            if (rebuyChipsRes.result.maxBuyIn <= 0)
            {
                rebuyChipsRes.result.maxBuyIn = UIManager.Instance.assetOfGame.SavedLoginData.chips;
            }
            BuyInAmountPanel.OpenReBuyinPanel(rebuyChipsRes.result.minBuyIn, rebuyChipsRes.result.maxBuyIn, rebuyChipsRes.result.playerChips);
        }
        else
        {
            //if (rebuyChipsRes.message.Equals("Player Not found!"))
            //{
            //	UIManager.Instance.DisplayMessagePanel(LocalizationManager.GetTranslation("Dynamic/Player Not found!"), null);
            //}
            //else if (rebuyChipsRes.message.Equals("Room Not found!"))
            //{
            //	UIManager.Instance.DisplayMessagePanel(LocalizationManager.GetTranslation("Dynamic/Room Not found!"), null);
            //}
            //else if (rebuyChipsRes.message.Equals("You have already requested for the chips."))
            //{
            //	UIManager.Instance.DisplayMessagePanel(LocalizationManager.GetTranslation("Dynamic/You have already requested for the chips."), null);
            //}
            //else if (rebuyChipsRes.message.Equals("Player Have Low Chips"))
            //{
            //	UIManager.Instance.DisplayMessagePanel(LocalizationManager.GetTranslation("Dynamic/Player Have Low Chips"), null);
            //}
            //else if (rebuyChipsRes.message.Equals("Can't add more chips"))
            //{
            //	UIManager.Instance.DisplayMessagePanel(LocalizationManager.GetTranslation("Dynamic/Can't add more chips"), null);
            //}
            //else if (rebuyChipsRes.message.Equals("Player Re-BuyIn Chips."))
            //{
            //	UIManager.Instance.DisplayMessagePanel(LocalizationManager.GetTranslation("Dynamic/Player Re-BuyIn Chips."), null);
            //}

            if (!string.IsNullOrEmpty(LocalizationManager.GetTranslation("Dynamic/" + rebuyChipsRes.message)) && LocalizationManager.CurrentLanguage == "Mongolian")
            {
                UIManager.Instance.DisplayMessagePanel(LocalizationManager.GetTranslation("Dynamic/" + rebuyChipsRes.message), null);
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(rebuyChipsRes.message, null);
            }
        }
    }
    */
    private void OnGetPlayerReBuyInChipsDone(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log(Constants.PokerEvents.GetPlayerReBuyInChips + " Response : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        UIManager.Instance.HideLoader();
        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        PokerEventResponse<GetPlayerReBuyInChips> rebuyChipsRes = JsonUtility.FromJson<PokerEventResponse<GetPlayerReBuyInChips>>(resp);

        if (rebuyChipsRes.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            if (rebuyChipsRes.result.maxBuyIn <= 0)
            {
                rebuyChipsRes.result.maxBuyIn = UIManager.Instance.assetOfGame.SavedLoginData.chips;
            }

            BuyInAmountPanel.OpenReBuyinPanel(rebuyChipsRes.result.minBuyIn, rebuyChipsRes.result.maxBuyIn, rebuyChipsRes.result.playerChips);
        }
        else
        {
            UIManager.Instance.DisplayMessagePanel(rebuyChipsRes.message, null);
        }
    }

    private void OnShowOtherPlayersCardDone(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnShowOtherPlayersCardDone  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        UIManager.Instance.HideLoader();
        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;


        PokerEventResponse<PlayerCardsAll> onPlayerCardsResp = JsonUtility.FromJson<PokerEventResponse<PlayerCardsAll>>(resp);

        if (onPlayerCardsResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            if (onPlayerCardsResp.result.roomId != currentRoomData.roomId && onPlayerCardsResp.result.roomId.Length != 0)
                return;

            PokerPlayer ownPlr = GetOwnPlayer();

            //			if (ownPlr != null && ownPlr.status == PlayerStatus.Fold) {
            //				return;
            //			}
            for (int i = 0; i < onPlayerCardsResp.result.playersCards.Length; i++)
            {
                PokerPlayer plr = GetPlayerById(onPlayerCardsResp.result.playersCards[i].playerId);
                if (plr != null)
                {
                    plr.CloseAllHiddenCards();
                    if (onPlayerCardsResp.result.playersCards[i].cards.Count > 0)
                    {
                        plr.cards = onPlayerCardsResp.result.playersCards[i].cards;
                        plr.Card1.Open();
                        plr.Card2.Open();

                        plr.Card1.PlayAnimation(plr.cards[0]);
                        plr.Card1.SetAlpha(1);
                        plr.Card2.PlayAnimation(plr.cards[1]);
                        plr.Card2.SetAlpha(1);
                        print("alpha => ");
                        if (currentRoomData.pokerGameType == PokerGameType.omaha.ToString())
                        {
                            plr.Card3.Open();

                            plr.Card4.Open();

                            plr.Card3.PlayAnimation(plr.cards[2]);
                            plr.Card3.SetAlpha(1);
                            plr.Card4.PlayAnimation(plr.cards[3]);
                            plr.Card4.SetAlpha(1);
                            print("alpha => ");
                        }
                        else if (currentRoomData.pokerGameType == PokerGameType.PLO5.ToString())
                        {
                            plr.Card3.Open();
                            plr.Card4.Open();
                            plr.Card5.Open();

                            plr.Card3.PlayAnimation(plr.cards[2]);
                            plr.Card3.SetAlpha(1);
                            plr.Card4.PlayAnimation(plr.cards[3]);
                            plr.Card4.SetAlpha(1);
                            plr.Card5.PlayAnimation(plr.cards[4]);
                            plr.Card5.SetAlpha(1);
                            print("alpha => ");
                        }
                    }

                    if (onPlayerCardsResp.result.advanceTableCards.Count > 0)
                    {
                        tableCardsListData.Clear();
                        for (int j = 0; j < onPlayerCardsResp.result.tableCards.Count; j++)
                        {
                            tableCardsListData.Add(onPlayerCardsResp.result.tableCards[j].ToString());
                        }

                        StopCoroutine("DistributeSuperPlayerTableCards");
                        StartCoroutine(DistributeSuperPlayerTableCards(onPlayerCardsResp.result.advanceTableCards));
                    }
                }
            }
        }
        else
        {
            //UIManager.Instance.DisplayMessagePanel(onPlayerCardsResp.message, null);
        }
    }

    private void OnPlayerAddChipsDone(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnPlayerAddChipsDone  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        UIManager.Instance.HideLoader();
        //		JSONArray arr = new JSONArray (packet.ToString ());
        //		string Source;
        //		Source = arr.getString (arr.length () - 1);
        //		var resp1 = Source;
    }

    private void OnLeaveRoomDone(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        print("OnLeaveRoomDone  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        UIManager.Instance.HideLoader();
        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp1 = Source;
        PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);
        if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            StartCoroutine(NextScreen(0f));
            HasJoinedRoom = false;
        }
        else
        {
            UIManager.Instance.DisplayMessagePanel(resp.message, null);
        }
    }

    private void OnUnSubscribeRoomDone(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        print("OnUnSubscribeRoomDone  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        UIManager.Instance.HideLoader();
        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp1 = Source;
        PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);

        if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            if (UIManager.Instance.selectedGameType == GameType.cash)
            {
                StartCoroutine(NextScreen(0f));
                HasJoinedRoom = false;
            }
            else
            {
                if (Switchingtable)
                {
                    Switchingtable = false;
                }
                else
                {
                    StartCoroutine(NextScreen(0f));
                    HasJoinedRoom = false;
                }
            }
        }
        else
        {
            UIManager.Instance.DisplayMessagePanel(resp.message, null);
        }
    }

    #endregion

    #region PUBLIC_METHODS

    public void DisplayTime(double dueSeconds)
    {
        if (dueSeconds < 0)
        {
            dueSeconds = 0;
            txtIAmBackRemainingTime.text = "";
            if (btnIAmBack.gameObject.activeInHierarchy)
            {
                OnLobbyDone();
            }
        }
    }

    public void reBuyTorunamentButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.GetReBuyInChips(this.currentRoomData.roomId, this.currentRoomData.tournamentId, (socket2, packet2, args2) =>
        {
            Debug.Log(Constants.PokerEvents.GetReBuyInChips + " Response : " + packet2.ToString());

            PokerEventResponse<GetReBuyInChipsResponse> response = JsonUtility.FromJson<PokerEventResponse<GetReBuyInChipsResponse>>(Utility.Instance.GetPacketString(packet2));

            //Do you want to ReBuy 5000 from 10 chips?
            UIManager.Instance.DisplayRebuyinConfirmationPanel("Do you want to Rebuy " /*+ response.result.buyInChips + " from " */ + response.result.buyIn + " chips?", "Rebuy", "Cancel", () =>
            {
                UIManager.Instance.DisplayLoader("Please wait...");
                UIManager.Instance.RebuyInMessagePanel.btnAffirmativeAction.interactable = false;
                UIManager.Instance.SocketGameManager.PlayerReBuyIn(currentRoomData.roomId, currentRoomData.tournamentId, (socket3, packet3, args3) =>
                {
                    Debug.Log(Constants.PokerEvents.PlayerReBuyIn + " Response : " + packet3.ToString());
                    UIManager.Instance.HidePopup();
                    JSONArray arr = new JSONArray(packet3.ToString());
                    string Source;
                    Source = arr.getString(arr.length() - 1);
                    var resp1 = Source;
                    PokerEventResponse<ReBuyAcceptResult> resp = JsonUtility.FromJson<PokerEventResponse<ReBuyAcceptResult>>(resp1);
                    UIManager.Instance.HideLoader();
                    UIManager.Instance.RebuyInMessagePanel.btnAffirmativeAction.interactable = true;
                    if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                    {
                        GetRunningGameList(resp.result.roomId);
                        UIManager.Instance.HidePopup();
                        btnRebuyinTournament.Close();
                    }
                    else
                    {
                        UIManager.Instance.DisplayMessagePanel(resp.message, null);
                    }
                });
            }, () =>
            {
                UIManager.Instance.SocketGameManager.DeclinedReBuyIn(currentRoomData.roomId, currentRoomData.tournamentId, (socket3, packet3, args3) =>
                {
                    Debug.Log(Constants.PokerEvents.DeclinedReBuyIn + " Response : " + packet3.ToString());
                    UIManager.Instance.HidePopup();
                });
                //UIManager.Instance.HidePopup();
            });
        });
    }

    public void ChatValue()
    {
        string str = inputfieldChat.text;
        HebrewOwn(str);
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();

        /*string ss = "";
        for (int i = charArray.Length - 1; i >= 0; i--)
        {
            ss += charArray[i];
        }*/

        Array.Reverse(charArray);
        return new string(charArray);
    }

    public void HebrewOwn(string msg)
    {
        bool isHebrew = false;
        bool isFirstHebrew = false;
        bool isprevious = false;
        char[] separators = new char[] {' ', '.'};
        string example = "This is an example.";
        string str1 = msg;
        string pattern = @"[\p{IsHebrew} ]+";
        string newstat = "";
        string abc = "";
        string updatedStat = "";
        foreach (var word in str1.Split(separators, StringSplitOptions.RemoveEmptyEntries))
        {
            var hebrewMatchCollection = Regex.Matches(word.ToString(), pattern);
            string hebrewPart = string.Join(" ", hebrewMatchCollection.Cast<Match>().Select(m => m.Value));
            string tempLetter = Regex.Replace(word.ToString(), "[^a-zA-Z]", "");
            string tempNumber = Regex.Replace(word.ToString(), @"^(\+[0-9]{9})$", "");


            if (hebrewPart != "")
            {
                isFirstHebrew = true;
                abc = Reverse(word);
                updatedStat += " " + abc;
                isprevious = true;
            }
            else if (tempLetter != "")
            {
                isFirstHebrew = false;
                isHebrew = false;
                abc = word;
                updatedStat += abc + " ";
                isprevious = false;
            }
            else
            {
                if (isFirstHebrew)
                    abc = Reverse(word);
                else
                    abc = word;
            }

            if (isFirstHebrew)
            {
                newstat = abc + " " + newstat;
            }
            else
            {
                newstat += " " + abc;
            }

            chatMessage = newstat;
        }
    }

    public void straddleButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        StraddleAccept();
        //UIManager.Instance.DisplayConfirmationPanel("Are you want to Be straddle Player ?", StraddleAccept, StraddleeReject);
    }

    public void twoXButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.DisplayConfirmationPanel("Would you like to Run it Twice?", runItTwiceAccept, runItTwiceReject);
    }

    public void StraddleAccept()
    {
        UIManager.Instance.SocketGameManager.StraddleRequest(true, (socket, packet, args) =>
        {
            Debug.Log("RunItTwiceRequest  : " + packet.ToString());
            UIManager.Instance.HideLoader();
            //			JSONArray arr = new JSONArray(packet.ToString ());
            //
            //			var resp1 = arr.getString(0);
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;
            PokerEventResponse userPurchaseDataResp = JsonUtility.FromJson<PokerEventResponse>(resp1);
            if (userPurchaseDataResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                //UIManager.Instance.DisplayMessagePanel(userPurchaseDataResp.message);
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(userPurchaseDataResp.message);
            }
        });
    }

    public void StraddleeReject()
    {
        UIManager.Instance.SocketGameManager.StraddleRequest(false, (socket, packet, args) =>
        {
            Debug.Log("RunItTwiceRequest  : " + packet.ToString());

            UIManager.Instance.HideLoader();

            //			JSONArray arr = new JSONArray(packet.ToString ());
            //
            //			var resp1 = arr.getString(0);
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse userPurchaseDataResp = JsonUtility.FromJson<PokerEventResponse>(resp1);

            if (userPurchaseDataResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                //UIManager.Instance.DisplayMessagePanel(userPurchaseDataResp.message);
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(userPurchaseDataResp.message);
            }
        });
    }

    public void runItTwiceAccept()
    {
        UIManager.Instance.SocketGameManager.RunItTwiceRequest(true, (socket, packet, args) =>
        {
            Debug.Log("RunItTwiceRequest  : " + packet.ToString());

            UIManager.Instance.HideLoader();

            //			JSONArray arr = new JSONArray(packet.ToString ());
            //
            //			var resp1 = arr.getString(0);
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse userPurchaseDataResp = JsonUtility.FromJson<PokerEventResponse>(resp1);


            if (userPurchaseDataResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                //UIManager.Instance.DisplayMessagePanel(userPurchaseDataResp.message);
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(userPurchaseDataResp.message);
            }
        });
    }

    public void runItTwiceReject()
    {
        UIManager.Instance.SocketGameManager.RunItTwiceRequest(false, (socket, packet, args) =>
        {
            Debug.Log("RunItTwiceRequest  : " + packet.ToString());

            UIManager.Instance.HideLoader();

            //			JSONArray arr = new JSONArray(packet.ToString ());
            //
            //			var resp1 = arr.getString(0);
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse userPurchaseDataResp = JsonUtility.FromJson<PokerEventResponse>(resp1);


            if (userPurchaseDataResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                //UIManager.Instance.DisplayMessagePanel(userPurchaseDataResp.message);
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(userPurchaseDataResp.message);
            }
        });
    }

    public void GetSuperPlayerRequest()
    {
        UIManager.Instance.SocketGameManager.ShowOtherPlayersCard(OnShowOtherPlayersCardDone);
    }

    public void GetPlayerReBuyInChips()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.GetPlayerReBuyInChips(OnGetPlayerReBuyInChipsDone);
    }

    public void GetPlayerReBuyInMoney()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.GetPlayerReBuyInChips(OnGetPlayerReBuyInChipsDone);
    }

    public void PlayerAddChips()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.PlayerAddChips(1000, OnPlayerAddChipsDone);
    }

    public void AddOnForTournament()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.GetAddOnDetails(currentRoomData.roomId, currentRoomData.tournamentId, (socket2, packet2, args2) =>
        {
            Debug.Log(Constants.PokerEvents.GetReBuyInChips + " Response : " + packet2.ToString());

            PokerEventResponse<GetAddOnDetailsData> response = JsonUtility.FromJson<PokerEventResponse<GetAddOnDetailsData>>(Utility.Instance.GetPacketString(packet2));

            //Do you want to ReBuy 5000 from 10 chips?
            UIManager.Instance.DisplayConfirmationPanel("Do you want to Add On " /*+ response.result.buyInChips + " from " */ + response.result.buyIn + " chips?", "Add On", "Cancel", () =>
            {
                UIManager.Instance.SocketGameManager.BuyAddOnChips(currentRoomData.roomId, currentRoomData.tournamentId, (socket3, packet3, args3) =>
                {
                    Debug.Log(Constants.PokerEvents.BuyAddOnChips + " Response : " + packet3.ToString());
                    UIManager.Instance.HidePopup();
                    JSONArray arr = new JSONArray(packet3.ToString());
                    string Source;
                    Source = arr.getString(arr.length() - 1);
                    var resp1 = Source;
                    PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);

                    if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                    {
                        UIManager.Instance.HidePopup();
                        btnRebuyinTournament.Close();
                    }
                    else
                    {
                        UIManager.Instance.DisplayMessagePanel(resp.message, null);
                    }
                });
            }, () => { UIManager.Instance.HidePopup(); });
        });
    }

    public void LobbyButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.AbsolutePlayer)
        {
            BackButtonTap();
        }
        else
        {
            UIManager.Instance.SocketGameManager.CheckLeaveRoomEligibility((socket, packet, args) =>
            {
                print("CheckLeaveRoomEligibility 1 : " + packet.ToString());

                if (!gameObject.activeSelf)
                    return;

                PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet));

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    BackToLobbyConfirmationOpen();
                }
            });
        }

        //UIManager.Instance.DisplayConfirmationPanel(LocalizationManager.GetTranslation("Dynamic/Are you sure you want to leave Game?"), OnLobbyDone);
        //	UIManager.Instance.DisplayConfirmationPanel ("Are you sure you want to leave Game? ", OnLobbyDone);
    }

    public void BackToLobbyConfirmationOpen()
    {
        UIManager.Instance.DisplayConfirmationPanel("Are you sure you want to leave game?",
            "Yes", "No", () =>
            {
                UIManager.Instance.SoundManager.OnButtonClick();
                UIManager.Instance.SocketGameManager.CheckLeaveRoomEligibility((socket, packet, args) =>
                {
                    print("CheckLeaveRoomEligibility 2 : " + packet.ToString());

                    if (!gameObject.activeSelf)
                        return;

                    PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet));

                    if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                    {
                        OnLobbyDone();
                        UIManager.Instance.HidePopup();
                    }
                });
            }, () =>
            {
                UIManager.Instance.SoundManager.OnButtonClick();
                UIManager.Instance.HidePopup();
            });
    }

    public void BackButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.tableManager.DeselectAllTableSelection();
        StartCoroutine(NextScreen(0f));
    }

    public void OnCancelTournamentDone()
    {
        OnLobbyDone();
        UIManager.Instance.HidePopup();
    }

    public void OnLobbyDone()
    {
        UIManager.Instance.DisplayLoader("");
        UIManager.Instance.tableManager.DeselectAllTableSelection();
        UIManager.Instance.tableManager.RemoveMiniTable(currentRoomData.roomId);
        //if (UIManager.Instance.gameType == GameType.texas) {
        if (HasJoinedRoom)
        {
            UIManager.Instance.SocketGameManager.LeaveRoom(UIManager.Instance.gameType, OnLeaveRoomDone);
        }
        else
        {
            UIManager.Instance.SocketGameManager.UnSubscribeRoom(UIManager.Instance.gameType, OnUnSubscribeRoomDone);
        }

        /*}
        else 
        {
            if (this.gameObject.activeSelf) {
                UIManager.Instance.SocketGamemanager.UnSubscribeRoom (UIManager.Instance.gameType, OnUnSubscribeRoomDone);
                //StartCoroutine (NextScreen (1.5f));
                //HasJoinedRoom = false;
                //btnRebuyin.interactable = HasJoinedRoom;
            }
        }*/
    }

    public void OnStandupButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (!UIManager.Instance.AbsolutePlayer)
        {
            OnStandup(Constants.Poker.TableId);
        }
    }

    private void OnStandUpDone(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("StandUpDone Response : " + packet.ToString());
        /*if (!gameObject.activeSelf)
            return;
         */

        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp1 = Source;
        PokerEventResponse<StandUp> resp = JsonUtility.FromJson<PokerEventResponse<StandUp>>(resp1);
        if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            OldPlayerBuyIn = resp.result.oldPlayerBuyIn;
        }
        else
        {
            UIManager.Instance.DisplayMessagePanel(resp.message);
        }
    }

    public void OnCloseButtonTap()
    {
        UIManager.Instance.messagePanel.Close();
    }

    public void OnChatButtonEnding()
    {
        string chatTemp;
        string str = "";


#if UNITY_WEBGL || UNITY_EDITOR
        if (inputfieldChatHebrew.gameObject.activeSelf)
            str = inputfieldChatHebrew.text;
        else
            str = inputfieldChat.text;

        HebrewOwn(str);
#else
        str = inputfieldChat.text;
        print("before => " + str);
        HebrewOwn(str);
        print("after => " + chatMessage);

#endif
        print("after => " + chatMessage);
        if (!string.IsNullOrEmpty(chatMessage))
        {
            UIManager.Instance.SocketGameManager.SendChat(chatMessage, (socket, packet, args) =>
            {
                Debug.Log("SendChat  : " + packet.ToString());
                UIManager.Instance.HideLoader();
                try
                {
                    JSONArray arr = new JSONArray(packet.ToString());
                    string Source;
                    Source = arr.getString(arr.length() - 1);
                    var resp1 = Source;
                    PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);

                    if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                    {
                        //     testinglanguage.text = inputfieldChat.text;
                        inputfieldChat.text = "";
                        inputfieldChatHebrew.text = "";
                        //   OnChatButtonTap();
                        CloseChatPanel();
#if UNITY_WEBGL
                        chatMessage = inputfieldChatHebrew.text.ToString();
#else
                        chatMessage = inputfieldChat.text.ToString();
#endif
                    }
                    else
                    {
                        UIManager.Instance.DisplayMessagePanel(resp.message);
                    }
                }
                catch (System.Exception e)
                {
                    UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
                    Debug.LogError("exception  : " + e);
                }
            });
        }
    }

    void CloseChatPanel()
    {
        chatPanel.gameObject.SetActive(false);
    }

    public void HebrewTick()
    {
        inputfieldChat.Close();
        inputfieldChatHebrew.Open();
    }

    public void EnglishTick()
    {
        inputfieldChat.Open();
        inputfieldChatHebrew.Close();
    }

    public void OnSeatButtonTap(int SeatIndex)
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (currentRoomData.maxPlayers <= GetActivePlayerCount())
        {
            UIManager.Instance.DisplayMessagePanel("Maximum" + " " + currentRoomData.maxPlayers + " players are allowed to play on this table");
            return;
        }

        if (UIManager.Instance.assetOfGame.SavedLoginData.chips < currentRoomData.minBuyIn)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.Game.NotEnoughChipsToPlayThisTable);
        }
        else
        {
            if (currentRoomData.maxBuyIn <= 0)
            {
                currentRoomData.maxBuyIn = UIManager.Instance.assetOfGame.SavedLoginData.chips;
            }

            if (OldPlayerBuyIn > 0 && UIManager.Instance.assetOfGame.SavedLoginData.chips >= OldPlayerBuyIn)
            {
                UIManager.Instance.DisplayLoader("");
                UIManager.Instance.SocketGameManager.JoinRoom(currentRoomData.roomId, OldPlayerBuyIn, SeatIndex, JoinRoomResponse);
            }
            else
            {
                UIManager.Instance.SocketGameManager.GetBuyinsAndPlayerchips(currentRoomData.roomId, (socket, packet, args) =>
                {
                    print("GetBuyinsAndPlayerchips response: " + packet.ToString());
                    PokerEventResponse<GetBuyinsAndPlayerchipsResponse> response = JsonUtility.FromJson<PokerEventResponse<GetBuyinsAndPlayerchipsResponse>>(Utility.Instance.GetPacketString(packet));
                    if (response.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                    {
                        //BuyInAmountPanel.OpenBuyinPanel (currentRoomData.minBuyIn, currentRoomData.maxBuyIn, currentRoomData.gameLimit, SeatIndex);
                        UIManager.Instance.assetOfGame.SavedLoginData.chips = response.result.playerChips;
                        if (response.result.OldPlayerchipsBuyin > 0 && response.result.playerChips >= response.result.OldPlayerchipsBuyin)
                        {
                            UIManager.Instance.DisplayLoader("");
                            UIManager.Instance.SocketGameManager.JoinRoom(currentRoomData.roomId, response.result.OldPlayerchipsBuyin, SeatIndex, JoinRoomResponse);
                        }
                        else
                        {
                            BuyInAmountPanel.OpenBuyinPanel(response.result.minBuyIn, response.result.maxBuyIn, currentRoomData.gameLimit, SeatIndex);
                        }
                    }
                    else
                    {
                        UIManager.Instance.DisplayMessagePanel(response.message);
                    }
                });
            }
        }
    }

    public void OnIAmBackButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        btnIAmBack.gameObject.SetActive(false);
        UIManager.Instance.SocketGameManager.SendPlayerOnline((socket, packet, args) =>
        {
            Debug.Log("OnSendPlayerOnline  : " + packet.ToString());
            //			JSONArray arr = new JSONArray (packet.ToString ());
            //			string Source;
            //			Source = arr.getString (arr.length () - 1);
            //			var resp = Source;
            //			PokerEventResponse SendPlayerOnlineResp = JsonUtility.FromJson <PokerEventResponse> (resp);
        });
    }

    public void SetRoomDataAndPlay(RoomsListing.Room currentRoomData)
    {
        _currentRoomData.SetRoom(currentRoomData);

        _pockerRoomCustomization.SetData(_currentRoomData);

        //this.currentRoomData = currentRoomData;
        this.currentRoomData = Utility.Instance.GetNewRoomObjectClone(currentRoomData);

        Constants.Poker.TableId = currentRoomData.roomId;
        Game.Lobby.SetSocketNamespace = currentRoomData.namespaceString;

        UIManager.Instance.gameType = Utility.Instance.GetGameFormatFromString(currentRoomData.pokerGameFormat);
        UIManager.Instance.selectedGameType = Utility.Instance.GetGameFormatFromString(currentRoomData.pokerGameFormat);

        if (UIManager.Instance.IsMultipleTableAllowed && !UIManager.Instance.tableManager.playingTableList.Contains(currentRoomData.roomId))
        {
            UIManager.Instance.tableManager.DeselectAllTableSelection();
            UIManager.Instance.tableManager.AddMiniTable(currentRoomData);
        }

        UIManager.Instance.tableManager.DeselectAllTableSelection();
        UIManager.Instance.tableManager.HighlightMiniTable(currentRoomData.roomId);
        /*if (this.currentRoomData.isTournament)
        {
            if (this.currentRoomData.namespaceString.Contains("sng") || this.currentRoomData.namespaceString.Contains("SNG"))
            {
                imgTable.sprite = imgTables[5];
                currentBG.sprite = imgBackgrounds[5];
                mainBG.sprite = currentBG.sprite;
            }
            else
            {
                imgTable.sprite = imgTables[0];
                currentBG.sprite = imgBackgrounds[0];
                mainBG.sprite = currentBG.sprite;
            }
            imgTableLogoMain.sprite = imgTableLogo[0];
        }
        else
        {
            if (this.currentRoomData.isPasswordProtected)
            {
                imgTable.sprite = imgTables[1];
                currentBG.sprite = imgBackgrounds[1];
                mainBG.sprite = currentBG.sprite;
                imgTableLogoMain.sprite = imgTableLogo[1];
            }
            else
            {
                imgTableLogoMain.sprite = imgTableLogo[0];
                if (this.currentRoomData.pokerGameType == PokerGameType.texas.ToString())
                {
                    imgTable.sprite = imgTables[2];
                    currentBG.sprite = imgBackgrounds[2];
                    mainBG.sprite = currentBG.sprite;
                }
                else if (this.currentRoomData.pokerGameType == PokerGameType.omaha.ToString())
                {
                    imgTable.sprite = imgTables[3];
                    currentBG.sprite = imgBackgrounds[3];
                    mainBG.sprite = currentBG.sprite;
                }
                else if (this.currentRoomData.pokerGameType == PokerGameType.PLO5.ToString())
                {
                    imgTable.sprite = imgTables[4];
                    currentBG.sprite = imgBackgrounds[4];
                    mainBG.sprite = currentBG.sprite;
                }
                else
                {
                    imgTable.sprite = imgTables[4];
                    currentBG.sprite = imgBackgrounds[4];
                    mainBG.sprite = currentBG.sprite;
                }
                if (this.currentRoomData.bigBlind >= 10)
                {
                    imgTable.sprite = imgTables[1];
                    currentBG.sprite = imgBackgrounds[1];
                    mainBG.sprite = currentBG.sprite;
                }
            }
        }
        */
    }

    /*
    public void SetRoomDataAndPlay(string roomId, string namespaceString, string pokerGameType, string pokerGameFormat)
    {
        this.currentRoomData.roomId = roomId;
        this.currentRoomData.isTournament = true;
        this.currentRoomData.pokerGameType = pokerGameType;
        this.currentRoomData.pokerGameFormat = pokerGameFormat;
        this.currentRoomData.namespaceString = namespaceString;

        Constants.Poker.TableId = roomId;
        Game.Lobby.SetSocketNamespace = namespaceString;

        UIManager.Instance.gameType = Utility.Instance.GetGameFormatFromString(pokerGameFormat);
        UIManager.Instance.selectedGameType = Utility.Instance.GetGameFormatFromString(pokerGameFormat);
        if (this.currentRoomData.isTournament)
        {
            if (this.currentRoomData.namespaceString.Contains("sng") || this.currentRoomData.namespaceString.Contains("SNG"))
            {
                imgTable.sprite = imgTables[5];
                currentBG.sprite = imgBackgrounds[5];
                mainBG.sprite = currentBG.sprite;
            }
            else
            {
                imgTable.sprite = imgTables[0];
                currentBG.sprite = imgBackgrounds[0];
                mainBG.sprite = currentBG.sprite;
            }
            imgTableLogoMain.sprite = imgTableLogo[0];
        }
        else
        {
            if (this.currentRoomData.isPasswordProtected)
            {
                imgTable.sprite = imgTables[1];
                currentBG.sprite = imgBackgrounds[1];
                mainBG.sprite = currentBG.sprite;
                imgTableLogoMain.sprite = imgTableLogo[1];
            }
            else
            {
                imgTableLogoMain.sprite = imgTableLogo[0];
                if (this.currentRoomData.pokerGameType == PokerGameType.texas.ToString())
                {
                    imgTable.sprite = imgTables[2];
                    currentBG.sprite = imgBackgrounds[2];
                    mainBG.sprite = currentBG.sprite;
                }
                else if (this.currentRoomData.pokerGameType == PokerGameType.omaha.ToString())
                {
                    imgTable.sprite = imgTables[3];
                    currentBG.sprite = imgBackgrounds[3];
                    mainBG.sprite = currentBG.sprite;
                }
                else if (this.currentRoomData.pokerGameType == PokerGameType.PLO5.ToString())
                {
                    imgTable.sprite = imgTables[4];
                    currentBG.sprite = imgBackgrounds[4];
                    mainBG.sprite = currentBG.sprite;
                }
                else
                {
                    imgTable.sprite = imgTables[4];
                    currentBG.sprite = imgBackgrounds[4];
                    mainBG.sprite = currentBG.sprite;
                }

                if (this.currentRoomData.bigBlind >= 10)
                {
                    imgTable.sprite = imgTables[1];
                    currentBG.sprite = imgBackgrounds[1];
                    mainBG.sprite = currentBG.sprite;
                }
            }
        }
    }
    */
    public void OnSeatButtonTapWaitingPlayer(int SeatIndex)
    {
        if (UIManager.Instance.assetOfGame.SavedLoginData.chips < currentRoomData.minBuyIn)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.Game.NotEnoughChipsToPlayThisTable);
        }
        else
        {
            if (currentRoomData.maxBuyIn <= 0)
            {
                currentRoomData.maxBuyIn = UIManager.Instance.assetOfGame.SavedLoginData.chips;
            }

            BuyInAmountPanel.OpenWaitingPlayerBuyinPanel(currentRoomData.minBuyIn, currentRoomData.maxBuyIn, SeatIndex, true);
        }
    }

    public void OnShowCardButtonTap()
    {
        if (PreviousGameId == null || PreviousGameId == "")
            return;

        btnShowCards.interactable = false;
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.ShowMyCards(PreviousGameId, (socket, packet, args) =>
        {
            if (!gameObject.activeSelf)
                return;

            Debug.Log("ShowMyCardsResponse : " + packet.ToString());
            try
            {
                PokerEventResponse response = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet));
                if (JsonUtility.ToJson(response.result) == Constants.PokerAPI.KeyStatusSuccess)
                {
                    btnShowCards.interactable = true;
                    btnShowCards.Close();
                }
                else
                {
                }
            }
            catch (System.Exception e)
            {
                print(e);
            }
        });
    }

    public void GeneratePlayers(PlayerInfoList SpadePlayerData)
    {
        ////Following code added to prevent - (for non seated player) hide other player's cards on playerInfo broadcast (in running game)
        //#############################
        bool isOwnPlayerSeatedInList = false;
        foreach (PlayerInfoList.PlayerInfos plr in SpadePlayerData.playerInfo)
        {
            if (plr.id == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
            {
                isOwnPlayerSeatedInList = true;
                ResetSeatIndexForOwnPlayer(plr.seatIndex);
                //if (!btnHistoryOpen.gameObject.activeSelf)
                //btnHistoryOpen.Open();

                if (this.currentRoomData.isTournament)
                {
                    btnClockOpen.Close();
                    btnRankingOpen.Open();
                    btnHistoryOpen.Close();
                }
                else
                {
                    if (!btnClockOpen.gameObject.activeSelf)
                    {
                        btnClockOpen.Open();
                    }

                    btnRankingOpen.Open();
                    btnHistoryOpen.Open();
                }

                break;
            }
        }

        if (!HasJoin && isOwnPlayerSeatedInList)
            ResetData(!HasJoin);
        //#############################
        ////following line code replaced with upper section code
        //ResetData (!HasJoin);

        this.AllPokerPlayerInfo = SpadePlayerData;

        foreach (PlayerInfoList.PlayerInfos plr in SpadePlayerData.playerInfo)
        {
            int newSeatIndex = GetSeatIndexForPlayer(plr.seatIndex);
            var playerPlaceSeat = _playerPlaces[newSeatIndex];
            var pokerPlayer = playerPlaceSeat.pokerPlayer;

            if (!pokerPlayer.gameObject.activeSelf)
            {
                pokerPlayer.Open();
                //GamePlayers[newSeatIndex].status = plr.status.ToEnum<PlayerStatus>();
                if (plr.status.Equals("ReBuyIn") || plr.status.Equals("ReBuyWait"))
                {
                    pokerPlayer.status = PlayerStatus.Waiting;
                }
                else
                {
                    pokerPlayer.status = plr.status.ToEnum<PlayerStatus>();
                }

                var profilePic = plr.avatar;
                if (profilePic == -1)
                {
                    playerPlaceSeat.SetImage(plr.profileImage);
                }
                else
                {
                    playerPlaceSeat.SetAvatar(profilePic);
                }

                playerPlaceSeat.SetFlag(plr.flag);

                playerPlaceSeat.SetName(plr.username);
                if (UIManager.Instance.assetOfGame.SavedLoginData.isCash)
                    playerPlaceSeat.SetChip(plr.cash);
                else
                    playerPlaceSeat.SetChip(plr.chips);
                //pokerPlayer.ProfilePic.sprite = UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[profilePic];
                //pokerPlayer.GetProfilePicImage().Open();
                pokerPlayer.isDealer = SpadePlayerData.dealerPlayerId == plr.id;

                _playerPlaces[newSeatIndex].openSeatButton.gameObject.SetActive(false);
                //_playerContainer.SetActiveOpenSeatButton(newSeatIndex, false);
                //Seats[newSeatIndex].Close();

                if (plr.status.ToEnum<PlayerStatus>() != PlayerStatus.Waiting)
                {
                    if (plr.allIn)
                    {
                        pokerPlayer.status = PlayerStatus.Allin;
                        plr.status = PlayerStatus.Allin.ToString();
                    }

                    if (plr.folded)
                    {
                        pokerPlayer.status = PlayerStatus.Fold;
                        plr.status = PlayerStatus.Fold.ToString();
                    }

                    if (plr.status.Equals("ReBuyIn") || plr.status.Equals("ReBuyWait"))
                    {
                        pokerPlayer.status = PlayerStatus.Waiting;
                    }
                    else
                    {
                        pokerPlayer.status = plr.status.ToEnum<PlayerStatus>();
                    }
                }
            }
            else
            {
                if (plr.status.Equals("ReBuyIn") || plr.status.Equals("ReBuyWait"))
                {
                    pokerPlayer.status = PlayerStatus.Waiting;
                }
                else
                {
                    pokerPlayer.status = plr.status.ToEnum<PlayerStatus>();
                }
            }
            //GamePlayers [newSeatIndex].txtUsername.text = plr.username;

            pokerPlayer.txtUsername.text = Utility.Instance.GetShortenName(plr.username);

            if (plr.waitingGameChips > 0)
                pokerPlayer.BuyInAmount = plr.waitingGameChips;
            else
                pokerPlayer.BuyInAmount = plr.chips;

            pokerPlayer.BetAmount = plr.betAmount;
            pokerPlayer.playerInfo = plr;
            pokerPlayer.PlayerId = plr.id;
            pokerPlayer.twiceIcon.gameObject.SetActive(plr.runItTwice);
            /*if (plr.isSuperPlayer)
            {
                GamePlayers[newSeatIndex].DetailObj.sprite = GamePlayers[newSeatIndex].DetailObjsSprites[0];
            }
            else
            {
                GamePlayers[newSeatIndex].DetailObj.sprite = GamePlayers[newSeatIndex].DetailObjsSprites[0];
            }
*/

            if (plr.idealPlayer)
            {
                pokerPlayer.canvasGroup.alpha = 0.4f;
            }

            if (pokerPlayer.PlayerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
            {
                btnIAmBack.gameObject.SetActive(plr.idealPlayer);

                /*if (plr.status.ToEnum<PlayerStatus>().Equals(PlayerStatus.Playing))
                 {
                     SuperPLayerCheck(plr);
                 }*/

                if (plr.idealPlayer)
                {
                    //#if UNITY_ANDROID || UNITY_IOS || UNITY_WEBGL || UNITY_EDITOR
                    IAmBackRemainingTime(plr.dueSeconds, plr.currentTime);
                    //#endif
                    Bet.Close();
                    BuyInAmountPanel.Close();
                    preBetButtonsPanel.Close();
                    Bet.Close();
                }

                HasJoin = true;
                btnChat.interactable = HasJoinedRoom;
                if (!this.currentRoomData.isTournament)
                {
                    btnHistoryOpen.interactable = HasJoinedRoom;
                }

                //if (HasJoinedRoom && !currentRoomData.isTournament && Bet.gameObject.activeInHierarchy == false && plr.idealPlayer == false)
                //{                    
                //    preBetButtonsPanel.toggleSitOutNextBigBlind.Open();
                //    preBetButtonsPanel.toggleSitOutNextHand.Open();
                //}
                //else
                //{
                //    preBetButtonsPanel.toggleSitOutNextBigBlind.Close();
                //    preBetButtonsPanel.toggleSitOutNextHand.Close();
                //}
                if (plr.absolute)
                {
                    //Debug.Log("if");
                    UIManager.Instance.assetOfGame.SavedLoginData.isAbsolute = plr.absolute;
                    UIManager.Instance.AbsolutePlayer = plr.absolute;
                    //#if UNITY_ANDROID || UNITY_IOS || UNITY_WEBGL //||UNITY_EDITOR
                    ClockRemainingTime(plr.dueSeconds, plr.absoluteTimeCurrentTime);
                    //#endif
                    //ClockData = new clockResult();
                    //ClockData.startTime = plr.absoluteTimeSartTime;
                    //StoreTimeNow();
                }
                else
                {
                    //Debug.Log("else");
                    UIManager.Instance.assetOfGame.SavedLoginData.isAbsolute = false;
                    UIManager.Instance.AbsolutePlayer = false;
                }

                if (currentRoomData.isTournament == false)
                {
                    _rebuyinButtons.Open();
                    btnAddOnTournament.Close();
                    btnStandup.gameObject.SetActive(true);
                    //btnStandup.gameObject.SetActive(false);
                }
                else
                {
                    _rebuyinButtons.Close();
                    btnStandup.gameObject.SetActive(false);
                }

                if (!currentRoomData.isTournament && plr.waitForBigBlindData != null)
                {
                    WaitForBigBlindCheckbox = plr.waitForBigBlindData.waitForBigBlindCheckbox;
                    if (WaitForBigBlindCheckbox)
                        WaitForBigBlindCheckboxValue = plr.waitForBigBlindData.waitForBigBlindCheckboxValue;
                }

                /*   if (plr.isRebuyIn && currentRoomData.isTournament)
                   {
                       btnRebuyinTournament.Open();
                   }
                   else
                   {
                       btnRebuyinTournament.Close();
                   }*/
                //btnRebuyinTournament.Open();
            }

            if (HasJoin && !currentRoomData.isTournament)
            {
                foreach (var orderPlayerPlace in _orderPlayerPlaces)
                {
                    orderPlayerPlace.openSeatButton.gameObject.SetActive(!HasJoin);
                }

                //_playerContainer.SetActiveOpenSeatButtons(!HasJoin);
            }

            if (UIManager.Instance.selectedGameType == GameType.cash)
            {
                foreach (var playerPlace in _playerPlaces)
                {
                    if (playerPlace.pokerPlayer.gameObject.activeSelf)
                    {
                        playerPlace.openSeatButton.gameObject.SetActive(false);

                        //_playerContainer.SetActiveOpenSeatButton(k, false);
                    }
                }
            }
            else
            {
                foreach (var playerPlace in _playerPlaces)
                {
                    playerPlace.openSeatButton.gameObject.SetActive(false);
                }
            }


            /*for (int k = 0; k < _playerContainer.GetPlayerPlaceCount(); k++)
            {
                if (UIManager.Instance.selectedGameType == GameType.cash)
                {
                    if (GamePlayers[k].gameObject.activeSelf)
                    {
                        _playerPlaces[k].openSeatButton.gameObject.SetActive(false);

                        //_playerContainer.SetActiveOpenSeatButton(k, false);
                    }
                }
                else
                {
                    _playerPlaces[k].openSeatButton.gameObject.SetActive(false);

                    //_playerContainer.SetActiveOpenSeatButton(k, false);
                }
            }*/
            pokerPlayer.Dealer.gameObject.SetActive(SpadePlayerData.dealerPlayerId == plr.id);
        }
    }

    private void ResetSeatIndexForOwnPlayer(int ownPlayerSeatIndex)
    {
        if (_playerPlaces[0].pokerPlayer.PlayerId != UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
        {
            DestroyInstantiatedObjects();
        }

        var playerPlaceCount = _playerPlaces.Count;
        int count = ownPlayerSeatIndex;

        for (int i = 0; i < playerPlaceCount - ownPlayerSeatIndex; i++)
        {
            int newSeatIndex = count;
            _orderPlayerPlaces[i].SetOpenSeat(() => OnSeatButtonTap(newSeatIndex));

            //_playerContainer.InitOpenSeatButton(i, () => OnSeatButtonTap(newSeatIndex));
            count++;
        }

        count = 0;
        for (int i = playerPlaceCount - ownPlayerSeatIndex; i < playerPlaceCount; i++)
        {
            int newSeatIndex = count;
            _orderPlayerPlaces[i].SetOpenSeat(() => OnSeatButtonTap(newSeatIndex));

            //_playerContainer.InitOpenSeatButton(i, () => OnSeatButtonTap(newSeatIndex));
            count++;
        }
    }

    private void SetGameHistoryPlayerAction(PlayerAction.PA action)
    {
        if (action.action != null)
        {
            PokerPlayer plr = GetPlayerById(action.playerId);

            if (plr != null)
            {
                if (action.gameRound.ToEnum<PokerGameRound>() == CurrentRound)
                {
                    //					plr.BetAmount += action.betAmount;
                    //
                    //					plr.BetAmount -= action.betAmount;

                    plr.SetLastActionPerformed(action.action, action.betAmount, action.hasRaised);
                }

                if (action.action == PokerPlayerAction.Fold)
                {
                    //					plr.playerCanvasGroup.alpha = Constants.Poker.FoldCanvasAlpha;
                    plr.PlayerActionStatus.Open();
                    plr.PlayerActionStatus.txtAction.text = PlayerStatus.Fold.ToString();
                    plr.playerInfo.status = PlayerStatus.Fold.ToString();
                }
                else if (action.action == PokerPlayerAction.Allin)
                {
                    plr.status = PlayerStatus.Allin;
                    plr.playerInfo.status = PlayerStatus.Allin.ToString();
                }

                //				HistoryManager.GetInstance ().AddHistory (plr.PlayerId, plr.playerInfo.username, action.gameRound.ToEnum<PokerGameRound> (), action.betAmount, plr.BetAmount, (PokerPlayerAction)action.action, action.hasRaised);
            }
        }
    }

    public void OnPreviousHandButtonTap(string GameId)
    {
        UIManager.Instance.DisplayLoader("");
        UIManager.Instance.SocketGameManager.GameHistory(GameId, (socket, packet, args) =>
        {
            print("GameHistory : " + packet.ToString());

            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            PokerEventResponse<FullGameHistoryResult> gameHistoryResponse = JsonUtility.FromJson<PokerEventResponse<FullGameHistoryResult>>(arr.getString(0));
            if (gameHistoryResponse.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                fullGameHistoryPanel.SetData(gameHistoryResponse.result.gameHistory);
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(gameHistoryResponse.message, null);
            }
        });
    }

    public void WaitForBigBlindCheckboxTap()
    {
        UIManager.Instance.SocketGameManager.WaitForBigBlindEvent(currentRoomData.roomId, WaitForBigBlindCheckboxValue, (socket, packet, args) =>
        {
            print("WaitForBigBlindEvent : " + packet.ToString());

            PokerEventResponse response = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet));
            if (response.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(response.message, null);
            }
        });
    }

    public void OnChatButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        chatPanel.gameObject.SetActive(!chatPanel.gameObject.activeSelf);
        if (chatPanel.activeSelf)
        {
#if UNITY_WEBGL
            EnglishLang.Open();
            HebrewLang.Open();
            //  inputfieldChat.Close();
            // inputfieldChatHebrew.Open();

#else
            EnglishLang.Close();
            HebrewLang.Close();
            inputfieldChat.Open();
            inputfieldChatHebrew.Close();
#endif
        }
    }

    public void OnStandup(string Room)
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.Standup(1, Room, OnStandUpDone);
    }

    public void OnHistoryButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        HistoryScreen.gameObject.SetActive(!HistoryScreen.gameObject.activeSelf);
    }

    public void OnRankingButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (this.currentRoomData.isTournament)
        {
            //tournamentLeaderBoardScreen.gameObject.SetActive(!tournamentLeaderBoardScreen.gameObject.activeSelf);
            tournamentLeaderBoardScreen.SetDataAndOpen();
        }
        else
        {
            RankingScreen.gameObject.SetActive(!RankingScreen.gameObject.activeSelf);
        }
    }

    public void OnClockButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.DisplayConfirmationPanel("Are you sure ?", onClockAccept, null);
    }

    public void onClockAccept()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.SocketGameManager.HasInternetConnection())
        {
            UIManager.Instance.DisplayLoader("");
            UIManager.Instance.SocketGameManager.AbsolutePlayer((socket, packet, args) =>
            {
                Debug.Log("AbsolutePlayer = " + packet.ToString());
                UIManager.Instance.HideLoader();
                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp = Source;

                PokerEventResponse<PlayerLoginResponse> loginResponse = JsonUtility.FromJson<PokerEventResponse<PlayerLoginResponse>>(resp);
                if (loginResponse.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                }
                else
                {
                }
            });
        }
    }

    #endregion

    #region PRIVATE_METHODS

    private void OnPortDistribution(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnPotDistribution  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;

        RunItTwiceRoundCompleteResponse round = JsonUtility.FromJson<RunItTwiceRoundCompleteResponse>(Utility.Instance.GetPacketString(packet));

        if (round.roomId == Constants.Poker.TableId || round.roomId == "")
        {
            foreach (var playerPlace in _playerPlaces)
            {
                var pokerPlayer = playerPlace.pokerPlayer;

                if (pokerPlayer.SidePotAmount > 0)
                {
                    GameObject potChipsObj = Instantiate(txtPotAmount.gameObject.transform.parent.gameObject) as GameObject;
                    potChipsObj.transform.GetChild(0).GetComponent<Text>().text = pokerPlayer.SidePotAmount.ToString();
                    Utility.Instance.UpdateHorizontalLayout(potChipsObj.transform.GetChild(0).gameObject);
                    potChipsObj.SetActive(true);

                    instantiatedObjList.Add(potChipsObj);

                    Transform transformSidePot = txtSidePot1.transform.parent;
                    potChipsObj.transform.SetParent(transformSidePot, false);

                    Vector3 fromPos = pokerPlayer.txtPotAmount.transform.position;
                    Vector3 toPos = transformSidePot.transform.position;

                    StartCoroutine(SidePotAnimation(fromPos, toPos, potChipsObj));
                    Destroy(potChipsObj, 0.5f);

                    pokerPlayer.SidePotAmount = 0;
                }
            }
            /*foreach (PokerPlayer pokerPlayer in GamePlayers)
            {
                if (pokerPlayer.SidePotAmount > 0)
                {
                    GameObject potChipsObj = Instantiate(txtPotAmount.gameObject.transform.parent.gameObject) as GameObject;
                    potChipsObj.transform.GetChild(0).GetComponent<Text>().text = pokerPlayer.SidePotAmount.ToString();
                    Utility.Instance.UpdateHorizontalLayout(potChipsObj.transform.GetChild(0).gameObject);
                    potChipsObj.SetActive(true);

                    instantiatedObjList.Add(potChipsObj);

                    Transform transformSidePot = txtSidePot1.transform.parent;
                    potChipsObj.transform.SetParent(transformSidePot, false);

                    Vector3 fromPos = pokerPlayer.txtPotAmount.transform.position;
                    Vector3 toPos = transformSidePot.transform.position;

                    StartCoroutine(SidePotAnimation(fromPos, toPos, potChipsObj));
                    Destroy(potChipsObj, 0.5f);

                    pokerPlayer.SidePotAmount = 0;
                }
            }*/

            if (round.topMainPot > 0)
            {
                GameObject potChipsObj = Instantiate(txtPotAmount.gameObject.transform.parent.gameObject) as GameObject;
                potChipsObj.transform.GetChild(0).GetComponent<Text>().text = round.topMainPot.ToString();
                Utility.Instance.UpdateHorizontalLayout(potChipsObj.transform.GetChild(0).gameObject);
                potChipsObj.SetActive(true);

                instantiatedObjList.Add(potChipsObj);

                Transform transformMainPots = txtMainPot1.transform.parent;
                potChipsObj.transform.SetParent(transformMainPots, false);

                Vector3 fromPos = txtPotAmount.transform.position;
                Vector3 toPos = transformMainPots.transform.position;

                StartCoroutine(SidePotAnimation(fromPos, toPos, potChipsObj));
                Destroy(potChipsObj, 0.5f);
            }

            MainPot1 = round.mainPot1;
            MainPot2 = round.mainPot2;

            SidePot1 = round.sidePot1;
            SidePot2 = round.sidePot2;

            //PotAmountValue = round.topMainPot;
        }
    }

    DateTime currentDateupdate;
    /*  void StoreTimeNow()
      {
          PlayerPrefs.SetString("TICK_TIME", System.DateTime.Now.ToBinary().ToString());
      }

      void GetPerfectClock()
      {
          if (PlayerPrefs.GetString("TICK_TIME").Equals(""))
          {
              StoreTimeNow();
          }
          else
          {
              long temp = Convert.ToInt64(PlayerPrefs.GetString("TICK_TIME"));
              DateTime oldDate = DateTime.FromBinary(temp);
              DateTime oldDateupdate = oldDate.AddMinutes(15);

              currentDateupdate = System.DateTime.Now;
              TimeSpan difference = currentDateupdate.Subtract(oldDateupdate);

              int _hours = difference.Hours;
              int _minute = difference.Minutes;
              int _second = difference.Seconds;

              if (_hours >= 0 && _minute >= 0 && _second >= 0)
              {
                  // Bonus Availabe btn On
                  txtClockTimerText.text = "";
                  //CollectCoins.enabled = true;
              }
              else
              {

                  if (_hours < 0)
                  {
                      _hours = _hours * (-1);
                  }
                  if (_minute < 0)
                  {
                      _minute = _minute * (-1);
                  }
                  if (_second < 0)
                  {
                      _second = _second * (-1);
                  }

                  // Bonus Text
                  //CollectCoins.enabled = false;
                  txtClockTimerText.text = _hours.ToString("00") + ":" + _minute.ToString("00") + ":" + _second.ToString("00");
              }
          }

      }*/

    public void waitingPlayerSeatManage(int SeatIndex)
    {
        if (gameObject.activeInHierarchy)
        {
            foreach (var orderPlayerPlace in _orderPlayerPlaces)
            {
                orderPlayerPlace.openSeatButton.gameObject.SetActive(false);
            }

            //_playerContainer.SetActiveOpenSeatButtons(false);
        }
    }

    public void reconnectResetData()
    {
        TournamentBreakTableMessage = "";
        Bet.Close();
        TotalTablePotAmount = 0;
        BuyInAmountPanel.Close();
        showBetPanel = false;

        foreach (var playerPlace in _playerPlaces)
        {
            var pokerPlayer = playerPlace.pokerPlayer;

            pokerPlayer.txtUsername.text = "";
            pokerPlayer.PlayerId = "";
            pokerPlayer.txtChips.text = "";
            pokerPlayer.txtPotAmount.text = "";
            pokerPlayer.PlayerActionStatus.ResetData();
            pokerPlayer.PlayerActionStatus.Close();
            pokerPlayer.Dealer.Close();
            pokerPlayer.Close();
            pokerPlayer.playerInfo = null;
        }

        /*foreach (PokerPlayer plr in GamePlayers)
        {
            plr.txtUsername.text = "";
            plr.PlayerId = "";
            plr.txtChips.text = "";
            plr.txtPotAmount.text = "";
            plr.PlayerActionStatus.ResetData();
            plr.PlayerActionStatus.Close();
            plr.Dealer.Close();
            plr.Close();
            plr.playerInfo = null;
        }*/
        BreakTimerPanel.Close();
        btnIAmBack.Close();
        HasJoin = false;
        WaitForBigBlindCheckbox = false;
        preBetButtonsPanel.toggleSitOutNextBigBlind.Close();
        preBetButtonsPanel.toggleSitOutNextHand.Close();
        _rebuyinButtons.Close();
        btnAddOnTournament.Close();
        btnRebuyinTournament.Close();
    }

    void ResetData(bool isPlayerOnly)
    {
        TournamentBreakTableMessage = "";
        showBetPanel = false;

        foreach (var playerPlace in _playerPlaces)
        {
            var pokerPlayer = playerPlace.pokerPlayer;
            if (isPlayerOnly || pokerPlayer.status == PlayerStatus.Waiting || pokerPlayer.status == PlayerStatus.None)
            {
                pokerPlayer.txtUsername.text = "";
                pokerPlayer.PlayerId = "";
                pokerPlayer.txtChips.text = "";
                pokerPlayer.txtPotAmount.text = "";
                pokerPlayer.PlayerActionStatus.ResetData();
                pokerPlayer.PlayerActionStatus.Close();
                pokerPlayer.Dealer.Close();
                pokerPlayer.Close();
            }
        }
        /*foreach (PokerPlayer plr in GamePlayers)
        {
            if (isPlayerOnly || plr.status == PlayerStatus.Waiting || plr.status == PlayerStatus.None)
            {
                plr.txtUsername.text = "";
                plr.PlayerId = "";
                plr.txtChips.text = "";
                plr.txtPotAmount.text = "";
                plr.PlayerActionStatus.ResetData();
                plr.PlayerActionStatus.Close();
                plr.Dealer.Close();
                plr.Close();
            }
        }*/
        /*
        if (!currentRoomData.isTournament) {
            foreach (Button Seats in Seats) {			
                Seats.Open ();
            }
        }
        */

        foreach (var playerPlace in _playerPlaces)
        {
            var pokerPlayer = playerPlace.pokerPlayer;
            pokerPlayer.txtSidePot.transform.parent.gameObject.SetActive(false);
        }

        /*foreach (PokerPlayer pokerPlayer in GamePlayers)
        {
            pokerPlayer.txtSidePot.transform.parent.gameObject.SetActive(false);
        }*/
        if (isPlayerOnly)
        {
            return;
        }

        allInPanel.Close();
        RaisePanel.Close();
        BreakTimerPanel.Close();
    }

    private void ResetTableCards()
    {
        foreach (PokerCard pc in TableCards)
        {
            pc.transform.eulerAngles = Vector3.zero;
            pc.Close();
            pc.ResetImage();
        }

        foreach (PokerCard pc in TableExtraCards)
        {
            pc.transform.eulerAngles = Vector3.zero;
            pc.Close();
            pc.ResetImage();
        }
    }

    private void PerformPlayerAction(PlayerAction action)
    {
        if (action.action != null)
        {
            //			print(action.action.action);
            /*   if (action.action.action == PokerPlayerAction.Call || (action.action.action == PokerPlayerAction.Bet))
               {
                   foreach (PokerPlayer Pplr in GamePlayers)
                   {
                       if (Pplr.gameObject.activeInHierarchy)
                       {
                           if (Pplr.RaiseObj.gameObject.activeInHierarchy)
                           {
                               Pplr.CloseRaiseAnimation();
                           }
                       }
                   }
               }*/
            PokerPlayer plr = GetPlayerById(action.action.playerId);
            if (plr != null)
            {
                if (plr.BetAmount > 0 && plr.BetAmount < plr.BetAmount + action.action.betAmount)
                {
                    //	SCALE ANIMATION
                    PokerPlayerBetValue imgBetIncr = Instantiate(plr.Bets).GetComponent<PokerPlayerBetValue>();
                    instantiatedObjList.Add(imgBetIncr.gameObject);

                    imgBetIncr.transform.SetParent(cardsParentForInstantiatedCards, false);
                    //imgBetIncr.txtBet.text = plr.Bets.txtBet.text;
                    imgBetIncr.txtBet.text = action.action.betAmount.ConvertToCommaSeparatedValue();
                    imgBetIncr.transform.position = plr.txtChips.transform.position;

                    Vector3 fromPos = plr.txtChips.transform.parent.position;
                    Vector3 toPos = plr.Bets.transform.position;

                    Utility.Instance.MoveObject(imgBetIncr.transform, fromPos, toPos, 0.2f);
                    Destroy(imgBetIncr.gameObject, 0.23f);
                }

                //	UP ANIMATION
                print("$$$$$$$$$$$$$$$$$ action.action.action1: " + action.action.action);
                print("$$$$$$$$$$$$$$$$$ action.action.action2: " + (action.action.action == PokerPlayerAction.SmallBlind));

                if (action.action.action == PokerPlayerAction.Check || (action.action.action == PokerPlayerAction.Call && action.action.betAmount == 0))
                {
                    plr.PlayerActionAnimation("CHECK");
                    UIManager.Instance.SoundManager.CheckClickOnce();
                }
                else if (action.action.action == PokerPlayerAction.Bet && plr.BetAmount < plr.BetAmount + action.action.betAmount)
                {
                    plr.PlayerActionAnimation("RAISE");
                    UIManager.Instance.SoundManager.RaiseClickOnce();
                    //    RaisePanel.Setdata(plr.txtUsername.text);
                    plr.RaiseAnimation();
                }
                else if (action.action.action == PokerPlayerAction.Call)
                {
                    plr.PlayerActionAnimation("CALL");
                    UIManager.Instance.SoundManager.CallClickOnce();
                }
                else if (action.action.action == PokerPlayerAction.Fold)
                {
                    plr.PlayerActionAnimation("FOLD");
                    UIManager.Instance.SoundManager.FoldClickOnce();
                    plr.FoldObjAnimation();
                }
                else if (action.action.action == PokerPlayerAction.Allin)
                {
                    plr.PlayerActionAnimation("ALL IN");
                    //allInPanel.Setdata(plr.txtUsername.text);
                    UIManager.Instance.SoundManager.allInClickOnce();
                    plr.AllInAnimation();
                }
                else if (action.action.action == PokerPlayerAction.SmallBlind)
                {
                    print("$$$$$$$$$$$$$$$$$ action.action.action3:");
                    plr.PlayerActionAnimation("SMALL BLIND");
                }
                else if (action.action.action == PokerPlayerAction.BigBlind)
                {
                    plr.PlayerActionAnimation("BIG BLIND");
                }

                //plr.BetAmount += action.action.betAmount;
                plr.BetAmount = action.action.totalBetAmount;
                //plr.BuyInAmount -= action.action.betAmount;
                //				Debug.Log("action.action.betAmount = > "+action.playerBuyIn.ConvertDoubleDecimals());
                //				Debug.Log("newBuyIn = > "+action.playerBuyIn.ConvertDoubleDecimals());
                plr.BuyInAmount = action.playerBuyIn;

                plr.SetLastActionPerformed(action.action.action, action.action.betAmount, action.action.hasRaised);

                if (action.action.action == PokerPlayerAction.Fold)
                {
                    //					plr.playerCanvasGroup.alpha = Constants.Poker.FoldCanvasAlpha;
                    plr.PlayerActionStatus.Open();
                    plr.PlayerActionStatus.txtAction.text = PlayerStatus.Fold.ToString();
                    plr.playerInfo.status = PlayerStatus.Fold.ToString();
                    plr.status = PlayerStatus.Fold;
                    plr.MoveCardsToDealer();
                }
                else if (action.action.action == PokerPlayerAction.Allin)
                {
                    plr.status = PlayerStatus.Allin;
                    plr.playerInfo.status = PlayerStatus.Allin.ToString();
                }

                //				try {
                //					HistoryManager.GetInstance ().AddHistory (plr.PlayerId, plr.playerInfo.username, CurrentRound, action.action.betAmount, plr.BetAmount, (PokerPlayerAction)action.action.action, action.action.hasRaised);
                //				} catch (System.Exception e) {
                //					stackTrace = new System.Diagnostics.StackTrace();
                //			Debug.Log("Error at !! " + stackTrace.GetFrame(1).GetMethod().Name);
                //				}
            }
        }
    }

    //	private void SetPresetButtons (PlayerAction PlayerActionreceived)
    //	{
    //		double minBetAmount = HistoryManager.GetInstance ().GetMinBetAmountInCurrentRound (CurrentRound);
    //		minBetAmount -= GetOwnPlayer ().BetAmount;
    //
    ////		if (minBetAmount > 0) {
    ////			Bet.btnCall.gameObject.SetActive (true);
    ////			Bet.btnCheck.gameObject.SetActive (false);
    ////		} else {
    ////			Bet.btnCall.gameObject.SetActive (false);
    ////			Bet.btnCheck.gameObject.SetActive (true);
    ////		}
    //
    //		Bet.btnCall.gameObject.SetActive (minBetAmount > 0);
    //		Bet.btnCheck.gameObject.SetActive (!(minBetAmount > 0));
    //
    ////		if (PlayerActionreceived.action.action.Equals (PokerPlayerAction.Call)) {
    ////			
    ////		}
    //
    //		//		txtPreCall.text = "CALL " + minBetAmount.ConvertToCommaSeparatedValue ();
    //	}

    //	private void RemovePlayer (string playerId)
    //	{
    //		int index = GetPlayerIndexByPlayerId (playerId);
    //
    //		if (index != -1) {
    ////			UIManager.Instance.historyPanel.AddPlayerLeftLog(GamePlayers[index].playerInfo.username);
    //			PokerPlayer plr = GetPlayerById (playerId);
    //			string Message = plr.txtUsername.text + " is " + Constants.Poker.PokerPlayerStatusLeft;
    //			StartCoroutine (RemovePLayerInfo (Message));		
    //
    //			if (playerId.Equals (UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)) {
    //				for (int i = 0; i < GamePlayers.Length; i++) {
    //					if (!GamePlayers [i].gameObject.activeSelf) {
    //						if (UIManager.Instance.selectedGameType == GameType.texas) {
    //							Seats [i].Open ();
    //							Seats [i].interactable = true;
    //						}
    //					}											
    //				}
    //				btnIAmBack.gameObject.SetActive (false);
    //				UIManager.Instance.assetOfGame.SavedLoginData.chips += GetOwnPlayer ().BuyInAmount;
    //				GamePlayers [index].BuyInAmount = 0;
    //
    //				HasJoinedRoom = false;
    //				if (UIManager.Instance.selectedGameType == GameType.texas) {
    //					Seats [index].gameObject.SetActive (!HasJoinedRoom);
    //					Seats [index].interactable = !HasJoinedRoom;
    //					btnRebuyin.interactable = HasJoinedRoom;
    //				}
    //
    //				chatPanel.SetActive (false);
    //				btnChat.interactable = HasJoin;
    //			} else {
    //				if (!HasJoinedRoom) {
    //					if (UIManager.Instance.selectedGameType == GameType.texas) {
    //						Seats [index].gameObject.SetActive (!HasJoinedRoom);
    //						Seats [index].interactable = !HasJoinedRoom;
    //					}
    //				}
    //			}	
    //			GamePlayers [index].Close ();
    //			GamePlayers [index].playerInfo = null;
    //			GamePlayers [index].PlayerId = "";
    //			GamePlayers [index].status = PlayerStatus.None;
    //		}
    //	}

    private void HideShowCard()
    {
        btnShowCards.Close();
    }

    private void RemovePlayer(string playerId)
    {
        int index = GetPlayerIndexByPlayerId(playerId);

        if (index != -1)
        {
            var pokerPlayer = _playerPlaces[index].pokerPlayer;
            //			UIManager.Instance.historyPanel.AddPlayerLeftLog(GamePlayers[index].playerInfo.username);
            //			PokerPlayer plr = GetPlayerById (playerId);
            //			string Message = plr.txtUsername.text + " is " + Constants.Poker.PokerPlayerStatusLeft;
            //			StartCoroutine (RemovePLayerInfo (Message));		

            if (playerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
            {
                BuyInAmountPanel.Close();
                btnIAmBack.gameObject.SetActive(false);
                UIManager.Instance.assetOfGame.SavedLoginData.chips += GetOwnPlayer().BuyInAmount;
                pokerPlayer.BuyInAmount = 0;
                HasJoinedRoom = false;
                chatPanel.SetActive(false);
                btnChat.interactable = HasJoin;
                btnRebuyinTournament.Close();
                /*
                                for (int i = 0; i < GamePlayers.Length; i++)
                                {
                                    GamePlayers[i].CloseAllOpenCards();
                                    GamePlayers[i].CloseAllHiddenCards();
                                }*/
            }
            else
            {
                if (!HasJoinedRoom && !currentRoomData.isTournament)
                {
                    if (UIManager.Instance.selectedGameType == GameType.cash)
                    {
                        //	Seats[index].gameObject.SetActive(!HasJoinedRoom);
                    }
                }
            }

            pokerPlayer.Close();
            pokerPlayer.playerInfo = null;
            pokerPlayer.PlayerId = "";
            pokerPlayer.status = PlayerStatus.None;
        }
    }

    private void RemoveAllPlayersFromTable()
    {
        foreach (var playerPlace in _playerPlaces)
        {
            var pokerPlayer = playerPlace.pokerPlayer;

            pokerPlayer.Close();
            pokerPlayer.playerInfo = null;
            pokerPlayer.PlayerId = "";
            pokerPlayer.status = PlayerStatus.None;
        }

        /*foreach (PokerPlayer player in GamePlayers)
        {
            player.Close();
            player.playerInfo = null;
            player.PlayerId = "";
            player.status = PlayerStatus.None;
        }*/
    }

    private void ResetMatchedTableCards()
    {
        foreach (PokerCard pc in TableCards)
        {
            pc.card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, 0, true);
        }

        foreach (PokerCard pc in TableExtraCards)
        {
            pc.card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, 0, true);
        }
    }

    private void DestoryStuff()
    {
        foreach (Transform Objects in ChatParent)
        {
            Destroy(Objects.gameObject);
        }

        foreach (Transform Objects in cardGeneratePosition)
        {
            Destroy(Objects.gameObject);
        }

        foreach (Transform Objects in cardsParentForInstantiatedCards)
        {
            Destroy(Objects.gameObject);
        }
    }

    private void DestroyInstantiatedObjects()
    {
        foreach (GameObject obj in instantiatedObjList)
        {
            if (obj)
            {
                Destroy(obj);
            }
        }

        instantiatedObjList.Clear();
    }

    //This function is created to modify winner object to prevent multiple time wininng type animation
    private PokerGameWinner RemoveDuplicateGameWinType(PokerGameWinner winner)
    {
        for (int i = 0; i < winner.winners.Count; i++)
        {
            for (int j = i + 1; j < winner.winners.Count; j++)
            {
                if (winner.winners[i].playerId == winner.winners[j].playerId)
                {
                    winner.winners[j].winningType = "";
                }
            }
        }

        return winner;
    }

    private int GetActivePlayerCount()
    {
        int count = 0;

        /*for (int i = 0; i < GamePlayers.Length; i++)
        {
            if (GamePlayers[i].isActiveAndEnabled)
            {
                count++;
            }
        }*/

        foreach (var playerPlace in _playerPlaces)
        {
            if (playerPlace.pokerPlayer.isActiveAndEnabled)
            {
                count++;
            }
        }

        return count;
    }

    public void IAmBackRemainingTime(double dueSeconds, string currentTime)
    {
        /* DateTime bonusStartTime = System.DateTime.Parse(startTime);
         //Debug.Log("startTime : " + bonusStartTime);

         DateTime bonusCurrentTime = System.DateTime.Parse(currentTime);
         //Debug.Log("currentTime : " + bonusCurrentTime);

         System.TimeSpan TimeDiff = bonusCurrentTime.Subtract(bonusStartTime).Duration();
         //Debug.Log("TimeDiff : " + TimeDiff);

         System.TimeSpan countdown = oneDay.Subtract(TimeDiff).Duration();
         //Debug.Log("countdown : " + countdown);

         remainingBonusTime = new string[0];

         if (TimeDiff.Hours == 0 && TimeDiff.Minutes == 0 && TimeDiff.Seconds == 0)
         {
             remainingBonusTime = new string[3];
             remainingBonusTime[0] = "00";
             remainingBonusTime[1] = "10";
             remainingBonusTime[2] = "00";
         }
         else
         {
             remainingBonusTime = countdown.ToString().Split(":"[0]);
         }

         if (float.Parse(remainingBonusTime[0]) > 0)
         {
             hoursInSeconds = 3600 * float.Parse(remainingBonusTime[0]);
         }
         else
         {
             hoursInSeconds = 0;
         }

         if (float.Parse(remainingBonusTime[1]) > 0)
         {
             minutesInSeconds = 60 * float.Parse(remainingBonusTime[1]);
         }
         else
         {
             minutesInSeconds = 0;
         }
         secondsInSeconds = float.Parse(remainingBonusTime[2]);

         totalSeconds = hoursInSeconds + minutesInSeconds + secondsInSeconds;*/
        totalSeconds = dueSeconds;
    }

    public void ClockRemainingTime(double startTime, string currentTime)
    {
        /*  DateTime bonusStartTime = DateTime.Parse(startTime);

          Debug.Log("startTime : " + bonusStartTime);

          DateTime bonusCurrentTime = DateTime.Parse(currentTime);
          //Debug.Log("currentTime : " + bonusCurrentTime);
          //DateTime bonusStartTime = DateTime.ParseExact(startTime, "MM-dd-yyyy hh:mm:ss:tt", CultureInfo.InvariantCulture);
          ////Debug.Log("startTime : " + bonusStartTime);

          //DateTime bonusCurrentTime = DateTime.ParseExact(currentTime, "MM-dd-yyyy hh:mm:ss:tt", CultureInfo.InvariantCulture);
          ////Debug.Log("currentTime : " + bonusCurrentTime);


          System.TimeSpan TimeDiff = bonusCurrentTime.Subtract(bonusStartTime).Duration();
          //Debug.Log("TimeDiff : " + TimeDiff);

          System.TimeSpan Clockcountdown = clockDay.Subtract(TimeDiff).Duration();
          //Debug.Log("countdown : " + countdown);

          remainingclockTime = new string[0];

          if (TimeDiff.Hours == 0 && TimeDiff.Minutes == 0 && TimeDiff.Seconds == 0)
          {
              remainingclockTime = new string[3];
              remainingclockTime[0] = "00";
              remainingclockTime[1] = "15";
              remainingclockTime[2] = "00";
          }
          else
          {
              remainingclockTime = Clockcountdown.ToString().Split(":"[0]);
          }

          if (float.Parse(remainingclockTime[0]) > 0)
          {
              clockhoursInSeconds = 3600 * float.Parse(remainingclockTime[0]);
          }
          else
          {
              clockhoursInSeconds = 0;
          }

          if (float.Parse(remainingclockTime[1]) > 0)
          {
              clockminutesInSeconds = 60 * float.Parse(remainingclockTime[1]);
          }
          else
          {
              clockminutesInSeconds = 0;
          }
          clocksecondsInSeconds = float.Parse(remainingclockTime[2]);

          clocktotalSeconds = clockhoursInSeconds + clockminutesInSeconds + clocksecondsInSeconds;*/
        clocktotalSeconds = startTime;
    }

    public void RebuyRemainingTime(double startTime)
    {
        totalRebuySeconds = startTime;
    }

    public void GetRunningGameList(string RoomID)
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
                        if (RoomID == currentRoomData.roomId && RoomID.Length != 0)
                        {
                            print(RoomID + " == " + currentRoomData.roomId);
                            return;
                        }

                        RemoveAllPlayersFromTable();

                        if (RoomID != "")
                        {
                            print("StartCoroutine (SwitchTable)");
                            StartCoroutine(reBuyinSwitchTable(RoomID));
                        }
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

    #region COROUTINES

    IEnumerator SidePotAnimation(Vector3 fromPos, Vector3 toPos, GameObject potChipsObj)
    {
        float time = 0.5f;
        float a = 0;
        while (a < 1)
        {
            a += Time.deltaTime * (1 / time);
            potChipsObj.transform.position = Vector3.Lerp(fromPos, toPos, a);
            yield return 0;
        }
    }

    IEnumerator popClose(float timer, string Message)
    {
        UIManager.Instance.DisplayMessagePanelOnly(Message);
        yield return new WaitForSeconds(timer);
        UIManager.Instance.HidemessagePanelInfoPopup();
    }

    IEnumerator RemovePLayerInfo(string ShowMessage)
    {
        UIManager.Instance.DisplayMessagePanelOnly(ShowMessage);
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.HidemessagePanelInfoPopup();
    }

    IEnumerator ForceScrollDownChat()
    {
        // Wait for end of frame AND force update all canvases before setting to bottom.
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
        chatHistoryScrollRect.verticalScrollbar.value = 0;
        Canvas.ForceUpdateCanvases();
    }

    private IEnumerator BLindValueRaised(BlindLevelRaised BlindLevelRaisedData)
    {
        /*currentRoomData.roomId = BlindLevelRaisedData.roomId;
        currentRoomData.minBuyIn = BlindLevelRaisedData.minBuyIn;
        currentRoomData.maxBuyIn = BlindLevelRaisedData.maxBuyIn;
        currentRoomData.smallBlind = BlindLevelRaisedData.smallBlind;
        currentRoomData.bigBlind = BlindLevelRaisedData.bigBlind;*/
        string ShowMessage = "";
        if (this.currentRoomData.isTournament && !BlindLevelRaisedData.isRebuyIn)
        {
            UIManager.Instance.HidePopup();
            btnRebuyinTournament.Close();
        }

        if (BlindLevelRaisedData.isNotify)
        {
            ShowMessage = BlindLevelRaisedData.message + " " + BlindLevelRaisedData.smallBlind + "/" + BlindLevelRaisedData.bigBlind;
            UIManager.Instance.DisplayMessagePanelOnly(ShowMessage);
            yield return new WaitForSeconds(1.5f);
            UIManager.Instance.HidemessagePanelInfoPopup();
        }
    }

    private IEnumerator bountyTournamentDisplay(BlindLevelRaised BlindLevelRaisedData)
    {
        string ShowMessage = "";
        ShowMessage = BlindLevelRaisedData.message + " " + BlindLevelRaisedData.smallBlind + "/" + BlindLevelRaisedData.bigBlind;
        UIManager.Instance.DisplayMessagePanelOnly(ShowMessage);
        yield return new WaitForSeconds(2f);
        UIManager.Instance.HidemessagePanelInfoPopup();
    }

    private IEnumerator SwitchTable(string RoomId)
    {
        UIManager.Instance.DisplayLoader("");
        Switchingtable = true;
        if (!currentRoomData.isTournament)
            UIManager.Instance.SocketGameManager.UnSubscribeRoom(UIManager.Instance.gameType, OnUnSubscribeRoomDone);
        yield return new WaitForSeconds(0.2f);
        Debug.Log("NewTableId = > " + RoomId);
        Constants.Poker.TableId = RoomId;
        print("old roomId: " + currentRoomData.roomId + " new roomId: " + RoomId);
        this.currentRoomData.roomId = RoomId;
        print("Room id changed");
        yield return new WaitForSeconds(0.5f);

        UIManager.Instance.SocketGameManager.SubscribeRoom(OnSubscribeRoomDone);
    }

    private IEnumerator reBuyinSwitchTable(string RoomId)
    {
        UIManager.Instance.DisplayLoader("");
        Switchingtable = true;
        UIManager.Instance.SocketGameManager.UnSubscribeRoom(UIManager.Instance.gameType, OnUnSubscribeRoomDone);
        yield return new WaitForSeconds(0.2f);
        Debug.Log("NewTableId = > " + RoomId);
        Constants.Poker.TableId = RoomId;
        print("old roomId: " + currentRoomData.roomId + " new roomId: " + RoomId);
        this.currentRoomData.roomId = RoomId;
        print("Room id changed");
        yield return new WaitForSeconds(0.5f);

        UIManager.Instance.SocketGameManager.SubscribeRoom(OnSubscribeRoomDone);
    }

    private IEnumerator SitPlayersAll()
    {
        yield return new WaitForSeconds(2.5f);
        /*for (int i = 0; i < GamePlayers.Length; i++)
        {
            OnSeatButtonTap(i);
            yield return new WaitForSeconds(0.2f);
        }*/
        for (int i = 0; i < _playerPlaces.Count; i++)
        {
            OnSeatButtonTap(i);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.2f);
        /*for (int i = 0; i < GamePlayers.Length; i++)
        {
            GamePlayers[i].BetAmount = 2000;
            yield return new WaitForSeconds(0.2f);
        }*/
        for (int i = 0; i < _playerPlaces.Count; i++)
        {
            _playerPlaces[i].pokerPlayer.BetAmount = 2000;
            yield return new WaitForSeconds(0.2f);
        }

        StartCoroutine("MovepotToAnimation");
    }

    IEnumerator MovepotToAnimation()
    {
        /*for (int i = 0; i < GamePlayers.Length; i++)
        {
            GamePlayers[i].StartCoroutine(GamePlayers[i].TransferBetAmountToPot(2000));
            yield return new WaitForSeconds(0.2f);
        }*/
        for (int i = 0; i < _playerPlaces.Count; i++)
        {
            var pokerPlayer = _playerPlaces[i].pokerPlayer;
            pokerPlayer.StartCoroutine(pokerPlayer.TransferBetAmountToPot(2000));
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.2f);
        StartCoroutine("DistributeCards");
    }

    IEnumerator NextScreen(float timer)
    {
        UIManager.Instance.DisplayLoader("");
        yield return new WaitForSeconds(timer);
        UIManager.Instance.GameScreeen.Close();
        UIManager.Instance.LobbyPanelNew.Open(); // LobbyScreeen not used more
    }

    //	private IEnumerator DistributeCards ()
    //	{
    //		Debug.Log ("DistributeCards");
    //		yield return new WaitForEndOfFrame ();
    //		for (int totalCards = 0; totalCards < 2; totalCards++) {
    //			for (int i = 0; i < GamePlayers.Length; i++) {
    ////				Debug.Log ("DistributeCards 1");
    //				if (GamePlayers [i].gameObject.activeSelf && GamePlayers [i].status.Equals (PlayerStatus.Playing) && !GamePlayers [i].playerInfo.idealPlayer) {
    ////					Debug.Log ("DistributeCards 2");
    //					GameObject card = Instantiate (cardPrefab) as GameObject;
    //					card.transform.SetParent (GamePlayers [i].transform, false);
    //					card.transform.SetAsFirstSibling ();
    //
    //					instantiatedObjList.Add (card);
    //
    //					Vector3 fromPos = cardGeneratePosition.position;
    //					Vector3 fromAngle = imgDealer.transform.eulerAngles;
    //					Vector3 toPos;
    //					Vector3 toAngle; 
    //
    //					//if (GamePlayers [i].PlayerId.Equals (UIManager.Instance.assetOfGame.SavedGamePlayData.PlayerId)) {
    //					if (totalCards == 0) {
    //						toPos = GamePlayers [i].Card1.transform.position;
    //						toAngle = GamePlayers [i].Card1.transform.eulerAngles;
    //					} else {
    //						toPos = GamePlayers [i].Card2.transform.position;
    //						toAngle = GamePlayers [i].Card2.transform.eulerAngles;
    //					}
    //
    //					/*} else {
    //						if (totalCards == 0) {
    //							toPos = GamePlayers [i].Card1.transform.position;
    //							toAngle = GamePlayers [i].Card1.transform.eulerAngles;
    //						} else {
    //							toPos = GamePlayers [i].Card2.transform.position;
    //							toAngle = GamePlayers [i].Card2.transform.eulerAngles;
    //						}
    //
    //					}*/
    //					card.transform.position = fromPos;
    //					StartCoroutine (MoveDistributedCard (GamePlayers [i], card, fromPos, toPos, fromAngle, toAngle, totalCards));
    //					UIManager.Instance.SoundManager.Card_DealtClickOnce ();
    //					yield return new WaitForSeconds (.1f);
    //				}
    //			}
    //		}
    //	}

    private IEnumerator DistributeCards(PlayerCards[] playerCards)
    {
        Debug.Log("DistributeCards");
        yield return new WaitForEndOfFrame();

        int cardsPerPlayer;
        if (currentRoomData.pokerGameType == PokerGameType.texas.ToString())
        {
            cardsPerPlayer = 2;
        }
        else if (currentRoomData.pokerGameType == PokerGameType.omaha.ToString())
        {
            cardsPerPlayer = 4;
        }
        else
        {
            cardsPerPlayer = 5;
        }

        Debug.Log("DistributeCards cardsPerPlayer => " + cardsPerPlayer);

        for (int totalCards = 0; totalCards < cardsPerPlayer; totalCards++)
        {
            for (int i = 0; i < playerCards.Length; i++)
            {
                PokerPlayer plr = GetPlayerById(playerCards[i].playerId);
                if (plr != null && plr.gameObject.activeSelf && plr.status.Equals(PlayerStatus.Playing) && (!plr.playerInfo.idealPlayer || currentRoomData.isTournament))
                {
                    GameObject card = Instantiate(cardPrefab) as GameObject;
                    instantiatedObjList.Add(card);
                    card.transform.SetParent(plr.transform, false);
                    card.transform.SetAsFirstSibling();

                    Vector3 fromPos = cardGeneratePosition.position;
                    Vector3 fromAngle = imgDealer.transform.eulerAngles;
                    Vector3 toPos;
                    Vector3 toAngle;

                    if (totalCards == 0)
                    {
                        toPos = plr.GetCard1Position();
                        toAngle = plr.GetCard1EulerAngles();
                    }
                    else if (totalCards == 1)
                    {
                        toPos = plr.GetCard2Position();
                        toAngle = plr.GetCard2EulerAngles();
                    }
                    else if (totalCards == 2)
                    {
                        toPos = plr.GetCard3Position();
                        toAngle = plr.GetCard3EulerAngles();
                    }
                    else if (totalCards == 3)
                    {
                        toPos = plr.GetCard4Position();
                        toAngle = plr.GetCard4EulerAngles();
                    }

                    else
                    {
                        toPos = plr.GetCard5Position();
                        toAngle = plr.GetCard5EulerAngles();
                    }

                    card.transform.position = fromPos;
                    StartCoroutine(MoveDistributedCard(plr, card, fromPos, toPos, fromAngle, toAngle, totalCards));
                    UIManager.Instance.SoundManager.Card_DealtClickOnce();
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        /*foreach (PokerPlayer plr in GamePlayers)
        {
            if (!plr.PlayerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
            {

                if (UIManager.Instance.assetOfGame.SavedLoginData.isSuperPlayer)
                {
                    Debug.Log("111111");
                    GetSuperPlayerRequest();
                }
            }
        }*/
        //foreach (PokerPlayer plr in GamePlayers)
        //{
        // if (!plr.PlayerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId) && plr.gameObject.activeInHierarchy)
        // {

        // if (UIManager.Instance.assetOfGame.SavedLoginData.isSuperPlayer)
        // {
        // Debug.Log("222222");
        // GetSuperPlayerRequest();
        // }
        // }
        //}
        if (UIManager.Instance.MySuperPlayer)
        {
            //Debug.Log("11111111");
            GetSuperPlayerRequest();
        }
    }

    private IEnumerator MoveDistributedCard(PokerPlayer plr, GameObject card, Vector3 fromPos, Vector3 toPos, Vector3 fromAngle, Vector3 toAngle, int totalCards)
    {
        float a = 0f;
        float time = 0.25f;
        //time = currentRoomData.pokerGameType == "texas" ? 0.25f : 0.10f;

        while (a < 1)
        {
            //a += Time.deltaTime * 3f;
            a += Time.deltaTime * (1 / time);
            card.transform.position = Vector3.Lerp(fromPos, toPos, a);
            card.transform.eulerAngles = Vector3.Lerp(fromAngle, toAngle, a);
            yield return 0;
        }


        // Following if condition added to prevent 1 issue - blank card apears on waiting player(own) while player seat(center) at the time of card shuffle
        if (plr.status == PlayerStatus.Waiting || plr.status == PlayerStatus.Fold)
        {
            plr.CloseNormalCards();
            plr.CloseHiddenCards();
            plr.CloseShowCards();
            yield break;
        }

        if (totalCards == 0)
        {
            if (GetOwnPlayer() != null && plr.PlayerId == GetOwnPlayer().PlayerId)
            {
                plr.Card1.transform.eulerAngles = Vector3.zero;
                plr.Card1.Open();
            }
            else
            {
                plr.HiddenCard1.transform.eulerAngles = Vector3.zero;
                plr.HiddenCard1.Open();
            }
        }
        else if (totalCards == 1)
        {
            if (GetOwnPlayer() != null && plr.PlayerId == GetOwnPlayer().PlayerId)
            {
                plr.Card2.transform.eulerAngles = Vector3.zero;
                plr.Card2.Open();
            }
            else
            {
                plr.HiddenCard2.transform.eulerAngles = Vector3.zero;
                plr.HiddenCard2.Open();
            }
        }
        else if (totalCards == 2)
        {
            if (GetOwnPlayer() != null && plr.PlayerId == GetOwnPlayer().PlayerId)
            {
                plr.Card3.transform.eulerAngles = Vector3.zero;
                plr.Card3.Open();
            }
            else
            {
                plr.HiddenCard3.transform.eulerAngles = Vector3.zero;
                plr.HiddenCard3.Open();
            }
        }
        else if (totalCards == 3)
        {
            if (GetOwnPlayer() != null && plr.PlayerId == GetOwnPlayer().PlayerId)
            {
                plr.Card4.transform.eulerAngles = Vector3.zero;
                plr.Card4.Open();
            }
            else
            {
                plr.HiddenCard4.transform.eulerAngles = Vector3.zero;
                plr.HiddenCard4.Open();
            }
        }

        else if (totalCards == 4)
        {
            if (GetOwnPlayer() != null && plr.PlayerId == GetOwnPlayer().PlayerId)
            {
                plr.Card5.transform.eulerAngles = Vector3.zero;
                plr.Card5.Open();
            }
            else
            {
                plr.HiddenCard5.transform.eulerAngles = Vector3.zero;
                plr.HiddenCard5.Open();
            }
        }

        Destroy(card);

        //Debug.Log("$ Card open");

        yield return 0;
    }

    private IEnumerator DistributeSuperPlayerTableCards(List<string> tableCards)
    {
        int openedCardCount = 0;
        if (tableCards.Count == 4)
            openedCardCount = 3;
        else if (tableCards.Count == 5)
            openedCardCount = 4;

        for (int i = 0; i < openedCardCount; i++)
        {
            this.TableCards[i].Open();
            if (UIManager.Instance.MySuperPlayer)
            {
                if (tableCardsListData.Count > 0)
                {
                    for (int j = 0; j < tableCardsListData.Count; j++)
                    {
                        if (tableCardsListData.Contains(tableCards[i]))
                        {
                            this.TableCards[i].SetAlpha(1f);
                        }
                        else
                        {
                            this.TableCards[i].SetAlpha(0.5f);
                        }
                    }
                }
                else
                {
                    this.TableCards[i].SetAlpha(0.5f);
                }
            }

            //else
            //{
            //    this.TableCards[i].SetAlpha(1f);
            //}
        }

        for (int i = 0; i < tableCards.Count; i++)
        {
            if (this.TableCards[i].gameObject.activeSelf)
            {
                this.TableCards[i].DisplayCardWithoutAnimation(tableCards[i]);

                //   this.TableCards[i].SetAlpha(1f);
                continue;
            }

            /*
                        GameObject tc = Instantiate(cardPrefab) as GameObject;
                        instantiatedObjList.Add(tc);
                        tc.transform.SetParent(cardsParentForInstantiatedCards, false);
                        Vector3 fromPos = imgDealer.transform.position;
                        Vector3 toPos = this.TableCards[i].transform.position;
                        Vector3 fromScale = Vector3.zero;
                        Vector3 toScale = TableCards[i].GetComponent<RectTransform>().sizeDelta;

                        float a = 0;
                        while (a < 1)
                        {
                            a += Time.deltaTime * 3f;
                            tc.transform.position = Vector3.Lerp(fromPos, toPos, a);
                            tc.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(fromScale, toScale, a);
                            yield return 0;
                        }

                        Destroy(tc);*/
            yield return 0;
            UIManager.Instance.SoundManager.Card_DealtClickOnce();
            this.TableCards[i].Open();
            if (UIManager.Instance.MySuperPlayer)
            {
                /*if (tableCardsListData.Count > 0)
                {
                    for (int j = 0; j < tableCardsListData.Count; j++)
                    {
                        this.TableCards[j].SetAlpha(1f);                       
                    }
                }
                else
                {*/
                this.TableCards[i].SetAlpha(0.5f);
                // }
            }
            else
            {
                this.TableCards[i].SetAlpha(1f);
            }

            this.TableCards[i].PlayAnimation(tableCards[i]);
        }
    }

    private IEnumerator DistributeTableCards(List<string> tableCards)
    {
        int openedCardCount = 0;
        if (tableCards.Count == 4)
            openedCardCount = 3;
        else if (tableCards.Count == 5)
            openedCardCount = 4;

        for (int i = 0; i < openedCardCount; i++)
        {
            this.TableCards[i].Open();
        }

        for (int i = 0; i < tableCards.Count; i++)
        {
            if (this.TableCards[i].gameObject.activeSelf)
            {
                this.TableCards[i].DisplayCardWithoutAnimation(tableCards[i]);

                //   this.TableCards[i].SetAlpha(1f);
                continue;
            }

            GameObject tc = Instantiate(cardPrefab) as GameObject;
            instantiatedObjList.Add(tc);
            tc.transform.SetParent(cardsParentForInstantiatedCards, false);
            Vector3 fromPos = imgDealer.transform.position;
            Vector3 toPos = this.TableCards[i].transform.position;
            Vector3 fromScale = Vector3.zero;
            Vector3 toScale = TableCards[i].GetComponent<RectTransform>().sizeDelta;

            float a = 0;
            while (a < 1)
            {
                a += Time.deltaTime * 3f;
                tc.transform.position = Vector3.Lerp(fromPos, toPos, a);
                //tc.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(fromScale, toScale, a);
                yield return 0;
            }

            //			yield return 0;
            UIManager.Instance.SoundManager.Card_DealtClickOnce();
            this.TableCards[i].Open();
            this.TableCards[i].PlayAnimation(tableCards[i]);
            Destroy(tc);
        }
    }

    private IEnumerator DistributeExtraTableCards(List<string> normalTableCards, List<string> extraTableCards, int extraCardsPosition)
    {
        //yield return new WaitForSeconds(0.5f);

        //for (int i = 3; i < extraTableCards.Count; i++)
        int temp = 0;

        int chintan = 0;
        if (extraCardsPosition == 0 && extraTableCards.Count == 3)
        {
            chintan = 3;
        }
        else if (extraCardsPosition == 0 && extraTableCards.Count == 4)
        {
            chintan = 4;
        }
        else if (extraCardsPosition == 0 && extraTableCards.Count == 5)
        {
            chintan = 5;
        }
        else if (extraCardsPosition == 3 && extraTableCards.Count == 1) //Cards Number 4
        {
            chintan = 4;
        }

        else if (extraCardsPosition == 3 && extraTableCards.Count == 2) //Cards Number 5
        {
            chintan = 5;
        }
        else if (extraCardsPosition == 4 && extraTableCards.Count == 1)
        {
            chintan = 5;
        }

        for (int i = extraCardsPosition; i < chintan; i++)
        {
            //Debug.Log("Temp : " + temp);
            if (this.TableExtraCards[i].gameObject.activeSelf)
            {
                this.TableExtraCards[i].DisplayCardWithoutAnimation(extraTableCards[temp]);
                temp++;
                continue;
            }

            //print (normalTableCards [i] + " - " + extraTableCards [i]);
            //print(normalTableCards [i] != extraTableCards [i]);

            //if (normalTableCards[i] != extraTableCards[i])
            //{
            GameObject tc = Instantiate(cardPrefab) as GameObject;
            tc.transform.SetParent(cardsParentForInstantiatedCards, false);

            //instantiatedObjList.Add(tc);

            Vector3 fromPos = imgDealer.transform.position;
            Vector3 toPos = this.TableExtraCards[i].transform.position;
            Vector3 fromScale = Vector3.zero;
            Vector3 toScale = TableExtraCards[i].GetComponent<RectTransform>().sizeDelta;

            float a = 0;
            while (a < 1)
            {
                a += Time.deltaTime * 3f;
                tc.transform.position = Vector3.Lerp(fromPos, toPos, a);
                //tc.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(fromScale, toScale, a);

                yield return 0;
            }

            UIManager.Instance.SoundManager.Card_DealtClickOnce();
            //yield return 0;
            this.TableExtraCards[i].gameObject.SetActive(true);
            this.TableExtraCards[i].PlayAnimation(extraTableCards[temp]);
            Destroy(tc);
            temp++;
        }
    }

    public GameObject winnerType;

    private IEnumerator DisplayWinningAnimation(PokerGameWinner winner)
    {
        winner = RemoveDuplicateGameWinType(winner);

        //		Debug.Log ("---------------- DisplayWinningAnimation -----------------");
        yield return new WaitForSeconds(.25f);
        TotalTablePotAmount = 0;

        foreach (PokerGameWinner.Wnr wnrObj in winner.winners)
        {
            //			Debug.Log  (wnrObj.playerName + " has won the game with " + wnrObj.hand.message + " rank");

            PokerPlayer winnerPlayer = GetPlayerById(wnrObj.playerId);
            if (winnerPlayer != null)
            {
                //winnerPlayer.HighlightWinningCards (wnrObj.hand);
                UIManager.Instance.SoundManager.chips_moved_from_potClickOnce();
                //				winnerPlayer.Winner.IsAnimationOn (true);
                //winnerPlayer.HighlighUpWinningCards(wnrObj.bestCards);
                winnerPlayer.IsAnimationOn(true);
                //				string winnerHist = wnrObj.playerName + "<color> won " + wnrObj.amount.ToString () + " chips. </color>";
                //					UIManager.Instance.historyPanel.StorePokerWinnerHistory (winnerHist);
                //				}
            }

            if (winnerPlayer != null && wnrObj.sidePotPlayerIndex == -1)
            {
                GameObject potChipsObj = Instantiate(txtPotAmount.gameObject.transform.parent.gameObject) as GameObject;
                instantiatedObjList.Add(potChipsObj);
                //				potChipsObj.GetComponent<TextMeshProUGUI> ().text = wnrObj.amount.ToString ();

                print("potChipsObj.transform.GetChild (0).GetComponent<TextMeshProUGUI> ().text: " + potChipsObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
                print("wnrObj.amount: " + wnrObj.amount);

                potChipsObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = wnrObj.amount.ToString();
                potChipsObj.SetActive(true);

                PlayerSidePot mainPot = SidePotAmountNew;
                mainPot.mainPot -= wnrObj.amount;
                SidePotAmountNew = mainPot;

                potChipsObj.transform.SetParent(cardsParentForInstantiatedCards, false);
                Vector3 fromPos = txtPotAmount.transform.position;
                Vector3 toPos = winnerPlayer.txtChips.transform.position;

                Debug.Log(" wnrObj.winningType " + wnrObj.winningType);
                GameObject winnerTypeObj = null;
                if (wnrObj.winningType.Length > 0)
                {
                    winnerTypeObj = Instantiate(winnerType) as GameObject;
                    instantiatedObjList.Add(winnerTypeObj);
                    winnerTypeObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = wnrObj.winningType;
                    winnerTypeObj.transform.position = winnerPlayer.transform.position;
                    winnerTypeObj.transform.SetParent(winnerPlayer.transform, false);
                    winnerTypeObj.SetActive(true);
                    Utility.Instance.MoveObject(winnerTypeObj.transform, new Vector3(winnerTypeObj.transform.position.x, winnerTypeObj.transform.position.y + 2f, winnerTypeObj.transform.position.z), 2f);
                }

                float a = 0;
                while (a < 1)
                {
                    a += Time.deltaTime * 2;
                    potChipsObj.transform.position = Vector3.Lerp(fromPos, toPos, a);
                    yield return 0;
                }

                SidePotAmountNew = new PlayerSidePot();
                winnerPlayer.BuyInAmount += wnrObj.amount;
                Destroy(potChipsObj);

                if (winnerTypeObj != null)
                    Destroy(winnerTypeObj, 1f);
            }
            else if (winnerPlayer != null)
            {
                int fromSeatPos = getPotIndexFromPlayerIndex(wnrObj.sidePotPlayerIndex);
                //int winnerSeatPos = winnerPlayer.SeatIndex;

                if (SidePotAmountNew.sidePot == null || SidePotAmountNew.sidePot.Count == 0)
                {
                    continue;
                }

                if (SidePotAmountNew.sidePot[fromSeatPos].sidePotAmount <= 0)
                    continue;

                GameObject potChipsObj = Instantiate(txtPotAmount.transform.parent.gameObject) as GameObject;
                instantiatedObjList.Add(potChipsObj);
                print("potChipsObj.transform.GetChild (0).GetComponent<TextMeshProUGUI> ().text: " + potChipsObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
                //print("wnrObj.amount: " + wnrObj.amount);

                potChipsObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = wnrObj.amount.ToString();
                potChipsObj.SetActive(true);

                List<PlayerSidePot.SidePot> sidePot = SidePotAmountNew.sidePot;
                //Debug.Log("SP1: " + sidePot[fromSeatPos].sidePotAmount + " - amount: " + wnrObj.amount);
                sidePot[fromSeatPos].sidePotAmount -= wnrObj.amount;
                //Debug.Log("SP2: " + sidePot[fromSeatPos].sidePotAmount);
                SidePotAmountNew.sidePot = sidePot;
                SidePotAmountNew = SidePotAmountNew;
                potChipsObj.transform.SetParent(transform, false);
                Vector3 fromPos = txtPotAmount.transform.parent.position;
                Vector3 toPos = winnerPlayer.txtChips.transform.position;

                try
                {
                    //PokerPlayer fromPlayer = GetPlayerById (wnrObj.sidePotPlayerId); //following line is commented to solve leave user side pot issue
                    //PokerPlayer fromPlayer = GamePlayers [wnrObj.winnerSeatIndex];
                    PokerPlayer fromPlayer = _playerPlaces[wnrObj.sidePotPlayerIndex].pokerPlayer;
                    PokerPlayer fromMainPlayer = GetPlayerById(getplayerBysidePotPlayerID(wnrObj.sidePotPlayerIndex));
                    if (fromMainPlayer != null)
                        fromPos = fromMainPlayer.txtSidePot.transform.parent.position;
                    //					Debug.Log ("winner sidePotPlayerIndex => " + wnrObj.sidePotPlayerId);
                    //					Debug.Log ("winner PlayerID => " + wnrObj.playerId);
                    //					Debug.Log ("FromPlayer => " + fromPlayer.txtUsername.text.ToString ());
                    //					Debug.Log ("FromPlayer id => " + fromPlayer.PlayerId.ToString ());
                }
                catch (System.Exception ex)
                {
                    Debug.Log("Exception  : " + ex);
                }

                Debug.Log(" wnrObj.winningType " + wnrObj.winningType);
                GameObject winnerTypeObj = null;
                if (wnrObj.winningType.Length > 0)
                {
                    winnerTypeObj = Instantiate(winnerType) as GameObject;
                    instantiatedObjList.Add(winnerTypeObj);
                    winnerTypeObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = wnrObj.winningType;
                    winnerTypeObj.transform.position = winnerPlayer.transform.position;
                    winnerTypeObj.transform.SetParent(winnerPlayer.transform, false);
                    winnerTypeObj.SetActive(true);
                    Utility.Instance.MoveObject(winnerTypeObj.transform, new Vector3(winnerTypeObj.transform.position.x, winnerTypeObj.transform.position.y + 2f, winnerTypeObj.transform.position.z), 2f);
                }

                float a = 0;
                while (a < 1)
                {
                    a += Time.deltaTime * 2;
                    potChipsObj.transform.position = Vector3.Lerp(fromPos, toPos, a);
                    yield return 0;
                }

                //winnerPlayer.BuyInAmount += wnrObj.amount;
                winnerPlayer.BuyInAmount = wnrObj.chips;
                Destroy(potChipsObj);

                if (winnerTypeObj != null)
                    Destroy(winnerTypeObj, 1f);
            }

            //			yield return new WaitForSeconds (3f);
            if (winnerPlayer != null)
            {
                winnerPlayer.BuyInAmount = wnrObj.chips;
            }
        }

        yield return new WaitForSeconds(3f);
        ResetMatchedTableCards();
        yield return 0;
    }

    /*private IEnumerator DisplayWinningAnimation (PokerGameWinner winner)
    {
        Debug.Log ("---------------- DisplayWinningAnimation -----------------"+winner.ToString());
        yield return new WaitForSeconds (.25f);

        foreach (PokerGameWinner.Wnr wnrObj in winner.winners) {
            print (wnrObj.playerName + " has won the game with " + wnrObj.hand.message + " rank");

            PokerPlayer winnerPlayer = GetPlayerById (wnrObj.playerId);
            if (winnerPlayer != null) {
                winnerPlayer.HighlightWinningCards (wnrObj.hand);
                winnerPlayer.Winner.IsAnimationOn (true);
                string winnerHist = wnrObj.playerName + "<color> won " + wnrObj.amount.ToString () + " chips. </color>";
                UIManager.Instance.historyPanel.StorePokerWinnerHistory (winnerHist);

            }

            if (wnrObj.sidePotPlayerIndex == -1) {
                GameObject potChipsObj = Instantiate (txtPotAmount.gameObject.transform.parent.gameObject) as GameObject;
                //potChipsObj.GetComponent<TextMeshProUGUI> ().text = wnrObj.amount.ToString ();
                potChipsObj.transform.GetChild(0).GetComponent<TextMeshProUGUI> ().text = wnrObj.amount.ToString ();
                potChipsObj.SetActive (true);

                PlayerSidePot mainPot = SidePotAmountNew;
                mainPot.mainPot -= wnrObj.amount;
                SidePotAmountNew = mainPot;

                instantiatedObjList.Add (potChipsObj);
                potChipsObj.transform.SetParent (cardsParentForInstantiatedCards, false);
                Vector3 fromPos = txtPotAmount.transform.position;
                Vector3 toPos = winnerPlayer.txtChips.transform.position;

                float a = 0;
                while (a < 1) {
                    a += Time.deltaTime * 2;
                    potChipsObj.transform.position = Vector3.Lerp (fromPos, toPos, a);
                    yield return 0;
                }

                PotAmountValue = 0;
                winnerPlayer.BuyInAmount += wnrObj.amount;
                Destroy (potChipsObj, .25f);

            } else {
                //				GetSeatIndexByPlayerId
                int fromSeatPos = getPotIndexFromPlayerIndex (wnrObj.sidePotPlayerIndex);
                Debug.Log ("fromSeatPos  => " +fromSeatPos +" getPotIndexFromPlayerIndex "+wnrObj.sidePotPlayerIndex);

                if (SidePotAmountNew.sidePot == null || SidePotAmountNew.sidePot.Count == 0) {
                    continue;
                }
                if (SidePotAmountNew.sidePot [fromSeatPos].sidePotAmount <= 0)
                    continue;

                GameObject potChipsObj = Instantiate (txtPotAmount.gameObject.transform.parent.gameObject) as GameObject;
                //potChipsObj.GetComponentInChildren <TextMeshProUGUI> ().text = wnrObj.amount.ConvertToCommaSeparatedValue ();
                potChipsObj.SetActive (true);
                instantiatedObjList.Add (potChipsObj);

                List<PlayerSidePot.SidePot> sidePot = SidePotAmountNew.sidePot;
                sidePot [fromSeatPos].sidePotAmount -= wnrObj.amount;
                SidePotAmountNew.sidePot = sidePot;
                SidePotAmountNew = SidePotAmountNew;

                potChipsObj.transform.SetParent (transform, false);

                Vector3 fromPos = txtPotAmount.transform.parent.position;
                Vector3 toPos = winnerPlayer.txtChips.transform.position;
                Debug.Log ("wnrObj.sidePotPlayerIndex => " + wnrObj.sidePotPlayerIndex);
                try {
                    PokerPlayer fromPlayer = GetSeatIndexByPlayerId (wnrObj.sidePotPlayerIndex);
                    Debug.Log ("wnrObj.sidePotPlayerIndex player name and object name  => " +fromPlayer.name +" uname "+fromPlayer.txtUsername.text);
                    fromPos = fromPlayer.txtPotAmount.transform.parent.position;
                } catch (System.Exception ex) {
                    Debug.LogError ("Exception  : " + ex);
                }

                float a = 0;
                while (a < 1) {
                    a += Time.deltaTime * 2;
                    potChipsObj.transform.position = Vector3.Lerp (fromPos, toPos, a);

                    yield return 0;
                }

                winnerPlayer.BuyInAmount += wnrObj.amount;

                Destroy (potChipsObj, .25f);

                yield return new WaitForSeconds (1f);
            }

            yield return new WaitForSeconds (3f);
        }

        yield return new WaitForSeconds (3f);

        ResetMatchedTableCards ();

        yield return 0;
    }*/

    private IEnumerator DisplayRevertPointAnimation(PokerGameWinner.Wnr wnr)
    {
        PokerPlayer DisplayRevertPlayer = GetPlayerById(wnr.playerId);
        /*
        GameObject potChipsObj = Instantiate (txtPotAmount.transform.parent.gameObject) as GameObject;
        potChipsObj.gameObject.SetActive (true);
        potChipsObj.transform.GetChild (0).GetComponent<TextMeshProUGUI> ().text = wnr.amount.ConvertDoubleDecimalsToValue ();
        */

        if (DisplayRevertPlayer != null)
        {
            PlayerSidePot mainPot = SidePotAmountNew;
            mainPot.mainPot -= wnr.amount;
            TotalTablePotAmount -= wnr.amount;
            SidePotAmountNew = mainPot;
            /*
        potChipsObj.transform.SetParent (cardsParentForInstantiatedCards, false);

        Vector3 fromPos = txtPotAmount.transform.parent.position;
        Vector3 toPos = DisplayRevertPlayer.txtChips.transform.position;

        UIManager.Instance.SoundManager.chips_moved_from_potClickOnce ();

        float a = 0;
        while (a < 1) {
            a += Time.deltaTime * 2;
            potChipsObj.transform.position = Vector3.Lerp (fromPos, toPos, a);

            yield return 0;
        }
        */

            //Destroy (potChipsObj.gameObject, .25f);		
            //DisplayRevertPlayer.BuyInAmount += wnr.amount;
            DisplayRevertPlayer.BuyInAmount = wnr.chips;
            yield return 0;
        }
    }

    private IEnumerator DisplayRevertPointFoldedAnimation(PokerGameWinner.Wnr wnr)
    {
        PlayerSidePot mainPot = SidePotAmountNew;
        mainPot.mainPot -= wnr.amount;
        SidePotAmountNew = mainPot;

        PokerPlayer DisplayRevertPlayer = GetPlayerById(wnr.playerId);

        if (DisplayRevertPlayer != null)
        {
            GameObject potChipsObj = Instantiate(txtPotAmount.transform.parent.gameObject) as GameObject;
            instantiatedObjList.Add(potChipsObj);
            potChipsObj.gameObject.SetActive(true);
            potChipsObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = wnr.amount.ToString();

            potChipsObj.transform.SetParent(cardsParentForInstantiatedCards, false);

            Vector3 fromPos = txtPotAmount.transform.parent.position;
            Vector3 toPos = DisplayRevertPlayer.txtChips.transform.position;

            UIManager.Instance.SoundManager.chips_moved_from_potClickOnce();

            float a = 0;
            while (a < 1)
            {
                a += Time.deltaTime * 2;
                potChipsObj.transform.position = Vector3.Lerp(fromPos, toPos, a);

                yield return 0;
            }

            Destroy(potChipsObj.gameObject);
            //DisplayRevertPlayer.BuyInAmount += wnr.amount;
            DisplayRevertPlayer.BuyInAmount = wnr.chips;
            yield return 0;
        }
    }

    private IEnumerator ShowPotAfterSomeTime(PlayerSidePot playerSidePot)

    {
        /*#if UNITY_WEBGL
        #else
                yield return new WaitForSeconds(0.7f);
        #endif*/
        yield return new WaitForSecondsRealtime(0.7f);
        SidePotAmountNew = playerSidePot;
    }

    private IEnumerator superplayercatdsopen(superPlayerCard onSuperPlayerCardsResp)
    {
        for (int i = 0; i < onSuperPlayerCardsResp.playersCards.Count; i++)
        {
            PokerPlayer plr = GetPlayerById(onSuperPlayerCardsResp.playersCards[i].playerId);
            if (plr != null)
            {
                plr.cards = onSuperPlayerCardsResp.playersCards[i].cards;

                plr.CloseAllHiddenCards();

                plr.Card1.SetAlpha(2);
                plr.Card2.SetAlpha(2);
                plr.Card1.Open();
                plr.Card2.Open();
                plr.CloseAllHiddenCards();
                plr.Card1.PlayAnimation(plr.cards[0]);
                plr.Card2.PlayAnimation(plr.cards[1]);
                //print("card1.a => " + plr.Card1.card.color.a);
                //print("card2.a => " + plr.Card2.card.color.a);
                if (currentRoomData.pokerGameType == PokerGameType.omaha.ToString())
                {
                    plr.Card3.SetAlpha(2);
                    plr.Card4.SetAlpha(2);
                    plr.Card3.Open();
                    plr.Card4.Open();
                    plr.CloseAllHiddenCards();
                    plr.Card3.PlayAnimation(plr.cards[2]);
                    plr.Card4.PlayAnimation(plr.cards[3]);
                    //print("card3.a => " + plr.Card3.card.color.a);
                    //print("card4.a => " + plr.Card4.card.color.a);
                }
                else if (currentRoomData.pokerGameType == PokerGameType.PLO5.ToString())
                {
                    //print("card3.a => " + plr.Card3.card.color.a);
                    //print("card4.a => " + plr.Card4.card.color.a);
                    //print("card5.a => " + plr.Card5.card.color.a);
                    plr.Card3.SetAlpha(2);
                    plr.Card4.SetAlpha(2);
                    plr.Card5.SetAlpha(2);
                    plr.Card3.Open();
                    plr.Card4.Open();
                    plr.Card5.Open();
                    plr.CloseAllHiddenCards();
                    plr.Card3.PlayAnimation(plr.cards[2]);
                    plr.Card4.PlayAnimation(plr.cards[3]);
                    plr.Card5.PlayAnimation(plr.cards[4]);
                }
            }

            yield return new WaitForSeconds(1.5f);
            plr.CloseAllHiddenCards();
        }
    }

    //Mobile Texas and Omaha player turn effect
    private IEnumerator OnNextTurnHighlightsPlayerEffect(string nextTurnPlayerId)
    {
        //foreach (PokerPlayer p in UIManager.Instance.GameScreeen.GamePlayers)
        foreach (var playerPlace in _playerPlaces)
        {
            var pokerPlayer = playerPlace.pokerPlayer;
            if (pokerPlayer.PlayerId.Equals(nextTurnPlayerId) && pokerPlayer.gameObject.activeSelf)
            {
                float turnoffset = 0f;
                while (turnoffset < 1f)
                {
                    turnoffset += Time.deltaTime * 6f;
                    var newRotation = Quaternion.LookRotation(currentTurnEffect.transform.position - pokerPlayer.GetProfilePicImage().transform.position, Vector3.back);
                    newRotation.x = 0.0f;
                    newRotation.y = 0.0f;
                    currentTurnEffect.transform.rotation = Quaternion.Slerp(currentTurnEffect.transform.rotation, newRotation, turnoffset);

                    currentTurnEffect.gameObject.GetComponent<Image>().enabled = true;
                    yield return 0;
                }
            }
        }
    }

    #endregion

    #region GETTER_SETTER

    public long MainPot1
    {
        set
        {
            txtMainPot1.text = value.ConvertToCommaSeparatedValue();
            txtMainPot1.gameObject.transform.parent.gameObject.SetActive(value > 0);
        }
    }

    public long MainPot2
    {
        set
        {
            txtMainPot2.text = value.ConvertToCommaSeparatedValue();
            txtMainPot2.gameObject.transform.parent.gameObject.SetActive(value > 0);
        }
    }

    public long SidePot1
    {
        set
        {
            txtSidePot1.text = value.ConvertToCommaSeparatedValue();
            txtSidePot1.gameObject.transform.parent.gameObject.SetActive(value > 0);
        }
    }

    public long SidePot2
    {
        set
        {
            txtSidePot2.text = value.ConvertToCommaSeparatedValue();
            txtSidePot2.gameObject.transform.parent.gameObject.SetActive(value > 0);
        }
    }

    public PokerGameRound CurrentRound
    {
        get { return _currentRound; }
        set { _currentRound = value; }
    }

    public int TurnTime
    {
        get { return _turnTime; }
        set { _turnTime = value; }
    }

    public double PotAmountValue
    {
        get { return _PotAmount; }
        set
        {
            _PotAmount = value;
            //			txtPotAmount.text =" Pot "+ _PotAmount.ConvertToCommaSeparatedValue ();
            //			txtPotAmount.text =" Pot "+ _PotAmount.ConvertDoubleDecimalsToValue();
            //			txtPotAmount.gameObject.SetActive (_PotAmount > 0);
            //			txtPotAmount.text = " Pot = "+_PotAmount.ConvertDoubleDecimalsToValue ();
            //			txtPotAmount.transform.parent.gameObject.SetActive (_PotAmount > 0);	
        }
    }

    public int GetSeatIndexForPlayer(int index)
    {
        //ownPlayerSeatIndex = 0;

        for (int i = 0; i < AllPokerPlayerInfo.playerInfo.Count; i++)
        {
            if (AllPokerPlayerInfo.playerInfo[i].id.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
            {
                ownPlayerSeatIndex = AllPokerPlayerInfo.playerInfo[i].seatIndex;
            }
        }

        int newSeatIndex = ownPlayerSeatIndex + (_playerPlaces.Count - index);
        if (newSeatIndex > _playerPlaces.Count - 1)
        {
            //	Debug.Log ("if new trrue");
            newSeatIndex = newSeatIndex - _playerPlaces.Count;
        }
        //Debug.Log ("Opponent index => " + ownPlayerSeatIndex);
        //Debug.Log ("Opponent index => " + newSeatIndex);

        return newSeatIndex;
    }

    public PokerPlayer GetOwnPlayer()
    {
        PokerPlayer pp = null;

        /* foreach (PokerPlayer p in GamePlayers)
         {
             if (p.PlayerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
                 pp = p;
         }*/

        foreach (var playerPlace in _playerPlaces)
        {
            var pokerPlayer = playerPlace.pokerPlayer;
            if (pokerPlayer.PlayerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
                pp = pokerPlayer;
        }

        return pp;
    }

    public PokerPlayer GetPlayerById(string playerId)
    {
        PokerPlayer plr = null;

        /*foreach (PokerPlayer p in GamePlayers)
        {
            if (p.PlayerId.Equals(playerId) &&
                p.gameObject.activeSelf)
                plr = p;
        }*/

        foreach (var playerPlace in _playerPlaces)
        {
            var pokerPlayer = playerPlace.pokerPlayer;
            if (pokerPlayer.PlayerId.Equals(playerId) &&
                pokerPlayer.gameObject.activeSelf)
                plr = pokerPlayer;
        }

        return plr;
    }

    public string GetPlayerNameById(string playerId)
    {
        string plrname = null;

        foreach (var playerPlace in _playerPlaces)
        {
            var pokerPlayer = playerPlace.pokerPlayer;
            if (pokerPlayer.PlayerId.Equals(playerId) &&
                pokerPlayer.gameObject.activeSelf)
                plrname = pokerPlayer.txtUsername.text.ToString();
        }
        /*foreach (PokerPlayer p in GamePlayers)
        {
            if (p.PlayerId.Equals(playerId) &&
                p.gameObject.activeSelf)
                plrname = p.txtUsername.text.ToString();
        }*/


        return plrname;
    }


    public PokerPlayer GetSeatIndexByPlayerId(int SeatId)
    {
        PokerPlayer plr = null;

        /*foreach (PokerPlayer p in GamePlayers)
        {
            if (p.SeatIndex == SeatId)
                plr = p;
        }*/

        foreach (var playerPlace in _playerPlaces)
        {
            var pokerPlayer = playerPlace.pokerPlayer;
            if (pokerPlayer.SeatIndex == SeatId)
                plr = pokerPlayer;
        }

        return plr;
    }

    private int getPotIndexFromPlayerIndex(int playerIndex)
    {
        int i = 0;
        if (SidePotAmountNew != null && SidePotAmountNew.sidePot.Count > 0)
        {
            foreach (PlayerSidePot.SidePot p in SidePotAmountNew.sidePot)
            {
                if (p.sidePotPlayerSeatIndex.Equals(playerIndex))
                {
                    return i;
                }

                i++;
            }
        }

        return 0;
    }

    private string getplayerBysidePotPlayerID(int playerIndex)
    {
        string Id = "";
        if (SidePotAmountNew != null && SidePotAmountNew.sidePot.Count > 0)
        {
            foreach (PlayerSidePot.SidePot p in SidePotAmountNew.sidePot)
            {
                if (p.sidePotPlayerSeatIndex.Equals(playerIndex))
                {
                    Id = p.sidePotPlayerID;
                    return Id;
                }
            }
        }

        return Id;
    }

    /*	private int getPotIndexFromPlayerIndex (int playerIndex)
    {
        int i = 0;
        if (SidePotAmountNew != null && SidePotAmountNew.sidePot.Count > 0) {
            foreach (PlayerSidePot.SidePot p in SidePotAmountNew.sidePot) {			
                if (p.sidePotPlayerSeatIndex.Equals (playerIndex)) {
                    return i;
                }
                i++;
            }	
        }
        return 0;
    }*/
    public int GetActivePlayersCount()
    {
        int activePlayers = 0;
        foreach (var playerPlace in _playerPlaces)
        {
            var pokerPlayer = playerPlace.pokerPlayer;
            if (pokerPlayer.gameObject.activeSelf && (pokerPlayer.status.Equals(PlayerStatus.Playing) || pokerPlayer.status == PlayerStatus.Allin))
                activePlayers++;
        }
        /*foreach (PokerPlayer plr in GamePlayers)
        {
            if (plr.gameObject.activeSelf && (plr.status.Equals(PlayerStatus.Playing) || plr.status == PlayerStatus.Allin))
                activePlayers++;
        }*/

        return activePlayers;
    }

    public int GetPlayerIndexByPlayerId(string playerId)
    {
        /*for (int i = 0; i < GamePlayers.Length; i++)
        {
            if (GamePlayers[i].PlayerId != null && GamePlayers[i].PlayerId.Equals(playerId))
            {
                return i;
            }
        }*/

        for (int i = 0; i < _playerPlaces.Count; i++)
        {
            var pokerPlayer = _playerPlaces[i].pokerPlayer;
            if (pokerPlayer.PlayerId != null && pokerPlayer.PlayerId.Equals(playerId))
            {
                return i;
            }
        }

        return -1;
    }

    public bool HasJoinedRoom
    {
        get { return HasJoin; }
        set { HasJoin = value; }
    }

    public PlayerSidePot SidePotAmountNew
    {
        get { return _sidePotAmountListNew; }
        set
        {
            _sidePotAmountListNew = value;
            //Debug.Log("-----------sidepot---");
            if (_sidePotAmountListNew.mainPot == 0)
            {
                txtPotAmount.text = "0";
                txtPotAmount.transform.parent.gameObject.SetActive(false);
            }

            if (_sidePotAmountListNew.sidePot == null)
            {
                foreach (var playerPlace in _playerPlaces)
                {
                    var pokerPlayer = playerPlace.pokerPlayer;
                    pokerPlayer.txtSidePot.text = "0";
                    pokerPlayer.txtSidePot.transform.parent.gameObject.SetActive(false);
                }

                /*for (int i = 0; i < GamePlayers.Length; i++)
                {
                    GamePlayers[i].txtSidePot.text = "0";
                    GamePlayers[i].txtSidePot.transform.parent.gameObject.SetActive(false);
                }*/
                return;
            }

            for (int i = 0; i < _sidePotAmountListNew.sidePot.Count; i++)
            {
                //PokerPlayer plr = GamePlayers[_sidePotAmountListNew.sidePot[i].sidePotPlayerSeatIndex];
                PokerPlayer plr = GetPlayerById(_sidePotAmountListNew.sidePot[i].sidePotPlayerID);

                _sidePotAmountListNew.sidePot[i].sidePotAmount = Math.Round(_sidePotAmountListNew.sidePot[i].sidePotAmount, 2);
                if (_sidePotAmountListNew.sidePot[i].sidePotAmount <= 0)
                {
                    plr.txtSidePot.text = "0";
                    plr.txtSidePot.transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    if (_sidePotAmountListNew.sidePot[i].sidePotAmount < 1)
                    {
                        plr.txtSidePot.text = _sidePotAmountListNew.sidePot[i].sidePotAmount.ToString();
                    }
                    else
                    {
                        plr.txtSidePot.text = _sidePotAmountListNew.sidePot[i].sidePotAmount.FormatNumberUS();
                    }

                    plr.txtSidePot.transform.parent.SetAsLastSibling();
                    plr.txtSidePot.transform.parent.gameObject.SetActive(_sidePotAmountListNew.sidePot[i].sidePotAmount > 0);
                    //plr.StartCoroutine (plr.ForceUpdateCanvasesNew ());
                }
            }

            if (_sidePotAmountListNew.mainPot == null || _sidePotAmountListNew.mainPot == 0)
            {
                txtPotAmount.text = "0";
                txtPotAmount.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                txtPotAmount.text = /*"Pot = " + */_sidePotAmountListNew.mainPot.ToString();
                txtPotAmount.transform.parent.gameObject.SetActive(_sidePotAmountListNew.mainPot > 0);
            }
        }
    }

    public double TotalTablePotAmount
    {
        get { return _totalTablePotAmount; }
        set
        {
            //print("TotalTablePotAmount:" + value);
            _totalTablePotAmount = value;
            txtTotalTablePotAmount.transform.parent.gameObject.SetActive(value > 0);
            txtTotalTablePotAmount.text = "Pot:" + value.ConvertToCommaSeparatedValue();
        }
    }

    public string GameName
    {
        set
        {
            if (value == "")
            {
                txtGameName.Close();
            }
            else
            {
                txtGameName.Open();
                //txtGameName.text = value;
                Utility.Instance.CheckHebrewOwn(txtGameName, value);
            }
        }
    }

    public string GameId
    {
        set { _gameId = value; }
        get { return _gameId; }
    }

    public double OldPlayerBuyIn
    {
        set { _oldPlayerBuyIn = value; }
        get { return _oldPlayerBuyIn; }
    }

    public string tournamentPlayersNumbers
    {
        set
        {
            _tournamentPlayers = value;

            if (value.Length <= 0)
            {
                txtplayersPlayingTournament.Close();
            }
            else
            {
                txtplayersPlayingTournament.text = "Players : " + value;
                txtplayersPlayingTournament.Open();
            }
        }
        get { return _tournamentPlayers; }
    }

    public string BreakLevelRank
    {
        set
        {
            _BreakLevel = value;

            if (value.Length <= 0)
            {
                txtbreakTournamentTimer.Close();
            }
            else
            {
                txtbreakTournamentTimer.text = "BREAK : " + value;
                txtbreakTournamentTimer.Open();
            }
        }
        get { return _BreakLevel; }
    }

    public string BlindLevelRank
    {
        set
        {
            _BlindLevel = value;

            if (value.Length == 0)
            {
                txtBlindLevel.Close();
            }
            else
            {
                if (value.Length > 0)
                {
                    txtBlindLevel.text = "LEVEL : " + value;
                    txtBlindLevel.Open();
                }
                else
                {
                    txtBlindLevel.Close();
                }
            }
        }
        get { return _BlindLevel; }
    }

    public string PreviousGameNumber
    {
        set
        {
            _previousGameNumber = value;

            if (value.Length == 0)
            {
                txtPreviousGameNumber.Close();
            }
            else
            {
                if (value.Length > 0)
                {
                    txtPreviousGameNumber.text = value;
                    txtPreviousGameNumber.Open();
                }
                else
                {
                    txtPreviousGameNumber.Close();
                }
            }
        }
        get { return _previousGameNumber; }
    }

    public string PreviousGameId
    {
        set { _previousGameId = value; }
        get { return _previousGameId; }
    }

    public bool ShowBetPanelOption
    {
        set { showBetPanel = value; }
    }

    public bool WaitForBigBlindCheckbox
    {
        set
        {
            print("WaitForBigBlindCheckbox value: " + value);
            toggleWaitForBigBlind.gameObject.SetActive(value);
            if (value)
            {
                preBetButtonsPanel.toggleSitOutNextBigBlind.gameObject.SetActive(false);
                preBetButtonsPanel.toggleSitOutNextHand.gameObject.SetActive(false);
            }
        }
        get { return toggleWaitForBigBlind.gameObject.activeSelf; }
    }

    public bool WaitForBigBlindCheckboxValue
    {
        set
        {
            print("WaitForBigBlindCheckboxValue value: " + value);
            toggleWaitForBigBlind.isOn = value;
        }
        get { return toggleWaitForBigBlind.isOn; }
    }

    public string TournamentBreakTableMessage
    {
        set
        {
            txtTournamentBreakTableMessage.text = value;
            if (value == "")
            {
                txtTournamentBreakTableMessage.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                txtTournamentBreakTableMessage.transform.parent.gameObject.SetActive(true);
            }
        }
    }

    /*public double Chips
    {
        set
        {
            txtTotalChips.text = value.ConvertToCommaSeparatedValueColor();
            txtTotalChips.transform.parent.gameObject.SetActive(value > 0);
        }
    }*/
    /* private bool IsLetterValid()
     {
         string str = inputfieldChat.text;
         string pattern = @"[\p{IsHebrew} ]+";
         var hebrewMatchCollection = Regex.Matches(str, pattern);
         string hebrewPart = string.Join(" ", hebrewMatchCollection.Cast<Match>().Select(m => m.Value));  //combine regex collection
         var englishPart = Regex.Split(str, pattern).Last();
         Debug.Log("hebrew lang : " + hebrewPart);
         PokerPlayer plr = GetPlayerById(OnChatdataResp.playerId);

         if (hebrewPart != "")
         {
             if (plr != null)
             {
                 Debug.Log("Hebrew==========");
         }
         else
         {
             Debug.Log("other============");
         }
         return true;
         /*     Regex regex = new Regex("^([u0590-\u05FF !@#$%^&*()\\-=+_`~,./';<>?{}])*$");//heb
              Match match = regex.Match(str);
              if (!match.Success)
              {
                  Debug.Log("match");
                  return false;
              }
              return true;

         //  str = Regex.IsMatch(inputfieldChat.Text, @"[\u05D0\u05D1\u05D2\u05D3\u05D4\u05D5\u05D6\u05D7\u05D8\u05D9\u05DA\u05DB\u05DC\u05DD\u05DE\u05DF\u05E0\u05E1\u05E2\u05E3\u05E4\u05E5\u05E6\u05E7\u05E8\u05E9\u05EA]");
         // Regex regex = new Regex("^\p[{Hebrew}| ]{2,15}? \+$u");

         //  return true;

     }*/
    private bool IsLetterValid(string strmsg, string pid)
    {
        string str = strmsg;
        string pattern = @"[\p{IsHebrew}]+";
        var hebrewMatchCollection = Regex.Matches(str, pattern);
        string hebrewPart = string.Join(" ", hebrewMatchCollection.Cast<Match>().Select(m => m.Value)); //combine regex collection
        var englishPart = Regex.Split(str, pattern).Last();

        PokerPlayer plr = GetPlayerById(pid);
        if (hebrewPart != "")
        {
            if (plr != null)
            {
                plr.txtChatMessage.alignment = TextAnchor.MiddleRight;
                plr.txtChatMessage.text = str;
                Canvas.ForceUpdateCanvases();
                string a = "";

                for (int i = 0; i < plr.txtChatMessage.cachedTextGenerator.lines.Count; i++)
                {
                    int startIndex = plr.txtChatMessage.cachedTextGenerator.lines[i].startCharIdx;
                    int endIndex = (i == plr.txtChatMessage.cachedTextGenerator.lines.Count - 1)
                        ? plr.txtChatMessage.text.Length
                        : plr.txtChatMessage.cachedTextGenerator.lines[i + 1].startCharIdx;
                    int length = endIndex - startIndex;
                    Debug.Log(plr.txtChatMessage.text.Substring(startIndex, length));
                    a = plr.txtChatMessage.text.Substring(startIndex, length) + "\n" + a;
                }

                plr.txtChatMessage.text = a;
            }
        }
        else
        {
            {
                if (plr != null)
                {
                    plr.txtChatMessage.alignment = TextAnchor.MiddleLeft;
                    plr.txtChatMessage.text = str + "\n";
                }
            }
        }

        return true;
    }

    #endregion
}