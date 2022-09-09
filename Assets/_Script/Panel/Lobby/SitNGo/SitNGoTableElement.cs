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
    
    [Space]
    [SerializeField] private GameObject _registerButtonGameObject;
    [SerializeField] private GameObject _lateRegisterButtonGameObject;
    [SerializeField] private GameObject _openButtonGameObject;

    [Space]
    [SerializeField] private Button _rightButton;

    private Button _button;
    TournamentRoomObject.TournamentRoom _data;
    private getTournamentInfoData _tournamentData;
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

        UpdateButton();
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

    private void OnOpenTournamentRoom()
    {
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

    private void OnRegisterButton()
    {
        
    }

    private void OnLateRegisterButton()
    {
        
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
        
        UIManager.Instance.SocketGameManager.getSngTournamentInfo(_data.tournamentId, _data.pokerGameType, (socket, packet, args) =>
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
                _rightButton.onClick.AddListener(OnRegisterButton);
                _lateRegisterButtonGameObject.gameObject.SetActive(true);
            }
        }
        
        if(_tournamentData.isRegistered)
        {
            _registerButtonGameObject.gameObject.SetActive(true);
            _rightButton.onClick.AddListener(OnRegisterButton);
        }
    }

    private bool IsLateRegister()
    {
        /*
        if (!string.IsNullOrEmpty(_tournamentData.dateTime))
        {
            Debug.LogError(_tournamentData.dateTime);
            var dateTime = ParseDateTime(_tournamentData.dateTime);
            int lastTileForRegister = _tournamentData.lateRegistrationLevel * _tournamentData.bindLevelRizeTime;
            if (dateTime < DateTime.UtcNow && dateTime.AddMinutes(lastTileForRegister) > DateTime.UtcNow)
            {
                return true;
            }
        }*/
        return false;
    }
}
