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

    private NormalTournamentDetails.NormalTournamentData _data;
    
    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void SetInfo(NormalTournamentDetails.NormalTournamentData data)
    {
        _data = data;
        
        _dateText.text = data.dateTime;
        _nameText.text = data.name;
        _typeText.text = $"{data.type}";
        _playersText.text = $"{data.players}";
        _buyInText.text = $"{data.buyIn}";
        _status.text = data.status;
    }

    private void OnButtonClick()
    {
        
    }
}
