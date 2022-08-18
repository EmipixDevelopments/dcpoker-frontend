using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TournamentTableFilterPanel : MonoBehaviour
{
    [SerializeField] private AllAndOtherToggleFilter _gameFilter;
    [SerializeField] private AllAndOtherToggleFilter _priceFilter;
    [SerializeField] private PlayerPerTableFilter _playerPerTableFilter;

    public Action FilterChanged;

    private void OnEnable()
    {
        Init();
    }
    private void Init()
    {
        _gameFilter.Init(Constants.PlayerPrefsKeys.TournamentTableSettingsGameFilter);
        _gameFilter.FilterChanged = null;
        _gameFilter.FilterChanged = () => FilterChanged?.Invoke();

        _priceFilter.Init(Constants.PlayerPrefsKeys.TournamentTableSettingsPriceFilter);
        _priceFilter.FilterChanged = null;
        _priceFilter.FilterChanged = () => FilterChanged?.Invoke();

        _playerPerTableFilter.Init(Constants.PlayerPrefsKeys.TournamentTableSettingsPlayerPerTableFilter);
        _playerPerTableFilter.FilterChanged = null;
        _playerPerTableFilter.FilterChanged = () => FilterChanged?.Invoke();
    }

    public List<NormalTournamentDetails.NormalTournamentData> UseFilter(List<NormalTournamentDetails.NormalTournamentData> tableData)
    {
        List<NormalTournamentDetails.NormalTournamentData> answer = new List<NormalTournamentDetails.NormalTournamentData>();

        // game GameType filter
        List<string> gameFilter = _gameFilter.GetFilterValue();
        List<string> gameFilterParser = new List<string>();
        foreach (string item in gameFilter)
        {
            if (item == "texas holdem") gameFilterParser.Add("texas");
            if (item == "omaha") gameFilterParser.Add("omaha");
            if (item == "plo5") gameFilterParser.Add("PLO5");
        }
        gameFilter = new List<string>(gameFilterParser);
        // use game filter
        foreach (var tableItem in tableData)
        {
            foreach (var game in gameFilter)
            {
                if (tableItem.pokerGameType == game)
                {
                    answer.Add(tableItem);
                }
            }
        }

        // Price filter
        List<string> priveValue = _priceFilter.GetFilterValue();
        int lowMin = 0;
        int lowMax = 10;
        int mediumMin = 10;
        int mediumMax = 50;
        int hightMin = 50;
        int hightMax = int.MaxValue;
        bool freeroll = false; // or true
        /*
         0 - 10 $ - low leaves
        10 - 50 $ - medium level
        50 - endless  - Hight level 
        freeroll is freeroll
         */

        // playerfilter
        List<string> playerFilter = _playerPerTableFilter.GetFilterValue();

        return answer;
    }
}
