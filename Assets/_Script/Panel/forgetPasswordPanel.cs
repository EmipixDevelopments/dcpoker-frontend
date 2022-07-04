using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class forgetPasswordPanel : MonoBehaviour
{
    [SerializeField] private FlagsOfCountries _flagsOfCountries;
    [Header("Panel 1")]
    [SerializeField] private GameObject Panel1;
    [SerializeField] private TMP_Dropdown _phoneCodeDropdownPanel1;
    [SerializeField] private TMP_InputField _phoneNumberPanel1;
    [SerializeField] private Button _okButton;
    [Header("Panel 2")]
    [SerializeField] private GameObject Panel2;
    [SerializeField] private TMP_Dropdown _phoneCodeDropdownPanel2;
    [SerializeField] private TMP_InputField _phoneNumberPanel2;
    [SerializeField] private TMP_InputField _codePanel2;
    [SerializeField] private Toggle _showCodePanel2;
    [SerializeField] private Button _verifyButton;
    [SerializeField] private GameObject _messageTextPanel2;
    [Header("Panel 3")]
    [SerializeField] private GameObject Panel3;
    [SerializeField] private TMP_InputField _passwordPanel3;
    [SerializeField] private Toggle _showPasswordPanel3;
    [SerializeField] private TMP_InputField _repeatPasswordPanel3;
    [SerializeField] private Toggle _showRepeatPasswordPanel3;
    [SerializeField] private GameObject _messageTextPanel3;
    [SerializeField] private Button _changePassword;

    private int _phoneCodeIndex;
    private string _phoneNumber;
    private string _password;
    private string _repeatPassword;

    private string _verificationCode = "1234";

    private enum States { First = 1, Second = 2, Third = 3}
    private States _state = States.First;

    private PhoneCodeAndFlagListData _phoneAndCodeList = new PhoneCodeAndFlagListData();


    private void OnEnable()
    {
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
        if (PermissionToGoState2())
        {
            _phoneCodeIndex = _phoneCodeDropdownPanel1.value;
            _phoneNumber = _phoneNumberPanel1.text;
            ChangeState(States.Second);
        }
    }
    private bool PermissionToGoState2() 
    {
        bool serverAnswer = true;
        // Sending data to the server and obtaining permission
        //_code = "" //verification code from server
        return serverAnswer;
    }

    #endregion

    #region Panel 2
    private void StartStateSeccnd()
    {
        OpenPanelAndCloseOther(_state);
        AddOptionToDropdown(_phoneAndCodeList, _phoneCodeDropdownPanel2);
        _phoneCodeDropdownPanel2.value = _phoneCodeIndex;
        _phoneNumberPanel2.text = _phoneNumber;
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
        StopCoroutine(HideMessagePanel2());
        if (_codePanel2.text == _verificationCode)
        {
            ChangeState(States.Third);
        }
        else
        {
            _messageTextPanel2.SetActive(true);
            StartCoroutine(HideMessagePanel2());
        }
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
        if (_passwordPanel3.text == _repeatPasswordPanel3.text)
        {
            ChangePassword();
        }
        else
        {
            _messageTextPanel3.SetActive(true);
            StartCoroutine(HideMessagePanel3());
        }
    }
    IEnumerator HideMessagePanel3()
    {
        yield return new WaitForSeconds(5);
        _messageTextPanel3.SetActive(false);
    }

    private void ChangePassword() 
    {
        // send password on server
        //......

        OnClickCloseButton(); // close window
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
        Panel1.SetActive(false);
        Panel2.SetActive(false);
        Panel3.SetActive(false);

        switch (panel)
        {
            case States.First:  Panel1.SetActive(true); break;
            case States.Second: Panel2.SetActive(true); break;
            case States.Third:  Panel3.SetActive(true); break;
        }
    }

    private void Update()
    {
        if (_state == States.First) UpdateForState1();
        if (_state == States.Second) UpdateForState2();
        if (_state == States.Third) UpdateForState3();
    }







    // need remove
    [Space(50)]
    public bool Oldlogic;
  

    #region PUBLIC_VARIABLES
    public TMP_InputField ValidEmail;
    //	public InputField ValidEmail;
    public Text ErrorText;
    #endregion

    #region PUBLIC_METHODS
    public void closeButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }
    public void OnSubmitButtonTap()
    {
        if (UIManager.Instance.SocketGameManager.HasInternetConnection())
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            if (IsEmailValid())
            {
                ErrorText.text = "";
                string Email = ValidEmail.text;
                UIManager.Instance.SocketGameManager.GetplayerForgotPassword(Email, (socket, packet, args) =>
                {

                    Debug.Log("GetplayerForgotPassword  : " + packet.ToString());

                    UIManager.Instance.HideLoader();

                    //			JSONArray arr = new JSONArray(packet.ToString ());
                    //
                    //			var resp1 = arr.getString(0);
                    JSONArray arr = new JSONArray(packet.ToString());
                    string Source;
                    Source = arr.getString(arr.length() - 1);
                    var resp1 = Source;

                    PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);

                    if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                    {
                        UIManager.Instance.DisplayMessagePanel(resp.message);
                        UIManager.Instance.assetOfGame.SavedLoginData.password = "";
                        ValidEmail.text = "";
                    }
                    else
                    {
                        UIManager.Instance.DisplayMessagePanel(resp.message);
                    }
                });

            }
            else
            {
                ErrorText.text = Constants.Messages.Register.EmailInvalid;
            }
        }
    }

    #endregion

    #region COROUTINES
    //	IEnumerator
    IEnumerator textempti()
    {

        yield return new WaitForSeconds(1.3f);
        ErrorText.text = "";
    }
    #endregion


    #region GETTER_SETTER


    private bool IsEmailValid()
    {
        string email = ValidEmail.text;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);

        if (!match.Success)
        {
            StartCoroutine(textempti());
            return false;
        }
        return true;
    }

    #endregion

}
