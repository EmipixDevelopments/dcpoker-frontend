<<<<<<< Updated upstream
﻿using System.Collections;
=======
﻿using System;
using System.Collections;
using TMPro;
>>>>>>> Stashed changes
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanelNew : MonoBehaviour
{
    [SerializeField] private TMP_Text textAppVersion;

    [Header("Menu")] [SerializeField] private Toggle _tournamentsToggle;
    [SerializeField] private Toggle _sitNGoToggle;
    [SerializeField] private Toggle _texasHoldemToggle;
    [SerializeField] private Toggle _omahaToggle;
    [SerializeField] private Toggle _pLO5Toggle;
    [SerializeField] private Toggle _accountInfoInToggle;
    [Header("Top panel")] [SerializeField] private Toggle _homeToggle;
    [SerializeField] private Button _logout;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _aboutButton;
    [SerializeField] private Button _supportButton;
<<<<<<< Updated upstream
    [Header("Bottom panel buttons")]
    [SerializeField] private Button _termsOfServiceButton;
=======
    [SerializeField] private ToggleImageNormal _soundToggleImageNormal;

    [Header("Bottom panel buttons")] [SerializeField]
    private Button _termsOfServiceButton;

>>>>>>> Stashed changes
    [SerializeField] private Button _privacyPolicyButton;
    [SerializeField] private Button _responsibleGamingButton;

    [Header("Panel objects")] [SerializeField]
    private GameObject _panelHome;

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
<<<<<<< Updated upstream
=======
        _scrollRect = GetComponent<ScrollRect>();

>>>>>>> Stashed changes
        InitButtonsAndToggles();
        textAppVersion.text = "v" + Application.version;
//        UserWalletBalance();
    }


    private void OnEnable()
    {
        SwitchAtlHome(true);
    }

    public void UpdatePanel() 
    {
        SwitchPanel(_currentPanel);
<<<<<<< Updated upstream
=======

        //SwitchAtlHome(true);

        _soundToggleImageNormal.AddListener(OnChangeSoundToggle);
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

=======
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

    [ContextMenu("UserWalletBalance")]
    private void UserWalletBalance()
    {
        string privateKeyString = JsonUtility.ToJson(UIManager.Instance.assetOfGame.SavedLoginData.privateKey);
        Debug.LogError("privateKeyString " + privateKeyString);
        if (UIManager.Instance.SocketGameManager.HasInternetConnection())
        {
            UIManager.Instance.SoundManager.OnButtonClick();
//            if (IsLoginDetailValid())
            {
                UIManager.Instance.DisplayLoader("");

                UIManager.Instance.SocketGameManager.UserWalletBalance(UIManager.Instance.assetOfGame.SavedLoginData.Username, UIManager.Instance.assetOfGame.SavedLoginData.PlayerId, UIManager.Instance.assetOfGame.SavedLoginData.publicKey
                    , privateKeyString, (socket, packet, args) =>
                    {
                        Debug.Log("UserWalletBalance  => " + packet.ToString());

                        UIManager.Instance.HideLoader();
                        JSONArray arr = new JSONArray(packet.ToString());
                        string Source;
                        Source = arr.getString(arr.length() - 1);
                        var resp = Source;
                        PokerEventResponse registrationResp = JsonUtility.FromJson<PokerEventResponse>(resp);

//                    RecoveryPhraseEventResponse recoveryPhraseEventResponse = JsonUtility.FromJson<RecoveryPhraseEventResponse>(registrationResp.result);

//                    Debug.LogError("Register Player Response : " + registrationResp + " | " + recoveryPhraseEventResponse);

                        if (registrationResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                        {
//                            _phrase = registrationResp.result.recoveryPhrase;
//                            textRecoveryPhraseFromServer.text = _phrase;
//                            panelPassword.SetActive(false);
//                            panelRecovery.SetActive(true);
                        }
                        else if (registrationResp.message == "Username already taken.")
                        {
//                            panelUsername.SetActive(true);
//                            panelPassword.SetActive(false);
                        }
                        else
                        {
                            UIManager.Instance.DisplayMessagePanel(registrationResp.message, null);
                        }
                    });
            }
        }
    }

    #region Menu

    private void InitButtonsAndToggles()
    {
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    }

=======

        _soundToggleImageNormal.SetActive(PlayerPrefs.GetInt("Sound") > 0);
    }


    private void OnChangeSoundToggle(bool active)
    {
        var uiManager = UIManager.Instance;

        if (uiManager)
            uiManager.SoundManager.SetSoundActive(active);
        ;
    }

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        _currentPanel = nextWindow;

        // need open in next frame for other panels to be opened
        StartCoroutine(ShowBottomMenu());
=======

        if (_currentPanel != nextWindow)
            _onSwitchLobbyPanel?.Invoke(nextWindow);

        _currentPanel = nextWindow;
        ShowBottomMenu();

        // need open in next frame for other panels to be opened
        //StartCoroutine(ShowBottomMenu());
    }

    private void ShowBottomMenu()
    {
        var isHomePage = _currentPanel == LobbyPanel.Home;
        _background.SetActiveBackgroundPanel(!isHomePage);
        _background.SetActiveChipsBottomImage(isHomePage || (int) _currentPanel > 6);

        _panelBottomMenu.gameObject.SetActive(true);
>>>>>>> Stashed changes
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

    public void OnClickLogoutButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.IsWebGLAffiliat)
#if UNITY_EDITOR
            UIManager.Instance.DisplayConfirmationPanel("Are you sure you want to exit? ", OnLogOutDone);
#else
//            OnLogOutDone();
            UIManager.Instance.DisplayConfirmationPanel("Are you sure you want to Logout? ", OnLogOutDone);
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
<<<<<<< Updated upstream
}
=======

    public void UpdateUi()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
    }

    public void UpdateAccountUi()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_panelMyAccount.GetComponent<RectTransform>());
        //_panelMyAccount.
    }

    public void AddOnSwitchLobbyPanelListener(Action<LobbyPanel> action)
    {
        _onSwitchLobbyPanel += action;
    }

    public void RemoveOnSwitchLobbyPanelListener(Action<LobbyPanel> action)
    {
        _onSwitchLobbyPanel -= action;
    }

    public ScrollRect GetScrollRect() => _scrollRect;
}
>>>>>>> Stashed changes
