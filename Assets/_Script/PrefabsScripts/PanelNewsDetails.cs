using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelNewsDetails : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    [Header("Gamobjects")]

    /*[Header ("Transforms")]
	 

	[Header ("ScriptableObjects")]
	 

	[Header ("DropDowns")]*/


    //[Header ("Images")]


    //	[Header("InputField")] 


    [Header("Text")]
    public Text Title;
    public TextMeshProUGUI MainDescription;

    //Header ("Prefabs")]

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
    void Update()
    {

    }
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void SetData(string Titletext, string ShortDesc, string LongDescription)// (RoomsListing.Room data, int i)
    {
        Title.text = Titletext;
        MainDescription.text = LongDescription;
        this.Open();
    }

    public void NewsListDetailPanelClose()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }
    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion
}
