using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelDepositsAndWithdrawals : MonoBehaviour
{
    [Header("Parent menu script")]
    [SerializeField] private MyAccountPanelNew _myAccountPanelNew;

    [Header("Toggle and Buttons")]
    [SerializeField] private Toggle _depositToggle;
    [SerializeField] private Toggle _withdrawToggle;
    [SerializeField] private Button _creditCardButton;
    [Header("Panels")]
    [SerializeField] private GameObject _depositStartPanel;
    [SerializeField] private GameObject _cardPanel;

    private enum DepositsAndWithdrawalsPanels 
    {
        DepositStartPanel,
        CardPanel
    }
    private DepositsAndWithdrawalsPanels _currentPanel;

    void Start()
    {
        InitButtonsAndToggles();
    }

    public void UpdatePanel()
    {
        SwitchPanel(_currentPanel);
    }

    private void InitButtonsAndToggles()
    {
        _depositToggle.onValueChanged.RemoveAllListeners();
        _creditCardButton.onClick.RemoveAllListeners();

        _depositToggle.onValueChanged.AddListener(OpenDepositStartPanel);
        _creditCardButton.onClick.AddListener(OpenCardPanel);
    }

    private void SwitchPanel(DepositsAndWithdrawalsPanels currentPanel)
    {
        // default, close all windows
        CloseAll();
        switch (currentPanel)
        {
            case DepositsAndWithdrawalsPanels.DepositStartPanel:
                _depositStartPanel.SetActive(true);
                break;
            case DepositsAndWithdrawalsPanels.CardPanel:
                _cardPanel.SetActive(true);
                break;
            default:
                break;
        }
        _currentPanel = currentPanel;
        _myAccountPanelNew.UpdatePanel();
    }

    private void OpenDepositStartPanel(bool check)
    {
        if (!check) return;
        SwitchPanel(DepositsAndWithdrawalsPanels.DepositStartPanel);
    }

    private void OpenCardPanel()
    {
        SwitchPanel(DepositsAndWithdrawalsPanels.CardPanel);
    }

    private void CloseAll()
    {
        _depositStartPanel.SetActive(false);
        _cardPanel.SetActive(false);
    }
}
