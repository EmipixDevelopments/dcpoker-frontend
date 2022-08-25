using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TableFilterPanel : MonoBehaviour
{
    [SerializeField] private AllAndOtherToggleFilter _priceFilter;
    [SerializeField] private PlayerPerTableFilter _playerPerTableFilter;
    [Header("Save Keys")]
    [SerializeField] private Constants.TablePlayerPrefsKeys keyForPriceFilter;
    [SerializeField] private Constants.TablePlayerPrefsKeys keyForPlayerPerTableFilter;

    public Action FilterChanged;

    private void OnEnable()
    {
        Init();
    }
    private void Init()
    {
        _priceFilter.Init(keyForPriceFilter.ToString());
        _priceFilter.FilterChanged = null;
        _priceFilter.FilterChanged = () => FilterChanged?.Invoke();

        _playerPerTableFilter.Init(keyForPlayerPerTableFilter.ToString());
        _playerPerTableFilter.FilterChanged = null;
        _playerPerTableFilter.FilterChanged = () => FilterChanged?.Invoke();
    }

    public List<RoomsListing.Room> UseFilters(List<RoomsListing.Room> reslt)
    {
        List<RoomsListing.Room> answer = new List<RoomsListing.Room>();

        // PriceFilter
        int lowMin = 0;
        int lowMax = 10;
        int mediumMin = 11;
        int mediumMax = 50;
        int hightMin = 51;
        int hightMax = int.MaxValue;

        List<string> priceFilterValue = _priceFilter.GetFilterValue();
        List<RoomsListing.Room> valueAfterPriceFilter = new List<RoomsListing.Room>();
        foreach (var priceFilter in priceFilterValue)
        {
            if (priceFilter == "low")
            {
                valueAfterPriceFilter.AddRange(GetTableWithInPrice(reslt, lowMin, lowMax));
            }
            if (priceFilter == "medium")
            {
                valueAfterPriceFilter.AddRange(GetTableWithInPrice(reslt, mediumMin, mediumMax));
            }
            if (priceFilter == "hight")
            {
                valueAfterPriceFilter.AddRange(GetTableWithInPrice(reslt, hightMin, hightMax));
            }
        }
        valueAfterPriceFilter.OrderBy(sortBy => sortBy.minBuyIn); // sorting
        answer.Clear();
        answer.AddRange(valueAfterPriceFilter);

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

        return answer;
    }

    private List<RoomsListing.Room> GetTableWithInPrice(List<RoomsListing.Room> tables, int minValue, int maxValue)
    {
        List<RoomsListing.Room> answer = new List<RoomsListing.Room>();
        foreach (var table in tables)
        {
            if (table.minBuyIn >= minValue && table.minBuyIn <= maxValue)
            {
                answer.Add(table);
            }
        }
        return answer;
    }

    private List<RoomsListing.Room> GetTableWithPlayersPerTable(List<RoomsListing.Room> tables, int players)
    {
        List<RoomsListing.Room> answer = new List<RoomsListing.Room>();
        foreach (var table in tables)
        {
            if (table.maxPlayers == players)
            {
                answer.Add(table);
            }
        }
        return answer;
    }
}
