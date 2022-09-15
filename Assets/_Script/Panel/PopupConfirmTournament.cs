using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupConfirmTournament : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    private PopupConfirmTournamentData _data;

    private void Start()
    {
        _confirmButton.onClick.AddListener(OnConfirmButton);
        _cancelButton.onClick.AddListener(OnCloseButton);
    }

    private void OnDestroy()
    {
        _confirmButton.onClick.RemoveListener(OnConfirmButton);
        _cancelButton.onClick.RemoveListener(OnCloseButton);
    }

    private void OnCloseButton()
    {
        gameObject.SetActive(false);    
    }
    
    private void OnConfirmButton()
    {
        _data?.ConfirmAction?.Invoke();
        OnCloseButton();
    }

    public void OpenPopup(PopupConfirmTournamentData data)
    {
        _data = data;
        if(_data == null)
            return;

        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        UpdateData();
    }

    private void UpdateData()
    {
        var values = _data.BuyInText.Split('+');
        
        var fistParam = "";
        float totalSum = 0;
        for (var i = 0; i < values.Length; i++)
        {
            var floatValue = float.Parse(values[i], CultureInfo.InvariantCulture.NumberFormat);
            fistParam = $"{fistParam} ${floatValue:0.00} " + (values.Length - 1 != i ? "+" : "");
            totalSum += floatValue;
        }
        
        _text.text = string.Format(_text.text, fistParam, $" ${totalSum:0.00} ");
    }
}

public class PopupConfirmTournamentData
{
    public Action ConfirmAction;
    public string BuyInText;
}
