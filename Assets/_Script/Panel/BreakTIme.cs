using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreakTIme : MonoBehaviour {


	#region PUBLIC_VARIABLES

	//[Header ("Gamobjects")]

	//[Header ("Transforms")]


	//[Header ("ScriptableObjects")]


	//[Header ("DropDowns")]


	//[Header ("Images")]


	[Header ("Text")]
	public TextMeshProUGUI CurrentTimerDisplay;


	//[Header ("Prefabs")]

	//[Header ("Enums")]


//	[Header ("Variables")]

	#endregion

	#region PRIVATE_VARIABLES

	#endregion

	#region UNITY_CALLBACKS
	// Use this for initialization
	void OnEnable()
	{
        
    }
	void OnDisable()
	{

	}
	// Update is called once per frame
	void Update ()
	{

	}

    void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus)
            this.Close();
    }
    
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void SetmethodandTime(float CurrentTimer)
	{
		System.TimeSpan t = System.TimeSpan.FromSeconds(CurrentTimer);
		CurrentTimerDisplay.text = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
		this.Open ();
		if (CurrentTimer <= 1) 
		{
			Invoke ("CloseDefault", 1f);
		}
	}


	#endregion

	#region PRIVATE_METHODS
	void CloseDefault()
	{
		this.Close();
	}
	#endregion

	#region COROUTINES



	#endregion


	#region GETTER_SETTER


	#endregion



}
