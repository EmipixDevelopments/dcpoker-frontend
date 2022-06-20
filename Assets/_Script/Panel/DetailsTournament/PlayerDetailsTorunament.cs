using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDetailsTorunament : MonoBehaviour
{


    #region PUBLIC_VARIABLES

    //[Header ("Gamobjects")]

    [Header("Transforms")]
    public Transform PlayersDataParent;

    //[Header ("ScriptableObjects")]


    //[Header ("DropDowns")]


    //[Header ("Images")]


    //[Header ("Text")]


    [Header("Prefabs")]
    public PlayerDetailsTorunamentObj ObjPlayerDetailsTorunament;
    public List<PlayerDetailsTorunamentObj> PlayerDetailsTorunamentObjList;
    //[Header ("Enums")]


    [Header("Variables")]
    public string TournamentDetailsId = "";
    public string SNGRoomId = "";
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
        PlayerDetailsTorunamentObjList.Clear();
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
        foreach (Transform child in PlayersDataParent)
        {
            Destroy(child.gameObject);
        }
    }

    /*	void DemoDisplayData()
        {
            for (int i = 0; i < 15; i++) 
            {
                PlayerDetailsTorunamentObj PlayerDetailsTorunamentDetails = Instantiate (ObjPlayerDetailsTorunament) as PlayerDetailsTorunamentObj;
                PlayerDetailsTorunamentDetails.SetData(i);
                PlayerDetailsTorunamentDetails.transform.SetParent (PlayersDataParent, false);
            }
        }*/

    void CallDataApi()
    {
        if (UIManager.Instance.gameType == GameType.Touranment)
        {
            print("if");
            UIManager.Instance.SocketGameManager.getTournamentPlayers(TournamentDetailsId, UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.pokerGameType, (socket, packet, args) =>
            {

                Debug.Log("getTournamentPlayers  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventListResponse<getTournamentPlayers> resp = JsonUtility.FromJson<PokerEventListResponse<getTournamentPlayers>>(resp1);


                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    for (int i = 0; i < resp.result.Count; i++)
                    {
                        PlayerDetailsTorunamentObj obj = GetPlayersObjIfAlreadyCreated(resp.result[i].id);
                        if (obj != null)
                        {
                            obj.SetData(resp.result[i], i);
                        }
                        else
                        {
                            PlayerDetailsTorunamentObj PlayerDetailsTorunamentDetails = Instantiate(ObjPlayerDetailsTorunament) as PlayerDetailsTorunamentObj;
                            PlayerDetailsTorunamentDetails.SetData(resp.result[i], i);
                            PlayerDetailsTorunamentDetails.transform.SetParent(PlayersDataParent, false);
                            PlayerDetailsTorunamentObjList.Add(PlayerDetailsTorunamentDetails);
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
            print("else");
            UIManager.Instance.SocketGameManager.getSngTournamentPlayers(TournamentDetailsId, UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.pokerGameType, (socket, packet, args) =>
            {

                Debug.Log("getSngTournamentPlayers  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventListResponse<getTournamentPlayers> resp = JsonUtility.FromJson<PokerEventListResponse<getTournamentPlayers>>(resp1);


                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    for (int i = 0; i < resp.result.Count; i++)
                    {
                        PlayerDetailsTorunamentObj obj = GetPlayersObjIfAlreadyCreated(resp.result[i].id);
                        if (obj != null)
                        {
                            obj.SetData(resp.result[i], i);
                        }
                        else
                        {
                            PlayerDetailsTorunamentObj PlayerDetailsTorunamentDetails = Instantiate(ObjPlayerDetailsTorunament) as PlayerDetailsTorunamentObj;
                            PlayerDetailsTorunamentDetails.SetData(resp.result[i], i);
                            PlayerDetailsTorunamentDetails.transform.SetParent(PlayersDataParent, false);
                            PlayerDetailsTorunamentObjList.Add(PlayerDetailsTorunamentDetails);
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
    private void RemoveOtherPlayers(List<getTournamentPlayers> roomsList)
    {
        if (PlayerDetailsTorunamentObjList != null)
        {
            if (roomsList == null || roomsList.Count == 0)
            {
                foreach (PlayerDetailsTorunamentObj tro in PlayerDetailsTorunamentObjList.ToArray())
                {
                    PlayerDetailsTorunamentObjList.Remove(tro);
                    Destroy(tro.gameObject);
                }
            }
            else
            {
                List<string> roomIdsList = roomsList.Select(o => o.id).ToList();

                foreach (PlayerDetailsTorunamentObj tro in PlayerDetailsTorunamentObjList.ToArray())
                {
                    for (int i = 0; i < roomsList.Count; i++)
                    {
                        if (roomsList == null || !roomIdsList.Contains(tro.Id))
                        {
                            PlayerDetailsTorunamentObjList.Remove(tro);
                            Destroy(tro.gameObject);
                        }
                    }
                }
            }
        }
    }
    private PlayerDetailsTorunamentObj GetPlayersObjIfAlreadyCreated(string tableID)
    {
        if (PlayerDetailsTorunamentObjList != null)
        {
            for (int i = 0; i < PlayerDetailsTorunamentObjList.Count; i++)
            {
                if (tableID.Equals(PlayerDetailsTorunamentObjList[i].Id))
                {
                    return PlayerDetailsTorunamentObjList[i];
                }
            }
        }
        return null;
    }
    private void DestroyAllTournamentTables()
    {
        if (PlayerDetailsTorunamentObjList == null)
            return;

        if (PlayerDetailsTorunamentObjList != null)
        {
            foreach (PlayerDetailsTorunamentObj go in PlayerDetailsTorunamentObjList)
            {
                Destroy(go.gameObject);
            }
            PlayerDetailsTorunamentObjList = new List<PlayerDetailsTorunamentObj>();
        }

    }

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
