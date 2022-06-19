using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TournaryListPrefab : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    [Header("Gamobjects")]

    /*[Header ("Transforms")]
	 

	[Header ("ScriptableObjects")]
	 

	[Header ("DropDowns")]*/


    [Header("Images")]
    public Image BarMain;

    //	[Header("InputField")] 

    //[Header("Button")]


    [Header("Text")]
    public TextMeshProUGUI Type;
    public Text GameName;
    public TextMeshProUGUI DateTime;
    public TextMeshProUGUI buyIn;
    public TextMeshProUGUI Players;
    public TextMeshProUGUI Status;

    /*[Header ("Prefabs")]
	 
	[Header ("Enums")]
	 */

    [Header("ScriptableObjects")]
    public TournamentsItem data;
    public Sprite[] Colors;
    //[Header("Variables")]

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

    public void SetTournamentData(int i, TournamentsItem data)
    {
        this.data = data;

        if (string.IsNullOrEmpty(data.type) == false)
        {
            Type.text = data.type;
        }
        else
        {
            Type.text = "---";
        }
        if (string.IsNullOrEmpty(data.name) == false)
        {
            //GameName.text = data.name;
            Utility.Instance.CheckHebrewOwn(GameName, data.name);
        }
        else
        {
            GameName.text = "---";
        }
        if (string.IsNullOrEmpty(data.dateTime) == false)
        {
            DateTime.text = data.dateTime.ToString();
        }
        else
        {
            DateTime.text = "---";
        }
        if (data.buyIn >= 0)
        {
            buyIn.text = data.buyIn.ToString();
        }
        else
        {
            buyIn.text = "---";
        }
        if (data.players >= 0)
        {
            Players.text = data.players.ToString();
        }
        else
        {
            Players.text = "---";
        }

        if (string.IsNullOrEmpty(data.status) == false)
        {

            Status.text = data.status.ToUpper();
        }
        else
        {
            Status.text = "---";
        }
        if (i % 2 == 0)
        {
            BarMain.sprite = Colors[0];
        }
        else
        {
            BarMain.sprite = Colors[1];
        }
        this.Open();

    }


    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion
}
