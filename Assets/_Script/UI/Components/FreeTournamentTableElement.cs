using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class FreeTournamentTableElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dateText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _typeText;
    [SerializeField] private TextMeshProUGUI _playersText;
    [SerializeField] private TextMeshProUGUI _buyInText;
    [SerializeField] private TextMeshProUGUI _status;
    [SerializeField] private Button _button;

    private Action _onButtonClick;

    public void Init(FreeTournamentTableElementData data, Action onButtonClick) 
    {
        _dateText.text = data.Data;
        _nameText.text = data.Name;
        _typeText.text = $"{data.Type}";
        _playersText.text = $"{data.Players}";
        _buyInText.text = $"{data.BuyIn}";
        _status.text = data.Status;
        _onButtonClick = onButtonClick;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _onButtonClick?.Invoke();
    }
}
