using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class WinnerPlayer : MonoBehaviour {

	#region PUBLIC_VARIABLES
	[Header ("Gamobjects")]


	[Header ("Transforms")]

	[Header ("Images")]
	public Image Stars;
	public Image Round;
	//public Image rectangleSurface;
	[Header ("Animators")]
	public Animator WinAnimation;

	//[Header ("Text")]


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
		ResetData ();
	}

	#endregion

	#region DELEGATE_CALLBACKS


	#endregion

	#region PUBLIC_METHODS

	public void ResetData()
	{
		WinAnimation.StopPlayback ();
		WinAnimation.enabled = false;
		//rectangleSurface.Close ();
		Round.Close ();
		Stars.Close ();
		this.gameObject.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
	}
	
	public void IsAnimationOn(bool IsOn)
	{
		if (IsOn) {
			WinAnimation.enabled = IsOn;
			//rectangleSurface.Open();
			Round.Close ();
			Stars.Close ();
			WinAnimation.Play ("Image-Winner-Animation");
			Invoke ("CloseAnimation",2f);
		}
		else 
		{
			ResetData ();
		}
	}

	public void CloseAnimation()
	{
		ResetData ();
	}
	#endregion

	#region PRIVATE_METHODS

	#endregion

	#region COROUTINES


	#endregion


	#region GETTER_SETTER


	#endregion
}
