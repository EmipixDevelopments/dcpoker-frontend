using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandEventRowObject : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    public Image imgColorLine;
    public Text txtPlayerName;
    public Text txtPlayerAction;
    public Text txtBetAmount;

    public Transform transCardPanel;

    public Color colorSmallBlind;
    public Color colorBigBlind;
    public Color colorCall;
    public Color colorBet;
    #endregion

    #region PRIVATE_VARIABLES
    private FullGameHistoryResult.GameHistory.HandsEvent handEvent;
    #endregion

    #region UNITY_CALLBACK
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS
    public void SetData(FullGameHistoryResult.GameHistory.HandsEvent handEvent, Color color)
    {
        this.Open();
        this.handEvent = handEvent;

        SetLineColor(color);
        SetPlayerName(handEvent.playerName);
        SetActionName(handEvent.playerAction);
        SetBetAmount(handEvent.betAmount);
        SetCards(handEvent.cards);
    }
    #endregion

    #region PRIVATE_METHODS

    public void SetLineColor(Color color)
    {
        imgColorLine.color = color;
    }

    private void SetPlayerName(string name)
    {
        txtPlayerName.text = name;
    }

    private void SetActionName(string action)
    {
        if (action == "null")
        {
            txtPlayerAction.text = action;
            return;
        }

        int actionNumber = int.Parse(action);
        txtPlayerAction.text = Utility.Instance.GetActionName(actionNumber);

        if (actionNumber == 0)
            txtPlayerAction.color = colorSmallBlind;
        else if (actionNumber == 1)
            txtPlayerAction.color = colorBigBlind;
        else if (actionNumber == 3)
            txtPlayerAction.color = colorBet;
        else if (actionNumber == 4)
            txtPlayerAction.color = colorCall;
        else if (actionNumber == 9)
            txtPlayerAction.color = colorSmallBlind;

    }

    private void SetBetAmount(double amount)
    {
        print("amount: " + amount);
        if (amount > 0)
        {
            txtBetAmount.text = amount.ToString();
        }
        else
        {
            txtBetAmount.text = "";
        }
    }

    private void SetCards(List<string> cards)
    {
        foreach (string cardString in cards)
        {
            GameObject NewObj = new GameObject();
            NewObj.transform.parent = transCardPanel;
            NewObj.transform.localScale = Vector3.one;
            Image NewImage = NewObj.AddComponent<Image>();
            NewImage.sprite = Utility.Instance.GetCard(cardString);
        }
    }
    #endregion

    #region COROUTINES
    #endregion
}
