using BestHTTP.SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTable : MonoBehaviour
{
    [SerializeField] private GameType _gameType = GameType.texas;
    [Space]
    [SerializeField] private TableFilterPanel _filtersPanel;
    [SerializeField] private TableElement _texasHoldemPrefab;
    [SerializeField] private Transform _content;
    private int _updatePanelAfterSecconds = 8;
    private List<TableElement> _tableElements = new List<TableElement>();

    private void Start()
    {
        _filtersPanel.FilterChanged = OnEnable;
    }
    private void OnDestroy()
    {
        _filtersPanel.FilterChanged = null;
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
            UIManager.Instance.gameType = _gameType;
            UIManager.Instance.selectedGameType = GameType.cash;

            PokerGameType pokerGameType = PokerGameType.texas;
            bool isLimitSelected = false;
            string gametype = "";
            string selectedLimitType = "all";
            string selectedStack = "all";
            string selectedPlayerPerTable = "all";
            string currencyType = UIManager.Instance.currencyType.ToString();


            UIManager.Instance.SocketGameManager.SearchLobby(
                pokerGameType.ToString(),
                UIManager.Instance.selectedGameSpeed,
                "",
                isLimitSelected,
                gametype,
                selectedLimitType,
                selectedStack,
                selectedPlayerPerTable,
                currencyType,
                TexasTableListReceived);
        }
    }

    private void TexasTableListReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log($"{UIManager.Instance.gameType}TableListReceived : {packet}");

        JSONArray arr = new JSONArray(packet.ToString());
        string source = arr.getString(arr.length() - 1);

        RoomsListing roomsResp = JsonUtility.FromJson<RoomsListing>(source);

        if (!roomsResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(roomsResp.message, null);
            return;
        }

        if (roomsResp.result != null)
        {
            //use filters
            List<RoomsListing.Room> tableData = roomsResp.result;
            tableData = _filtersPanel.UseFilters(tableData);

            if (tableData != null)
            {
                // if the number of rows in the table has not changed, update them
                if (_tableElements.Count == tableData.Count)
                {
                    for (int i = 0; i < tableData.Count; i++)
                    {
                        var rowTableData = tableData[i];
                        _tableElements[i].SetData(rowTableData);
                    }
                }
                else // remove all rows and create new rows
                {
                    RemoveAllRow();
                    // create new row
                    foreach (var item in tableData)
                    {
                        TableElement row = Instantiate(_texasHoldemPrefab, _content);
                        row.SetData(item);
                        _tableElements.Add(row);
                    }
                    // update all UI
                    UIManager.Instance.LobbyPanelNew.UpdatePanel();
                }
            }
        }
    }
}
