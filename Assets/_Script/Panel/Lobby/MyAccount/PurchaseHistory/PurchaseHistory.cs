using System;
using System.Collections.Generic;
using BestHTTP.SocketIO;
using UnityEngine;

public class PurchaseHistory : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private PurchaseHistoryElement _purchaseHistoryElement;

    private TableContainer<PurchaseHistoryElement> _purchaseHistoryTableContainer;

    private void Start()
    {
        _purchaseHistoryTableContainer = new TableContainer<PurchaseHistoryElement>(_container, _purchaseHistoryElement);
    }

    private void OnEnable()
    {
        UpdateTransactions();
    }

    private void UpdateTransactions()
    {
        UIManager.Instance.SocketGameManager.Transactions(Test);
    }

    private void Test(Socket socket, Packet packet, object[] args)
    {
        JSONArray arr = new JSONArray(packet.ToString());
        string Source = arr.getString(arr.length() - 1);
        var statusStandard = JsonUtility.FromJson<StatusStandard<Transaction>>(Source);

        if (!statusStandard.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            Debug.LogError("Purchase history: " + statusStandard.statusCode);
            return;
        }

        var data = statusStandard.result;
        for (var i = 0; i < data.Count; i++)
        {
            _purchaseHistoryTableContainer.GetElement(i).SetData(data[i]);
        }
        _purchaseHistoryTableContainer.HideFromIndex(data.Count);

    }
}
