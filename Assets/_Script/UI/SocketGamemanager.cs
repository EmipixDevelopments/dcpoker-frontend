using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.SocketIO;
using BestHTTP.SocketIO.Events;
using BestHTTP.JSON;
using UnityEngine.Purchasing;
//using UnityEngine.Purchasing;

public class SocketGamemanager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }


    void OnEnable()
    {
    }

    #region PUBLIC_VARIABLES

    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    // Update is called once per frame


    #endregion

    #region DELEGATE_SocketEvents
    public void InAppPurchase(SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("os", Utility.Instance.GetOSName());
        jsonObj.put("appVersion", Utility.Instance.GetApplicationVersion());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);
        print("AvailableInAppPurchase: " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.AvailableInAppPurchase, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// Login player.
    /// </summary>
    /// <param name="recipt">recipt.</param>
    /// <param name="price">price.</param>
    /// <param name="chips">chips.</param>
    /// <param name="action">action.</param>
    public void InAppPurchaseSuccess(Product product, float price, int chips, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("availableToPurchase", product.availableToPurchase);
        jsonObj.put("receipt", product.receipt);
        jsonObj.put("deviceId", SystemInfo.deviceUniqueIdentifier.ToString());
        jsonObj.put("transactionID", product.transactionID);
        jsonObj.put("isoCurrencyCode", product.metadata.isoCurrencyCode);
        jsonObj.put("localizedDescription", product.metadata.localizedDescription);
        jsonObj.put("localizedPrice", product.metadata.localizedPrice);
        jsonObj.put("localizedPriceString", product.metadata.localizedPriceString);
        jsonObj.put("localizedTitle", product.metadata.localizedTitle);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("price", price);
        jsonObj.put("chips", chips);
        jsonObj.put("os", Utility.Instance.GetOSName());
        jsonObj.put("appVersion", Utility.Instance.GetApplicationVersion());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.VerifyInApp, action, Json.Decode(jsonObj.toString()));
    }


    /// <summary>
    /// Login player.
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="password">Password.</param>
    /// <param name="action">Action.</param>
    public void Login(string username, string password, string authset, string ipAddress, bool forceLogin, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();

        //jsonObj.put("username", username);
        //jsonObj.put("password", password);
        jsonObj.put("auth", authset);
        jsonObj.put("forceLogin", forceLogin);
        jsonObj.put("AppId", "");
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("ipAddress", ipAddress);
        jsonObj.put("fcmId", UIManager.Instance.assetOfGame.SavedLoginData.fcmRegistrationToken);
        jsonObj.put("os", Utility.Instance.GetOSName());
        jsonObj.put("appVersion", Utility.Instance.GetApplicationVersion());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);

        Debug.Log("Login Event: " + jsonObj.toString());

        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.Login, action, Json.Decode(jsonObj.toString()));
    }

    public void VerifyIdentifierToken(string ipAddress, string identifiertoken, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("identifiertoken", identifiertoken);
        jsonObj.put("os", Utility.Instance.GetOSName());
        jsonObj.put("ipAddress", ipAddress);
        jsonObj.put("appVersion", Utility.Instance.GetApplicationVersion());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);

        Debug.Log("VerifyIdentifierToken Event: " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.VerifyIdentifierToken, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// Registers the player.
    /// </summary>
    /// <param name="name"> Name.</param>
    /// <param name="email">Email.</param>
    /// <param name="username">Username.</param>
    /// <param name="password">Password.</param>
    /// <param name="action">Action.</param>
    public void RegisterPlayer(string username, string password, string mobile, string refferralcode, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        //jsonObj.put ("name", name);
        jsonObj.put("username", username.ToLower());
        jsonObj.put("password", password);
        jsonObj.put("mobile", mobile);
        jsonObj.put("refferralCode", refferralcode);
        jsonObj.put("deviceId", SystemInfo.deviceUniqueIdentifier.ToString());
        jsonObj.put("os", Utility.Instance.GetOSName());
        jsonObj.put("appVersion", Utility.Instance.GetApplicationVersion());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);

        Debug.Log("Register=> " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.Register, action, Json.Decode(jsonObj.toString()));
    }
    /// <summary>
    /// Lists the rooms.
    /// </summary>
    /// <param name="tableSpeed">Table speed.</param>
    /// <param name="tableName">Table name.</param>
    /// <param name="limit">If set to <c>true</c> limit.</param>
    /// <param name="action">Action.</param>
    public void SearchLobby(string pokerGameType, GameSpeed tableSpeed, string tableName, bool limit, string gametype, string game, string stacks, string maxPlayer, string currencyType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("gametype", gametype);
        jsonObj.put("game", game.ToLower());
        jsonObj.put("stacks", stacks);
        jsonObj.put("maxPlayer", maxPlayer);
        jsonObj.put("maxPlayer", maxPlayer);
        jsonObj.put("currencyType", currencyType);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("appVersion", Utility.Instance.GetApplicationVersion());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);

        print(Constants.PokerEvents.SearchLobby + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// Lists the rooms.
    /// </summary>
    /// <param name="tableSpeed">Table speed.</param>
    /// <param name="tableName">Table name.</param>
    /// <param name="limit">If set to <c>true</c> limit.</param>
    /// <param name="action">Action.</param>
    public void SearchTournamentLobby(string pokerGameType, string tableSpeed, string tableName, bool limit, string gametype, string game, string stacks, string maxPlayer, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("tableSpeed", tableSpeed);
        jsonObj.put("gametype", gametype);
        jsonObj.put("game", game.ToLower());
        jsonObj.put("stacks", stacks);
        jsonObj.put("maxPlayer", maxPlayer);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        Debug.Log(Constants.PokerEvents.SearchTournamentLobby + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.SearchTournamentLobby, action, Json.Decode(jsonObj.toString()));
    }

    public void JoinTournamentRoom(string tournamentId, SocketIOAckCallback action)
    {
        ///		Debug.Log("JoinTournamentRoom");
        JSON_Object jsonObj = new JSON_Object();

        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("JoinTournamentRoom  : " + jsonObj.toString());

        Debug.Log(Constants.PokerEvents.JoinTournamentRoom + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.JoinTournamentRoom, action, Json.Decode(jsonObj.toString()));
    }
    /// <summary>
    /// Lists the rooms.
    /// </summary>
    /// <param name="tableSpeed">Table speed.</param>
    /// <param name="tableName">Table name.</param>
    /// <param name="limit">If set to <c>true</c> limit.</param>
    /// <param name="action">Action.</param>
    public void SearchSngLobby(string pokerGameType, GameSpeed tableSpeed, string tableName, bool limit, string gametype, string game, string stacks, string maxPlayer, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("gametype", gametype);
        jsonObj.put("game", game.ToLower());
        jsonObj.put("stacks", stacks);
        jsonObj.put("maxPlayer", maxPlayer);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.SearchSngLobby + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.SearchSngLobby, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// getTournamentInfo.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="limit">If set to <c>true</c> limit.</param>
    /// <param name="action">Action.</param>
    public void getTournamentInfo(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("timezone", PlayerPrefs.GetString("timezone", ""));
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.TournamentInfo + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.TournamentInfo, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// SngTournamentInfo.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="limit">If set to <c>true</c> limit.</param>
    /// <param name="action">Action.</param>
    public void getSngTournamentInfo(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.SngTournamentInfo + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.SngTournamentInfo, action, Json.Decode(jsonObj.toString()));
    }
    public void MyTournament(SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.MyTournament + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.MyTournament, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// getTournamentInfo.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getTournamentPlayers(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.TournamentPlayers + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.TournamentPlayers, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// getTournamentInfo.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getTournamentTables(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.TournamentTables + " - " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.TournamentTables, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// TournamentPayout.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getTournamentPayout(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.TournamentPayout + " - " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.TournamentPayout, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// TournamentPayout.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getTournamentBlinds(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.TournamentBlinds + " - " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.TournamentBlinds, action, Json.Decode(jsonObj.toString()));
    }
    ///Start sng 
    /// 
    /// <summary>
    /// getSngTournamentPlayers.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="roomId">room Id.</param>
    /// <param name="action">Action.</param>
    public void getSngTournamentPlayers(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.SngTournamentPlayers + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.SngTournamentPlayers, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// SngTournamentTables.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getSngTournamentTables(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.SngTournamentTables + " - " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.SngTournamentTables, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// SngTournamentPayout.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getSngTournamentPayout(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.SngTournamentPayout + " - " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.SngTournamentPayout, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// SngTournamentBlinds.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getSngTournamentBlinds(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName); jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.SngTournamentBlinds + " - " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.SngTournamentBlinds, action, Json.Decode(jsonObj.toString()));
    }
    /// SNG End

    public void UpdateAccNo(string title, string description, string accno, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("title", title);
        jsonObj.put("receipt", description);
        jsonObj.put("accountNumber", accno);
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);

        Debug.Log("UpdateAccountNumber Event: " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.UpdateAccountNumber, action, Json.Decode(jsonObj.toString()));
    }

    public void UploadDeposit(string amount, string imgname, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("depositChips", amount);
        jsonObj.put("receipt", imgname);
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);

        Debug.Log("UploadDeposit Event: " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.UploadDepositReceipt, action, Json.Decode(jsonObj.toString()));
    }

    public void UploadWithdraw(string amount, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("withdrawAmount", amount);
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);

        Debug.Log("UploadWithdraw Event: " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.Withdrawal, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// RegisterTournament.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getRegisterTournament(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.RegisterTournament + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.RegisterTournament, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// RegisterTournament.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getUnRegisterTournament(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.UnRegisterTournament + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.UnRegisterTournament, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// RegisterSngTournament.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getRegisterSngTournament(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.RegisterSngTournament + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.RegisterSngTournament, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// RegisterSngTournament.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getUnRegisterSngTournament(string tournamentId, string pokerGameType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", tournamentId);
        jsonObj.put("pokerGameType", pokerGameType);
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.UnRegisterSngTournament + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.UnRegisterSngTournament, action, Json.Decode(jsonObj.toString()));
    }


    /// <summary>
    /// getJoinTournament.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getJoinTournament(int ID, string tournamentId, string tournamentType, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("tournamentType", tournamentType);
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);

        print("Game.Lobby.RegTournamentSocket: " + Game.Lobby.RegTournamentSocket.Namespace);
        print(Constants.PokerEvents.JoinTournament + " - " + jsonObj.toString());

        if (ID == 0)
            Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.JoinTournament, action, Json.Decode(jsonObj.toString()));
        else
            Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.JoinTournament, action, Json.Decode(jsonObj.toString()));
    }


    /// <summary>
    /// JoinSngTournament.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getJoinSngTournament(string tournamentId, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.JoinSngTournament + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.SNGTournamentSocket.Emit(Constants.PokerEvents.JoinSngTournament, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// RejectTournament.
    /// </summary>
    /// <param name="playerId">player Id.</param>
    /// <param name="tournamentId">tournamentId.</param>
    /// <param name="action">Action.</param>
    public void getRejectTournament(string tournamentId, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log("ListRooms  : " + jsonObj.toString());

        print(Constants.PokerEvents.RejectTournament + " - " + jsonObj.toString());
        //GetGameSocket(gametype).Emit(Constants.PokerEvents.SearchLobby, action, Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.RejectTournament, action, Json.Decode(jsonObj.toString()));
    }
    /// <summary>
    /// Lists the rooms.
    /// </summary>
    /// <param name="tableSpeed">Table speed.</param>
    /// <param name="tableName">Table name.</param>
    /// <param name="limit">If set to <c>true</c> limit.</param>
    /// <param name="action">Action.</param>
    public void GetProfile(SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);

        //print(Constants.PokerEvents.Playerprofile + " - " + jsonObj.toString());

        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.Playerprofile, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// Leaderboard.
    /// </summary>
    /// <param name="PlayerId">Player iD.</param>
    /// <param name="action">Action.</param>
    public void GetLeaderboard(SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("os", Utility.Instance.GetOSName());
        jsonObj.put("appVersion", Utility.Instance.GetApplicationVersion());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);

        print(Constants.PokerEvents.Leaderboard + " - " + jsonObj.toString());

        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.Leaderboard, action, Json.Decode(jsonObj.toString()));
    }


    public void PlayerAccountInfo(SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("os", Utility.Instance.GetOSName());
        jsonObj.put("appVersion", Utility.Instance.GetApplicationVersion());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print(Constants.PokerEvents.GetPlayerAccountInfo + " - " + jsonObj.toString());

        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.GetPlayerAccountInfo, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// Leaderboard.
    /// </summary>
    /// <param name="PlayerId">Player iD.</param>
    /// <param name="action">Action.</param>
    public void GetPurchaseHistory(SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print(Constants.PokerEvents.purchaseHistory + " - " + jsonObj.toString());

        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.purchaseHistory, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// playerChangePassword.
    /// </summary>
    /// <param name="PlayerId">Player iD.</param>
    /// <param name="newPassword">new Password.</param>
    /// <param name="verifyNewPassword">verify NewPassword.</param>
    /// <param name="oldPassword">old Password.</param>
    /// <param name="action">Action.</param>
    public void GetplayerChangePassword(string newPassword, string verifyNewPassword, string oldPassword, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("newPassword", newPassword);
        jsonObj.put("verifyNewPassword", verifyNewPassword);
        jsonObj.put("oldPassword", oldPassword);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print(Constants.PokerEvents.playerChangePassword + " - " + jsonObj.toString());

        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.playerChangePassword, action, Json.Decode(jsonObj.toString()));
    }

    public void TransferChips(string userId, double chips, string password, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("password", password);
        jsonObj.put("receiver", userId);
        jsonObj.put("chips", chips);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print(Constants.PokerEvents.TransferChips + " - " + jsonObj.toString());

        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.TransferChips, action, Json.Decode(jsonObj.toString()));
    }

    public void ChangeUsername(string newUsername, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("newUsername", newUsername);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print(Constants.PokerEvents.ChangeUsername + " - " + jsonObj.toString());

        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.ChangeUsername, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// playerForgotPassword.
    /// </summary>
    /// <param name="Email">Email iD.</param>
    /// <param name="action">Action.</param>
    public void GetplayerForgotPassword(string Email, SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("email", Email);
        jsonObj.put("productName", Application.productName);
        print(Constants.PokerEvents.playerForgotPassword + " - " + jsonObj.toString());

        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.playerForgotPassword, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// newsBlog.
    /// </summary>
    /// <param name="action">Action.</param>
    public void GetnewsBlog(SocketIOAckCallback action)
    {
        ///		Debug.Log("TexasAndOmahaRoomLists");
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print(Constants.PokerEvents.newsBlog + " - ");

        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.newsBlog, action, Json.Decode(""));
    }

    /// <summary>
    /// playerProfilePic.
    /// </summary>
    /// <param name="playerId">Player iD.</param>
    /// <param name="profilePic">profilePic iD.</param>
    /// <param name="action">Action.</param>
    public void GetplayerProfilePic(int profilePicId, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("profilePic", profilePicId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print(Constants.PokerEvents.playerProfilePic + " - " + jsonObj.toString());

        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.playerProfilePic, action, Json.Decode(jsonObj.toString()));
    }
    /// <summary>
    /// newsBlog.
    /// </summary>
    /// <param name="action">Action.</param>
    public void GameHistoryList(int pageNo, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        if (pageNo > 0)
            jsonObj.put("pageNo", pageNo);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("GameHistoryList : " + jsonObj.toString());

        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.GameHistoryList, action, Json.Decode(jsonObj.toString()));
    }

    public void PrivateRoomLogin(string roomId, string password, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", roomId);
        jsonObj.put("password", password);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Debug.Log("PrivateRoomLogin: " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.PrivateRoomLogin, action, Json.Decode(jsonObj.toString()));
    }

    public void GameHistory(string gameId, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("gameId", gameId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		Debug.Log ("GameHistory : " + jsonObj.toString());

        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.GameHistory, action, Json.Decode(jsonObj.toString()));
    }

    public void SubscribeRoom(SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("latitude", UIManager.Instance.ipLocationService.locationCordinates.latitude);
        jsonObj.put("longitude", UIManager.Instance.ipLocationService.locationCordinates.longitude);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);


        Debug.Log("SubscribeRoom  : " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.SubscribeRoom, action, Json.Decode(jsonObj.toString()));
        //Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.SubscribeRoom, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// Joins the room.
    /// </summary>
    /// <param name="roomId">Room identifier.</param>
    /// <param name="buyinAmount">Buyin amount.</param>
    /// <param name="seatIndex">Seat index.</param>
    /// <param name="autoBuyin">If set to <c>true</c> auto buyin.</param>
    /// <param name="action">Action.</param>
    public void JoinRoom(string roomId, double buyinAmount, int seatIndex, bool isWaitingPlayer, double autoBuyin, SocketIOAckCallback action)
    {

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", roomId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("chips", buyinAmount.ToString());
        jsonObj.put("seatIndex", seatIndex);
        jsonObj.put("isWaitingPlayer", isWaitingPlayer);
        jsonObj.put("latitude", UIManager.Instance.ipLocationService.locationCordinates.latitude);
        jsonObj.put("longitude", UIManager.Instance.ipLocationService.locationCordinates.longitude);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("JoinRoom API call: " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.JoinRoom, action, Json.Decode(jsonObj.toString()));
    }

    public void GetBuyinsAndPlayerchips(string roomId, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", roomId);
        jsonObj.put("latitude", UIManager.Instance.ipLocationService.locationCordinates.latitude);
        jsonObj.put("longitude", UIManager.Instance.ipLocationService.locationCordinates.longitude);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("GetBuyinsAndPlayerchips: " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.GetBuyinsAndPlayerchips, action, Json.Decode(jsonObj.toString()));
    }

    public void JoinRoom(string roomId, double buyinAmount, int seatIndex, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", roomId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("chips", buyinAmount.ToString());
        jsonObj.put("seatIndex", seatIndex);
        jsonObj.put("latitude", UIManager.Instance.ipLocationService.locationCordinates.latitude);
        jsonObj.put("longitude", UIManager.Instance.ipLocationService.locationCordinates.longitude);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("JoinRoom API call: " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.JoinRoom, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// Sends the player action.
    /// </summary>
    /// <param name="playerId">Player identifier.</param>
    /// <param name="betAmount">Bet amount.</param>
    /// <param name="playerAction">Player action.</param>
    /// <param name="hasRaised">If set to <c>true</c> has raised.</param>
    /// <param name="action">Action.</param>
    public void PlayersCards(SocketIOAckCallback action)
    {
        JSON_Object actionObj = new JSON_Object();
        actionObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        actionObj.put("roomId", Constants.Poker.TableId);
        actionObj.put("authToken", UIManager.Instance.tokenHack());
        actionObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        actionObj.put("productName", Application.productName);
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.PlayersCards, action, Json.Decode(actionObj.toString()));
    }

    public void ShowFoldedPlayerCards(string gameId, SocketIOAckCallback action)
    {
        JSON_Object actionObj = new JSON_Object();
        actionObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        actionObj.put("roomId", Constants.Poker.TableId);
        actionObj.put("gameId", gameId);
        actionObj.put("authToken", UIManager.Instance.tokenHack());
        actionObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        actionObj.put("productName", Application.productName);
        print("ShowFoldedPlayerCards:" + actionObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.ShowFoldedPlayerCards, action, Json.Decode(actionObj.toString()));
    }

    /// <summary>
    /// Sends the player action.
    /// </summary>
    /// <param name="playerId">Player identifier.</param>
    /// <param name="betAmount">Bet amount.</param>
    /// <param name="playerAction">Player action.</param>
    /// <param name="hasRaised">If set to <c>true</c> has raised.</param>
    /// <param name="action">Action.</param>
    public void SendPlayerAction(string playerId, double betAmount, PokerPlayerAction playerAction, bool hasRaised, SocketIOAckCallback action)
    {
        JSON_Object actionObj = new JSON_Object();
        actionObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        actionObj.put("betAmount", betAmount.ToString());
        actionObj.put("action", (int)playerAction);
        actionObj.put("roomId", Constants.Poker.TableId);
        actionObj.put("hasRaised", hasRaised);
        actionObj.put("authToken", UIManager.Instance.tokenHack());
        actionObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        actionObj.put("productName", Application.productName);

        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.PlayerAction, action, Json.Decode(actionObj.toString()));
    }

    public void WaitForBigBlindEvent(string roomId, bool checkboxValue, SocketIOAckCallback action)
    {
        JSON_Object actionObj = new JSON_Object();
        actionObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        actionObj.put("roomId", roomId);
        actionObj.put("checkboxValue", checkboxValue);
        actionObj.put("authToken", UIManager.Instance.tokenHack());
        actionObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        actionObj.put("productName", Application.productName);

        print("WaitForBigBlindEvent  : " + actionObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.WaitForBigBlindEvent, action, Json.Decode(actionObj.toString()));
    }

    /// <summary>
    /// Sends player is back online.
    /// </summary>
    /// <param name="playerId">Player identifier.</param>
    /// <param name="betAmount">Bet amount.</param>
    /// <param name="playerAction">Player action.</param>
    /// <param name="hasRaised">If set to <c>true</c> has raised.</param>
    /// <param name="action">Action.</param>
    public void SendPlayerOnline(SocketIOAckCallback action)
    {
        JSON_Object actionObj = new JSON_Object();
        actionObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        actionObj.put("roomId", Constants.Poker.TableId);
        actionObj.put("authToken", UIManager.Instance.tokenHack());
        actionObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        actionObj.put("productName", Application.productName);
        print("SendPlayerOnline  : " + actionObj.toString());

        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.PlayerOnline, action, Json.Decode(actionObj.toString()));
    }

    public void ExtraTimer(string roomId, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", roomId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("ExtraTimer : " + jsonObj.toString());

        /*	Socket socket;
            if (UIManager.Instance.lobbyPanel.gameType == GameType.Texas)
                socket = UIManager.Instance.texasSocket;
            else
                socket = UIManager.Instance.omahaSocket;
            */
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.ExtraTimer, action, Json.Decode(jsonObj.toString()));
    }

    public void DefaultActionSelection(string roomId, string option, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", roomId);
        jsonObj.put("option", option);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("DefaultActionSelection : " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.DefaultActionSelection, action, Json.Decode(jsonObj.toString()));
    }

    public void SitOutNextHand(string roomId, bool actionValue, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", roomId);
        jsonObj.put("actionValue", actionValue);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("SitOutNextHand : " + jsonObj.toString());

        /*Socket socket;
		if (UIManager.Instance.lobbyPanel.gameType == GameType.Texas)
			socket = UIManager.Instance.texasSocket;
		else
			socket = UIManager.Instance.omahaSocket;
		*/
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.SitOutNextHand, action, Json.Decode(jsonObj.toString()));
    }

    public void SitOutNextBigBlind(string roomId, bool actionValue, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", roomId);
        jsonObj.put("actionValue", actionValue);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("SitOutNextBigBlind : " + jsonObj.toString());

        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.SitOutNextBigBlind, action, Json.Decode(jsonObj.toString()));
    }
    public void onAllowMuck(string roomId, bool actionValue, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", roomId);
        jsonObj.put("action", actionValue);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("onAllowMuck : " + jsonObj.toString());

        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.PlayerMuckAction, action, Json.Decode(jsonObj.toString()));
    }

    public void AbsolutePlayer(SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("AbsolutePlayer : " + jsonObj.toString());

        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.AbsolutePlayer, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// Leave the room.
    /// </summary>
    /// <param name="action">Action.</param>
    public void LeaveRoom(GameType gameType, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //		print("LeaveRoom  : " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.LeaveRoom, action, Json.Decode(jsonObj.toString()));
    }
    public void Standup(int standUp, string roomId, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", roomId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("standUp", standUp);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);

        Debug.Log("LeaveRoom : " + jsonObj.toString());

        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.LeaveRoom, action, Json.Decode(jsonObj.toString()));
    }
    public void RunItTwiceRequest(bool twice, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("twice", twice);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Debug.Log("RunItTwiceRequest => " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.RunItTwiceRequest, action, Json.Decode(jsonObj.toString()));
    }

    public void StraddleRequest(bool straddle, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("straddle", straddle);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Debug.Log("StraddleRequest => " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.StraddleRequest, action, Json.Decode(jsonObj.toString()));
    }


    public void JoinWaitingList(SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Debug.Log("JoinWaitingList: " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.JoinWaitingList, action, Json.Decode(jsonObj.toString()));
    }

    public void LeaveWaitingList(SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Debug.Log("LeaveWaitingList: " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.LeaveWaitingList, action, Json.Decode(jsonObj.toString()));
    }

    public void AcceptSeatRequest(int seatIndex, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("seatIndex", seatIndex);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Debug.Log("AcceptSeatRequest: " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.AcceptSeatRequest, action, Json.Decode(jsonObj.toString()));
    }

    public void RejectSeatRequest(string roomId, int seatIndex, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", roomId);
        jsonObj.put("seatIndex", seatIndex);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);

        Debug.Log("RejectSeatRequest: " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.RejectSeatRequest, action, Json.Decode(jsonObj.toString()));
    }
    public void WaitingListPlayers(SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Debug.Log("WaitingListPlayers: " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.WaitingListPlayers, action, Json.Decode(jsonObj.toString()));
    }

    public void InGamePlayerGameHistory(SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Debug.Log("InGamePlayerGameHistory: " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.InGamePlayerGameHistory, action, Json.Decode(jsonObj.toString()));

    }
    public void clubTablesResult(SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Debug.Log("clubTablesResult: " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.clubTablesResult, action, Json.Decode(jsonObj.toString()));
    }
    public void TournamentLeaderboard(string tournamentId, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Debug.Log("TournamentLeaderboard: " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.TournamentLeaderboard, action, Json.Decode(jsonObj.toString()));
    }
    /// <summary>
    /// GEt data for rebuy min max amount
    /// </summary>
    /// <param name="action">Action.</param>
    public void GetPlayerReBuyInChips(SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.GetPlayerReBuyInChips, action, Json.Decode(jsonObj.toString()));
    }

    public void ShowOtherPlayersCard(SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //Debug.Log("ShowOtherPlayersCard: " + jsonObj.toString());

        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.ShowOtherPlayersCard, action, Json.Decode(jsonObj.toString()));
    }
    public void CheckLeaveRoomEligibility(SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);

        Debug.Log("CheckLeaveRoomEligibility: " + jsonObj.toString());

        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.CheckLeaveRoomEligibility, action, Json.Decode(jsonObj.toString()));
    }

    public void ShowMyCards(string gameId, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("gameId", gameId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.ShowMyCards, action, Json.Decode(jsonObj.toString()));
    }

    public void PlayerReBuyIn(string roomId, string tournamentId, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", roomId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("PlayerReBuyIn  : " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.PlayerReBuyIn, action, Json.Decode(jsonObj.toString()));
    }

    public void DeclinedReBuyIn(string roomId, string tournamentId, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", roomId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("DeclinedReBuyIn  : " + jsonObj.toString());

        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.DeclinedReBuyIn, action, Json.Decode(jsonObj.toString()));
    }
    public void GetReBuyInChips(string roomId, string tournamentId, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", roomId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);

        print("GetReBuyInChips  : " + jsonObj.toString());

        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.GetReBuyInChips, action, Json.Decode(jsonObj.toString()));
    }

    public void GetAddOnDetails(string roomId, string tournamentId, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", roomId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);

        print("GetAddOnDetails  : " + jsonObj.toString());

        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.GetAddOnDetails, action, Json.Decode(jsonObj.toString()));
    }

    public void BuyAddOnChips(string roomId, string tournamentId, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", roomId);
        jsonObj.put("tournamentId", tournamentId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("BuyAddOnChips  : " + jsonObj.toString());

        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.BuyAddOnChips, action, Json.Decode(jsonObj.toString()));
    }
    /// <summary>
    /// Rebuy call 
    /// </summary>
    /// <param name="action">Action.</param>
    public void PlayerAddChips(double chips, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("chips", chips);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.PlayerAddChips, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// Subscribes the room.
    /// </summary>
    /// <param name="roomId">Room identifier.</param>
    /// <param name="action">Action.</param>
    public void UnSubscribeRoom(GameType gameType, SocketIOAckCallback action)
    {
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("UnsubscribeRoom  : " + jsonObj.toString());


        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.UnsubscribeRoom, action, Json.Decode(jsonObj.toString()));
    }

    public void GetTokenForEssentials(SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);

        Debug.Log("GetTokenForEssentials Event: " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.GetTokenForEssentials, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// Login player.
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="password">Password.</param>
    /// <param name="action">Action.</param>
    public void GetStacks(SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //Debug.Log (Json.Decode (jsonObj.toString ()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.GetStacks, action, Json.Decode(jsonObj.toString()));
    }
    public void LogOutPlayer(SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("os", Utility.Instance.GetOSName());
        jsonObj.put("appVersion", Application.version);
        jsonObj.put("productName", Application.productName);
        //Debug.Log (Json.Decode (jsonObj.toString ()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.LogOutPlayer, action, Json.Decode(jsonObj.toString()));
    }
    public void StaticTournament(SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);

        //Debug.Log (Json.Decode (jsonObj.toString ()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.StaticTournament, action, Json.Decode(jsonObj.toString()));
    }
    public void StaticBanners(string SID, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("staticId", SID);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Debug.Log(Constants.PokerEvents.StaticBanners + " " + Json.Decode(jsonObj.toString()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.StaticBanners, action, Json.Decode(jsonObj.toString()));
    }
    /// <summary>
    /// Reconnect.
    /// </summary>
    /// <param name="PlayerId">Player ID.</param>
    /// <param name="action">Action.</param>
    public void GetReconnect(string ipAddress, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("ipAddress", ipAddress);
        jsonObj.put("os", Utility.Instance.GetOSName());
        jsonObj.put("appVersion", Application.version);
        jsonObj.put("productName", Application.productName);
        Debug.Log("GetReconnect: " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.ReconnectPlayer, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// CheckRunningGame.
    /// </summary>
    /// <param name="PlayerId">Player ID.</param>
    /// <param name="action">Action.</param>
    public void GetCheckRunningGame(SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //Debug.Log (Json.Decode (jsonObj.toString ()));
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.GetCheckRunningGame, action, Json.Decode(jsonObj.toString()));
    }

    public void GetRunningGameList(SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Debug.Log("##### GetRunningGameList: " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.GetRunningGameList, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// ReconnectGame.
    /// </summary>for reconnect to game if user is on table!
    /// <param name="PlayerId">Player ID.</param>
    /// <param name="roomId">room ID.</param>
    /// <param name="action">Action.</param>
    public void GetReconnectGame(SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        Debug.Log("Constants.PokerEvents.ReconnectGame: " + jsonObj.toString());
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.ReconnectGame, action, Json.Decode(jsonObj.toString()));
    }

    /// <summary>
    /// Chat.
    /// </summary>for reconnect to game if user is on table!
    /// <param name="PlayerId">Player ID.</param>
    /// <param name="roomId">room ID.</param>
    /// <param name="action">Action.</param>
    public void SendChat(string message, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", Constants.Poker.TableId);
        jsonObj.put("message", message);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        //Debug.Log (Json.Decode (jsonObj.toString ()));
        Game.Lobby.CashSocket.Emit(Constants.PokerEvents.Chat, action, Json.Decode(jsonObj.toString()));
    }

    public void LocationTableValidation(string roomId, double latitude, double longitude, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("roomId", roomId);
        jsonObj.put("latitude", latitude);
        jsonObj.put("longitude", longitude);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("LocationTableValidation: " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.LocationTableValidation, action, Json.Decode(jsonObj.toString()));
    }

    public void SendIPAddress(string eventName, string ipAddress, SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;

        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("eventName", eventName);
        jsonObj.put("ipAddress", ipAddress);
        jsonObj.put("os", Utility.Instance.GetOSName());
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("SendIPAddress: " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.SendIPAddress, action, Json.Decode(jsonObj.toString()));
    }

    public void RulesofPlay(SocketIOAckCallback action)
    {
        if (!HasInternetConnection())
            return;
        JSON_Object jsonObj = new JSON_Object();
        jsonObj.put("playerId", UIManager.Instance.assetOfGame.SavedLoginData.PlayerId);
        jsonObj.put("authToken", UIManager.Instance.tokenHack());
        jsonObj.put("deviceId", Utility.Instance.GetDeviceIdForOsBased());
        jsonObj.put("productName", Application.productName);
        print("RulesofPlay: " + jsonObj.toString());
        Game.Lobby.socketManager.Socket.Emit(Constants.PokerEvents.RulesofPlay, action, Json.Decode(jsonObj.toString()));
    }
    #endregion
    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS


    #endregion

    #region PRIVATE_METHODS


    #endregion

    #region COROUTINES


    #endregion


    #region GETTER_SETTER
    /// <summary>
    /// Determines whether the app has internet connection.
    /// </summary>
    /// <returns><c>true</c> if this app has internet connection; otherwise, <c>false</c>.</returns>
    public bool HasInternetConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            UIManager.Instance.DisplayMessagePanel(Constants.Messages.NoInternetConnection);
            return false;
        }
        return true;
    }

    #endregion

}
public enum PlayerStatus
{
    None,
    Subscribed,
    Waiting,
    ReBuyWait,
    Playing,
    Fold,
    Allin,
    Ideal,
}