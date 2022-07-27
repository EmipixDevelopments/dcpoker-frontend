using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PanelForgotPassword : MonoBehaviour
{
    [SerializeField] private FlagsOfCountries _flagsOfCountries;
    [Header("Panel 1")]
    [SerializeField] private GameObject _panel1;
    [SerializeField] private TMP_Dropdown _phoneCodeDropdownPanel1;
    [SerializeField] private TMP_InputField _phoneNumberPanel1;
    [SerializeField] private Button _okButton;
    [Header("Panel 2")]
    [SerializeField] private GameObject _panel2;
    [SerializeField] private TMP_Dropdown _phoneCodeDropdownPanel2;
    [SerializeField] private TMP_InputField _phoneNumberPanel2;
    [SerializeField] private TMP_InputField _codePanel2;
    [SerializeField] private Toggle _showCodePanel2;
    [SerializeField] private Button _verifyButton;
    [SerializeField] private GameObject _messageTextPanel2;
    [Header("Panel 3")]
    [SerializeField] private GameObject _panel3;
    [SerializeField] private TMP_InputField _passwordPanel3;
    [SerializeField] private Toggle _showPasswordPanel3;
    [SerializeField] private TMP_InputField _repeatPasswordPanel3;
    [SerializeField] private Toggle _showRepeatPasswordPanel3;
    [SerializeField] private GameObject _messageTextPanel3;
    [SerializeField] private Button _changePassword;

    private int _phoneCodeIndex;
    private string _phoneNumber;
    private string _fullyPhoneNumber;
    private string _password;

    private enum States { First = 1, Second = 2, Third = 3 }
    private States _state = States.First;

    private PhoneCodeAndFlagListData _phoneAndCodeList = new PhoneCodeAndFlagListData();


    private void OnEnable()
    {
        // reset data
        _state = States.First;
        _phoneCodeDropdownPanel1.value = 0;
        _phoneNumberPanel1.text = "";
        _phoneCodeDropdownPanel2.value = 0;
        _phoneNumberPanel2.text = "";
        _codePanel2.text = "";
        _passwordPanel3.text = "";
        _repeatPasswordPanel3.text = "";
        _phoneCodeIndex = 0;
        _phoneNumber = "";
        _fullyPhoneNumber = "";
        _password = "";

        _showCodePanel2.isOn = false;
        _showPasswordPanel3.isOn = false;
        _showRepeatPasswordPanel3.isOn = false;

        ChangeState(_state);
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

    public void OnClickCloseButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }

    public void OnClickButtonNextPanel1()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        string fullyPhoneNumber = _phoneCodeDropdownPanel1.options[_phoneCodeDropdownPanel1.value].text + _phoneNumberPanel1.text;
        fullyPhoneNumber = fullyPhoneNumber.Replace("+", "");
        fullyPhoneNumber = fullyPhoneNumber.Replace("-", "");
        _fullyPhoneNumber = fullyPhoneNumber;

        UIManager.Instance.SocketGameManager.PlayerForgotPassword(fullyPhoneNumber, (socket, packet, args) =>
        {
            Debug.Log("PlayerForgotPassword  : " + packet.ToString());

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
        _phoneNumberPanel2.text = _phoneNumber;
        _showCodePanel2.isOn = false;
    }
    private void UpdateForState2()
    {
        if (_codePanel2.text.Length > 3)
        {
            _verifyButton.interactable = true;
        }
        else
        {
            _verifyButton.interactable = false;
        }
    }

    public void OnClickShowCodeToggle()
    {
        if (_showCodePanel2.isOn)
        {
            _codePanel2.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            _codePanel2.contentType = TMP_InputField.ContentType.Password;
        }
        _codePanel2.ForceLabelUpdate();
    }

    public void OnClickButtonBackPanel2()
    {
        ChangeState(States.First);
    }

    public void OnClickButtonNextPanel2()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        int code = int.Parse(_codePanel2.text);

        UIManager.Instance.SocketGameManager.PlayerConfirmForgotPasswordCode(_fullyPhoneNumber, code, (socket, packet, args) =>
        {
            Debug.Log("PlayerConfirmForgotPasswordCode  : " + packet.ToString());

            JSONArray jsonArray = new JSONArray(packet.ToString());
            string Source = jsonArray.getString(jsonArray.length() - 1);
            PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(Source);
            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                ChangeState(States.Third);
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
        _showPasswordPanel3.isOn = false;
        _showRepeatPasswordPanel3.isOn = false;
    }
    private void UpdateForState3()
    {
        if (_passwordPanel3.text.Length > 3)
        {
            _changePassword.interactable = true;
        }
        else
        {
            _changePassword.interactable = false;
        }
    }

    public void OnClickShowPasswordToggle()
    {
        if (_showPasswordPanel3.isOn)
        {
            _passwordPanel3.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            _passwordPanel3.contentType = TMP_InputField.ContentType.Password;
        }
        _passwordPanel3.ForceLabelUpdate();
    }

    public void OnClickShowRepeatPasswordToggle()
    {
        if (_showRepeatPasswordPanel3.isOn)
        {
            _repeatPasswordPanel3.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            _repeatPasswordPanel3.contentType = TMP_InputField.ContentType.Password;
        }
        _repeatPasswordPanel3.ForceLabelUpdate();
    }

    public void OnClickButtonBackPanel3()
    {
        ChangeState(States.Second);
    }

    public void OnClickButtonNextPanel3()
    {
        StopCoroutine(HideMessagePanel3());
        if (!ComparePaswords()) return;

        string authToken = tokenHack(_fullyPhoneNumber, _password);

        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.PlayerChangeForgetPassword(authToken, (socket, packet, args) =>
        {
            Debug.Log("PlayerChangeForgetPassword  : " + packet.ToString());

            JSONArray jsonArray = new JSONArray(packet.ToString());
            string Source = jsonArray.getString(jsonArray.length() - 1);
            PokerEventResponse<AuthTokenFromJSON> resp = JsonUtility.FromJson<PokerEventResponse<AuthTokenFromJSON>>(Source);
            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                UIManager.Instance.DisplayMessagePanel(resp.message, null);
                gameObject.SetActive(false);
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message, null);
            }
        });
    }

    private bool ComparePaswords()
    {
        bool answer = true;
        _password = _passwordPanel3.text;
        if (_password != _repeatPasswordPanel3.text)
        {
            _messageTextPanel3.SetActive(true);
            StartCoroutine(HideMessagePanel3());
            answer = false;
        }
        return answer;
    }

    IEnumerator HideMessagePanel3()
    {
        yield return new WaitForSeconds(5);
        _messageTextPanel3.SetActive(false);
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

        switch (panel)
        {
            case States.First: _panel1.SetActive(true); break;
            case States.Second: _panel2.SetActive(true); break;
            case States.Third: _panel3.SetActive(true); break;
        }
    }

    private void Update()
    {
        if (_state == States.First) UpdateForState1();
        if (_state == States.Second) UpdateForState2();
        if (_state == States.Third) UpdateForState3();
    }

    public string tokenHack(string mobile, string password)
    {
        rootHackBase root = new rootHackBase();
        root.mobile = mobile;
        root.password = password;
        root.timestamp = DateTimeToUnix(DateTime.UtcNow);
        string json = JsonUtility.ToJson(root);
        return UIManager.Instance.MainHomeScreen.AESEncryption(json);
    }

    public long DateTimeToUnix(DateTime MyDateTime)
    {
        TimeSpan timeSpan = MyDateTime - new DateTime(1970, 1, 1, 0, 0, 0);
        return (long)timeSpan.TotalSeconds;
    }
}
