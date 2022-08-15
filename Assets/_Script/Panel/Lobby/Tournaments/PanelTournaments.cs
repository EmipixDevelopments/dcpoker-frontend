using BestHTTP.SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTournaments : MonoBehaviour
{
    [SerializeField] TournamentTableElement _tournamentTablePrefab;
    [SerializeField] Transform _content;

    [SerializeField] private List<TournamentTableElement> _tableElements = new List<TournamentTableElement>();
    private int updateTimeInSecconds = 3;

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateAtTime());
    }

    bool firstStart = true;
    IEnumerator UpdateAtTime() 
    {
        if (firstStart)
        {
            RemoveAllRow();
            UpdateTable();
            firstStart = false;
        }
        yield return new WaitForSeconds(updateTimeInSecconds);
        RemoveAllRow();
        UpdateTable();
    }

    private void RemoveAllRow()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
        _tableElements = new List<TournamentTableElement>();
    }

    private void UpdateTable()
    {
        if (UIManager.Instance)
        {
            string tournamentPokerType = "all";
            string selectedGameSpeed = UIManager.Instance.selectedGameSpeed.ToString();
            bool isLimitSelected = false;
            string gametype = "Touranment"; // or "sng";
            string selectedLimitType = "all";
            string selectedStack = "all";
            string selectedPlayerPerTable = "all";

        UIManager.Instance.SocketGameManager.SearchTournamentLobby(
            tournamentPokerType,
            selectedGameSpeed,
            "",
            isLimitSelected,
            gametype,
            selectedLimitType,
            selectedStack,
            selectedPlayerPerTable,
            OnRegularTableListReceived);
        }
    }

    private void OnRegularTableListReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnRegularTableListReceived : " + packet.ToString());

        JSONArray arr = new JSONArray(packet.ToString());
        string Source = arr.getString(arr.length() - 1);
        NormalTournamentDetails touramentDetail = JsonUtility.FromJson<NormalTournamentDetails>(Source);

        if (!touramentDetail.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(touramentDetail.message, null);
            return;
        }

        if (touramentDetail.result != null)
        {
            // if the number of rows in the table has not changed, update them
            if (_tableElements.Count == touramentDetail.result.Count)
            {
                for (int i = 0; i < touramentDetail.result.Count; i++)
                {
                    var rowTableData = touramentDetail.result[i];
                    _tableElements[i].UpdateValue(rowTableData);
                }
            }
            else // remove all rows and create new rows
            {
                RemoveAllRow();
                // create new row
                foreach (var item in touramentDetail.result)
                {
                    TournamentTableElement row = Instantiate(_tournamentTablePrefab, _content);
                    row.Init(item);
                    _tableElements.Add(row);
                }
                // update all UI
                //UIManager.Instance.LobbyPanelNew.UpdatePanel();
                StartCoroutine(UpdateUIInNextFrame());
            }
        }
    }

    IEnumerator UpdateUIInNextFrame()
    {
        yield return new WaitForEndOfFrame();
        UIManager.Instance.LobbyPanelNew.UpdatePanel();
    }
}
