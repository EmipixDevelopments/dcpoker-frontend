using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class PurchaseHistoryElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dataText;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private TextMeshProUGUI _typeText;
    [SerializeField] private TextMeshProUGUI _fromToText;

    [SerializeField] private TableListPurchaseHistoryColors _tableListPurchaseHistoryColors; 

    private Transaction _transaction;

    public void SetData(Transaction transaction)
    {
        _transaction = transaction;
        
        _dataText.text = CheckStringData(GetData());
        _amountText.text = CheckStringData(GetAmountText());
        _typeText.text = CheckStringData(_transaction.type);
        _fromToText.text = CheckStringData(_transaction.message);
    }

    private string GetData()
    {
        return ParsingDateTime(_transaction.createdAt);
    }

    private string GetAmountText()
    {
        var amount = _transaction.cash;
        var dop = "$";
        
        if (string.IsNullOrEmpty(amount))
        {
            amount =  _transaction.chips;
            dop = "chips ";
        }

        if (int.Parse(amount) > 0)
        {
            _typeText.color = _tableListPurchaseHistoryColors.Green;
            dop = '+' + dop;
        }
        else
        {
            _typeText.color = _tableListPurchaseHistoryColors.Red;
            dop = '-' + dop;
        }

        return dop + amount;
    }
    
    private string CheckStringData(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return $"---";
        }
        return text;
    }

    private string ParsingDateTime(string dateTime)
    {
        if (string.IsNullOrEmpty(dateTime))
            return null;
        
        string result = "";
        string year = "";
        DateTime dt = DateTime.Parse(dateTime);
        if (dt.Year != DateTime.Now.Year)
        {
            year = $"{dt.Year} ";
        }
        result = $"{year}{dt.ToString("MMM dd", CultureInfo.CreateSpecificCulture("en-US"))} / {dt.ToString("HH:mm")}";
        return result;
    }
}
