using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BuyinPanel : MonoBehaviour
{
    #region PUBLIC_VARIABLES

    public TextMeshProUGUI txtTotalChips;
    public Text txtRemainingTime;
    public Text txtSliderSelectedChips;
    public Slider buyinSlider;
    //public Button btnSelectBuyin;

    public TextMeshProUGUI txtMinBuyin;
    public TextMeshProUGUI txtMaxBuyin;
    public TMP_InputField txtInputFieldSelectedChips;
    public Toggle toggleBuyin;

    #endregion

    #region PRIVATE_VARIABLES

    private double _minBuyinAmount;
    private double _maxBuyinAmount;
    private double _maxReBuyinAmount;
    private int _selectedSeatIndex;
    private bool _isWaitingPlayer = false;
    private double _lastBuyinAmount;
    public double selectedBuyinAmount;
    [SerializeField]
    //	private double selectedBuyinAmount;

    #endregion

    #region UNITY_CALLBACKS

    void OnEnable()
    {
        //		buyinSlider.minValue = 0f;
        //		buyinSlider.maxValue = 1f;
        //		buyinSlider.value = 0f;
        //		txtTotalChips.text = "Total chips  : " ;
        //		txtMinBuyin.text = "MIN\n "  ;
        //		txtMaxBuyin.text = "MAX\n " ;
    }

    void OnDisable()
    {
        buyinSlider.minValue = 0f;
        buyinSlider.maxValue = 1f;
        buyinSlider.value = 0f;
        txtTotalChips.text = "Total chips" + " : ";
        txtMinBuyin.text = "";
        txtMaxBuyin.text = "";
        txtSliderSelectedChips.text = "";
    }

    #endregion

    #region DELEGATE_CALLBACKS

    #endregion

    #region PUBLIC_METHODS

    public void OpenBuyinPanel(double minBuyin, double maxBuyin, string gameLimit, int seatIndex)
    {
        //		Debug.Log ("minBuyin " +minBuyin);
        //		Debug.Log ("maxBuyin " +maxBuyin);
        print("minBuyin: " + minBuyin);
        print("maxBuyin: " + maxBuyin);

        if (minBuyin <= 0 || maxBuyin <= 0 || maxBuyin < minBuyin)
        {
            UIManager.Instance.DisplayMessagePanel("Something went wrong", null);
            return;
        }
        RemainingTime = 0;
        isRebuyIn = false;
        IsWaitingPlayer = false;
        MinBuyinAmount = minBuyin;
        MaxBuyinAmount = maxBuyin;
        txtTotalChips.text = "Total chips : " + UIManager.Instance.assetOfGame.SavedLoginData.chips.ConvertToCommaSeparatedValue();

        txtMinBuyin.text = minBuyin.ConvertToCommaSeparatedValue();
        txtMaxBuyin.text = maxBuyin.ConvertToCommaSeparatedValue();

        buyinSlider.minValue = minBuyin.doubleToFloat();
        buyinSlider.maxValue = maxBuyin.doubleToFloat();

        buyinSlider.value = minBuyin.doubleToFloat();//((float)(maxBuyin + minBuyin)) / 2f;
        this.Open();
        OnBuyinSliderValueChanged();
        //		Invoke ("OnBuyinSliderValueChanged",0.5f);
        SelectedSeatIndex = seatIndex;
    }
    public void OpenWaitingPlayerBuyinPanel(double minBuyin, double maxBuyin, int seatIndex, bool isWaitingPlayer)
    {
        print("minBuyin : " + minBuyin);
        print("maxBuyin : " + maxBuyin);

        if (minBuyin <= 0 || maxBuyin <= 0 || maxBuyin < minBuyin)
        {
            UIManager.Instance.DisplayMessagePanel("Something went wrong", null);
            return;
        }

        isRebuyIn = false;
        IsWaitingPlayer = isWaitingPlayer;
        MinBuyinAmount = minBuyin;

        txtTotalChips.text = /*"Total chips  : " +*/ UIManager.Instance.assetOfGame.SavedLoginData.chips.ConvertToCommaSeparatedValue();

        txtMinBuyin.text = minBuyin.ConvertToCommaSeparatedValue();
        txtMaxBuyin.text = maxBuyin.ConvertToCommaSeparatedValue();

        buyinSlider.minValue = minBuyin.doubleToFloat();
        buyinSlider.maxValue = maxBuyin.doubleToFloat();

        buyinSlider.value = minBuyin.doubleToFloat();//((float)(maxBuyin + minBuyin)) / 2f;
        /* if (UIManager.Instance.GameScreeen.currentRoomData.isPlayFree)
         {
             toggleBuyin.Open();
             toggleBuyin.isOn = true;
         }
         else
         {
             toggleBuyin.Close();
         }*/
        this.Open();
        OnBuyinSliderValueChanged();
        //Invoke ("OnBuyinSliderValueChanged",0.5f);
        SelectedSeatIndex = seatIndex;
    }
    bool isRebuyIn;

    public void OpenReBuyinPanel(double minBuyin, double maxBuyin, double playerChips)
    {
        if (minBuyin < 0f || maxBuyin <= 0f || maxBuyin < minBuyin)
        {
            UIManager.Instance.DisplayMessagePanel("Can't add more chips", null);
            return;
        }
        RemainingTime = 0;
        isRebuyIn = true;
        IsWaitingPlayer = false;
        //  Total chips
        txtTotalChips.text = "Total chips : " + playerChips.ConvertToCommaSeparatedValue();
        MinBuyinAmount = minBuyin;
        MaxReBuyinAmount = maxBuyin;
        txtMinBuyin.text = minBuyin.ConvertToCommaSeparatedValue();
        txtMaxBuyin.text = maxBuyin.ConvertToCommaSeparatedValue();

        buyinSlider.minValue = minBuyin.doubleToFloat();
        buyinSlider.maxValue = maxBuyin.doubleToFloat();

        buyinSlider.value = minBuyin.doubleToFloat();
        this.Open();
        OnBuyinSliderValueChanged();
        //Invoke ("OnBuyinSliderValueChanged",0.5f);
    }

    public void OnBuyinSliderValueChanged()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        //		if (!down) {
        //			int temp = (int)buyinSlider.value;
        //			buyinSlider.value = (float)temp;
        //		}

        //txtSliderSelectedChips.text = buyinSlider.value.ConvertToCommaSeparatedValue ();
        if (buyinSlider.value > buyinSlider.minValue && buyinSlider.value < buyinSlider.maxValue)
        {
            buyinSlider.value = buyinSlider.value.RoundTo(UIManager.Instance.GameScreeen.currentRoomData.smallBlind.doubleToFloat());
        }

        txtSliderSelectedChips.text = buyinSlider.value.ConvertToCommaSeparatedValue();
        txtInputFieldSelectedChips.text = buyinSlider.value.ConvertToCommaSeparatedValue();
        selectedBuyinAmount = buyinSlider.value.floatToDouble();

        down = false;
    }
    public void OnInputFieldSliderValueChanged()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        if (txtInputFieldSelectedChips.text.Equals(""))
        {
            txtInputFieldSelectedChips.text = MinBuyinAmount.ConvertToCommaSeparatedValue();
            selectedBuyinAmount = MinBuyinAmount;
            return;
        }

        double temp = double.Parse(txtInputFieldSelectedChips.text.ToString());
        if (isRebuyIn == true)
        {
            Debug.Log("IF");
            if (temp < MinBuyinAmount || temp > MaxReBuyinAmount)
            {
                UIManager.Instance.DisplayMessagePanel("Enter value in between Buy-In Range", RestoreVal);
                return;
            }
            else
            {
                Debug.Log(temp.ConvertToCommaSeparatedValue());
                txtInputFieldSelectedChips.text = temp.ConvertToCommaSeparatedValue().ToString();
                buyinSlider.value = temp.doubleToFloat();

                selectedBuyinAmount = buyinSlider.value;
            }
        }
        else
        {
            Debug.Log("ELSE");
            if (temp < MinBuyinAmount || temp > MaxBuyinAmount)
            {
                UIManager.Instance.DisplayMessagePanel("Enter value in between Buy-In Range", RestoreVal);
                return;
            }
            else
            {
                txtInputFieldSelectedChips.text = temp.ConvertToCommaSeparatedValue();
                buyinSlider.value = temp.doubleToFloat();
                selectedBuyinAmount = buyinSlider.value;
            }
        }
    }

    void RestoreVal()
    {
        Debug.Log("Here");
        UIManager.Instance.HidePopup();
        selectedBuyinAmount = MinBuyinAmount;
        buyinSlider.value = MinBuyinAmount.doubleToFloat();
        txtInputFieldSelectedChips.contentType = TMP_InputField.ContentType.Standard;
        txtInputFieldSelectedChips.text = MinBuyinAmount.ConvertToCommaSeparatedValue();
    }

    public void OnSelectBuyinButtonTap()
    {
        OnCloseButtonTap();

        //LastBuyinAmount = selectedBuyinAmount;
        //UIManager.Instance.DisplayLoader("");

        selectedBuyinAmount = double.Parse(txtInputFieldSelectedChips.text);
        Debug.Log(selectedBuyinAmount);
        LastBuyinAmount = selectedBuyinAmount;
        UIManager.Instance.DisplayLoader("");

        if (isRebuyIn == true)
        {
            UIManager.Instance.SocketGameManager.PlayerAddChips(selectedBuyinAmount, (socket, packet, args) =>
            {
                Debug.Log("OnPlayerAddChipsDone  : " + packet.ToString());
                UIManager.Instance.HideLoader();

                JSONArray arr = new JSONArray(packet.ToString());
                PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(arr.getString(arr.length() - 1));

                if (resp.message.Length > 0)
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message, null);
                }
            });
        }
        else
        {
            UIManager.Instance.SocketGameManager.JoinRoom(Constants.Poker.TableId, selectedBuyinAmount, _selectedSeatIndex, IsWaitingPlayer, toggleBuyin.isOn ? LastBuyinAmount : 0, (socket, packet, args) =>
            {

                Debug.Log("OnJoinRoomDone  : " + packet.ToString());

                UIManager.Instance.HideLoader();
                JSONArray arr = new JSONArray(packet.ToString());
                string Source;
                Source = arr.getString(arr.length() - 1);
                var resp1 = Source;

                PokerEventResponse<joinroomResult> resp = JsonUtility.FromJson<PokerEventResponse<joinroomResult>>(resp1);

                if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.assetOfGame.SavedLoginData.chips -= selectedBuyinAmount;
                    //					foreach (Button Seats in  UIManager.Instance.GameScreeen.Seats) {
                    //						Seats.interactable = false;
                    //					}

                    UIManager.Instance.GameScreeen.preBetButtonsPanel.toggleSitOutNextBigBlind.isOn = false;
                    UIManager.Instance.GameScreeen.preBetButtonsPanel.toggleSitOutNextHand.isOn = false;
                    UIManager.Instance.GameScreeen.btnStandup.gameObject.SetActive(true);
                    UIManager.Instance.GameScreeen.btnHistoryOpen.gameObject.SetActive(true);
                    UIManager.Instance.GameScreeen.btnClockOpen.gameObject.SetActive(true);
                    if (resp.result.gameStatus.Equals("Running"))
                    {
                        Debug.Log("********");
                        UIManager.Instance.GameScreeen.btnClockOpen.interactable = true;
                    }
                    else
                    {
                        UIManager.Instance.GameScreeen.btnClockOpen.interactable = false;
                        Debug.Log("...");
                    }

                    if (UIManager.Instance.GameScreeen.currentRoomData.isTournament)
                    {
                        UIManager.Instance.GameScreeen.btnClockOpen.gameObject.SetActive(false);
                        UIManager.Instance.GameScreeen.btnHistoryOpen.gameObject.SetActive(false);
                    }
                    else
                    {
                        UIManager.Instance.GameScreeen.btnClockOpen.gameObject.SetActive(true);
                        UIManager.Instance.GameScreeen.btnHistoryOpen.gameObject.SetActive(true);

                    }
                    UIManager.Instance.messagePanelJoinTable.Close();
                    //UIManager.Instance.GameScreeen.btnStandup.gameObject.SetActive(false);
                    //preBetButtonsPanel.toggleSitOutNextBigBlind.gameObject.SetActive (HasJoinedRoom);
                    //preBetButtonsPanel.toggleSitOutNextHand.gameObject.SetActive (HasJoinedRoom);
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(resp.message);
                }

                UIManager.Instance.GameScreeen.HasJoin = resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess);
            });
        }
    }

    bool down;
    public void OnPointerUpButtonTap()
    {
        //		down = false;
    }

    public void OnPointerDownButtonTap()
    {
        //		down = true;
        txtInputFieldSelectedChips.contentType = TMP_InputField.ContentType.DecimalNumber;
    }

    public void OnPlusBuyinButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        buyinSlider.value += UIManager.Instance.GameScreeen.currentRoomData.smallBlind.doubleToFloat();
        buyinSlider.value = buyinSlider.value > buyinSlider.maxValue ? buyinSlider.maxValue : buyinSlider.value;
    }

    public void OnMinusBuyinButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        buyinSlider.value -= UIManager.Instance.GameScreeen.currentRoomData.smallBlind.doubleToFloat();
        buyinSlider.value = buyinSlider.value > buyinSlider.maxValue ? buyinSlider.maxValue : buyinSlider.value;
    }

    public void OnCloseButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }

    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES

    #endregion

    #region GETTER_SETTER

    public double MinBuyinAmount
    {
        get
        {
            return _minBuyinAmount;
        }
        set
        {
            _minBuyinAmount = value;
        }
    }
    public double MaxReBuyinAmount
    {
        get
        {
            return _maxReBuyinAmount;
        }
        set
        {
            _maxReBuyinAmount = value;
        }
    }

    public double MaxBuyinAmount
    {
        get
        {
            return _maxBuyinAmount;
        }
        set
        {
            _maxBuyinAmount = value;
        }
    }

    public int SelectedSeatIndex
    {
        set
        {
            _selectedSeatIndex = value;
        }
    }

    public double LastBuyinAmount
    {
        get
        {
            return _lastBuyinAmount;
        }
        set
        {
            _lastBuyinAmount = value;
        }
    }

    public bool IsWaitingPlayer
    {
        get
        {
            return _isWaitingPlayer;
        }
        set
        {
            _isWaitingPlayer = value;
        }
    }
    public int RemainingTime
    {
        set
        {
            if (value > 0)
            {
                txtRemainingTime.Open();
                txtRemainingTime.text = "Buy-In panel will be close in " + value + " seconds";
            }
            else
            {
                txtRemainingTime.Close();
            }
        }
    }

    #endregion
}