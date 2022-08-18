using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TournamentTableElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dateTimeText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _typeText;
    [SerializeField] private TextMeshProUGUI _playersText;
    [SerializeField] private TextMeshProUGUI _buyInText;
    [SerializeField] private TextMeshProUGUI _statusInText;

    private Button _button;
    NormalTournamentDetails.NormalTournamentData _tournamentTableElementData;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnTournamentTableSelectButtonTap);
    }

    private void OnDestroy()
    {
        if (_button)
        {
            _button.onClick.RemoveAllListeners();
        }
    }

    public void Init(NormalTournamentDetails.NormalTournamentData data)
    {
        UpdateValue(data);
    }

    public void UpdateValue(NormalTournamentDetails.NormalTournamentData data)
    {
        _tournamentTableElementData = data;
        _dateTimeText.text = $"{CheckStringData(ParsingDateTime(data.tournamentStartTime))}";
        _nameText.text = $"{CheckStringData(data.name)}";
        _typeText.text = $"{CheckStringData(data.type)}";
        _playersText.text = $"{data.players}";
        _buyInText.text = $"{CheckStringData(data.buyIn)}";
        _statusInText.text = $"{CheckStringData(data.status)}";
    }
    public NormalTournamentDetails.NormalTournamentData GetData() { return _tournamentTableElementData; }

    public void OnTournamentTableSelectButtonTap()
    {
        Debug.Log("On button Click");
        // need get popup TournamentDetailsScreen from LobbyScreeen (old UI)
        //UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.TournamentDetailsId = _tournamentTableElementData.tournamentId;
        //UIManager.Instance.SoundManager.OnButtonClick();
        //UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.GetDetailsTournamentButtonTap(_tournamentTableElementData.tournamentId, _tournamentTableElementData.pokerGameType);
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
