using BestHTTP;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    [SerializeField] private FlagsOfCountries _flags;
    [SerializeField] private TMP_Dropdown _phoneCodeDropdown;
    [SerializeField] private TMP_InputField _phoneNumber;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private Toggle _showPasswordToggle;
    [SerializeField] private Toggle _rememberMeToggle;
    [SerializeField] private TMP_Text _messageText;


    private PhoneCodeAndFlagListData _phoneAndCodeList = new PhoneCodeAndFlagListData();

    void OnEnable()
    {
        _phoneAndCodeList.InitializeUsingSettings();
        // or download JSON online
        //string url = "https://drive.google.com/uc?export=download&id=1Qs9VTpx-n8IT2FpXI_jhIJwomLbsuo_P";
        //StartCoroutine(GetData(url));

        AddOptionToDropdown(_phoneAndCodeList);
        LoadFieldsState();
    }

    IEnumerator GetData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogError("Error get phone JSON");
        }
        else
        {
            _phoneAndCodeList = JsonUtility.FromJson<PhoneCodeAndFlagListData>(request.downloadHandler.text);

            AddOptionToDropdown(_phoneAndCodeList);
        }
    }

    private void AddOptionToDropdown(PhoneCodeAndFlagListData phoneAndCodeList)
    {
        // added phone code options
        _phoneCodeDropdown.options.Clear();
        foreach (var item in phoneAndCodeList.List)
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
            optionData.text = item.PhoneCode;
            optionData.image = _flags.GetSpriteByName(item.FlagName);
            _phoneCodeDropdown.options.Add(optionData);
        }
        _phoneCodeDropdown.options.Add(new TMP_Dropdown.OptionData());
    }
    private void LoadFieldsState()
    {
        if (_messageText != null)
        {
            _messageText.text = "";
        }
        _showPasswordToggle.isOn = false;

        if (UIManager.Instance && UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe)
        {
            string phoneCode = UIManager.Instance.assetOfGame.SavedLoginData.phoneCode;
            int index = 0;
            for (int i = 0; i < _phoneCodeDropdown.options.Count; i++)
            {
                if (_phoneCodeDropdown.options[i].text == phoneCode)
                {
                    index = i;
                }
            }
            _phoneCodeDropdown.value = index;

            _rememberMeToggle.isOn = UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe;
            _phoneNumber.text = UIManager.Instance.assetOfGame.SavedLoginData.phoneNumber;
            _password.text = UIManager.Instance.assetOfGame.SavedLoginData.password;
        }
        else
        {
            if (UIManager.Instance)
            {
                _rememberMeToggle.isOn = UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe;
            }
            _phoneNumber.text = "";
            _password.text = "";
            _phoneCodeDropdown.value = 0;
        }
        if (UIManager.Instance)
        {
            UIManager.Instance.HideLoader();
        }
    }

    public void OnClickLoginButton(bool forceLoin = false)
    {
        if (CheckDataBeforeLogin())
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            if (UIManager.Instance.SocketGameManager.HasInternetConnection())
            {
                string fullPhoneNumber = _phoneCodeDropdown.options[_phoneCodeDropdown.value].text + _phoneNumber.text;
                fullPhoneNumber = fullPhoneNumber.Replace("+","");
                fullPhoneNumber = fullPhoneNumber.Replace("-", "");
                string password = _password.text;
                string AuthTokenverify = tokenHack(fullPhoneNumber, password);
                UIManager.Instance.DisplayLoader("");
                HTTPRequest httpRequest = new HTTPRequest(new Uri("http://ip-api.com/json"), (request, response) =>
                {
                    JSON_Object data = new JSON_Object(response.DataAsText);

                    string ipAddress = "NA";
                    if (data.has("ip"))
                    {
                        ipAddress = data.getString("ip");
                    }
                    else if (data.has("query"))
                    {
                        ipAddress = data.getString("query");
                    }
                    // login and password not used
                    UIManager.Instance.SocketGameManager.Login(AuthTokenverify, ipAddress, forceLoin, (socket, packet, args) =>
                    {
                        Debug.Log("login = " + packet.ToString());
                        UIManager.Instance.HideLoader();
                        JSONArray arr = new JSONArray(packet.ToString());
                        string Source;
                        Source = arr.getString(arr.length() - 1);
                        var resp = Source;

                        PokerEventResponse<PlayerLoginResponse> loginResponse = JsonUtility.FromJson<PokerEventResponse<PlayerLoginResponse>>(resp);
                        if (loginResponse.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                        {
                            UIManager.Instance.webglToken = loginResponse.result.deviceId;
                            UIManager.Instance.IsMultipleTableAllowed = loginResponse.result.isMultipleTableAllowed;
                            UIManager.Instance.isChipsTransferAllowed = loginResponse.result.isChipsTransferAllowed;
                            UIManager.Instance.assetOfGame.SavedLoginData.chips = loginResponse.result.chips;
                            UIManager.Instance.assetOfGame.SavedLoginData.PlayerId = loginResponse.result.id;
                            UIManager.Instance.assetOfGame.SavedLoginData.PlayerId = loginResponse.result.playerId;
                            UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar = loginResponse.result.profilePic;
                            UIManager.Instance.assetOfGame.SavedLoginData.isCash = loginResponse.result.isCash;
                            UIManager.Instance.assetOfGame.SavedLoginData.isSuperPlayer = loginResponse.result.isSuperPlayer;
                            UIManager.Instance.MySuperPlayer = loginResponse.result.isSuperPlayer;

                            UIManager.Instance.assetOfGame.SavedLoginData.isInAppPurchaseAllowed = loginResponse.result.isInAppPurchaseAllowed;
                            UIManager.Instance.ProfilePic = UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar;
                            UIManager.Instance.assetOfGame.SavedLoginData.Username = loginResponse.result.username;//LoginUsername.text;

                            UIManager.Instance.assetOfGame.SavedLoginData.password = _password.text;
                            UIManager.Instance.assetOfGame.SavedLoginData.phoneNumber = _phoneNumber.text;
                            UIManager.Instance.assetOfGame.SavedLoginData.phoneCode = _phoneCodeDropdown.options[_phoneCodeDropdown.value].text;

                            UIManager.Instance.assetOfGame.SavedLoginData.userUuid = loginResponse.result.userUuid;
                            UIManager.Instance.assetOfGame.SavedLoginData.accountNumber = loginResponse.result.accountNumber;
                            UIManager.Instance.assetOfGame.SavedLoginData.mobile = loginResponse.result.mobile;
                            if (_rememberMeToggle.isOn)
                            {
                                UIManager.Instance.SetPlayerLoginType(1);
                            }
                            UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe = _rememberMeToggle.isOn;
                            SaveLoad.SaveGame();
                            //------------------//

                            //						UIManager.Instance.assetOfGame.SavedLoginData.isCash= false;
                            if (UIManager.Instance.assetOfGame.SavedLoginData.isCash)
                            {
                                UIManager.Instance.currencyType = UIManager.CurrencyType.cash;
                            }
                            else
                            {
                                UIManager.Instance.currencyType = UIManager.CurrencyType.coin;
                            }
                            UIManager.Instance.SetCurrencyImages();
                            UIManager.Instance.ipLocationService.SendIPAddress("login");

                            UIManager.Instance.LobbyPanelNew.Open(); // LobbyScreeen not used more
                            UIManager.Instance.MainHomeScreen.Close();
                        }
                        else
                        {
                            if (loginResponse.message == "updateApp")
                            {
                                UIManager.Instance.DisplayConfirmationPanel("Please update the game By click on Update", "Update", "Exit Game",
                                    () =>
                                    {
                                        Application.OpenURL(loginResponse.result.storeUrl);
                                    },
                                    () =>
                                    {
                                        Application.Quit();
                                    });
                            }
                            else if (loginResponse.message == "alreadyLogin")
                            {
                                UIManager.Instance.DisplayConfirmationPanel("Same player is already logged in from another device, are you sure you want to login?", "Login", "Cancel",
                                        () =>
                                        {
                                            UIManager.Instance.messagePanel.Close();
                                            OnClickLoginButton(true);
                                        }, () =>
                                        {
                                            UIManager.Instance.messagePanel.Close();
                                        });
                            }
                            else if (loginResponse.status == "forceLogout")
                            {

                            }
                            else
                            {
                                UIManager.Instance.DisplayMessagePanel(loginResponse.message, null);
                            }
                        }
                    });
                });
                httpRequest.Send();
            }
        }
    }

    public void OnClickShowPasswordToggle()
    {
        if (_showPasswordToggle.isOn)
        {
            _password.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            _password.contentType = TMP_InputField.ContentType.Password;
        }
        _password.ForceLabelUpdate();
    }

    public void closeButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }

    public void OnRegisterButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.MainHomeScreen.registerScreen.Open();
        UIManager.Instance.MainHomeScreen.LoginScreen.Close();
    }

    public void OnForgotpasswordButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.MainHomeScreen.ForgotPasswordScreen.Open();
        UIManager.Instance.MainHomeScreen.LoginScreen.Close();
    }

    private bool CheckDataBeforeLogin()
    {
        string phoneCode = _phoneCodeDropdown.options[_phoneCodeDropdown.value].text;
        string phoneNumber = _phoneNumber.text;
        string password = _password.text;

        if (string.IsNullOrEmpty(phoneCode))
        {
            _messageText.text = Constants.Messages.Login.PhoneCodeEmpty;
            return false;
        }
        if (string.IsNullOrEmpty(phoneNumber))
        {
            _messageText.text = Constants.Messages.Login.PhoneNumberEmpty;
            return false;
        }
        else if (string.IsNullOrEmpty(password))
        {
            _messageText.text = Constants.Messages.Login.PasswordEmpty;
            return false;
        }
        else if (password.Length < Constants.Messages.Login.PasswordLength)
        {
            _messageText.text = Constants.Messages.Login.MinPasswordLength;
            return false;
        }

        return true;
    }
    public string tokenHack(string mobile, string password)
    {
        string syndicate = null;
        rootHackBase root = new rootHackBase();
        root.mobile = mobile;
        root.password = password;
        var dateTime = DateTime.UtcNow.AddSeconds(-9); // fix
        root.timestamp = DateTimeToUnix(dateTime);
        string json = JsonUtility.ToJson(root);
        //Debug.LogError("Not Encrypt: " + json);
        string encryptedJson = UIManager.Instance.MainHomeScreen.AESEncryption(json);
        //Debug.LogError("Encrypt: " + encryptedJson);

        syndicate = encryptedJson;
        //syndicate = System.Uri.EscapeDataString(syndicate);
        //print(syndicate);
        return syndicate;
    }
    public long DateTimeToUnix(DateTime MyDateTime)
    {
        TimeSpan timeSpan = MyDateTime - new DateTime(1970, 1, 1, 0, 0, 0);
        return (long)timeSpan.TotalSeconds;
    }
}
