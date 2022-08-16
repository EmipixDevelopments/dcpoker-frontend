using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TournamentTableFilter : MonoBehaviour
{
    [Header("Game type")]
    [SerializeField] Button _allTypeButton;
    [SerializeField] Toggle _texasHoldemToggle;
    [SerializeField] Toggle _omahaToggle;
    [SerializeField] Toggle _plo5Toggle;
    [Header("Price value")]
    [SerializeField] Button _allPriceButton;
    [SerializeField] Toggle _lowPriceToggle;
    [SerializeField] Toggle _mediumPriceToggle;
    [SerializeField] Toggle _hightPriceToggle;
    [SerializeField] Toggle _freeRollToggle;

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

        _allTypeButton.onClick.RemoveAllListeners();
        _texasHoldemToggle.onValueChanged.RemoveAllListeners();
        _omahaToggle.onValueChanged.RemoveAllListeners();
        _plo5Toggle.onValueChanged.RemoveAllListeners();
        _allPriceButton.onClick.RemoveAllListeners();
        _lowPriceToggle.onValueChanged.RemoveAllListeners();
        _mediumPriceToggle.onValueChanged.RemoveAllListeners();
        _hightPriceToggle.onValueChanged.RemoveAllListeners();

        _allTypeButton.onClick.AddListener(AllTypeButton);
        _texasHoldemToggle.onValueChanged.AddListener(TexasHoldemToggle);
        _omahaToggle.onValueChanged.AddListener(OmahaToggle);
        _plo5Toggle.onValueChanged.AddListener(Plo5Toggle);
        _allPriceButton.onClick.AddListener(AllPriceButton);
        _lowPriceToggle.onValueChanged.AddListener(LowPriceToggle);
        _mediumPriceToggle.onValueChanged.AddListener(MediumPriceToggle);
        _hightPriceToggle.onValueChanged.AddListener(HightPriceToggle);
        _freeRollToggle.onValueChanged.AddListener(FreeRollToggle);

        // select all games if none is selected. Required to get data
        if (_texasHoldemToggle.isOn == false
            && _omahaToggle.isOn == false
            && _plo5Toggle.isOn == false)
        {
            AllTypeButton();
        }
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


        return answer;
    }


    #region Buttons and Toggls
    private void AllTypeButton()
    {
        if (_texasHoldemToggle.isOn == true
         && _omahaToggle.isOn == true
         && _plo5Toggle.isOn == true)
        {
            _texasHoldemToggle.isOn = false;
            _omahaToggle.isOn = false;
            _plo5Toggle.isOn = false;
        }
        else
        {
            _texasHoldemToggle.isOn = true;
            _omahaToggle.isOn = true;
            _plo5Toggle.isOn = true;
        }
    }
    private void TexasHoldemToggle(bool value)
    {
        FilterChanged?.Invoke();
    }
    private void OmahaToggle(bool arg0)
    {
        FilterChanged?.Invoke();
    }
    private void Plo5Toggle(bool arg0)
    {
        FilterChanged?.Invoke();
    }
    private void AllPriceButton()
    {
        if (_lowPriceToggle.isOn == true
         && _mediumPriceToggle.isOn == true
         && _hightPriceToggle.isOn == true
         && _freeRollToggle.isOn == true)
        {
            _lowPriceToggle.isOn = false;
            _mediumPriceToggle.isOn = false;
            _hightPriceToggle.isOn = false;
            _freeRollToggle.isOn = false;
        }
        else
        {
            _lowPriceToggle.isOn = true;
            _mediumPriceToggle.isOn = true;
            _hightPriceToggle.isOn = true;
            _freeRollToggle.isOn = true;

        }
    }
    private void LowPriceToggle(bool arg0)
    {
        FilterChanged?.Invoke();
    }
    private void MediumPriceToggle(bool arg0)
    {
        FilterChanged?.Invoke();
    }
    private void HightPriceToggle(bool arg0)
    {
        FilterChanged?.Invoke();
    }
    private void FreeRollToggle(bool arg0)
    {
        FilterChanged?.Invoke();
    }
    #endregion

}
