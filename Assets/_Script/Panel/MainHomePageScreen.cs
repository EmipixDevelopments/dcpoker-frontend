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
    public LoginPanel LoginScreen;
    public RegisterPanel registerScreen;
    public forgetPasswordPanel ForgotPasswordScreen;

    void OnEnable()
    {
        SelectedOptionButtonTap(0);
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
        FreeTournamentTableElementData[] elements = new FreeTournamentTableElementData[5];
        elements[0] = new FreeTournamentTableElementData();
        elements[0].Data = "Aug12/12:00";
        elements[0].Name = "Don Huan Alberto";
        elements[0].SeatsCurrent = 5;
        elements[0].SeatsMaximum = 9;
        elements[0].BlindsCurrent = 100;
        elements[0].BlindsMaximum = 200;
        elements[0].BuyIn = 10000;
        elements[0].Type = "NML";

        elements[1] = new FreeTournamentTableElementData();
        elements[1].Data = "Jan12/12:00";
        elements[1].Name = "Hanna Kukina";
        elements[1].SeatsCurrent = 1;
        elements[1].SeatsMaximum = 6;
        elements[1].BlindsCurrent = 10;
        elements[1].BlindsMaximum = 900;
        elements[1].BuyIn = 1000;
        elements[1].Type = "MLRS";

        elements[2] = new FreeTournamentTableElementData();
        elements[2].Data = "Sep5/10:50";
        elements[2].Name = "Seva Kurov";
        elements[2].SeatsCurrent = 8;
        elements[2].SeatsMaximum = 9;
        elements[2].BlindsCurrent = 200;
        elements[2].BlindsMaximum = 200;
        elements[2].BuyIn = 20000;
        elements[2].Type = "LAMBDA";

        elements[3] = new FreeTournamentTableElementData();
        elements[3].Data = "Oct22/09:30";
        elements[3].Name = "Zubr Zubrovich";
        elements[3].SeatsCurrent = 7;
        elements[3].SeatsMaximum = 9;
        elements[3].BlindsCurrent = 100;
        elements[3].BlindsMaximum = 200;
        elements[3].BuyIn = 10;
        elements[3].Type = "KU";

        elements[4] = new FreeTournamentTableElementData();
        elements[4].Data = "Nov01/01:30";
        elements[4].Name = "Zoya Zubrova";
        elements[4].SeatsCurrent = 2;
        elements[4].SeatsMaximum = 6;
        elements[4].BlindsCurrent = 10;
        elements[4].BlindsMaximum = 100;
        elements[4].BuyIn = 10;
        elements[4].Type = "KUTUZOV";

        _tournamentTable?.Init(elements, () => { Debug.Log("Hi"); });

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

        _texasHoldemTable?.Init(elements1, () => { Debug.Log("Hi"); });

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

        _omahaOrPLO5Table?.Init(elements2, () => { Debug.Log("Hi"); });
        /// Test zone end ///
    }





    public void SelectedOptionButtonTap(int SelectedOption)
    {
        ResetSelectedgameButtons(SelectedOption);
    }
    private void ResetSelectedgameButtons(int GameSelect)
    {
        foreach (GameObject Obj in SelectedGame)
        {
            Obj.SetActive(false);
        }
    }
    public void Reset()
    {
        registerScreen.Close();
        ForgotPasswordScreen.Close();
        LoginScreen.Close();
    }



    #region Buttons / Toggls method
    // PanelTopButtons
    public void OnClickHowToPlayButton()
    {
        Debug.Log("OnClickHowToPlay");
    }
    public void OnClickAboutButton()
    {
        Debug.Log("OnClickAboutButton");
    }
    public void OnClickSupportButton()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        Utility.Instance.OpenLink("https://support.macau-gold.com");
    }
    public void SoundToggleCallBack()
    {
        Debug.Log($"toggleState");
    }
    public void OnClickBanner() 
    {
        StartCoroutine(GetBannerUrl("https://httpbin.org/ip"));
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
    public void OnClickTermsOfServiceButton()
    {
        Debug.Log("OnClickTermsOfServiceButton");
    }
    public void OnClickPrivacyPolicyButton()
    {
        Debug.Log("OnClickPrivacyPolicyButton");
    }
    public void OnClickResponsibleGaming()
    {
        Debug.Log("OnClickResponsibleGaming");
    }
    #endregion

    void OnDisable()
    {
        SelectedGames = 0;
    }


















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
