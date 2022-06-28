using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using BestHTTP;
using UnityEngine.Networking;

public class LoginPanel : MonoBehaviour
{
    [SerializeField] private FlagsOfCountries _flags;
    [SerializeField] private TMP_Dropdown _phoneCodeDropdown;

    private PhoneCodeAndFlagListData _phoneAndCodeList = new PhoneCodeAndFlagListData();

    void OnEnable()
    {
        ResetData(); // old logic

        #region Filling dropdown phone element
        // in code
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+93", FlagName = "afghanistan" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+358", FlagName = "finland" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+355", FlagName = "albania" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+21", FlagName = "algeria" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+376", FlagName = "andorra" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+244", FlagName = "angola" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+1-268", FlagName = "antigua-barbuda" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+54", FlagName = "argentina" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+374", FlagName = "armenia" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+31", FlagName = "aruba" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+61", FlagName = "australia" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+994", FlagName = "azerbaijan" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+1-242", FlagName = "bahamas" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+973", FlagName = "bahrain" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+880", FlagName = "bangladesh" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+1-246", FlagName = "barbados" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+375", FlagName = "belarus" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+501", FlagName = "belize" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+229", FlagName = "benin" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+975", FlagName = "bhutan" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+591", FlagName = "bolivia" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+387", FlagName = "bosnia-herzegovina" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+267", FlagName = "botswana" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+55", FlagName = "brazil" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+226", FlagName = "burkina-faso" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+95", FlagName = "myanmar-burma" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+257", FlagName = "burundi" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+237", FlagName = "cameroon" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+235", FlagName = "chad" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+61", FlagName = "cocos-keeling-islands" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+269", FlagName = "comoros" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+253", FlagName = "djibouti" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+20", FlagName = "egypt" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+220", FlagName = "gambia" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+224", FlagName = "guinea" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+62", FlagName = "indonesia" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+98", FlagName = "iran" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+964", FlagName = "iraq" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+962", FlagName = "jordan" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+7", FlagName = "kazakhstan" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+383", FlagName = "kosovo" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+965", FlagName = "kuwait" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+961", FlagName = "lebanon" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+218", FlagName = "libya" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+60", FlagName = "malaysia" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+960", FlagName = "maldives" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+223", FlagName = "mali" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+222", FlagName = "mauritania" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+262", FlagName = "mayotte" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+212", FlagName = "morocco" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+227", FlagName = "niger" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+92", FlagName = "pakistan" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+966", FlagName = "saudi-arabia" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+221", FlagName = "senegal" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+252", FlagName = "somalia" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+249", FlagName = "sudan" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+963", FlagName = "syria" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+992", FlagName = "tajikistan" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+216", FlagName = "tunisia" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+993", FlagName = "turkmenistan" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+971", FlagName = "united-arab-emirates" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+998", FlagName = "uzbekistan" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+212", FlagName = "western-sahara" });
        _phoneAndCodeList.List.Add(new CodeAndFlag() { PhoneCode = "+967", FlagName = "yemen" });

        // or download JSON online
        //string url = "https://drive.google.com/uc?export=download&id=1Qs9VTpx-n8IT2FpXI_jhIJwomLbsuo_P";
        //StartCoroutine(GetData(url));
        #endregion
        AddOptionToDropdown(_phoneAndCodeList);
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











        // old logic
        [SerializeField]
        [Space(50)]
        public bool NewLogic;
    
        #region PUBLIC_VARIABLES
    
        [Header("InputField")]
        public TMP_InputField LoginUsername;
        public TMP_InputField LoginPassword;
        public Text txtMessage;
        public Text txtdemo;
        [Header("Toggle")]
        public Toggle RememberMe;
    
        public Image imgEng, imgMongolian;
    
        #endregion
    
    
        #region UNITY_CALLBACKS
    
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    
        void SSS()
        {
            this.Open();
        }
    
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !UIManager.Instance.loader.gameObject.activeSelf)
            {
                StopCoroutine("PreviousScreen");
                StartCoroutine(PreviousScreen(0f));
            }
        }
    
        #endregion
    
    
        #region PUBLIC_METHODS
    
        public void GILButtonTap()
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            Utility.Instance.OpenLink("https://gamingintegritylabs.com");
        }
        public void closeButtonTap()
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            this.Close();
        }
        public void LowerCapsFunction()
        {
            LoginUsername.text = LoginUsername.text.ToLower();
        }
    
        public void OnRegisterButtonTap()
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            UIManager.Instance.MainHomeScreen.registerScreen.Open();
            UIManager.Instance.MainHomeScreen.LoginScreen.Close();
        }
    
        public void OnEnglish()
        {
            imgEng.Open();
            imgMongolian.Close();
        }
    
        public void OnMongolian()
        {
            imgEng.Close();
            imgMongolian.Open();
        }
    
        public void OnForgotpasswordButtonTap()
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            UIManager.Instance.MainHomeScreen.ForgotPasswordScreen.Open();
            UIManager.Instance.MainHomeScreen.LoginScreen.Close();
        }
    
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
