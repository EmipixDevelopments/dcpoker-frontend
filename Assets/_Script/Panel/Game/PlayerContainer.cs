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

        var playerPlaceIndex = _playerIndexPlayerPlace[playerCount - 2];
        InitPlayerPlace(playerPlaceIndex._orderPlayerPlaceIndex, _currentOrderPlace);
        InitPlayerPlace(playerPlaceIndex._playerPlaceIndex, _currentPlace);
    }

    private void InitPlayerPlace(string playerIndexesString, List<PlayerPlace> playerPlaces)
    {
        playerPlaces.Clear();
        
        var indexesString = playerIndexesString.Split(',');
        
        foreach (var indexString in indexesString)
        {
            var index = int.Parse(indexString);
            var playerPlace = _playerPlaces[index];
            
            //playerPlace.Reset();
            playerPlaces.Add(playerPlace);
            
            playerPlace.gameObject.SetActive(true);
        }
    }

    private void ResetPlace()
    {
        foreach (var playerPlace in _playerPlaces)
        {
            playerPlace.gameObject.SetActive(false);
            playerPlace.SetIsBigPlayer(false);
            //playerPlace.Reset();
        }
    }

    public List<PlayerPlace> GetPlayerPaces() => _currentPlace;
    public List<PlayerPlace> GetOrderPlayerPaces() => _currentOrderPlace;

}

[Serializable]
public class PlayPlaceIndex
{
    public string _playerPlaceIndex;
    public string _orderPlayerPlaceIndex;
}
