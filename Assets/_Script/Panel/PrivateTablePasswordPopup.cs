using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PrivateTablePasswordPopup : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    [Header("Text")]
    public TextMeshProUGUI txtTitle;

    [Header("Inputfield")]
    public TMP_InputField inptPassword;

    [Header("Buttons")]
    public Button btnUnlock;
    public Button btnCancel;
    #endregion

    #region PRIVATE_VARIABLES
    private RoomsListing.Room data;
    private TableData tableData;
    #endregion

    #region UNITY_CALLBACKS
    void Awake()
    {
        btnUnlock.onClick.AddListener(OnUnlockButtonTap);
        btnCancel.onClick.AddListener(OnCloseButtonTap);
    }

    void OnEnable()
    {
        inptPassword.text = "";
    }
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS
    public void OpenTablePasswordPopup(TableData tableData)
    {
        this.tableData = tableData;
        //txtTitle.text = LocalizationManager.GetTranslation("Dynamic/Enter password to access") + " \"" + tableData.data.roomName + "\" " + LocalizationManager.GetTranslation("Dynamic/table");
        txtTitle.text = "Enter password to access \"" + tableData.data.roomName + "\" table";
        this.Open();
    }

    public void OnUnlockButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (inptPassword.text.Length == 0)
        {
            UIManager.Instance.DisplayMessagePanel("Incorrect password value", null);
            return;
        }

        UIManager.Instance.DisplayLoader("");
        UIManager.Instance.SocketGameManager.PrivateRoomLogin(tableData.data.roomId, inptPassword.text, (socket, packet, args) =>
        {
            UIManager.Instance.HideLoader();
            print("PrivateRoomLogin response: " + packet.ToString());

            PokerEventResponse response = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet));
            if (response.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                if (tableData.isActiveAndEnabled)
                {
                    tableData.OnViewGameTap();
                }
                else
                {
                    UIManager.Instance.DisplayMessagePanel("Table not found", null);
                }
                this.Close();
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(response.message, null);
            }
        });
    }

    public void OnCloseButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }
    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES

    #endregion
}
