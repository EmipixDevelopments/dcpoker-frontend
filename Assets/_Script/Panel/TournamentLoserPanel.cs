using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TournamentLoserPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtRank;
    [SerializeField] private TextMeshProUGUI txtMessage;

    private void OnEnable()
    {
        CancelInvoke("ClosePopup");
        Invoke("ClosePopup", 10);
    }

    public void SetData(OnTournamentPrizeResponse data)
    {
        Debug.Log("Loser panel open");
        txtRank.text = "Rank" + " " + data.rank;

        if (data.prize > 0)
        {
            string prize = "<color=#FFE13B>" + data.prize + "</color>";
            txtMessage.text = "Tournament Winner Message".Replace("{0}", prize);
        }
        else
        {
            txtMessage.text = "Tournament Loser Message";
        }

        this.Open();
    }

    public void OnLobbyButtonTap()
    {
        UIManager.Instance.GameScreeen.OnLobbyDone();
    }

    public void ClosePopup()
    {
        this.gameObject.SetActive(false);
    }
}
