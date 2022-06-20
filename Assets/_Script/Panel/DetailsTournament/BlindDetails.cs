using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlindDetails : MonoBehaviour
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
    public BlindDetailsObj ObjBlindDetailsTorunament;
    public List<BlindDetailsObj> BlindDetailsObjList;
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
        Reset();
        TournamentDetailsId = Constants.Poker.TournamentId;

        //		DemoDisplayData ();
        StopCoroutine("RefreshTableOnInterval");
        StartCoroutine("RefreshTableOnInterval");
        RefreshTable(true);
    }
    void OnDisable()
    {
        Reset();
        TournamentDetailsId = "";
        BlindDetailsObjList.Clear();
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
    void Reset()
    {
        foreach (Transform child in PayOutDetailsDataParent)
        {
            Destroy(child.gameObject);
        }
    }

    void CallDataApi()
    {
        if (UIManager.Instance.gameType == GameType.Touranment)
        {
            UIManager.Instance.SocketGameManager.getTournamentBlinds(TournamentDetailsId, UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.pokerGameType, (socket, packet, args) =>
            {

                Debug.Log("getTournamentBlinds  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventListResponse<GetBlindDetails> resp = JsonUtility.FromJson<PokerEventListResponse<GetBlindDetails>>(resp1);


                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    for (int i = 0; i < resp.result.Count; i++)
                    {
                        BlindDetailsObj obj = GetTableObjIfAlreadyCreated(resp.result[i].index);
                        if (obj != null)
                        {
                            obj.SetData(resp.result[i], i);
                        }
                        else
                        {
                            BlindDetailsObj BlindDetails = Instantiate(ObjBlindDetailsTorunament) as BlindDetailsObj;
                            BlindDetails.SetData(resp.result[i], i);
                            BlindDetails.transform.SetParent(PayOutDetailsDataParent, false);
                            BlindDetailsObjList.Add(BlindDetails);
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
            UIManager.Instance.SocketGameManager.getSngTournamentBlinds(TournamentDetailsId, UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.pokerGameType, (socket, packet, args) =>
            {

                Debug.Log("getSngTournamentBlinds  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventListResponse<GetBlindDetails> resp = JsonUtility.FromJson<PokerEventListResponse<GetBlindDetails>>(resp1);


                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    for (int i = 0; i < resp.result.Count; i++)
                    {
                        BlindDetailsObj obj = GetTableObjIfAlreadyCreated(resp.result[i].index);
                        if (obj != null)
                        {
                            obj.SetData(resp.result[i], i);
                        }
                        else
                        {
                            BlindDetailsObj BlindDetails = Instantiate(ObjBlindDetailsTorunament) as BlindDetailsObj;
                            BlindDetails.SetData(resp.result[i], i);
                            BlindDetails.transform.SetParent(PayOutDetailsDataParent, false);
                            BlindDetailsObjList.Add(BlindDetails);
                        }
                    }
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }

            });
        }

    }

    private void RemoveOtherPlayers(List<GetBlindDetails> roomsList)
    {
        if (BlindDetailsObjList != null)
        {
            if (roomsList == null || roomsList.Count == 0)
            {
                foreach (BlindDetailsObj tro in BlindDetailsObjList.ToArray())
                {
                    BlindDetailsObjList.Remove(tro);
                    Destroy(tro.gameObject);
                }
            }
            else
            {
                List<string> roomIdsList = roomsList.Select(o => o.blinds).ToList();

                foreach (BlindDetailsObj tro in BlindDetailsObjList.ToArray())
                {

                    for (int i = 0; i < roomsList.Count; i++)
                    {
                        if (roomsList == null || !roomIdsList.Contains(tro.Blinds.ToString()))
                        {
                            BlindDetailsObjList.Remove(tro);
                            Destroy(tro.gameObject);
                        }
                    }
                }
            }
        }
    }

    private BlindDetailsObj GetTableObjIfAlreadyCreated(int tableID)
    {
        if (BlindDetailsObjList != null)
        {
            for (int i = 0; i < BlindDetailsObjList.Count; i++)
            {
                if (tableID.Equals(BlindDetailsObjList[i].data.index))
                {
                    return BlindDetailsObjList[i];
                }
            }
        }
        return null;
    }

    private void DestroyAllTournamentTables()
    {
        if (BlindDetailsObjList == null)
            return;

        if (BlindDetailsObjList != null)
        {
            foreach (BlindDetailsObj go in BlindDetailsObjList)
            {
                Destroy(go.gameObject);
            }
            BlindDetailsObjList = new List<BlindDetailsObj>();
        }

    }


    /*
	 * void DemoDisplayData()
	{
		for (int i = 0; i < 10; i++) 
		{
			BlindDetailsObj BlindDetails = Instantiate (ObjBlindDetailsTorunament) as BlindDetailsObj;
			BlindDetails.SetData(i+1,750,0.40f);
			BlindDetails.transform.SetParent (PayOutDetailsDataParent, false);
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
