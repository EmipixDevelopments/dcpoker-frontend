using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Linq;

public class TableData : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    [Header("Images")]
    public Image BarMain;
    public Image imgLockIcon;
    public Image imgjoinIcon;

    [Header("Text")]
    public TextMeshProUGUI Type;
    public Text GameName;
    public TextMeshProUGUI Seats;
    public TextMeshProUGUI Blind;
    public TextMeshProUGUI minBuyin;
    public TextMeshProUGUI AVGP;

    [Header("ScriptableObjects")]
    public RoomsListing.Room data;

    [Header("Colors")]
    public Sprite[] Colors;
    public Sprite[] joinColors;

    #endregion

    #region PRIVATE_VARIABLES
    #endregion

    #region UNITY_CALLBACKS
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS
    public void ButtonHold(Animator a)
    {
        a.SetBool("IsJoin", true);
    }

    public void ButtonRelease(Animator a)
    {
        a.SetBool("IsJoin", false);
    }
    public void OnGameButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.tableManager.playingTableList.Count == UIManager.Instance.tableManager.maxTableLimit && !UIManager.Instance.tableManager.playingTableList.Contains(data.roomId))
        {
            UIManager.Instance.DisplayMessagePanel("You can not join more than " + UIManager.Instance.tableManager.maxTableLimit + "tables at the same time.");
            return;
        }
        else if (data.isPasswordProtected == true && !UIManager.Instance.tableManager.playingTableList.Contains(data.roomId))
        {
            UIManager.Instance.LobbyScreeen.privateTablePasswordPopup.OpenTablePasswordPopup(this);
            return;
        }

        OnViewGameTap();
    }

    public void OnViewGameTap()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (data.isGPSRestriction)
        {
            StartCoroutine(CheckGPSLocation());
            return;
        }
#endif

        StartCoroutine(NextScreen(0f));
    }

    public void SetData(RoomsListing.Room data, int i)
    {
        this.data = data;

        if (string.IsNullOrEmpty(data.roomName) == false)
        {
            //   GameName.text = data.roomName;
            //HebrewOwn(data.roomName);
            Utility.Instance.CheckHebrewOwn(GameName, data.roomName);
            //GameName.text = name;
        }
        else
        {
            GameName.text = "---";
        }

        //		if (data.playerCount > 0) {
        Seats.text = data.playerCount + "/" + data.maxPlayers;
        //		} else {
        //			Seats.text = data.playerCount + "/9";
        //		}

        if (string.IsNullOrEmpty(data.stake) == false)
        {

            Blind.text = data.stake;
        }
        else
        {
            Blind.text = "---";
        }

        if (string.IsNullOrEmpty(data.type) == false)
        {

            Type.text = data.type.ToUpper();
        }
        else
        {
            Type.text = "---";
        }

        if (double.IsNaN(data.minBuyIn) == false)
        {
            minBuyin.text = data.minBuyIn.ToString();
        }
        else
        {
            minBuyin.text = "---";
        }

        //game.text = data.isLimitGame;
        //		txtPlayers.text = data.maxPlayers;
        if (i % 2 == 0)
        {
            BarMain.sprite = Colors[0];
        }
        else
        {
            BarMain.sprite = Colors[1];
        }

        if (data.isPasswordProtected)
            imgLockIcon.Open();
        else
            imgLockIcon.Close();


        if (data.playerCount > 0)
        {
            imgjoinIcon.sprite = joinColors[0];
        }
        else
        {
            imgjoinIcon.sprite = joinColors[1];
        }

        this.Open();

    }


    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES

    IEnumerator CheckGPSLocation()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            UIManager.Instance.DisplayMessagePanel("Location service is disabled. Enable location service to join the table.");
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        UIManager.Instance.DisplayLoader("");

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            print("maxWait: " + maxWait);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            UIManager.Instance.HideLoader();
            print("Timed out");
            UIManager.Instance.DisplayMessagePanel("Not able to fetch location data. Please try again.");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            UIManager.Instance.HideLoader();
            print("Unable to determine device location");
            //UIManager.Instance.DisplayMessagePanel("Unable to determine device location. Please try again.");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            //print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

            //locationCordinates.latitude = Input.location.lastData.latitude.floatToDouble();
            //locationCordinates.longitude = Input.location.lastData.longitude.floatToDouble ();

            UIManager.Instance.SocketGameManager.LocationTableValidation(data.roomId, Input.location.lastData.latitude.floatToDouble(), Input.location.lastData.longitude.floatToDouble(), (socket, packet, args) =>
            {
                Debug.Log("LocationTableValidation response: " + packet.ToString());

                PokerEventResponse eventResponse = JsonUtility.FromJson<PokerEventResponse>(Utility.Instance.GetPacketString(packet));
                if (eventResponse.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                {
                    UIManager.Instance.ipLocationService.locationCordinates.latitude = Input.location.lastData.latitude.floatToDouble();
                    UIManager.Instance.ipLocationService.locationCordinates.longitude = Input.location.lastData.longitude.floatToDouble();
                    StartCoroutine(NextScreen(0f));
                }
                else
                {
                    UIManager.Instance.HideLoader();
                    UIManager.Instance.DisplayMessagePanel(eventResponse.message, null);
                }
            });
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

    IEnumerator NextScreen(float timer)
    {
        /*if (UIManager.Instance.IsMultipleTableAllowed && !UIManager.Instance.tableManager.playingTableList.Contains (data.roomId)) {
			UIManager.Instance.tableManager.DeselectAllTableSelection ();
			UIManager.Instance.tableManager.AddMiniTable (data);
			UIManager.Instance.tableManager.HighlightMiniTable (data.roomId);
		}*/

        Constants.Poker.TableId = data.roomId;
        UIManager.Instance.GameScreeen.SetRoomDataAndPlay(data);

        UIManager.Instance.DisplayLoader("");
        yield return new WaitForSeconds(timer);
        UIManager.Instance.LobbyPanelNew.Close(); // LobbyScreeen not used more
        UIManager.Instance.GameScreeen.Open();
    }
    #endregion

    #region GETTER_SETTER
    #endregion

}
