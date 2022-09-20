using System.Globalization;
using BestHTTP.SocketIO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SitNGoTableElement : MonoBehaviour, IHighlightTableElement
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
    [SerializeField] private GameObject _unregisterButtonGameObject;

    [Space]
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _infoButton;

    private Image _highlightImage;
    TournamentRoomObject.TournamentRoom _data;
    private getTournamentInfoData _tournamentData;

    private PanelSitNGo _panelSitNGo;
    private DetailsTournamentData _detailsTournamentData;
    
    private void Start()
    {
        _highlightImage = GetComponent<Image>();
        _infoButton.onClick.AddListener(OnTournamentTableSelectButtonTap);
    }

    private void OnClickRegisterGreenButton()
    {
        var uiManager = UIManager.Instance;
        uiManager.SoundManager.OnButtonClick();
        
        //var detailsTournament = uiManager.DetailsTournament;
        //detailsTournament.GetDetailsTournamentButtonTap(_data.id, _data.pokerGameType);
        //uiManager.DetailsTournament.OpenPanel(this, _tournamentData, _data.tournamentId);
        //detailsTournament.Open();
    }

    public void Init(PanelSitNGo panelSitNGo)
    {
        _panelSitNGo = panelSitNGo;
        
        _detailsTournamentData = new DetailsTournamentData();
        _detailsTournamentData.HighlightTableElement = this;
    }

    public void SetData(TournamentRoomObject.TournamentRoom data)
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
    
    private void UpdateButton()
    {
        UIManager.Instance.SocketGameManager.getSngTournamentInfo(_data.id, _data.pokerGameType, (socket, packet, args) =>
        {
            Debug.Log("getSngTournamentInfo  : " + packet.ToString());
            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            var source = arr.getString(arr.length() - 1);
            PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(source);

            if (!resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                return;

            var result = resp.result;
            _tournamentData = result;
            _detailsTournamentData.TournamentInfoData = result;
            _detailsTournamentData.TournamentId = _data.id;
            
            if(_tournamentData == null)
                return;

            ResetButton();
            
            if(!_tournamentData.isRegistered)
            {
                _registerButtonGameObject.gameObject.SetActive(true);
                _rightButton.onClick.AddListener(OnRegisterButton);
                _detailsTournamentData.TournamentButtonState = TournamentButtonState.Register;
                _detailsTournamentData.ButtonAction = OnRegisterButton;
            }
            else
            {
                if (_tournamentData.status == Constants.PokerStatus.RunningStatus)
                {
                    _openButtonGameObject.gameObject.SetActive(true);
                    _rightButton.onClick.AddListener(OnOpenTournamentRoom); 
                    _detailsTournamentData.TournamentButtonState = TournamentButtonState.Open;
                    _detailsTournamentData.ButtonAction = OnOpenTournamentRoom;

                    return;
                }
                
                _unregisterButtonGameObject.gameObject.SetActive(true);
                _rightButton.onClick.AddListener(OnUnregisterButton);
                _detailsTournamentData.TournamentButtonState = TournamentButtonState.Unregister;
                _detailsTournamentData.ButtonAction = OnUnregisterButton;

            }
            
        });
    }
    
    private void ResetButton()
    {
        _registerButtonGameObject.gameObject.SetActive(false);
        _lateRegisterButtonGameObject.gameObject.SetActive(false);
        _openButtonGameObject.gameObject.SetActive(false);
        _unregisterButtonGameObject.gameObject.SetActive(false);
        
        _rightButton.onClick.RemoveAllListeners();
    }

    public void OnTournamentTableSelectButtonTap()
    {
        var uiManager = UIManager.Instance;
        uiManager.SoundManager.OnButtonClick();
        uiManager.gameType = GameType.sng;
        //UIManager.Instance.DetailsTournament.GetDetailsTournamentButtonTap(_data.id, _data.pokerGameType);

        uiManager.DetailsTournament.OpenPanel(_detailsTournamentData);
    }
    
    private string CheckStringData(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return $"---";
        }
        return text;
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
        
        uiManager.SocketGameManager.getSngTournamentTables(_data.id,_data.pokerGameType,
            (socket, packet, args) =>
            {

                Debug.Log("getSngTournamentTables  : " + packet.ToString());

                uiManager.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse<RoomsListing.Room> resp =
                    JsonUtility.FromJson<PokerEventResponse<RoomsListing.Room>>(resp1);

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    var room = resp.result;
                    Constants.Poker.TableId = room.roomId;
                    uiManager.GameScreeen.SetRoomDataAndPlay(room);

                    uiManager.DisplayLoader("");
                    uiManager.LobbyPanelNew.Close();
                    uiManager.GameScreeen.Open();
                }
                else
                {
                    uiManager.DisplayMessagePanel(resp.message);
                }
            });
    }

    private void OnRegisterButton()
    {
        var uiManager = UIManager.Instance;
        
        if(!uiManager.PopupDepositeOrClose.Open(GetAmountBuyIn()))
            return;
        
        uiManager.SocketGameManager.getRegisterSngTournament(_tournamentData.id, GameType.sng.ToString(), OnRegisterResponse);
    }

    private void OnRegisterResponse(Socket socket, Packet packet, object[] args)
    {
        Debug.Log("Register SnG: "+packet.ToString());          
        JSONArray arr = new JSONArray(packet.ToString());
        var source = arr.getString(arr.length() - 1);           
        var statusMessageStandard = JsonUtility.FromJson<StatusMessageStandard<RoomId>>(source);
        
        if(!statusMessageStandard.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(statusMessageStandard.message);  
            return;
        }
        
        UIManager.Instance.DisplayMessagePanel(statusMessageStandard.message);
        
        _panelSitNGo.UpdateTable();
    }

    private void OnUnregisterButton()
    {
        UIManager.Instance.SocketGameManager.getUnRegisterSngTournament(_tournamentData.id, GameType.sng.ToString(), OnUnregisterResponse);
    }

    private void OnUnregisterResponse(Socket socket, Packet packet, object[] args)
    {
        Debug.Log("Unregister SnG: " + packet);
        var arr = new JSONArray(packet.ToString());
        var source = arr.getString(arr.length() - 1);
        var statusMessageStandard = JsonUtility.FromJson<StatusMessageStandard<PlayerId>>(source);
        
        if(!statusMessageStandard.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(statusMessageStandard.message);  
            return;
        }
        
        UIManager.Instance.DisplayMessagePanel(statusMessageStandard.message);
        
        _panelSitNGo.UpdateTable();
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
