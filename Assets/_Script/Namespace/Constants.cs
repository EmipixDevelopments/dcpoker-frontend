
using UnityEngine;

namespace Constants
{
    /// <summary>
    /// Constants.
    /// </summary>
    public class Constants
    {
        public const float CheckSocketConnectionInterval = 2f;
        public const double minRebuyAmount = 10;
        public const float foldedCardsAlpha = 0.4f;
        public const float nonfoldedCardsAlpha = 1f;
        public const int CHAT_BUBBLE_DISPLAY_TIMER = 5;
        public const string CountryCodeUrl = "https://ipapi.co/json";
    }

    /// <summary>
    /// Constants.
    /// </summary>
    public class player
    {
        public static PokerPlayerInfo PlayerInfo;
    }
    /// <summary>
    /// Message title.
    /// </summary>
    public class MessageTitle
    {
        public const string DefaultAffirmativeButtonTitle = "Yes";
        public const string DefaultNegativeButtonTitle = "No";
        public const string DefaultOkButtonTitle = "OK";
    }

    /// <summary>
    /// Tag.
    /// </summary>
    public class Tag
    {

    }

    /// <summary>
    /// Player prefs.
    /// </summary>
    public class PlayerPrefs
    {
        public const string PlayerPrefsSound = "Sound";
        public const string PlayerPrefsMusic = "Music";

        public const string PlayerPrefsRememberMe = "RememberMe";

        public const string PlayerPrefsUsername = "Username";
        public const string PlayerPrefsPassword = "Password";
    }

    /// <summary>
    /// Messages.
    /// </summary>
    public class Messages
    {

        public const string RoomToCloseOneHour = "Room will be close in next one hour.";
        public const string RoomToCloseHalfHour = "Room will be close in next half hour";
        public const string Update_Profile_Error = "Profile not updated. Please try again.";
        public const string NoInternetConnection = "Internet connection not available.";
        public const string OtherPlayerDisconnect = "The other player disconnected. Your winstreak will not be reset.";

        public const string RegistredTournamentmessage = "Running Game! Click here.";
        public const string LoggingIn = "Logging In..";
        public const string PleaseWait = "Please wait...";
        public const string SomethingWentWrong = "Something went wrong.";

        public const string Disconnected = "Disconnected from the server.";
        public const string DisconnectedServer = "Internet Connection is Low Or Server TimerOut";

        public const string UpdateChipsCredit = "<color>" + KeyChips + "</color> Chips credited into your account";
        public const string UpdateChipsDebit = "<color>" + KeyChips + "</color> Chips debited from your account";

        //		public const string RequestForChips = "Your request for <color>" + KeyChips + "</color> chips will be sent. Are you sure?";
        public const string KeyChips = "[CHIPS]";

        /// <summary>
        /// Login panel messages
        /// </summary>
        public class Login
        {
            public const int PasswordLength = 6;

            public const string PhoneCodeEmpty = "Phone code is empty.";
            public const string PhoneNumberEmpty = "Phone number is empty.";
            public const string UsernameEmpty = "Username is empty.";
            public const string PasswordEmpty = "Password is empty.";

            public const string MinPasswordLength = "Password must be at least 6 characters.";
        }

        /// <summary>
        /// Register panel messages
        /// </summary>
        public class Register
        {
            public const string UsernameEmpty = "Username is empty.";
            public const string FirstNameEmpty = "Name is empty.";
            public const string LastNameEmpty = "Last name is empty.";
            public const string EmailEmpty = "Email is empty.";
            public const string EmailInvalid = "Email is invalid.";
            public const string PasswordEmpty = "Password is empty.";
            public const string AccountNumberEmpty = "ID is empty.";
            public const string NewPasswordEmpty = "New password is empty.";
            public const string ConfirmPasswordEmpty = "Confirm password is empty.";
            public const string PasswordNotMatched = "Password not matched.";
            public const string MobileEmpty = "Mobile is empty.";
            public const string MobileInvalid = "Mobile is invalid. Must be at least 10 digits or up.";
            public const string UsernameInvalid = "Username is invalid. Must be at least 4 digits or up.";
            public const string paypal = "Paypal/Venmo Id is empty";

            public const string MinPasswordLength = "Password must be at least 6 characters.";
        }

        public class TransferChips
        {
            public const string ChipsAmountEmpty = "Chips amount is empty.";
            public const string ChipsAmountGreaterThan = "Chips amount should greater than";
        }

        /// <summary>
        /// Lobby panel messages.
        /// </summary>
        public class Lobby
        {
            public const string LogoutConfirmation = "You will be logout. Are you sure?";
            public const string ExitGameConfirmation = "Game will be closed. Are you sure?";
        }

        /// <summary>
        /// Create table panel messages
        /// </summary>
        public class CreateTable
        {
            public const string TableNameEmpty = "Table name is empty.";
        }

        /// <summary>
        /// Game panel messages.
        /// </summary>
        public class Game
        {

            public const string NotEnoughChipsToPlayThisTable = "You do not have enough chips to play in this table.";
            public const string RedirectToLobbyConfirmation = "Are you sure you want to quit? Quitting may reset your win streak.";
            public const string BeforeGamePlayText = "Make sure to be at the table before the tournament begins! If you are not present at the table, you will not be permitted into the tournament and lose your TC entry.";
            public const string AfterGamePlayText = "If you leave the tournament, you will not be allowed back in.";
            public const string LeaveGamePlayText = "Are you sure you want to quit?\u0003\u0003\u0003\u0003\u0003 Quitting may reset your win streak.\u0003";
            public const string NotEnoughChips = "You do not have enough chips.";

        }



        /// <summary>
        /// In app panel messages.
        /// </summary>
        public class InApp
        {
            public const string RequestForChips = "Your request for <color>" + KeyChips + "</color> chips will be sent. Are you sure?";
            public const string KeyChips = "[CHIPS]";
        }
    }

    /// <summary>
    /// Poker API.
    /// </summary>
    public class PokerAPI
    {
        public static string BaseUrl
        {
            get
            {
                switch (UIManager.Instance.server)
                {
                    case SERVER.Macau:
                        return MacauBaseUrl;
                    case SERVER.Club:
                        return clubBaseUrl;
                    //case SERVER.Staging:
                    //    return StagingUrl;
                    case SERVER.Developer:
                        return DevelopmentBaseUrl;
                    case SERVER.Custom:
                        return UIManager.Instance.CustormUrl;
                }
                return StagingUrl;
            }
        }

        public const string KeyResult = "result";
        public const string KeyMessage = "message";
        public const string KeyssStatus = "status";

        public const string KeyStatusSuccess = "success";
        public const string KeyStatusFail = "fail";


        public static string EssentialLiveUrl = "https://mgl2020.com";


        public static string MacauBaseUrl = "https://macau-gold.com";
        public static string clubBaseUrl = "https://macau.aistechnolabs.club";
        public static string StagingUrl = "https://pokerbet.aistechnolabs.pro";
        public static string DevelopmentBaseUrl = "http://localhost:401"; //"https://pokerbet.aistechnolabs.in";


        public const string KeyPlayerIdUrl = "[PLAYER_ID]";
        public static string UpdateProfileUrl = BaseUrl + "/rest-api/v1/player/updateUser";
        public const string FieldAuthorization = "Authorization";
        public const string PlayerProfileAvatar = "home/developer/poker-sharks/public/backend/img/users/";

        public const string KeyBody = "body";

        public const string KeyStatusCode = "statusCode";
        public const int StatusCodeSuccessValue = 200;

        public const string StatusAuthorized = "authorized";
        public const string StatusUnauthorized = "unauthorized";
        public const string StatusFailed = "failed";

        public const string GenderMale = "Male";
        public const string GenderFemale = "Female";
        public const string GenderOther = "Other";
    }

    /// <summary>
    /// Poker.
    /// </summary>
    public class Poker
    {
        public static string roomId = "";
        public static string TableId = "";
        public static string TournamentId = "";
        public static string GameId = "";
        public static string TableType = "";

        public static string TableNumber = "";
        public static string GameNumber = "";

        public const float OnOffTurnAnimTime = .25f;

        public static Vector2 OffTurnScale = Vector2.one;
        public static Vector2 OnTurnScale = Vector2.one * 1.15f;

        public const float OwnPlayerPostionMargin = 15f;

        public const string CardAnimationTrigger = "card-flip";
        public const string SpinHandleTrigger = "spin";

        public static double MinBuyinAmount;
        public static double MaxBuyinAmount;

        public static double SmallBlindAmount;
        public static double BigBlindAmount;


        public const float FoldCanvasAlpha = .4f;

        public const float HandMinRank = 0f;
        public const float HandMaxRank = 303f;

        public const float MatchedCardAlpha = 1f;
        public const float NotMatchedCardAlpha = .25f;

        /// <summary>
        /// The card distribution speed.
        /// </summary>
        public const float CardDistributionSpeed = 5f;

        /// <summary>
        /// The player chat display time in seconds.
        /// </summary>
        public const float PlayerChatDisplayTime = 3f;

        /// <summary>
        /// The player Action display time in seconds.
        /// </summary>
        public const float PlayeractionDisplayTime = 1.5f;

        /// <summary>
        /// The dealer message display time.
        /// </summary>
        public const float DealerMessageDisplayTime = 2f;

        /// <summary>
        /// The tip amount.
        /// </summary>
        public static long TipAmount = 10;

        /// <summary>
        /// The refresh table interval in seconds.
        /// </summary>
        public const int RefreshTableInterval = 15;
        /// <summary>
        /// The refresh tournamentRegistretedtable interval in seconds.
        /// </summary>
        public const int RefreshRegistredTableInterval = 2;

        public const string PokerPlayerStatusLeft = "Left";
    }

    public class PokerStatus
    {
        public static string RunningStatus = "Running";
    }
    /// <summary>
    /// Poker events
    /// </summary>
    public class PokerEvents
    {
        public const string Connect = "connect";
        public const string Disconnect = "disconnect";
        public const string Login = "LoginPlayer";
        public const string VerifyIdentifierToken = "VerifyIdentifierToken";
        public const string RoomPlayerDetails = "RoomPlayerDetails";
        public const string ReconnectPlayer = "ReconnectPlayer";
        public const string ReconnectGame = "ReconnectGame";
        public const string GetCheckRunningGame = "CheckRunningGame";
        public const string GetRunningGameList = "GetRunningGameList";
        public const string Register = "RegisterPlayer";
        public const string GetStacks = "GetStacks";
        public const string StaticTournament = "StaticTournament";
        public const string Banner = "Banner";
        public const string StaticBanners = "StaticBanners";
        public const string SearchLobby = "SearchLobby";
        public const string SearchTournamentLobby = "SearchTournamentLobby";
        public const string JoinTournamentRoom = "JoinTournamentRoom";
        public const string SearchSngLobby = "SearchSngLobby";
        public const string TournamentInfo = "TournamentInfo";
        public const string SngTournamentInfo = "SngTournamentInfo";
        public const string MyTournament = "MyTournament";

        public const string TournamentPlayers = "TournamentPlayers";
        public const string TournamentTables = "TournamentTables";
        public const string TournamentPayout = "TournamentPayout";
        public const string TournamentBlinds = "TournamentBlinds";

        public const string SngTournamentPlayers = "SngTournamentPlayers";
        public const string SngTournamentTables = "SngTournamentTables";
        public const string SngTournamentPayout = "SngTournamentPayout";
        public const string SngTournamentBlinds = "SngTournamentBlinds";


        public const string RegisterTournament = "RegisterTournament";
        public const string UnRegisterTournament = "UnRegisterTournament";
        public const string RegisterSngTournament = "RegisterSngTournament";
        public const string UnRegisterSngTournament = "UnRegisterSngTournament";


        public const string JoinTournament = "JoinTournament";
        public const string JoinSngTournament = "JoinTournament";
        public const string RejectTournament = "RejectTournament";
        public static string LateJoinTournament = "LateJoinTournament";

        public const string UploadDepositReceipt = "UploadDepositReceipt";
        public const string Withdrawal = "Withdrawal";

        public const string UpdateAccountNumber = "UpdateAccountNumber";
        public const string Playerprofile = "Playerprofile";
        public const string Leaderboard = "Leaderboard";
        public const string GetPlayerAccountInfo = "GetPlayerAccountInfo";
        public const string purchaseHistory = "purchaseHistory";
        public const string playerChangePassword = "playerChangePassword";
        public const string TransferChips = "TransferChips";
        public const string TransferCash = "TransferCash";
        public const string ChangeUsername = "ChangeUsername";
        public const string playerNewPassword = "playerNewPassword"; // old logic

        public const string PlayerForgotPassword = "playerForgotPassword";
        public const string PlayerConfirmForgotPasswordCode = "playerConfirmForgotPasswordCode";
        public const string PlayerChangeForgetPassword = "playerChangeForgotPassword";

        public const string newsBlog = "newsBlog";
        public const string playerProfilePic = "playerProfilePic";
        public const string GetReBuyInChips = "GetReBuyInChips";
        public const string GetAddOnDetails = "GetAddOnDetails";
        public const string BuyAddOnChips = "BuyAddOnChips";

        public const string PlayerReBuyIn = "PlayerReBuyIn";
        public const string DeclinedReBuyIn = "DeclinedReBuyIn";

        public const string GameHistoryList = "playerGameHistory";
        public const string UpdateProfile = "UpdateProfile";
        public const string PrivateRoomLogin = "PrivateRoomLogin";

        public const string UpdateIsCash = "updateIsCash";
        //public const string СontactUs = "contactUs";

        public const string DeletePlayerSendCode = "deletePlayerSendCode";
        public const string DeletePlayerConfirmCode = "deletePlayerConfirmCode";
        public const string DeletePlayer = "deletePlayer";

        public const string ListRooms = "ListTouFrnaments";
        public const string PlayerLeaderBoard = "PlayerLeaderBoard";
        public const string SupportAdmin = "SupportAdmin";
        public const string ListApprovedTournaments = "ListApprovedTournaments";
        public const string RegisterInTournament = "RegisterTournament";
        public const string CreateRoom = "CreateRoom";
        public const string RoomInfo = "RoomInfo";
        public const string SubscribeRoom = "SubscribeRoom";
        public const string JoinRoom = "JoinRoom";
        public const string GetBuyinsAndPlayerchips = "GetBuyinsAndPlayerchips";
        public const string BuyinAmount = "BuyinAmount";
        public const string LeaveRoom = "LeaveRoom";
        public const string GetPlayerReBuyInChips = "GetPlayerReBuyInChips";
        public const string ShowOtherPlayersCard = "ShowOtherPlayersCard";

        public const string PlayerAddChips = "PlayerAddChips";
        public const string UnsubscribeRoom = "UnSubscribeRoom";
        public const string TournamentWinner = "TournamentWinner";
        public const string ShowMyCards = "ShowMyCards";

        public const string GameListing = "GameListing";

        public const string OnSubscribeRoom = "OnSubscribeRoom";
        public const string PlayerInfo = "PlayerInfo";
        public const string PlayerInfoList = "playerInfoList";
        public const string PlayerLeft = "PlayerLeft";
        public const string PlayerAction = "PlayerAction";
        public const string PlayersCards = "PlayersCards";
        public const string ShowFoldedPlayerCards = "ShowFoldedPlayerCards";
        public const string PlayerOnline = "PlayerOnline";
        //		public const string PlayerActionNew = "PlayerActionNew";
        public const string InviteFriend = "InviteFriend";
        public const string UpdateAutoBuyIn = "UpdateAutoBuyIn";
        public const string Playerdetail = "Playerdetail";
        public const string WaitForBigBlindEvent = "WaitForBigBlindEvent";

        public const string Chat = "Chat";
        public const string IAmBack = "onIdealPlayer";
        public const string OnGameBoot = "OnGameBoot";
        public const string OnPlayerCards = "OnPlayerCards";
        public const string superPlayerData = "superPlayerData";
        public const string OnPlayersCardsDistribution = "OnPlayersCardsDistribution";
        public const string GameFinishedPlayersCards = "GameFinishedPlayersCards";
        public const string OnSubscibePlayersCards = "OnSubscibePlayersCards";
        public const string OnOpenBuyInPanel = "OnOpenBuyInPanel";
        public const string DisplayShowCardButton = "DisplayShowCardButton";
        public const string ShowCardResult = "ShowCardResult";
        public const string WaitForBigBlind = "WaitForBigBlind";
        public const string GamePopupNotification = "GamePopupNotification";
        
        public const string SendContactUs = "sendContactUs";
        public const string ContactUs = "contactUs";
        public const string ReadContactUs = "readContactUs";

        public const string RoundStarted = "NewRoundStarted";
        public const string GameStarted = "GameStarted";
        public const string PlayerCards = "PlayerCards";
        public const string PlayerCardsReconnect = "PlayerCardsReconnect";

        public const string TableCards = "TableCards";
        public const string RoundComplete = "RoundComplete";
        public const string GameFinished = "GameFinished";
        public const string NextTurnPlayer = "NextTurnPlayer";
        public const string ResetGame = "ResetGame";
        public const string OnTurnTimer = "OnTurnTimer";
        public const string RevertPoint = "RevertPoint";
        public const string RevertPointFolded = "RevertPointFolded";
        public const string OnGameStartWait = "OnGameStartWait";
        public const string OnSngTournamentFinished = "SngTournamentFinished";
        public const string OnSwitchRoom = "OnSwitchRoom";
        public const string RegularTournamentFinished = "RegularTournamentFinished";
        public const string OnBreak = "OnBreak";
        public const string OnTournamentTableBreakMessage = "OnTournamentTableBreakMessage";

        public const string OnBlindLevelsRaised = "OnBlindLevelsRaised";
        public const string OnBlindLevels = "OnBlindLevels";

        public const string PlayerHandInfo = "PlayerHandInfo";
        public const string TurnOnRunItTwice = "TurnOnRunItTwice";
        public const string TurnOffRunItTwice = "TurnOffRunItTwice";
        public const string cancelTwice = "cancelTwice";

        public const string RunItTwiceResponse = "RunItTwiceResponse";
        public const string TurnOnStraddle = "TurnOnStraddle";
        public const string TurnOffStraddle = "TurnOffStraddle";
        public const string StraddleTwiceTimer = "StraddleTwiceTimer";
        public const string OnPlayerKicked = "OnPlayerKicked";

        public const string OnReservedSeatList = "OnReservedSeatList";
        public const string onAbsolutePlayer = "onAbsolutePlayer";
        public const string TournamentBounty = "TournamentBounty";
        public const string AddOnTimer = "AddOnTimer";
        public const string CancelTournament = "CancelTournament";
        public const string BanPlayerTournament = "BanPlayerTournament";
        public const string RoomDeleted = "RoomDeleted";
        public const string TableBalanceNotice = "TableBalanceNotice";



        public const string OnWaitingJoinRoom = "OnWaitingJoinRoom";
        public const string OffWaitingJoinRoom = "OffWaitingJoinRoom";
        public const string Gift = "PokerPlayerGift";
        public const string GameHistory = "GameHistory";
        public const string ExtraTimer = "ExtraTimer";
        public const string DefaultActionSelection = "DefaultActionSelection";
        public const string SitOutNextHand = "SitOutNextHand";
        public const string SitOutNextBigBlind = "SitOutNextBigBlind";
        public const string PlayerMuckAction = "PlayerMuckAction";
        public const string AbsolutePlayer = "AbsolutePlayer";
        public const string TipToDealer = "Tip";
        public const string RequestChips = "Requestchips";
        public const string PlayerChipsUpdated = "PlayerChipsUpdated";

        public const string RoomToCloseOneHour = "RoomToCloseOneHour";
        public const string RoomToCloseHalfHour = "RoomToCloseHalfHour";
        public const string ResetGamecall = "ResetGame";

        public const string LogOutPlayer = "LogOutPlayer";

        public const string PokerPlayerTournamentResponse = "PokerPlayerTournamentResponse";
        public static string SOCKET_EVENT_OnTournamentStart = "OnTournamentStart";
        public static string SOCKET_EVENT_OnSngTournamentStart = "OnSngTournamentStart";

        public const string AvailableInAppPurchase = "AvailableInAppPurchase";
        public const string VerifyInApp = "VerifyInApp";
        public const string SendIPAddress = "SendIPAddress";
        public const string LocationTableValidation = "LocationTableValidation";
        public const string RulesofPlay = "RulesofPlay";

        public const string OnReBuyIn = "OnReBuyIn";
        public const string OnCloseReBuyIn = "OnCloseReBuyIn";

        public const string OnTournamentPrize = "OnTournamentPrize";

        public const string GetTokenForEssentials = "GetTokenForEssentials";

        public const string CheckLeaveRoomEligibility = "CheckLeaveRoomEligibility";

        public const string RunItTwiceRequest = "RunItTwiceRequest";
        public const string StraddleRequest = "StraddleRequest";
        public static string JoinWaitingList = "JoinWaitingList";
        public static string LeaveWaitingList = "LeaveWaitingList";
        public static string AcceptSeatRequest = "AcceptSeatRequest";
        public static string RejectSeatRequest = "RejectSeatRequest";
        public static string WaitingListPlayers = "WaitingListPlayers";
        public static string InGamePlayerGameHistory = "InGamePlayerGameHistory";
        public static string clubTablesResult = "clubTablesResult";
        public static string TournamentLeaderboard = "TournamentLeaderboard";

        public static string Transactions = "transactions";
    }
    
    
    

    /// <summary>
    /// keys for save/load data
    /// </summary>
    public enum TablePlayerPrefsKeys 
    {
        // Regular Tournament
        TournamentTableSettingsPlayerPerTableFilter,
        TournamentTableSettingsGameFilter,
        TournamentTableSettingsPriceFilter,
        // Taxes holdem
        TaxesHoldemTableSettingsPriceFilter,
        TaxesHoldemTableSettingsPlayerPerTableFilter,
        // Omaha
        OmahaTableSettingsPriceFilter,
        OmahaTableSettingsPlayerPerTableFilter,
        // Plo5
        Plo5TableSettingsPriceFilter,
        Plo5TableSettingsPlayerPerTableFilter,
        // Regular Tournament
        SitNGoTableSettingsPlayerPerTableFilter,
        SitNGoTableSettingsGameFilter,
        SitNGoTableSettingsPriceFilter,
    }
    
}

public enum PokerCardSuit
{
    S = 0,
    D = 1,
    C = 2,
    H = 3
}

public enum PokerCardRank
{
    two = 0,
    three = 1,
    four = 2,
    five = 3,
    six = 4,
    seven = 5,
    eight = 6,
    nine = 7,
    ten = 8,
    jack = 9,
    queen = 10,
    king = 11,
    ace = 12
}

public enum PokerGameRound
{
    Preflop,
    Flop,
    Turn,
    River,
    Showdown
}

public enum PokerPlayerAction
{
    SmallBlind = 0,
    BigBlind = 1,
    Check = 2,
    Bet = 3,
    Call = 4,
    Fold = 6,
    Timeout = 7,
    Allin = 8
}

public enum PokerGameStatus
{
    Stopped = 0,
    Running = 1,
    Paused = 2,
    Resumed = 3,
    Finished = 4,
    CardDistribute = 5,
    Restart = 6
}

public enum PokerHandRank
{
    RoyalFlush = 10,
    StraightFlush = 9,
    FourOfAKind = 8,
    FullHouse = 7,
    Flush = 6,
    Straight = 5,
    ThreeOfAKind = 4,
    TwoPair = 3,
    OnePair = 2,
    HighCard = 1
}

public enum GameSpeed
{
    regular,
    turbo,
    hyper_turbo
}

public enum SERVER
{
    Macau,
    Club,
    //Staging,
    Developer,
    Custom
}
public enum GameType
{
    cash, // what is it cash?
    sng,
    Touranment,
    texas,
    omaha,
    PLO5
}

public enum PreBet
{
    Check,
    Call,
    CallAny,
    Fold,
    AutoPost,
    SitOut,
    None
}

public enum LimitType
{
    All, //0
    Limit, //1
    No_Limit, //2
    Pot_Limit //3
}
public enum PlayerPerTableStuff
{
    All = 9,
    two = 2,
    three = 3,
    four = 4,
    five = 5,
    six = 6,
    seven = 7,
    eight = 8,
}

public enum TableType
{
    Normal,
    Tournament,
    Special
}

public enum PokerGameType
{
    all,
    texas,
    omaha,
    PLO5
}

public enum TournamentGameType
{
    reg,
    sng,
    PLO5
}