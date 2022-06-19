using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PurchaseDataHistory : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    [Header("Images")]
    public Image BarMain;

    [Header("Colors")]
    public Sprite[] Colors;

    [Header("Text")]
    public TextMeshProUGUI Date;
    public TextMeshProUGUI Chips;
    public TextMeshProUGUI Amount;
    public TextMeshProUGUI Type;
    public TextMeshProUGUI Status;
    #endregion

    #region PRIVATE_VARIABLES
    #endregion

    #region UNITY_CALLBACKS
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS
    public void SetData(purchaseHistoryData Data, int i)
    {
        //DateTime dateTime = DateTime.Parse("time string");
        //Date.text = dateTime.ToString("yyyy-MM-dd HH:mm tt");
        if (Data.date.Equals(""))
        {
            Date.text = "-";
        }
        else
        {
            Date.text = Data.date.ToString();
        }


        if (Data.chips.Equals(""))
        {
            Chips.text = "-";
        }
        else
        {
            Chips.text = Data.chips.ConvertToCommaSeparatedValue();
        }

        if (Data.amount.Equals(""))
        {
            Amount.text = "-";
        }
        else
        {
            Amount.text = Data.amount.ToString();
        }

        if (Data.type.Equals(""))
        {
            Type.text = "-";
        }
        else
        {
            Type.text = Data.type.ToString();
        }
        if (Data.status.Equals(""))
        {
            Status.text = "-";
        }
        else
        {
            Status.text = Data.status.ToString();
        }

        if (i % 2 == 0)
        {
            BarMain.sprite = Colors[0];
        }
        else
        {
            BarMain.sprite = Colors[1];
        }

        this.Open();
        //if (Data.isCredit)
        //{
        //    Chips.color = Color.red;
        //}
        //else
        //{
        //    Chips.color = Color.red;
        //}
    }
    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES
    #endregion

    #region GETTER_SETTER
    #endregion
}