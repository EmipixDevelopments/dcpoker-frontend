using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentTableElementData
{
    /*
      "type": "LH",
      "tournamentId": "62f513da8903fd4b9419188e",
      "name": "Tournament Chips",
      "buyIn": "10+10",
      "status": "Waiting",
      "isJoinable": false,
      "players": 0,
      "dateTime": "0D 2H 2M 40S",
      "tournamentStartTime": "2022-08-12T14:35:00.000Z",
      "namespaceString": "cash_regular_texas",
      "pokerGameType": "texas",
      "pokerGameFormat": "tournament"
     */

    public string Type = "";
    public string TournamentId = "";
    public string Name = "";
    public string BuyIn = "";
    public string Status = "";
    public bool IsJoinable = false;
    public int Players = 0;
    public string DateTime = "";
    public string TournamentStartTime = "";
    public string NamespaceString = "";
    public string PokerGameType = "";
    public string PokerGameFormat = "";
}
