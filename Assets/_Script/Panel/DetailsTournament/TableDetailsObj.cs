using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TableDetailsObj : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    /*[Header("Images")]
    public Image BarMain;

    [Header("Colors")]
    public Sprite[] Colors;
    [Header("Button")]*/
    public Button join;

    [Header("Text")]
    public TextMeshProUGUI TableName;

    [Header("Variables")]
    public string TournamentTableId = "";

    public RoomsListing.Room Data = null;
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
    public void ButtonHold(Animator a)
    {
        a.SetBool("IsJoin", true);
    }

    public void ButtonRelease(Animator a)
    {
        a.SetBool("IsJoin", false);
    }
    public void JoinTournamentTableButtonap()
    {
        Constants.Poker.TableId = TournamentTableId;
        //UIManager.Instance.GameScreeen.SetRoomDataAndPlay (Data.id, Data.namespaceString, Data.pokerGameType, Data.pokerGameFormat);
        UIManager.Instance.GameScreeen.SetRoomDataAndPlay(Data);
        UIManager.Instance.SoundManager.OnButtonClick();
        //UIManager.Instance.GameScreeen.currentRoomData.isTournament = true;
        UIManager.Instance.TournamentScreenCall();
    }

    public void SetData(RoomsListing.Room Data, int i = 0)// (RoomsListing.Room data, int i)
    {
        TableName.text = Data.name;
        TournamentTableId = Data.roomId;

        this.Data = Data;
        /*if (i % 2 == 0)
        {
            BarMain.sprite = Colors[0];
        }
        else
        {
            BarMain.sprite = Colors[1];
        }*/

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
