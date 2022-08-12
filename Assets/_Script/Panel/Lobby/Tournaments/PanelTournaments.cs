using BestHTTP.SocketIO;
using System.Collections;
using UnityEngine;

public class PanelTournaments : MonoBehaviour
{
    [SerializeField] TournamentTableElement _tournamentTablePrefab;
    [SerializeField] Transform _content;

    private int updateTimeInSecconds = 8;

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateAtTime());
    }

    IEnumerator UpdateAtTime() 
    {
        yield return new WaitForSeconds(updateTimeInSecconds);
        ClearAllRow();
        UpdateTable();
    }

    private void ClearAllRow()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
    }

    private void UpdateTable()
    {
        if (UIManager.Instance)
        {
            string tournamentPokerType = "all";
            string selectedGameSpeed = UIManager.Instance.selectedGameSpeed.ToString();
            bool isLimitSelected = false;
            string gametype = "Touranment"; // "sng";
            string selectedLimitType = "all";
            string selectedStack = "all";
            string selectedPlayerPerTable = "all";

        UIManager.Instance.SocketGameManager.SearchTournamentLobby(
            tournamentPokerType,
            selectedGameSpeed,
            "",
            isLimitSelected,
            gametype,
            selectedLimitType,
            selectedStack,
            selectedPlayerPerTable,
            OnRegularTableListReceived);
        }
    }

    private void OnRegularTableListReceived(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
    {
        Debug.Log("OnRegularTableListReceived : " + packet.ToString());

        JSONArray arr = new JSONArray(packet.ToString());
        string Source = arr.getString(arr.length() - 1);
        NormalTournamentDetails TouramentDetail = JsonUtility.FromJson<NormalTournamentDetails>(Source);

        if (!TouramentDetail.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
        {
            UIManager.Instance.DisplayMessagePanel(TouramentDetail.message, null);
            return;
        }

        if (TouramentDetail.result != null)
        {
            foreach (var item in TouramentDetail.result)
            {
                TournamentTableElementData tournamentTableElementData = new TournamentTableElementData();
                tournamentTableElementData.Type = item.type;
                tournamentTableElementData.TournamentId = item.tournamentId;
                tournamentTableElementData.Name = item.name;
                tournamentTableElementData.BuyIn = item.buyIn;
                tournamentTableElementData.Status = item.status;
                tournamentTableElementData.IsJoinable = item.isJoinable;
                tournamentTableElementData.Players = item.players;
                tournamentTableElementData.DateTime = item.dateTime;
                tournamentTableElementData.TournamentStartTime = item.displayDateTime; // finde bug here

                TournamentTableElement row = Instantiate(_tournamentTablePrefab, _content);
                row.Init(tournamentTableElementData);
            }
        }
        UIManager.Instance.LobbyPanelNew.UpdatePanel();
    }
}
