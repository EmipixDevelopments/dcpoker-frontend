using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class History
{
	public string playerID;
	public string playerName;

	public PokerGameRound gameRound;

	public Double betAmount;
	public Double totalBetAmount;
	public PokerPlayerAction playerAction;

	public bool hasRaised;
}