using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;
using System.Globalization;
using System.Threading.Tasks;

public class DetailsTournamentNew : MonoBehaviour
{
    [Header("Right panel")]
    [SerializeField] private Toggle _summaryToggle;
    [SerializeField] private TextMeshProUGUI _summaryToggleText;
    [SerializeField] private TextMeshProUGUI _tournamentNameText;
    [SerializeField] private TextMeshProUGUI _statusText;
    [SerializeField] private TextMeshProUGUI _blindTimeText;
    [SerializeField] private TextMeshProUGUI _prizePoolText;
    [SerializeField] private TextMeshProUGUI _limitText;
    [SerializeField] private TextMeshProUGUI _buyInText;
    [SerializeField] private TextMeshProUGUI _playersText;
    [SerializeField] private TextMeshProUGUI _startInText;
    [SerializeField] private Button _registerButton;
    [SerializeField] private Button _unRegisterButton;
    [SerializeField] private Button _reBuyButton;
    [Space]
    [SerializeField] private Image _registerButtonSprite;
    [SerializeField] private Image _unRegisterButtonSprite;
    [SerializeField] private Image _reBuyButtonSprite;
    [SerializeField] private Sprite _greenSprite;
    [SerializeField] private Sprite _orangeSprite;
    [Space]
    [SerializeField] private TextMeshProUGUI _registerButtonText;

    [Header("Left panel")]
    [SerializeField] private DetailsTournamentNewLeftPanel _detailsTournamentNewLeftPanel;
    
    [Space] 
    [SerializeField] private GameObject _registerButtonGameObject;
    [SerializeField] private GameObject _unregisterButtonGameObject;
    [SerializeField] private GameObject _lateRegisterButtonGameObject;
    [SerializeField] private GameObject _openButtonGameObject;
    [SerializeField] private Button _button;

    [SerializeField] private Button _closeButton;

    private Animator _animator;
    private int _openAnimationId = Animator.StringToHash("open");
    private int _closeAnimationId = Animator.StringToHash("close");

    public string TournamentDetailsId = "";

    private int    _selectedTab = 0;
    private string _pokerGameType = "";
    private string _TID = "";
    private string _nameSpaceStr = "";
    private long   _rebuyAmount;
    private double totalSeconds;

    private Coroutine _updateDataCoroutine;
    
    private DetailsTournamentData _detailsTournamentData;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //not need remove
        _summaryToggle.onValueChanged.RemoveAllListeners();
        _reBuyButton.onClick.RemoveAllListeners();
        _registerButton.onClick.RemoveAllListeners();
        _unRegisterButton.onClick.RemoveAllListeners();

        _summaryToggle.onValueChanged.AddListener(OpenOrCloseLeftPanel);
        _reBuyButton.onClick.AddListener(RebuyButtonaTap);
        _registerButton.onClick.AddListener(RegisterButtonTap);
        _unRegisterButton.onClick.AddListener(UnRegisterButtonTap);
        
        _button.onClick.AddListener(OnButton);
        _closeButton.onClick.AddListener(CloseAnim);

    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveListener(CloseAnim);

    }

    private void OpenOrCloseLeftPanel(bool value)
    {
        _summaryToggleText.text = value ? "Close Summary" : "Show Summary";
        _detailsTournamentNewLeftPanel.SetActive(value);
        //_detailsTournamentNewLeftPanel.gameObject.SetActive(value);
    }

    private void OnEnable()
    {
        var uiManager = UIManager.Instance;
        if(uiManager == null)
            return;
        
        uiManager.LobbyPanelNew.AddOnSwitchLobbyPanelListener(OnSwitchLobbyPanel);
        
        _animator.Play(_openAnimationId);
        AnimScrollRect(false);
    }

    private void OnDisable()
    {
        var uiManager = UIManager.Instance;
        if(uiManager == null)
            return;

        if(_summaryToggle.isOn)
            _summaryToggle.isOn = false;
        
        uiManager.LobbyPanelNew.RemoveOnSwitchLobbyPanelListener(OnSwitchLobbyPanel);
        _detailsTournamentData.HighlightTableElement.SetHighlight(false);
        
        if(_updateDataCoroutine!=null)
            StopCoroutine(_updateDataCoroutine);
    }

    private void OnSwitchLobbyPanel(LobbyPanelNew.LobbyPanel lobbyPanel)
    {
        gameObject.SetActive(false);
        EnableScrollBar();
    }

    private void CloseAnim()
    {
        _animator.Play(_closeAnimationId);
        _detailsTournamentNewLeftPanel.SetActive(false);
        AnimScrollRect(true);
        StartCoroutine(DisableAfterSeconds());
    }

    private IEnumerator DisableAfterSeconds()
    {
        yield return new WaitForSeconds(.15f);
        gameObject.SetActive(false);
    }
    private async void DisableAfterAnimAsync()
    {
        await Task.Delay(250); // This is animation duration
        gameObject.SetActive(false);
    }

    public void OpenPanel(DetailsTournamentData detailsTournamentData)
    {
        TournamentDetailsId = detailsTournamentData.TournamentId;//It's old
        _pokerGameType = detailsTournamentData.TournamentInfoData.pokerGameType;//It's old
        Constants.Poker.TournamentId = TournamentDetailsId; //I don't know why it need.
        
        if(detailsTournamentData.HighlightTableElement == null || detailsTournamentData.TournamentInfoData == null)
            return;

        /* if (detailsTournamentData == _detailsTournamentData) // need to rewrite
        {
            if(!gameObject.activeSelf)
                gameObject.SetActive(true);
            
            return;
        }*/
        
        if(gameObject.activeSelf)
            _detailsTournamentData.HighlightTableElement.SetHighlight(false);

        _detailsTournamentData = detailsTournamentData;

        detailsTournamentData.HighlightTableElement.SetHighlight(true);

        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            StartCoroutine(updateDataEnumerator());
            return;
        }
        
        UpdateData();
    }

    private IEnumerator updateDataEnumerator()
    {
        while (true)
        {
            UpdateData();
            yield return new WaitForSeconds(5);
        }
    }

    private void UpdateButton()
    {
        ResetButton();
        GameObject gameObjectButton = null;
        
        switch (_detailsTournamentData.TournamentButtonState)
        {
            case TournamentButtonState.Open:
                gameObjectButton = _openButtonGameObject;
                break;
            case TournamentButtonState.Register:
                gameObjectButton = _registerButtonGameObject;
                break;
            case TournamentButtonState.Unregister:
                gameObjectButton = _unregisterButtonGameObject;
                break;
            case TournamentButtonState.LateRegister:
                gameObjectButton = _lateRegisterButtonGameObject;
                break;
        }
        
        if(gameObjectButton == null)
        {
            _button.gameObject.SetActive(false);
            return;
        }
        
        gameObjectButton.SetActive(true);
        _button.gameObject.SetActive(true);
    }

    private void OnButton()
    {
        _detailsTournamentData.ButtonAction?.Invoke();
        _detailsTournamentData.HighlightTableElement.UpdateData();
        UpdateData();
    }

    private void ResetButton()
    {
        _registerButtonGameObject.SetActive(false);
        _unregisterButtonGameObject.SetActive(false);
        _lateRegisterButtonGameObject.SetActive(false);
        _openButtonGameObject.SetActive(false);
    }

    private void UpdateData()
    {
        ResetData();
        
        var tournamentInfoData = _detailsTournamentData.TournamentInfoData;
        _statusText.text = tournamentInfoData.status;
        _buyInText.text = tournamentInfoData.buyIn;
        _blindTimeText.text = tournamentInfoData.blindTime + " min.";
        _playersText.text = tournamentInfoData.players.ToString();
        _limitText.text = tournamentInfoData.gameType;
        _prizePoolText.text = tournamentInfoData.prizePool.ConvertToCommaSeparatedValue();

        _tournamentNameText.text = tournamentInfoData.name;
        _nameSpaceStr = tournamentInfoData.namespaceString;
        _TID = tournamentInfoData.id;
        _rebuyAmount = tournamentInfoData.rebuyAmount;
        
        ChangeInfoText();
        UpdateButton();
    }

    private void ChangeInfoText()
    {
        var tournamentInfoData = _detailsTournamentData.TournamentInfoData;
        _startInText.gameObject.SetActive(true);
        _startInText.text = "";
        //todo rewrite this
        //dateTime always not empty or null



        /*try
        {
            //var timeString = tournamentInfoData.dateTime.Substring(0, tournamentInfoData.dateTime.Length - 5);

            //DateTime dateTime = DateTime.ParseExact(timeString, "dd-MM-yyyy HH:mm:ss tt",null);
            DateTime dateTime = DateTime.Parse(_detailsTournamentData.NormalTournamentData.dateTime);

            int lastTileForRegister = tournamentInfoData.lateRegistrationLevel * tournamentInfoData.bindLevelRizeTime;
            
            // 2. Турнир не начался по времени (время есть)
            if (dateTime > DateTime.Now)
            {
                TimeSpan lastTime = dateTime.Subtract(DateTime.UtcNow);
                _startInText.text = $"Will start in <b>{lastTime.Days} Days : {lastTime.Hours} Hours : {lastTime.Minutes} Minutes</b>";
            }
            
            // 1. Турнир начался, и не прошло разрешенное время
            else if (dateTime.AddMinutes(lastTileForRegister) > DateTime.UtcNow)
            {
                _startInText.gameObject.SetActive(false);
            }
        }
        catch (Exception e)
        {
            _startInText.text = $"Will start when <b>{tournamentInfoData.min_players - tournamentInfoData.players} playeres will join</b>";
        }
        */
        
        var dateString = _detailsTournamentData.NormalTournamentData?.tournamentStartTime;
        
        if (string.IsNullOrEmpty(dateString))
        {
            // 3. Турнир не начался из за того, что нет минимального количества игроков (время пустое)

            var needPlayerToStart = tournamentInfoData.min_players - tournamentInfoData.players;
            
            if(needPlayerToStart <= 0)
                return;
            
            _startInText.text = $"Will start when <b>{needPlayerToStart} playeres will join</b>";
            _playersText.text = $"{tournamentInfoData.players}/{tournamentInfoData.min_players}";
        }
        else
        {
            var dateTime = DateTime.Parse(dateString);
            var currentTime = DateTime.UtcNow.AddHours(3); // Europe/Lisbon (idk 3)

            int lastTileForRegister = tournamentInfoData.lateRegistrationLevel * tournamentInfoData.bindLevelRizeTime;
            // 2. Турнир не начался по времени (время есть)
            if (dateTime > currentTime)
            {
                TimeSpan lastTime = dateTime.Subtract(currentTime);
                _startInText.text = $"Will start in <b>{lastTime.Days} Days : {lastTime.Hours} Hours : {lastTime.Minutes} Minutes</b>";
            }
            // 1. Турнир начался, и не прошло разрешенное время
            else if (dateTime.AddMinutes(lastTileForRegister) > currentTime)
            {
                _startInText.gameObject.SetActive(false);
            }
        }
    }

    private float _oldValueScrollbar;
    private void AnimScrollRect(bool active)
    {
        var scrollRect = UIManager.Instance.LobbyPanelNew.GetScrollRect();

        if (active)
        {
            AnimScrollBarAsync(1, _oldValueScrollbar, .15f,
                value => scrollRect.verticalScrollbar.value = value,
                EnableScrollBar);

            return;
        }

        _oldValueScrollbar = scrollRect.verticalScrollbar.value;
        
        AnimScrollBarAsync(scrollRect.verticalScrollbar.value, 1, .15f,
            value => scrollRect.verticalScrollbar.value = value);
        
        scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.Permanent;
        scrollRect.vertical = false;
    }

    private void EnableScrollBar()
    {
        var scrollRect = UIManager.Instance.LobbyPanelNew.GetScrollRect();
        scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
        scrollRect.vertical = true; 
    }

    private async void AnimScrollBarAsync(float fromValue, float toValue, float duration, Action<float> setAction, Action onCompleteAction = null)// I don't know (if need)
    {
        float value;
        var rawValue = toValue - fromValue;
        
        float time = 0;
        while ( time <= duration )
        {
            value = fromValue + rawValue * (time / duration);
            time += Time.deltaTime;
            setAction(value);
            
            await Task.Yield();
        }

        value = toValue;
        setAction(value);
        
        onCompleteAction?.Invoke();
    }

    /// <summary>
    /// old
    /// </summary>
    /*void OnEnable()
    {
        ResetData();
        TournamentEventCall();
    }

    void OnDisable()
    {
        CancelInvoke();
    }*/

    //void Update()
    //{
    //    if (totalSeconds > 0)
    //    {
    //        totalSeconds -= 1 * Time.deltaTime;
    //        txtReBuyTime.Open();
    //        txtReBuyTime.text = "Remaining Time : " + string.Format("{1:D2}:{2:D2}", TimeSpan.FromSeconds(totalSeconds).Hours, TimeSpan.FromSeconds(totalSeconds).Minutes, TimeSpan.FromSeconds(totalSeconds).Seconds);
    //    }
    //    else
    //    {
    //        totalSeconds = 0;
    //        txtReBuyTime.text = "";
    //        txtReBuyTime.Close();
    //        //isBonusReady = true;
    //        //txtDailyBonus.text = "Bonus Ready!";
    //    }
    //}
    
    //todo: not need get Tournament Data again
    public void GetDetailsTournamentButtonTap(string TournamentId, string pokerGameType)
    {
        this.Close();
        TournamentDetailsId = TournamentId;
        _pokerGameType = pokerGameType;
        Constants.Poker.TournamentId = TournamentDetailsId;
        this.Open();
    }

    private void ResetData()
    {
        _statusText.text = "";
        _blindTimeText.text = "";
        _prizePoolText.text = "";
        _limitText.text = "";
        _buyInText.text = "";
        _playersText.text = "";
        _startInText.text = "";
    }

    private void TournamentEventCall()
    {
        CancelInvoke();
        if (TournamentDetailsId != "")
        {
            if (UIManager.Instance.gameType == GameType.Touranment)
            {
                RegularTournamentEventCall();
            }
            if (UIManager.Instance.gameType == GameType.sng)
            {
                SngTournamentEventCall();
            }
        }
    }
    private void RegularTournamentEventCall()
    {
        if (TournamentDetailsId != "")
        {
            UIManager.Instance.SocketGameManager.getTournamentInfo(TournamentDetailsId, _pokerGameType, (socket, packet, args) =>
            {

                Debug.Log("getTournamentInfo  : " + packet.ToString());
                UIManager.Instance.HideLoader();
                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;
                JSON_Object TournamentEventObj = new JSON_Object(resp1.ToString());
                PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(resp1);

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    _statusText.text = resp.result.status;
                    _buyInText.text = resp.result.buyIn;
                    _playersText.text = resp.result.players.ToString();
                    _limitText.text = resp.result.gameType;
                    _prizePoolText.text = resp.result.prizePool.ConvertToCommaSeparatedValue();

                    _tournamentNameText.text = resp.result.name;
                    _nameSpaceStr = resp.result.namespaceString;
                    _TID = resp.result.id;
                    _rebuyAmount = resp.result.rebuyAmount;

                    ChangeTextAndColorButton(resp);
                    ChangeInfoText(resp);

                    if (resp.result.allowRebuy)
                    {
                        _registerButton.gameObject.SetActive(false);
                        _unRegisterButton.gameObject.SetActive(false);
                        _reBuyButton.Open();
                        totalSeconds = resp.result.remainRebuySec;
                    }
                    else
                    {
                        _registerButton.gameObject.SetActive(!resp.result.isRegistered);
                        _unRegisterButton.gameObject.SetActive(resp.result.isRegistered);
                        _reBuyButton.Close();
                    }
                    if (resp.result.registrationStatus.Equals("open") || resp.result.registrationStatus.Equals("Open"))
                    {
                        _registerButton.interactable = true;
                    }
                    else
                    {
                        _registerButton.interactable = false;
                        _unRegisterButton.interactable = false;
                    }
                    Invoke("RegularTournamentEventCall", 5);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }
            });
        }
    }
    private void SngTournamentEventCall()
    {
        if (TournamentDetailsId != "")
        {
            UIManager.Instance.SocketGameManager.getSngTournamentInfo(TournamentDetailsId, _pokerGameType, (socket, packet, args) =>
            {
                Debug.Log("getSngTournamentInfo  : " + packet.ToString());
                UIManager.Instance.HideLoader();
                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;
                PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(resp1);

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    _statusText.text = resp.result.status;
                    _buyInText.text = resp.result.buyIn;
                    _playersText.text = resp.result.players.ToString();
                    _limitText.text = resp.result.gameType;
                    _prizePoolText.text = resp.result.prizePool.ConvertToCommaSeparatedValue();
                    _tournamentNameText.text = resp.result.name;

                    _nameSpaceStr = resp.result.namespaceString;
                    _TID = resp.result.id;
                    _rebuyAmount = resp.result.rebuyAmount;

                    ChangeTextAndColorButton(resp);
                    ChangeInfoText(resp);

                    if (resp.result.allowRebuy)
                    {
                        _registerButton.gameObject.SetActive(false);
                        _unRegisterButton.gameObject.SetActive(false);
                        _reBuyButton.Open();
                        totalSeconds = resp.result.remainRebuySec;
                    }
                    else
                    {
                        _registerButton.gameObject.SetActive(!resp.result.isRegistered);
                        _unRegisterButton.gameObject.SetActive(resp.result.isRegistered);
                        _reBuyButton.Close();
                    }
                    if (resp.result.registrationStatus.Equals("open") || resp.result.registrationStatus.Equals("Open"))
                    {
                        _registerButton.interactable = true;
                    }
                    else
                    {
                        _registerButton.interactable = false;
                    }
                    Invoke("SngTournamentEventCall", 5);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }
            });
        }
    }

    private void ChangeTextAndColorButton(PokerEventResponse<getTournamentInfoData> resp)
    {
        _registerButtonSprite.sprite = _orangeSprite;
        if (!string.IsNullOrEmpty(resp.result.dateTime))
        {
            Debug.LogError(resp.result.dateTime);
            var dateTime = DateTime.Now;
            try
            {
                dateTime = ParseDateTime(resp.result.dateTime);

            }
            catch (Exception e)
            {
                return;
            }
            int lastTileForRegister = resp.result.lateRegistrationLevel * resp.result.bindLevelRizeTime;
            if (dateTime < DateTime.UtcNow && dateTime.AddMinutes(lastTileForRegister) > DateTime.UtcNow)
            {
                _registerButtonText.text = "late register";
                _registerButtonSprite.sprite = _greenSprite;
            }
        }
    }

    private DateTime ParseDateTime(string dateTime)
    {
        Debug.LogError(dateTime);
        var timeString = dateTime.Substring(0, dateTime.Length - 5);
        Debug.LogError("timeString: " + timeString);

        return DateTime.ParseExact(timeString, "dd-MM-yyyy H:mm:ss tt",null);
    }

    private void ChangeInfoText(PokerEventResponse<getTournamentInfoData> resp)
    {
        _startInText.gameObject.SetActive(true);
        if (string.IsNullOrEmpty(resp.result.dateTime))
        {
            // 3. Турнир не начался из за того, что нет минимального количества игроков (время пустое)
            _startInText.text = $"Will start when <b>{resp.result.min_players - resp.result.players} playeres will join</b>";
        }
        else
        {
            DateTime dateTime = ParseDateTime(resp.result.dateTime);
            int lastTileForRegister = resp.result.lateRegistrationLevel * resp.result.bindLevelRizeTime;
            // 2. Турнир не начался по времени (время есть)
            if (dateTime > DateTime.Now)
            {
                TimeSpan lastTime = dateTime.Subtract(DateTime.UtcNow);
                _startInText.text = $"Will start in <b>{lastTime.Days} Days : {lastTime.Hours} Hours : {lastTime.Minutes} Minutes</b>";
            }
            // 1. Турнир начался, и не прошло разрешенное время
            else if (dateTime.AddMinutes(lastTileForRegister) > DateTime.UtcNow)
            {
                _startInText.gameObject.SetActive(false);
            }
        }
    }

    public void RebuyButtonaTap()
    {
        Game.Lobby.SetSocketNamespace = _nameSpaceStr;
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.DisplayRebuyinConfirmationPanel("Do you want to Rebuy " /*+ response.result.buyInChips + " from " */+ _rebuyAmount + " chips?", "Rebuy", "Cancel", () =>
        {
            UIManager.Instance.DisplayLoader("Please wait...");
            UIManager.Instance.RebuyInMessagePanel.btnAffirmativeAction.interactable = false;
            UIManager.Instance.SoundManager.OnButtonClick();
            UIManager.Instance.SocketGameManager.PlayerReBuyIn("", _TID, (socket, packet, args) =>
            {
                Debug.Log(Constants.PokerEvents.PlayerReBuyIn + " Response : " + packet.ToString());
                UIManager.Instance.HidePopup();
                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;
                PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);
                UIManager.Instance.HideLoader();
                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    TournamentEventCall();
                    UIManager.Instance.DisplayMessagePanel(resp.message, null);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message, null);
                }

            });
        }, () =>
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            UIManager.Instance.SocketGameManager.DeclinedReBuyIn("", _TID, (socket, packet, args) =>
            {
                Debug.Log(Constants.PokerEvents.DeclinedReBuyIn + " Response : " + packet.ToString());
                UIManager.Instance.HidePopup();
            });
            //UIManager.Instance.HidePopup();
        });
    }
    public void RegisterButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.gameType == GameType.Touranment)
        {
            UIManager.Instance.SocketGameManager.getRegisterTournament(TournamentDetailsId, _pokerGameType, (socket, packet, args) =>
            {

                Debug.Log("getRegisterTournament  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(resp1);
                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                    _registerButton.gameObject.SetActive(false);
                    _unRegisterButton.gameObject.SetActive(true);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }
            });
        }
        if (UIManager.Instance.gameType == GameType.sng)
        {
            UIManager.Instance.SocketGameManager.getRegisterSngTournament(TournamentDetailsId, _pokerGameType, (socket, packet, args) =>
            {
                Debug.Log("RegisterSngTournament  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(resp1);


                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                    _registerButton.gameObject.SetActive(false);
                    _unRegisterButton.gameObject.SetActive(true);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }

            });
        }
    }
    public void UnRegisterButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.gameType == GameType.Touranment)
        {
            UIManager.Instance.SocketGameManager.getUnRegisterTournament(TournamentDetailsId, _pokerGameType, (socket, packet, args) =>
            {

                Debug.Log("getRegisterTournament  : " + packet.ToString());

                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(resp1);

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                    _registerButton.gameObject.SetActive(true);
                    _unRegisterButton.gameObject.SetActive(false);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }
            });
        }
        if (UIManager.Instance.gameType == GameType.sng)
        {
            UIManager.Instance.SocketGameManager.getUnRegisterSngTournament(TournamentDetailsId, _pokerGameType, (socket, packet, args) =>
            {
                Debug.Log("getUnRegisterSngTournament  : " + packet.ToString());
                UIManager.Instance.HideLoader();
                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse<getTournamentInfoData> resp = JsonUtility.FromJson<PokerEventResponse<getTournamentInfoData>>(resp1);
                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                    _registerButton.gameObject.SetActive(true);
                    _unRegisterButton.gameObject.SetActive(false);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }
            });
        }
    }
}
