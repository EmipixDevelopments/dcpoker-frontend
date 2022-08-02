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
            string pokerGameType = "";
            GameSpeed tableSpeed = GameSpeed.regular;
            string tableName = "";
            bool limit = true;
            string gametype = "";
            string game = "";
            string stacks = "";
            string maxPlayer = "";
            string currencyType = "";
            UIManager.Instance.SocketGameManager.SearchLobby(pokerGameType, tableSpeed, tableName, limit,
               gametype, game, stacks, maxPlayer, currencyType,OnTableListReceived);
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
