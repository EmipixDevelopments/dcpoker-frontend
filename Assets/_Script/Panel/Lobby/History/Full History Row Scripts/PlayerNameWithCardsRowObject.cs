using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameWithCardsRowObject : MonoBehaviour {

	#region PUBLIC_VARIABLES
	public Text txtPlayerName;
	public Transform transPlayerCardPanel;
	#endregion

	#region PRIVATE_VARIABLES
//	private FullGameHistoryResult.GameHistory.Player player;
	#endregion

	#region UNITY_CALLBACK
	#endregion

	#region DELEGATE_CALLBACKS
	#endregion

	#region PUBLIC_METHODS
	public void SetData(FullGameHistoryResult.GameHistory.Player player)
	{
		this.Open ();
//		this.player = player;
		SetPlayerName (player.playerName);
		SetPlayerCards (player.cards);
	}
	#endregion

	#region PRIVATE_METHODS

	private void SetPlayerName(string name)
	{
		txtPlayerName.text = name;
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
