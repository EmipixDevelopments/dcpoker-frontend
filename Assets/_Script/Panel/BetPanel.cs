using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BestHTTP.SocketIO;
using TMPro;
using System;

public class BetPanel : MonoBehaviour
{
    #region PUBLIC_VARIABLES

    public Button raiseSliderMinusButton;
    public Button raiseSliderPlusButton;
    public Button btnFold;
    public Button btnCheck;
    public Button btnCall;
    public Button btnRaise;
    public Button btnBet;
    public Button btnMoreTime;
    public Button AllInButton;

    public Button btnX2;
    public Button btnX3;
    public Button btnX5;
    public Button btn1By2Pot;
    public Button btn2By3Pot;
    public Button btn1By3Pot;
    public Button btn1By5Pot;
    public Button btnPotRaise;
    public Button btnAllInRaise;
    public ScalePunch scalePunchBet;
    public Animator BlinkCursorAnim;
    public Image panelBetOptions;
    public Slider sliderRaise;
    public TextMeshProUGUI txtCallAmount;
    public TextMeshProUGUI txtRaiseSlider;
    public TextMeshProUGUI txtRaiseSliderwithBar;
    public TextMeshProUGUI txtRaiseValue;
    public TextMeshProUGUI txtBetValue;
    [Header("InputField")]
    public TMP_InputField customBetInputField;
    #endregion

    #region PRIVATE_VARIABLES

    private double _minBetAmount;

    private double _callAmount;

    public bool _isPreCheckSelected;
    private bool _isPreCallSelected;
    private bool _isPreCallAnySelected;
    private bool _isPreFoldSelected;

    private double _preCallSelectedAmount;

    private double maxAmount;
    private double minSliderAmount;
    private double AllinAmount;
    public GameObject raisePanel;
    #endregion

    #region UNITY_CALLBACKS

    void OnEnable()
    {
        //		sliderRaise.minValue = 0f;
        //		sliderRaise.maxValue =1f;
        sliderRaise.value = sliderRaise.minValue;
        OnRaiseSliderValueChanged();
        //		txtRaiseSlider.text = "";
        //		RaiseButtonValue = 0;		


        raiseSliderMinusButton.interactable = false;
#if UNITY_ANDROID || UNITY_IPHONE
        if (PlayerPrefs.GetInt("Vibration", 1) == 1)
        {
            Handheld.Vibrate();
        }
#endif
    }

    void OnDisable()
    {
        btnCall.gameObject.SetActive(false);
        btnCheck.gameObject.SetActive(false);
        btnRaise.gameObject.SetActive(false);
        btnBet.gameObject.SetActive(false);
        AllInButton.gameObject.SetActive(false);

        sliderRaise.minValue = 0f;
        sliderRaise.maxValue = 1f;
        sliderRaise.value = 0f;
        txtRaiseSlider.text = "";
        txtRaiseSliderwithBar.text = "";
        RaiseButtonValue = 0;
        BetButtonValue = 0;

        raiseSliderMinusButton.interactable = false;
        scalePunchBet.enabled = false;
        //BlinkCursorAnim.StopPlayback();
    }


    #endregion

    #region DELEGATE_CALLBACKS

    #endregion

    #region PUBLIC_METHODS
    public void curserStopBlinker()
    {
        BlinkCursorAnim.gameObject.SetActive(false);
    }
    public void ResetButtons()
    {

    }
    public void SetButtonsCheck(bool IsOpen)
    {
        //		raiseSliderMinusButton.interactable = IsOpen;
        //		raiseSliderPlusButton.interactable = IsOpen;
        //		btnFold.enabled = IsOpen;
        //		btnCheck.enabled = IsOpen;
        //		btnCall.enabled = IsOpen;
        //		btnRaise.enabled = IsOpen;
        //
        //		AllInButton.enabled = IsOpen;
        //		btn1By2Pot.enabled = IsOpen;
        //		btn2By3Pot.enabled = IsOpen;
        //		btnPotRaise.enabled = IsOpen;
        //		btnAllInRaise.enabled = IsOpen;
    }
    public void OnMoreTimeButtonTap()
    {
        UIManager.Instance.SocketGameManager.ExtraTimer(Constants.Poker.roomId, (socket, packet, args) =>
        {
            Debug.Log("OnMoreTimeButtonTap  : " + packet.ToString());
            btnMoreTime.Close();
        });
    }

    public void OnFoldButtonTap()
    {
        SetButtonsCheck(false);
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.SendPlayerAction(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId, 0, PokerPlayerAction.Fold, false, (socket, packet, args) =>
        {
            Debug.Log("SendPlayerAction  : " + packet.ToString());

            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            PokerEventResponse SendPlayerActionResp = JsonUtility.FromJson<PokerEventResponse>(resp);

            if (SendPlayerActionResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                SetButtonsCheck(true);
            }

            UIManager.Instance.GameScreeen.ShowBetPanelOption = false;
        });

        HideBetPanel();

        //		SoundManager.Instance.PlayButtonTapSound ();
    }

    public void OnCallButtonTap()
    {
        SetButtonsCheck(false);
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.SendPlayerAction(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId, CallAmount, PokerPlayerAction.Call, false, (socket, packet, args) =>
        {
            Debug.Log("SendPlayerAction  : " + packet.ToString());
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            PokerEventResponse SendPlayerActionResp = JsonUtility.FromJson<PokerEventResponse>(resp);

            if (SendPlayerActionResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                SetButtonsCheck(true);
            }

            UIManager.Instance.GameScreeen.ShowBetPanelOption = false;
        });

        HideBetPanel();

        //		SoundManager.Instance.PlayButtonTapSound ();
    }

    public void OnCheckButtonTap()
    {
        SetButtonsCheck(false);
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.SendPlayerAction(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId, 0, PokerPlayerAction.Check, false, (socket, packet, args) =>
        {
            //			Debug.Log ("SendPlayerAction  : " + packet.ToString ());
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            PokerEventResponse SendPlayerActionResp = JsonUtility.FromJson<PokerEventResponse>(resp);

            if (SendPlayerActionResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                SetButtonsCheck(true);
            }

            UIManager.Instance.GameScreeen.ShowBetPanelOption = false;
        });

        HideBetPanel();
    }

    public void OnRaiseButtonTap()
    {
        double raiseAmount = sliderRaise.value.floatToDouble();
        PokerPlayer plr = UIManager.Instance.GameScreeen.GetOwnPlayer();

        if (raiseAmount > plr.BuyInAmount)
            raiseAmount = plr.BuyInAmount;

        SetButtonsCheck(false);
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.SendPlayerAction(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId, raiseAmount, PokerPlayerAction.Bet, true, (socket, packet, args) =>
        {
            Debug.Log("SendPlayerAction  : " + packet.ToString());
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            PokerEventResponse SendPlayerActionResp = JsonUtility.FromJson<PokerEventResponse>(resp);

            if (SendPlayerActionResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                SetButtonsCheck(true);
            }

            UIManager.Instance.GameScreeen.ShowBetPanelOption = false;
        });

        HideBetPanel();
    }

    public void OnBetButtonTap()
    {
        double betAmount = sliderRaise.value.floatToDouble();
        PokerPlayer plr = UIManager.Instance.GameScreeen.GetOwnPlayer();

        if (betAmount > plr.BuyInAmount)
        {
            betAmount = plr.BuyInAmount;
        }

        SetButtonsCheck(false);
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.SendPlayerAction(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId, betAmount, PokerPlayerAction.Bet, true, (socket, packet, args) =>
        {
            Debug.Log("SendPlayerAction  : " + packet.ToString());
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            PokerEventResponse SendPlayerActionResp = JsonUtility.FromJson<PokerEventResponse>(resp);

            if (SendPlayerActionResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                SetButtonsCheck(true);
            }
            UIManager.Instance.GameScreeen.ShowBetPanelOption = false;
        });
        HideBetPanel();
    }

    public void OnAllinButtonTap()
    {
        double AllinAmountVal = AllinAmount;
        SetButtonsCheck(false);
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.SendPlayerAction(UIManager.Instance.assetOfGame.SavedLoginData.PlayerId, AllinAmountVal, PokerPlayerAction.Allin, false, (socket, packet, args) =>
        {
            Debug.Log("SendPlayerAction  : " + packet.ToString());
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;
            PokerEventResponse SendPlayerActionResp = JsonUtility.FromJson<PokerEventResponse>(resp);

            if (SendPlayerActionResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                SetButtonsCheck(true);
            }

            UIManager.Instance.GameScreeen.ShowBetPanelOption = false;
        });

        HideBetPanel();
    }


    public void OpenBetPanel(double minAmount, double maxAmount)
    {
        this.maxAmount = maxAmount;
        minAmount = minAmount < 0 ? 0 : minAmount;

        minAmount = minAmount <= maxAmount ? minAmount : maxAmount;

        double newMaxAmount = maxAmount;

        maxAmount = newMaxAmount > maxAmount ? maxAmount : newMaxAmount;

        maxAmount += minAmount;

        PokerPlayer ownPlayer = UIManager.Instance.GameScreeen.GetOwnPlayer();

        maxAmount = maxAmount > ownPlayer.BuyInAmount ? ownPlayer.BuyInAmount : maxAmount;

        _callAmount = minAmount;
        CallAmount = minAmount;
        BlinkCursorAnim.gameObject.SetActive(false);
        minSliderAmount = minAmount /*+ UIManager.Instance.GameScreeen.currentRoomData.smallBlind*/;
        ModifyBetRaiseButtons();
        double sliderMinValue = minAmount;  //+ UIManager.Instance.GameScreeen.currentRoomData.smallBlind;
        sliderMinValue = sliderMinValue <= maxAmount ? sliderMinValue : maxAmount;
        sliderRaise.minValue = sliderMinValue.doubleToFloat();
        sliderRaise.maxValue = maxAmount.doubleToFloat();
        sliderRaise.value = sliderMinValue.doubleToFloat();
        //	Left buttons
        double min = minAmount;
        min = min <= 0 ? 100 : min;
        min = min <= maxAmount ? min : maxAmount;
        bool isAllinOnlyLeft = sliderRaise.minValue == sliderRaise.maxValue;
        btnRaise.enabled = !isAllinOnlyLeft;
        raisePanel.SetActive(!isAllinOnlyLeft);
        if (isAllinOnlyLeft)
        {
            txtCallAmount.text = "";
        }

        OnRaiseSliderValueChanged();

        raiseSliderMinusButton.interactable = false;

        UIManager.Instance.SoundManager.AttentionSoundOnce();
        UIManager.Instance.SoundManager.Vibrate();
        UIManager.Instance.GameScreeen.preBetButtonsPanel.Close();

        this.Open();
    }

    bool IsAllButtonBool;
    public void OpenBetPanelTurn(double callamount, double betamount, double minRaise, double maxraise, double allinamount, bool IscallButton, bool IsBetButton, bool IscheckButton, bool IsRaiseButton, bool IsAllButton, float Timer, bool isLimitGame)
    {
        UIManager.Instance.SoundManager.AttentionSoundOnce();
        UIManager.Instance.SoundManager.Vibrate();

        scalePunchBet.enabled = true;
        CallAmount = callamount;
        AllinAmount = allinamount;
        this.maxAmount = maxraise;
        sliderRaise.minValue = minRaise.doubleToFloat();
        sliderRaise.maxValue = maxraise.doubleToFloat();
        btnCall.gameObject.SetActive(IscallButton);
        btnCheck.gameObject.SetActive(IscheckButton);
        btnRaise.gameObject.SetActive(IsRaiseButton);
        btnBet.gameObject.SetActive(IsBetButton);
        AllInButton.gameObject.SetActive(IsAllButton);
        ModifyBetRaiseButtons();
        /* if (IsAllButton)
         {
             btnAllInRaise.interactable = false;
             btnAllInRaise.image.CrossFadeAlpha(0.5f, 0f, true);

         }
         else
         {
             btnAllInRaise.interactable = true;
             btnAllInRaise.image.CrossFadeAlpha(1f, 0f, true);
         }*/
        //if (isLimitGame)
        //    panelBetOptions.gameObject.SetActive(false);
        //else
        panelBetOptions.gameObject.SetActive(IsRaiseButton || IsBetButton);

        IsAllButtonBool = IsAllButton;
        //txtRaiseSlider.text = minRaise.ConvertToCommaSeparatedValue();
        TextRaiseButtonValue = minRaise;
        RaiseButtonValue = minRaise + UIManager.Instance.GameScreeen.GetOwnPlayer().BetAmount;
        customBetInputField.text = (minRaise + UIManager.Instance.GameScreeen.GetOwnPlayer().BetAmount).ToString();
        BetButtonValue = betamount;
        OnRaiseSliderValueChanged();

        this.Open();
        UIManager.Instance.GameScreeen.preBetButtonsPanel.Close();
    }

    public void OnRaiseSliderValueChanged()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        if (sliderRaise.value > sliderRaise.minValue && sliderRaise.value < sliderRaise.maxValue)
        {
            //sliderRaise.value = UIManager.Instance.GameScreeen.currentRoomData.smallBlind.doubleToFloat();
            sliderRaise.value = Convert.ToInt32(sliderRaise.value);//.RoundTo(UIManager.Instance.GameScreeen.currentRoomData.smallBlind.doubleToFloat());
        }

        //txtRaiseSlider.text = sliderRaise.value.ConvertToCommaSeparatedValue();
        TextRaiseButtonValue = sliderRaise.value;
        RaiseButtonValue = sliderRaise.value.floatToDouble() + UIManager.Instance.GameScreeen.GetOwnPlayer().BetAmount;
        BetButtonValue = sliderRaise.value.floatToDouble();

        raiseSliderMinusButton.interactable = sliderRaise.value != sliderRaise.minValue;
        raiseSliderPlusButton.interactable = sliderRaise.value != sliderRaise.maxValue;
    }

    public void OnCustomBetValueChanged()
    {

        Debug.Log("OnCustomBetValueChanged");
        if (!gameObject.activeSelf)
        {
            return;
        }

        if (!string.IsNullOrEmpty(customBetInputField.text))
        {
            float sliderMinValue = (sliderRaise.minValue + UIManager.Instance.GameScreeen.GetOwnPlayer().BetAmount.doubleToFloat());
            float sliderMaxValue = (sliderRaise.maxValue + UIManager.Instance.GameScreeen.GetOwnPlayer().BetAmount.doubleToFloat());
            if (float.Parse(customBetInputField.text) >= sliderMinValue && float.Parse(customBetInputField.text) <= sliderMaxValue)
            {
                sliderRaise.value = (float.Parse(customBetInputField.text) - UIManager.Instance.GameScreeen.GetOwnPlayer().BetAmount.doubleToFloat());
            }
            else
            {
                customBetInputField.text = (sliderRaise.minValue.floatToDouble() + UIManager.Instance.GameScreeen.GetOwnPlayer().BetAmount).ToString();
            }
        }

        //if(customBetInputField.text == "")
        //{
        //customBetInputField.text = sliderRaise.minValue.ToString();
        //sliderRaise.value = 
        //}
    }

    public void OnCustomBetValueEndEdit()
    {
        Debug.Log("OnCustomBetValueEndEdit");
        if (!gameObject.activeSelf)
        {
            return;
        }

        if (string.IsNullOrEmpty(customBetInputField.text))
        {
            customBetInputField.text = (sliderRaise.value.floatToDouble() + UIManager.Instance.GameScreeen.GetOwnPlayer().BetAmount).ToString();
        }
        else if (float.Parse(customBetInputField.text) < sliderRaise.minValue)
        {
            customBetInputField.text = (sliderRaise.minValue.floatToDouble() + UIManager.Instance.GameScreeen.GetOwnPlayer().BetAmount).ToString();
        }
        else if (float.Parse(customBetInputField.text) > sliderRaise.maxValue)
        {
            customBetInputField.text = (sliderRaise.maxValue.floatToDouble() + UIManager.Instance.GameScreeen.GetOwnPlayer().BetAmount).ToString();
        }
        OnCustomBetValueChanged();
    }
    public void OnRaiseSliderPlusButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        float SliderVal = sliderRaise.value;
        SliderVal = SliderVal + UIManager.Instance.GameScreeen.currentRoomData.smallBlind.doubleToFloat();
        //txtRaiseSlider.text = "" + sliderRaise.value;
        TextRaiseButtonValue = sliderRaise.value;
        customBetInputField.text = sliderRaise.value.ToString();
        sliderRaise.value = SliderVal > sliderRaise.maxValue ? sliderRaise.maxValue : SliderVal;
    }

    public void OnRaiseSliderMinusButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        sliderRaise.value -= UIManager.Instance.GameScreeen.currentRoomData.smallBlind.doubleToFloat();
        //txtRaiseSlider.text = ""+sliderRaise.value;
        TextRaiseButtonValue = sliderRaise.value;
        customBetInputField.text = sliderRaise.value.ToString();
        sliderRaise.value = sliderRaise.value < sliderRaise.minValue ? sliderRaise.minValue : sliderRaise.value;
    }

    public void OnX2ButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        sliderRaise.value = (minSliderAmount * 2).doubleToFloat();
    }

    public void OnX3ButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        sliderRaise.value = (minSliderAmount * 3).doubleToFloat();
    }

    public void OnX5ButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        sliderRaise.value = (minSliderAmount * 5).doubleToFloat();
    }

    public void On1By2PotRaiseButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot > 0)
        {
            Debug.Log("On1By2PotRaiseButtonTap");
            //  sliderRaise.value = (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot / 2f).doubleToFloat();
            //            customBetInputField.text = (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot / 2f).doubleToFloat().ToString();

            sliderRaise.value = (UIManager.Instance.GameScreeen.TotalTablePotAmount / 2f).doubleToFloat();
            customBetInputField.text = (UIManager.Instance.GameScreeen.TotalTablePotAmount / 2f).doubleToFloat().ToString();
        }
    }

    public void On1By3PotRaiseButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.GameScreeen.TotalTablePotAmount > 0)
        {
            Debug.Log("On1By3PotRaiseButtonTap => " + (UIManager.Instance.GameScreeen.TotalTablePotAmount / 3f).doubleToFloat());
            //   sliderRaise.value = (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot / 3f).doubleToFloat();
            //   customBetInputField.text = (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot / 3f).doubleToFloat().ToString();
            //sliderRaise.value = (UIManager.Instance.GameScreeen.TotalTablePotAmount / 3f).doubleToFloat();
            //customBetInputField.text = (UIManager.Instance.GameScreeen.TotalTablePotAmount / 3f).doubleToFloat().ToString();


            float aa = Mathf.Ceil((UIManager.Instance.GameScreeen.TotalTablePotAmount / 3f).doubleToFloat());
            print("after aaffff => " + aa);
            customBetInputField.text = aa.ToString();
            sliderRaise.value = aa;
        }
    }

    public void On1By5PotRaiseButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.GameScreeen.TotalTablePotAmount > 0)
        {
            Debug.Log("On1By5PotRaiseButtonTap" + (UIManager.Instance.GameScreeen.TotalTablePotAmount / 5f).doubleToFloat());
            //   sliderRaise.value = (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot / 5f).doubleToFloat();
            //   customBetInputField.text = (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot / 5f).doubleToFloat().ToString();
            //sliderRaise.value = (UIManager.Instance.GameScreeen.TotalTablePotAmount / 5f).doubleToFloat();
            //customBetInputField.text = (UIManager.Instance.GameScreeen.TotalTablePotAmount / 5f).doubleToFloat().ToString();
            float aa = Mathf.Ceil((UIManager.Instance.GameScreeen.TotalTablePotAmount / 5f).doubleToFloat());
            print("after aaffff => " + aa);
            customBetInputField.text = aa.ToString();
            sliderRaise.value = aa;
        }
    }

    public void On2By3PotRaiseButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        sliderRaise.value = (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot * (2f / 3f)).doubleToFloat();
    }

    public void OnRaiseToPotValueButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot > 0)
        {
            sliderRaise.value = (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot).doubleToFloat();
            customBetInputField.text = (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot).doubleToFloat().ToString();
        }
    }

    public void OnRaiseToAllInButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        sliderRaise.value = (maxAmount).doubleToFloat();
    }

    public void EnableExtraTimeButton(float time)
    {
        if (time == 0)
        {
            //btnMoreTime.Open ();
        }
        else
        {
            Invoke("EnableExtraTimeButtonWithInterval", time);
        }

    }

    public void DisableExtraTimeButton()
    {
        //btnMoreTime.Close ();		
    }
    #endregion

    #region PRIVATE_METHODS

    private void ModifyBetRaiseButtons()
    {
        btn1By2Pot.interactable = false;
        btn2By3Pot.interactable = false;
        btnPotRaise.interactable = false;
        btn1By3Pot.interactable = false;
        btn1By5Pot.interactable = false;

        if (minSliderAmount == 0)
        {
            minSliderAmount = UIManager.Instance.GameScreeen.currentRoomData.smallBlind;
        }

        //  if ((UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot / 3f) > 0 && (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot / 3f) <= maxAmount && (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot / 3f) > minSliderAmount)
        if ((UIManager.Instance.GameScreeen.TotalTablePotAmount / 3f) > 0 && (UIManager.Instance.GameScreeen.TotalTablePotAmount / 3f) <= maxAmount)// && (UIManager.Instance.GameScreeen.TotalTablePotAmount / 3f) > minSliderAmount)
        {

            if ((UIManager.Instance.GameScreeen.TotalTablePotAmount / 3f) > sliderRaise.minValue)
            {
                btn1By3Pot.interactable = true;
                btn1By3Pot.image.CrossFadeAlpha(0.5f, 0f, true);
            }
        }
        if ((UIManager.Instance.GameScreeen.TotalTablePotAmount / 5f) > 0 && (UIManager.Instance.GameScreeen.TotalTablePotAmount / 5f) <= maxAmount)// && (UIManager.Instance.GameScreeen.TotalTablePotAmount / 5f) > minSliderAmount)
        {
            if ((UIManager.Instance.GameScreeen.TotalTablePotAmount / 5f) > sliderRaise.minValue)
            {
                btn1By5Pot.interactable = true;
                btn1By5Pot.image.CrossFadeAlpha(0.5f, 0f, true);
            }
        }
        if (AllInButton.gameObject.activeInHierarchy)
        {
            btnAllInRaise.interactable = false;
            btnAllInRaise.image.CrossFadeAlpha(0.5f, 0f, true);
        }
        else
        {
            btnAllInRaise.interactable = true;
            btnAllInRaise.image.CrossFadeAlpha(1f, 0f, true);
        }

        /*
        if ((UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot / 2f) > 0 && (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot / 2f) <= maxAmount && (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot / 2f) > minSliderAmount)
        {
            btn1By2Pot.interactable = true;
        }

        if ((UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot * (2f / 3f)) > 0 && (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot * (2f / 3f)) <= maxAmount && (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot * (2f / 3f)) > minSliderAmount)
        {
            btn2By3Pot.interactable = true;
        }

        if ((UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot) > 0 && (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot) <= maxAmount && (UIManager.Instance.GameScreeen.SidePotAmountNew.mainPot) > minSliderAmount)
        {
            btnPotRaise.interactable = true;
        }*/
    }

    private void EnableExtraTimeButtonWithInterval()
    {
        btnMoreTime.Open();
    }

    private void HideBetPanel()
    {
        IsPreCallAnySelected = IsPreCheckSelected = IsPreFoldSelected = IsPreCallSelected = false;

        PokerPlayer ownPlayer = UIManager.Instance.GameScreeen.GetOwnPlayer();
        if (ownPlayer != null)
            ownPlayer.StopTurnTimerAnimation();

        UIManager.Instance.GameScreeen.preBetButtonsPanel.Close();
        this.Close();
    }

    #endregion

    #region COROUTINES

    #endregion

    #region GETTER_SETTER
    public double MinBetAmount
    {
        get
        {
            return _minBetAmount;
        }
        set
        {
            _minBetAmount = value;
        }
    }
    public double CallAmount
    {
        get
        {
            return _callAmount;
        }
        set
        {
            _callAmount = value;
            txtCallAmount.text = _callAmount.ConvertToCommaSeparatedValue();

            btnCall.gameObject.SetActive(value > 0);
            btnCheck.gameObject.SetActive(value <= 0);
        }
    }
    public bool IsPreCheckSelected
    {
        get
        {
            return _isPreCheckSelected;
        }
        set
        {
            _isPreCheckSelected = value;
        }
    }
    public bool IsPreCallSelected
    {
        get
        {
            return _isPreCallSelected;
        }
        set
        {
            _isPreCallSelected = value;
        }
    }
    public bool IsPreCallAnySelected
    {
        get
        {
            return _isPreCallAnySelected;
        }
        set
        {
            _isPreCallAnySelected = value;
        }
    }
    public bool IsPreFoldSelected
    {
        get
        {
            return _isPreFoldSelected;
        }
        set
        {
            _isPreFoldSelected = value;
        }
    }
    public double PreCallSelectedAmount
    {
        get
        {
            return _preCallSelectedAmount;
        }
        set
        {
            _preCallSelectedAmount = value;
        }
    }
    public double TextRaiseButtonValue
    {
        set
        {
            if (value == 0)
            {
                txtRaiseSlider.text = "";
                txtRaiseSliderwithBar.text = "";
            }
            else
            {
                txtRaiseSlider.text = value.ConvertToCommaSeparatedValue();
                txtRaiseSliderwithBar.text = "BET " + value.ConvertToCommaSeparatedValue();
            }
        }
    }
    public double RaiseButtonValue
    {
        set
        {
            if (value == 0)
            {
                txtRaiseValue.text = "";
            }
            else
            {
                txtRaiseValue.text = value.ConvertToCommaSeparatedValue();
            }
        }
    }
    public double BetButtonValue
    {
        set
        {
            if (value == 0)
            {
                txtBetValue.text = "";
            }
            else
            {
                txtBetValue.text = value.ConvertToCommaSeparatedValue();
            }
        }
    }
    #endregion
}