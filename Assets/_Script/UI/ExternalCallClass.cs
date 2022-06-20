using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
To call function from unity to web,
Application.ExternalCall("functionName");

To receive string data from web,
public void FunctionName(string jsonData)
{

}
*/

public class ExternalCallClass : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    public static ExternalCallClass Instance;
    #endregion

    #region PRIVATE_VARIABLES]
    public bool isRequestGameEventCalled = false;
    #endregion

    #region UNITY_CALLBACKS
    void Awake()
    {
        Instance = this;
    }
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS_TO_CALL_WEB_SIDE
    public void RequestGameData()
    {
        //Debug.unityLogger.logEnabled = true;
        print("UNITY RequestGameData call");
        if (isRequestGameEventCalled == false)
        {
            Screen.fullScreen = true;
            Application.ExternalCall("requestGameData");
            isRequestGameEventCalled = true;

#if UNITY_EDITOR
            ReceiveGameData("");
#endif
        }
        //Debug.unityLogger.logEnabled = UIManager.Instance.isLogAllEnabled;
    }

    public void ExitGame()
    {
        //Debug.unityLogger.logEnabled = true;
        print("UNITY ExitGame call");
        Application.ExternalCall("requestExitGame", UIManager.Instance.assetOfGame.SavedLoginData.userUuid);
        //Debug.unityLogger.logEnabled = UIManager.Instance.isLogAllEnabled;
    }
    public void OpenDepositBrowsePanel()
    {
        print("UNITY Deposit Browse Panel call");
        Application.ExternalCall("requestDepositBrowsePanel");
    }

    public void OpenUrl(string url)
    {
        print("UNITY OpenUrl call");
        Application.ExternalCall("requestUrlOpen", url);
    }

    #endregion

    #region PUBLIC_METHODS_UNITY
    public void ReceiveGameData(string data)
    {
        UIManager.Instance.webGLAffiliatePanel.StoreLoginData(data);
    }
    public void ReceiveDepositBase64Data(string data)
    {
        UIManager.Instance.LobbyScreeen.ProfileScreen.PanelMyAccount.depositPanel.DepositBase64Img(data);
    }


    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES
    #endregion

    #region GETTER_SETTER 
    #endregion
}