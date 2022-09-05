using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanelNew : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private Toggle _tournamentsToggle;
    [SerializeField] private Toggle _sitNGoToggle;
    [SerializeField] private Toggle _texasHoldemToggle;
    [SerializeField] private Toggle _omahaToggle;
    [SerializeField] private Toggle _pLO5Toggle;
    [SerializeField] private Toggle _accountInfoInToggle;
    [Header("Top panel")]
    [SerializeField] private Toggle _homeToggle;
    [SerializeField] private Button _logout;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _aboutButton;
    [SerializeField] private Button _supportButton;
    [Header("Bottom panel buttons")]
    [SerializeField] private Button _termsOfServiceButton;
    [SerializeField] private Button _privacyPolicyButton;
    [SerializeField] private Button _responsibleGamingButton;

    [Header("Panel objects")]
    [SerializeField] private GameObject _panelHome;
    [SerializeField] private MyAccountPanelNew _panelMyAccount;
    [SerializeField] private GameObject _panelTournaments;
    [SerializeField] private GameObject _panelSitNGo;
    [SerializeField] private GameObject _panelTexasHoldem;
    [SerializeField] private GameObject _panelOmaha;
    [SerializeField] private GameObject _panelPlo5;
    [SerializeField] private GameObject _panelHowToPlay;
    [SerializeField] private GameObject _panelAbout;
    [SerializeField] private GameObject _panelTermsOfService;
    [SerializeField] private GameObject _panelPrivacyPolicy;
    [SerializeField] private GameObject _panelResponsibleGaming;
    [SerializeField] private GameObject _panelBottomMenu;

    [Space] public Messages Messages;

    private enum LobbyPanel
    {
        Home,
        MyAccount,
        Tournaments,
        SitNGo,
        TexasHoldem,
        Omaha,
        Plo5,
        HowToPlay,
        About,
        TermsOfService,
        PrivacyPolicy,
        ResponsibleGaming
    }
    private LobbyPanel _currentPanel;

    private void Start()
    {
        InitButtonsAndToggles();
    }


    private void OnEnable()
    {
        SwitchAtlHome(true);
    }

    public void UpdatePanel() 
    {
        SwitchPanel(_currentPanel);
    }

    public void OpenWitchdawPanel() 
    {
        _accountInfoInToggle.isOn = true;
        _currentPanel = LobbyPanel.MyAccount;
        UpdatePanel();
        _panelMyAccount.OpenWithdraw();
    }

    #region Menu
    private void InitButtonsAndToggles()
    {
        // init
        // clear all listeners
        _tournamentsToggle.onValueChanged.RemoveAllListeners();
        _sitNGoToggle.onValueChanged.RemoveAllListeners();
        _texasHoldemToggle.onValueChanged.RemoveAllListeners();
        _omahaToggle.onValueChanged.RemoveAllListeners();
        _pLO5Toggle.onValueChanged.RemoveAllListeners();
        _accountInfoInToggle.onValueChanged.RemoveAllListeners();

        _homeToggle.onValueChanged.RemoveAllListeners();
        _logout.onClick.RemoveAllListeners();
        _howToPlayButton.onClick.RemoveAllListeners();
        _aboutButton.onClick.RemoveAllListeners();
        _supportButton.onClick.RemoveAllListeners();

        _termsOfServiceButton.onClick.RemoveAllListeners();
        _privacyPolicyButton.onClick.RemoveAllListeners();
        _responsibleGamingButton.onClick.RemoveAllListeners();

        // set listeners
        _tournamentsToggle.onValueChanged.AddListener(SwitchAtTournaments);
        _sitNGoToggle.onValueChanged.AddListener(SwitchAtSitNGo);
        _texasHoldemToggle.onValueChanged.AddListener(SwitchAtTexasHoldem);
        _omahaToggle.onValueChanged.AddListener(SwitchAtOmaha);
        _pLO5Toggle.onValueChanged.AddListener(SwitchAtPlo5);
        _accountInfoInToggle.onValueChanged.AddListener(SwitchAtMyAccount);

        _homeToggle.onValueChanged.AddListener(SwitchAtlHome);
        _logout.onClick.AddListener(OnClickLogoutButton);
        _howToPlayButton.onClick.AddListener(SwitchAtHowToPlay);
        _aboutButton.onClick.AddListener(SwitchAtAbout);
        _supportButton.onClick.AddListener(SwitchAtSupport);

        _termsOfServiceButton.onClick.AddListener(SwitchAtTermsOfService);
        _privacyPolicyButton.onClick.AddListener(SwitchAtPrivacyPolicy);
        _responsibleGamingButton.onClick.AddListener(SwitchAtResponsibleGaming);
    }

    private void SwitchPanel(LobbyPanel nextWindow)
    {
        // default, close all windows
        CloseAll();

        switch (nextWindow)
        {
            case LobbyPanel.Home:
                _panelHome.SetActive(true);
                break;
            case LobbyPanel.MyAccount:
                _panelMyAccount.gameObject.SetActive(true);
                break;
            case LobbyPanel.Tournaments:
                _panelTournaments.SetActive(true);
                break;
            case LobbyPanel.SitNGo:
                _panelSitNGo.SetActive(true);
                break;
            case LobbyPanel.TexasHoldem:
                _panelTexasHoldem.SetActive(true);
                break;
            case LobbyPanel.Omaha:
                _panelOmaha.SetActive(true);
                break;
            case LobbyPanel.Plo5:
                _panelPlo5.SetActive(true);
                break;
            case LobbyPanel.HowToPlay:
                _panelHowToPlay.SetActive(true);
                break;
            case LobbyPanel.About:
                _panelAbout.SetActive(true);
                break;
            case LobbyPanel.TermsOfService:
                _panelTermsOfService.SetActive(true);
                break;
            case LobbyPanel.PrivacyPolicy:
                _panelPrivacyPolicy.SetActive(true);
                break;
            case LobbyPanel.ResponsibleGaming:
                _panelResponsibleGaming.SetActive(true);
                break;
            default:
                break;
        }
        _currentPanel = nextWindow;

        // need open in next frame for other panels to be opened
        StartCoroutine(ShowBottomMenu());
    }

    IEnumerator ShowBottomMenu()
    {
        yield return new WaitForEndOfFrame();
        _panelBottomMenu.SetActive(true);
    }

    private void SwitchAtlHome(bool run)
    {
        if (!run) return;
        SwitchPanel(LobbyPanel.Home);
    }
    private void SwitchAtMyAccount(bool run)
    {
        if (!run) return;
        SwitchPanel(LobbyPanel.MyAccount);
    }
    private void SwitchAtTournaments(bool run)
    {
        if (!run) return;
        SwitchPanel(LobbyPanel.Tournaments);
    }
    private void SwitchAtSitNGo(bool run)
    {
        if (!run) return;
        SwitchPanel(LobbyPanel.SitNGo);
    }
    private void SwitchAtTexasHoldem(bool run)
    {
        if (!run) return;
        SwitchPanel(LobbyPanel.TexasHoldem);
    }
    private void SwitchAtOmaha(bool run)
    {
        if (!run) return;
        SwitchPanel(LobbyPanel.Omaha);
    }
    private void SwitchAtPlo5(bool run)
    {
        if (!run) return;
        SwitchPanel(LobbyPanel.Plo5);
    }
    private void SwitchAtHowToPlay()
    {
        SwitchPanel(LobbyPanel.HowToPlay);
    }
    private void SwitchAtAbout()
    {
        SwitchPanel(LobbyPanel.About);
    }
    private void SwitchAtSupport()
    {
        UIManager.Instance.PanelContactSupportPopup.Open();
    }
    private void SwitchAtTermsOfService()
    {
        SwitchPanel(LobbyPanel.TermsOfService);
    }
    private void SwitchAtPrivacyPolicy()
    {
        SwitchPanel(LobbyPanel.PrivacyPolicy);
    }
    private void SwitchAtResponsibleGaming()
    {
        SwitchPanel(LobbyPanel.ResponsibleGaming);
    }

    private void CloseAll()
    {
        _panelHome.SetActive(false);
        _panelMyAccount.gameObject.SetActive(false);
        _panelTournaments.SetActive(false);
        _panelSitNGo.SetActive(false);
        _panelTexasHoldem.SetActive(false);
        _panelOmaha.SetActive(false);
        _panelPlo5.SetActive(false);
        _panelHowToPlay.SetActive(false);
        _panelAbout.SetActive(false);
        _panelTermsOfService.SetActive(false);
        _panelPrivacyPolicy.SetActive(false);
        _panelResponsibleGaming.SetActive(false);
        _panelBottomMenu.SetActive(false);
    }
    #endregion

    #region Logout
    private void OnClickLogoutButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.IsWebGLAffiliat)
#if UNITY_EDITOR
            UIManager.Instance.DisplayConfirmationPanel("Are you sure you want to exit? ", OnLogOutDone);
#else
            OnLogOutDone();
#endif
        else
            UIManager.Instance.DisplayConfirmationPanel("Are you sure you want to Logout? ", OnLogOutDone);
    }

    private void OnLogOutDone()
    {
        UIManager.Instance.SocketGameManager.LogOutPlayer((socket, packet, args) =>
        {
            Debug.Log("LogOutPlayer  : " + packet.ToString());
            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                if (UIManager.Instance.IsWebGLAffiliat)
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    ExternalCallClass.Instance.ExitGame();
                }
                else
                {
                    StopCoroutine(LogoutFunction(0f));
                    StartCoroutine(LogoutFunction(0f));
                }
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }

    IEnumerator LogoutFunction(float timer)
    {
        UIManager.Instance.isLogOut = true;
        Game.Lobby.socketManager.Close();
        UIManager.Instance.DisplayLoader("");
        Game.Lobby.socketManager.Open();
        Game.Lobby.ConnectToSocket();
        UIManager.Instance.tableManager.RemoveAllMiniTableData();
        UIManager.Instance.SoundManager.stopBgSound();
        yield return new WaitForSeconds(timer);
        UIManager.Instance.Reset(false);
    }
    #endregion
}
