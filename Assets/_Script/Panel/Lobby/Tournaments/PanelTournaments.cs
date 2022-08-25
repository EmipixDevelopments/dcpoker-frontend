using BestHTTP.SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTournaments : MonoBehaviour
{
    [SerializeField] private TournamentTableFilterPanel _tournamentTableFilter;
    [Space]
    [SerializeField] private TournamentTableElement _tournamentTablePrefab;
    [SerializeField] private Transform _content;
    
    private int _updatePanelAfterSecconds = 8;
    private List<TournamentTableElement> _tableElements = new List<TournamentTableElement>();

    private void Start()
    {
        _tournamentTableFilter.FilterChanged = OnEnable;
    }
    private void OnDestroy()
    {
        _tournamentTableFilter.FilterChanged = null;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateAtTime());
    }

    IEnumerator UpdateAtTime() 
    {
        while (true)
        {
            UpdateTable();
            yield return new WaitForSeconds(_updatePanelAfterSecconds);
        }
    }

    private void RemoveAllRow()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
        _tableElements.Clear();
    }

    private void UpdateTable()
    {
        if (UIManager.Instance)
        {
            string tournamentPokerType = "all";
            string selectedGameSpeed = UIManager.Instance.selectedGameSpeed.ToString();
            bool isLimitSelected = false;
            string gametype = "Touranment";
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
        NormalTournamentDetails touramentsDetail = JsonUtility.FromJson<NormalTournamentDetails>(Source);

        if (!touramentsDetail.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(touramentsDetail.message, null);
            return;
        }

        List<NormalTournamentDetails.NormalTournamentData> tableData = touramentsDetail.result;
        // used filter
        tableData = _tournamentTableFilter.UseFilter(tableData);

        if (tableData != null)
        {
            // if the number of rows in the table has not changed, update them
            if (_tableElements.Count == tableData.Count)
            {
                for (int i = 0; i < tableData.Count; i++)
                {
                    var rowTableData = tableData[i];
                    _tableElements[i].UpdateValue(rowTableData);
                }
            }
            else // remove all rows and create new rows
            {
                RemoveAllRow();
                // create new row
                foreach (var item in tableData)
                {
                    TournamentTableElement row = Instantiate(_tournamentTablePrefab, _content);
                    row.Init(item);
                    _tableElements.Add(row);
                }
                // update all UI
                UIManager.Instance.LobbyPanelNew.UpdatePanel();
            }
        }
    }
}
