using BestHTTP.SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTable : MonoBehaviour
{
    [Header("Request settings")]
    [SerializeField] private GameType _gameType = GameType.texas;
    [SerializeField] private PokerGameType _pockerGameType = PokerGameType.all;
    [SerializeField] private GameType _selectedType = GameType.cash;
    [Space]
    [SerializeField] private TableFilterPanel _filtersPanel;
    [SerializeField] private TableElement _texasHoldemPrefab;
    [SerializeField] private Transform _content;
    private int _updatePanelAfterSeconds = 5;
    private TableContainer<TableElement> _tableContainer;
    //private List<TableElement> _tableElements = new List<TableElement>();
    private RectTransform _rectTransform;

    private void Start()
    {
        _filtersPanel.FilterChanged = OnEnable;
        _tableContainer = new TableContainer<TableElement>(_content, _texasHoldemPrefab, null);
        _rectTransform = GetComponent<RectTransform>();
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
            yield return new WaitForSeconds(_updatePanelAfterSeconds);
        }
    }

    private void UpdateTable()
    {
        if (UIManager.Instance)
        {
            UIManager.Instance.gameType = _gameType;
            UIManager.Instance.selectedGameType = _selectedType;

            PokerGameType pokerGameType = _pockerGameType;
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
                for (int i = 0; i < tableData.Count; i++)
                {
                    _tableContainer.GetElement(i).SetData(tableData[i]);
                }
                _tableContainer.HideFromIndex(tableData.Count);
                
                LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
                UIManager.Instance.LobbyPanelNew.UpdateUi();
            }
        }
    }
}
