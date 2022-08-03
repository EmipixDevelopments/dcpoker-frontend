using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.SocketIO;

public class GetTableData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            string pokerGameType = PokerGameType.omaha.ToString();
            GameSpeed gameSpeed = UIManager.Instance.selectedGameSpeed;
            string tableName = "";
            bool isLimitSelected = false;
            string gametype = "Touranment";//"sng"; or  "Touranment"
            string SelectedLimitType = LimitType.All.ToString();
            string SelectedStack = "all";
            string SelectedPlayerPerTable = "all";
            string currencyType = UIManager.CurrencyType.cash.ToString();//  UIManager.Instance.currencyType = UIManager.CurrencyType.cash; // может не важно
            UIManager.Instance.SocketGameManager.SearchLobby(pokerGameType, gameSpeed, tableName, isLimitSelected, gametype, SelectedLimitType, SelectedStack, SelectedPlayerPerTable, currencyType, OnTableListReceived);
        }
    }

    private void OnTableListReceived(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("OnTableListReceived : " + packet.ToString());

        JSONArray arr = new JSONArray(packet.ToString());
        string Source = arr.getString(arr.length() - 1);


        RoomsListing roomsResp = JsonUtility.FromJson<RoomsListing>(Source);
    }
}
