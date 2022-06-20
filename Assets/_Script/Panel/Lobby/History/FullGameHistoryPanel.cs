using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullGameHistoryPanel : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    [Header("Text")]
    public Text txtHeader1;
    public Text txtHeader2;
    public Text txtGameID;

    [Header("Prefab")]
    public PlayerNameWithCardsRowObject playerNameWithCardPrefab;
    public PlayerWinnerRowObject playerWinnerPrefab;
    public HandEventRowObject handEventPrefab;

    [Header("Transform")]
    public Transform transPlayerNameWithCards;
    public Transform transWinnerPlayers;
    public Transform transPreFlop;
    public Transform transFlop;
    public Transform transTurn;
    public Transform transRiver;
    public Transform transShowDown;
    public Transform transFlopRoundCardContainer;
    public Transform transTurnRoundCardContainer;
    public Transform transRiverRoundCardContainer;

    [Header("Game Round Colors")]
    public Color colorPreFlop;
    public Color colorFlop;
    public Color colorTurn;
    public Color colorRiver;
    public Color colorShowDown;

    [Header("ScrollRect")]
    public ScrollRect myScrollRect;

    [Header("Content Size Fitter")]
    public ContentSizeFitter contentSizeFitter;
    #endregion

    #region PRIVATE_VARIABLES
    private FullGameHistoryResult.GameHistory fullHistory;

    private List<PlayerNameWithCardsRowObject> playerNameWithCardsRowObjectList = new List<PlayerNameWithCardsRowObject>();
    private List<PlayerWinnerRowObject> playerWinnerRowObjectList = new List<PlayerWinnerRowObject>();
    private List<HandEventRowObject> handEventList = new List<HandEventRowObject>();

    private List<Image> roundCards = new List<Image>();
    #endregion

    #region UNITY_CALLBACK
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS
    public void SetData(FullGameHistoryResult.GameHistory fullHistory)
    {
        ResetData();
        this.fullHistory = fullHistory;
        this.Open();

        SetHeader();
        SetPlayerNameAndCard();
        SetWinnerPlayers();
        SetHandEventData();
        SetTableCards();
    }

    public void ClosePanel()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }
    public void ButtonHold(Animator a)
    {
        a.SetBool("IsJoin", true);
    }

    public void ButtonRelease(Animator a)
    {
        a.SetBool("IsJoin", false);
    }
    #endregion

    #region PRIVATE_METHODS

    private void ResetData()
    {
        contentSizeFitter.enabled = false;
        myScrollRect.verticalNormalizedPosition = 1f;

        transPreFlop.gameObject.SetActive(false);
        transFlop.gameObject.SetActive(false);
        transTurn.gameObject.SetActive(false);
        transRiver.gameObject.SetActive(false);
        transShowDown.gameObject.SetActive(false);

        foreach (PlayerNameWithCardsRowObject obj in playerNameWithCardsRowObjectList)
        {
            Destroy(obj.gameObject);
        }

        foreach (PlayerWinnerRowObject obj in playerWinnerRowObjectList)
        {
            Destroy(obj.gameObject);
        }

        foreach (HandEventRowObject obj in handEventList)
        {
            Destroy(obj.gameObject);
        }

        playerNameWithCardsRowObjectList.Clear();
        playerWinnerRowObjectList.Clear();
        handEventList.Clear();
    }

    private void SetHeader()
    {
        txtHeader1.text = fullHistory.gameName + ", " + fullHistory.stack;
        txtHeader2.text = "Hand Started : " + fullHistory.dataTime + " - Game id: " + fullHistory.gameId;
    }

    private void SetPlayerNameAndCard()
    {
        foreach (FullGameHistoryResult.GameHistory.Player playerData in fullHistory.players)
        {
            PlayerNameWithCardsRowObject playerObject = Instantiate(playerNameWithCardPrefab, transPlayerNameWithCards).GetComponent<PlayerNameWithCardsRowObject>();
            playerObject.SetData(playerData);
            playerObject.Open();

            playerNameWithCardsRowObjectList.Add(playerObject);
        }
    }

    private void SetWinnerPlayers()
    {
        if (fullHistory.winners.Count == 0)
        {
            transWinnerPlayers.gameObject.SetActive(false);
        }
        else
        {
            transWinnerPlayers.gameObject.SetActive(true);
        }

        foreach (FullGameHistoryResult.GameHistory.Winner winnerData in fullHistory.winners)
        {
            PlayerWinnerRowObject winnerObject = Instantiate(playerWinnerPrefab, transWinnerPlayers).GetComponent<PlayerWinnerRowObject>();
            winnerObject.SetData(winnerData);
            winnerObject.Open();

            playerWinnerRowObjectList.Add(winnerObject);
        }
    }

    private void SetHandEventData()
    {
        foreach (FullGameHistoryResult.GameHistory.HandsEvent handData in fullHistory.handsEvents)
        {
            HandEventRowObject handEventObject = null;

            Color color = Color.white;
            Transform trans = null;

            if (handData.gameRound == "Preflop")
            {
                color = colorPreFlop;
                trans = transPreFlop;
            }
            else if (handData.gameRound == "Flop")
            {
                color = colorFlop;
                trans = transFlop;
            }
            else if (handData.gameRound == "Turn")
            {
                color = colorTurn;
                trans = transTurn;
            }
            else if (handData.gameRound == "River")
            {
                color = colorRiver;
                trans = transRiver;
            }
            else if (handData.gameRound == "ShowDown")
            {
                color = colorShowDown;
                trans = transShowDown;
            }
            else
            {
                continue;
            }

            if (trans.gameObject.activeSelf == false)
            {
                trans.gameObject.SetActive(true);
            }

            handEventObject = Instantiate(handEventPrefab, trans).GetComponent<HandEventRowObject>();
            handEventObject.SetData(handData, color);
            handEventObject.Open();
            handEventList.Add(handEventObject);

            Invoke("RefreshContent", 0.25f);
        }
    }

    private void SetTableCards()
    {
        List<string> flopRoundCards = fullHistory.tableCards.Flop;
        List<string> turnRoundCards = fullHistory.tableCards.Turn;
        List<string> riverRoundCards = fullHistory.tableCards.River;

        print(flopRoundCards.Count);
        print(turnRoundCards.Count);
        print(riverRoundCards.Count);

        foreach (Image img in roundCards)
        {
            Destroy(img.gameObject);
        }
        roundCards.Clear();

        if (flopRoundCards.Count > 0)
        {
            transFlop.gameObject.SetActive(true);
            foreach (string card in flopRoundCards)
            {
                CreateCardList(card, transFlopRoundCardContainer);
            }
        }

        if (turnRoundCards.Count > 0)
        {
            transTurn.gameObject.SetActive(true);
            foreach (string card in turnRoundCards)
            {
                CreateCardList(card, transTurnRoundCardContainer);
            }
        }

        if (riverRoundCards.Count > 0)
        {
            transRiver.gameObject.SetActive(true);
            foreach (string card in riverRoundCards)
            {
                CreateCardList(card, transRiverRoundCardContainer);
            }
        }
    }

    private void CreateCardList(string cardString, Transform transPlayerCardPanel)
    {
        print(cardString + " - " + transPlayerCardPanel.name);
        GameObject NewObj = new GameObject();
        NewObj.transform.parent = transPlayerCardPanel;
        NewObj.transform.localScale = Vector3.one;
        Image NewImage = NewObj.AddComponent<Image>();
        NewImage.sprite = Utility.Instance.GetCard(cardString);
        roundCards.Add(NewImage);
    }

    private void RefreshContent()
    {
        contentSizeFitter.enabled = true;
    }
    #endregion

    #region COROUTINES
    #endregion
}
