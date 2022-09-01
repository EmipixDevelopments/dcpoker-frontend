using BestHTTP.SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int _updatePanelAfterSecconds = 8;
    private List<TableElement> _tableElements = new List<TableElement>();

    [Space] 
    [SerializeField] private GameObject _emptyTableElement;
    private List<GameObject> _tableEmptyElements = new List<GameObject>();
    private float _sizeTableElement;

    private RectTransform _canvasRectTransform;
    
    private int _sizeHeaderBottom = 123 + 109 + 117 + 135 + 50; // Size header + bottom + margin bottom 


    private void Start()
    {
        _canvasRectTransform = GetRootCanvas(_content.GetComponent<RectTransform>()).GetComponent<RectTransform>();
        _sizeTableElement = _emptyTableElement.GetComponent<RectTransform>().rect.height;
        
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
        _tableEmptyElements.Clear();
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
                    
                    //SetEmptyTableElements();

                    // update all UI
                    UIManager.Instance.LobbyPanelNew.UpdatePanel();
                }
            }
        }
        SetEmptyTableElements();
    }

    private void SetEmptyTableElements()
    {
        var size = _canvasRectTransform.rect.height - _sizeHeaderBottom;
        var amount = (int)(size / _sizeTableElement);
        amount -= _tableElements.Count + 1;

        if (amount <= _tableEmptyElements.Count)
        {
            Debug.Log(_tableEmptyElements.Count + " " + (amount));
            if (_tableEmptyElements.Count > amount)
            {
                RemoveEmptyAtRange(amount);
                UIManager.Instance.LobbyPanelNew.UpdatePanel(); 
            }
            return;
        }
        
        for (var i = 0; i < amount; i++)
        {
            var tableElement = Instantiate(_emptyTableElement, _content);
            _tableEmptyElements.Add(tableElement);
        }
        UIManager.Instance.LobbyPanelNew.UpdatePanel(); 
    }

    private void RemoveEmptyAtRange(int index)
    {
        for (var i = index; i < _tableEmptyElements.Count; i++)
        {
            var element = _tableEmptyElements[i];
            _tableEmptyElements.RemoveAt(i);
            Destroy(element);
        }
    }
    
    private Canvas GetRootCanvas(RectTransform rectTransform)
    {
        Canvas canvas = rectTransform.GetComponentInParent<Canvas>();
        return canvas.isRootCanvas ? canvas : canvas.rootCanvas;
    }
}
