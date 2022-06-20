using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tournamentLeaderBoardPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES 
    [Header("Gamobjects")]

    public GameObject titleRebuys;
    public GameObject titleAddOn;

    [Header("Transforms")]
    public Transform Parent;

    //[Header ("ScriptableObjects")]
    public tournamentleaderboardPrefab tournamentleaderboardPrefabData;

    //[Header ("DropDowns")]


    //[Header ("Images")]


    //[Header("Text")]

    //[Header ("Prefabs")]

    //[Header ("Enums")]


    //[Header ("Variables")]

    #endregion 
    #region PRIVATE_VARIABLES 
    #endregion 
    #region UNITY_CALLBACKS     // Use this for initialization
    void OnEnable()
    {
    }
    void OnDisable()
    {         Destroydata();
    }

    #endregion 
    #region DELEGATE_CALLBACKS 

    #endregion 
    #region PUBLIC_METHODS     public void SetDataAndOpen()
    {         UIManager.Instance.SocketGameManager.TournamentLeaderboard(UIManager.Instance.GameScreeen.currentRoomData.tournamentId, (socket, packet, args) =>
        {
            print("TournamentLeaderboard response: " + packet.ToString());

            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventListResponse<TournamentLeaderBoardData> ReportResultDataResp = JsonUtility.FromJson<PokerEventListResponse<TournamentLeaderBoardData>>(resp1);
            Destroydata();
            if (ReportResultDataResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                if (ReportResultDataResp.result.Count > 0 && ReportResultDataResp.result[0].rebuys == null)
                {
                    titleRebuys.SetActive(false);
                }
                else
                {
                    titleRebuys.SetActive(true);
                }
                if (ReportResultDataResp.result.Count > 0 && ReportResultDataResp.result[0].addon == null)
                {
                    titleAddOn.SetActive(false);
                }
                else
                {
                    titleAddOn.SetActive(true);
                }

                for (int i = 0; i < ReportResultDataResp.result.Count; i++)
                {
                    tournamentleaderboardPrefab kickData = Instantiate(tournamentleaderboardPrefabData) as tournamentleaderboardPrefab;
                    kickData.SetData(ReportResultDataResp.result[i]);
                    kickData.transform.SetParent(Parent, false);
                }
                //txtGameName.text = ReportResultDataResp.result.name;
                //txtStacks.text = ReportResultDataResp.result.stake;
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(ReportResultDataResp.message);
            }
        });         this.Open();     }
    public void closeButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }



    #endregion 
    #region PRIVATE_METHODS      void Destroydata()
    {
        foreach (Transform tr in Parent)
        {
            Destroy(tr.gameObject);
        }
    }
    #endregion 
    #region COROUTINES 


    #endregion 

    #region GETTER_SETTER 

    #endregion 


}