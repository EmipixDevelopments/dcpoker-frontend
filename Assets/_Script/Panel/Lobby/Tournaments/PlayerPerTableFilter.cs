using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerPerTableFilter : MonoBehaviour
{
    [Header("Lable Panel")]
    [SerializeField] private GameObject      _lablePanel;
    [SerializeField] private Button          _labelPanelButton;
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private TextMeshProUGUI _selectedFilterText;
    [SerializeField] private GameObject      _arrowImage;
    [Header("Player Filter Panel")]
    [SerializeField] private GameObject _playerFilterPanel;
    [SerializeField] private Button     _allButton;
    [SerializeField] private Toggle     _twoPlayerToggle;
    [SerializeField] private Toggle     _sixPlayerToggle;
    [SerializeField] private Toggle     _eightPlayerToggle;
    [SerializeField] private Toggle     _ninePlayerToggle;

    public Action FilterChanged;

    private bool _isShowFilter = false;


    private string keyTwoPlayerToggle = "TournamentTableSettings.twoPlayerToggle";
    private string keySixPlayerToggle = "TournamentTableSettings.sixPlayerToggle";
    private string keyEightPlayerToggle = "TournamentTableSettings.eightPlayerToggle";
    private string keyNinePlayerToggle = "TournamentTableSettings.ninePlayerToggle";


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
        PlayerPrefs.SetInt(keyTwoPlayerToggle, BoolToInt(_twoPlayerToggle.isOn));
        PlayerPrefs.SetInt(keySixPlayerToggle, BoolToInt(_sixPlayerToggle.isOn));
        PlayerPrefs.SetInt(keyEightPlayerToggle, BoolToInt(_eightPlayerToggle.isOn));
        PlayerPrefs.SetInt(keyNinePlayerToggle, BoolToInt(_ninePlayerToggle.isOn));
    }
    private void Load()
    {
        _twoPlayerToggle.isOn = IntToBool(PlayerPrefs.GetInt(keyTwoPlayerToggle, 1));
        _sixPlayerToggle.isOn = IntToBool(PlayerPrefs.GetInt(keySixPlayerToggle, 1));
        _eightPlayerToggle.isOn = IntToBool(PlayerPrefs.GetInt(keyEightPlayerToggle, 1));
        _ninePlayerToggle.isOn = IntToBool(PlayerPrefs.GetInt(keyNinePlayerToggle, 1));

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
        if (_twoPlayerToggle.isOn) result.Add(2);
        if (_sixPlayerToggle.isOn) result.Add(6);
        if (_eightPlayerToggle.isOn) result.Add(8);
        if (_ninePlayerToggle.isOn) result.Add(9);
        return result;
    }

    private void Init()
    {
        Load();
        ButtonsAndTogglesRemoveAllListeners();
        ButtonsAndTogglesAddListeners();
        UpdateInfoText();
    }

    private void ButtonsAndTogglesAddListeners()
    {
        _labelPanelButton.onClick.AddListener(LabelPanelClick);
        _allButton.onClick.AddListener(AllFilterClick);
        _twoPlayerToggle.onValueChanged.AddListener(FilterNumberClick);
        _sixPlayerToggle.onValueChanged.AddListener(FilterNumberClick);
        _eightPlayerToggle.onValueChanged.AddListener(FilterNumberClick);
        _ninePlayerToggle.onValueChanged.AddListener(FilterNumberClick);
    }

    private void ButtonsAndTogglesRemoveAllListeners()
    {
        _labelPanelButton.onClick.RemoveAllListeners();
        _allButton.onClick.RemoveAllListeners();
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
    private void AllFilterClick()
    {
        ButtonsAndTogglesRemoveAllListeners();

        _twoPlayerToggle.isOn = true;
        _sixPlayerToggle.isOn = true;
        _eightPlayerToggle.isOn = true;
        _ninePlayerToggle.isOn = true;

        UpdateInfoText();
        FilterChanged?.Invoke();
        ButtonsAndTogglesAddListeners();
    }

    private void FilterNumberClick(bool arg0)
    {
        if (_twoPlayerToggle.isOn == false
         && _sixPlayerToggle.isOn == false
         && _eightPlayerToggle.isOn == false
         && _ninePlayerToggle.isOn == false)
        {
            AllFilterClick();
        }
        else
        {
            UpdateInfoText();
            FilterChanged?.Invoke();
        }
    }
    #endregion

    private void UpdateInfoText()
    {
        string selectedFilter = "";
        if (_twoPlayerToggle.isOn) selectedFilter += " "+2;
        if (_sixPlayerToggle.isOn) selectedFilter += " " + 6;
        if (_eightPlayerToggle.isOn) selectedFilter += " " + 8;
        if (_ninePlayerToggle.isOn) selectedFilter += " " + 9;

        if (_twoPlayerToggle.isOn
         && _sixPlayerToggle.isOn
         && _eightPlayerToggle.isOn
         && _ninePlayerToggle.isOn)
        {
            selectedFilter = "ALL";
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