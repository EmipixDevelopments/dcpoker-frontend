using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTexasHoldemFilterPanel : MonoBehaviour
{
    [SerializeField] private AllAndOtherToggleFilter _priceFilter;
    [SerializeField] private PlayerPerTableFilter _playerPerTableFilter;

    public Action FilterChanged;

    private void OnEnable()
    {
        Init();
    }
    private void Init()
    {
        _priceFilter.Init(Constants.PlayerPrefsKeys.TaxesHoldemTableSettingsPriceFilter);
        _priceFilter.FilterChanged = null;
        _priceFilter.FilterChanged = () => FilterChanged?.Invoke();

        _playerPerTableFilter.Init(Constants.PlayerPrefsKeys.TaxesHoldemTableSettingsPlayerPerTableFilter);
        _playerPerTableFilter.FilterChanged = null;
        _playerPerTableFilter.FilterChanged = () => FilterChanged?.Invoke();
    }
}
