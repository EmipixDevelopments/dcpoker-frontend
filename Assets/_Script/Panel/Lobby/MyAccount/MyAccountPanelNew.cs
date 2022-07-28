using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAccountPanelNew : MonoBehaviour
{
    [SerializeField] private GameObject _panelProfile;
    [SerializeField] private GameObject _panelMyBonuses;
    [SerializeField] private GameObject _panelGameHistory;
    [SerializeField] private GameObject _panelLeaderBoard;
    [SerializeField] private GameObject _panelDepositsAndWithdrawals;
    [SerializeField] private GameObject _panelPurchaseHistory;


    public void OpenPanelProfile() 
    {
        CloseAll();
        _panelProfile.SetActive(true);
    }
    public void OpenPanelMyBonuses() 
    {
        CloseAll();
        _panelMyBonuses.SetActive(true);
    }
    public void OpenPanelGameHistory()
    {
        CloseAll();
        _panelGameHistory.SetActive(true);
    }
    public void OpenPanelLeaderBoard()
    {
        CloseAll();
        _panelLeaderBoard.SetActive(true);
    }
    public void OpenPanelDepositsAndWithdrawals()
    {
        CloseAll();
        _panelDepositsAndWithdrawals.SetActive(true);
    }
    public void OpenPanelPurchaseHistory()
    {
        CloseAll();
        _panelPurchaseHistory.SetActive(true);
    }

    public void CloseAll() 
    {
        _panelProfile.SetActive(false);
        _panelMyBonuses.SetActive(false);
        _panelGameHistory.SetActive(false);
        _panelLeaderBoard.SetActive(false);
        _panelDepositsAndWithdrawals.SetActive(false);
        _panelPurchaseHistory.SetActive(false);
    }
}
