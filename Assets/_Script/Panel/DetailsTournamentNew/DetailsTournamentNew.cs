using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class DetailsTournamentNew : MonoBehaviour
{
    [Header("Right panel")]
    [SerializeField] private Button _summaryButton;
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
    [SerializeField] private GameObject _leftPanel;


    private int    _selectedTab = 0;
    private string _tournamentDetailsId = "";
    private string _pokerGameType = "";
    private string _TID = "";
    private string _nameSpaceStr = "";
    private long   _rebuyAmount;
    private double totalSeconds;

    private void Start()
    {
        _reBuyButton.onClick.RemoveAllListeners();
        _registerButton.onClick.RemoveAllListeners();
        _unRegisterButton.onClick.RemoveAllListeners();

        _reBuyButton.onClick.AddListener(RebuyButtonaTap);
        _registerButton.onClick.AddListener(RegisterButtonTap);
        _unRegisterButton.onClick.AddListener(UnRegisterButtonTap);
    }

    void OnEnable()
    {
        ResetData();
        TournamentEventCall();
    }

    void OnDisable()
    {
        CancelInvoke();
    }

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

    public void GetDetailsTournamentButtonTap(string TournamentId, string pokerGameType)
    {
        this.Close();
        _tournamentDetailsId = TournamentId;
        _pokerGameType = pokerGameType;
        Constants.Poker.TournamentId = _tournamentDetailsId;
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
        if (_tournamentDetailsId != "")
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
        if (_tournamentDetailsId != "")
        {
            UIManager.Instance.SocketGameManager.getTournamentInfo(_tournamentDetailsId, _pokerGameType, (socket, packet, args) =>
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

                    DateTime dateTime = DateTime.Parse(resp.result.dateTime);
                    if (dateTime > DateTime.Now)
                    {
                        _registerButtonText.text = "late register";
                        _registerButtonSprite.sprite = _greenSprite;

                        _startInText.gameObject.SetActive(false);
                    }
                    else
                    {
                        _registerButtonText.text = "register";
                        _registerButtonSprite.sprite = _orangeSprite;

                        _startInText.gameObject.SetActive(true);

                    if (resp.result.players <= resp.result.max_players && resp.result.status.ToLower() != "cancel")
                        {
                            _startInText.text = $"Will start when <b>{resp.result.max_players - resp.result.players} playeres will join</b>";
                        }
                        else
                        {
                            TimeSpan divDataTime = DateTime.Now.Subtract(dateTime);
                            _startInText.text = $"Will start in <b>{divDataTime.Days} Days : {divDataTime.Hours} Hours : {divDataTime.Minutes} Minutes</b>";
                        }
                    }

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
        if (_tournamentDetailsId != "")
        {
            UIManager.Instance.SocketGameManager.getSngTournamentInfo(_tournamentDetailsId, _pokerGameType, (socket, packet, args) =>
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


                    DateTime dateTime = DateTime.Parse(resp.result.dateTime);
                    if (dateTime > DateTime.Now)
                    {
                        _registerButtonText.text = "late register";
                        _registerButtonSprite.sprite = _greenSprite;

                        _startInText.gameObject.SetActive(false);
                    }
                    else
                    {
                        _registerButtonText.text = "register";
                        _registerButtonSprite.sprite = _orangeSprite;

                        _startInText.gameObject.SetActive(true);

                        if (resp.result.players <= resp.result.max_players && resp.result.status.ToLower() != "cancel")
                        {
                            _startInText.text = $"Will start when <b>{resp.result.max_players - resp.result.players} playeres will join</b>";
                        }
                        else
                        {
                            TimeSpan divDataTime = DateTime.Now.Subtract(dateTime);
                            _startInText.text = $"Will start in <b>{divDataTime.Days} Days : {divDataTime.Hours} Hours : {divDataTime.Minutes} Minutes</b>";
                        }
                    }


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
            UIManager.Instance.SocketGameManager.getRegisterTournament(_tournamentDetailsId, _pokerGameType, (socket, packet, args) =>
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
            UIManager.Instance.SocketGameManager.getRegisterSngTournament(_tournamentDetailsId, _pokerGameType, (socket, packet, args) =>
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
            UIManager.Instance.SocketGameManager.getUnRegisterTournament(_tournamentDetailsId, _pokerGameType, (socket, packet, args) =>
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
            UIManager.Instance.SocketGameManager.getUnRegisterSngTournament(_tournamentDetailsId, _pokerGameType, (socket, packet, args) =>
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
