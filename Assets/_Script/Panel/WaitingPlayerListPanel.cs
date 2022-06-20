using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingPlayerListPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES 
    //[Header ("Gamobjects")]

    [Header("Transforms")]
    public Transform Parent;

    [Header("ScriptableObjects")]
    public waitingPlayerData waitingPlayerDataPrefab;

    //[Header ("DropDowns")]


    //[Header ("Images")]


    //[Header ("Text")]


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
    #region PUBLIC_METHODS     public void CloseScreen()
    {         this.Close();     }     public void ResulpanelCall()
    {
        if (this.gameObject.activeInHierarchy)
        {
            Destroydata();


            UIManager.Instance.SocketGameManager.WaitingListPlayers((socket, packet, args) =>
                {
                    print("WaitingListPlayers response: " + packet.ToString());

                    JSONArray arr = new JSONArray(packet.ToString());
                    string Source;
                    Source = arr.getString(arr.length() - 1);
                    var resp1 = Source;

                    PokerEventListResponse<WaitingPlayerResultData> ReportResultDataResp = JsonUtility.FromJson<PokerEventListResponse<WaitingPlayerResultData>>(resp1);
                    Destroydata();
                    if (ReportResultDataResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                    {
                        for (int i = 0; i < ReportResultDataResp.result.Count; i++)
                        {
                            waitingPlayerData waitingData = Instantiate(waitingPlayerDataPrefab) as waitingPlayerData;
                            waitingData.SetData(ReportResultDataResp.result[i]);
                            waitingData.transform.SetParent(Parent, false);
                        }
                        
                    }
                    else
                    {
                        UIManager.Instance.DisplayMessagePanel(ReportResultDataResp.message);
                    }
                });
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
