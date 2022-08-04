using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelInformationAboutMoneyOnAccountPopup : MonoBehaviour
{
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _deleteAccountlButton;
    [SerializeField] private Button _witchdawButton;
    [Space]
    [SerializeField] private TextMeshProUGUI _InfoText;


    private void Start()
    {
        _cancelButton.onClick.RemoveAllListeners();
        _deleteAccountlButton.onClick.RemoveAllListeners();
        _witchdawButton.onClick.RemoveAllListeners();

        _cancelButton.onClick.AddListener(OnClickCancelButton);
        _deleteAccountlButton.onClick.AddListener(OnClickDeleteAccountButton);
        _witchdawButton.onClick.AddListener(OnClickWitchdawButton);
    }

    private void OnEnable()
    {
        OpenOrClose();
    }

    private void OpenOrClose()
    {
        double cash = UIManager.Instance.assetOfGame.SavedLoginData.cash;
        if (cash > 0)
        {
            string text =
                $"your remaining assets in the account \n" +
                $"<b>${cash:#.##}</b>";

            _InfoText.text = text;
        }
        else
        {
            OnClickDeleteAccountButton();
        }
    }

    private void OnClickCancelButton()
    {
        Close();
    }

    private void OnClickDeleteAccountButton()
    {
        UIManager.Instance.DeleteAccountPopup.Open();
        Close();
    }

    private void OnClickWitchdawButton()
    {
        UIManager.Instance.LobbyPanelNew.OpenWitchdawPanel();
        Close();
    }


    private void Close() 
    {
        gameObject.SetActive(false);
    }
}
