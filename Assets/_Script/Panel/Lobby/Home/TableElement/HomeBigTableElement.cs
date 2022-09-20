using System;
using System.Globalization;
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
    [Space]
    [SerializeField] private TableListColors _tableListColors;

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
        
        _dateText.text = $"{CheckStringData(ParsingDateTime(data.tournamentStartTime))}";
        _nameText.text = $"{CheckStringData(data.name)}";
        _typeText.text = $"{CheckStringData(data.type)}";
        _playersText.text = $"{data.players}";
        _buyInText.text = $"{CheckStringData(data.buyIn)}";
        _status.text = $"{CheckStringData(data.status)}";

        var textColor = _tableListColors.GetColorByName(data.colorOfCapture);
        SetColorText(textColor);
        
        //todo add logic
        _button.gameObject.SetActive(false);
    }

    private void SetColorText(Color color)
    {
        _dateText.color = color;
        _nameText.color = color;
        _typeText.color = color;
        _playersText.color = color;
        _buyInText.color = color;
        _status.color = color;
    }

    private void OnButtonClick()
    {
        
    }
    
    private string CheckStringData(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return $"---";
        }
        return text;
    }

    private string ParsingDateTime(string dateTime)
    {
        if (string.IsNullOrEmpty(dateTime))
            return null;
        
        string result = "";
        string year = "";
        DateTime dt = DateTime.Parse(dateTime);
        if (dt.Year != DateTime.Now.Year)
        {
            year = $"{dt.Year} ";
        }
        result = $"{year}{dt.ToString("MMM dd", CultureInfo.CreateSpecificCulture("en-US"))} / {dt.ToString("HH:mm")}";
        return result;
    }
}
