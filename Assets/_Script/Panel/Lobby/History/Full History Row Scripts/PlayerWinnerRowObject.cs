using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWinnerRowObject : MonoBehaviour {

	#region PUBLIC_VARIABLES
	public Text txtPlayerName;
	public Text txtWinningAmount;
	public Transform transPlayerCardPanel;
	#endregion

	#region PRIVATE_VARIABLES
	private FullGameHistoryResult.GameHistory.Winner winner;
	#endregion

	#region UNITY_CALLBACK
	#endregion

	#region DELEGATE_CALLBACKS
	#endregion

	#region PUBLIC_METHODS
	public void SetData(FullGameHistoryResult.GameHistory.Winner winner)
	{
		this.Open ();
		this.winner = winner;
		SetPlayerName (winner.playerName);
		print ("winner.winningAmount: " + winner.winningAmount);
		SetWinningAmount (winner.winningAmount);
		SetPlayerCards (winner.winningHands);
	}
	#endregion

	#region PRIVATE_METHODS

	private void SetPlayerName(string name)
	{
		txtPlayerName.text = name;
	}

	private void SetWinningAmount(double amount)
	{
		txtWinningAmount.text = amount.ToString();
	}

	private void SetPlayerCards(List<string> cards)
	{
		foreach (string cardString in cards) {
			GameObject NewObj = new GameObject();
			NewObj.transform.parent = transPlayerCardPanel;
			NewObj.transform.localScale = Vector3.one;
			Image NewImage = NewObj.AddComponent<Image>();
			NewImage.sprite = Utility.Instance.GetCard(cardString);
		}
	}
	#endregion

	#region COROUTINES
	#endregion
}
