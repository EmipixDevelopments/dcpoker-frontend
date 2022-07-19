using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class MainHomePageScreen : MonoBehaviour
{
    [SerializeField] private ToggleImage _soundToggle;
    [SerializeField] private FreeTournamentTable _tournamentTable;
    [SerializeField] private HomePageTournamentTable _texasHoldemTable;
    [SerializeField] private HomePageTournamentTable _omahaOrPLO5Table;
    [Space]
    public GameObject PanelTournamentUnlogin;
    public LoginPanel LoginScreen;
    public RegisterPanel registerScreen;
    public forgetPasswordPanel ForgotPasswordScreen;
    public GameObject PanelHowToPlay;
    public GameObject PanelAbout;
    public GameObject PanelSupport;
    public GameObject PanelTermsOfService;
    public GameObject PanelPrivacyPolicy;
    public GameObject PanelResponsibleGaming;

    void OnEnable()
    {
        //SelectedOptionButtonTap(0);
        Reset();
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
    void Start() 
    {

        ///--- Test zone ---///
        FreeTournamentTableElementData[] elements = new FreeTournamentTableElementData[1];
        elements[0] = new FreeTournamentTableElementData();
        elements[0].Data = "Aug 12 / 12:00";
        elements[0].Name = "Don Huan Alberto";
        elements[0].Type = "NML";
        elements[0].Players = "1,345";
        elements[0].BuyIn = 10000;
        elements[0].Status = "ACTIVE";

        _tournamentTable?.Init(elements, () => { OnClickloginButton(); });

        //_texasHoldemTable
        HomePageTournamentElementData[] elements1 = new HomePageTournamentElementData[5];
        elements1[0] = new HomePageTournamentElementData();
        elements1[0].Limit = "No Limit";
        elements1[0].SeatsCurrent = 1;
        elements1[0].SeatsMaximum = 9;
        elements1[0].BlindsCurrent = 4;
        elements1[0].BlindsMaximum = 9;
        elements1[0].BuyIn = 10;

        elements1[1] = new HomePageTournamentElementData();
        elements1[1].Limit = "No Limit";
        elements1[1].SeatsCurrent = 4;
        elements1[1].SeatsMaximum = 6;
        elements1[1].BlindsCurrent = 3;
        elements1[1].BlindsMaximum = 9;
        elements1[1].BuyIn = 100;

        elements1[2] = new HomePageTournamentElementData();
        elements1[2].Limit = "No Limit";
        elements1[2].SeatsCurrent = 1;
        elements1[2].SeatsMaximum = 3;
        elements1[2].BlindsCurrent = 1;
        elements1[2].BlindsMaximum = 4;
        elements1[2].BuyIn = 1000;

        elements1[3] = new HomePageTournamentElementData();
        elements1[3].Limit = "No Limit";
        elements1[3].SeatsCurrent = 1;
        elements1[3].SeatsMaximum = 3;
        elements1[3].BlindsCurrent = 1;
        elements1[3].BlindsMaximum = 4;
        elements1[3].BuyIn = 1000;

        elements1[4] = new HomePageTournamentElementData();
        elements1[4].Limit = "No Limit";
        elements1[4].SeatsCurrent = 1;
        elements1[4].SeatsMaximum = 3;
        elements1[4].BlindsCurrent = 1;
        elements1[4].BlindsMaximum = 4;
        elements1[4].BuyIn = 1000;

        _texasHoldemTable?.Init(elements1, () => { OnClickloginButton(); });

        //_omahaOrPLO5Table
        HomePageTournamentElementData[] elements2 = new HomePageTournamentElementData[5];
        elements2[0] = new HomePageTournamentElementData();
        elements2[0].Limit = "No Limit";
        elements2[0].SeatsCurrent = 1;
        elements2[0].SeatsMaximum = 2;
        elements2[0].BlindsCurrent = 4;
        elements2[0].BlindsMaximum = 4;
        elements2[0].BuyIn = 10;
                
        elements2[1] = new HomePageTournamentElementData();
        elements2[1].Limit = "No Limit";
        elements2[1].SeatsCurrent = 4;
        elements2[1].SeatsMaximum = 12;
        elements2[1].BlindsCurrent = 3;
        elements2[1].BlindsMaximum = 5;
        elements2[1].BuyIn = 1000;
                
        elements2[2] = new HomePageTournamentElementData();
        elements2[2].Limit = "No Limit";
        elements2[2].SeatsCurrent = 1;
        elements2[2].SeatsMaximum = 3;
        elements2[2].BlindsCurrent = 12;
        elements2[2].BlindsMaximum = 14;
        elements2[2].BuyIn = 1000;
                
        elements2[3] = new HomePageTournamentElementData();
        elements2[3].Limit = "No Limit";
        elements2[3].SeatsCurrent = 1;
        elements2[3].SeatsMaximum = 3;
        elements2[3].BlindsCurrent = 2;
        elements2[3].BlindsMaximum = 12;
        elements2[3].BuyIn = 10000;
                
        elements2[4] = new HomePageTournamentElementData();
        elements2[4].Limit = "No Limit";
        elements2[4].SeatsCurrent = 3;
        elements2[4].SeatsMaximum = 5;
        elements2[4].BlindsCurrent = 5;
        elements2[4].BlindsMaximum = 15;
        elements2[4].BuyIn = 100;

        _omahaOrPLO5Table?.Init(elements2, () => { OnClickloginButton(); });
        /// Test zone end ///
    }





    //public void SelectedOptionButtonTap(int SelectedOption)
    //{
    //    ResetSelectedgameButtons(SelectedOption);
    //}
    //private void ResetSelectedgameButtons(int GameSelect)
    //{
    //    foreach (GameObject Obj in SelectedGame)
    //    {
    //        Obj.SetActive(false);
    //    }
    //}
    public void Reset()
    {
        CloseAllWindow();
        PanelTournamentUnlogin.SetActive(true);
        LoginScreen.Open();
    }


    private void CloseAllWindow() 
    {
        PanelTournamentUnlogin.SetActive(false);
        LoginScreen.gameObject.SetActive(false);
        registerScreen.gameObject.SetActive(false);
        ForgotPasswordScreen.gameObject.SetActive(false);
        PanelHowToPlay.gameObject.SetActive(false);
        PanelAbout.gameObject.SetActive(false);
        PanelSupport.gameObject.SetActive(false);
        PanelTermsOfService.gameObject.SetActive(false);
        PanelPrivacyPolicy.gameObject.SetActive(false);
        PanelResponsibleGaming.gameObject.SetActive(false);
    }


    #region Buttons / Toggls method
    // PanelTopButtons
    public void OnClickMacauGoldLogo() 
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        CloseAllWindow();
        PanelTournamentUnlogin.SetActive(true);
    }

    public void OnClickHowToPlayButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        CloseAllWindow();
        PanelHowToPlay.SetActive(true);
    }
    public void OnClickAboutButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        CloseAllWindow();
        PanelAbout.SetActive(true);
    }
    public void OnClickSupportButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        CloseAllWindow();
        PanelSupport.SetActive(true);
        //UIManager.Instance.SoundManager.OnButtonClick();
        //Utility.Instance.OpenLink("https://support.macau-gold.com");
    }
    public void SoundToggleCallBack()
    {
        Debug.Log($"toggleState");
    }
    public void OnClickBanner() 
    {
        //StartCoroutine(GetBannerUrl("https://httpbin.org/ip"));
        Utility.Instance.OpenLink("https://google.com");
    }
    IEnumerator GetBannerUrl(string url) 
    {
        UnityWebRequest infoRequest = UnityWebRequest.Get(url);
        yield return infoRequest.SendWebRequest();
        if (infoRequest.isNetworkError || infoRequest.isHttpError)
        {
            Debug.LogError(infoRequest.error);
            yield break;
        }
        
        Debug.Log(infoRequest.downloadHandler.text);
        //Utility.Instance.OpenLink(infoRequest.downloadHandler.text);
    }
    // PanelMenu
    public void OnClickTournamentsButton()
    {
        OnClickloginButton();
    }
    public void OnClickSitNGoButton()
    {
        OnClickloginButton();
    }
    public void OnClickTexasHoldemButton()
    {
        OnClickloginButton();
    }
    public void OnClickOmahaButton()
    {
        OnClickloginButton();
    }
    public void OnClickPLO5Button()
    {
        OnClickloginButton();
    }
    public void OnClickCreateAccountButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        registerScreen.Open();
    }
    public void OnClickloginButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        Reset();
        LoginScreen.Open();
    }
    // Free Tournament panel
    public void OnClickTournamentToggleAll() 
    {
        Debug.Log("OnClickTournamentToggleAll");
    }
    public void OnClickTournamentToggleLow()
    {
        Debug.Log("OnClickTournamentToggleLow");
    }
    public void OnClickTournamentToggleMedium()
    {
        Debug.Log("OnClickTournamentToggleMedium");
    }
    public void OnClickTournamentToggleHigh()
    {
        Debug.Log("OnClickTournamentToggleHigh");
    }
    public void OnClickTournamentToggleFreeroll()
    {
        Debug.Log("OnClickTournamentToggleFreeroll");
    }
    // the bottom panel
    public void OnClickTermsOfServiceButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        CloseAllWindow();
        PanelTermsOfService.SetActive(true);
    }
    public void OnClickPrivacyPolicyButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        CloseAllWindow();
        PanelPrivacyPolicy.SetActive(true);
    }
    public void OnClickResponsibleGaming()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        CloseAllWindow();
        PanelResponsibleGaming.SetActive(true);
    }
    #endregion

    void OnDisable()
    {
        SelectedGames = 0;
    }

















    // need remove old logic


    [Header("Old Logic")]
    [Space(100)]
    public bool TrashIsHere;


    #region PUBLIC_VARIABLES

    [Header("Gamobjects")]
    public GameObject[] SelectedGame;
    //[Header ("Transforms")]


    [Header("ScriptableObjects")]

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

    public void suppportButtonTap() // used
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        Utility.Instance.OpenLink("https://support.macau-gold.com");
    }
    public void loginbuttonTap() // used
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        LoginScreen.Open();
    }
    public void RegisterbuttonTap() // used
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        registerScreen.Open();
    }


    #endregion

    #region PRIVATE_METHODS


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
