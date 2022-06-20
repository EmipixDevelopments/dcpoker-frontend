using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class MainHomePageScreen : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    [Header("Gamobjects")]
    public GameObject[] SelectedGame;
    //[Header ("Transforms")]


    [Header("ScriptableObjects")]
    public LoginPanel LoginScreen;
    public RegisterPanel registerScreen;
    public forgetPasswordPanel ForgotPasswordScreen;
    //public ResendEmailPasswordPanel ResendEmailPasswordScreen;

    //[Header ("DropDowns")]


    //[Header ("Images")]


    [Header("Text")]
    //public Text txtTimer;
    //public Text txtjackpotAmount;
    //public Text txtonlinePlayers;
    //[Header ("Prefabs")]
    //[Header ("Enums")]


    [Header("Variables")]
    public int SelectedGames;
    public int loginPageFocusCounter = 0;
    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    void OnEnable()
    {
        SelectedOptionButtonTap(0);
        Reset();
        //PlayerPrefs.SetInt("gameRules", 0);
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
        if (!PlayerPrefs.HasKey("gameRules"))
        {
            PlayerPrefs.SetInt("gameRules", 0);
        }
        if (PlayerPrefs.GetInt("gameRules").Equals(0))
        {
            UIManager.Instance.TandCondPopup.SetDataOpen();
            PlayerPrefs.SetInt("gameRules", 1);
        }
#endif



    }
    void OnDisable()
    {
        SelectedGames = 0;
    }
    // Update is called once per frame
    /*void Update()
    {
        string s = System.DateTime.Now.Day.ToString() + " " + System.DateTime.Now.ToString("MMMM") + " " + System.DateTime.Now.ToShortTimeString();
        txtTimer.text = s;
    }*/
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public bool ShowLoader()
    {
        if (loginPageFocusCounter > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void SelectedOptionButtonTap(int SelectedOption)
    {
        ResetSelectedgameButtons(SelectedOption);
    }
    public void suppportButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        Utility.Instance.OpenLink("https://support.macau-gold.com");
    }
    public void loginbuttonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        LoginScreen.Open();
    }
    public void RegisterbuttonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        registerScreen.Open();
    }
    public void Reset()
    {
        registerScreen.Close();
        ForgotPasswordScreen.Close();
        //ResendEmailPasswordScreen.Close();
        LoginScreen.Close();
    }

    #endregion

    #region PRIVATE_METHODS
    private void ResetSelectedgameButtons(int GameSelect)
    {
        foreach (GameObject Obj in SelectedGame)
        {
            Obj.SetActive(false);
        }

        //loginbuttonTap();

        //SelectedGames = GameSelect;
        //SelectedGame[GameSelect].SetActive(true);
    }

    public void SetObject(int GameSelect)
    {
        foreach (GameObject Obj in SelectedGame)
        {
            Obj.SetActive(false);
        }


        SelectedGames = GameSelect;
        SelectedGame[GameSelect].SetActive(true);
    }

    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER
    public string AESEncryption(string inputData)
    {
        AesCryptoServiceProvider AEScryptoProvider = new AesCryptoServiceProvider();
        AEScryptoProvider.BlockSize = 128;
        AEScryptoProvider.KeySize = 256;
        AEScryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes("6oXaHd88sjQhiL8cjOu7AUwsgi4IaZU2");
        AEScryptoProvider.IV = ASCIIEncoding.ASCII.GetBytes("37bOysXvYlM32WmO");
        AEScryptoProvider.Mode = CipherMode.CBC;
        AEScryptoProvider.Padding = PaddingMode.PKCS7;

        byte[] txtByteData = ASCIIEncoding.ASCII.GetBytes(inputData);
        ICryptoTransform trnsfrm = AEScryptoProvider.CreateEncryptor(AEScryptoProvider.Key, AEScryptoProvider.IV);

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return Convert.ToBase64String(result);
    }

    #endregion



}
