using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeBigTableElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dateText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _typeText;
    [SerializeField] private TextMeshProUGUI _playersText;
    [SerializeField] private TextMeshProUGUI _buyInText;
    [SerializeField] private TextMeshProUGUI _status;
    [SerializeField] private Button _button;

    private HomeBigElementData _data;
    
    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void SetInfo(HomeBigElementData data)
    {
        _data = data;
        
        _dateText.text = data.Data;
        _nameText.text = data.Name;
        _typeText.text = $"{data.Type}";
        _playersText.text = $"{data.Players}";
        _buyInText.text = $"{data.BuyIn}";
        _status.text = data.Status;
    }

    private void OnButtonClick()
    {
        
    }
}
