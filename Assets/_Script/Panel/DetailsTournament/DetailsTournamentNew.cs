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
    [SerializeField] private TextMeshProUGUI _startInInfoText;
    [SerializeField] private Button RegisterButton;
    [SerializeField] private Button UnRegisterButton;
    [SerializeField] private Button ReBuyButton;

    [Header("Left panel")]
    [SerializeField] private GameObject _leftPanel;


    private int    _selectedTab = 0;
    private string _tournamentDetailsId = "";
    private string _pokerGameType = "";
    private string TID = "";
    private string nameSpaceStr = "";
    private long   RebuyAmount;
    private double totalSeconds;

    private void Start()
    {
        ReBuyButton.onClick.RemoveAllListeners();
        RegisterButton.onClick.RemoveAllListeners();
        UnRegisterButton.onClick.RemoveAllListeners();

        ReBuyButton.onClick.AddListener(RebuyButtonaTap);
        RegisterButton.onClick.AddListener(RegisterButtonTap);
        UnRegisterButton.onClick.AddListener(UnRegisterButtonTap);
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
        Constants.Poker.TournamentId = _tournamentDetailsId;
        _pokerGameType = pokerGameType;
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
        _startInInfoText.text = "";
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
                    nameSpaceStr = resp.result.namespaceString;
                    TID = resp.result.id;
                    RebuyAmount = resp.result.rebuyAmount;

                    if (resp.result.allowRebuy)
                    {
                        RegisterButton.gameObject.SetActive(false);
                        UnRegisterButton.gameObject.SetActive(false);
                        ReBuyButton.Open();
                        totalSeconds = resp.result.remainRebuySec;
                    }
                    else
                    {
                        RegisterButton.gameObject.SetActive(!resp.result.isRegistered);
                        UnRegisterButton.gameObject.SetActive(resp.result.isRegistered);
                        ReBuyButton.Close();
                    }
                    if (resp.result.registrationStatus.Equals("open") || resp.result.registrationStatus.Equals("Open"))
                    {
                        RegisterButton.interactable = true;
                    }
                    else
                    {
                        RegisterButton.interactable = false;
                        UnRegisterButton.interactable = false;
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

                    nameSpaceStr = resp.result.namespaceString;
                    TID = resp.result.id;
                    RebuyAmount = resp.result.rebuyAmount;
                    if (resp.result.allowRebuy)
                    {
                        RegisterButton.gameObject.SetActive(false);
                        UnRegisterButton.gameObject.SetActive(false);
                        ReBuyButton.Open();
                        totalSeconds = resp.result.remainRebuySec;
                    }
                    else
                    {
                        RegisterButton.gameObject.SetActive(!resp.result.isRegistered);
                        UnRegisterButton.gameObject.SetActive(resp.result.isRegistered);
                        ReBuyButton.Close();
                    }
                    if (resp.result.registrationStatus.Equals("open") || resp.result.registrationStatus.Equals("Open"))
                    {
                        RegisterButton.interactable = true;
                    }
                    else
                    {
                        RegisterButton.interactable = false;
                    }
                    //Invoke("SngTournamentEventCall", 5);
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
        Game.Lobby.SetSocketNamespace = nameSpaceStr;
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.DisplayRebuyinConfirmationPanel("Do you want to Rebuy " /*+ response.result.buyInChips + " from " */+ RebuyAmount + " chips?", "Rebuy", "Cancel", () =>
        {
            UIManager.Instance.DisplayLoader("Please wait...");
            UIManager.Instance.RebuyInMessagePanel.btnAffirmativeAction.interactable = false;
            UIManager.Instance.SoundManager.OnButtonClick();
            UIManager.Instance.SocketGameManager.PlayerReBuyIn("", TID, (socket, packet, args) =>
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
            UIManager.Instance.SocketGameManager.DeclinedReBuyIn("", TID, (socket, packet, args) =>
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
                    RegisterButton.gameObject.SetActive(false);
                    UnRegisterButton.gameObject.SetActive(true);
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
                    RegisterButton.gameObject.SetActive(false);
                    UnRegisterButton.gameObject.SetActive(true);
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
                    RegisterButton.gameObject.SetActive(true);
                    UnRegisterButton.gameObject.SetActive(false);
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
                    RegisterButton.gameObject.SetActive(true);
                    UnRegisterButton.gameObject.SetActive(false);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }
            });
        }
    }
}
