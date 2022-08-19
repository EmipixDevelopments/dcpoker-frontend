using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TableElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _seatsText;
    [SerializeField] private TextMeshProUGUI _blindsText;
    [SerializeField] private TextMeshProUGUI _byInText;
    [SerializeField] private TextMeshProUGUI _typeText;
    [SerializeField] private Button _button;

    private RoomsListing.Room _data;

    private void Start()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnGameButtonTap);
    }

    public RoomsListing.Room GetData() { return _data; }

    public void SetData(RoomsListing.Room data) 
    {
        _data = data;
        _nameText.text = CheckStringData(data.roomName);
        _seatsText.text = CheckStringData($"{data.playerCount} / {data.maxPlayers}");
        _blindsText.text = CheckStringData(data.stake);
        _byInText.text = CheckStringData($"{data.minBuyIn}");
        _typeText.text = CheckStringData(data.type);
    }
    private string CheckStringData(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return $"---";
        }
        return text;
    }

    // copypast from TableData.cs
    public void OnGameButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        if (UIManager.Instance.tableManager.playingTableList.Count == UIManager.Instance.tableManager.maxTableLimit && !UIManager.Instance.tableManager.playingTableList.Contains(_data.roomId))
        {
            UIManager.Instance.DisplayMessagePanel("You can not join more than " + UIManager.Instance.tableManager.maxTableLimit + "tables at the same time.");
            return;
        }
        else if (_data.isPasswordProtected == true && !UIManager.Instance.tableManager.playingTableList.Contains(_data.roomId))
        {
            TableData tableData = new TableData();
            tableData.OnlySetDataNotView(_data);
            UIManager.Instance.PrivateTablePasswordPopup.OpenTablePasswordPopup(tableData);
            return;
        }

        OnViewGameTap();
    }
    // copypast from TableData.cs
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
    // copypast from TableData.cs
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
            yield break;
        }
        else
        {
            UIManager.Instance.SocketGameManager.LocationTableValidation(_data.roomId, Input.location.lastData.latitude.floatToDouble(), Input.location.lastData.longitude.floatToDouble(), (socket, packet, args) =>
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
    // copypast from TableData.cs
    IEnumerator NextScreen(float timer)
    {
        Constants.Poker.TableId = _data.roomId;
        UIManager.Instance.GameScreeen.SetRoomDataAndPlay(_data);

        UIManager.Instance.DisplayLoader("");
        yield return new WaitForSeconds(timer);
        UIManager.Instance.LobbyPanelNew.Close();
        UIManager.Instance.GameScreeen.Open();
    }
}
