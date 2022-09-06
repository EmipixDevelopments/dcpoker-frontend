using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HomeSmallTableElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _limitText;
    [SerializeField] private TextMeshProUGUI _seatsText;
    [SerializeField] private TextMeshProUGUI _blindsText;
    [SerializeField] private TextMeshProUGUI _buyInText;
    [SerializeField] private Button _button;

    private HomeSmallTableElementData _data;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void SetInfo(HomeSmallTableElementData data)
    {
        _data = data;
        
        _limitText.text = data.Limit;
        _seatsText.text = $"{data.SeatsCurrent}/{data.SeatsMaximum}";
        _blindsText.text = $"{data.BlindsCurrent}/{data.BlindsMaximum}";
        _buyInText.text = $"{data.BuyIn}";
    }

    private void OnButtonClick()
    {
        
    }
}
