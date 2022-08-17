using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TournamentTableFilter : MonoBehaviour
{
    [SerializeField] private PlayerPerTableFilter _playerPerTableFilter;
    [Space]
    [Header("Game type")]
    [SerializeField] private Button _allTypeButton;
    [SerializeField] private Toggle _texasHoldemToggle;
    [SerializeField] private Toggle _omahaToggle;
    [SerializeField] private Toggle _plo5Toggle;
    [Header("Price value")]
    [SerializeField] private Button _allPriceButton;
    [SerializeField] private Toggle _lowPriceToggle;
    [SerializeField] private Toggle _mediumPriceToggle;
    [SerializeField] private Toggle _hightPriceToggle;
    [SerializeField] private Toggle _freeRollToggle;

    public Action FilterChanged;

    private string keyTexasHoldemToggle = "TournamentTableSettings.texasHoldemToggle";
    private string keyOmahaToggle       = "TournamentTableSettings.omahaToggle";
    private string keyPlo5Toggle        = "TournamentTableSettings.plo5Toggle";
    private string keyLowPriceToggle    = "TournamentTableSettings.lowPriceToggle";
    private string keyMediumPriceToggle = "TournamentTableSettings.mediumPriceToggle";
    private string keyHightPriceToggle  = "TournamentTableSettings.hightPriceToggle";
    private string keyFreeRollToggle    = "TournamentTableSettings.freeRollToggle";

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
        PlayerPrefs.SetInt(keyTexasHoldemToggle, BoolToInt(_texasHoldemToggle.isOn));
        PlayerPrefs.SetInt(keyOmahaToggle, BoolToInt(_omahaToggle.isOn));
        PlayerPrefs.SetInt(keyPlo5Toggle, BoolToInt(_plo5Toggle.isOn));
        PlayerPrefs.SetInt(keyLowPriceToggle, BoolToInt(_lowPriceToggle.isOn));
        PlayerPrefs.SetInt(keyMediumPriceToggle, BoolToInt(_mediumPriceToggle.isOn));
        PlayerPrefs.SetInt(keyHightPriceToggle, BoolToInt(_hightPriceToggle.isOn));
        PlayerPrefs.SetInt(keyFreeRollToggle, BoolToInt(_freeRollToggle.isOn));
    }
    private void Load() 
    {
        _texasHoldemToggle.isOn = IntToBool(PlayerPrefs.GetInt(keyTexasHoldemToggle,1));
        _omahaToggle.isOn = IntToBool(PlayerPrefs.GetInt(keyOmahaToggle, 1));
        _plo5Toggle.isOn = IntToBool(PlayerPrefs.GetInt(keyPlo5Toggle, 1));
        _lowPriceToggle.isOn = IntToBool(PlayerPrefs.GetInt(keyLowPriceToggle, 1));
        _mediumPriceToggle.isOn = IntToBool(PlayerPrefs.GetInt(keyMediumPriceToggle, 1));
        _hightPriceToggle.isOn = IntToBool(PlayerPrefs.GetInt(keyHightPriceToggle, 1));
        _freeRollToggle.isOn = IntToBool(PlayerPrefs.GetInt(keyFreeRollToggle, 1));
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
        ButtonsAndTogglesRemoveAllListeners();
        ButtonsAndTogglesAddListeners();

        _playerPerTableFilter.FilterChanged = null;
        _playerPerTableFilter.FilterChanged = () => FilterChanged?.Invoke();

        // select all games if none is selected. Required to get data
        if (_texasHoldemToggle.isOn == false
            && _omahaToggle.isOn == false
            && _plo5Toggle.isOn == false)
        {
            AllTypeButtonClick();
        }
    }

    private void ButtonsAndTogglesRemoveAllListeners()
    {
        // Game type
        _allTypeButton.onClick.RemoveAllListeners();
        _texasHoldemToggle.onValueChanged.RemoveAllListeners();
        _omahaToggle.onValueChanged.RemoveAllListeners();
        _plo5Toggle.onValueChanged.RemoveAllListeners();
        // Game price
        _allPriceButton.onClick.RemoveAllListeners();
        _lowPriceToggle.onValueChanged.RemoveAllListeners();
        _mediumPriceToggle.onValueChanged.RemoveAllListeners();
        _hightPriceToggle.onValueChanged.RemoveAllListeners();
        _freeRollToggle.onValueChanged.RemoveAllListeners();
    }
    private void ButtonsAndTogglesAddListeners()
    {
        // Game type
        _allTypeButton.onClick.AddListener(AllTypeButtonClick);
        _texasHoldemToggle.onValueChanged.AddListener(FilterGameClick);
        _omahaToggle.onValueChanged.AddListener(FilterGameClick);
        _plo5Toggle.onValueChanged.AddListener(FilterGameClick);
        // Game price
        _allPriceButton.onClick.AddListener(AllPriceButtonClick);
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
        if (_texasHoldemToggle.isOn) gameFilter.Add("texas");
        if (_omahaToggle.isOn) gameFilter.Add("omaha");
        if (_plo5Toggle.isOn) gameFilter.Add("PLO5");
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
    private void AllTypeButtonClick()
    {
        ButtonsAndTogglesRemoveAllListeners();

        _texasHoldemToggle.isOn = true;
        _omahaToggle.isOn = true;
        _plo5Toggle.isOn = true;

        FilterChanged?.Invoke();
        ButtonsAndTogglesAddListeners();
    }
    private void FilterGameClick(bool arg0)
    {
        if (_texasHoldemToggle.isOn == false
         && _omahaToggle.isOn == false
         && _plo5Toggle.isOn == false)
        {
            AllTypeButtonClick();
        }
        else
        {
            FilterChanged?.Invoke();
        }
    }

    private void AllPriceButtonClick()
    {
        ButtonsAndTogglesRemoveAllListeners();

        _lowPriceToggle.isOn = true;
        _mediumPriceToggle.isOn = true;
        _hightPriceToggle.isOn = true;
        _freeRollToggle.isOn = true;

        FilterChanged?.Invoke();
        ButtonsAndTogglesAddListeners();
    }
    private void FilterPriceClick(bool arg0)
    {
        if (_lowPriceToggle.isOn == false
         && _mediumPriceToggle.isOn == false
         && _hightPriceToggle.isOn == false
         && _freeRollToggle.isOn == false)
        {
            AllPriceButtonClick();
        }
        else
        {
            FilterChanged?.Invoke();
        }
    }
    #endregion
}
