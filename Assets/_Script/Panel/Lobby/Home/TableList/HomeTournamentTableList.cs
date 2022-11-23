using System.Collections.Generic;
using BestHTTP.SocketIO;
using UnityEngine;

public class HomeTournamentTableList : MonoBehaviour
{
    [SerializeField] private HomeBigTableElement _prefab;

    private HomeBigTableElement[] _homeBigTableElements = new HomeBigTableElement[5];
    private int _amountElements = 5;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (var i = 0; i < _amountElements; i++)
        {
            _homeBigTableElements[i] = Instantiate(_prefab, transform);
        }
    }
    public void UpdateList()
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
    
    private void OnRegularTableListReceived(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("OnRegularTableListReceived Home: " + packet.ToString());
        
        JSONArray arr = new JSONArray(packet.ToString());
        string Source = arr.getString(arr.length() - 1);
        var touramentsDetail = JsonUtility.FromJson<NormalTournamentDetails>(Source);
        
        if (!touramentsDetail.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(touramentsDetail.message, null);
            return;
        }

        var tableData = touramentsDetail.result;

        for (var i = 0; i < 5; i++)
        {
            if ( i < tableData.Count)
            {
                var element = _homeBigTableElements[i];
                element.SetInfo(tableData[i]);
                element.gameObject.SetActive(true);
            }
            else
            {
                _homeBigTableElements[i].gameObject.SetActive(false);
            }
        }
    }
    
}
