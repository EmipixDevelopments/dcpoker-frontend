using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SitNGoTableFilterPanel : MonoBehaviour
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
        _gameFilter.Init(Constants.TablePlayerPrefsKeys.SitNGoTableSettingsGameFilter.ToString());
        _gameFilter.FilterChanged = null;
        _gameFilter.FilterChanged = () => FilterChanged?.Invoke();

        _priceFilter.Init(Constants.TablePlayerPrefsKeys.SitNGoTableSettingsPriceFilter.ToString());
        _priceFilter.FilterChanged = null;
        _priceFilter.FilterChanged = () => FilterChanged?.Invoke();

        _playerPerTableFilter.Init(Constants.TablePlayerPrefsKeys.SitNGoTableSettingsPlayerPerTableFilter.ToString());
        _playerPerTableFilter.FilterChanged = null;
        _playerPerTableFilter.FilterChanged = () => FilterChanged?.Invoke();
    }

    internal List<TournamentRoomObject.TournamentRoom> UseFilter(List<TournamentRoomObject.TournamentRoom> tableData)
    {
        List<TournamentRoomObject.TournamentRoom> answer = new List<TournamentRoomObject.TournamentRoom>();
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
        int lowMin = 0;
        int lowMax = 10;
        int mediumMin = 11;
        int mediumMax = 50;
        int hightMin = 51;
        int hightMax = int.MaxValue;

        List<string> priceFilterValue = _priceFilter.GetFilterValue();
        List<TournamentRoomObject.TournamentRoom> afterPriceFilter = new List<TournamentRoomObject.TournamentRoom>();
        foreach (var priceFilter in priceFilterValue)
        {
            if (priceFilter == "low")
            {
                afterPriceFilter.AddRange(GetTableWithInPrice(answer, lowMin, lowMax));
            }
            if (priceFilter == "medium")
            {
                afterPriceFilter.AddRange(GetTableWithInPrice(answer, mediumMin, mediumMax));
            }
            if (priceFilter == "hight")
            {
                afterPriceFilter.AddRange(GetTableWithInPrice(answer, hightMin, hightMax));
            }
        }
        afterPriceFilter.OrderBy(sortBy => sortBy.buyIn); // sorting
        answer.Clear();
        answer.AddRange(afterPriceFilter);
        //-----------------------------------------

        /*
        // playerfilter
        List<string> playerFilter = _playerPerTableFilter.GetFilterValue();
        //-----------------------------------------
        // player per table filter
        valueAfterPriceFilter.Clear();
        List<string> playerPerTableValue = _playerPerTableFilter.GetFilterValue();
        foreach (var playersPerTable in playerPerTableValue)
        {
            if (playersPerTable == "all")
            {
                valueAfterPriceFilter.AddRange(answer);
            }
            if (playersPerTable == "2")
            {
                valueAfterPriceFilter.AddRange(GetTableWithPlayersPerTable(answer, 2));
            }
            if (playersPerTable == "6")
            {
                valueAfterPriceFilter.AddRange(GetTableWithPlayersPerTable(answer, 6));
            }
            if (playersPerTable == "8")
            {
                valueAfterPriceFilter.AddRange(GetTableWithPlayersPerTable(answer, 8));
            }
            if (playersPerTable == "9")
            {
                valueAfterPriceFilter.AddRange(GetTableWithPlayersPerTable(answer, 9));
            }
        }
        valueAfterPriceFilter.OrderBy(sortBy => sortBy.maxPlayers); // sorting
        answer.Clear();
        answer.AddRange(valueAfterPriceFilter);
        //------------------------------------------
        */
        return answer;
    }
    private List<TournamentRoomObject.TournamentRoom> GetTableWithInPrice(List<TournamentRoomObject.TournamentRoom> tables, int minValue, int maxValue)
    {
        List<TournamentRoomObject.TournamentRoom> answer = new List<TournamentRoomObject.TournamentRoom>();
        foreach (var table in tables)
        {
            int byInValue = ParsingByinValue(table.buyIn);
            if (byInValue >= minValue && byInValue <= maxValue)
            {
                answer.Add(table);
            }
        }
        return answer;
    }
    private int ParsingByinValue(string value)
    {
        int answer;
        string[] strArr = value.Split('+');
        answer = int.Parse(strArr[0]) + int.Parse(strArr[1]);
        return answer;
    }
}
