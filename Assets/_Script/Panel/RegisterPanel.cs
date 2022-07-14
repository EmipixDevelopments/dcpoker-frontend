using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class RegisterPanel : MonoBehaviour
{
    [SerializeField] private FlagsOfCountries _flags;
    [SerializeField] private TMP_Dropdown _phoneCode;
    [SerializeField] private TMP_InputField _phoneNumber;
    [SerializeField] private TMP_InputField _userName;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_InputField _repeatPassword;
    [SerializeField] private Toggle _showPassword;
    [SerializeField] private Toggle _showRepeatPassword;
    [SerializeField] private TMP_Text _massageText;

    [Header("Other user name panel")]
    [SerializeField] private OtherUserNamePanel _otherUserNamePanel; // need created class and logic
    [SerializeField] private TMP_Text _userNameTaken;

    private PhoneCodeAndFlagListData _phoneAndCodeList = new PhoneCodeAndFlagListData();

    void OnEnable()
    {
        ResetAllInputFields();

        _phoneAndCodeList.InitializeUsingSettings();
        // or download JSON online
        //string url = "https://drive.google.com/uc?export=download&id=1Qs9VTpx-n8IT2FpXI_jhIJwomLbsuo_P";
        //StartCoroutine(GetData(url));
        AddOptionToDropdown(_phoneAndCodeList);
    }

    private void Start()
    {
        _otherUserNamePanel.OnSelectName = SelectOtherName;
    }

    private void OnDisable()
    {
        _otherUserNamePanel.OnSelectName -= SelectOtherName;
    }

    private void AddOptionToDropdown(PhoneCodeAndFlagListData phoneAndCodeList)
    {
        // added phone code options
        _phoneCode.options.Clear();
        foreach (var item in phoneAndCodeList.List)
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
            optionData.text = item.PhoneCode;
            optionData.image = _flags.GetSpriteByName(item.FlagName);
            _phoneCode.options.Add(optionData);
        }
        _phoneCode.options.Add(new TMP_Dropdown.OptionData());
    }

    private void ResetAllInputFields()
    {
        _phoneCode.value = 0;
        _phoneNumber.text = "";
        _userName.text = "";
        _password.text = "";
        _repeatPassword.text = "";
        _otherUserNamePanel.gameObject.SetActive(false);
    }

    public void OnClickShowPasswordToggle()
    {
        if (_showPassword.isOn)
        {
            _password.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            _password.contentType = TMP_InputField.ContentType.Password;
        }
        _password.ForceLabelUpdate();
    }

    public void OnClickShowRepeatPasswordToggle()
    {
        if (_showRepeatPassword.isOn)
        {
            _repeatPassword.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            _repeatPassword.contentType = TMP_InputField.ContentType.Password;
        }
        _repeatPassword.ForceLabelUpdate();
    }

    public void OnClickLoginButton() 
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.MainHomeScreen.LoginScreen.Open();
        UIManager.Instance.MainHomeScreen.registerScreen.Close();
    }

    public void OnClickCloseButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }


    public void OnRegisterButtonTap()
    {
        string username = _userName.text;
        string password = _password.text;
        string phoneNumber = _phoneNumber.text;
        string repeatPassword = _repeatPassword.text;
        string phoneCode = _phoneCode.options[_phoneCode.value].text;

        string fullymobylePhone = phoneCode + phoneNumber;
        fullymobylePhone = fullymobylePhone.Replace("+", "");
        fullymobylePhone = fullymobylePhone.Replace("-", "");

        if (UIManager.Instance.SocketGameManager.HasInternetConnection())
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            if (IsLoginDetailValid())
            {
                UIManager.Instance.DisplayLoader("");

                UIManager.Instance.SocketGameManager.RegisterPlayer(username, password, fullymobylePhone, "", (socket, packet, args) =>
                {
                    Debug.Log("RegisterPlayer  => " + packet.ToString());

                    UIManager.Instance.HideLoader();
                    JSONArray arr = new JSONArray(packet.ToString());
                    string Source;
                    Source = arr.getString(arr.length() - 1);
                    var resp = Source;
                    PokerEventResponse registrationResp = JsonUtility.FromJson<PokerEventResponse>(resp);

                    if (registrationResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                    {
                        UIManager.Instance.DisplayMessagePanel(registrationResp.message, () =>
                        {
                            this.Close();
                            UIManager.Instance.HidePopup();
                        });
                    }
                    else if (registrationResp.message == "Username already taken.")
                    {
                        ShowOtherUserNameWindow(registrationResp.result);
                    }
                    else
                    {
                        UIManager.Instance.DisplayMessagePanel(registrationResp.message, null);
                    }
                });
            }
        }
    }

    private void ShowOtherUserNameWindow(string otherNames) 
    {
        _userNameTaken.gameObject.SetActive(true);

        _otherUserNamePanel.gameObject.SetActive(true);
        string[] nameArr = otherNames.Split(',');
        _otherUserNamePanel.Init(nameArr);
    }

    private void SelectOtherName(string name)
    {
        _userName.text = name;
        _userNameTaken.gameObject.SetActive(false);
    }

    private bool IsLoginDetailValid()
    {
        StopCoroutine(textempti());

        string username = _userName.text;
        string password = _password.text;
        string repeatPassword = _repeatPassword.text;

        if (string.IsNullOrEmpty(username))
        {
            _massageText.text = Constants.Messages.Register.UsernameEmpty;
            StartCoroutine(textempti());
            return false;
        }

        else if (!IsUsernameValid())
        {
            _massageText.text = Constants.Messages.Register.UsernameInvalid;
            StartCoroutine(textempti());
            return false;
        }
        else if (string.IsNullOrEmpty(password))
        {
            _massageText.text = Constants.Messages.Register.PasswordEmpty;
            StartCoroutine(textempti());
            return false;
        }
        else if (password.Length < Constants.Messages.Login.PasswordLength)
        {
            _massageText.text = Constants.Messages.Register.MinPasswordLength;
            StartCoroutine(textempti());
            return false;
        }
        else if (string.IsNullOrEmpty(repeatPassword))
        {
            _massageText.text = Constants.Messages.Register.ConfirmPasswordEmpty;
            StartCoroutine(textempti());
            return false;
        }
        else if (!password.Equals(repeatPassword))
        {
            _massageText.text = Constants.Messages.Register.PasswordNotMatched;
            StartCoroutine(textempti());
            return false;
        }
        return true;
    }

    private bool IsUsernameValid()
    {
        string number = _userName.text;

        if (number.Length < 4)
        {
            return false;
        }
        return true;
    }
    IEnumerator textempti()
    {
        yield return new WaitForSeconds(4.3f);
        _massageText.text = "";
    }
}