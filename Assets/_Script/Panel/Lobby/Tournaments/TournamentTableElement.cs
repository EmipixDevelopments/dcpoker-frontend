﻿using System;
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
    [Space] 
    [SerializeField] private Button _detailInfo;
    [SerializeField] private Button _rightButton;
    [SerializeField] private GameObject _lateRegisterButtonGameObject; 
    [SerializeField] private GameObject _registerButtonGameObject; 
    [SerializeField] private GameObject _openButtonGameObject; 
    [Space]
    [SerializeField] private TableListColors _tableListColors;
    
    NormalTournamentDetails.NormalTournamentData _data;
    private getTournamentInfoData _tournamentData;

    private void OnEnable()
    {
        _detailInfo.onClick.AddListener(OnTournamentTableSelectButtonTap);
    }

    private void OnDisable()
    {
        _detailInfo.onClick.RemoveListener(OnTournamentTableSelectButtonTap);
    }

    public void Init(NormalTournamentDetails.NormalTournamentData data)
    {
        UpdateValue(data);
    }

    public void UpdateValue(NormalTournamentDetails.NormalTournamentData data)
    {
        _data = data;
        _dateTimeText.text = $"{CheckStringData(ParsingDateTime(data.tournamentStartTime))}";
        _nameText.text = $"{CheckStringData(data.name)}";
        _typeText.text = $"{CheckStringData(data.type)}";
        _playersText.text = $"{data.players}";
        _buyInText.text = $"{CheckStringData(data.buyIn)}";
        _statusInText.text = $"{CheckStringData(data.status)}";
        
        var textColor = _tableListColors.GetColorByName(data.colorOfCapture);
        SetColorText(textColor);
        UpdateButton();
    }

    private void SetColorText(Color color)
    {
        _dateTimeText.color = color;
        _nameText.color = color;
        _typeText.color = color;
        _playersText.color = color;
        _buyInText.color = color;
        _statusInText.color = color;
    }

    //public NormalTournamentDetails.NormalTournamentData GetData() { return _tournamentTableElementData; }

    private void OnTournamentTableSelectButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        
        UIManager.Instance.gameType = GameType.Touranment;
        UIManager.Instance.DetailsTournament.GetDetailsTournamentButtonTap(_data.tournamentId, _data.pokerGameType);
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
    
    private void OnOpenTournamentRoom()
    {
        Debug.LogError(_data.tournamentId);
        Debug.LogError(_tournamentData.id);
        UIManager.Instance.SocketGameManager.JoinTournamentRoom(_tournamentData.id, (socket, packet, args) =>
        {
            print("JoinTournamentRoom response: " + packet.ToString());

            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse<RoomsListing.Room> JoinTournamentRoomResp = JsonUtility.FromJson<PokerEventResponse<RoomsListing.Room>>(resp1);
            if (JoinTournamentRoomResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                /*if (!UIManager.Instance.tableManager.IsMiniTableTournamentExisted(JoinTournamentRoomResp.result.tournamentId))
                    UIManager.Instance.LobbyScreeen.GetRunningGameList();

                if (UIManager.Instance.IsMultipleTableAllowed && !UIManager.Instance.tableManager.playingTableList.Contains(JoinTournamentRoomResp.result.roomId))
                {
                    UIManager.Instance.tableManager.DeselectAllTableSelection();
                    UIManager.Instance.tableManager.AddMiniTable(JoinTournamentRoomResp.result);
                }
                UIManager.Instance.tableManager.DeselectAllTableSelection();

                MiniTable miniTable = UIManager.Instance.tableManager.GetMiniTable(JoinTournamentRoomResp.result.roomId);
                if (miniTable != null)
                {
                    UIManager.Instance.LobbyScreeen.TournamentDetailsScreen.Close();
                    miniTable.MiniTableButtonTap();
                }*/
                var room = JoinTournamentRoomResp.result;
                Constants.Poker.TableId = room.roomId;
                UIManager.Instance.GameScreeen.SetRoomDataAndPlay(room);

                UIManager.Instance.DisplayLoader("");
                UIManager.Instance.LobbyPanelNew.Close();
                UIManager.Instance.GameScreeen.Open();

            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(JoinTournamentRoomResp.message);
            }
        });
    }

    private void ResetButton()
    {
        _registerButtonGameObject.gameObject.SetActive(false);
        _lateRegisterButtonGameObject.gameObject.SetActive(false);
        _openButtonGameObject.gameObject.SetActive(false);
        
        _rightButton.onClick.RemoveAllListeners();
    }

    private void UpdateButton()
    {
        ResetButton();
        
        UIManager.Instance.SocketGameManager.getTournamentInfo(_data.tournamentId, _data.pokerGameType, (socket, packet, args) =>
        {
            Debug.Log("getSngTournamentInfo  : " + packet.ToString());
            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            var source = arr.getString(arr.length() - 1);
            PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(source);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                var result = resp.result;
                _tournamentData = result;
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
                return;
            }
        });
        
        if(_tournamentData == null)
            return;
        
        if(_tournamentData.isRegistered)
        //move text from here
        if (_data.status == "Running")
        {
            if (_tournamentData.isRegistered)
            {
                _openButtonGameObject.gameObject.SetActive(true);
                _rightButton.onClick.AddListener(OnOpenTournamentRoom);
                return;
            }
            
            if (IsLateRegister())
            {
                _lateRegisterButtonGameObject.gameObject.SetActive(true);
                return;
            }
        }

        _registerButtonGameObject.gameObject.SetActive(true);
    }

    private bool IsLateRegister()
    {
        if (!string.IsNullOrEmpty(_tournamentData.dateTime))
        {
            Debug.LogError(_tournamentData.dateTime);
            var dateTime = ParseDateTime(_tournamentData.dateTime);
            int lastTileForRegister = _tournamentData.lateRegistrationLevel * _tournamentData.bindLevelRizeTime;
            if (dateTime < DateTime.UtcNow && dateTime.AddMinutes(lastTileForRegister) > DateTime.UtcNow)
            {
                return true;
            }
        }
        return false;
    }
    
    private DateTime ParseDateTime(string dateTime)
    {
        Debug.LogError(dateTime);
        var timeString = dateTime.Substring(0, dateTime.Length - 5);
        return DateTime.ParseExact(timeString, "dd-MM-yyyy H:mm:ss tt ",null);
    }

}
