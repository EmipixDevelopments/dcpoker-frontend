using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Constants;

public class MyAccountPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    [Header("Gamobjects")]
    public GameObject[] SelectedGame;
    public GameObject[] SelectedGameTableList;
    public GameObject[] OptionButtons;
    public GameObject transferChipsObject;

    public GameObject buttonPanelCashPlayer;

    [Header("ScriptableObjects")]
    public ProfileScreenPanel ProfilePanel;
    public PurchaseHistoryPanel PanelPurchaseHistory;
    public StatisticsPanel PanelStatistics;
    public DepositPanel depositPanel;

    #endregion

    #region PRIVATE_VARIABLES
    private int SelectedOption = 0;
    #endregion

    #region UNITY_CALLBACKS
    private void Awake()
    {
#if UNITY_ANDROID || UNITY_IOS
	/*	OptionButtons[7].GetComponent<CanvasGroup>().alpha = 0;
		OptionButtons[7].GetComponent<CanvasGroup>().interactable = false;*/
#else
        OptionButtons[4].GetComponent<CanvasGroup>().alpha = 0;
        OptionButtons[4].GetComponent<CanvasGroup>().interactable = false;

        /*	OptionButtons[5].GetComponent<CanvasGroup>().alpha = 0;
            OptionButtons[5].GetComponent<CanvasGroup>().interactable = false;

            OptionButtons[6].GetComponent<CanvasGroup>().alpha = 0;
            OptionButtons[6].GetComponent<CanvasGroup>().interactable = false;

            OptionButtons[7].GetComponent<CanvasGroup>().alpha = 1;
            OptionButtons[7].GetComponent<CanvasGroup>().interactable = true;
            OptionButtons[7].transform.SetAsFirstSibling();*/
#endif
    }

    void OnEnable()
    {
        if (UIManager.Instance.assetOfGame.SavedLoginData.isCash == true)
        {
            buttonPanelCashPlayer.SetActive(true);
        }
        else
        {
            buttonPanelCashPlayer.SetActive(false);
        }

        SelectedOptionButtonTap(SelectedOption);
        transferChipsObject.SetActive(false);
    }
    void OnDisable()
    {
        foreach (GameObject Obj in SelectedGameTableList)
        {
            Obj.SetActive(false);
        }
        SelectedOption = 0;
    }
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void SelectedOptionButtonTap(int SelectedOption)
    {
        this.SelectedOption = SelectedOption;
        ResetSelectedgameButtons(SelectedOption);
    }

    public void CallEssentialsEvent()
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

                    Utility.Instance.OpenLink(Constants.PokerAPI.BaseUrl + "/Essentials/index.html?token=" + tokenEventResponse.result);
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

    #region PRIVATE_METHODS
    void ResetSelectedgameButtons(int GameSelect)
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
    #endregion

    #region COROUTINES



    #endregion

    #region GETTER_SETTER


    #endregion

}
