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
    private void CloseAll()
    {
        _panelHome.SetActive(false);
        _panelMyAccount.SetActive(false);
        _panelTournaments.SetActive(false);
        _panelSitNGo.SetActive(false);
        _panelTexasHoldem.SetActive(false);
        _panelOmaha.SetActive(false);
        _panelPlo5.SetActive(false);
    }
}
