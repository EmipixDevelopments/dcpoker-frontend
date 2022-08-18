using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPerTableFilter : MonoBehaviour
{
    [Header("Lable Panel")]
    [SerializeField] private GameObject _lablePanel;
    [SerializeField] private Button _labelPanelButton;
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private TextMeshProUGUI _selectedFilterText;
    [SerializeField] private GameObject _arrowImage;
    [Header("Player Filter Panel")]
    [SerializeField] private GameObject _playerFilterPanel;
    [SerializeField] private Toggle _allPlayerToggle;
    [SerializeField] private Toggle _twoPlayerToggle;
    [SerializeField] private Toggle _sixPlayerToggle;
    [SerializeField] private Toggle _eightPlayerToggle;
    [SerializeField] private Toggle _ninePlayerToggle;

    public Action FilterChanged;

    private bool _isShowFilter = false;

    private string key = "TournamentTableSettings.PlayerPerTableFilter";

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
        PlayerPrefs.SetInt($"{key}.{_allPlayerToggle.name}", BoolToInt(_allPlayerToggle.isOn));
        PlayerPrefs.SetInt($"{key}.{_twoPlayerToggle.name}", BoolToInt(_twoPlayerToggle.isOn));
        PlayerPrefs.SetInt($"{key}.{_sixPlayerToggle.name}", BoolToInt(_sixPlayerToggle.isOn));
        PlayerPrefs.SetInt($"{key}.{_eightPlayerToggle.name}", BoolToInt(_eightPlayerToggle.isOn));
        PlayerPrefs.SetInt($"{key}.{_ninePlayerToggle.name}", BoolToInt(_ninePlayerToggle.isOn));
    }
    private void Load()
    {
        _allPlayerToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{key}.{_allPlayerToggle.name}", 1));
        _twoPlayerToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{key}.{_twoPlayerToggle.name}", 1));
        _sixPlayerToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{key}.{_sixPlayerToggle.name}", 1));
        _eightPlayerToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{key}.{_eightPlayerToggle.name}", 1));
        _ninePlayerToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{key}.{_ninePlayerToggle.name}", 1));
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

    private void Start()
    {
        Init();
    }

    public List<int> GetFilterValue()
    {
        List<int> result = new List<int>();
        if (_allPlayerToggle.isOn)
        {
            result.Add(2);
            result.Add(6);
            result.Add(8);
            result.Add(9);
        }
        else
        {
            if (_twoPlayerToggle.isOn) result.Add(2);
            if (_sixPlayerToggle.isOn) result.Add(6);
            if (_eightPlayerToggle.isOn) result.Add(8);
            if (_ninePlayerToggle.isOn) result.Add(9);
        }
        return result;
    }

    private void Init()
    {
        Load();
        RemoveListeners();
        AddListeners();
        UpdateInfoText();
    }

    private void AddListeners()
    {
        _labelPanelButton.onClick.AddListener(LabelPanelClick);
        _allPlayerToggle.onValueChanged.AddListener(AllFilterClick);
        _twoPlayerToggle.onValueChanged.AddListener(FilterNumberClick);
        _sixPlayerToggle.onValueChanged.AddListener(FilterNumberClick);
        _eightPlayerToggle.onValueChanged.AddListener(FilterNumberClick);
        _ninePlayerToggle.onValueChanged.AddListener(FilterNumberClick);
    }

    private void RemoveListeners()
    {
        _labelPanelButton.onClick.RemoveAllListeners();
        _allPlayerToggle.onValueChanged.RemoveAllListeners();
        _twoPlayerToggle.onValueChanged.RemoveAllListeners();
        _sixPlayerToggle.onValueChanged.RemoveAllListeners();
        _eightPlayerToggle.onValueChanged.RemoveAllListeners();
        _ninePlayerToggle.onValueChanged.RemoveAllListeners();
    }

    #region Buttons and Toggles
    private void LabelPanelClick()
    {
        _isShowFilter = !_isShowFilter;

        if (_isShowFilter) _playerFilterPanel.SetActive(true);
        else _playerFilterPanel.SetActive(false);
    }
    private void AllFilterClick(bool varlue)
    {
        if (varlue)
        {
            RemoveListeners();

            _twoPlayerToggle.isOn = false;
            _sixPlayerToggle.isOn = false;
            _eightPlayerToggle.isOn = false;
            _ninePlayerToggle.isOn = false;

            UpdateInfoText();
            FilterChanged?.Invoke();
            AddListeners();
        }
        else
        {
            if (_twoPlayerToggle.isOn == false
             && _sixPlayerToggle.isOn == false
             && _eightPlayerToggle.isOn == false
             && _ninePlayerToggle.isOn == false)
            {
                _allPlayerToggle.isOn = true;
            }
            return;
        }
    }

    private void FilterNumberClick(bool arg0)
    {
        if (_twoPlayerToggle.isOn == false
         && _sixPlayerToggle.isOn == false
         && _eightPlayerToggle.isOn == false
         && _ninePlayerToggle.isOn == false)
        {
            _allPlayerToggle.isOn = true;
        }
        else if (_twoPlayerToggle.isOn == true
         && _sixPlayerToggle.isOn == true
         && _eightPlayerToggle.isOn == true
         && _ninePlayerToggle.isOn == true)
        {
            _allPlayerToggle.isOn = true;
        }
        else
        {
            _allPlayerToggle.isOn = false;
            UpdateInfoText();
            FilterChanged?.Invoke();
        }
    }
    #endregion

    private void UpdateInfoText()
    {
        string selectedFilter = "";
        if (_allPlayerToggle.isOn) selectedFilter = "ALL";
        else
        {
            if (_twoPlayerToggle.isOn) selectedFilter += " " + 2;
            if (_sixPlayerToggle.isOn) selectedFilter += " " + 6;
            if (_eightPlayerToggle.isOn) selectedFilter += " " + 8;
            if (_ninePlayerToggle.isOn) selectedFilter += " " + 9;
        }
        _selectedFilterText.text = selectedFilter;

        RefreshAllElementsLabelPanel();
    }

    private void RefreshAllElementsLabelPanel()
    {
        _labelText.gameObject.SetActive(false);
        _selectedFilterText.gameObject.SetActive(false);
        _arrowImage.SetActive(false);

        _labelText.gameObject.SetActive(true);
        _selectedFilterText.gameObject.SetActive(true);
        _arrowImage.SetActive(true);
    }
}