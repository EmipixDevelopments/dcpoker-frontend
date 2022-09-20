using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupDepositeOrClose : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _depositButton;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(OnCloseButton);
        _depositButton.onClick.AddListener(OnDepositButton);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnCloseButton);
        _depositButton.onClick.RemoveListener(OnDepositButton);
    }

    public bool Open(double needAmount)
    {
        double amount;

        var uiManger = UIManager.Instance;
        
        if (!uiManger)
            return false;
        
        amount = uiManger.assetOfGame.SavedLoginData.isCash ? 
            uiManger.assetOfGame.SavedLoginData.cash : 
            uiManger.assetOfGame.SavedLoginData.chips;

        if (amount < needAmount)
        {
            gameObject.SetActive(true);
            return false;
        }

        return true;
    }

    private void OnDepositButton()
    {
        UIManager.Instance.LobbyPanelNew.OpenWitchdawPanel();
        gameObject.SetActive(false);
    }

    private void OnCloseButton()
    {
        gameObject.SetActive(false);
    }
}
