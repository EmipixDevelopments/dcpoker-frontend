using BestHTTP.SocketIO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PanelSitNGo : MonoBehaviour
{
    [SerializeField] private SitNGoTableFilterPanel _sitNGoTableFilter;
    [Space]
    [SerializeField] private SitNGoTableElement _sitNGoTablePrefab;
    [SerializeField] private RectTransform _content;
    
    private TableContainer<SitNGoTableElement> _tableContainer;
    private int _delayUpdateTableMilliseconds = 8000;
    private bool _isNeedUpdate;
    private int _oldTableContainerAmount;

    private void Start()
    {
        _sitNGoTableFilter.FilterChanged = UpdateTable;
        _tableContainer = new TableContainer<SitNGoTableElement>( _content, _sitNGoTablePrefab, element => element.Init(this));
    }
    
    private void OnDestroy()
    {
        _sitNGoTableFilter.FilterChanged = null;
    }

    private void OnEnable()
    {
        StartUpdateTableAsync();
    }

    private void OnDisable()
    {
        _isNeedUpdate = false;
    }

    private async void StartUpdateTableAsync()
    {
        _isNeedUpdate = true;
        while (_isNeedUpdate)
        {
            UpdateTable();
            await Task.Delay(_delayUpdateTableMilliseconds);
        }
    }
    
    public void UpdateTable()
    {
        if (UIManager.Instance)
        {
            UIManager.Instance.gameType = GameType.sng;
            UIManager.Instance.selectedGameType = GameType.sng;

            string tournamentPokerType = "all";
            GameSpeed selectedGameSpeed = UIManager.Instance.selectedGameSpeed;
            bool isLimitSelected = false;
            string gametype = "all"; //"Touranment";
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

        var tableData = roomsResp.result;

        if (tableData != null)
        {
            // use filter
            tableData = _sitNGoTableFilter.UseFilter(tableData);

            if (tableData != null)
            {
                for (var i = 0; i < tableData.Count; i++)
                {
                    _tableContainer.GetElement(i).SetData(tableData[i]);
                }
                _tableContainer.HideFromIndex(tableData.Count);

                if (_oldTableContainerAmount != tableData.Count)
                {
                    UpdateUi();
                    _oldTableContainerAmount = tableData.Count;
                }
            }
        }
    }

    private void UpdateUi()
    {
        //LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
        LayoutRebuilder.MarkLayoutForRebuild(_content);
        UIManager.Instance.LobbyPanelNew.UpdateUi();
    }
}
