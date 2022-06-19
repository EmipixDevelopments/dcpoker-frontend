using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class newsList : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    [Header("Images")]
    public Image BarMain;

    [Header("Colors")]
    public Sprite[] Colors;
    /*[Header ("Transforms")]
	 

	[Header ("ScriptableObjects")]
	 

	[Header ("DropDowns")]*/


    //[Header ("Images")]


    //	[Header("InputField")] 


    [Header("Text")]
    public Text Date;
    public Text Title;
    public Text Description;


    //Header ("Prefabs")]

    //[Header ("Enums")]


    [Header("Variables")]
    public string LongDesc = "";

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
    public void SetData(string Titletext, string ShortDesc, string LongDescription, int i)// (RoomsListing.Room data, int i)
    {
        if (i % 2 == 0)
        {
            BarMain.sprite = Colors[0];
        }
        else
        {
            BarMain.sprite = Colors[1];
        }
        Date.text = System.DateTime.Today.ToString("dd/MM/yyyy");
        Title.text = Titletext;
        Description.text = ShortDesc;
        LongDesc = LongDescription;
        //Debug.Log("count : " + Description.text.Length);
        ReadMoreString(ShortDesc);
    }

    public void NewsListDetailPanelOpen()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.LobbyScreeen.ProfileScreen.PanelNews.NewsDetailsPanel.SetData(Title.text, Description.text, LongDesc);
    }
    #endregion

    #region PRIVATE_METHODS
    void ReadMoreString(string ShortDesc)
    {
        if (Description.text.Length > 90)
        {

            string myString = ShortDesc;
            myString = myString.Substring(0, 90);
            Description.text = myString + "...Read More";

            //Debug.Log("read more== " + myString.Length);

        }
        else
        {
            Description.text = ShortDesc;
            Description.text += "...Read More";

        }
    }
    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion
}
