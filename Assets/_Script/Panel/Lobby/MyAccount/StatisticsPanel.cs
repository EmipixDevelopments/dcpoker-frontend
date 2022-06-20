using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatisticsPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    [Header("Gamobjects")]
    public GameObject[] SelectedGame;
    public GameObject[] SelectedGameTableList;

    [Header("Transforms")]
    public Transform LeaderBoardParent;

    //[Header ("ScriptableObjects")]


    //	[Header ("DropDowns")]


    //[Header ("Images")]


    //	[Header("InputField")] 


    [Header("Text")]
    public TextMeshProUGUI NoOfPlayedGames;
    public TextMeshProUGUI LoseGames;
    public TextMeshProUGUI WinGames;
    public TextMeshProUGUI MyPosition;

    [Header("Prefabs")]
    public LeaderBoardListData LeaderBoardDataList;
    //[Header ("Enums")]


    //	[Header ("Variables")]

    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    void OnEnable()
    {

        SelectedOptionButtonTap(0);
        UIManager.Instance.DisplayLoader("Please wait...");
        UIManager.Instance.SocketGameManager.GetLeaderboard((socket, packet, args) =>
        {

            Debug.Log("GetLeaderboard  : " + packet.ToString());

            UIManager.Instance.HideLoader();

            //			JSONArray arr = new JSONArray(packet.ToString ());
            //
            //			var resp1 = arr.getString(0);
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse<TournamentData> resp = JsonUtility.FromJson<PokerEventResponse<TournamentData>>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                DestroyData();

                for (int i = 0; i < resp.result.topPlayer.Count; i++)
                {
                    LeaderBoardListData LeaderBoardListDataList = Instantiate(LeaderBoardDataList) as LeaderBoardListData;
                    LeaderBoardListDataList.SetData(resp.result.topPlayer[i], i);
                    LeaderBoardListDataList.transform.SetParent(LeaderBoardParent, false);

                }
                NoOfPlayedGames.text = resp.result.gamePlayed.ToString();
                LoseGames.text = resp.result.lost.ToString();
                WinGames.text = resp.result.won.ToString();
                MyPosition.text = resp.result.rank.ToString();
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }

        });

        //StaticDataDisplay ();
    }
    void OnDisable()
    {
        DestroyData();
    }
    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS

    public void SelectedOptionButtonTap(int SelectedOption)
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        ResetSelectedgameButtons(SelectedOption);
    }
    #endregion

    #region PRIVATE_METHODS
    void ResetSelectedgameButtons(int GameSelect)
    {
        foreach (GameObject Obj in SelectedGame)
        {
            Obj.SetActive(false);
        }
        foreach (GameObject Obj in SelectedGameTableList)
        {
            Obj.SetActive(false);
        }

        SelectedGame[GameSelect].SetActive(true);
        SelectedGameTableList[GameSelect].SetActive(true);

    }

    /*void StaticDataDisplay()
	{
		MyPosition.text = "5";
		NoOfPlayedGames.text = "50";
		LoseGames.text = "10";
		WinGames.text = "40";

		for (int i = 0; i < 10; i++) {

			LeaderBoardListData LeaderBoardListDataList = Instantiate (LeaderBoardDataList) as LeaderBoardListData;
			LeaderBoardListDataList.SetData (i);
			LeaderBoardListDataList.transform.SetParent (LeaderBoardParent, false);
		}
	}*/
    void DestroyData()
    {
        foreach (Transform child in LeaderBoardParent)
        {
            Destroy(child.gameObject);
        }
    }
    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion
}
