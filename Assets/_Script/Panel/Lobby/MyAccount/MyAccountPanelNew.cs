using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyAccountPanelNew : MonoBehaviour
{
    [Header("Parent menu script")]
    [SerializeField] private LobbyPanelNew _lobbyPanelNew;

    [Header("Menu toggles")]
    public Toggle ProfileToggle;
    public Toggle MyBonusesToggle;
    public Toggle GamesHistoryToggle;
    public Toggle LeaderBoardToggle;
    public Toggle DepositsAndWithdrawalsToggle;
    public Toggle PurchaseHistoryToggle;

    [Header("Panel objects")]
    [SerializeField] private GameObject _panelProfile;
    [SerializeField] private GameObject _panelMyBonuses;
    [SerializeField] private GameObject _panelGameHistory;
    [SerializeField] private GameObject _panelLeaderBoard;
    [SerializeField] private GameObject _panelDepositsAndWithdrawals;
    [SerializeField] private GameObject _panelPurchaseHistory;

    private enum MyAccountPanel
    {
        Profile,
        MyBonuses,
        GameHistory,
        LeaderBoard,
        DepositsAndWithdrawals,
        PurchaseHistory
    }
    private MyAccountPanel _currentPanel;

    private void Start()
    {
        InitButtonsAndToggles();
    }

    public void UpdatePanel()
    {
        SwitchPanel(_currentPanel);
    }


    private void InitButtonsAndToggles()
    {
        ProfileToggle.onValueChanged.RemoveAllListeners();
        MyBonusesToggle.onValueChanged.RemoveAllListeners();
        GamesHistoryToggle.onValueChanged.RemoveAllListeners();
        LeaderBoardToggle.onValueChanged.RemoveAllListeners();
        DepositsAndWithdrawalsToggle.onValueChanged.RemoveAllListeners();
        PurchaseHistoryToggle.onValueChanged.RemoveAllListeners();

        ProfileToggle.onValueChanged.AddListener(OpenPanelProfile);
        MyBonusesToggle.onValueChanged.AddListener(OpenPanelMyBonuses);
        GamesHistoryToggle.onValueChanged.AddListener(OpenPanelGameHistory);
        LeaderBoardToggle.onValueChanged.AddListener(OpenPanelLeaderBoard);
        DepositsAndWithdrawalsToggle.onValueChanged.AddListener(OpenPanelDepositsAndWithdrawals);
        PurchaseHistoryToggle.onValueChanged.AddListener(OpenPanelPurchaseHistory);
    }

    private void SwitchPanel(MyAccountPanel nextPanel)
    {
        // default, close all windows
        CloseAll();

        switch (nextPanel)
        {
            case MyAccountPanel.Profile:
                _panelProfile.SetActive(true);
                break;
            case MyAccountPanel.MyBonuses:
                _panelMyBonuses.SetActive(true);
                break;
            case MyAccountPanel.GameHistory:
                _panelGameHistory.SetActive(true);
                break;
            case MyAccountPanel.LeaderBoard:
                _panelLeaderBoard.SetActive(true);
                break;
            case MyAccountPanel.DepositsAndWithdrawals:
                _panelDepositsAndWithdrawals.SetActive(true);
                break;
            case MyAccountPanel.PurchaseHistory:
                _panelPurchaseHistory.SetActive(true);
                break;
            default:
                break;
        }
        _currentPanel = nextPanel;

        // default need call update on main menu
        _lobbyPanelNew.UpdatePanel();
    }

    private void OpenPanelProfile(bool run)
    {
        if (!run) return;
        SwitchPanel(MyAccountPanel.Profile);
    }
    private void OpenPanelMyBonuses(bool run)
    {
        if (!run) return;
        SwitchPanel(MyAccountPanel.MyBonuses);
    }
    private void OpenPanelGameHistory(bool run)
    {
        if (!run) return;
        SwitchPanel(MyAccountPanel.GameHistory);
    }
    private void OpenPanelLeaderBoard(bool run)
    {
        if (!run) return;
        SwitchPanel(MyAccountPanel.LeaderBoard);
    }
    private void OpenPanelDepositsAndWithdrawals(bool run)
    {
        if (!run) return;
        SwitchPanel(MyAccountPanel.DepositsAndWithdrawals);
    }
    private void OpenPanelPurchaseHistory(bool run)
    {
        if (!run) return;
        SwitchPanel(MyAccountPanel.PurchaseHistory);
    }

    private void CloseAll()
    {
        _panelProfile.SetActive(false);
        _panelMyBonuses.SetActive(false);
        _panelGameHistory.SetActive(false);
        _panelLeaderBoard.SetActive(false);
        _panelDepositsAndWithdrawals.SetActive(false);
        _panelPurchaseHistory.SetActive(false);
    }

}
