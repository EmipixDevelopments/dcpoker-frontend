using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class PokerEventResponse
{
    public string status;
    public RecoveryPhraseEventResponse result;
    public string message;
}

[Serializable]
public class RecoveryPhraseEventResponse
{
    public string playerId;
    public string username;
    public double chips;
    public double cash;
    public string publicKey;
    public string recoveryPhrase;
    public string password;
    public int[] privateKey;
}

[Serializable]
public class PokerEventResponse<T> where T : class
{
    public string status;
    public T result;
    public string message;
    public string statusCode;
}

[Serializable]
public class PokerEventListResponse<T> where T : class
{
    public PokerEventListResponse()
    {
        result = new List<T>();
    }

    public string status;
    public List<T> result;
    public string message;
    public string statusCode;
}

[Serializable]
public class PlayerLoginResponse
{
    public string storeUrl = "";
    public string username; // used in server
    public string email;
    public string firstname;
    public string lastname;
    public string mobile; // used in server
    public string gender;
    public double chips; // used in server
    public double cash;
    public string status;
    public string activationCode;
    public string createdAt;
    public string updatedAt;
    public string id;
    public string playerId; // used in server
    public string socketId;
    public string token;
    public string timeZone;
    public int profilePic; // used in server
    public int winningCount;
    public int winningToken;
    public int winningTotal;
    public int loseTotal;

    public int winningTournament;
    public int totalTournament;
    public int winningSpecial;
    public int totalSpecial;
    public bool isCash; // used in server
    public bool isSuperPlayer = false; // used in server
    public string accountNumber; // used in server
    public string webGLDeviceId = ""; // used in server
    public bool isMultipleTableAllowed = true; // used in server
    public bool isInAppPurchaseAllowed = true;
    public bool isChipsTransferAllowed = false; // used in server

    public string userUuid = "";
    public string deviceId = "";
}

[Serializable]
public class RoomsListing
{
    public RoomsListing()
    {
        result = new List<Room>();
    }

    public List<Room> result;
    public string message;
    public string status;
    public string statusCode;

    [Serializable]
    public class Room
    {
        public string tournamentId = "";
        public string roomId = "";
        public string id = "";
        public string roomName = "";
        public string status = "";
        public string stake = "";
        public string currencyType = "";
        public string type = "";
        public int playerCount = 0;
        public int maxPlayers = 0;
        public double pot = 0;
        public double minBuyIn = 0;
        public double maxBuyIn = 0;
        public string gameLimit = "";
        public bool isLimitGame;
        public double smallBlind = 0;
        public double bigBlind = 0;
        public bool isTournament = false;
        public bool isPasswordProtected = false;
        public bool isGPSRestriction = false;
        public bool muck = false;
        public bool isIPAddressRestriction = false;
        public string namespaceString = "cash_texas";
        public string pokerGameType = "texas";

        public string pokerGameFormat = "cash";

        public string tableNumber = "";
        public string name = "";
    }
}

[Serializable]
public class TournamentList
{
    public TournamentList()
    {
        result = new List<Room>();
    }

    public List<Room> result;
    public string message;
    public string status;
    public string statusCode;

    [Serializable]
    public class Room
    {
        public string roomId = "";
        public string roomName;
        public string tableType;
        public string startDate;
        public double entryChips;
        public double prizePool;
        public int playerLeft;
        public string timeLeft;
        public int isJoinable;
    }
}

[Serializable]
public class PlayerTournamentList
{
    public List<PlayerTournament> result;
    public string message;
    public string status;
    public string statusCode;

    [Serializable]
    public class PlayerTournament
    {
        public int position;
        public double winPrizePool;
        public string playedDate;
        public string roomName;
    }
}

[Serializable]
public class LeaderboardListingData
{
    public MyData myData;
    public List<MyData> data;

    [Serializable]
    public class MyData
    {
        public string playerId;

        public int rank;
        public string type;
        public int playerPic;
        public int winStreak;
        public string playerName;
        public int winRatio;
        public int wonGames;
        public int gameCount;
    }
}

[Serializable]
public class PlayerInfoList
{
    public List<PlayerInfos> playerInfo;
    public string dealerPlayerId;
    public string smallBlindPlayerId;
    public string bigBlindPlayerId;
    public string roomId = "";
    public string tournamenId = "";

    [Serializable]
    public class PlayerInfos
    {
        public string id = "";
        public string username = "";
        public double chips = 0;
        public double waitingGameChips = 0;
        public bool runItTwice = false;
        public double betAmount = 0;
        public bool folded;
        public bool allIn;
        public bool talked;
        public bool idealPlayer;
        public bool absolute = false;
        public bool isSuperPlayer = false;
        public bool isCardsSeen = false;
        public bool isRebuyIn = false;
        public int seatIndex;
        public List<string> cards;
        public string status;
        public int avatar;
        public string fb_avatar;
        public string token;
        public double dueSeconds;
        public string startTime;
        public string currentTime;
        public string absoluteTimeCurrentTime;
        public string absoluteTimeSartTime;
        public PlayerProfile player;
        public WaitForBigBlindData waitForBigBlindData = null;
    }

    public PlayerSidePot PlayerSidePot = null;
}

[Serializable]
public class WaitForBigBlindData
{
    public bool waitForBigBlindCheckbox = false;
    public bool waitForBigBlindCheckboxValue = false;
}

[Serializable]
public class PokerPlayerInfo
{
    public string id;
    public string username;
    public double chips;
    public bool folded;
    public bool allIn;
    public bool talked;
    public int seatIndex;
    public List<string> cards;
    public string status;
    public string avatar;
    public string fb_avatar;
    public string token;
    public PlayerProfile player;
}

[Serializable]
public class PokerTable
{
    public string id;
    public string tableNumber;
    public double smallBlind;
    public double bigBlind;
    public int minPlayers;
    public int maxPlayers;
    public double minBuyIn;
    public double maxBuyIn;
    public string name;
    public bool isLimitGame;
    public List<string> joinedPlayer;
}

[Serializable]
public class PokerTableNew
{
    public string roomId = "";
    public string roomName;
    public string tableType;
    public string startDate;
    public double entryChips;
    public double prizePool;
    public int playerLeft;
}

[Serializable]
public class PokerJoinRoom
{
    public string status;
    public string message;
    public JoinRoomData data;
}

[Serializable]
public class JoinRoomData
{
    public int id;
    public string name;
    public double smallBlind;
    public double bigBlind;
    public double minPlayers;
    public double maxPlayers;
    public double minBuyIn;
    public double maxBuyIn;
    public int dealer;
    public TurnBet turnBet;
    public List<PokerPlayerInfo> players;
    public List<PokerPlayerInfo> playersToRemove;
    public List<PokerPlayerInfo> playersToAdd;
    public List<PokerPlayerInfo> gameWinners;
    public List<PokerPlayerInfo> gameLosers;
    public object game;
}

[Serializable]
public class TurnBet
{
}

[Serializable]
public class PokerPlayerCards
{
    public string roomId = "";
    public List<PC> players;
    public string smallBlindPlayerId;
    public string bigBlindPlayerId;
    public string dealerPlayerId;
    public double smallBlindChips;
    public double bigBlindChips;
    public string gameId;

    [Serializable]
    public class PC
    {
        public string playerId;
        public List<string> cards;
        public double chips;
    }
}

[Serializable]
public class PokerTableCards
{
    public TC tableCards;

    [Serializable]
    public class TC
    {
        public List<string> cards;
    }
}

[Serializable]
public class PlayerAction
{
    public PA action;
    public double playerBuyIn;
    public double totalTablePotAmount = 0;
    public List<Player> players;
    public string roomId = "";

    [Serializable]
    public class PA
    {
        public string roomId = "";
        public string playerId;
        public double betAmount;
        public double totalBetAmount;
        public PokerPlayerAction action;

        public bool hasRaised;
        public string gameRound;
    }

    [Serializable]
    public class Player
    {
        public string id;
        public double chips;
        public int winstreak;
    }
}

[Serializable]
public class PlayerActionNew
{
    public Action action;
    public List<Player> player;
    public string tag;
    public DateTime time;

    [Serializable]
    public class Action
    {
        public PokerPlayerAction action;
        public string playerId;
        public double betAmount;
        public bool hasRaised;
    }

    [Serializable]
    public class Player
    {
        public string id;
        public double chips;
        public int winstreak;
    }
}

[Serializable]
public class RoundComplete
{
    public string roomId = "";
    public string roundStarted;
    public List<string> cards;
    public List<string> extraCards = new List<string>();
    public double potAmount;
    public int extraCardsPosition;

    public PlayerSidePot PlayerSidePot;
}

[Serializable]
public class PlayerSidePot
{
    public List<SidePot> sidePot;
    public double mainPot = 0;

    [Serializable]
    public class SidePot
    {
        public string sidePotPlayerID;
        public double sidePotAmount;
        public int sidePotPlayerSeatIndex;
    }
}

//
//[Serializable]
//public class PokerGameWinner
//{
//	public List<Wnr> winners;
//	public int headsUpFinished;
//	[Serializable]
//	public class Wnr
//	{
//		public string playerId;
//		public string playerName;
//		public double amount;
//		public PokerHandStrength hand;
//		public double chips;
//	}
//}


[Serializable]
public class PokerGameWinner
{
    public List<Wnr> winners;
    public List<Player> players;
    public int headsUpFinished;
    public string previousGameId = "";
    public string previousGameNumber = "";
    public string roomId = "";

    [Serializable]
    public class Wnr
    {
        public string roomId = "";
        public string playerId;
        public string playerName;
        public double amount;

        public string winningType;

        //		public PokerHandStrength hand;
        public double chips;
        public int sidePotPlayerIndex;
        public string sidePotPlayerId;
        public int winnerSeatIndex;
        public List<string> bestCards;
    }

    [Serializable]
    public class Player
    {
        public string id;
        public double chips;
        public int winstreak;
    }
}

[Serializable]
public class PokerHandStrength
{
    public List<string> cards;
    public float rank;
    public string message;
    public List<string> bestCards;
}

[Serializable]
public class PokerTournamentGameWinner
{
    //	public List<TournamentWnr> result;
    //
    //	[Serializable]
    //	public class TournamentWnr
    //	{
    public string playerId;
    public string playerName;
    public double amount;
    public double chips;
    public int leaveIndex;
    public int profilePic;

    public PokerTournamentHandStrength hand;
    //	}
}

[Serializable]
public class PokerTournamentHandStrength
{
    public List<string> cards;
    public float rank;
    public string message;
    public List<string> bestCards;
}

[Serializable]
public class GetPlayerReBuyInChips
{
    public string roomId = "";
    public double minBuyIn;
    public double maxBuyIn;
    public double playerChips;
}

[Serializable]
public class PokerGameHistory
{
    public GA gameHistory;
    public int turnTime;
    public string roomId = "";
    public string gameId = "";
    public string tableNumber;
    public bool isTournament = false;
    public string nextTurnPlayerId;
    public string limitType;
    public double oldPlayerBuyIn = 0;
    public double minBuyIn;
    public double maxBuyIn;
    public double smallBlindChips;
    public double bigBlindChips;
    public string straddlePlayerId;
    public double straddleChips;
    public double straddlePlayerChips;
    public bool isRebuyIn = false;
    public int remainRebuySec = 0;
    public string previousGameNumber = "";
    public string previousGameId = "";
    public OnBlindLevelsData blindLevelsData = null;
    public string gameStatus;
    public DefaultButtons defaultButtons;

    public bool roomIdChanged = false;
    public string tournamentTableWaitMessage = "";

    [Serializable]
    public class GA
    {
        public List<PlayerAction.PA> history;
        public string currentRound;
        public List<string> cards;
        public List<string> extraCards; // = new List<string>();
        public double potAmount;
        public PlayerSidePot PlayerSidePot;
        public double totalTablePotAmount = 0;
    }
}

[Serializable]
public class joinroomResult
{
    public string roomId;
    public int turnTime;
    public string gameStatus;
}

[Serializable]
public class DefaultButtons
{
    public bool sitOutNextHand = false;
    public bool sitOutNextBigBlind = false;
    public bool isFold = false;
    public bool isCheck = false;
    public bool isCall = false;
}

[Serializable]
public class MResult
{
    public string roomId = "";
    public string roomName;
}

[Serializable]
public class MRootObject
{
    public MRootObject()
    {
        result = new List<MResult>();
    }

    public List<MResult> result;
    public string message;
    public string status;
    public string statusCode;
}

[Serializable]
public class AProovedTournament
{
    public string response;
}

[Serializable]
public class GameStarted
{
    public string message;
    public string gameId;
    public string gameNumber;
    public List<Player> player;
    public GameData gameData;

    [Serializable]
    public class Player
    {
        public string id;
        public double chips;
        public int winstreak;
    }

    [Serializable]
    public class GameData
    {
        public string smallBlindPlayerId;
        public string bigBlindPlayerId;
        public string dealerPlayerId;
        public double smallBlindChips;
        public double bigBlindChips;
    }
}

[Serializable]
public class OnTurnTimer
{
    public string playerId;
    public float timer;
    public float maxTimer;
    public string roomId = "";
    public ButtonAction buttonAction;
    public bool isLimitGame = false;
}

[Serializable]
public class ButtonAction
{
    public bool allIn;
    public bool check;
    public bool call;
    public bool raise;
    public bool bet;
    public bool callAny;
    public double minRaise;
    public double maxRaise;
    public double callAmount;
    public double allInAmount;
    public double betAmount;
}

[Serializable]
public class GameHandHistoryResult
{
    public List<GameHistoryList> gamesHistoryList;
    public int recordsTotal = 0;
    public int currentPage = 0;
    public int totalPages = 0;
    public int resultPerPage = 10;

    [Serializable]
    public class GameHistoryList
    {
        public string gameId;
        public string dateTime;
        public string gameName;
        public List<string> handCards;
        public Winner winner;
    }

    [Serializable]
    public class Winner
    {
        public double winningAmount;
        public List<string> winningHands;
    }
}

/*
public class FullGameHistoryResult
{
	public GameHistory gameHistory;

	[Serializable]
	public class GameHistory
	{
		public string dataTime;
		public string gameName;
		public string stack;
		public string gameId;
		public List<Players> players;
		public List<Winners> winners;
		public List<HandsEvents> handsEvents;

		[Serializable]
		public class Players
		{
			public string id;
			public string playerName;
			public List<string> cards;
		}

		[Serializable]
		public class Winners
		{
			public string id;
			public string playerName;
			public double winningAmount;
			public List<string> winningHands;
		}

		[Serializable]
		public class HandsEvents
		{
			public string id;
			public string playerName;
			public string gameRound;
			public string playerAction;
			public double betAmount;
			public List<string> cards;
		}
	}		
}*/
[Serializable]
public class FullGameHistoryResult
{
    public GameHistory gameHistory;

    [Serializable]
    public class GameHistory
    {
        public string dataTime;
        public string gameName;
        public string stack;
        public string gameId;
        public List<Player> players;
        public List<Winner> winners;
        public List<HandsEvent> handsEvents;
        public TableCards tableCards;

        [Serializable]
        public class Player
        {
            public string id;
            public string playerName;
            public List<string> cards;
        }

        [Serializable]
        public class Winner
        {
            public string id;
            public string playerName;
            public double winningAmount;
            public List<string> winningHands;
        }

        [Serializable]
        public class HandsEvent
        {
            public string id;
            public string playerName;
            public string gameRound;
            public string playerAction;
            public double betAmount;
            public List<string> cards;
        }

        [Serializable]
        public class TableCards
        {
            public List<string> Flop = new List<string>();
            public List<string> Turn = new List<string>();
            public List<string> River = new List<string>();
        }
    }
}


[Serializable]
public class StacksUpdateResult
{
    public string _id;
    public double minStack;
    public double maxStack;
    public string flag;
    public int __v;
}

[Serializable]
public class StacksUpdate
{
    public string status;
    public List<StacksUpdateResult> result;
    public string message;
    public int statusCode;
}

[Serializable]
public class Profile
{
    public string playerId;
    public string email;
    public string username;
    public int avatar;
    public double chips;
    public double cash;
    public long mobile;
}

[Serializable]
public class BannerDataRequest
{
    public string status;
    public BannerData result;

    [Serializable]
    public class BannerData
    {
        // public string _id; // not used
        public string position;
        public string tournamentId;

        public string image;
        // public int __v; // not used
    }
}

[Serializable]
public class TournamentRoomObject
{
    public TournamentRoomObject()
    {
        result = new List<TournamentRoom>();
    }

    public string status;
    public List<TournamentRoom> result;
    public string message;
    public int statusCode;

    [Serializable]
    public class TournamentRoom
    {
        /*	public string type ;
		public string name ;
		public int seat ;
		public int blinds ;
		public int minBuyIn ;
		public int playerCount;
		public int maxPlayers;
		public string status;*/
        public string id;
        public string tournamentId;
        public string roomId = "";
        public string type;
        public string name;
        public string seat;
        public string blinds;
        public string buyIn;
        public string status;
        public string pokerGameType = "";
    }
}

[Serializable]
public class RunItTwiceRoundCompleteResponse
{
    public long topMainPot = 0;

    public long mainPot1 = 0;
    public long mainPot2 = 0;

    public long sidePot1 = 0;
    public long sidePot2 = 0;

    public string roomId = "";
}

[Serializable]
public class NormalTournamentDetails
{
    public NormalTournamentDetails()
    {
        result = new List<NormalTournamentData>();
    }

    public string status;
    public List<NormalTournamentData> result;
    public string message;
    public int statusCode;

    [Serializable]
    public class NormalTournamentData
    {
        public string type;
        public string tournamentId;
        public string name;
        public string prize;
        public string buyIn;
        public string status;
        public int players;
        public int maxPlayersPerTable;
        public string dateTime;
        public string pokerGameType = "";
        public int timerDueSeconds;
        public string tournamentStartTime;
        public int timerDisplayWhen;
        public string displayDateTime;
<<<<<<< Updated upstream
        public bool   isJoinable = false;
        public bool   isFreeRoll = false;
=======
        public bool isJoinable = false;
        public bool isFreeRoll = false;
        public string colorOfCapture = "";
>>>>>>> Stashed changes

        public bool Compare(NormalTournamentData data)
        {
            bool answer = true;
            if (type != data.type) answer = false;
            if (tournamentId != data.tournamentId) answer = false;
            if (name != data.name) answer = false;
            if (prize != data.prize) answer = false;
            if (buyIn != data.buyIn) answer = false;
            if (status != data.status) answer = false;
            if (players != data.players) answer = false;
            if (maxPlayersPerTable != data.maxPlayersPerTable) answer = false;
            if (dateTime != data.dateTime) answer = false;
            if (pokerGameType != data.pokerGameType) answer = false;
            if (timerDueSeconds != data.timerDueSeconds) answer = false;
            if (tournamentStartTime != data.tournamentStartTime) answer = false;
            if (timerDisplayWhen != data.timerDisplayWhen) answer = false;
            if (displayDateTime != data.displayDateTime) answer = false;
            if (isJoinable != data.isJoinable) answer = false;
            if (isFreeRoll != data.isFreeRoll) answer = false;
            return answer;
        }
    }
}

[Serializable]
public class TopPlayer
{
    public string player;
    public int position;
    public int winRate;
}

[Serializable]
public class TournamentData
{
    public List<TopPlayer> topPlayer;
    public int rank;
    public int gamePlayed;
    public int won;
    public int lost;
}

[Serializable]
public class PlayerAccountInfo
{
    public string username;
    public int mobile;
    public string accountNumber;
    public string _id;
}

[Serializable]
public class purchaseHistoryData
{
    public string date = "";
    public double chips;
    public string amount;
    public string type = "";
    public string status = "";
    public bool isCredit = false;
}

[Serializable]
public class purchaseHistory
{
    public string status;
    public List<purchaseHistoryData> result;
    public string message;
}

[Serializable]
public class getTournamentInfoData
{
    public string id;
    public int players;
    public string registrationStatus;
    public string name;
    public string gameType;
    public double prizePool;
    public string stacks;
    public string buyIn;
    public int min_players;
    public int max_players;
    public string status;
    public string dateTime = "";
    public bool isRegistered;
    public string pokerGameType = "texas";
    public string reBuy;
    public string addOn;
    public string reBuyEnds;
    public string reBuyLimit;
    public string namespaceString;
    public bool allowRebuy = false;
    public int remainRebuySec = 0;
    public long rebuyAmount;
}

[Serializable]
public class getTournamentPlayers
{
    public string id;
    public int rank;
    public string name;
    public string avatar;
    public double cash;
    public double winning;
    public int tableId;
}

[Serializable]
public class TournamentStartData
{
    public string tournamentId;
    public string roomId = "";
    public string type;
    public string message;
    public int timer;
    public int maxTimer;
}

[Serializable]
public class GetTableDetails
{
    public string id;
    public string name;
    public string namespaceString = "";
    public string pokerGameType = "";
    public string pokerGameFormat = "";
}

[Serializable]
public class GetBlindDetails
{
    public int index;
    public string blinds;
    public int duaration;
}

[Serializable]
public class GetpayoutDetails
{
    public int position;
    public double amount;
}

[Serializable]
public class GetnewsBlogResult
{
    public string title;
    public string shortDesc;
    public string longDesc;
}

[Serializable]
public class playerProfilePic
{
    public string title;
    public string shortDesc;
    public string longDesc;
}

[Serializable]
public class RegularTournament
{
    public string roomId = "";
    public string newRoomId = "";
    public string namespaceString = "";
    public string pokerGameType = "";
    public string pokerGameFormat = "";
    public string tournamentTableWaitMessage = "";
}

[Serializable]
public class BanPlayerTournamentData
{
    public string message;
    public string roomId = "";
    public string tournamentId;
    public string banPlayerId;
    public string banPlayerName;
}

[Serializable]
public class CancelTournamentData
{
    public string message;
    public string roomId = "";
    public string tournamentId;
}

[Serializable]
public class AddOnTimeFinished
{
    public bool tournamentAddon = false;
    public double remaingAddOnTime;
    public string roomId = "";
    public string tournamentId;
    public List<string> eligiblePlayers;
}

[Serializable]
public class GetAddOnDetailsData
{
    public string roomId;
    public string tournamentId;
    public long buyIn;
    public double playerChips;
    public long addonStacks;
}

[Serializable]
public class BlindLevelRaised
{
    public string roomId = "";
    public string tournamentId;
    public string type;
    public string message;
    public double minBuyIn;
    public double maxBuyIn;
    public double smallBlind;
    public double bigBlind;
    public bool isRebuyIn;
    public bool isNotify = false;
}

[Serializable]
public class BreakTimerData
{
    public string roomId = "";
    public float timer;
    public string name;
}

[Serializable]
public class TournamentTableBreakMessage
{
    public string roomId = "";
    public string message = "";
}

[Serializable]
public class Current
{
    public double smallBlind = 0;
    public double bigBlind = 0;
    public int level;
}

[Serializable]
public class Next
{
    public double smallBlind = 0;
    public double bigBlind = 0;
}

[Serializable]
public class OnBlindLevelsData
{
    public string roomId = "";
    public Current current;
    public Next next;
    public double remain = 0;
    public string breakLevel;
    public string players = "";
}

[Serializable]
public class SngTournamentFinishedData
{
    public string tournamentId = "";
    public string roomId = "";
    public List<RegularTournamentFinishedData> winners;
}

[Serializable]
public class RegularTournamentFinishedData
{
    public string id = "";
    public string username = "";
    public int avatar = 0;
    public double winningChips = 0;
}

[Serializable]
public class GetCheckRunningGamedata
{
    public string roomId = "";
    public string tableNumber;
    public bool isTournament;
    public string tournamentType;
    public string type;

    public string namespaceString = "";
    public string pokerGameType = "";
    public string pokerGameFormat = "";
}

[Serializable]
public class Chatdata
{
    public string playerId;
    public string roomId = "";
    public string message;
}

[Serializable]
public class IAmBack
{
    public string roomId = "";
    public string playerId;
    public double dueSeconds;
    public string startTime;
    public string currentTime;
    public bool status;
    public double waitingGameChips = 0;
}

[Serializable]
public class PlayerCardsAll
{
    public string roomId = "";
    public PlayerCards[] playersCards;
    public List<string> tableCards;
    public List<string> advanceTableCards;
}

[Serializable]
public class PlayerCards
{
    public string roomId = "";
    public string playerId = "";
    public List<string> cards;
    public float cardActiveTime = 1.5f;
    public bool flipAnimation = false;
    public bool showFoldedCards = false;
}

[Serializable]
public class DisplayShowCardResult
{
    public string roomId = "";
    public string gameId = "";
    public List<string> playerIdList;
    public float buttonActiveTime = 1.5f;
}

[Serializable]
public class GameBoot
{
    public string smallBlindPlayerId;
    public double smallBlindChips;
    public double smallBlindPlayerChips;
    public string bigBlindPlayerId;
    public double bigBlindChips;
    public double bigBlindPlayerChips;
    public string straddlePlayerId;
    public double straddleChips;
    public double straddlePlayerChips;
    public string dealerPlayerId;
    public string roomId = "";
    public double totalTablePotAmount = 0;

    public List<BlindPlayer> bigBlindPlayerList = new List<BlindPlayer>();

    [Serializable]
    public class BlindPlayer
    {
        public string playerId = "";
        public double chips = 0;
        public double playerChips = 0;
    }
}

[Serializable]
public class OpenBuyInPanelResult
{
    public string roomId = "";
}

[Serializable]
public class ResetGame
{
    public string roomId = "";
}

[Serializable]
public class LocationCordinates
{
    public double latitude = 0;
    public double longitude = 0;
}

//[Serializable]
//public class OwnCards
//{
//	public List<string> cards;
//}

[System.Serializable]
public class IAPGivingResponseData
{
    public string availableToPurchase;
    public string receipt;
    public string transactionID;
    public string isoCurrencyCode;
    public string localizedDescription;
    public string localizedPrice;
    public string localizedPriceString;
    public string localizedTitle;
}

[System.Serializable]
public class IAPGettingResponseData
{
    public double totalChips;
}

[System.Serializable]
public class IAPFile
{
    public List<IAPTransaction> transactionList = new List<IAPTransaction>();

    [System.Serializable]
    public class IAPTransaction
    {
        public string productOd = "";
    }
}

[System.Serializable]
public class MaintenaceResponse
{
    public string message;
}

[System.Serializable]
public class AdminNotificationResponse
{
    public string message = "";
}

[System.Serializable]
public class GamePopupNotificationResponse
{
    public string roomId = ""; // used only in game
    public string message = "";
}

[System.Serializable]
public class GetBuyinsAndPlayerchipsResponse
{
    public double playerChips = 0;
    public double minBuyIn = 0;
    public double maxBuyIn = 0;
    public double OldPlayerchipsBuyin = 0;
}

[Serializable]
public class OnReBuyInResult
{
    public string playerId = "";
    public string tournamentId = "";
    public string roomId = "";
    public int remainRebuySec = 0;
}

[Serializable]
public class GetReBuyInChipsResponse
{
    public string roomId = "";
    public string tournamentId = "";
    public double buyInChips = 0;
    public long buyIn = 0;
    public double playerChips = 0;
}

[Serializable]
public class OnTournamentPrizeResponse
{
    public string roomId = "";
    public int rank = 0;
    public long prize = 0;
}

[Serializable]
public class PlayersCardsItem
{
    public string playerId;
    public List<string> cards;
}

[Serializable]
public class superPlayerCard
{
    public List<PlayersCardsItem> playersCards;
    public List<string> tableCards;
    public string roomId;
}

[Serializable]
public class RunItTwiceData
{
    public float displayTime;
    public string message;
    public string type;
}

[Serializable]
public class RunItTwiceRequestData
{
    public string playerId;
    public string roomId;
    public string action;
    public bool twice;
}

[Serializable]
public class StreddleData
{
    public float displayTime;
    public string message;
    public string playerId;
    public string roomId;
}

[Serializable]
public class OnStraddleTwiceTimerReceivedData
{
    public string roomId;
    public bool isStraddle;
    public bool isRIT;
    public float totalTimer;
    public float currentTimer;
    public string message;
    public string playerId;
}

[Serializable]
public class ReservedListResult
{
    public List<int> reservedSeat;
    public List<Player> waitingPlayers;
    public List<string> roomPlayers;
    public string roomId = "";
    public bool enableJoinWaitingListButton = false;
    public bool isLeaveWaiting = false;

    [Serializable]
    public class Player
    {
        public string playerId = "";
        public int seatIndex = 0;
    }
}

[Serializable]
public class WaitingJoinRoomResult
{
    //public string playerId = "";
    public string roomId = "";

    //public int seatIndex = 0;
    public List<Playerjoin> joiners;
    public int timer = 0;
    public int maxTimer = 0;

    [Serializable]
    public class Playerjoin
    {
        public string playerId = "";
        public int seatIndex = 0;
    }
}

[Serializable]
public class WaitingPlayerResultData
{
    public string id;
    public int position;
    public string username;
    public string avatar;
    public int profilePic;
}

[Serializable]
public class ResultItem
{
    public string dateTime;
    public string gameType;
    public string amount;
    public string gameId;
}

[Serializable]
public class historyPlayer
{
    public string player;
    public int position;
    public double amount;
}

[Serializable]
public class ReportResultStuff
{
    public List<ReportResultData> players;
    public string name;
    public string stake;
}

[Serializable]
public class ReportResultData
{
    public string playerId;
    public string name;
    public string hand;
    public int buyin;
    public float profit;
    public int rank;
}

[Serializable]
public class GamesHistoryListItem
{
    public string gameId;
    public string gameName;
    public string dateTime;
    public List<string> handCards;
}

[Serializable]
public class GamesHistoryResult
{
    public List<GamesHistoryListItem> gamesHistoryList;
    public int recordsTotal;
    public int currentPage;
    public int totalPages;
    public int resultPerPage;
}

[Serializable]
public class clockResult
{
    public string playerId;

    public string status;
    public string roomId;
    public string startTime;
    public string currentTime;
    public double dueSeconds;
}

[Serializable]
public class LateJoinTournament
{
    public string tournamentId;
    public string roomId = "";
    public string playerId = "";
    public string type;
    public string message;
    public int timer;
    public int maxTimer;
}

[Serializable]
public class StandUp
{
    public double oldPlayerBuyIn;
}

[Serializable]
public class TournamentsItemResult
{
    public List<TournamentsItem> tournaments;
}

[Serializable]
public class TournamentsItem
{
    public string type;
    public string name;
    public int buyIn;
    public string status;
    public int players;
    public string dateTime;
}

[Serializable]
public class TournamentLeaderBoardData
{
    public string position;
    public string username;
    public string chips;
    public string rebuys;
    public string addon;
}

[Serializable]
public class rootHack
{
    public string userId = "";
    public long timestamp;
}

[Serializable]
public class TermsResult
{
    public string content;
}

[Serializable]
public class ReBuyAcceptResult
{
    public string roomId;
    public string playerId;
    public int chips;
}

[Serializable]
public class rootHackBase
{
    public string mobile = "";
    public string password = "";
    public long timestamp;
}

[Serializable]
public class TableBalanceNoticeResp
{
    public bool isVisible = false;
    public string message = "";
    public string roomId;
}

[Serializable]
public class AuthTokenFromJSON
{
    public string authToken = "";
<<<<<<< Updated upstream
=======
}

[Serializable]
public class Transaction
{
    public string message;
    public string createdAt;
    public string type;
    public string chips;

    public string cash;
    /*
    "beforeBalance"
    "afterBalance"
    */
}

[Serializable]
public class RoomId
{
    public string roomId;
}

[Serializable]
public class PlayerId
{
    public string playerId;
}

[Serializable]
public class StatusStandard<T>
{
    public string status;
    public List<T> result;
    public int statusCode;
}

[Serializable]
public class StatusMessageStandard<T>
{
    public string status;
    public List<T> result;
    public string message;
    public int statusCode;
>>>>>>> Stashed changes
}