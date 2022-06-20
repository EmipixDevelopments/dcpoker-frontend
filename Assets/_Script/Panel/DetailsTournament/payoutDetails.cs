using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class payoutDetails : MonoBehaviour
{
    #region PUBLIC_VARIABLES

    //[Header ("Gamobjects")]

    [Header("Transforms")]
    public Transform PayOutDetailsDataParent;

    //[Header ("ScriptableObjects")]


    //[Header ("DropDowns")]


    //[Header ("Images")]


    //[Header ("Text")]


    [Header("Prefabs")]
    public PayOutDetailsObj ObjTableDetailsTorunament;
    public List<PayOutDetailsObj> PayOutDetailsObjList;
    //[Header ("Enums")]


    [Header("Variables")]
    public string TournamentDetailsId = "";

    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    void OnEnable()
    {
        TournamentDetailsId = Constants.Poker.TournamentId;

        Reset();
        //		DemoDisplayData ();
        StopCoroutine("RefreshTableOnInterval");
        StartCoroutine("RefreshTableOnInterval");
        RefreshTable(true);

    }
    void OnDisable()
    {
        StopCoroutine("RefreshTableOnInterval");
        Reset();
        TournamentDetailsId = "";
        PayOutDetailsObjList.Clear();
    }
    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void RefreshTable(bool displayLoader = false)
    {
        if (displayLoader)
        {
            DestroyAllTournamentTables();
            //loadingPanel.Open ();
        }
        CallDataApi();
    }


    #endregion

    #region PRIVATE_METHODS

    void CallDataApi()
    {
        if (UIManager.Instance.gameType == GameType.Touranment)
        {
            UIManager.Instance.SocketGameManager.getTournamentPayout(TournamentDetailsId, UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.pokerGameType, (socket, packet, args) =>
            {

                Debug.Log("getTournamentPayout  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventListResponse<GetpayoutDetails> resp = JsonUtility.FromJson<PokerEventListResponse<GetpayoutDetails>>(resp1);


                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    for (int i = 0; i < resp.result.Count; i++)
                    {
                        PayOutDetailsObj obj = GetTableObjIfAlreadyCreated(resp.result[i].position);
                        if (obj != null)
                        {
                            obj.SetData(resp.result[i], i);
                        }
                        else
                        {
                            PayOutDetailsObj PayOutDetails = Instantiate(ObjTableDetailsTorunament) as PayOutDetailsObj;
                            PayOutDetails.SetData(resp.result[i], i);
                            PayOutDetails.transform.SetParent(PayOutDetailsDataParent, false);
                            PayOutDetailsObjList.Add(PayOutDetails);
                        }
                    }
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }

            });
        }
        if (UIManager.Instance.gameType == GameType.sng)
        {
            UIManager.Instance.SocketGameManager.getSngTournamentPayout(TournamentDetailsId, UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.pokerGameType, (socket, packet, args) =>
            {

                Debug.Log("getSngTournamentPayout  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventListResponse<GetpayoutDetails> resp = JsonUtility.FromJson<PokerEventListResponse<GetpayoutDetails>>(resp1);

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    for (int i = 0; i < resp.result.Count; i++)
                    {
                        PayOutDetailsObj obj = GetTableObjIfAlreadyCreated(resp.result[i].position);
                        if (obj != null)
                        {
                            obj.SetData(resp.result[i], i);
                        }
                        else
                        {
                            PayOutDetailsObj PayOutDetails = Instantiate(ObjTableDetailsTorunament) as PayOutDetailsObj;
                            PayOutDetails.SetData(resp.result[i], i);
                            PayOutDetails.transform.SetParent(PayOutDetailsDataParent, false);
                            PayOutDetailsObjList.Add(PayOutDetails);
                        }
                    }
                    RemoveOtherPlayers(resp.result);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }

            });
        }
    }
    private void RemoveOtherPlayers(List<GetpayoutDetails> roomsList)
    {
        if (PayOutDetailsObjList != null)
        {
            if (roomsList == null || roomsList.Count == 0)
            {
                foreach (PayOutDetailsObj tro in PayOutDetailsObjList.ToArray())
                {
                    PayOutDetailsObjList.Remove(tro);
                    Destroy(tro.gameObject);
                }
            }
            else
            {
                List<int> roomIdsList = roomsList.Select(o => o.position).ToList();

                foreach (PayOutDetailsObj tro in PayOutDetailsObjList.ToArray())
                {

                    for (int i = 0; i < roomsList.Count; i++)
                    {
                        if (roomsList == null || !roomIdsList.Contains(tro.Datavalue.position))
                        {
                            PayOutDetailsObjList.Remove(tro);
                            Destroy(tro.gameObject);
                        }
                    }
                }
            }
        }
    }

    private PayOutDetailsObj GetTableObjIfAlreadyCreated(int tableID)
    {
        if (PayOutDetailsObjList != null)
        {
            for (int i = 0; i < PayOutDetailsObjList.Count; i++)
            {
                int Ranker = PayOutDetailsObjList[i].Datavalue.position;
                if (tableID.Equals(Ranker))
                {
                    return PayOutDetailsObjList[i];
                }
            }
        }
        return null;
    }

    private void DestroyAllTournamentTables()
    {
        if (PayOutDetailsObjList == null)
            return;

        if (PayOutDetailsObjList != null)
        {
            foreach (PayOutDetailsObj go in PayOutDetailsObjList)
            {
                Destroy(go.gameObject);
            }
            PayOutDetailsObjList = new List<PayOutDetailsObj>();
        }

    }



    void Reset()
    {
        foreach (Transform child in PayOutDetailsDataParent)
        {
            Destroy(child.gameObject);
        }
    }

    /*	
     * void DemoDisplayData()
        {
            for (int i = 0; i < 2; i++) 
            {
                PayOutDetailsObj PayOutDetails = Instantiate (ObjTableDetailsTorunament) as PayOutDetailsObj;
                PayOutDetails.SetData(i+1,750);
                PayOutDetails.transform.SetParent (PayOutDetailsDataParent, false);
            }
        }*/
    #endregion

    #region COROUTINES
    private IEnumerator RefreshTableOnInterval()
    {
        while (true)
        {
            if (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(Constants.Poker.RefreshTableInterval);
                RefreshTable();
            }
        }
    }

    #endregion


    #region GETTER_SETTER


    #endregion



}
