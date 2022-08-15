using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
        _dateTimeText.text = $"{CheckStringData(data.dateTime)}";
        _nameText.text = $"{CheckStringData(data.name)}";
        _typeText.text = $"{CheckStringData(data.type)}";
        _playersText.text = $"{data.players}";
        _buyInText.text = $"{CheckStringData(data.buyIn)}";
        _statusInText.text = $"{CheckStringData(data.status)}";
    }
    public NormalTournamentDetails.NormalTournamentData GetData() { return _tournamentTableElementData; }

    public void OnTournamentTableSelectButtonTap()
    {
        UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.TournamentDetailsId = _tournamentTableElementData.tournamentId;
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.GetDetailsTournamentButtonTap(_tournamentTableElementData.tournamentId, _tournamentTableElementData.pokerGameType);
    }

    private string CheckStringData(string text) 
    {
        if (string.IsNullOrEmpty(text))
        {
            return $"---";
        }
        return text;
    }
}
