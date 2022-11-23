using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionPlayer : MonoBehaviour {

	#region PUBLIC_VARIABLES
	[Header ("Gamobjects")]


	[Header ("Transforms")]

	[Header ("Images")]

	[Header ("Text")]
	public TextMeshProUGUI txtAction;

	[SerializeField] private GameObject _playerNameTextToHide;

	[Header ("String")]
	public string LastAction;
	//[Header ("Prefabs")]
	//public TableData tables;

	#endregion

	#region PRIVATE_VARIABLES

	#endregion

	#region UNITY_CALLBACKS
	// Use this for initialization
	// Update is called once per frame
	void OnEnable()
	{
		if(_playerNameTextToHide)
			_playerNameTextToHide.SetActive(false);
		StartCoroutine (closeObjects (Constants.Poker.PlayeractionDisplayTime));
	}

	#endregion

	#region DELEGATE_CALLBACKS


	#endregion

	#region PUBLIC_METHODS


	#endregion

	#region PRIVATE_METHODS
	public void ResetData()
	{
		txtAction.text = "";
		LastAction = "";
	}

	#endregion

	#region COROUTINES
	IEnumerator closeObjects(float timer)
	{
		yield return new WaitForSeconds (timer);
		
		if(_playerNameTextToHide)
			_playerNameTextToHide.SetActive(true);
		
		this.Close ();
	}

	#endregion


	#region GETTER_SETTER


	#endregion

}
