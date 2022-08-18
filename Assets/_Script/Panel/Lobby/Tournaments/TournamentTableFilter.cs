using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TournamentTableFilter : MonoBehaviour
{
    [SerializeField] private PlayerPerTableFilter _playerPerTableFilter;
    [Space]
    [Header("Game type")]
    [SerializeField] private Toggle _allTypeToggle;
    [SerializeField] private Toggle _texasHoldemToggle;
    [SerializeField] private Toggle _omahaToggle;
    [SerializeField] private Toggle _plo5Toggle;
    [Header("Price value")]
    [SerializeField] private Toggle _allPriceToggle;
    [SerializeField] private Toggle _lowPriceToggle;
    [SerializeField] private Toggle _mediumPriceToggle;
    [SerializeField] private Toggle _hightPriceToggle;
    [SerializeField] private Toggle _freeRollToggle;

    public Action FilterChanged;

    private string _key = Constants.PlayerPrefsKeys.TournamentTableSettings;

    private void OnEnable()
    {
        Init();
    }
    private void OnDisable()
    {
        Save();
    }

    #region Save/Load
    private void Save()
    {
        // Game filter
        PlayerPrefs.SetInt($"{_key}.{_allTypeToggle.name}", BoolToInt(_allTypeToggle.isOn));
        PlayerPrefs.SetInt($"{_key}.{_texasHoldemToggle.name}", BoolToInt(_texasHoldemToggle.isOn));
        PlayerPrefs.SetInt($"{_key}.{_omahaToggle.name}", BoolToInt(_omahaToggle.isOn));
        PlayerPrefs.SetInt($"{_key}.{_plo5Toggle.name}", BoolToInt(_plo5Toggle.isOn));
        // Prive filter
        PlayerPrefs.SetInt($"{_key}.{_allPriceToggle.name}", BoolToInt(_allPriceToggle.isOn));
        PlayerPrefs.SetInt($"{_key}.{_lowPriceToggle.name}", BoolToInt(_lowPriceToggle.isOn));
        PlayerPrefs.SetInt($"{_key}.{_mediumPriceToggle.name}", BoolToInt(_mediumPriceToggle.isOn));
        PlayerPrefs.SetInt($"{_key}.{_hightPriceToggle.name}", BoolToInt(_hightPriceToggle.isOn));
        PlayerPrefs.SetInt($"{_key}.{_freeRollToggle.name}", BoolToInt(_freeRollToggle.isOn));
    }
    private void Load()
    {
        // Game filter
        _allTypeToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{_key}.{_allTypeToggle.name}", 1));
        _texasHoldemToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{_key}.{_texasHoldemToggle.name}", 1));
        _omahaToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{_key}.{_omahaToggle.name}", 1));
        _plo5Toggle.isOn = IntToBool(PlayerPrefs.GetInt($"{_key}.{_plo5Toggle.name}", 1));
        // Prive filter
        _allPriceToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{_key}.{_allPriceToggle.name}", 1));
        _lowPriceToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{_key}.{_lowPriceToggle.name}", 1));
        _mediumPriceToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{_key}.{_mediumPriceToggle.name}", 1));
        _hightPriceToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{_key}.{_hightPriceToggle.name}", 1));
        _freeRollToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{_key}.{_freeRollToggle.name}", 1));
    }
    private int BoolToInt(bool value)
    {
        if (value) return 1;
        else return 0;
    }
    private bool IntToBool(int value)
    {
        if (value > 0) return true;
        else return false;
    }
    #endregion

    private void Init()
    {
        Load();
        RemoveAllListeners();
        AddListeners();

        _playerPerTableFilter.FilterChanged = null;
        _playerPerTableFilter.FilterChanged = () => FilterChanged?.Invoke();
    }

    private void RemoveAllListeners()
    {
        // Game type
        _allTypeToggle.onValueChanged.RemoveAllListeners();
        _texasHoldemToggle.onValueChanged.RemoveAllListeners();
        _omahaToggle.onValueChanged.RemoveAllListeners();
        _plo5Toggle.onValueChanged.RemoveAllListeners();
        // Game price
        _allPriceToggle.onValueChanged.RemoveAllListeners();
        _lowPriceToggle.onValueChanged.RemoveAllListeners();
        _mediumPriceToggle.onValueChanged.RemoveAllListeners();
        _hightPriceToggle.onValueChanged.RemoveAllListeners();
        _freeRollToggle.onValueChanged.RemoveAllListeners();
    }
    private void AddListeners()
    {
        // Game type
        _allTypeToggle.onValueChanged.AddListener(AllTypeButtonClick);
        _texasHoldemToggle.onValueChanged.AddListener(FilterGameClick);
        _omahaToggle.onValueChanged.AddListener(FilterGameClick);
        _plo5Toggle.onValueChanged.AddListener(FilterGameClick);
        // Game price
        _allPriceToggle.onValueChanged.AddListener(AllPriceButtonClick);
        _lowPriceToggle.onValueChanged.AddListener(FilterPriceClick);
        _mediumPriceToggle.onValueChanged.AddListener(FilterPriceClick);
        _hightPriceToggle.onValueChanged.AddListener(FilterPriceClick);
        _freeRollToggle.onValueChanged.AddListener(FilterPriceClick);
    }

    public List<NormalTournamentDetails.NormalTournamentData> UseFilter(List<NormalTournamentDetails.NormalTournamentData> tableData)
    {
        List<NormalTournamentDetails.NormalTournamentData> answer = new List<NormalTournamentDetails.NormalTournamentData>();

        // game GameType filter
        List<string> gameFilter = new List<string>();
        if (_allTypeToggle.isOn)
        {
            gameFilter.Add("texas");
            gameFilter.Add("omaha");
            gameFilter.Add("PLO5");
        }
        else
        {
            if (_texasHoldemToggle.isOn) gameFilter.Add("texas");
            if (_omahaToggle.isOn) gameFilter.Add("omaha");
            if (_plo5Toggle.isOn) gameFilter.Add("PLO5");
        }
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
        /*
         0 - 10 $ - low leaves
        10 - 50 $ - medium level
        50 - endless  - Hight level 
        freeroll is freeroll
         */

        // playerfilter
        //List<int> playerFilter = _playerPerTableFilter.GetFilterValue();


        return answer;
    }


    #region Buttons and Toggls
    private void AllTypeButtonClick(bool value)
    {
        if (value)
        {
            RemoveAllListeners();

            _texasHoldemToggle.isOn = false;
            _omahaToggle.isOn = false;
            _plo5Toggle.isOn = false;

            FilterChanged?.Invoke();
            AddListeners();
        }
        else
        {
            if (_texasHoldemToggle.isOn == false
             && _omahaToggle.isOn == false
             && _plo5Toggle.isOn == false)
            {
                _allTypeToggle.isOn = true;
            }
            return;
        }
    }
    private void FilterGameClick(bool value)
    {
        if (_texasHoldemToggle.isOn == false
         && _omahaToggle.isOn == false
         && _plo5Toggle.isOn == false)
        {
            _allTypeToggle.isOn = true;
        }
        else if (_texasHoldemToggle.isOn == true
         && _omahaToggle.isOn == true
         && _plo5Toggle.isOn == true)
        {
            _allTypeToggle.isOn = true;
        }
        else
        {
            _allTypeToggle.isOn = false;
            FilterChanged?.Invoke();
        }
    }

    private void AllPriceButtonClick(bool value)
    {
        if (value)
        {
            RemoveAllListeners();

            _lowPriceToggle.isOn = false;
            _mediumPriceToggle.isOn = false;
            _hightPriceToggle.isOn = false;
            _freeRollToggle.isOn = false;

            FilterChanged?.Invoke();
            AddListeners();
        }
        else
        {
            if (_lowPriceToggle.isOn == false
             && _mediumPriceToggle.isOn == false
             && _hightPriceToggle.isOn == false
             && _freeRollToggle.isOn == false)
            {
                _allPriceToggle.isOn = true;
            }
            return;
        }
    }
    private void FilterPriceClick(bool arg0)
    {
        if (_lowPriceToggle.isOn == false
         && _mediumPriceToggle.isOn == false
         && _hightPriceToggle.isOn == false
         && _freeRollToggle.isOn == false)
        {
            _allPriceToggle.isOn = true;
        }
        else if (_lowPriceToggle.isOn == true
         && _mediumPriceToggle.isOn == true
         && _hightPriceToggle.isOn == true
         && _freeRollToggle.isOn == true)
        {
            _allPriceToggle.isOn = true;
        }
        else
        {
            _allPriceToggle.isOn = false;
            FilterChanged?.Invoke();
        }
    }
    #endregion
}
