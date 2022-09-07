using BestHTTP.SocketIO;
using UnityEngine;


public class HomeTexasHoldemTableList : MonoBehaviour
{
    [SerializeField] private HomeSmallTableElement _prefab;

    private HomeSmallTableElement[] _homeSmallTableElements = new HomeSmallTableElement[5];
    private int _amountElements = 5;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (var i = 0; i < _amountElements; i++)
        {
            _homeSmallTableElements[i] = Instantiate(_prefab, transform);
        }
    }

    public void UpdateList()
    {
        if (UIManager.Instance)
        {

            bool isLimitSelected = false;
            string gametype = "";
            string selectedLimitType = "all";
            string selectedStack = "all";
            string selectedPlayerPerTable = "all";
            string currencyType = UIManager.Instance.currencyType.ToString();


            UIManager.Instance.SocketGameManager.SearchLobby(
                GameType.texas.ToString(),
                UIManager.Instance.selectedGameSpeed,
                "",
                isLimitSelected,
                gametype,
                selectedLimitType,
                selectedStack,
                selectedPlayerPerTable,
                currencyType,
                TexasTableListReceived);
        }
    }

    private void TexasTableListReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log($"{UIManager.Instance.gameType}TableListReceived : {packet}");

        JSONArray arr = new JSONArray(packet.ToString());
        string source = arr.getString(arr.length() - 1);

        RoomsListing roomsResp = JsonUtility.FromJson<RoomsListing>(source);

        if (!roomsResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(roomsResp.message, null);
            return;
        }

        if (!roomsResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(roomsResp.message, null);
            return;
        }

        var tableData = roomsResp.result;

        for (var i = 0; i < 5; i++)
        {
            if ( i < tableData.Count)
            {
                _homeSmallTableElements[i].SetInfo(tableData[i]);
            }
            else
            {
                _homeSmallTableElements[i].gameObject.SetActive(false);
            }
        }
    }
}
