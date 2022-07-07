using UnityEngine;

public class LobbyPanelNew : MonoBehaviour
{
    [SerializeField] private GameObject _panelHome;
    [SerializeField] private GameObject _panelMyAccount;

    //--- Test zone ---//
    private void CloseAll()
    {
        _panelHome.SetActive(false);
        _panelMyAccount.SetActive(false);
    }
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
    //-------------------
}
