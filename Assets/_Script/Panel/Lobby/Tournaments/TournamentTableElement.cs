using System;
using System.Globalization;
using BestHTTP.SocketIO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TournamentTableElement : MonoBehaviour, IHighlightTableElement
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
    [SerializeField] private GameObject _unregisterButtonGameObject;
    //[SerializeField] private GameObject _waitingListStatusTextGameObject;
    [Space]
    [SerializeField] private TableListColors _tableListColors;

    NormalTournamentDetails.NormalTournamentData _data;
    private getTournamentInfoData _tournamentData;

    private PanelTournaments _panelTournaments;
    private Image _highlightImage;

    private DetailsTournamentData _detailsTournamentData;
    private PopupConfirmTournamentData _popupConfirmTournamentData;

    private void OnEnable()
    {
        _highlightImage = GetComponent<Image>();
        _detailInfo.onClick.AddListener(OnTournamentTableSelectButtonTap);
    }

    private void OnDisable()
    {
        _detailInfo.onClick.RemoveListener(OnTournamentTableSelectButtonTap);
    }

    public void Init(PanelTournaments panelTournaments)
    {
        _panelTournaments = panelTournaments;
        
        _detailsTournamentData = new DetailsTournamentData();
        _popupConfirmTournamentData = new PopupConfirmTournamentData();
        _detailsTournamentData.HighlightTableElement = this;
    }

    public void SetData(NormalTournamentDetails.NormalTournamentData data)
    {
        _data = data;
        _detailsTournamentData.TournamentId = _data.tournamentId;
        _detailsTournamentData.NormalTournamentData = _data;
        
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

    private void OnTournamentTableSelectButtonTap()
    {
        if(_tournamentData == null)
            return;
        
        var uiManager = UIManager.Instance;
        uiManager.SoundManager.OnButtonClick();
        
        uiManager.gameType = GameType.Touranment;
        uiManager.DetailsTournament.OpenPanel(_detailsTournamentData);
        //UIManager.Instance.DetailsTournament.GetDetailsTournamentButtonTap(_data.tournamentId, _data.pokerGameType);
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

    private double GetAmountBuyIn()
    {
        var values = _data.buyIn.Split('+');
        
        double totalSum = 0;
        for (var i = 0; i < values.Length; i++)
        {
            var floatValue = double.Parse(values[i], CultureInfo.InvariantCulture.NumberFormat);
            totalSum += floatValue;
        }

        return totalSum;
    }
    
    private void OnOpenTournamentRoom()
    {
        var uiManager = UIManager.Instance;
        
        if(!uiManager.PopupDepositeOrClose.Open(GetAmountBuyIn()))
            return;
        
        UIManager.Instance.SocketGameManager.JoinTournamentRoom(_tournamentData.id, (socket, packet, args) =>
        {
            print("JoinTournamentRoom response: " + packet.ToString());

            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse<RoomsListing.Room> JoinTournamentRoomResp = JsonUtility.FromJson<PokerEventResponse<RoomsListing.Room>>(resp1);
            Debug.LogError(JoinTournamentRoomResp.ToString());
            if (JoinTournamentRoomResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
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
        _registerButtonGameObject.SetActive(false);
        _lateRegisterButtonGameObject.SetActive(false);
        _openButtonGameObject.SetActive(false);
        _unregisterButtonGameObject.SetActive(false);
        //_waitingListStatusTextGameObject.SetActive(false);
        
        _rightButton.onClick.RemoveAllListeners();
    }

    private void UpdateButton()
    {
        UIManager.Instance.SocketGameManager.getTournamentInfo(_data.tournamentId, _data.pokerGameType, (socket, packet, args) =>
        {
            Debug.Log("getTournamentInfo  : " + packet.ToString());
            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            var source = arr.getString(arr.length() - 1);
            PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(source);

            if (!resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                return;
            
            var result = resp.result;
            _tournamentData = result;
            _detailsTournamentData.TournamentInfoData = result;

            ResetButton();
            
            switch (_data.status)
            {
                case "Running":
                    if (_tournamentData.isRegistered)
                        SetButtonToOpen();
                    break;
                case "Waiting":
                    if (_tournamentData.isRegistered)
                        SetButtonToUnregister();
                    else// if(_data.isJoinable) //it's not work 
                        SetButtonToRegister();
                    break;
                case "Late Register":
                    if (_tournamentData.isRegistered)
                        SetButtonToOpen();
                    else //if(_data.isJoinable)
                        SetButtonToLateRegister();
                    break;
                default:
                    ResetDetailsTournamentData();
                    break;
                //case "Cancel": break;
            }
/*
            if (_tournamentData.status == "Running")
            {
                if (_tournamentData.isRegistered)
                {
                    _openButtonGameObject.gameObject.SetActive(true);
                    _rightButton.onClick.AddListener(OnOpenTournamentRoom);
                    _detailsTournamentData.TournamentButtonState = TournamentButtonState.Open;
                    _detailsTournamentData.ButtonAction = OnOpenTournamentRoom;
                    return;
                }

                if (IsLateRegister())
                {
                    _rightButton.onClick.AddListener(OnRegisterButton);
                    _lateRegisterButtonGameObject.gameObject.SetActive(true);
                    _detailsTournamentData.TournamentButtonState = TournamentButtonState.LateRegister;
                    _detailsTournamentData.ButtonAction = OnRegisterButton;
                    return;
                }
            }
    
            if(!IsRegister())
            {
                //_waitingListStatusTextGameObject.SetActive(true);
                _detailsTournamentData.TournamentButtonState = TournamentButtonState.None;
                _detailsTournamentData.ButtonAction = null;
                return;
            }
            
            if(!_tournamentData.isRegistered)
            {
                _registerButtonGameObject.gameObject.SetActive(true);
                _rightButton.onClick.AddListener(OnRegisterButton);
                _detailsTournamentData.TournamentButtonState = TournamentButtonState.Register;
                _detailsTournamentData.ButtonAction = OnRegisterButton;
            }
            else
            {
                _unregisterButtonGameObject.gameObject.SetActive(true);
                _rightButton.onClick.AddListener(OnUnregisterButton);
                _detailsTournamentData.TournamentButtonState = TournamentButtonState.Unregister;
                _detailsTournamentData.ButtonAction = OnUnregisterButton;
            }
*/
        });
    }

    private void SetButtonToOpen()
    {
        _openButtonGameObject.gameObject.SetActive(true);
        _rightButton.onClick.AddListener(OnOpenTournamentRoom);
        _detailsTournamentData.TournamentButtonState = TournamentButtonState.Open;
        _detailsTournamentData.ButtonAction = OnOpenTournamentRoom;
    }

    private void SetButtonToLateRegister()
    {
        _rightButton.onClick.AddListener(OnRegisterButton);
        _lateRegisterButtonGameObject.gameObject.SetActive(true);
        _detailsTournamentData.TournamentButtonState = TournamentButtonState.LateRegister;
        _detailsTournamentData.ButtonAction = OnRegisterButton;
    }

    private void SetButtonToUnregister()
    {
        _unregisterButtonGameObject.gameObject.SetActive(true);
        _rightButton.onClick.AddListener(OnUnregisterButton);
        _detailsTournamentData.TournamentButtonState = TournamentButtonState.Unregister;
        _detailsTournamentData.ButtonAction = OnUnregisterButton;
    }

    private void SetButtonToRegister()
    {
        _registerButtonGameObject.gameObject.SetActive(true);
        _rightButton.onClick.AddListener(OnRegisterButton);
        _detailsTournamentData.TournamentButtonState = TournamentButtonState.Register;
        _detailsTournamentData.ButtonAction = OnRegisterButton;
    }

    private void ResetDetailsTournamentData()
    {
        _detailsTournamentData.TournamentButtonState = TournamentButtonState.None;
        _detailsTournamentData.ButtonAction = null;
    }


    private bool IsLateRegister()
    {
        if (!string.IsNullOrEmpty(_tournamentData.dateTime))
        {
            try
            {
                var dateTime = ParseDateTime(_tournamentData.dateTime);
                int lastTileForRegister = _tournamentData.lateRegistrationLevel * _tournamentData.bindLevelRizeTime;
                if (dateTime < DateTime.UtcNow && dateTime.AddMinutes(lastTileForRegister) > DateTime.UtcNow)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Not Parse To Date Time: " + _tournamentData.dateTime);
                return false;
            }
            
        }
        return false;
    }

    private bool IsRegister()
    {
        if (!string.IsNullOrEmpty(_tournamentData.dateTime))
        {
            try
            {
                var dateTime = ParseDateTime(_tournamentData.dateTime);
                if (dateTime < DateTime.UtcNow)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Not Parse To Date Time: " + _tournamentData.dateTime);
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

    private void OnRegisterButton()
    {
        var uiManager = UIManager.Instance;

        if (_data.buyIn == "Freeroll")
        {
            Register();
            return;
        }
        //if(!_data.isFreeRoll) 
        if(!uiManager.PopupDepositeOrClose.Open(GetAmountBuyIn()))
                return;
        
        _popupConfirmTournamentData.ConfirmAction = Register;
        
        _popupConfirmTournamentData.BuyInText = _data.buyIn;

        UIManager.Instance.PopupConfirmTournament.OpenPopup(_popupConfirmTournamentData);
    }

    private void Register()
    {
        UIManager.Instance.SocketGameManager.getRegisterTournament(_tournamentData.id,GameType.Touranment.ToString(),RegisterTournamentResponse);
    }

    private void RegisterTournamentResponse(Socket socket, Packet packet, object[] args)
    {
        Debug.Log("Register Tournament: "+packet.ToString());
        JSONArray arr = new JSONArray(packet.ToString());
        var source = arr.getString(arr.length() - 1);
        var statusMessageStandard = JsonUtility.FromJson<StatusMessageStandard<RoomId>>(source);
        
        if(!statusMessageStandard.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(statusMessageStandard.message);  
            return;
        }
        
        UIManager.Instance.DisplayMessagePanel(statusMessageStandard.message);  
        _panelTournaments.UpdateTable();
    }

    private void OnUnregisterButton()
    {
        UIManager.Instance.SocketGameManager.getUnRegisterTournament(_tournamentData.id,GameType.Touranment.ToString(),UnregisterTournamentResponse);
    }
    
    private void UnregisterTournamentResponse(Socket socket, Packet packet, object[] args)
    {
        Debug.Log("Unregister Tournament: "+packet.ToString());
        JSONArray arr = new JSONArray(packet.ToString());
        var source = arr.getString(arr.length() - 1);
        var statusMessageStandard = JsonUtility.FromJson<StatusMessageStandard<PlayerId>>(source);
        
        if(!statusMessageStandard.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(statusMessageStandard.message);  
            return;
        }
        
        UIManager.Instance.DisplayMessagePanel(statusMessageStandard.message);  
        _panelTournaments.UpdateTable();
    }

    public void SetHighlight(bool active)
    {
        _highlightImage.enabled = active;
    }

    public void UpdateData()
    {
        UpdateButton();
    }
}
