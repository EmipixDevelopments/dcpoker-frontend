﻿using System;
using System.Globalization;
using BestHTTP.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SitNGoTableElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _typeText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _seatText;
    [SerializeField] private TextMeshProUGUI _blindsText;
    [SerializeField] private TextMeshProUGUI _buyInText;
    [SerializeField] private TextMeshProUGUI _statusText;
  /*  [Space]
    [SerializeField] private Button _registerGoldButton;
    [SerializeField] private Button _registerGreenButton;
    [SerializeField] private Button _openButton;
*/
    private Button _button;
    TournamentRoomObject.TournamentRoom _data;
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnTournamentTableSelectButtonTap);
    }



    private void OnClickRegisterGreenButton()
    {
        var uiManager = UIManager.Instance;
        uiManager.SoundManager.OnButtonClick();
        
        var detailsTournament = uiManager.DetailsTournament;
        detailsTournament.GetDetailsTournamentButtonTap(_data.id, _data.pokerGameType);
        //detailsTournament.Open();
    }
    

    private void OnDestroy()
    {
        if (_button)
        {
            _button.onClick.RemoveAllListeners();
        }
    }

    public void Init(TournamentRoomObject.TournamentRoom data)
    {
        UpdateValue(data);
    }

    public void UpdateValue(TournamentRoomObject.TournamentRoom data)
    {
        _data = data;
        _seatText.text = $"{CheckStringData(data.seat)}";
        _nameText.text = $"{CheckStringData(data.name)}";
        _typeText.text = $"{CheckStringData(data.type)}";
        _blindsText.text = $"{CheckStringData(data.blinds)}";
        _buyInText.text = $"{CheckStringData(data.buyIn)}";
        _statusText.text = $"{CheckStringData(data.status)}";
    }

    
    public TournamentRoomObject.TournamentRoom GetData() { return _data; }

    public void OnTournamentTableSelectButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.gameType = GameType.sng;
        UIManager.Instance.DetailsTournament.GetDetailsTournamentButtonTap(_data.id, _data.pokerGameType);
    }

    //Why might this happen?
    private string CheckStringData(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return $"---";
        }
        return text;
    }
}
