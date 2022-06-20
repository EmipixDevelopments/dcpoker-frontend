using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Loader : MonoBehaviour
{
	#region PUBLIC_VARIABLES

	//public Image imgLoader;
	public TextMeshProUGUI txtMessage;

	#endregion

	#region PRIVATE_VARIABLES

	#endregion

	#region UNITY_CALLBACKS

	void OnEnable ()
	{
		transform.SetAsLastSibling ();
//		txtMessage.text = "";
	}

	void OnDisable ()
	{
		
	}

	#endregion

	#region DELEGATE_CALLBACKS

	#endregion

	#region PUBLIC_METHODS

	#endregion

	#region PRIVATE_METHODS

	#endregion

	#region COROUTINES

	#endregion
}