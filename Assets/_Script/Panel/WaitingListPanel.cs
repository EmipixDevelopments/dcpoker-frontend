using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaitingListPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    public WaitingPlayerListPanel WaitingPlayerListScreen;
    [Header("Buttons")]
    public Button btnJoinWaitingList;
    public Button btnLeaveWaitingList;

    [Header("Text")]
    public Text txtWaitingListCount;
    public Text txtYourPosition;


    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS

    void OnEnable()
    {
        ChangeLanguage();
    }

    private void ChangeLanguage()
    {
        WaitingPlayerListScreen.Close();
        txtYourPosition.text = "Your Position";
        txtWaitingListCount.text = "View Waiting list";
    }

    #endregion

    #region DELEGATE_CALLBACKS

    #endregion

    #region PUBLIC_METHODS
    public void OpenWaitingPlayerListPanel()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        WaitingPlayerListScreen.Open();
    }
    public void WaitingListFunction(ReservedListResult result)
    {

        bool isPlayingInRoom = false;
        foreach (string playerId in result.roomPlayers)
        {
            if (playerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
            {
                isPlayingInRoom = true;
                btnJoinWaitingList.Close();
                btnLeaveWaitingList.Close();
                this.Close();
                return;
            }
        }

        bool isAvailableInWaitingList = false;
        bool isMyInWaiting = false;
        int yourPosition = 0;
        int playersInWaitingList = 0;
        playersInWaitingList = result.waitingPlayers.Count;

        for (int i = 0; i < result.waitingPlayers.Count && !isAvailableInWaitingList; i++)
        {

            if (result.waitingPlayers[i].playerId == UIManager.Instance.assetOfGame.SavedLoginData.PlayerId)
            {
                yourPosition = (i + 1);
                isAvailableInWaitingList = true;
                UIManager.Instance.GameScreeen.waitingPlayerSeatManage(result.waitingPlayers[i].seatIndex);
            }
            else
            {
                isMyInWaiting = true;
                UIManager.Instance.GameScreeen.waitingPlayerSeatManage(10);
            }

        }



        //btnLeaveWaitingList.gameObject.SetActive(result.isLeaveWaiting);
        if (isAvailableInWaitingList)
        { //leave button
            Debug.Log("Here1");
            this.Open();
            YourWaitingPosition = yourPosition;
            TotalWaitingList = playersInWaitingList;
            btnLeaveWaitingList.Open();
        }
        else if (result.enableJoinWaitingListButton)
        { //join button
            this.Open();
            UIManager.Instance.GameScreeen.waitingPlayerSeatManage(10);
            Debug.Log("Here2");
            YourWaitingPosition = 0;
            TotalWaitingList = playersInWaitingList;
            btnJoinWaitingList.interactable = true;
            btnJoinWaitingList.Open();
            btnLeaveWaitingList.Close();
        }

        else
        {
            btnJoinWaitingList.Close();
            btnLeaveWaitingList.Close();
            //Debug.Log("Here3");
            this.Close();
            var gamePanel = UIManager.Instance.GameScreeen;
            if (gamePanel.gameObject.activeInHierarchy)
            {
                gamePanel.UpdateOpenSeatButton();
                /*for (int i = 0; i < UIManager.Instance.GameScreeen.GamePlayers.Length; i++)
                {
                    if (!UIManager.Instance.GameScreeen.GamePlayers[i].gameObject.activeInHierarchy)
                    {
                        UIManager.Instance.GameScreeen.SetActiveOpenSeatButton(i, true);
                    }
                    else
                    {
                        UIManager.Instance.GameScreeen.SetActiveOpenSeatButton(i, false);
                    }
                }*/
            }
        }
        //if (isMyInWaiting && !result.enableJoinWaitingListButton)
        //{
        //    this.Open();
        //    YourWaitingPosition = 0;
        //    TotalWaitingList = playersInWaitingList;
        //    btnJoinWaitingList.Open();
        //}

        /*
        foreach (Button btn in UIManager.Instance.GameScreeen.Seats)
        {
            //btn.transform.GetChild (0).gameObject.SetActive (false);
            btn.transform.GetChild(1).gameObject.SetActive(false);
        }

        foreach (int index in result.reservedSeat)
        {
            //UIManager.Instance.gamePanel.Seats [index].transform.GetChild (0).gameObject.SetActive (true);
            UIManager.Instance.gamePanel.Seats[index].transform.GetChild(1).gameObject.SetActive(true);
        }*/
    }

    public void JoinWaitingList()
    {
        btnJoinWaitingList.interactable = false;
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.JoinWaitingList((socket, packet, args) =>
        {
            print("JoinWaitingList response: " + packet.ToString());

            PokerEventResponse response = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet));

            if (response.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                btnJoinWaitingList.interactable = true;
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(response.message);
            }
        });
    }

    public void LeaveWaitingList()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        UIManager.Instance.SocketGameManager.LeaveWaitingList((socket, packet, args) =>
        {
            print("LeaveWaitingList response: " + packet.ToString());

            PokerEventResponse response = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet));

            if (response.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                //this.Close();
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(response.message);
            }
        });
    }

    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES

    #endregion

    #region GETTER_SETTER

    public int TotalWaitingList
    {
        set
        {
            if (value > 0)
            {
                txtWaitingListCount.text = "View Waiting list ( " + value + " )";
                txtWaitingListCount.Open();
            }
            else
            {
                txtWaitingListCount.Close();
            }
        }
    }

    public int YourWaitingPosition
    {
        set
        {
            if (value > 0)
            {
                txtYourPosition.text = "Your position: " + value;
                txtYourPosition.Open();
                btnJoinWaitingList.Close();

            }
            else
            {
                txtYourPosition.Close();
                btnJoinWaitingList.Open();
            }
        }
    }

    #endregion
}
