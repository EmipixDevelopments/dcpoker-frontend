using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BestHTTP.SocketIO;

public class PokerPlayer : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    [Header("Boolean")]
    public bool isLeftProfilePanel;

    [Header("Other")]
    public CanvasGroup canvasGroup;
    public List<string> cards;

    [Header("Gamobjects")]
    public PokerCard Card1;
    public PokerCard Card2;
    public PokerCard Card3;
    public PokerCard Card4;
    public PokerCard Card5;
    public PokerCard HiddenCard1;
    public PokerCard HiddenCard2;
    public PokerCard HiddenCard3;
    public PokerCard HiddenCard4;
    public PokerCard HiddenCard5;
    public PokerCard ShowCard1;
    public PokerCard ShowCard2;
    public PokerCard ShowCard3;
    public PokerCard ShowCard4;
    public PokerCard ShowCard5;
    private List<GameObject> instantiatedObjectsList = new List<GameObject>();
    //[Header ("Transforms")]

    //public Transform TableParent;
    [Header("ScripatbleObject")]
    public ActionPlayer PlayerActionStatus;
    public PokerPlayerBetValue Bets;
    public ContentSizeFitter contentSizeFitter;
    [Header("Images")]
    public Image ProfilePic;
    public Image TurnPlayer;
    public Image Dealer;
    public Image twiceIcon;
    public Image StraddleIcon;
    public Image DetailObj;
    public Sprite[] DetailObjsSprites;
    [Header("Text")]
    public TextMeshProUGUI txtUsername;
    public TextMeshProUGUI txtChips;
    public TextMeshProUGUI txtSidePot;
    public TextMeshProUGUI txtPotAmount;
    public PlayerInfoList.PlayerInfos playerInfo;
    public WinnerPlayer Winner;
    public PlayerStatus status;
    [Header("String")]
    public string PlayerId;
    public bool isDealer;
    //[Header ("Prefabs")]
    //public TableData tables;

    [Header("Chat")]
    public GameObject chatBubble;
    public GameObject AllinObj;
    public GameObject FoldObj;
    public GameObject RaiseObj;
    public CanvasGroup chatCanvasGroup;
    public Text txtChatMessage;
    #endregion

    #region PRIVATE_VARIABLES

    [Header("Private")]
    public double _betAmount;
    public double _buyInAmount;
    public int SeatIndex;
    private bool isTurnTimerRunning = false;
    private PlayerSidePot _sidePotAmountListNew;
    private long _sidePotAmount;
    [Header("Timer Variables")]
    public float maxTimer;
    public float timer;
    #endregion

    #region UNITY_CALLBACKS

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            TurnPlayer.fillAmount = 1f - (timer / maxTimer);
        }
    }

    void Start()
    {
        SetButtonOnProfilePicture(ProfilePic.gameObject);
    }

    // Use this for initialization
    // Update is called once per frame
    void OnEnable()
    {
        //SetCardPositions();
        DestroyAllInstantiatedObjects();
        ResetData();
        Game.Lobby.CashSocket.On(Constants.PokerEvents.PlayerCards, OnPlayerCardsDataReceived);
        //Game.Lobby.CashSocket.On(Constants.PokerEvents.PlayerAction, OnPlayerActionReceived);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.RoundComplete, OnRoundComplete);
        //Game.Lobby.CashSocket.On(Constants.PokerEvents.NextTurnPlayer, OnNextTurnPlayer);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.GameFinished, OnGameFinished);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.ResetGame, OnPokerResetGame);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.OnTurnTimer, OnTurnTimerRecieved);
        Game.Lobby.CashSocket.On(Constants.PokerEvents.GameStarted, OnGameStarted);
        TurnPlayer.fillAmount = 0;
        TurnPlayer.Close();
        twiceIcon.Close();
        StraddleIcon.Close();

        CloseShowCards();
        chatCanvasGroup.alpha = 0;
    }

    void OnDisable()
    {
        cards = new List<string>();
        status = PlayerStatus.None;
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.PlayerCards, OnPlayerCardsDataReceived);
        //Game.Lobby.CashSocket.Off(Constants.PokerEvents.PlayerAction, OnPlayerActionReceived);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.RoundComplete, OnRoundComplete);
        //Game.Lobby.CashSocket.Off(Constants.PokerEvents.NextTurnPlayer, OnNextTurnPlayer);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.GameFinished, OnGameFinished);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.ResetGame, OnPokerResetGame);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.OnTurnTimer, OnTurnTimerRecieved);
        Game.Lobby.CashSocket.Off(Constants.PokerEvents.GameStarted, OnGameStarted);
        TurnPlayer.fillAmount = 0;
        TurnPlayer.Close();
        twiceIcon.Close();
        StraddleIcon.Close();
        DestroyAllInstantiatedObjects();
        chatCanvasGroup.alpha = 0;
        StopCoroutine("DisplayChatMessageCoroutine");
    }


    #endregion

    #region DELEGATE_CALLBACKS

    void TexasGameManager_onResetData()
    {
        //imgWinner.Close();
        //txtWinnerHandRank.text = "";
        Dealer.Close();
    }

    void TexasGameManager_setPlayerStatus()
    {
        if (playerInfo.folded)
            BetAmount = 0;
    }

    private void OnPlayerCardsDataReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] arg)
    {
        if (!gameObject.activeSelf)
            return;

        JSONArray arr = new JSONArray(packet.ToString());
        var resp = arr.getString(arr.length() - 1);

        PokerPlayerCards playerCards = JsonUtility.FromJson<PokerPlayerCards>(resp);

        if (playerCards.roomId != UIManager.Instance.GameScreeen.currentRoomData.roomId && playerCards.roomId.Length != 0)
            return;

        UIManager.Instance.GameScreeen.currentRoomData.smallBlind = playerCards.smallBlindChips;
        UIManager.Instance.GameScreeen.currentRoomData.bigBlind = playerCards.bigBlindChips;

        for (int i = 0; i < playerCards.players.Count; i++)
        {
            if (playerCards.players[i].playerId.Equals(this.PlayerId))
            {
                this.playerInfo.cards = playerCards.players[i].cards;
                this.status = PlayerStatus.Playing;
                this.playerInfo.status = PlayerStatus.Playing.ToString();
                this.BuyInAmount = playerCards.players[i].chips;
            }
        }

        //		isDealer = playerCards.dealerPlayerId.Equals(this.PlayerId);
        //		Dealer.gameObject.SetActive(isDealer);
        //		if (playerCards.smallBlindPlayerId.Equals(this.PlayerId))
        //			{
        //			BetAmount = playerCards.smallBlindChips;
        //			}
        //		if (playerCards.bigBlindPlayerId.Equals (this.PlayerId)) 
        //		{
        //			BetAmount = playerCards.bigBlindChips;
        //		}
        //		if (playerCards.smallBlindPlayerId.Equals(this.PlayerId) &&
        //			!HistoryManager.GetInstance().HasAnyHistoryOfAction(PokerPlayerAction.SmallBlind, UIManager.Instance.GameScreeen.CurrentRound))
        //		{
        //			SetLastActionPerformed(PokerPlayerAction.Call, BetAmount, false);
        //
        //			HistoryManager.GetInstance().AddHistory(this.PlayerId, playerInfo.username, UIManager.Instance.GameScreeen.CurrentRound, BetAmount, BetAmount, PokerPlayerAction.SmallBlind, false);
        //		}

        //		print ("--------------------------------------------- BigBlind: " + HistoryManager.GetInstance().HasAnyHistoryOfAction(PokerPlayerAction.BigBlind, UIManager.Instance.GameScreeen.CurrentRound));
        //		if (playerCards.bigBlindPlayerId.Equals(this.PlayerId) &&
        //			!HistoryManager.GetInstance().HasAnyHistoryOfAction(PokerPlayerAction.BigBlind, UIManager.Instance.GameScreeen.CurrentRound))
        //		{
        ////			print ("--------------------------------------------- BigBlind");
        //			//BetAmount = playerCards.bigBlindChips;
        //
        //			//BuyInAmount -= playerCards.bigBlindChips;
        //
        //			SetLastActionPerformed(PokerPlayerAction.Call, BetAmount, false);
        //
        //			HistoryManager.GetInstance().AddHistory(this.PlayerId, playerInfo.username, UIManager.Instance.GameScreeen.CurrentRound, BetAmount, BetAmount, PokerPlayerAction.BigBlind, false);
        //		}
    }

    //	private void OnPlayerActionReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] arg)
    //	{
    //
    //	}

    private void OnRoundComplete(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] arg)
    {
        if (!gameObject.activeSelf)
            return;
        JSONArray arr = new JSONArray(packet.ToString());
        var resp = arr.getString(arr.length() - 1);
        RoundComplete round = JsonUtility.FromJson<RoundComplete>(resp);

        if (round.roomId != UIManager.Instance.GameScreeen.currentRoomData.roomId && round.roomId.Length != 0)
            return;

        if (status.Equals(PlayerStatus.Fold.ToString()))
        {
            //			imgCheckFold.gameObject.SetActive (true);
            //			imgCheckFold.sprite = spFold;
        }

        //		var resp = JSON.Parse (packet.ToString());

        if (BetAmount > 0)
        {
            StartCoroutine(TransferBetAmountToPot(round.PlayerSidePot.mainPot));
        }
        StopTurnTimerAnimation();

    }

    private void OnNextTurnPlayer(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] arg)
    {
        if (!gameObject.activeSelf)
            return;

        JSONArray arr = new JSONArray(packet.ToString());

        JSON_Object nextTurn = new JSON_Object(arr.getString((arr.length() - 1)));
        string nextTurnPlayerId = nextTurn.getString("nextTurnPlayerId");

        if (!nextTurn.isNull("restartTimer"))
        {
            isTurnTimerRunning = false;
        }

        if (nextTurnPlayerId.Equals(this.PlayerId))
        {
            DisplayTurnTimerAnimation();
        }
        else
        {
            StopTurnTimerAnimation();
        }

        if (playerInfo != null && playerInfo.cards != null && playerInfo.cards.Count > 0)
        {
            if (PlayerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
            {

                CloseAllHiddenCards();

                Card1.Open();
                Card2.Open();

                Card1.DisplayCardWithoutAnimation(playerInfo.cards[0]);
                Card1.card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, 0, true);

                Card2.DisplayCardWithoutAnimation(playerInfo.cards[1]);
                Card2.card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, 0, true);

                if (playerInfo.cards.Count == 4)
                {
                    Card3.Open();
                    Card4.Open();

                    Card3.DisplayCardWithoutAnimation(playerInfo.cards[2]);
                    Card3.card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, 0, true);

                    Card4.DisplayCardWithoutAnimation(playerInfo.cards[3]);
                    Card4.card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, 0, true);
                }
            }
        }
    }

    private void OnGameFinished(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] arg)
    {

        if (!gameObject.activeSelf)
            return;
        //		ScaleDown ();

        RoundComplete round = JsonUtility.FromJson<RoundComplete>(Utility.Instance.GetPacketString(packet));

        if (round.roomId != UIManager.Instance.GameScreeen.currentRoomData.roomId && round.roomId.Length != 0)
            return;

        StopTurnTimerAnimation();

        if (playerInfo == null)
            return;

        //		Debug.Log ("player name " + playerInfo.username + " status " + status);
        //		if (status == PlayerStatus.Allin || status == PlayerStatus.Playing) {
        //			if (UIManager.Instance.GameScreeen.GetActivePlayersCount () > 1) {
        //				Card1.Open ();
        ////					Card1.DisplayCardWithoutAnimation(playerInfo.cards[0]);
        //				Card1.card.CrossFadeAlpha (Constants.Poker.MatchedCardAlpha, 0, true);
        //
        //				Card2.Open ();
        ////					Card2.DisplayCardWithoutAnimation(playerInfo.cards[1]);
        //				Card2.card.CrossFadeAlpha (Constants.Poker.MatchedCardAlpha, 0, true);				 
        //			}
        //		}
    }

    private void OnPokerResetGame(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] arg)
    {
        if (!gameObject.activeSelf)
            return;
        //		Debug.Log("----------Poker Reset Game---------- " + packet.ToString());

        ResetGame resetGame = JsonUtility.FromJson<ResetGame>(Utility.Instance.GetPacketString(packet));

        if (resetGame.roomId != UIManager.Instance.GameScreeen.currentRoomData.roomId && resetGame.roomId.Length != 0)
            return;

        BetAmount = 0;
        Card1.ResetImage();
        Card2.ResetImage();
        Card3.ResetImage();
        Card4.ResetImage();
        Card5.ResetImage();
        twiceIcon.Close();
        StraddleIcon.Close();
        ResetImageAllHiddenCards();

        CloseAllOpenCards();

        CloseAllHiddenCards();

        Winner.IsAnimationOn(false);
        IsAnimationOn(false);

        DestroyAllInstantiatedObjects();
        //			txtWinnerHandRank.transform.parent.gameObject.SetActive(false);
        //			txtWinnerHandRank.text = "";
        //			imgWinner.CrossFadeAlpha(0, 0, true);
        //			txtWinnerHandRank.CrossFadeAlpha(0, 0, true);


        //imgSidePotAmount.Close();
        //imgSidePotAmount.gameObject.SetActive(false);
    }

    private void OnGameStarted(Socket scoket, Packet packet, params object[] args)
    {
        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;

        JSON_Object gameStartedObj = new JSON_Object(resp);

        if (gameStartedObj.has("roomId") && gameStartedObj.getString("roomId") != UIManager.Instance.GameScreeen.currentRoomData.roomId)
            return;

        HiddenCard1.SetBackCard();
        HiddenCard2.SetBackCard();
        HiddenCard3.SetBackCard();
        HiddenCard4.SetBackCard();
        HiddenCard5.SetBackCard();

        Card1.SetBackCard();
        Card2.SetBackCard();
        Card3.SetBackCard();
        Card4.SetBackCard();
        Card5.SetBackCard();
    }

    private void OnTurnTimerRecieved(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] arg)
    {
        if (!gameObject.activeSelf)
            return;
        //		print("---------- OnTurnTimerRecieved ---------- " + packet.ToString());
    }


    #endregion

    #region PUBLIC_METHODS

    public void StartTimer(float timer, float maxTimer)
    {
        this.timer = timer;
        this.maxTimer = maxTimer;
    }

    public void IsAnimationOn(bool IsOn)
    {
        if (IsOn)
        {
            //			txtUsername.Close ();
            Winner.IsAnimationOn(IsOn);
            Invoke("CloseAnimation", 2f);
        }
        else
        {
            txtUsername.Open();
            Winner.IsAnimationOn(IsOn);
        }
    }
    // public void AllInAnimation()
    public void AllInAnimation()
    {
        AllinObj.SetActive(true);
        Invoke("CloseAllInAnimation", 2f);
    }

    public void FoldObjAnimation()
    {
        FoldObj.SetActive(true);
        Invoke("CloseFoldAnimation", 2f);
    }
    public void RaiseAnimation()
    {
        RaiseObj.SetActive(true);
        Invoke("CloseRaiseAnimation", 2f);
    }

    public void CloseAllHiddenCards()
    {
        HiddenCard1.Close();
        HiddenCard2.Close();
        HiddenCard3.Close();
        HiddenCard4.Close();
        HiddenCard5.Close();
    }

    public void ResetImageAllHiddenCards()
    {
        HiddenCard1.ResetImage();
        HiddenCard2.ResetImage();
        HiddenCard3.ResetImage();
        HiddenCard4.ResetImage();
        HiddenCard5.ResetImage();
    }

    public void CloseAllOpenCards()
    {
        Card1.Close();
        Card2.Close();
        Card3.Close();
        Card4.Close();
        Card5.Close();
    }

    public void DisplayTurnTimerAnimation()
    {
        if (isTurnTimerRunning)
            return;

        //StopCoroutine("DisplayTurnTimer");
        //StartCoroutine ("DisplayTurnTimer");
    }

    public void StopTurnTimerAnimation()
    {
        TurnPlayer.fillAmount = 0;
        TurnPlayer.Close();
        isTurnTimerRunning = false;
        //	StopCoroutine("DisplayTurnTimer");*/
    }

    public void MoveCardsToDealer()
    {
        //		StartCoroutine (MoveFoldedCardsToDealer ());
        /*Card1.ResetImage ();
		Card1.Close ();

		Card2.ResetImage ();
		Card2.Close ();		

		Card3.ResetImage ();
		Card3.Close ();		

		Card4.ResetImage ();
		Card4.Close ();
		Card5.ResetImage();
		Card5.Close();*/
        print("alpha => ");
        Card1.SetAlpha(Constants.Constants.foldedCardsAlpha);
        Card2.SetAlpha(Constants.Constants.foldedCardsAlpha);
        Card3.SetAlpha(Constants.Constants.foldedCardsAlpha);
        Card4.SetAlpha(Constants.Constants.foldedCardsAlpha);
        Card5.SetAlpha(Constants.Constants.foldedCardsAlpha);
        ResetImageAllHiddenCards();
        CloseAllHiddenCards();
    }

    public void PlayerActionAnimation(string text)
    {
        txtUsername.Close();
        PlayerActionStatus.Open();
        PlayerActionStatus.txtAction.text = text;
        Invoke("CloseAction", 1.5f);
    }

    void CloseAction()
    {
        txtUsername.Open();
        PlayerActionStatus.Close();
    }

    public void SetLastActionPerformed(PokerPlayerAction action, double betAmount, bool hasRaised)
    {
        switch (action)
        {
            case PokerPlayerAction.Allin:
                //SoundManager.Instance.PlayAllinSound();
                //			txtLastActionPerformed.text = "All in";
                //			imgBetIcon.sprite = UIManager.Instance.pokerGamePanel.spRaised;
                //			txtBetAmount.color = Color.yellow;
                break;
            case PokerPlayerAction.Bet:
                //SoundManager.Instance.PlayChipSound();
                //			txtLastActionPerformed.text = "Called";
                //			imgBetIcon.sprite = UIManager.Instance.pokerGamePanel.spCall;
                //			txtBetAmount.color = Color.green;
                break;
            case PokerPlayerAction.Call:
                //SoundManager.Instance.PlayChipSound();
                if (betAmount == 0)
                {
                    //				txtLastActionPerformed.text = "Checked";
                    //				imgBetIcon.sprite = UIManager.Instance.pokerGamePanel.spCheck;
                    //				txtBetAmount.color = Color.green;
                }
                else
                {
                    //				txtLastActionPerformed.text = "Called";
                    //				imgBetIcon.sprite = UIManager.Instance.pokerGamePanel.spCall;
                    //				txtBetAmount.color = Color.green;
                }
                break;
            case PokerPlayerAction.Fold:
                //SoundManager.Instance.PlayFoldSound();
                //			imgFold.Open ();
                //			txtLastActionPerformed.text = "Folded";
                //			imgBetIcon.sprite = UIManager.Instance.pokerGamePanel.spFold;
                //			txtBetAmount.color = Color.red;
                break;
            default:
                //			txtLastActionPerformed.text = "";
                break;
        }

        if (hasRaised)
        {
            //			txtLastActionPerformed.text = "Raised";
            //			imgBetIcon.sprite = UIManager.Instance.pokerGamePanel.spRaised;
            //			txtBetAmount.color = Color.yellow;
        }

        if (action == PokerPlayerAction.Call && betAmount == 0)
        {
            //			imgCheckFold.gameObject.SetActive (true);
            //			imgCheckFold.sprite = spCheck;
        }
        else if (action == PokerPlayerAction.Fold && BetAmount == 0)
        {
            //			imgCheckFold.gameObject.SetActive (true);
            //			imgCheckFold.sprite = spFold;
        }
        TurnPlayer.fillAmount = 0;
        TurnPlayer.Close();
    }

    public void HighlightWinningCards(List<string> bestCards)
    {
        if (bestCards.Contains(Card1.currentCard))
        {
            Card1.PlayAnimation(Card1.currentCard);
            Card1.card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, .25f, true);
        }
        else
        {
            Card1.card.CrossFadeAlpha(Constants.Poker.NotMatchedCardAlpha, .25f, true);
        }

        if (bestCards.Contains(Card2.currentCard))
        {
            Card2.card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, .25f, true);
            Card2.PlayAnimation(Card2.currentCard);
        }
        else
        {
            Card2.card.CrossFadeAlpha(Constants.Poker.NotMatchedCardAlpha, .25f, true);
        }

        if (UIManager.Instance.GameScreeen.currentRoomData.pokerGameType == PokerGameType.omaha.ToString())
        {
            if (bestCards.Contains(Card3.currentCard))
            {
                Card3.PlayAnimation(Card3.currentCard);
                Card3.card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, .25f, true);
            }
            else
            {
                Card3.card.CrossFadeAlpha(Constants.Poker.NotMatchedCardAlpha, .25f, true);
            }

            if (bestCards.Contains(Card4.currentCard))
            {
                Card4.card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, .25f, true);
                Card4.PlayAnimation(Card4.currentCard);
            }
            else
            {
                Card4.card.CrossFadeAlpha(Constants.Poker.NotMatchedCardAlpha, .25f, true);
            }
            if (bestCards.Contains(Card5.currentCard))
            {
                Card5.card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, .25f, true);
                Card5.PlayAnimation(Card5.currentCard);
            }
            else
            {
                Card5.card.CrossFadeAlpha(Constants.Poker.NotMatchedCardAlpha, .25f, true);
            }
        }

        foreach (PokerCard pc in UIManager.Instance.GameScreeen.TableCards)
        {
            if (bestCards.Contains(pc.currentCard))
            {
                pc.card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, .25f, true);
            }
            else
            {
                pc.card.CrossFadeAlpha(Constants.Poker.NotMatchedCardAlpha, .25f, true);
            }
        }
        foreach (PokerCard pc in UIManager.Instance.GameScreeen.TableExtraCards)
        {
            if (bestCards.Contains(pc.currentCard))
            {
                pc.card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, 8f, true);
                pc.card.transform.position = new Vector2(pc.card.transform.position.x, pc.card.transform.position.y + 0.2f);
            }
            else
            {
                pc.card.CrossFadeAlpha(Constants.Poker.NotMatchedCardAlpha, 8f, true);
            }
        }
    }
    public void HighlighUpWinningCards(List<string> bestCards)
    {
        StartCoroutine(DisplayUpCards(bestCards, 3f));
    }
    public void OpenShowCards(List<string> cards, float time)
    {
        CloseAllHiddenCards();

        ShowCard1.SetBackCard();
        ShowCard2.SetBackCard();
        ShowCard1.Open();
        ShowCard2.Open();
        ShowCard1.DisplayCardWithoutAnimation(cards[0]);
        ShowCard2.DisplayCardWithoutAnimation(cards[1]);

        if (cards.Count == 4)
        {
            ShowCard3.SetBackCard();
            ShowCard4.SetBackCard();
            ShowCard3.Open();
            ShowCard4.Open();
            ShowCard3.DisplayCardWithoutAnimation(cards[2]);
            ShowCard4.DisplayCardWithoutAnimation(cards[3]);
        }
        if (cards.Count == 5)
        {
            ShowCard3.SetBackCard();
            ShowCard4.SetBackCard();
            ShowCard5.SetBackCard();
            ShowCard3.Open();
            ShowCard4.Open();
            ShowCard5.Open();
            ShowCard3.DisplayCardWithoutAnimation(cards[2]);
            ShowCard4.DisplayCardWithoutAnimation(cards[3]);
            ShowCard5.DisplayCardWithoutAnimation(cards[4]);
        }

        Invoke("CloseShowCards", time);
    }

    public void CloseNormalCards()
    {
        Card1.Close();
        Card2.Close();
        Card3.Close();
        Card4.Close();
        Card5.Close();
    }

    public void CloseHiddenCards()
    {
        HiddenCard1.Close();
        HiddenCard2.Close();
        HiddenCard3.Close();
        HiddenCard4.Close();
        HiddenCard5.Close();
    }

    public void CloseShowCards()
    {
        ShowCard1.Close();
        ShowCard2.Close();
        ShowCard3.Close();
        ShowCard4.Close();
        ShowCard5.Close();
        ShowCard1.SetBackCard();
        ShowCard2.SetBackCard();
        ShowCard3.SetBackCard();
        ShowCard4.SetBackCard();
        ShowCard5.SetBackCard();
    }

    public Vector3 GetCard1Position()
    {
        return Card1.transform.position;
    }

    public Vector3 GetCard2Position()
    {
        return Card2.transform.position;
    }

    public Vector3 GetCard3Position()
    {
        return Card3.transform.position;
    }

    public Vector3 GetCard4Position()
    {
        return Card4.transform.position;
    }
    public Vector3 GetCard5Position()
    {
        return Card5.transform.position;
    }

    public Vector3 GetCard1EulerAngles()
    {
        return Card1.transform.eulerAngles;
    }

    public Vector3 GetCard2EulerAngles()
    {
        return Card2.transform.eulerAngles;
    }

    public Vector3 GetCard3EulerAngles()
    {
        return Card3.transform.eulerAngles;
    }

    public Vector4 GetCard4EulerAngles()
    {
        return Card4.transform.eulerAngles;
    }
    public Vector4 GetCard5EulerAngles()
    {
        return Card5.transform.eulerAngles;
    }

    public void showChatBubble(string playerId, string message)
    {
        if (playerId.Equals(this.PlayerId))
        {
            if (message != null)
            {
                //string message = chatMessageReceivedResponse.message;
                /*if (message.Length > 75)
                {
                    message = message.Substring(0, 75);// + "...";
                }*/

                txtChatMessage.color = Color.white;
                chatBubble.GetComponent<VerticalLayoutGroup>().enabled = false;
                Canvas.ForceUpdateCanvases();
                chatBubble.GetComponent<VerticalLayoutGroup>().enabled = true;

                if (gameObject.activeSelf)
                    StopCoroutine("DisplayChatMessageCoroutine");
                if (gameObject.activeSelf)
                    StartCoroutine("DisplayChatMessageCoroutine");
            }
        }
    }
    #endregion

    #region PRIVATE_METHODS
    public void CloseAnimation()
    {
        txtUsername.Open();
        Winner.CloseAnimation();
    }
    public void CloseFoldAnimation()
    {
        FoldObj.SetActive(false);
    }
    public void CloseAllInAnimation()
    {
        AllinObj.SetActive(false);
    }

    public void CloseRaiseAnimation()
    {
        RaiseObj.SetActive(false);
    }
    void ResetData()
    {
        cards = new List<string>();
        canvasGroup.alpha = 1f;
        status = PlayerStatus.None;

        CloseAllOpenCards();

        CloseAllHiddenCards();

        Dealer.Close();
        BetAmount = 0;
        isDealer = false;
        isTurnTimerRunning = false;
        Winner.IsAnimationOn(false);
        AllinObj.SetActive(false);
        FoldObj.SetActive(false);
        IsAnimationOn(false);
        txtSidePot.transform.parent.gameObject.SetActive(false);
    }

    void DestroyAllInstantiatedObjects()
    {
        foreach (GameObject go in instantiatedObjectsList)
        {
            if (go != null)
                Destroy(go);
        }
        instantiatedObjectsList.Clear();
    }

    private void SetButtonOnProfilePicture(GameObject gameObject)
    {
        gameObject.AddComponent<Button>();
        Button profilePicButton = gameObject.GetComponent<Button>();
        profilePicButton.transition = Selectable.Transition.None;
        profilePicButton.onClick.AddListener(CallFoldedCardAPI);
    }

    private void CallFoldedCardAPI()
    {
        if (PlayerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
        {
            print("CallFoldedCardAPI()");
            string gameId = UIManager.Instance.GameScreeen.GameId;

            if (gameId.Length == 0)
                return;

            UIManager.Instance.SocketGameManager.ShowFoldedPlayerCards(gameId, (socket, packet, args) =>
            {
                print("ShowFoldedPlayerCards response: " + packet.ToString());

                PokerEventResponse<PlayerCards> playerCards = JsonUtility.FromJson<PokerEventResponse<PlayerCards>>(Utility.Instance.GetPacketString(packet));
                if (playerCards.result.roomId == "" || playerCards.result.roomId == UIManager.Instance.GameScreeen.currentRoomData.roomId)
                {
                    if (playerCards.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                    {
                        OpenShowCards(playerCards.result.cards, 2);
                    }
                }
            });
        }
    }

    private void SetCardPositions()
    {
        if (UIManager.Instance.GameScreeen.currentRoomData.pokerGameType == PokerGameType.texas.ToString())
        {
            ////// Card 2
            if (isLeftProfilePanel == true)
            {
                //Left
                Card1.transform.localPosition = new Vector3(0f, 17.78f, 0);
                Card2.transform.localPosition = new Vector3(56.685f, 4.61f, 0);
            }
            else
            {
                //Right
                Card1.transform.localPosition = new Vector3(-56.685f, 4.61f, 0);
                Card2.transform.localPosition = new Vector3(0f, 17.78f, 0);
            }
            ShowCard1.transform.localPosition = Card1.transform.localPosition;
            ShowCard2.transform.localPosition = Card2.transform.localPosition;

            HiddenCard1.transform.localPosition = new Vector3(-18f, -5f);
            HiddenCard2.transform.localPosition = new Vector3(18f, 5f);
        }
        else
        {
            ////// Card 4
            if (isLeftProfilePanel == true)
            {
                //Left
                //Card1.transform.localPosition = new Vector3 (-20f, -39f, 0);
                //Card2.transform.localPosition = new Vector3 (20f, -39f, 0);
                //Card3.transform.localPosition = new Vector3 (60f, -39f, 0);
                //Card4.transform.localPosition = new Vector3 (100f, -39f, 0);
                //Card5.transform.localPosition = new Vector3 (140f, -39f, 0);
                /*
                Card1.transform.localPosition = new Vector3(-47.5f, -39f, 0);
                Card2.transform.localPosition = new Vector3(-14.2f, -39f, 0);
                Card3.transform.localPosition = new Vector3(21f, -39f, 0);
                Card4.transform.localPosition = new Vector3(58.3f, -39f, 0);
                Card5.transform.localPosition = new Vector3(95f, -39f, 0);
                */

                Card1.transform.localPosition = new Vector3(-105.4f, -39f, 0);
                Card2.transform.localPosition = new Vector3(-62.7f, -39f, 0);
                Card3.transform.localPosition = new Vector3(-20f, -39f, 0);
                Card4.transform.localPosition = new Vector3(22.7f, -39f, 0);
                Card5.transform.localPosition = new Vector3(65.4f, -39f, 0);
            }
            else
            {
                //Right
                /*
               Card1.transform.localPosition = new Vector3(-100f, -39f, 0);
               Card2.transform.localPosition = new Vector3(-60f, -39f, 0);
               Card3.transform.localPosition = new Vector3(-20f, -39f, 0);
               Card4.transform.localPosition = new Vector3(20f, -39f, 0);
               Card5.transform.localPosition = new Vector3(60f, -39f, 0);
               */
                Card1.transform.localPosition = new Vector3(-105.4f, -39f, 0);
                Card2.transform.localPosition = new Vector3(-62.7f, -39f, 0);
                Card3.transform.localPosition = new Vector3(-20f, -39f, 0);
                Card4.transform.localPosition = new Vector3(22.7f, -39f, 0);
                Card5.transform.localPosition = new Vector3(65.4f, -39f, 0);


            }
            ShowCard1.transform.localPosition = Card1.transform.localPosition;
            ShowCard2.transform.localPosition = Card2.transform.localPosition;
            ShowCard3.transform.localPosition = Card3.transform.localPosition;
            ShowCard4.transform.localPosition = Card4.transform.localPosition;
            ShowCard5.transform.localPosition = Card5.transform.localPosition;

            HiddenCard1.transform.localPosition = new Vector3(-30f, -10f);
            HiddenCard2.transform.localPosition = new Vector3(-12f, -10f);
            HiddenCard3.transform.localPosition = new Vector3(5f, -10f);
            HiddenCard4.transform.localPosition = new Vector3(26f, -10f);
            HiddenCard5.transform.localPosition = new Vector3(46f, -10f);
        }
    }
    #endregion

    #region COROUTINES
    private IEnumerator DisplayUpCards(List<string> bestCards, float timer)
    {
        yield return new WaitForSeconds(timer);
        if (bestCards.Contains(Card1.currentCard))
        {
            Card1.PlayAnimation(Card1.currentCard);
            Card1.transform.position = new Vector2(Card1.transform.position.x, Card1.transform.position.y + 0.2f);
        }

        if (bestCards.Contains(Card2.currentCard))
        {
            Card2.PlayAnimation(Card2.currentCard);
            Card2.transform.position = new Vector2(Card2.transform.position.x, Card2.transform.position.y + 0.2f);
        }

        if (UIManager.Instance.GameScreeen.currentRoomData.pokerGameType == PokerGameType.omaha.ToString())
        {
            if (bestCards.Contains(Card3.currentCard))
            {
                Card3.PlayAnimation(Card3.currentCard);
                Card3.transform.position = new Vector2(Card3.transform.position.x, Card3.transform.position.y + 0.2f);
            }

            if (bestCards.Contains(Card4.currentCard))
            {
                Card4.PlayAnimation(Card4.currentCard);
                Card4.transform.position = new Vector2(Card4.transform.position.x, Card4.transform.position.y + 0.2f);
            }
        }

        foreach (PokerCard pc in UIManager.Instance.GameScreeen.TableCards)
        {
            if (bestCards.Contains(pc.currentCard))
            {
                pc.card.transform.position = new Vector2(pc.card.transform.position.x, pc.card.transform.position.y + 0.2f);
            }
        }

        foreach (PokerCard pc in UIManager.Instance.GameScreeen.TableExtraCards)
        {
            if (bestCards.Contains(pc.currentCard))
            {
                pc.card.transform.position = new Vector2(pc.card.transform.position.x, pc.card.transform.position.y + 0.2f);
            }
        }
    }
    /*	private IEnumerator DisplayTurnTimer ()
	{
		
		TurnPlayer.Open();
			float i = 0;
			while (i < 1) {
			i += Time.deltaTime / UIManager.Instance.GameScreeen.TurnTime;
			TurnPlayer.fillAmount = Mathf.Lerp (0, 1, i);
				yield return 0;
			}


	}*/
    public IEnumerator ForceUpdateCanvasesNew()
    {
        // Wait for end of frame AND force update all canvases before setting to bottom.
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.SetLayoutHorizontal();
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)contentSizeFitter.transform);
        Canvas.ForceUpdateCanvases();
    }

    public IEnumerator TransferBetAmountToPot(double totalPotAmount)
    {
        Vector3 fromPos = Bets.txtBet.transform.position;
        Vector3 toPos;

        toPos = UIManager.Instance.GameScreeen.txtPotAmount.transform.position;

        PokerPlayerBetValue chipsObj = Instantiate(Bets) as PokerPlayerBetValue;
        instantiatedObjectsList.Add(chipsObj.gameObject);

        chipsObj.Open();
        chipsObj.txtBet.text = BetAmount.ConvertToCommaSeparatedValue();
        //chipsObj.txtBet.text = BetAmount.ToString ("0.00");

        BetAmount = 0;

        chipsObj.transform.SetParent(UIManager.Instance.GameScreeen.cardsParentForInstantiatedCards, false);

        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime * 2f;
            chipsObj.transform.position = Vector3.Lerp(fromPos, toPos, i);

            yield return 0;
        }
        UIManager.Instance.SoundManager.chips_moved_to_potClickOnce();
        UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot = totalPotAmount;
        chipsObj.transform.localScale = Vector3.zero; // this code will hide chips on screen to prevent glitch & followed line will delete object after 0.25 second.
        Destroy(chipsObj.gameObject, .25f);
    }

    /*private IEnumerator MoveFoldedCardsToDealer ()
	{
//		SoundManager.Instance.PlayCardSound();
//		UIManager.Instance.SoundManager.FoldClickOnce ();

		Card1.ResetImage ();
		Card1.Close ();

		Card2.ResetImage ();
		Card2.Close ();

		Card3.ResetImage ();
		Card3.Close ();

		Card4.ResetImage ();
		Card4.Close ();

		ResetImageAllHiddenCards ();
		CloseAllHiddenCards ();

		GameObject cards = Instantiate (Card1.transform.parent.gameObject) as GameObject;
		instantiatedObjectsList.Add (cards);
		cards.SetActive (true);

		if (PlayerId.Equals (UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
			cards.transform.localScale = Vector3.one;

		cards.transform.SetParent (UIManager.Instance.GameScreeen.cardsParentForInstantiatedCards, false);

		Vector3 fromPos = Card1.transform.position;
		Vector3 fromRot = Card1.transform.eulerAngles;
		Vector3 fromScale = cards.transform.localScale;

		Vector3 toPos = UIManager.Instance.GameScreeen.cardGeneratePosition.position;
		Vector3 toRot = UIManager.Instance.GameScreeen.cardGeneratePosition.eulerAngles;
		Vector3 toScale = Card1.transform.localScale;

		Destroy (cards, 2f);

		float i = 0;
		while (i < 1) {
			i += Time.deltaTime * 3f;

			cards.transform.position = Vector3.Lerp (fromPos, toPos, i);
			cards.transform.eulerAngles = Vector3.Lerp (fromRot, toRot, i);
			cards.transform.localScale = Vector3.Lerp (fromScale, toScale, i);

			yield return 0;
		}		
	} */
    private IEnumerator DisplayChatMessageCoroutine()
    {
        //StartCoroutine(ForceUpdateCanvasCoroutine());
        float i = 0;

        float fromAlpha = chatCanvasGroup.alpha;
        float toAlpha = 1;

        while (i < 1)
        {
            i += Time.deltaTime * Constants.Constants.CHAT_BUBBLE_DISPLAY_TIMER;
            chatCanvasGroup.alpha = Mathf.Lerp(fromAlpha, toAlpha, i);

            yield return 0;
        }

        fromAlpha = 1;
        toAlpha = 0;

        yield return new WaitForSeconds(Constants.Constants.CHAT_BUBBLE_DISPLAY_TIMER);

        i = 0;
        while (i < 1)
        {
            i += Time.deltaTime * Constants.Constants.CHAT_BUBBLE_DISPLAY_TIMER;

            chatCanvasGroup.alpha = Mathf.Lerp(fromAlpha, toAlpha, i);
            yield return 0;
        }
    }

    #endregion

    #region GETTER_SETTER

    public double BetAmount
    {
        get
        {
            return _betAmount;
        }
        set
        {
            _betAmount = value;
            txtPotAmount.text = _betAmount.ConvertToCommaSeparatedValue();

            Bets.gameObject.SetActive(_betAmount > 0);
            txtPotAmount.gameObject.SetActive(_betAmount > 0);
        }
    }

    public double BuyInAmount
    {
        get
        {
            return _buyInAmount;
        }
        set
        {
            _buyInAmount = value;
            //txtChips.text = _buyInAmount.ConvertToCommaSeparatedValue();
            _buyInAmount = _buyInAmount < 0 ? 0 : _buyInAmount;
            if (UIManager.Instance.assetOfGame.SavedLoginData.isCash)
            { txtChips.text = "$ " + _buyInAmount.ConvertToCommaSeparatedValue(); }
            else
            { txtChips.text = _buyInAmount.ConvertToCommaSeparatedValue(); }

        }
    }
    public long SidePotAmount
    {
        set
        {
            _sidePotAmount = value;
            txtSidePot.text = value.ConvertToCommaSeparatedValue();
            txtSidePot.transform.parent.gameObject.SetActive(value > 0);
            Utility.Instance.UpdateHorizontalLayout(txtSidePot.gameObject);
        }
        get
        {
            return _sidePotAmount;
        }
    }
    #endregion

}
