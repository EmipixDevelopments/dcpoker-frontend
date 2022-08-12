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
    TournamentTableElementData _tournamentTableElementData;

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

    public void Init(TournamentTableElementData data) 
    {
        _tournamentTableElementData = data;
        _dateTimeText.text  = $"{CheckStringData(data.TournamentStartTime)}";
        _nameText.text      = $"{CheckStringData(data.Name)}";
        _typeText.text      = $"{CheckStringData(data.Type)}";
        _playersText.text   = $"{data.Players}";
        _buyInText.text     = $"{CheckStringData(data.BuyIn)}";
        _statusInText.text  = $"{CheckStringData(data.Status)}";
    }

    public void OnTournamentTableSelectButtonTap()
    {
        UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.TournamentDetailsId = _tournamentTableElementData.TournamentId;
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.GetDetailsTournamentButtonTap(_tournamentTableElementData.TournamentId, _tournamentTableElementData.PokerGameType);
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
