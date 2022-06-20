using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyTournaryPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    [Header("Transforms")]
    public Transform transfomrListContainer;

    [Header("Prefabs")]
    public TournaryListPrefab tables;

    [Header("Text")]
    public TextMeshProUGUI txtNoOfPlayedGames;
    public TextMeshProUGUI txtTotalWonGames;
    public TextMeshProUGUI txtTotalLoseGames;

    #endregion

    #region PRIVATE_VARIABLES
    private List<TournaryListPrefab> list = new List<TournaryListPrefab>();
    #endregion

    #region UNITY_CALLBACKS
    void OnEnable()
    {
        CallAPI();
    }
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS

    void CallAPI()
    {
        ResetData();

        UIManager.Instance.SocketGameManager.MyTournament((socket3, packet3, args3) =>
        {
            Debug.Log(Constants.PokerEvents.MyTournament + " Response : " + packet3.ToString());

            JSONArray arr = new JSONArray(packet3.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;
            PokerEventResponse<TournamentsItemResult> resp = JsonUtility.FromJson<PokerEventResponse<TournamentsItemResult>>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                for (int i = 0; i < resp.result.tournaments.Count; i++)
                {
                    TournaryListPrefab tableDestails = Instantiate(tables) as TournaryListPrefab;
                    list.Add(tableDestails);

                    tableDestails.SetTournamentData(i, resp.result.tournaments[i]);
                    tableDestails.transform.SetParent(transfomrListContainer, false);
                }
            }

            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message, null);
            }

        });


    }
    #endregion

    #region PRIVATE_METHODS
    private void ResetData()
    {
        foreach (TournaryListPrefab data in list)
        {
            Destroy(data.gameObject);
        }
        list.Clear();
    }
    #endregion

    #region COROUTINES
    #endregion

    #region GETTER_SETTER
    public string PlayedGamesCount
    {
        set
        {
            txtNoOfPlayedGames.text = value;
        }
    }

    public string WonGamesCount
    {
        set
        {
            txtTotalWonGames.text = value;
        }
    }

    public string LostGamesCount
    {
        set
        {
            txtTotalLoseGames.text = value;
        }
    }
    #endregion
}
