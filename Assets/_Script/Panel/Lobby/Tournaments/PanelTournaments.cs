using BestHTTP.SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PanelTournaments : MonoBehaviour
{
    [SerializeField] private TournamentTableFilterPanel _tournamentTableFilter;
    [Space]
    [SerializeField] private TournamentTableElement _tournamentTablePrefab;
    [SerializeField] private RectTransform _content;

    private TableContainer<TournamentTableElement> _tableContainer;
    private int _delayUpdateTableSeconds = 5;
    private int _oldTableContainerAmount;
    private Coroutine _updateTableListCoroutine;
    

    private void Start()
    {
        _tableContainer = new TableContainer<TournamentTableElement>(_content, _tournamentTablePrefab, element => element.Init(this));
        _tournamentTableFilter.FilterChanged = OnEnable;
    }
    private void OnDestroy()
    {
        _tournamentTableFilter.FilterChanged = null;
    }

    private void OnEnable()
    {
        _updateTableListCoroutine = StartCoroutine(UpdateTableList());
    }

    private void OnDisable()
    {
        StopCoroutine(_updateTableListCoroutine);
    }

    private IEnumerator UpdateTableList()
    {
        while (true)
        {
            UpdateTable();
            yield return new WaitForSeconds(_delayUpdateTableSeconds);
        }
    }

    public void UpdateTable()
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
        
        // use filter
        tableData = _tournamentTableFilter.UseFilter(tableData);

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
    
    private void UpdateUi()
    {
        //LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
        LayoutRebuilder.MarkLayoutForRebuild(_content);
        UIManager.Instance.LobbyPanelNew.UpdateUi();
    }
}
