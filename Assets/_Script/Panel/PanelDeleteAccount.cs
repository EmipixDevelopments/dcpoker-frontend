using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelDeleteAccount : MonoBehaviour
{
    [SerializeField] private FlagsOfCountries _flagsOfCountries;
    [Header("Panel 1")]
    [SerializeField] private GameObject _panel1;
    [SerializeField] private TMP_Dropdown _phoneCodeDropdownPanel1;
    [SerializeField] private TMP_InputField _phoneNumberPanel1;
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _closeButtonPanel1;
    [Header("Panel 2")]
    [SerializeField] private GameObject _panel2;
    [SerializeField] private TMP_Dropdown _phoneCodeDropdownPanel2;
    [SerializeField] private TMP_InputField _phoneNumberInputFieldPanel2;
    [SerializeField] private TMP_InputField _codeInputFieldPanel2;
    [SerializeField] private Toggle _showCodeTogglePanel2;
    [SerializeField] private Button _verifyButtonPanel2;
    [SerializeField] private Button _backButtonPanel2;
    [SerializeField] private GameObject _messageTextPanel2;
    [Header("Panel 3")]
    [SerializeField] private GameObject _panel3;
    [SerializeField] private TextMeshProUGUI _infoTextPanel3;
    [SerializeField] private Button _deleteButtonPanel3;
    [SerializeField] private Button _cancelButtonPanel3;
    [Header("Panel 4")]
    [SerializeField] private GameObject _panel4;
    [SerializeField] private TextMeshProUGUI _infoTextPanel4;
    [SerializeField] private Button _deleteButtonPanel4;
    [SerializeField] private Button _cancelButtonPanel4;

    private int _phoneCodeIndex;
    private string _phoneNumber;
    private string _fullyPhoneNumber;
    private string _password;

    private enum States { First = 1, Second = 2, Third = 3, Fourth = 4 }
    private States _state = States.First;

    private PhoneCodeAndFlagListData _phoneAndCodeList = new PhoneCodeAndFlagListData();

    private void Start()
    {
        // Initialization panel 1
        _okButton.onClick.RemoveAllListeners();
        _closeButtonPanel1.onClick.RemoveAllListeners();

        _okButton.onClick.AddListener(OnClickButtonNextPanel1);
        _closeButtonPanel1.onClick.AddListener(OnClickCloseButton);

        // Initialization panel 2
        _backButtonPanel2.onClick.RemoveAllListeners();
        _verifyButtonPanel2.onClick.RemoveAllListeners();
        _showCodeTogglePanel2.onValueChanged.RemoveAllListeners();

        _backButtonPanel2.onClick.AddListener(OnClickButtonBackPanel2);
        _verifyButtonPanel2.onClick.AddListener(OnClickButtonNextPanel2);
        _showCodeTogglePanel2.onValueChanged.AddListener(OnClickShowCodeToggle);

        // Initialization Panel 3
        _cancelButtonPanel3.onClick.RemoveAllListeners();
        _deleteButtonPanel3.onClick.RemoveAllListeners();

        _cancelButtonPanel3.onClick.AddListener(OnClickCloseButton);
        _deleteButtonPanel3.onClick.AddListener(OnClickButtonNextPanel3);

        // Initialization Panel 4
        _cancelButtonPanel4.onClick.AddListener(OnClickCloseButton);
        _deleteButtonPanel4.onClick.AddListener(OnClickButtonNextPanel3);
    }

    private void OnEnable()
    {
        // reset data
        _state = States.First;
        _phoneCodeDropdownPanel1.value = 0;
        _phoneNumberPanel1.text = "";
        _phoneCodeDropdownPanel2.value = 0;
        _phoneNumberInputFieldPanel2.text = "";
        _codeInputFieldPanel2.text = "";
        _infoTextPanel3.text = "";
        _phoneCodeIndex = 0;
        _phoneNumber = "";
        _fullyPhoneNumber = "";
        _password = "";

        ChangeState(_state);
    }

    private void Update()
    {
        if (_state == States.First) UpdateForState1();
        if (_state == States.Second) UpdateForState2();
    }

    private void ChangeState(States state)
    {
        _state = state;
        switch (state)
        {
            case States.First:
                StartStateFirst();
                break;
            case States.Second:
                StartStateSeccnd();
                break;
            case States.Third:
                StartStateThird();
                break;
            case States.Fourth:
                StartStateFourth();
                break;
            default:
                break;
        }
    }


    #region Panel 1
    private void StartStateFirst()
    {
        OpenPanelAndCloseOther(_state);
        _phoneAndCodeList.InitializeUsingSettings();
        // or download JSON online
        //string url = "https://drive.google.com/uc?export=download&id=1Qs9VTpx-n8IT2FpXI_jhIJwomLbsuo_P";
        //StartCoroutine(GetData(url));
        AddOptionToDropdown(_phoneAndCodeList, _phoneCodeDropdownPanel1);
    }

    private void UpdateForState1()
    {
        if (_phoneNumberPanel1.text.Length > 3)
        {
            _okButton.interactable = true;
        }
        else
        {
            _okButton.interactable = false;
        }
    }

    private void OnClickCloseButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }

    private void OnClickButtonNextPanel1()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        string fullyPhoneNumber = _phoneCodeDropdownPanel1.options[_phoneCodeDropdownPanel1.value].text + _phoneNumberPanel1.text;
        fullyPhoneNumber = fullyPhoneNumber.Replace("+", "");
        fullyPhoneNumber = fullyPhoneNumber.Replace("-", "");
        _fullyPhoneNumber = fullyPhoneNumber;

        UIManager.Instance.SocketGameManager.DeletePlayerSendCode(fullyPhoneNumber, (socket, packet, args) =>
        {
            Debug.Log("DeletePlayerSendCode  : " + packet.ToString());

            JSONArray jsonArray = new JSONArray(packet.ToString());
            string Source = jsonArray.getString(jsonArray.length() - 1);
            PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(Source);
            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                _phoneCodeIndex = _phoneCodeDropdownPanel1.value;
                _phoneNumber = _phoneNumberPanel1.text;
                ChangeState(States.Second);
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }

    #endregion

    #region Panel 2
    private void StartStateSeccnd()
    {
        OpenPanelAndCloseOther(_state);
        AddOptionToDropdown(_phoneAndCodeList, _phoneCodeDropdownPanel2);
        _phoneCodeDropdownPanel2.value = _phoneCodeIndex;
        _phoneNumberInputFieldPanel2.text = _phoneNumber;
    }
    private void UpdateForState2()
    {
        if (_codeInputFieldPanel2.text.Length > 3)
        {
            _verifyButtonPanel2.interactable = true;
        }
        else
        {
            _verifyButtonPanel2.interactable = false;
        }
    }

    private void OnClickShowCodeToggle(bool value)
    {
        if (value)
        {
            _codeInputFieldPanel2.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            _codeInputFieldPanel2.contentType = TMP_InputField.ContentType.Password;
        }
        _codeInputFieldPanel2.ForceLabelUpdate();
    }

    private void OnClickButtonBackPanel2()
    {
        ChangeState(States.First);
    }

    private void OnClickButtonNextPanel2()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        int code = int.Parse(_codeInputFieldPanel2.text);

        UIManager.Instance.SocketGameManager.DeletePlayerConfirmCode(_fullyPhoneNumber, code, (socket, packet, args) =>
        {
            Debug.Log("DeletePlayerConfirmCode  : " + packet.ToString());

            JSONArray jsonArray = new JSONArray(packet.ToString());
            string Source = jsonArray.getString(jsonArray.length() - 1);
            PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(Source);
            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                if (UIManager.Instance.assetOfGame.SavedLoginData.cash <= 0)
                {
                    ChangeState(States.Third);
                }
                else
                {
                    ChangeState(States.Fourth);
                }
            }
            else
            {
                _messageTextPanel2.SetActive(true);
                StartCoroutine(HideMessagePanel2());
            }
        });
    }
    IEnumerator HideMessagePanel2()
    {
        yield return new WaitForSeconds(5);
        _messageTextPanel2.SetActive(false);
    }
    #endregion

    #region Panel 3
    private void StartStateThird()
    {
        OpenPanelAndCloseOther(_state);

        string userName = UIManager.Instance.assetOfGame.SavedLoginData.Username;

        _infoTextPanel3.text = $"You are about to delete your account \n" +
            $"“<b>{userName}</b>” \n" +
            $"Are you sure";
    }

    private void OnClickButtonNextPanel3()
    {
        string authToken = tokenHack(_fullyPhoneNumber, _password);

        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.DeleteUser((socket, packet, args) =>
        {
            Debug.Log("DeleteUser  : " + packet.ToString());

            JSONArray jsonArray = new JSONArray(packet.ToString());
            string Source = jsonArray.getString(jsonArray.length() - 1);
            PokerEventResponse<AuthTokenFromJSON> resp = JsonUtility.FromJson<PokerEventResponse<AuthTokenFromJSON>>(Source);
            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                UIManager.Instance.LobbyPanelNew.Close();
                UIManager.Instance.MainHomeScreen.Open();

                UIManager.Instance.DisplayMessagePanel(resp.message, null);
                gameObject.SetActive(false);
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message, null);
            }
        });
    }
    #endregion

    #region Panel 4
    private void StartStateFourth()
    {
        OpenPanelAndCloseOther(_state);

        double cash = UIManager.Instance.assetOfGame.SavedLoginData.cash;
        string userName = UIManager.Instance.assetOfGame.SavedLoginData.Username;

        _infoTextPanel4.text = 
            $"You are about to delete your account\n" +
            $"“<b>{userName}</b>”\n" +
            $"\n" +
            $"Other assets <b>${cash:#.##}</b>\n" +
            $"will be transferred to the recently used\n" +
            $"source payment method. Or will be lost if the transfer is not possible.";
    }
    #endregion

    private void AddOptionToDropdown(PhoneCodeAndFlagListData phoneAndCodeList, TMP_Dropdown dropdown)
    {
        // added phone code options
        dropdown.options.Clear();
        foreach (var item in phoneAndCodeList.List)
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
            optionData.text = item.PhoneCode;
            optionData.image = _flagsOfCountries.GetSpriteByName(item.FlagName);
            dropdown.options.Add(optionData);
        }
        dropdown.options.Add(new TMP_Dropdown.OptionData());
    }

    private void OpenPanelAndCloseOther(States panel)
    {
        _panel1.SetActive(false);
        _panel2.SetActive(false);
        _panel3.SetActive(false);
        _panel4.SetActive(false);

        switch (panel)
        {
            case States.First: _panel1.SetActive(true); break;
            case States.Second: _panel2.SetActive(true); break;
            case States.Third: _panel3.SetActive(true); break;
            case States.Fourth: _panel4.SetActive(true); break;
        }
    }

    private string tokenHack(string mobile, string password)
    {
        rootHackBase root = new rootHackBase();
        root.mobile = mobile;
        root.password = password;
        root.timestamp = DateTimeToUnix(DateTime.UtcNow);
        string json = JsonUtility.ToJson(root);
        return UIManager.Instance.MainHomeScreen.AESEncryption(json);
    }

    private long DateTimeToUnix(DateTime MyDateTime)
    {
        TimeSpan timeSpan = MyDateTime - new DateTime(1970, 1, 1, 0, 0, 0);
        return (long)timeSpan.TotalSeconds;
    }
}
