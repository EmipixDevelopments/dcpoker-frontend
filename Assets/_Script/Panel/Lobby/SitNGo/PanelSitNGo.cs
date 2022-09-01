using BestHTTP.SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSitNGo : MonoBehaviour
{
    [SerializeField] private SitNGoTableFilterPanel _sitNGoTableFilter;
    [Space]
    [SerializeField] private SitNGoTableElement _sitNGoTablePrefab;
    [SerializeField] private Transform _content;

    private int _updatePanelAfterSecconds = 8;
    private List<SitNGoTableElement> _tableElements = new List<SitNGoTableElement>();
    
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
        
        _sitNGoTableFilter.FilterChanged = OnEnable;
    }
    private void OnDestroy()
    {
        _sitNGoTableFilter.FilterChanged = null;
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
            UIManager.Instance.gameType = GameType.sng;
            UIManager.Instance.selectedGameType = GameType.sng;

            string tournamentPokerType = "all";
            GameSpeed selectedGameSpeed = UIManager.Instance.selectedGameSpeed;
            bool isLimitSelected = false;
            string gametype = "sng"; //"Touranment";
            string selectedLimitType = "all";
            string selectedStack = "all";
            string selectedPlayerPerTable = "all";

            UIManager.Instance.SocketGameManager.SearchSngLobby(
                tournamentPokerType, selectedGameSpeed,
                "", 
                isLimitSelected, 
                gametype,
                selectedLimitType, 
                selectedStack, 
                selectedPlayerPerTable, OnSNGTableListReceived);
        }
    }

    private void OnSNGTableListReceived(Socket socket, Packet packet, object[] args)
    {
        Debug.Log("OnSNGTableListReceived : " + packet.ToString());

        JSONArray arr = new JSONArray(packet.ToString());
        string Source = arr.getString(arr.length() - 1);

        TournamentRoomObject roomsResp = JsonUtility.FromJson<TournamentRoomObject>(Source);
        if (!roomsResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(roomsResp.message, null);
            return;
        }

        List<TournamentRoomObject.TournamentRoom> tableData = roomsResp.result;

        if (tableData != null)
        {
            // used filter
            tableData = _sitNGoTableFilter.UseFilter(tableData);

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
                        SitNGoTableElement row = Instantiate(_sitNGoTablePrefab, _content);
                         row.Init(item);
                        _tableElements.Add(row);
                    }
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
