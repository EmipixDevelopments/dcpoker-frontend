using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreBetButtonsPanel : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    [Header("Client side handling")]
    public Toggle toggleFoldToAnyBet;
    public Toggle toggleSitOutNextHand;
    public Toggle toggleSitOutNextBigBlind;
    public Toggle toggleCheck;
    public Toggle toggleCallAny;
    public Toggle toggleAllowMuck;
    #endregion

    #region PRIVATE_VARIABLES
    #endregion

    #region UNITY_CALLBACKS
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS
    public void setOnData(bool isRaise)
    {
        if (isRaise)
        {
            toggleCallAny.Close();
        }
        else
        {
            toggleCallAny.Open();
        }
        this.Open();
    }
    public void ResetToggles(bool isopen)
    {
        if (!isopen)
        {
            toggleAllowMuck.isOn = false;
        }
        toggleCheck.isOn = false;
        toggleCallAny.isOn = false;
        toggleFoldToAnyBet.isOn = false;
    }

    public void ResetAllToggles()
    {
        toggleCheck.isOn = false;
        toggleCallAny.isOn = false;
        toggleFoldToAnyBet.isOn = false;
        toggleSitOutNextHand.isOn = false;
        toggleSitOutNextBigBlind.isOn = false;
    }

    public void OnSitOutNextHandButtonTap()
    {
        UIManager.Instance.SocketGameManager.SitOutNextHand(UIManager.Instance.GameScreeen.currentRoomData.roomId, toggleSitOutNextHand.isOn, (socket, packet, args) =>
        {
            print("OnSitOutNextHandButtonTap(): " + packet.ToString());
        });
    }

    public void OnSitOutNextBigBlind()
    {
        UIManager.Instance.SocketGameManager.SitOutNextBigBlind(UIManager.Instance.GameScreeen.currentRoomData.roomId, toggleSitOutNextBigBlind.isOn, (socket, packet, args) =>
        {
            print("OnSitOutNextBigBlind(): " + packet.ToString());
        });
    }
    public void OnMuckToggleTap()
    {
        UIManager.Instance.SocketGameManager.onAllowMuck(UIManager.Instance.GameScreeen.currentRoomData.roomId, toggleAllowMuck.isOn, (socket, packet, args) =>
        {
            print("OnMuckToggleTap(): " + packet.ToString());
        });
    }
    public void OnPresetFoldButtonTap()
    {
        string key = "";
        if (toggleFoldToAnyBet.isOn)
            key = "isFold";
        else
            key = "removeIsFold";

        DefaultActionEventCall(key);
    }

    public void OnPresetCallButtonTap()
    {
        string key = "";
        if (toggleCallAny.isOn)
            key = "isCall";
        else
            key = "removeIsCall";

        DefaultActionEventCall(key);
    }


    public void OnPresetCheckButtonTap()
    {
        string key = "";
        if (toggleCheck.isOn)
            key = "isCheck";
        else
            key = "removeIsCheck";

        DefaultActionEventCall(key);
    }

    public void SetPrebetOptions(DefaultButtons defaultButtons)
    {
        toggleFoldToAnyBet.isOn = defaultButtons.isFold;
        toggleCheck.isOn = defaultButtons.isCheck;
        toggleCallAny.isOn = defaultButtons.isCall;

        toggleSitOutNextHand.isOn = defaultButtons.sitOutNextHand;
        toggleSitOutNextBigBlind.isOn = defaultButtons.sitOutNextBigBlind;
    }
    #endregion

    #region PRIVATE_METHODS
    private void DefaultActionEventCall(string key)
    {
        UIManager.Instance.SocketGameManager.DefaultActionSelection(UIManager.Instance.GameScreeen.currentRoomData.roomId, key, (socket, packet, args) =>
        {
            print("DefaultActionSelection response: " + packet.ToString());
            PokerEventResponse response = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet));
            if (response.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                print("OnCheckButtonTap response: " + packet.ToString());
            }
        });
    }
    #endregion

    #region COROUTINES
    #endregion
}