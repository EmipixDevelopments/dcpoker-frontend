using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TableDetails : MonoBehaviour
{


    #region PUBLIC_VARIABLES

    //[Header ("Gamobjects")]

    [Header("Transforms")]
    public Transform TableDetailsDataParent;

    //[Header ("ScriptableObjects")]


    //[Header ("DropDowns")]


    //[Header ("Images")]


    //[Header ("Text")]


    [Header("Prefabs")]
    public TableDetailsObj ObjTableDetailsTorunament;
    public List<TableDetailsObj> TableDetailsTorunamentObjList;

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
        StopCoroutine("RefreshTableOnInterval");
        Reset();
        TournamentDetailsId = "";
        TableDetailsTorunamentObjList.Clear();
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
            UIManager.Instance.SocketGameManager.getTournamentTables(TournamentDetailsId, UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.pokerGameType, (socket, packet, args) =>
            {

                Debug.Log("getTournamentTables  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventListResponse<RoomsListing.Room> resp = JsonUtility.FromJson<PokerEventListResponse<RoomsListing.Room>>(resp1);


                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    for (int i = 0; i < resp.result.Count; i++)
                    {
                        TableDetailsObj obj = GetTableObjIfAlreadyCreated(resp.result[i].roomId);
                        if (obj != null)
                        {
                            obj.SetData(resp.result[i], i);
                        }
                        else
                        {
                            TableDetailsObj PlayerDetailsTorunamentDetails = Instantiate(ObjTableDetailsTorunament) as TableDetailsObj;
                            PlayerDetailsTorunamentDetails.SetData(resp.result[i], i);
                            PlayerDetailsTorunamentDetails.transform.SetParent(TableDetailsDataParent, false);
                            TableDetailsTorunamentObjList.Add(PlayerDetailsTorunamentDetails);
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

        if (UIManager.Instance.gameType == GameType.sng)
        {
            UIManager.Instance.SocketGameManager.getSngTournamentTables(TournamentDetailsId, UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.pokerGameType, (socket, packet, args) =>
            {

                Debug.Log("getSngTournamentTables  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse<RoomsListing.Room> resp = JsonUtility.FromJson<PokerEventResponse<RoomsListing.Room>>(resp1);

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    //for (int i = 0; i < resp.result.Count; i++) {
                    TableDetailsObj obj = GetTableObjIfAlreadyCreated(resp.result.roomId);
                    if (obj != null)
                    {
                        obj.SetData(resp.result);
                    }
                    else
                    {
                        TableDetailsObj PlayerDetailsTorunamentDetails = Instantiate(ObjTableDetailsTorunament) as TableDetailsObj;
                        PlayerDetailsTorunamentDetails.SetData(resp.result);
                        PlayerDetailsTorunamentDetails.transform.SetParent(TableDetailsDataParent, false);
                        TableDetailsTorunamentObjList.Add(PlayerDetailsTorunamentDetails);

                        RemoveOtherPlayer(resp.result);
                    }
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }

            });
        }
    }
    void Reset()
    {
        foreach (Transform child in TableDetailsDataParent)
        {
            Destroy(child.gameObject);
        }
    }
    private void RemoveOtherPlayer(RoomsListing.Room roomsList)
    {
        if (TableDetailsTorunamentObjList != null)
        {
            if (roomsList == null)
            {
                foreach (TableDetailsObj tro in TableDetailsTorunamentObjList.ToArray())
                {
                    TableDetailsTorunamentObjList.Remove(tro);
                    Destroy(tro.gameObject);
                }
            }
            else
            {
                string roomIdsList = roomsList.roomId;

                foreach (TableDetailsObj tro in TableDetailsTorunamentObjList.ToArray())
                {

                    if (roomsList == null || !roomIdsList.Equals(tro.TournamentTableId))
                    {
                        TableDetailsTorunamentObjList.Remove(tro);
                        Destroy(tro.gameObject);
                    }

                }
            }
        }
    }
    private void RemoveOtherPlayers(List<RoomsListing.Room> roomsList)
    {
        if (TableDetailsTorunamentObjList != null)
        {
            if (roomsList == null || roomsList.Count == 0)
            {
                foreach (TableDetailsObj tro in TableDetailsTorunamentObjList.ToArray())
                {
                    TableDetailsTorunamentObjList.Remove(tro);
                    Destroy(tro.gameObject);
                }
            }
            else
            {
                List<string> roomIdsList = roomsList.Select(o => o.roomId).ToList();

                foreach (TableDetailsObj tro in TableDetailsTorunamentObjList.ToArray())
                {
                    for (int i = 0; i < roomsList.Count; i++)
                    {
                        if (roomsList == null || !roomIdsList.Contains(tro.TournamentTableId))
                        {
                            TableDetailsTorunamentObjList.Remove(tro);
                            Destroy(tro.gameObject);
                        }
                    }
                }
            }
        }
    }

    private TableDetailsObj GetTableObjIfAlreadyCreated(string tableID)
    {
        if (TableDetailsTorunamentObjList != null)
        {
            for (int i = 0; i < TableDetailsTorunamentObjList.Count; i++)
            {
                if (tableID.Equals(TableDetailsTorunamentObjList[i].TournamentTableId))
                {
                    return TableDetailsTorunamentObjList[i];
                }
            }
        }
        return null;
    }

    private void DestroyAllTournamentTables()
    {
        if (TableDetailsTorunamentObjList == null)
            return;

        if (TableDetailsTorunamentObjList != null)
        {
            foreach (TableDetailsObj go in TableDetailsTorunamentObjList)
            {
                Destroy(go.gameObject);
            }
            TableDetailsTorunamentObjList = new List<TableDetailsObj>();
        }

    }


    /*
	 * void DemoDisplayData()
	{
		for (int i = 0; i < 1; i++) 
		{
			TableDetailsObj PlayerDetailsTorunamentDetails = Instantiate (ObjTableDetailsTorunament) as TableDetailsObj;
			PlayerDetailsTorunamentDetails.SetData(i+". Tablename");
			PlayerDetailsTorunamentDetails.transform.SetParent (TableDetailsDataParent, false);
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
