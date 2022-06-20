using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES 
    //[Header ("Gamobjects")]

    [Header("Transforms")]
    public Transform Parent;

    //[Header ("ScriptableObjects")]
    public clubResultPrefab ClubResultData;

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
        ResulpanelCall();
    }
    void OnDisable()
    {         Destroydata();
    }

    #endregion 
    #region DELEGATE_CALLBACKS 

    #endregion 
    #region PUBLIC_METHODS     public void ResulpanelCall()
    {
        if (this.gameObject.activeInHierarchy)
        {
            Destroydata();


            /* UIManager.Instance.SocketGameManager.clubTablesResult(
            UIManager.Instance.LobbyScreeen.PanelMainCLubScreen.MainClubListScreen.clubReportScreen.ROOMID,
                 UIManager.Instance.LobbyScreeen.PanelMainCLubScreen.MainClubListScreen.clubstuff.id,
                 (socket, packet, args) =>
             {
                print("clubTablesResult response: " + packet.ToString());

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse<ReportResultStuff> ReportResultDataResp = JsonUtility.FromJson<PokerEventResponse<ReportResultStuff>>(resp1);
                 Destroydata();
                 if (ReportResultDataResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                { 
                     for (int i = 0; i < ReportResultDataResp.result.players.Count; i++)
                     {
                         clubResultPrefab kickData = Instantiate(ClubResultData) as clubResultPrefab;
                         kickData.SetData(ReportResultDataResp.result.players[i]);
                         kickData.transform.SetParent(Parent, false);
                     }
                 txtGameName.text = ReportResultDataResp.result.name;
                 txtStacks.text = ReportResultDataResp.result.stake;
                 }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(ReportResultDataResp.message);
                }
            });*/
        }
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
