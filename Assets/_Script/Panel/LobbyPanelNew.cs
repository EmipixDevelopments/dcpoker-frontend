using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanelNew : MonoBehaviour
{
    [Header("Menu toggles")]
    [SerializeField] private Toggle _tournamentsToggle;
    [SerializeField] private Toggle _sitNGoToggle;
    [SerializeField] private Toggle _texasHoldemToggle;
    [SerializeField] private Toggle _omahaToggle;
    [SerializeField] private Toggle _pLO5Toggle;
    [SerializeField] private Toggle _accountInfoInToggle;
    [Header("Top panel buttons")]
    [SerializeField] private Button _homeButton;
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
    [SerializeField] private GameObject _panelMyAccount;
    [SerializeField] private GameObject _panelTournaments;
    [SerializeField] private GameObject _panelSitNGo;
    [SerializeField] private GameObject _panelTexasHoldem;
    [SerializeField] private GameObject _panelOmaha;
    [SerializeField] private GameObject _panelPlo5;
    [SerializeField] private GameObject _panelHowToPlay;
    [SerializeField] private GameObject _panelAbout;
    [SerializeField] private GameObject _panelSupport;
    [SerializeField] private GameObject _panelTermsOfService;
    [SerializeField] private GameObject _panelPrivacyPolicy;
    [SerializeField] private GameObject _panelResponsibleGaming;


    private void Start()
    {
        InitButtonsAndToggles();
    }


    private void OnEnable()
    {
        OpenPanelHome();
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

        _homeButton.onClick.RemoveAllListeners();
        _logout.onClick.RemoveAllListeners();
        _howToPlayButton.onClick.RemoveAllListeners();
        _aboutButton.onClick.RemoveAllListeners();
        _supportButton.onClick.RemoveAllListeners();

        _termsOfServiceButton.onClick.RemoveAllListeners();
        _privacyPolicyButton.onClick.RemoveAllListeners();
        _responsibleGamingButton.onClick.RemoveAllListeners();

        // set listeners
        _tournamentsToggle.onValueChanged.AddListener(OpenPanelTournaments);
        _sitNGoToggle.onValueChanged.AddListener(OpenPanelSitNGo);
        _texasHoldemToggle.onValueChanged.AddListener(OpenPanelTexasHoldem);
        _omahaToggle.onValueChanged.AddListener(OpenPanelOmaha);
        _pLO5Toggle.onValueChanged.AddListener(OpenPanelPlo5);
        _accountInfoInToggle.onValueChanged.AddListener(OpenPanelMyAccount);

        _homeButton.onClick.AddListener(OpenPanelHome);
        _logout.onClick.AddListener(OnClickLogoutButton);
        _howToPlayButton.onClick.AddListener(OpenPanelHowToPlay);
        _aboutButton.onClick.AddListener(OpenPanelAbout);
        _supportButton.onClick.AddListener(OpenPanelSupport);

        _termsOfServiceButton.onClick.AddListener(OpenPanelTermsOfService);
        _privacyPolicyButton.onClick.AddListener(OpenPanelPrivacyPolicy);
        _responsibleGamingButton.onClick.AddListener(OpenPanelResponsibleGaming);
    }

    private void OpenPanelHome()
    {
        CloseAll();
        _panelHome.SetActive(true);
    }
    private void OpenPanelMyAccount(bool run)
    {
        if (!run) return;
        CloseAll();
        _panelMyAccount.SetActive(true);
    }
    private void OpenPanelTournaments(bool run)
    {
        if (!run) return;
        CloseAll();
        _panelTournaments.SetActive(true);
    }
    private void OpenPanelSitNGo(bool run)
    {
        if (!run) return;
        CloseAll();
        _panelSitNGo.SetActive(true);
    }
    private void OpenPanelTexasHoldem(bool run)
    {
        if (!run) return;
        CloseAll();
        _panelTexasHoldem.SetActive(true);
    }
    private void OpenPanelOmaha(bool run)
    {
        if (!run) return;
        CloseAll();
        _panelOmaha.SetActive(true);
    }
    private void OpenPanelPlo5(bool run)
    {
        if (!run) return;
        CloseAll();
        _panelPlo5.SetActive(true);
    }
    private void OpenPanelHowToPlay()
    {
        CloseAll();
        _panelHowToPlay.SetActive(true);
    }
    private void OpenPanelAbout()
    {
        CloseAll();
        _panelAbout.SetActive(true);
    }
    private void OpenPanelSupport()
    {
        CloseAll();
        _panelSupport.SetActive(true);
    }
    private void OpenPanelTermsOfService()
    {
        CloseAll();
        _panelTermsOfService.SetActive(true);
    }
    private void OpenPanelPrivacyPolicy()
    {
        CloseAll();
        _panelPrivacyPolicy.SetActive(true);
    }
    private void OpenPanelResponsibleGaming()
    {
        CloseAll();
        _panelResponsibleGaming.SetActive(true);
    }

    private void CloseAll()
    {
        _panelHome.SetActive(false);
        _panelMyAccount.SetActive(false);
        _panelTournaments.SetActive(false);
        _panelSitNGo.SetActive(false);
        _panelTexasHoldem.SetActive(false);
        _panelOmaha.SetActive(false);
        _panelPlo5.SetActive(false);
        _panelHowToPlay.SetActive(false);
        _panelAbout.SetActive(false);
        _panelSupport.SetActive(false);
        _panelTermsOfService.SetActive(false);
        _panelPrivacyPolicy.SetActive(false);
        _panelResponsibleGaming.SetActive(false);
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
                    StopCoroutine("LogoutFunction");
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
