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


        // Player per table filter
        afterPriceFilter.Clear();
        List<string> playerPerTableValue = _playerPerTableFilter.GetFilterValue();
        foreach (var playersPerTable in playerPerTableValue)
        {
            if (playersPerTable == "all")
            {
                afterPriceFilter.AddRange(answer);
            }
            if (playersPerTable == "2")
            {
                afterPriceFilter.AddRange(GetTableWithPlayersPerTable(answer, 2));
            }
            if (playersPerTable == "6")
            {
                afterPriceFilter.AddRange(GetTableWithPlayersPerTable(answer, 6));
            }
            if (playersPerTable == "8")
            {
                afterPriceFilter.AddRange(GetTableWithPlayersPerTable(answer, 8));
            }
            if (playersPerTable == "9")
            {
                afterPriceFilter.AddRange(GetTableWithPlayersPerTable(answer, 9));
            }
        }
        afterPriceFilter.OrderBy(sortBy => GetMaxPlayersPerTable(sortBy.seat)); // sorting
        answer.Clear();
        answer.AddRange(afterPriceFilter);
        //------------------------------------------

        answer = RemoveDuplicate(answer);
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

    private List<TournamentRoomObject.TournamentRoom> GetTableWithPlayersPerTable(List<TournamentRoomObject.TournamentRoom> data, int value)
    {
        List<TournamentRoomObject.TournamentRoom> answer = new List<TournamentRoomObject.TournamentRoom>();
        foreach (var table in data)
        {
            int byInValue = GetMaxPlayersPerTable(table.seat);
            if (byInValue == value)
            {
                answer.Add(table);
            }
        }
        return answer;
    }
    private int GetMaxPlayersPerTable(string value)
    {
        string[] strArr = value.Split('/');
        return int.Parse(strArr[1]);
    }

    private List<TournamentRoomObject.TournamentRoom> RemoveDuplicate(List<TournamentRoomObject.TournamentRoom> data)
    {
        List<TournamentRoomObject.TournamentRoom> answer = new List<TournamentRoomObject.TournamentRoom>();
        foreach (var item in data)
        {
            if (!answer.Contains(item))
            {
                answer.Add(item);
            }
        }
        return answer;
    }
}
