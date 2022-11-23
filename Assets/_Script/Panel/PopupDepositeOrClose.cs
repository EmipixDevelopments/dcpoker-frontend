using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupDepositeOrClose : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _depositButton;

    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _chipsText;
    

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(OnCloseButton);
        _depositButton.onClick.AddListener(OnDepositButton);

        if (UIManager.Instance.assetOfGame.SavedLoginData.isCash)
        {
            _moneyText.gameObject.SetActive(true);
            _chipsText.gameObject.SetActive(false);
            return;
        }
        
        _moneyText.gameObject.SetActive(false);
        _chipsText.gameObject.SetActive(true);
        _depositButton.gameObject.SetActive(false);
        
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
