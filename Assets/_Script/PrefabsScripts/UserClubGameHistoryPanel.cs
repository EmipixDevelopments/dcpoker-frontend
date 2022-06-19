using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserClubGameHistoryPanel : MonoBehaviour
{
    #region PUBLIC_VARIABLES

    //[Header ("Gamobjects")]


    [Header("Transforms")]
    public Transform LeaderBoardParent;

    //[Header ("ScriptableObjects")]


    //	[Header ("DropDowns")]


    //[Header ("Images")]


    //	[Header("InputField")] 


    //[Header ("Text")]

    [Header("Prefabs")]
    public UserHandClubData LeaderBoardDataList;
    //[Header ("Enums")]


    //	[Header ("Variables")]

    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    void OnEnable()
    {
        UIManager.Instance.DisplayLoader("Please wait...");
        UIManager.Instance.SocketGameManager.InGamePlayerGameHistory((socket, packet, args) =>
        {
            Debug.Log("InGamePlayerGameHistory  : " + packet.ToString());


            //			JSONArray arr = new JSONArray(packet.ToString ());
            //
            //			var resp1 = arr.getString(0);
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse<GamesHistoryResult> resp = JsonUtility.FromJson<PokerEventResponse<GamesHistoryResult>>(resp1);


            UIManager.Instance.HideLoader();
            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                DestroyData();

                for (int i = 0; i < resp.result.gamesHistoryList.Count; i++)
                {
                    UserHandClubData LeaderBoardListDataList = Instantiate(LeaderBoardDataList) as UserHandClubData;
                    LeaderBoardListDataList.SetData(resp.result.gamesHistoryList[i]);
                    LeaderBoardListDataList.transform.SetParent(LeaderBoardParent, false);

                }

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
    //// Update is called once per frame
    //void Update ()
    //{

    //}
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void closeButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }
    #endregion

    #region PRIVATE_METHODS


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
