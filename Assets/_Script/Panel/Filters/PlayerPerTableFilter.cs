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
    [SerializeField] private Toggle _allToggle;
    [SerializeField] private Toggle[] _otherToggle;

    public Action FilterChanged;

    private bool _isShowFilter = false;

    private string _key = "";

    private void OnDisable()
    {
        Save();
    }

    public bool IsAllOn() => _allToggle.isOn;

    #region Save/Load
    private void Save()
    {
        PlayerPrefs.SetInt($"{_key}.{_allToggle.name}", BoolToInt(_allToggle.isOn));
        foreach (Toggle item in _otherToggle)
        {
            PlayerPrefs.SetInt($"{_key}.{item.name}", BoolToInt(item.isOn));
        }
    }
    private void Load()
    {
        _allToggle.isOn = IntToBool(PlayerPrefs.GetInt($"{_key}.{_allToggle.name}", 1));
        foreach (Toggle item in _otherToggle)
        {
            item.isOn = IntToBool(PlayerPrefs.GetInt($"{_key}.{item.name}", 0));
        }
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

    public List<string> GetFilterValue()
    {
        List<string> answer = new List<string>();
        if (_allToggle.isOn)
        {
            // if need sent all avaliable value
            //foreach (Toggle item in _otherToggle)
            //{
            //    TextMeshProUGUI textUGUI = item.GetComponentInChildren<TextMeshProUGUI>();
            //    if (textUGUI)
            //    {
            //        answer.Add(textUGUI.text);
            //    }
            //}

            // send "all"
            TextMeshProUGUI textUGUI = _allToggle.GetComponentInChildren<TextMeshProUGUI>();
            if (textUGUI)
            {
                answer.Add(textUGUI.text);
            }
        }
        else
        {
            foreach (Toggle item in _otherToggle)
            {
                TextMeshProUGUI textUGUI = item.GetComponentInChildren<TextMeshProUGUI>();
                if (textUGUI)
                {
                    if (item.isOn) answer.Add(textUGUI.text);
                }
            }
        }

        return answer;
    }

    public void Init(string saveLoadKey)
    {
        _key = saveLoadKey;
        Load();
        RemoveListeners();
        AddListeners();
        UpdateInfoText();
    }

    private void AddListeners()
    {
        _labelPanelButton.onClick.AddListener(LabelPanelClick);
        _allToggle.onValueChanged.AddListener(AllButtonClick);
        foreach (Toggle item in _otherToggle)
        {
            item.onValueChanged.AddListener(NumberClick);
        }
    }

    private void RemoveListeners()
    {
        _labelPanelButton.onClick.RemoveAllListeners();
        _allToggle.onValueChanged.RemoveAllListeners();
        foreach (Toggle item in _otherToggle)
        {
            item.onValueChanged.RemoveAllListeners();
        }
    }

    #region Buttons and Toggles
    private void LabelPanelClick()
    {
        _isShowFilter = !_isShowFilter;

        if (_isShowFilter) _playerFilterPanel.SetActive(true);
        else _playerFilterPanel.SetActive(false);
    }
    private void AllButtonClick(bool value)
    {
        if (value)
        {
            RemoveListeners();

            foreach (Toggle item in _otherToggle)
            {
                item.isOn = false;
            }

            UpdateInfoText();
            FilterChanged?.Invoke();
            AddListeners();
        }
        else
        {
            if (AllOtherToggleIsFalse())
            {
                _allToggle.isOn = true;
            }
            return;
        }
    }

    private void NumberClick(bool arg0)
    {
        if (AllOtherToggleIsFalse())
        {
            _allToggle.isOn = true;
        }
        else if (AllOtherToggleIsTrue())
        {
            _allToggle.isOn = true;
        }
        else
        {
            _allToggle.isOn = false;
            UpdateInfoText();
            FilterChanged?.Invoke();
        }
    }
    #endregion

    private void UpdateInfoText()
    {
        string selectedFilter = "";
        if (_allToggle.isOn) selectedFilter = "ALL";
        else
        {
            foreach (Toggle item in _otherToggle)
            {
                TextMeshProUGUI textUGUI = item.GetComponentInChildren<TextMeshProUGUI>();
                if (textUGUI)
                {
                    if (item.isOn) selectedFilter += $" {textUGUI.text}";
                }
            }
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
    private bool AllOtherToggleIsTrue()
    {
        foreach (Toggle item in _otherToggle)
        {
            if (item.isOn == false)
            {
                return false;
            }
        }
        return true;
    }
    private bool AllOtherToggleIsFalse()
    {
        foreach (Toggle item in _otherToggle)
        {
            if (item.isOn == true)
            {
                return false;
            }
        }
        return true;
    }
}