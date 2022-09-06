using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllAndOtherToggleFilter : MonoBehaviour
{
    [SerializeField] private Toggle _allToggle;
    [SerializeField] private Toggle[] _otherToggle;

    public Action FilterChanged;

    private string _key = "";

    public bool IsAllOn() => _allToggle.isOn;

    private void OnDisable()
    {
        Save();
    }

    public void Init(string saveLoadKey)
    {
        _key = saveLoadKey;
        Load();
        RemoveAllListeners();
        AddListeners();
    }

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

    private void RemoveAllListeners()
    {
        _allToggle.onValueChanged.RemoveAllListeners();
        foreach (Toggle item in _otherToggle)
        {
            item.onValueChanged.RemoveAllListeners();
        }
    }
    private void AddListeners()
    {
        _allToggle.onValueChanged.AddListener(AllTypeButtonClick);
        foreach (Toggle item in _otherToggle)
        {
            item.onValueChanged.AddListener(FilterButtonClick);
        }
    }

    public List<string> GetFilterValue()
    {
        List<string> answer = new List<string>();
        if (_allToggle.isOn)
        {
            foreach (Toggle item in _otherToggle)
            {
                TextMeshProUGUI textUGUI = item.GetComponentInChildren<TextMeshProUGUI>();
                if (textUGUI)
                {
                    answer.Add(textUGUI.text);
                }
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

    private void AllTypeButtonClick(bool value)
    {
        if (value)
        {
            RemoveAllListeners();

            foreach (Toggle item in _otherToggle)
            {
                item.isOn = false;
            }

            FilterChanged?.Invoke();
            AddListeners();
        }
        else
        {
            if (AllOtherToggleIsFalse())
            {
                _allToggle.isOn = true;
            }
        }
    }
    private void FilterButtonClick(bool value)
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
            FilterChanged?.Invoke();
        }
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
