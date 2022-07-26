using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RegistredTableData : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    //[Header ("Gamobjects")]

    //[Header ("Transforms")]


    //[Header ("ScriptableObjects")]


    //[Header ("DropDowns")]


    //[Header ("Images")]


    [Header("Text")]
    public Text TournamentName;


    //[Header ("Prefabs")]

    //[Header ("Enums")]


    [Header("Variables")]
    public string Roomid;
    public string Type;
    public RoomsListing.Room data;
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

    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void SetData(RoomsListing.Room Data)
    {
        this.data = Data;
        TournamentName.text = data.tableNumber;
        Roomid = data.roomId;
        Type = data.type;
        this.Open();
    }

    public void JoinButtonTap()
    {
        if (data.isTournament)
        {
            //UIManager.Instance.GameScreeen.SetRoomDataAndPlay (data.roomId, data.namespaceString, data.pokerGameType, data.pokerGameFormat);
            UIManager.Instance.GameScreeen.SetRoomDataAndPlay(data);
            /*if (data.tournamentType.Equals ("regular")) {
				UIManager.Instance.selectedGameType = GameType.Touranment;
			} else {
				UIManager.Instance.selectedGameType = GameType.sng;
			}*/
        }
        else
        {
            UIManager.Instance.selectedGameType = GameType.cash;
        }
        StartCoroutine(NextScreen(2f));
    }

    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES
    IEnumerator NextScreen(float timer)
    {
        UIManager.Instance.DisplayLoader("");
        yield return new WaitForSeconds(timer);
        UIManager.Instance.DisplayLoader("");

        UIManager.Instance.LobbyPanelNew.Close(); // LobbyScreeen not used more
        UIManager.Instance.GameScreeen.Open();
    }


    #endregion


    #region GETTER_SETTER


    #endregion




}
