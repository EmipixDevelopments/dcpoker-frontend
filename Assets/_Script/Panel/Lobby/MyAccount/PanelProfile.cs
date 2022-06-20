using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelProfile : MonoBehaviour
{

    #region PUBLIC_VARIABLES 
    //[Header ("Gamobjects")]

    //[Header ("Transforms")]
    public GameObject[] SelectedGame;
    public GameObject[] SelectedGameTableList;

    [Header("ScriptableObjects")]
    public MyAccountPanel PanelMyAccount;
    public NewsPanel PanelNews;
    public SettingsPanel PanelSettings;
    public MyTournaryPanel PanelMyTournary;

    //[Header ("DropDowns")]


    //[Header ("Button")]
    public Button btnEssentials;

    [Header("Text")]
    public TextMeshProUGUI txtGameName;

    //[Header ("Prefabs")]

    //[Header ("Enums")]


    //[Header ("Variables")]

    #endregion 
    #region PRIVATE_VARIABLES 
    #endregion 
    #region UNITY_CALLBACKS     // Use this for initialization
    void OnEnable()
    {

        if (UIManager.Instance.assetOfGame.SavedLoginData.isCash || Application.platform == RuntimePlatform.WebGLPlayer || !UIManager.Instance.assetOfGame.SavedLoginData.isInAppPurchaseAllowed)
        {
            btnEssentials.Open();
        }
        else
        {
            btnEssentials.Close();
        }


#if UNITY_WEBGL
        btnEssentials.Close();
#endif
        txtGameName.text = "";
        ResetSelectedgameButtons(0);
        txtGameName.text = "My Account";
    }
    void OnDisable()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    #endregion 
    #region DELEGATE_CALLBACKS 

    #endregion 
    #region PUBLIC_METHODS     public void closeButtonTap()
    {         ResetAllPanels();         UIManager.Instance.LobbyScreeen.HomeButtonTap();         UIManager.Instance.SoundManager.OnButtonClick();         this.Close();     }     public void MyaccountButtonTap()
    {
        ResetAllPanels();
        ResetSelectedgameButtons(0);
        UIManager.Instance.SoundManager.OnButtonClick();
        txtGameName.text = "My Account"; //"My Account";
    }
    public void tourneysButtonTap()
    {
        ResetAllPanels();
        ResetSelectedgameButtons(1);
        UIManager.Instance.SoundManager.OnButtonClick();
        txtGameName.text = "My Tourneys"; //"My Tourneys";
    }
    public void settingsButtonTap()
    {
        ResetAllPanels();
        ResetSelectedgameButtons(2);
        UIManager.Instance.SoundManager.OnButtonClick();
        txtGameName.text = "My Settings"; //"My Settings";
    }
    public void newsButtonTap()
    {
        ResetAllPanels();
        ResetSelectedgameButtons(3);
        UIManager.Instance.SoundManager.OnButtonClick();
        txtGameName.text = "News"; //"News";
    }
    public void EssentialsButtonTap()
    {
        UIManager.Instance.DisplayLoader();
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.GetTokenForEssentials((socket, packet, args) =>
        {
            Debug.Log("GetTokenForEssentials response: " + packet.ToString());
            UIManager.Instance.HideLoader();
            PokerEventResponse tokenEventResponse = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet));
            {
                if (tokenEventResponse.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {

                    Utility.Instance.OpenLink(Utility.Instance.CurrentDomain + "/Essentials/index.html?token=" + tokenEventResponse.result);
                    /*
                    if (UIManager.Instance.server == SERVER.Live)

                        Utility.Instance.OpenLink(Constants.PokerAPI.EssentialLiveUrl + "/Essentials/index.html?token=" + tokenEventResponse.result);
                    else
                        Utility.Instance.OpenLink(Utility.Instance.CurrentDomain + "/Essentials/index.html?token=" + tokenEventResponse.result);*/
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel(tokenEventResponse.message);
                }
            }
        });

    }

    #endregion 
    #region PRIVATE_METHODS       void ResetSelectedgameButtons(int GameSelect)
    {
        foreach (GameObject Obj in SelectedGame)
        {
            Obj.SetActive(false);
        }
        foreach (GameObject Obj in SelectedGameTableList)
        {
            Obj.SetActive(false);
        }
        SelectedGame[GameSelect].SetActive(true);
        SelectedGameTableList[GameSelect].SetActive(true);
    }
    void ResetAllPanels()
    {
        PanelMyAccount.Close();
        PanelMyTournary.Close();
        PanelSettings.Close();
        PanelNews.Close();
    }
    #endregion 
    #region COROUTINES 

    #endregion 

    #region GETTER_SETTER 

    #endregion 


}
