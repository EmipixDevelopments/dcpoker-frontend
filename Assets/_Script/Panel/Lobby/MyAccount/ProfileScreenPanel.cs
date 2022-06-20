using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProfileScreenPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    [Header("Images")]
    public Image PlayerProfile;
    public SelectedAvatarPnel ProfileAvatar;
    public ChangePasswordPanel Panelchangepassword;
    public ChangeUsernamePanel panelChangeUsername;
    public TransferChipsPanel transferChipsPanel;
    [Header("InputField")]
    public Text txtUsername;
    public TextMeshProUGUI txtEmail;
    public TextMeshProUGUI txtCoins;

    [Header("Button")]
    public Button btnAddChipsIAP;
    public Button btnTransferChips;
    public Button ChangePasswordObject;

    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    void OnApplicationFocus(bool hasFocus)
    {
        Debug.Log("OnApplicationFocus: " + hasFocus);
        if (hasFocus)
        {
            UIManager.Instance.backgroundEventManager.GetProfileEventCall();
        }
    }
    void OnEnable()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        ProfileAvatar.Close();
        Panelchangepassword.Close();
        panelChangeUsername.Close();
        transferChipsPanel.Close();

        CallProfileEvent();

        if (UIManager.Instance.assetOfGame.SavedLoginData.isCash || Application.platform == RuntimePlatform.WebGLPlayer || !UIManager.Instance.assetOfGame.SavedLoginData.isInAppPurchaseAllowed)
        {
            btnAddChipsIAP.Close();
        }
        else
        {
            btnAddChipsIAP.Open();
        }

        //    if(UIManager.Instance.isChipsTransferAllowed)
        //         btnTransferChips.Open();
        //     else
        btnTransferChips.Close();

#if UNITY_WEBGL
        btnAddChipsIAP.Close();
#endif

#if UNITY_WEBGL
        /*if (UIManager.Instance.assetOfGame.SavedLoginData.isCash)
        {
            ChangePasswordObject.Close();
        }
        else
        {*/
        ChangePasswordObject.Open();
        //}
#else
        ChangePasswordObject.Open();
#endif
    }

    private void OnDisable()
    {
        transferChipsPanel.Close();
    }
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS

    public void ChangeAvatarBtnTap(bool IsOPen)
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (IsOPen)
        {
            ProfileAvatar.Open();
        }
        else
        {
            ProfileAvatar.Close();
        }

    }

    public void ChangePasswordBtnTap(bool IsOPen)
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (IsOPen)
        {
            Panelchangepassword.Open();
        }
        else
        {
            Panelchangepassword.Close();
        }
    }

    public void ChangeUsernameBtnTap(bool IsOPen)
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (IsOPen)
        {
            panelChangeUsername.Open();
        }
        else
        {
            panelChangeUsername.Close();
        }
    }

    public void TransferChips(bool IsOpen)
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (IsOpen)
        {
            transferChipsPanel.Open();
        }
        else
        {
            transferChipsPanel.Close();
        }
    }
    #endregion

    #region PRIVATE_METHODS
    public void CallProfileEvent()
    {
        UIManager.Instance.SocketGameManager.GetProfile((socket, packet, args) =>
        {

            Debug.Log("GetProfile  : " + packet.ToString());

            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse<Profile> resp = JsonUtility.FromJson<PokerEventResponse<Profile>>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                PlayerProfile.sprite = UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[resp.result.avatar];
                txtUsername.text = resp.result.username;
                txtEmail.text = resp.result.mobile.ToString();
                UIManager.Instance.assetOfGame.SavedLoginData.chips = resp.result.chips;
                txtCoins.text = "Balance : " + UIManager.Instance.assetOfGame.SavedLoginData.chips.ConvertToCommaSeparatedValueColor();

            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }
    #endregion

    #region COROUTINES

    #endregion

    #region GETTER_SETTER
    public double Chips
    {
        set
        {
            txtCoins.text = "Balance : " + value.ConvertToCommaSeparatedValueColor();
        }
    }

    public string Username
    {
        set
        {
            txtUsername.text = value;
        }
    }
    #endregion

}
