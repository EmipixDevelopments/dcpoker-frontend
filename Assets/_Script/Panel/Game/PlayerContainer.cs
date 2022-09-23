using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    [SerializeField] private PlayerPlace[] _playerPlaces;

    [SerializeField] private List<PlayPlaceIndex> _playerIndexPlayerPlace;

    private List<PlayerPlace> _currentOrderPlace;
    private List<PlayerPlace> _currentPlace;

    public void Init()
    {
        _currentOrderPlace = new List<PlayerPlace>();
        _currentPlace = new List<PlayerPlace>();
        foreach (var playerPlace in _playerPlaces)
        {
            playerPlace.Init();
        }

        ResetPlace();
    }

    public void SetActivePlayerPlace(int playerCount)
    {
        if(playerCount < 2 || playerCount > 9)
            return;
        
        ResetPlace();
        _currentOrderPlace.Clear();

        var playerPlaceIndex = _playerIndexPlayerPlace[playerCount - 2];
        InitPlayerPlace(playerPlaceIndex._orderPlayerPlaceIndex, _currentOrderPlace);
        InitPlayerPlace(playerPlaceIndex._playerPlaceIndex, _currentPlace);
    }

    private void InitPlayerPlace(string playerIndexesString, List<PlayerPlace> playerPlaces)
    {
        var indexesString = playerIndexesString.Split(',');
        foreach (var indexString in indexesString)
        {
            var index = int.Parse(indexString);
            var playerPlace = _playerPlaces[index];
            
            playerPlace.Reset();
            playerPlaces.Add(playerPlace);
            
            playerPlace.gameObject.SetActive(true);
        }
    }

    public void InitOpenSeatButtons(Action<int> action)
    {
        for (var i = 0; i < _currentOrderPlace.Count; i++)
        {
            var index = i;
            _currentOrderPlace[i].SetOpenSeat(() => action.Invoke(index));
        }
    }
    
    public void InitOpenSeatButton(int index, Action action)
    {
        _currentOrderPlace[index].SetOpenSeat(action.Invoke);
    }

    public void SetActiveOpenSeatButtons(bool active)
    {
        foreach (var playerPlace in _currentOrderPlace)
        {
            playerPlace.SetActiveOpenSeatButton(active);
        }
    }

    public int GetPlayerPlaceCount() => _currentPlace.Count;

    public void SetActiveOpenSeatButton(int index, bool active)
    {
        _currentPlace[index].gameObject.SetActive(active);
    }

    private void ActivePlayerPlace(int[] playerPlace)
    {
        for (var i = 0; i < playerPlace.Length; i++)
        {
            var place = _playerPlaces[playerPlace[i]];
            _playerPlaces[playerPlace[i]].gameObject.SetActive(true);   
        }
    }

    private void ResetPlace()
    {
        foreach (var playerPlace in _playerPlaces)
        {
            playerPlace.gameObject.SetActive(false);
            playerPlace.Reset();
        }
    }
}

[Serializable]
public class PlayPlaceIndex
{
    public string _playerPlaceIndex;
    public string _orderPlayerPlaceIndex;
}