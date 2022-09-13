using System;
using System.Collections.Generic;
using BestHTTP.SocketIO;
using UnityEngine;

public class HomeOmahaPolo5TableList : MonoBehaviour
{
    [SerializeField] private HomeSmallTableElement _prefab;
    
    private int _amountOmaha = 3;
    private int _amountElements = 5;
    private HomeSmallTableElement[] _homeSmallTableElements;

    private List<RoomsListing.Room> _roomsOmaha;
    private List<RoomsListing.Room> _roomsPolo5;
    private bool _lock;

    private void Start()
    {
        _homeSmallTableElements = new HomeSmallTableElement[_amountElements];
        Init();
    }

    private void Init()
    {
        for (var i = 0; i < _amountElements; i++)
        {
            _homeSmallTableElements[i] = Instantiate(_prefab, transform);
        }
    }
    public void UpdateList()
    {
        if (UIManager.Instance)
        {

            bool isLimitSelected = false;
            string gametype = "";
            string selectedLimitType = "all";
            string selectedStack = "all";
            string selectedPlayerPerTable = "all";
            string currencyType = UIManager.Instance.currencyType.ToString();
            
            _lock = true;

            UIManager.Instance.SocketGameManager.SearchLobby(
                GameType.omaha.ToString(),
                UIManager.Instance.selectedGameSpeed,
                "",
                isLimitSelected,
                gametype,
                selectedLimitType,
                selectedStack,
                selectedPlayerPerTable,
                currencyType,
                (socket, packet, args) => ListReceived(packet, GameType.omaha));
            
            UIManager.Instance.SocketGameManager.SearchLobby(
                GameType.PLO5.ToString(),
                UIManager.Instance.selectedGameSpeed,
                "",
                isLimitSelected,
                gametype,
                selectedLimitType,
                selectedStack,
                selectedPlayerPerTable,
                currencyType,
                (socket, packet, args) => ListReceived(packet, GameType.PLO5));
        }
    }

    private void ListReceived(Packet packet, GameType type)
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

        if (!roomsResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(roomsResp.message, null);
            return;
        }

        if (type == GameType.omaha)
        {
            _roomsOmaha = roomsResp.result;
        }
        else
        {
            _roomsPolo5 = roomsResp.result;
        }

        if (_lock == false)
        {
            UpdateUiData();
            return;
        }
        
        _lock = false;
    }

    private void UpdateUiData()
    {
        var omahaRoomAmount = Math.Min(_amountOmaha, _roomsOmaha.Count);
        var polo5RoomAmount = Math.Min(_amountElements - omahaRoomAmount, _roomsPolo5.Count);
        
        for (var i = 0; i < omahaRoomAmount; i++)
        {
            var element = _homeSmallTableElements[i];
            element.SetInfo(_roomsOmaha[i]);
            element.gameObject.SetActive(true);
        }
        for (var i = 0; i < polo5RoomAmount; i++)
        {
            var element = _homeSmallTableElements[i + omahaRoomAmount];
            element.SetInfo(_roomsPolo5[i]);
            element.gameObject.SetActive(true);
        }

        var amountRoom = omahaRoomAmount + polo5RoomAmount;
        if(amountRoom >= _amountElements )
            return;
        
        for(var i = amountRoom; i < _amountElements; i++)
        {
            _homeSmallTableElements[i].gameObject.SetActive(false);
        }
    }
}
