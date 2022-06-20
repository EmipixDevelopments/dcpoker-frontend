using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using BestHTTP;

public class LoginPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    [Header("InputField")]
    public TMP_InputField LoginUsername;
    public TMP_InputField LoginPassword;
    public Text txtMessage;
    public Text txtdemo;
    [Header("Toggle")]
    public Toggle RememberMe;

    public Image imgEng, imgMongolian;

    string individualLine = ""; //Control individual line in the multi-line text component.

    int numberOfAlphabetsInSingleLine = 20;
    #endregion

    #region PRIVATE_VARIABLES
    #endregion

    #region UNITY_CALLBACKS
    /*void Awake()
	{
		UIManager.Instance.ipLocationService.onGPSCollect += LoginButton;
	}*/
    // Updates button's text while user is typing

    void OnEnable()
    {
        /*
#if UNITY_WEBGL || UNITY_EDITOR || UNITY_STANDALONE
        gameObject.transform.localScale = new Vector2(.8f, .8f);
#elif UNITY_IOS
		if(SystemInfo.deviceModel.Contains("iPad")) {
		gameObject.transform.localScale = new Vector2(.8f,.8f);
		}
#endif
        */
        ResetData();

    }
    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    void Start()
    {
        //this.Close();
        //Invoke("SSS", 0.5f);
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

    #region DELEGATE_CALLBACKS

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
        txtMessage.text = "";

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
