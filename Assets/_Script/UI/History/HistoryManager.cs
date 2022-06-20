using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BestHTTP.SocketIO;


public class HistoryManager : MonoBehaviour
{
//	private static HistoryManager Instance;
//
//	public List<History> preflopRoundHistory;
//	public List<History> flopRoundHistory;
//	public List<History> turnRoundHistory;
//	public List<History> riverRoundHistory;
//
//	public PokerGameRound currentRound;
//
//	public double minBetAmountInCurrentRound;
//
//	void Awake ()
//	{
//		Instance = this;
//
//		preflopRoundHistory = new List<History> ();
//		flopRoundHistory = new List<History> ();
//		turnRoundHistory = new List<History> ();
//		riverRoundHistory = new List<History> ();
//	}
//
//	void Start ()
//	{
////		UIManager.Instance.socketIO.On (Constants.PokerEvents.RoundComplete, OnRoundComplete);
////		UIManager.Instance.socketManager.Socket.On (Constants.PokerEvents.GameStarted, OnGameStarted);
//	}
//
//	void OnEnable ()
//	{
//		//UIManager.Instance.GameScreeen.onResetData += HandleOnResetData;
//	}
//
//	void OnDisable ()
//	{
//		//UIManager.Instance.GameScreeen.onResetData -= HandleOnResetData;
//	}
//
//	public static HistoryManager GetInstance ()
//	{
//		if (Instance == null) {
//			Instance = new GameObject ("HistoryManager").AddComponent<HistoryManager> ();
//		}
//
//		return Instance;
//	}
//
//	/// <summary>
//	/// Adds the history.
//	/// </summary>
//	/// <param name="playerID">Player ID.</param>
//	/// <param name="playerName">Player name.</param>
//	/// <param name="gameRound">Game round.</param>
//	/// <param name="betAmount">Bet amount.</param>
//	/// <param name="playerAction">Player action.</param>
//	public void AddHistory (string playerID, string playerName, PokerGameRound gameRound, double betAmount, double totalBetAmount, PokerPlayerAction playerAction, bool hasRaised)
//	{
//		if (playerAction == PokerPlayerAction.SmallBlind ||
//		    playerAction == PokerPlayerAction.BigBlind) {
//			if (HasAnyHistoryOfAction (playerAction, gameRound))
//				return;
//		}
//
//		History history = new History ();
//		history.playerID = playerID;
//		history.playerName = playerName;
//		history.gameRound = gameRound;
//		history.betAmount = betAmount;
//		history.totalBetAmount = totalBetAmount;
//		history.playerAction = playerAction;
//		history.hasRaised = hasRaised;
//
//		switch (gameRound) {
//		case PokerGameRound.Preflop:
//			preflopRoundHistory.Add (history);
//			break;
//		case PokerGameRound.Flop:
//			flopRoundHistory.Add (history);
//			break;
//		case PokerGameRound.Turn:
//			turnRoundHistory.Add (history);
//			break;
//		case PokerGameRound.River:
//			riverRoundHistory.Add (history);
//			break;
//		}
//
//		minBetAmountInCurrentRound = GetMinBetAmountInCurrentRound (gameRound);
//
////		Debug.Log(playerID + " has " + playerAction + " in " + gameRound + " round.\t\t--> bet amount  : " + betAmount + "\t\t--> total bet amount  : " + totalBetAmount);
//	}
//
//	/// <summary>
//	/// Gets the history.
//	/// </summary>
//	/// <returns>The history.</returns>
//	/// <param name="round">Round.</param>
//	public List<History> GetHistory (PokerGameRound round)
//	{
//		switch (round) {
//		case PokerGameRound.Preflop:
//			return preflopRoundHistory;
//		case PokerGameRound.Flop:
//			return flopRoundHistory;
//		case PokerGameRound.Turn:
//			return turnRoundHistory;
//		case PokerGameRound.River:
//			return riverRoundHistory;
//		}
//
//		return null;
//	}
//
//	/// <summary>
//	/// Gets the max bet amount in current round.
//	/// </summary>
//	/// <returns>The max bet amount in current round.</returns>
//	public double GetMinBetAmountInCurrentRound (PokerGameRound currentRound)
//	{
//		List<History> history = HistoryManager.GetInstance ().GetHistory (currentRound);
//
//		double minBetAmount = 0;
//		for (int i = 0; i < history.Count; i++) {
//			if (history [i].totalBetAmount >= minBetAmount)
//				minBetAmount = history [i].totalBetAmount;
//		}
//
//		return minBetAmount;
//	}
//
//	public void ResetHistory ()
//	{
//		preflopRoundHistory = new List<History> ();
//		flopRoundHistory = new List<History> ();
//		turnRoundHistory = new List<History> ();
//		riverRoundHistory = new List<History> ();
//		currentRound = PokerGameRound.Preflop;
//		minBetAmountInCurrentRound = 0;
//	}
//
//	#region DELEGATE_CALLBACKS
//
//	void OnGameStarted(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
//	{
//		Debug.Log ("OnGameStarted  : " + packet.ToString());
//
//		preflopRoundHistory = new List<History> ();
//		flopRoundHistory = new List<History> ();
//		turnRoundHistory = new List<History> ();
//		riverRoundHistory = new List<History> ();
//	}
//
//	private void HandleOnResetData ()
//	{
//		preflopRoundHistory = new List<History> ();
//		flopRoundHistory = new List<History> ();
//		turnRoundHistory = new List<History> ();
//		riverRoundHistory = new List<History> ();
//	}
//
//	private void OnRoundComplete (Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
//	{
//		Debug.Log ("OnRoundComplete  : " + packet.ToString());
//		JSONArray arr = new JSONArray (packet.ToString ());
//		string Source;
//		Source = arr.getString (arr.length()-1);
//		var resp = Source;
//
//		RoundComplete round = JsonUtility.FromJson<RoundComplete> (resp);
//		currentRound = round.roundStarted.ToEnum<PokerGameRound> ();
//	}
//
//	#endregion
//
//	public bool HasAnyHistoryOfAction (PokerPlayerAction action, PokerGameRound gameRound)
//	{
//		List<History> hist = GetHistory (gameRound);
//
//		foreach (History h in hist)
//		{
//			if (h.playerAction == action)
//				return true;
//		}
//
//		return false;
//	}
}