using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryRowObject : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public Text txtDateAndTime;
    public Text txtGameName;
    public Text txtWinningAmount;
    public Image imgBG;

    public Transform transPlayerCardsContainer;
    public Transform transBestCardsContainer;
    #endregion

    #region PRIVATE_VARIABLES
    private GameHandHistoryResult.GameHistoryList history;
    public FullGameHistoryPanel fullGameHistoryPanel;
    #endregion

    #region UNITY_CALLBACK
    void OnDisable()
    {
        fullGameHistoryPanel.ClosePanel();
    }
    #endregion

    #region DELEGATE_CALLBACKS

    #endregion

    #region PUBLIC_METHODS

    public void SetData(GameHandHistoryResult.GameHistoryList history, Color color)
    {
        this.history = history;

        txtDateAndTime.text = history.dateTime;
        txtGameName.text = history.gameName;

        SetPlayerCards(history.handCards);
        //imgBG.color = color;

        SetBestCards(history.winner.winningHands);

        if (history.winner.winningAmount > 0)
        {
            if (txtWinningAmount != null)
            {
                txtWinningAmount.text = history.winner.winningAmount.ToString();
            }
        }
    }

    public void OpenFullGameHistory()
    {
        UIManager.Instance.DisplayLoader("");
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.GameHistory(history.gameId, (socket, packet, args) =>
            {

                print("GameHistory : " + packet.ToString());

                UIManager.Instance.HideLoader();
                JSONArray arr = new JSONArray(packet.ToString());
                print("GameHistory : 1");
                PokerEventResponse<FullGameHistoryResult> gameHistoryResponse = JsonUtility.FromJson<PokerEventResponse<FullGameHistoryResult>>(arr.getString(0));
                print("GameHistory : 2");
                if (gameHistoryResponse.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    print("GameHistory : 3");
                    fullGameHistoryPanel.SetData(gameHistoryResponse.result.gameHistory);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(gameHistoryResponse.message, null);
                }
            });
    }

    #endregion

    #region PRIVATE_METHODS

    private void SetBestCards(List<string> cards)
    {
        foreach (string cardString in cards)
        {
            GameObject NewObj = new GameObject();
            NewObj.transform.parent = transBestCardsContainer;
            NewObj.transform.localScale = Vector3.one;
            Image NewImage = NewObj.AddComponent<Image>();
            NewImage.sprite = Utility.Instance.GetCard(cardString);
        }
    }

    private void SetPlayerCards(List<string> cards)
    {
        foreach (string cardString in cards)
        {
            GameObject NewObj = new GameObject();
            NewObj.transform.parent = transPlayerCardsContainer;
            NewObj.transform.localScale = Vector3.one;
            Image NewImage = NewObj.AddComponent<Image>();
            NewImage.sprite = Utility.Instance.GetCard(cardString);
        }
    }

    #endregion

    #region COROUTINES

    #endregion
}
