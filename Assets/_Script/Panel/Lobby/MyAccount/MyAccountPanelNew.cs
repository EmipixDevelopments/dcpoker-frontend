using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyAccountPanelNew : MonoBehaviour
{
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

    private void OnEnable()
    {
        ProfileToggle.isOn = true;
    }

    private void Start()
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

    private void OpenPanelProfile(bool run)
    {
        if (!run) return;
        CloseAll();
        _panelProfile.SetActive(true);
    }
    private void OpenPanelMyBonuses(bool run)
    {
        if (!run) return;
        CloseAll();
        _panelMyBonuses.SetActive(true);
    }
    private void OpenPanelGameHistory(bool run)
    {
        if (!run) return;
        CloseAll();
        _panelGameHistory.SetActive(true);
    }
    private void OpenPanelLeaderBoard(bool run)
    {
        if (!run) return;
        CloseAll();
        _panelLeaderBoard.SetActive(true);
    }
    private void OpenPanelDepositsAndWithdrawals(bool run)
    {
        if (!run) return;
        CloseAll();
        _panelDepositsAndWithdrawals.SetActive(true);
    }
    private void OpenPanelPurchaseHistory(bool run)
    {
        if (!run) return;
        CloseAll();
        _panelPurchaseHistory.SetActive(true);
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
