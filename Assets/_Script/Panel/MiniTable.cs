// Tournament and Sng Touranament Texas and Omaha Game Type to be set is remainning.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BestHTTP.SocketIO;
using BestHTTP.SocketIO.Events;
using BestHTTP.JSON;

public class MiniTable : MonoBehaviour
{
    #region PUBLIC_VARIABLES

    public GameObject imgSelected;
    public PokerCard[] Cards;
    public ScalePunch scalePunch;
    public string roomId;
    public RoomsListing.Room miniTableRoomData;

    public Socket _MiniSelectTableSocket;
    public Image imgTable;
    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS

    void Start()
    {
        scalePunch.enabled = false;
    }

    void OnEnable()
    {
        StartCoroutine(EnableBroadcast(0.1f));

    }

    void OnDisable()
    {
        _MiniSelectTableSocket.Off(Constants.PokerEvents.OnSubscribeRoom, OnSubscribeRoomReceived);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.RoundComplete, OnRoundComplete);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.GameFinished, OnGameFinished);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.PlayerAction, OnPlayerActionReceived);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.OnTurnTimer, OnTurnTimerRecieved);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.OnPlayerCards, OnPlayerCardsReceived);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.PlayerLeft, OnPlayerLeft);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.ResetGame, OnPokerResetGame);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.IAmBack, OnIAmBackReceived);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.OnSwitchRoom, OnSwitchRoomReceived);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.OnSngTournamentFinished, OnSngTournamentFinishedReceived);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.RegularTournamentFinished, OnRegularTournamentFinishedReceived);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.OnWaitingJoinRoom, OnWaitingJoinRoom);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.superPlayerData, OnsuperPlayerDataReceived);
        _MiniSelectTableSocket.Off(Constants.PokerEvents.OffWaitingJoinRoom, OffWaitingJoinRoom);
        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].ResetImage();
            Cards[i].Close();
        }
    }

    #endregion

    #region DELEGATE_CALLBACKS
    public void ReSubscribeRoom()
    {
        scalePunch.enabled = false;
        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].Close();
        }

        SubscribeRoom(miniTableRoomData.roomId, OnSubscribeRoomDone);
    }

    public void SubscribeRoom(string TableId, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", TableId);
        //Debug.Log("Mini SubscribeRoom: " + jsonObj.toString());
        _MiniSelectTableSocket.Emit(Constants.PokerEvents.SubscribeRoom, action, Json.Decode(jsonObj.toString()));
    }
    public void ShowOtherPlayersCard(SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", miniTableRoomData.roomId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("productName", Application.productName);
        Debug.Log("ShowOtherPlayersCard: " + jsonObj.toString());

        _MiniSelectTableSocket.Emit(Constants.PokerEvents.ShowOtherPlayersCard, action, Json.Decode(jsonObj.toString()));
    }
    #endregion

    #region PUBLIC_METHODS
    public void GetSuperPlayerRequest()
    {
        ShowOtherPlayersCard(OnShowOtherPlayersCardDone);
    }
    public void MiniTableButtonTap()
    {
        if (roomId != UIManager.Instance.GameScreeen.currentRoomData.roomId || !UIManager.Instance.GameScreeen.isActiveAndEnabled)
        {
            UIManager.Instance.StopCoroutine(UIManager.Instance.tableManager.SwitchGameTable(null));
            UIManager.Instance.StartCoroutine(UIManager.Instance.tableManager.SwitchGameTable(miniTableRoomData));
        }
    }
    public void SetMiniTableData(RoomsListing.Room MinitableData)
    {
        //this.miniTableRoomData = MinitableData;
        this.miniTableRoomData = Utility.Instance.GetNewRoomObjectClone(MinitableData);
        roomId = MinitableData.roomId;
        imgSelected.SetActive(true);
        this.Open();
    }

    public void OnMiniTableButtonTap()
    {
        if (roomId != UIManager.Instance.GameScreeen.currentRoomData.roomId || !UIManager.Instance.GameScreeen.isActiveAndEnabled)
        {
            UIManager.Instance.StopCoroutine(UIManager.Instance.tableManager.SwitchGameTable(null));
            UIManager.Instance.StartCoroutine(UIManager.Instance.tableManager.SwitchGameTable(miniTableRoomData));
        }

        //UIManager.Instance.tableManager.DeselectAllTableSelection ();
        //imgSelected.SetActive (true);
    }
    public void OnMiniTableButtonTapForWaitingPlayer(int seatIndex)
    {
        if (roomId != Constants.Poker.TableId)
        {
            Debug.Log("in iff");
            UIManager.Instance.StartCoroutine(UIManager.Instance.SwitchGameTable(miniTableRoomData, true, seatIndex));
        }
        else
        {
            Debug.Log("in else");
            UIManager.Instance.GameScreeen.OnSeatButtonTapWaitingPlayer(seatIndex);
        }
    }

    public void Reset()
    {
        this.miniTableRoomData = null;
        roomId = "";
        scalePunch.enabled = false;
        this.Close();
    }
    #endregion

    #region PRIVATE_METHODS
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
            if (onPlayerCardsResp.result.roomId != miniTableRoomData.roomId && onPlayerCardsResp.result.roomId.Length != 0)
                return;

            for (int i = 0; i < onPlayerCardsResp.result.playersCards.Length; i++)
            {

                if (onPlayerCardsResp.result.playersCards[i].playerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
                {
                    foreach (PokerCard card in Cards)
                    {
                        card.Close();
                    }
                    for (int j = 0; j < onPlayerCardsResp.result.playersCards[i].cards.Count; j++)
                    {
                        Cards[j].Open();
                        //Cards[i].SetAlpha(result.showFoldedCards == true ? Constants.Constants.foldedCardsAlpha : 1);
                        this.Cards[j].DisplayCardWithoutAnimation(onPlayerCardsResp.result.playersCards[i].cards[j]);
                    }
                }
            }
        }
        else
        {
            //UIManager.Instance.DisplayMessagePanel(onPlayerCardsResp.message, null);
        }
    }
    void SetMiniTableData()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].Close();
        }

        _MiniSelectTableSocket = Game.Lobby.socketManager.GetSocket("/" + miniTableRoomData.namespaceString);
        print("_MiniSelectTableSocket: " + _MiniSelectTableSocket.Namespace);
    }
    private void OnSubscribeRoomReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnSubscribeRoomReceived  : " + packet.ToString());

        if (!gameObject.activeSelf)
            return;


        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;
        PokerGameHistory subscribeResp = JsonUtility.FromJson<PokerGameHistory>(resp);


        UIManager.Instance.HideLoader();

        if (subscribeResp == null || miniTableRoomData.roomId != subscribeResp.roomId)
            return;


        miniTableRoomData.smallBlind = subscribeResp.smallBlindChips;
        miniTableRoomData.bigBlind = subscribeResp.bigBlindChips;
        if (UIManager.Instance.MySuperPlayer && subscribeResp.gameId != "")
        {
            //Debug.Log("222222");
            GetSuperPlayerRequest();
        }
    }
    private void OnRoundComplete(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("MiniTable OnRoundComplete  : " + packet.ToString());
        JSONArray arr = new JSONArray(packet.ToString());
        var resp = arr.getString(arr.length() - 1);
        RoundComplete round = JsonUtility.FromJson<RoundComplete>(resp);
        //		if (round.roomId.Equals (roomId)) {
        //		}			
    }

    private void OnPlayerCardsReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("MiniTable OnPlayerCardsReceived  : " + packet.ToString());

        PlayerCards result = JsonUtility.FromJson<PlayerCards>(Utility.Instance.GetPacketString(packet));
        if ((result.roomId.Equals(roomId) || result.roomId.Length == 0) && result.playerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
        {
            for (int i = 0; i < result.cards.Count; i++)
            {
                Cards[i].Open();
                if (result.showFoldedCards)
                {
                    Cards[i].SetAlpha(Constants.Constants.foldedCardsAlpha);
                }
                else
                {
                    Cards[i].SetAlpha(Constants.Poker.MatchedCardAlpha);
                }
                //Cards[i].SetAlpha(result.showFoldedCards == true ? Constants.Constants.foldedCardsAlpha : 1);
                this.Cards[i].DisplayCardWithoutAnimation(result.cards[i]);
            }

            for (int i = result.cards.Count; i < Cards.Length; i++)
            {
                Cards[i].Close();
            }
        }
    }

    private void OnPlayerLeft(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("Minitable OnPlayerLeft  : " + packet.ToString());

        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;
        JSON_Object playerObj = new JSON_Object(resp.ToString());

        if (playerObj.has("roomId") && playerObj.getString("roomId") != roomId)
            return;

        if (playerObj.getString("playerId") == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
        {
            for (int i = 0; i < Cards.Length; i++)
            {
                Cards[i].Close();
            }

            //if (!miniTableRoomData.isTournament)
            //{                
            //    UIManager.Instance.tableManager.RemoveMiniTable(roomId);
            //}
        }
    }

    private void OnPokerResetGame(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {

        print("Minitable Reset");
        try
        {
            ResetGame resetGame = JsonUtility.FromJson<ResetGame>(Utility.Instance.GetPacketString(packet));

            if (resetGame.roomId != roomId && resetGame.roomId.Length != 0)
                return;

            for (int i = 0; i < Cards.Length; i++)
            {
                Cards[i].Close();
                Cards[i].SetAlpha(Constants.Poker.MatchedCardAlpha);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("exception  : " + e);
        }
    }

    private void OnIAmBackReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("Minitable OnIAmBackReceived : " + packet.ToString());

        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            IAmBack onIAmBackResp = JsonUtility.FromJson<IAmBack>(resp);

            if (onIAmBackResp.roomId != roomId && onIAmBackResp.roomId.Length != 0)
                return;

            if (onIAmBackResp.playerId != UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
                return;


            if (onIAmBackResp.playerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    Cards[i].SetAlpha(Constants.Poker.MatchedCardAlpha);
                }
            }

        }
        catch (System.Exception e)
        {
            Debug.Log("exception  : " + e);
        }
    }

    private void OnSwitchRoomReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("Minitable OnSwitchRoomReceived : " + packet.ToString());

        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;
            RegularTournament resp = JsonUtility.FromJson<RegularTournament>(resp1);

            if (resp.roomId != roomId && resp.roomId.Length != 0)
                return;

            if (UIManager.Instance.tableManager.IsMiniTableExists(resp.newRoomId) == false)
            {
                UIManager.Instance.tableManager.ReplaceMiniTableRoomId(resp.roomId, resp.newRoomId);
                roomId = resp.newRoomId;
                miniTableRoomData.roomId = roomId;
            }
            else
            {
                UIManager.Instance.tableManager.DeselectAllTableSelection();
                UIManager.Instance.tableManager.TableSelection(resp.newRoomId);
                UIManager.Instance.tableManager.RemoveMiniTable(resp.roomId);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("exception  : " + e);
        }
    }

    private void OnSngTournamentFinishedReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("Minitable OnSngTournamentFinishedReceived : " + packet.ToString());
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            SngTournamentFinishedData RegularTournamentFinishedDataResp = JsonUtility.FromJson<SngTournamentFinishedData>(resp);

            if (RegularTournamentFinishedDataResp.roomId == roomId || RegularTournamentFinishedDataResp.tournamentId == miniTableRoomData.tournamentId)
                UIManager.Instance.tableManager.RemoveMiniTable(roomId);

        }
        catch (System.Exception e)
        {
            Debug.Log("exception  : " + e);
        }
    }

    private void OnRegularTournamentFinishedReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("Minitable OnRegularTournamentFinishedReceived : " + packet.ToString());

        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            SngTournamentFinishedData RegularTournamentFinishedDataResp = JsonUtility.FromJson<SngTournamentFinishedData>(resp);

            if (RegularTournamentFinishedDataResp.roomId == roomId || RegularTournamentFinishedDataResp.tournamentId == miniTableRoomData.tournamentId)
                UIManager.Instance.tableManager.RemoveMiniTable(roomId);

        }
        catch (System.Exception e)
        {
            Debug.Log("exception  : " + e);
        }
    }

    private IEnumerator DistributeTableCards(List<string> tableCards)
    {
        for (int i = 0; i < tableCards.Count; i++)
        {
            this.Cards[i].DisplayCardWithoutAnimation(tableCards[i]);
        }
        yield return 0;
    }

    private void OnGameFinished(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("minitable OnGameFinished  : " + packet.ToString());

        JSONArray arr = new JSONArray(packet.ToString());
        var resp = arr.getString(arr.length() - 1);

        PokerGameWinner wnr = JsonUtility.FromJson<PokerGameWinner>(resp);
        if (wnr.roomId.Equals(roomId) || wnr.roomId.Length == 0)
        {
            for (int i = 0; i < Cards.Length; i++)
            {
                Cards[i].Close();
            }
        }
    }

    private void OnPlayerActionReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("Minitable OnPlayerActionReceived  : " + packet.ToString());


        PlayerAction action = JsonUtility.FromJson<PlayerAction>(Utility.Instance.GetPacketString(packet));
        if (action.roomId.Equals(roomId) || action.roomId.Length == 0)
        {
            scalePunch.enabled = false;

            if (action.action.playerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId && action.action.action == PokerPlayerAction.Fold)
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    //Cards[i].Close();
                    Cards[i].SetAlpha(Constants.Constants.foldedCardsAlpha);
                    print("alpha => ");
                }
            }
            else
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    //Cards[i].Close();
                    Cards[i].SetAlpha(Constants.Poker.MatchedCardAlpha);
                }
            }
        }
    }

    private void OnTurnTimerRecieved(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] arg)
    {
        //print("---------- MiniTable OnTurnTimerRecieved ---------- " + packet.ToString());
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());

            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            OnTurnTimer onTurnTimerCall = JsonUtility.FromJson<OnTurnTimer>(resp);
            if (onTurnTimerCall.roomId.Equals(roomId) || onTurnTimerCall.roomId.Length == 0)
            {
                if (onTurnTimerCall.playerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
                {
                    scalePunch.enabled = true;
                }
                else
                {
                    scalePunch.enabled = false;
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("exception  : " + e);
        }
    }

    private void OnSubscribeRoomDone(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("minitable subscribe room repsponse  : " + packet.ToString());

        JSONArray arr = new JSONArray(packet.ToString());
        string Source;
        Source = arr.getString(arr.length() - 1);
        var resp = Source;
        PokerEventResponse<PokerGameHistory> subscribeResp = JsonUtility.FromJson<PokerEventResponse<PokerGameHistory>>(resp);

        if (subscribeResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
        }
        else
        {
            UIManager.Instance.tableManager.RemoveMiniTable(roomId);
        }
    }

    private void OnWaitingJoinRoom(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("Minitable OnWaitingJoinRoom  : " + packet.ToString());

        WaitingJoinRoomResult result = JsonUtility.FromJson<WaitingJoinRoomResult>(Utility.Instance.GetPacketString(packet));

        try
        {
            foreach (WaitingJoinRoomResult.Playerjoin jPlr in result.joiners)
            {
                if (result.roomId == roomId && jPlr.playerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
                {
                    if (!UIManager.Instance.messagePanelJoinTable.isActiveAndEnabled && !UIManager.Instance.GameScreeen.BuyInAmountPanel.isActiveAndEnabled && UIManager.Instance.GameScreeen.isActiveAndEnabled)
                    {
                        UIManager.Instance.GameScreeen.waitingPlayerSeatManage(jPlr.seatIndex);
                        UIManager.Instance.DisplayJoinConfirmationPanel("Seat reserved for you. Join now to play.", "Join", "Cancel",
                            () =>
                            { //accept
                                UIManager.Instance.messagePanelJoinTable.btnAffirmativeAction.interactable = false;
                                UIManager.Instance.DisplayLoader("Please wait...");
                                OnMiniTableButtonTapForWaitingPlayer(jPlr.seatIndex);
                                UIManager.Instance.SoundManager.OnButtonClick();
                                UIManager.Instance.messagePanelJoinTable.Close();
                                UIManager.Instance.HideLoader();
                            },
                            () =>
                            { //reject
                                UIManager.Instance.SoundManager.OnButtonClick();
                                UIManager.Instance.SocketGameManager.RejectSeatRequest(roomId, jPlr.seatIndex, (socket1, packet1, args1) =>
                                {
                                    PokerEventResponse response = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet1));
                                    //if (response.status.Equals(Constants.PokerAPI.KeyStatusSuccess)) {}
                                    UIManager.Instance.GameScreeen.waitingPlayerSeatManage(10);
                                    scalePunch.enabled = false;
                                });
                                UIManager.Instance.messagePanelJoinTable.Close();
                            }
                        );
                    }
                    if (gameObject.activeInHierarchy)
                    {
                        scalePunch.enabled = true;
                    }
                    if (result.timer <= 0)
                    {
                        UIManager.Instance.messagePanelJoinTable.Close();
                        UIManager.Instance.GameScreeen.BuyInAmountPanel.Close();
                        scalePunch.enabled = false;
                    }

                    UIManager.Instance.GameScreeen.BuyInAmountPanel.RemainingTime = result.timer;
                }
                else
                {
                    scalePunch.enabled = false;
                }
            }
        }

        catch (System.Exception e)
        {
            Debug.LogError("OnWaitingJoinRoom -> Exception  : " + e);
        }
    }

    private void OffWaitingJoinRoom(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("OffWaitingJoinRoom : " + packet.ToString());
        if (!gameObject.activeSelf)
            return;
        try
        {
            StartCoroutine(offJoinWaitPopup(1f));
        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("exception  : " + e + " " + stackTrace.GetFrame(0).GetMethod().Name);
        }
    }

    private void OnsuperPlayerDataReceived(Socket scoket, Packet packet, params object[] args)
    {
        Debug.Log("Minitable OnsuperPlayerDataReceived : " + packet.ToString());
        if (!gameObject.activeSelf)
            return;
        try
        {
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            superPlayerCard onSuperPlayerCardsResp = JsonUtility.FromJson<superPlayerCard>(Utility.Instance.GetPacketString(packet));

            if (onSuperPlayerCardsResp.roomId != miniTableRoomData.roomId && onSuperPlayerCardsResp.roomId.Length != 0)
                return;

            StartCoroutine(superplayercatdsopen(onSuperPlayerCardsResp));


        }
        catch (System.Exception e)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("exception  : " + e + " " + stackTrace.GetFrame(0).GetMethod().Name);
        }
    }
    #endregion

    #region COROUTINES
    public IEnumerator offJoinWaitPopup(float timer)
    {
        yield return new WaitForSeconds(timer);
        UIManager.Instance.messagePanelJoinTable.Close();
    }
    public IEnumerator EnableBroadcast(float timer)
    {
        yield return new WaitForSeconds(timer);
        SetMiniTableData();
        _MiniSelectTableSocket.On(Constants.PokerEvents.OnSubscribeRoom, OnSubscribeRoomReceived);
        _MiniSelectTableSocket.On(Constants.PokerEvents.RoundComplete, OnRoundComplete);
        _MiniSelectTableSocket.On(Constants.PokerEvents.GameFinished, OnGameFinished);
        _MiniSelectTableSocket.On(Constants.PokerEvents.PlayerAction, OnPlayerActionReceived);
        _MiniSelectTableSocket.On(Constants.PokerEvents.OnTurnTimer, OnTurnTimerRecieved);
        _MiniSelectTableSocket.On(Constants.PokerEvents.OnPlayerCards, OnPlayerCardsReceived);
        _MiniSelectTableSocket.On(Constants.PokerEvents.PlayerLeft, OnPlayerLeft);
        _MiniSelectTableSocket.On(Constants.PokerEvents.ResetGame, OnPokerResetGame);
        _MiniSelectTableSocket.On(Constants.PokerEvents.IAmBack, OnIAmBackReceived);
        _MiniSelectTableSocket.On(Constants.PokerEvents.OnSwitchRoom, OnSwitchRoomReceived);
        _MiniSelectTableSocket.On(Constants.PokerEvents.OnSngTournamentFinished, OnSngTournamentFinishedReceived);
        _MiniSelectTableSocket.On(Constants.PokerEvents.RegularTournamentFinished, OnRegularTournamentFinishedReceived);
        _MiniSelectTableSocket.On(Constants.PokerEvents.OnWaitingJoinRoom, OnWaitingJoinRoom);
        _MiniSelectTableSocket.On(Constants.PokerEvents.superPlayerData, OnsuperPlayerDataReceived);
        SubscribeRoom(miniTableRoomData.roomId, OnSubscribeRoomDone);

    }
    private IEnumerator superplayercatdsopen(superPlayerCard onSuperPlayerCardsResp)
    {
        for (int i = 0; i < onSuperPlayerCardsResp.playersCards.Count; i++)
        {

            if (onSuperPlayerCardsResp.playersCards[i].playerId.Equals(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId))
            {
                foreach (PokerCard card in Cards)
                {
                    card.Close();
                }
                for (int j = 0; j < onSuperPlayerCardsResp.playersCards[i].cards.Count; j++)
                {
                    Cards[j].Open();
                    //Cards[i].SetAlpha(result.showFoldedCards == true ? Constants.Constants.foldedCardsAlpha : 1);
                    this.Cards[j].SetAlpha(1);
                    this.Cards[j].DisplayCardWithoutAnimation(onSuperPlayerCardsResp.playersCards[i].cards[j]);
                }
            }
            yield return new WaitForSeconds(0f);

        }
    }

    #endregion

    #region GETTER_SETTER

    #endregion

}
