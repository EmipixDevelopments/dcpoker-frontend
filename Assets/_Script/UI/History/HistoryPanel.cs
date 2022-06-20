using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BestHTTP.SocketIO;


public class HistoryPanel : MonoBehaviour
{
//    public Text txtPokerHandHistory;
//    public ScrollRect myScrollRect;
//
//    //	public Scrollbar pokerHandHistoryScrollbar;
//
//    // Use this for initialization
//    //	void Start ()
//    //	{
//    //		UIManager.Instance.socketManager.Socket.On (Constants.PokerEvents.PlayerAction, OnPlayerActionReceived);
//    //	}
//
//
//
//    #region DELEGATE_CALLBACKS
//
//    private void OnPlayerActionReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
//    {
//        print("History Panel - OnPlayerActionReceived : " + packet.ToString());
//
//        JSONArray arr = new JSONArray(packet.ToString());
//		var resp = arr.getString(arr.length()-1);
//
//        //		string removedEvent = packet.RemoveEventName (true);
//        PlayerAction action = JsonUtility.FromJson<PlayerAction>(resp);
//        StoreHistory(action);
//    }
//
//    void Start()
//    {
//		ChangeScrollPositionToBottom ();
//    }
//
//    void OnEnable()
//    {
//		ChangeScrollPositionToBottom ();
//    }
//
//    #endregion
//
//    #region PUBLIC_METHODS
//
//	public void StorePokerWinnerHistory(string message, List<string> matchedCards = null)
//    {
//        if (matchedCards == null)
//        {
//			//txtPokerHandHistory.text += "<color>" + message + "</color>\n";
//        }
//        else
//        {
////			txtPokerHandHistory.text += "<color>" + message + " " + GetMatchedCardsString(matchedCards) + ".</color>\n";
//        }
//
////		if (pokerHandHistoryScrollbar != null) {
////			Canvas.ForceUpdateCanvases ();
////			pokerHandHistoryScrollbar.value = 0;
////			Canvas.ForceUpdateCanvases ();
////		}
//    }
//
//    public void TurnBroadcastOn()
//    {
//        UIManager.Instance.socketManager.Socket.On(Constants.PokerEvents.PlayerAction, OnPlayerActionReceived);
//		Game.Lobby.CashSocket.On(Constants.PokerEvents.PlayerAction, OnPlayerActionReceived);
//  
//    }
//
//    public void TurnBroadcastOff()
//    {
//        //UIManager.Instance.socketManager.Socket.Off(Constants.PokerEvents.PlayerAction, OnPlayerActionReceived);
//		Game.Lobby.CashSocket.Off(Constants.PokerEvents.PlayerAction, OnPlayerActionReceived);
//        
//    }
//
//    public void OnPokerResetGame()
//    {
//        //txtPokerHandHistory.text += "****************************\n";
//    }
//
//    public void OnGameDisable()
//    {
//        TurnBroadcastOff();
////        //txtPokerHandHistory.text = "";
//    }
//
//    public void AddPlayerJoinLog(string playerName)
//    {
//		//txtPokerHandHistory.text += playerName + "<color> joined the table </color>" + "\n";
//    }
//
//    public void AddPlayerLeftLog(string playerName)
//    {
//		//txtPokerHandHistory.text += playerName + "<color> left the table </color>" + "\n";
//    }
//
//	public void ChangeScrollPositionToBottom()
//	{
//		//myScrollRect.verticalNormalizedPosition = 0f;
//	}
//    #endregion
//
//    #region PRIVATE_METHODS
//
//    private void StoreHistory(PlayerAction action)
//    {
//        if (action.action != null)
//        {
//            PokerPlayer plr = UIManager.Instance.GameScreeen.GetPlayerById(action.action.playerId); 
//
//            if (plr != null)
//            {
//                if (action.action.action == PokerPlayerAction.Fold)
//                {
//					//txtPokerHandHistory.text += plr.playerInfo.username + "<color> folded. </color>\n";
//                }
//                else if (action.action.action == PokerPlayerAction.Allin)
//                {
//					//txtPokerHandHistory.text += plr.playerInfo.username + "<color> all-in with " + action.action.betAmount.ToString() + " chips. </color>\n";
//                }
//                else if (action.action.action == PokerPlayerAction.Call ||
//                action.action.action == PokerPlayerAction.Bet)
//                {
//					
//                 /*   if (action.action.betAmount == 0)
//						//txtPokerHandHistory.text += plr.playerInfo.username + "<color> checked. </color>\n";
//                    else
//                    {
//                        if (action.action.hasRaised)
//							//txtPokerHandHistory.text += plr.playerInfo.username + "<color> raised " + action.action.betAmount.ToString() + ". </color>\n";
//                        else
//							//txtPokerHandHistory.text += plr.playerInfo.username + "<color> called " + action.action.betAmount.ToString() + ". </color>\n";
//                    }*/
//                }
//            }
//
//			 
//        }
//
////		if (pokerHandHistoryScrollbar != null) {
////			Canvas.ForceUpdateCanvases ();
////			pokerHandHistoryScrollbar.value = 0;
////			Canvas.ForceUpdateCanvases ();
////		}
//    }
//
//    private string GetMatchedCardsString(List<string> matchedCards)
//    {
////        string spades = "♠";
////        string diamonds = "♦";
////        string clubs = "♣";
////        string hearts = "♥";
////
////        string newString = "(";
////
////        for (int i = 0; i < matchedCards.Count; i++)
////        {
////            string rank = matchedCards[i].Substring(0, 1);
////            string suit = matchedCards[i].Substring(1, 1);
////
////            rank = rank.Equals("T") ? "10" : rank;
////
////            if (suit.Equals(PokerCardSuit.C.ToString()))
////            {
////                newString += clubs + rank + " ";
////            }
////            else if (suit.Equals(PokerCardSuit.D.ToString()))
////            {
////                newString += diamonds + rank + " ";
////            }
////            else if (suit.Equals(PokerCardSuit.H.ToString()))
////            {
////                newString += hearts + rank + " ";
////            }
////            else if (suit.Equals(PokerCardSuit.S.ToString()))
////            {
////                newString += spades + rank + " ";
////            }
////        }
////
////        newString += ")";
////
////        return newString;
//
//		string spades = "^";
//		string diamonds = "~";
//		string clubs = "#";
//		string hearts = "|";
//
//		string newString = "(";
//
//		for (int i = 0; i < matchedCards.Count; i++) {
//			string rank = matchedCards [i].Substring (0, 1);
//			string suit = matchedCards [i].Substring (1, 1);
//
//			rank = rank.Equals ("T") ? "10" : rank;
//
//			if (suit.Equals (PokerCardSuit.C.ToString ())) {
//				newString += clubs + rank + " ";
//			} else if (suit.Equals (PokerCardSuit.D.ToString ())) {
//				newString += diamonds + rank + " ";
//			} else if (suit.Equals (PokerCardSuit.H.ToString ())) {
//				newString += hearts + rank + " ";
//			} else if (suit.Equals (PokerCardSuit.S.ToString ())) {
//				newString += spades + rank + " ";
//			}
//		}
//
//		newString += ")";
//
//		return newString;
//    }
//
//    private string GetCurrentTime()
//    {
//        return string.Format("<color=yellow>[{0:hh:mm:ss tt}]</color> ", System.DateTime.Now);
//    }
//
//    #endregion
}
