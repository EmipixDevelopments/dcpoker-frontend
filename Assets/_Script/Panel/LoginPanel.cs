﻿using BestHTTP;
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


    [Header("Note. Remove old UI logic after finish")]
    [SerializeField] bool _useNewUI;

    private PhoneCodeAndFlagListData _phoneAndCodeList = new PhoneCodeAndFlagListData();

    void OnEnable()
    {
        if (_useNewUI)
        {
            _phoneAndCodeList.InitializeUsingSettings();
            // or download JSON online
            //string url = "https://drive.google.com/uc?export=download&id=1Qs9VTpx-n8IT2FpXI_jhIJwomLbsuo_P";
            //StartCoroutine(GetData(url));

            AddOptionToDropdown(_phoneAndCodeList);
            LoadFieldsState();
        }
        else
        {
            ResetData(); // old logic
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !UIManager.Instance.loader.gameObject.activeSelf)
        {
            StopCoroutine("PreviousScreen");
            StartCoroutine(PreviousScreen(0f));
        }
    }

    IEnumerator GetData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.Send();
        if (request.isError)
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

        if (UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe)
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
            _phoneNumber.text = UIManager.Instance.assetOfGame.SavedLoginData.Username;
            _password.text = UIManager.Instance.assetOfGame.SavedLoginData.password;
        }
        else
        {
            _rememberMeToggle.isOn = UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe;
            _phoneNumber.text = "";
            _password.text = "";
            _phoneCodeDropdown.value = 0;
        }
        UIManager.Instance.HideLoader();
    }

    public void OnClickLoginButton(bool forceLoin = false)
    {
        if (CheckDataBeforeLogin())
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            if (UIManager.Instance.SocketGameManager.HasInternetConnection())
            {
                string username = _phoneCodeDropdown.options[_phoneCodeDropdown.value].text + _phoneNumber.text;
                username = username.ToLower();
                string password = _password.text;
                string AuthTokenverify = tokenHack(username, password);
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
                    UIManager.Instance.SocketGameManager.Login(username, password, AuthTokenverify, ipAddress, forceLoin, (socket, packet, args) =>
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
                            UIManager.Instance.assetOfGame.SavedLoginData.userUuid = loginResponse.result.userUuid;
                            UIManager.Instance.assetOfGame.SavedLoginData.accountNumber = loginResponse.result.accountNumber;
                            UIManager.Instance.assetOfGame.SavedLoginData.mobile = loginResponse.result.mobile;
                            if (_rememberMeToggle.isOn)
                            {
                                UIManager.Instance.SetPlayerLoginType(1);
                            }
                            UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe = _rememberMeToggle.isOn;
                            //LocalSaveData.current.Username = LoginUsername.text;//UIManager.Instance.assetOfGame.SavedLoginData.Username;
                            LocalSaveData.current.password = UIManager.Instance.assetOfGame.SavedLoginData.password;
                            LocalSaveData.current.isRememberMe = UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe;
                            SaveLoad.SaveGame();

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
                            StartCoroutine(NextScreen(0f));
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
                                            LoginButtonTap(true);
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
    public string tokenHack(string username, string password)
    {
        string syndicate = null;
        rootHackBase root = new rootHackBase();
        root.username = username;
        root.password = password;
        root.timestamp = DateTimeToUnix(DateTime.UtcNow);
        string json = JsonUtility.ToJson(root);
        //Debug.Log(json);
        string encryptedJson = UIManager.Instance.MainHomeScreen.AESEncryption(json);
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


    















        // old logic
        [SerializeField]
        [Space(50)]
        public string OldLogic;
    
        #region PUBLIC_VARIABLES
    
        [Header("InputField")]
        public TMP_InputField LoginUsername;
        public TMP_InputField LoginPassword;
        public Text txtMessage;
        [Header("Toggle")]
        public Toggle RememberMe;
    
        #endregion
    
    
        #region UNITY_CALLBACKS
    
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    
    

    
        #endregion
    
    
        #region PUBLIC_METHODS
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="forceLoin"></param>
        public void LoginButtonTap(bool forceLoin = false)
        {
            if (IsLoginDetailValid())
            {
                UIManager.Instance.SoundManager.OnButtonClick();
                if (UIManager.Instance.SocketGameManager.HasInternetConnection())
                {
                    string username = LoginUsername.text.ToLower();
                    string password = LoginPassword.text;
                    string AuthTokenverify = tokenHack(username, password);
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
                        UIManager.Instance.SocketGameManager.Login(username, password, AuthTokenverify, ipAddress, forceLoin, (socket, packet, args) =>
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
                                 UIManager.Instance.assetOfGame.SavedLoginData.password = LoginPassword.text;
                             UIManager.Instance.assetOfGame.SavedLoginData.userUuid = loginResponse.result.userUuid;
                             UIManager.Instance.assetOfGame.SavedLoginData.accountNumber = loginResponse.result.accountNumber;
                             UIManager.Instance.assetOfGame.SavedLoginData.mobile = loginResponse.result.mobile;
                             if (RememberMe.isOn)
                             {
                                 UIManager.Instance.SetPlayerLoginType(1);
                             }
                             UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe = RememberMe.isOn;
                             LocalSaveData.current.Username = LoginUsername.text;//UIManager.Instance.assetOfGame.SavedLoginData.Username;
                                 LocalSaveData.current.password = UIManager.Instance.assetOfGame.SavedLoginData.password;
                             LocalSaveData.current.isRememberMe = UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe;
                             SaveLoad.SaveGame();
    
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
                             StartCoroutine(NextScreen(0f));
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
                                             LoginButtonTap(true);
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
            //UIManager.Instance.SavedGameData.SaveGame ();
        }
    
        #endregion
    
        #region PRIVATE_METHODS
    
        void ResetData()
        {
            if (txtMessage != null)
            {
                txtMessage.text = "";
            }
    
            if (UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe)
            {
                RememberMe.isOn = UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe;
                LoginUsername.text = UIManager.Instance.assetOfGame.SavedLoginData.Username;
                LoginPassword.text = UIManager.Instance.assetOfGame.SavedLoginData.password;
                /*
        #if UNITY_ANDROID || UNITY_IOS
                            UIManager.Instance.autoLoginLoader.OnAutoLogin();
        #endif
                //			LoginButtonTap ();
                */
            }
            else
            {
                RememberMe.isOn = UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe;
                LoginUsername.text = "";
                LoginPassword.text = "";
            }
            /*
            #if UNITY_EDITOR
                LoginUsername.text = "superplayer1";
                        LoginPassword.text = "123456";
            #endif*/
            UIManager.Instance.HideLoader();
        }
    
        #endregion
    
        #region COROUTINES
    
        IEnumerator NextScreen(float timer)
        {
            UIManager.Instance.DisplayLoader("");
            yield return new WaitForSeconds(timer);
            UIManager.Instance.MainHomeScreen.Close();
            //UIManager.Instance.GameSelectionScreen.Open();
            UIManager.Instance.LobbyScreeen.Open();
        }
    
        IEnumerator PreviousScreen(float timer)
        {
            UIManager.Instance.DisplayLoader("");
            yield return new WaitForSeconds(timer);
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
                Application.Quit();
    #endif
        }
    
        #endregion
    
        #region GETTER_SETTER

        private bool IsLoginDetailValid()
        {
            string username = LoginUsername.text;
            string password = LoginPassword.text;
    
            if (string.IsNullOrEmpty(username))
            {
                print("Username is empty");
                txtMessage.text = Constants.Messages.Login.UsernameEmpty;
                return false;
            }
            else if (string.IsNullOrEmpty(password))
            {
                txtMessage.text = Constants.Messages.Login.PasswordEmpty;
                return false;
            }
            else if (password.Length < Constants.Messages.Login.PasswordLength)
            {
                txtMessage.text = Constants.Messages.Login.MinPasswordLength;
                return false;
            }
    
            return true;
        }
    
    
        #endregion

}
