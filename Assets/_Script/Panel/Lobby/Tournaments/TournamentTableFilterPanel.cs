using System;
using System.Collections.Generic;
using System.Linq;
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
        _gameFilter.Init(Constants.TablePlayerPrefsKeys.TournamentTableSettingsGameFilter.ToString());
        _gameFilter.FilterChanged = null;
        _gameFilter.FilterChanged = () => FilterChanged?.Invoke();

        _priceFilter.Init(Constants.TablePlayerPrefsKeys.TournamentTableSettingsPriceFilter.ToString());
        _priceFilter.FilterChanged = null;
        _priceFilter.FilterChanged = () => FilterChanged?.Invoke();

        _playerPerTableFilter.Init(Constants.TablePlayerPrefsKeys.TournamentTableSettingsPlayerPerTableFilter.ToString());
        _playerPerTableFilter.FilterChanged = null;
        _playerPerTableFilter.FilterChanged = () => FilterChanged?.Invoke();
    }

    public List<NormalTournamentDetails.NormalTournamentData> UseFilter(List<NormalTournamentDetails.NormalTournamentData> tournamentData)
    {
        if (_gameFilter.IsAllOn() && _priceFilter.IsAllOn() && _playerPerTableFilter.IsAllOn())
        {
            return tournamentData;
        }
        
        List<NormalTournamentDetails.NormalTournamentData> answer = new List<NormalTournamentDetails.NormalTournamentData>();

        //--- Game GameType filter ---//
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
        foreach (var tournament in tournamentData)
        {
            foreach (var game in gameFilter)
            {
                if (tournament.pokerGameType == game)
                {
                    answer.Add(tournament);
                }
            }
        }
        //-----------------------------------------

        //--- Price filter ---//
        int lowMin = 0;
        int lowMax = 10;
        int mediumMin = 11;
        int mediumMax = 50;
        int hightMin = 51;
        int hightMax = int.MaxValue;

        List<string> priceFilterValue = _priceFilter.GetFilterValue();
        List<NormalTournamentDetails.NormalTournamentData> afterPriceFilter = new List<NormalTournamentDetails.NormalTournamentData>();
        foreach (var priceFilter in priceFilterValue)
        {
            if (priceFilter == "low")
            {
                afterPriceFilter.AddRange(GetTournaryWithInPrice(answer, lowMin, lowMax));
            }
            if (priceFilter == "medium")
            {
                afterPriceFilter.AddRange(GetTournaryWithInPrice(answer, mediumMin, mediumMax));
            }
            if (priceFilter == "hight")
            {
                afterPriceFilter.AddRange(GetTournaryWithInPrice(answer, hightMin, hightMax));
            }
            if (priceFilter == "freeroll")
            {
                afterPriceFilter.AddRange(GetIsFreeRoll(answer));
            }
        }
        afterPriceFilter.OrderBy(sortBy => sortBy.buyIn); // sorting by buyIn
        answer.Clear();
        answer.AddRange(afterPriceFilter);
        //-----------------------------------------

        //--- Max players per table filter ---//
        afterPriceFilter.Clear();
        List<string> playerPerTableFilterValue = _playerPerTableFilter.GetFilterValue();
        foreach (var playersPerTable in playerPerTableFilterValue)
        {
            if (playersPerTable == "all")
            {
                afterPriceFilter.AddRange(answer);
            }
            if (playersPerTable == "2")
            {
                afterPriceFilter.AddRange(GetTournaryWithPlayersPerTable(answer, 2));
            }
            if (playersPerTable == "6")
            {
                afterPriceFilter.AddRange(GetTournaryWithPlayersPerTable(answer, 6));
            }
            if (playersPerTable == "8")
            {
                afterPriceFilter.AddRange(GetTournaryWithPlayersPerTable(answer, 8));
            }
            if (playersPerTable == "9")
            {
                afterPriceFilter.AddRange(GetTournaryWithPlayersPerTable(answer, 9));
            }
        }
        afterPriceFilter.OrderBy(sortBy => sortBy.maxPlayersPerTable); // sorting
        answer.Clear();
        answer.AddRange(afterPriceFilter);
        //------------------------------------------

        // This is a simple solution to the problem. I don't understand why duplicates are being created.
        answer = RemoveDuplicate(answer);

        return answer;
    }

    private List<NormalTournamentDetails.NormalTournamentData> RemoveDuplicate(List<NormalTournamentDetails.NormalTournamentData> data)
    {
        List<NormalTournamentDetails.NormalTournamentData> answer = new List<NormalTournamentDetails.NormalTournamentData>();
        foreach (var item in data)
        {
            if (!answer.Contains(item))
            {
                answer.Add(item);
            }
        }
        return answer;
    }

    private List<NormalTournamentDetails.NormalTournamentData> GetIsFreeRoll(List<NormalTournamentDetails.NormalTournamentData> tournarys)
    {
        List<NormalTournamentDetails.NormalTournamentData> answer = new List<NormalTournamentDetails.NormalTournamentData>();
        foreach (var tournary in tournarys)
        {
            if (tournary.isFreeRoll && !answer.Contains(tournary))
            {
                answer.Add(tournary);
            }
        }
        return answer;
    }

    private int ParsingByinValue(string value) 
    {
        int answer;
        string[] strArr = value.Split('+');
        answer =  int.Parse(strArr[0]) + int.Parse(strArr[1]);
        return answer;
    }


    private List<NormalTournamentDetails.NormalTournamentData> GetTournaryWithInPrice(List<NormalTournamentDetails.NormalTournamentData> tournarys, int minValue, int maxValue)
    {
        List<NormalTournamentDetails.NormalTournamentData> answer = new List<NormalTournamentDetails.NormalTournamentData>();
        foreach (var tournary in tournarys)
        {
            int byInValue = ParsingByinValue(tournary.buyIn);
            if (byInValue >= minValue && byInValue <= maxValue
                && !answer.Contains(tournary))
            {
                answer.Add(tournary);
            }
        }
        return answer;
    }

    private List<NormalTournamentDetails.NormalTournamentData> GetTournaryWithPlayersPerTable(List<NormalTournamentDetails.NormalTournamentData> tournarys, int players)
    {
        List<NormalTournamentDetails.NormalTournamentData> answer = new List<NormalTournamentDetails.NormalTournamentData>();
        foreach (var tournary in tournarys)
        {
            if (tournary.maxPlayersPerTable == players && !answer.Contains(tournary))
            {
                answer.Add(tournary);
            }
        }
        return answer;
    }
}
