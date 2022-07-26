using System.Collections;
using UnityEngine;

public class LobbyPanelNew : MonoBehaviour
{
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


    public void OpenPanelHome()
    {
        CloseAll();
        _panelHome.SetActive(true);
    }
    public void OpenPanelMyAccount()
    {
        CloseAll();
        _panelMyAccount.SetActive(true);
    }
    public void OpenPanelTournaments()
    {
        CloseAll();
        _panelTournaments.SetActive(true);
    }
    public void OpenPanelSitNGo()
    {
        CloseAll();
        _panelSitNGo.SetActive(true);
    }
    public void OpenPanelTexasHoldem()
    {
        CloseAll();
        _panelTexasHoldem.SetActive(true);
    }
    public void OpenPanelOmaha()
    {
        CloseAll();
        _panelOmaha.SetActive(true);
    }
    public void OpenPanelPlo5()
    {
        CloseAll();
        _panelPlo5.SetActive(true);
    }
    public void OpenPanelHowToPlay()
    {
        CloseAll();
        _panelHowToPlay.SetActive(true);
    }
    public void OpenPanelAbout()
    {
        CloseAll();
        _panelAbout.SetActive(true);
    }
    public void OpenPanelSupport()
    {
        CloseAll();
        _panelSupport.SetActive(true);
    }
    public void OpenPanelTermsOfService()
    {
        CloseAll();
        _panelTermsOfService.SetActive(true);
    }
    public void OpenPanelPrivacyPolicy()
    {
        CloseAll();
        _panelPrivacyPolicy.SetActive(true);
    }
    public void OpenPanelResponsibleGaming()
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

    private void OnEnable()
    {
        OpenPanelHome();
    }

    #region Logout
    public void OnClickLogoutButton()
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
