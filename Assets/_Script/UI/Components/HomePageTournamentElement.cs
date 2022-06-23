using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomePageTournamentElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _limitText;
    [SerializeField] private TextMeshProUGUI _seatsText;
    [SerializeField] private TextMeshProUGUI _blindsText;
    [SerializeField] private TextMeshProUGUI _buyInText;
    [SerializeField] private Button _button;

    private Action _onButtonClick;

    public void Init(HomePageTournamentElementData data, Action onButtonClick)
    {
        _limitText.text = data.Limit;
        _seatsText.text = $"{data.SeatsCurrent}/{data.SeatsMaximum}";
        _blindsText.text = $"{data.BlindsCurrent}/{data.BlindsMaximum}";
        _buyInText.text = $"{data.BuyIn}";
        
        _onButtonClick = onButtonClick;
        
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _onButtonClick?.Invoke();
    }
}
