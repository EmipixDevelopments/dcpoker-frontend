using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BestHTTP;
using TMPro;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class RegisterPanelNew : MonoBehaviour
{
    #region PUBLIC_VARIABLE

    public GameObject panelUsername;
    public GameObject panelPassword;
    public GameObject panelRecovery;
    public GameObject panelRecoveryVerify;
    public GameObject panelRecoveryVerifySuccess;
    public GameObject panelRecoveryVerifyFail;

    public TMP_Text textMessage;

    [Header("Username")] public TMP_InputField inputFieldUsername;
    [Header("Password")] public TMP_InputField inputFieldPassword;
    public TMP_InputField inputFieldRePassword;

    [Header("Phrase Recovery")] public TMP_Text textRecoveryPhraseFromServer;

    [Header("Phrase Recovery Verify")] public List<TMP_Text> listPhraseVerificationText;
    public TMP_Text textPhraseVerificationText;

    [Header("Phrase Recovery Verify Success")]
    public TMP_Text textTimer;

    [SerializeField] private Toggle _showPasswordToggle;
    [SerializeField] private Toggle _showRepeatPasswordToggle;

    #endregion

    #region PRIVATE_VARIABLE

    private int _phraseVerifySuccessTimer = 0;
    private string _username = "";
    private string _password = "";
    public string _phrase = "";
    public string _playerEnteredPhrase = "";
    private List<string> _listPhrasePartition = new List<string>();

    #endregion


    public void OnClickShowPasswordToggle()
    {
        if (_showPasswordToggle.isOn)
        {
            inputFieldPassword.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            inputFieldPassword.contentType = TMP_InputField.ContentType.Password;
        }

        inputFieldPassword.ForceLabelUpdate();
    }

    public void OnClickShowRePasswordToggle()
    {
        if (_showRepeatPasswordToggle.isOn)
        {
            inputFieldRePassword.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            inputFieldRePassword.contentType = TMP_InputField.ContentType.Password;
        }

        inputFieldRePassword.ForceLabelUpdate();
    }

    private void OnEnable()
    {
        panelUsername.SetActive(true);
        panelPassword.SetActive(false);
        panelRecovery.SetActive(false);
        panelRecoveryVerify.SetActive(false);
        panelRecoveryVerifySuccess.SetActive(false);
        panelRecoveryVerifyFail.SetActive(false);
    }

    private bool IsLoginDetailValid()
    {
        StopCoroutine(textempti());

        string username = _username;
        string password = inputFieldPassword.text;
        string repeatPassword = inputFieldRePassword.text;

        if (string.IsNullOrEmpty(username))
        {
            textMessage.text = Constants.Messages.Register.UsernameEmpty;
            StartCoroutine(textempti());
            return false;
        }

        else if (!IsUsernameValid())
        {
            textMessage.text = Constants.Messages.Register.UsernameInvalid;
            StartCoroutine(textempti());
            return false;
        }
        else if (string.IsNullOrEmpty(password))
        {
            textMessage.text = Constants.Messages.Register.PasswordEmpty;
            StartCoroutine(textempti());
            return false;
        }
        else if (password.Length < Constants.Messages.Login.PasswordLength)
        {
            textMessage.text = Constants.Messages.Register.MinPasswordLength;
            StartCoroutine(textempti());
            return false;
        }
        else if (string.IsNullOrEmpty(repeatPassword))
        {
            textMessage.text = Constants.Messages.Register.ConfirmPasswordEmpty;
            StartCoroutine(textempti());
            return false;
        }
        else if (!password.Equals(repeatPassword))
        {
            textMessage.text = Constants.Messages.Register.PasswordNotMatched;
            StartCoroutine(textempti());
            return false;
        }

        return true;
    }

    private bool IsUsernameValid()
    {
        string number = inputFieldUsername.text;

        if (number.Length < 4)
        {
            return false;
        }

        return true;
    }

    private void RegisterUser()
    {
        string username = _username;
        string password = _password;

        if (UIManager.Instance.SocketGameManager.HasInternetConnection())
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            if (IsLoginDetailValid())
            {
                UIManager.Instance.DisplayLoader("");

                UIManager.Instance.SocketGameManager.RegisterPlayer(username, password, "", "", "", (socket, packet, args) =>
                {
                    Debug.Log("RegisterPlayer  => " + packet.ToString());

                    UIManager.Instance.HideLoader();
                    JSONArray arr = new JSONArray(packet.ToString());
                    string Source;
                    Source = arr.getString(arr.length() - 1);
                    var resp = Source;
                    PokerEventResponse registrationResp = JsonUtility.FromJson<PokerEventResponse>(resp);

//                    RecoveryPhraseEventResponse recoveryPhraseEventResponse = JsonUtility.FromJson<RecoveryPhraseEventResponse>(registrationResp.result);

//                    Debug.LogError("Register Player Response : " + registrationResp + " | " + recoveryPhraseEventResponse);

                    if (registrationResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                    {
                        _phrase = registrationResp.result.recoveryPhrase;
                        textRecoveryPhraseFromServer.text = _phrase;
                        panelPassword.SetActive(false);
                        panelRecovery.SetActive(true);
                    }
                    else if (registrationResp.message == "Username already taken.")
                    {
                        panelUsername.SetActive(true);
                        panelPassword.SetActive(false);
                    }
                    else
                    {
                        UIManager.Instance.DisplayMessagePanel(registrationResp.message, null);
                    }
                });
            }
        }
    }

    private void VerifyPhrase()
    {
        if (UIManager.Instance.SocketGameManager.HasInternetConnection())
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            if (IsLoginDetailValid())
            {
                string AuthTokenverify = tokenHack(_username, _password);

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

                    UIManager.Instance.SocketGameManager.PlayerRecoveryPhraseVerification(_username, _password, _playerEnteredPhrase, AuthTokenverify, ipAddress, true, (socket, packet, args) =>
                    {
                        Debug.Log("Verify Phrase  => " + packet.ToString());
                        UIManager.Instance.HideLoader();
                        JSONArray arr = new JSONArray(packet.ToString());
                        string Source;
                        Source = arr.getString(arr.length() - 1);
                        var resp = Source;
                        PokerEventResponse registrationResp = JsonUtility.FromJson<PokerEventResponse>(resp);
                        if (registrationResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                        {
                            // set timer here....
                            UIManager.Instance.assetOfGame.SavedLoginData.publicKey = registrationResp.result.publicKey;
                            UIManager.Instance.assetOfGame.SavedLoginData.privateKey = registrationResp.result.privateKey;

                            _phraseVerifySuccessTimer = 10;
                            textTimer.text = "" + _phraseVerifySuccessTimer;
                            panelRecoveryVerifySuccess.SetActive(true);
                            panelRecoveryVerify.SetActive(false);
                            StartCoroutine(DoAnimateTimer(_phraseVerifySuccessTimer));
                        }
                        else if (registrationResp.message == "Username already taken.")
                        {
                            panelUsername.SetActive(true);
                            panelPassword.SetActive(false);
                        }
                        else
                        {
                            UIManager.Instance.DisplayMessagePanel(registrationResp.message, () =>
                            {
                                textPhraseVerificationText.text = "This is Recovery phrase which is unique for every user at Time";
                                _playerEnteredPhrase = null;
                                panelRecoveryVerifyFail.SetActive(true);
                                panelRecoveryVerify.SetActive(false);
                                UIManager.Instance.messagePanel.gameObject.SetActive(false);
                            });
                        }
                    });
                });
                httpRequest.Send();
            }
        }
    }

    private string tokenHack(string mobile, string password)
    {
        string syndicate = null;
        rootHackBase root = new rootHackBase();
        root.mobile = mobile;
        root.password = password;
        var dateTime = DateTime.UtcNow; //.AddSeconds(-9); // fix: not work
        Debug.LogError("Data: " + dateTime.ToString());
        root.timestamp = DateTimeToUnix(dateTime);
        Debug.LogError("Timestamp: " + root.timestamp);
        string json = JsonUtility.ToJson(root);
        Debug.LogError("Not Encrypt: " + json);
        string encryptedJson = UIManager.Instance.MainHomeScreen.AESEncryption(json);
        Debug.LogError("Encrypt: " + encryptedJson);

        syndicate = encryptedJson;
        //syndicate = System.Uri.EscapeDataString(syndicate);
        //print(syndicate);
        return syndicate;
    }

    private long DateTimeToUnix(DateTime MyDateTime)
    {
        TimeSpan timeSpan = MyDateTime - new DateTime(1970, 1, 1, 0, 0, 0);
        return (long) timeSpan.TotalSeconds;
    }

    [ContextMenu("SeperatePhraseToken")]
    private void SeperatePhraseToken()
    {
        string[] temp = _phrase.Split(" ");
        for (int i = 0; i < temp.Length; i++)
        {
            _listPhrasePartition.Add(temp[i]);
        }
    }

    [ContextMenu("ShufflePhraseToken")]
    private void ShufflePhraseToken()
    {
        _listPhrasePartition = _listPhrasePartition.OrderBy(x => Random.value).ToList();
        for (int i = 0; i < listPhraseVerificationText.Count; i++)
        {
            listPhraseVerificationText[i].text = _listPhrasePartition[i];
        }
    }

    #region COROUTINE

    IEnumerator textempti()
    {
        yield return new WaitForSeconds(2f);
        textMessage.text = "";
    }

    private IEnumerator DoAnimateTimer(int timer)
    {
        while (timer > 0)
        {
            textTimer.text = timer + "";
            yield return new WaitForSeconds(1);
            timer--;
        }

        UIManager.Instance.MainHomeScreen.LoginScreen.ForceLogin(_username, _password);
//        UIManager.Instance.MainHomeScreen.LoginScreen.OnClickLoginButton(true);
        panelRecoveryVerifySuccess.SetActive(false);
    }

    #endregion

    #region UI_CALLBACKS

    public void OnClickCloseButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }

    public void On_Button_Click_Username_Next()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (IsUsernameValid())
        {
            _username = inputFieldUsername.text;
            panelUsername.SetActive(false);
            panelPassword.SetActive(true);
        }
    }

    public void On_Button_Click_Password_Next()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (inputFieldPassword.text.Equals(inputFieldRePassword.text))
        {
            _password = inputFieldPassword.text;
            RegisterUser();
        }
    }

    public void On_Button_Click_Recovery_Next()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        SeperatePhraseToken();
        ShufflePhraseToken();
        panelRecovery.SetActive(false);
        panelRecoveryVerify.SetActive(true);
    }

    public void On_Button_Click_Recovery_Copy()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        Debug.Log("copy!!!");
        GUIUtility.systemCopyBuffer = textRecoveryPhraseFromServer.text;
        Debug.Log(GUIUtility.systemCopyBuffer);
        textMessage.text = "phrase copied successfully";
        StartCoroutine(textempti());
    }

    public void On_Button_Click_RecoveryVerify_Token(int index)
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (string.IsNullOrEmpty(_playerEnteredPhrase))
            _playerEnteredPhrase = _listPhrasePartition[index];
        else
            _playerEnteredPhrase += " " + _listPhrasePartition[index];

        textPhraseVerificationText.text = _playerEnteredPhrase;
    }

    public void On_Button_Click_RecoveryVerify_Next()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        VerifyPhrase();
    }

    public void On_Button_Click_RecoveryVerify_Success()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.MainHomeScreen.LoginScreen.ForceLogin(_username, _password);
    }

    public void On_Button_Click_RecoveryVerify_Fail()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        panelRecoveryVerify.SetActive(true);
        panelRecoveryVerifyFail.SetActive(false);
    }

    public void On_Button_Click_Login()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
        UIManager.Instance.MainHomeScreen.LoginScreen.Open();
    }

    #endregion
}