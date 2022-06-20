using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using System;

public class IPLocationService : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    public delegate void CallLoginFunction(bool gpsCollected);
    public event CallLoginFunction onGPSCollect;
    #endregion

    #region PRIVATE_VARIABLES
    //private string FindIPAddressAPI = "https://api6.ipify.org?format=json";
    private string FindIPAddressAPI = "http://ip-api.com/json";
    private string _ipAddress = "";
    private LocationCordinates _locationCordinates = new LocationCordinates();
    #endregion

    #region UNITY_CALLBACKS
    #endregion

    #region PUBLIC_METHODS
    public void SendIPAddress(string eventName)
    {
#if !UNITY_WEBGL
        HTTPRequest httpRequest = new HTTPRequest(new Uri(FindIPAddressAPI), (request, response) => {
			JSON_Object data = new JSON_Object (response.DataAsText);

			string ipAddress = "NA";
			if (data.has("ip"))
            {	
				ipAddress = data.getString("ip");
			}
            else if(data.has("query"))
            {
                ipAddress = data.getString("query");
            }

            if(data.has("timezone"))
            {
                PlayerPrefs.SetString("timezone", data.getString("timezone"));
            }

			UIManager.Instance.SocketGameManager.SendIPAddress(eventName, ipAddress, (socket,packet,args) => {
				PokerEventResponse eventResponse = JsonUtility.FromJson<PokerEventResponse> (Utility.Instance.GetPacketString(packet));
				if (eventResponse.status.Equals (Constants.PokerAPI.KeyStatusSuccess)) {
				}
			});
		});
		httpRequest.Send();
#endif
    }

    public void CheckGPSLocationFunction()
    {
        StartCoroutine(CheckGPSLocation());
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
            UIManager.Instance.DisplayMessagePanel("Location service is disabled.");
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

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
            print("Timed out");
            UIManager.Instance.DisplayMessagePanel("Timed out.");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            UIManager.Instance.DisplayMessagePanel("Unable to determine device location.");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            //print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

            locationCordinates.latitude = Input.location.lastData.latitude.floatToDouble();
            locationCordinates.longitude = Input.location.lastData.longitude.floatToDouble();
            UIManager.Instance.DisplayMessagePanel("GPS:" + locationCordinates.latitude + "," + locationCordinates.longitude);
            onGPSCollect(true);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
    #endregion

    #region SETTER_GETTER
    public LocationCordinates locationCordinates
    {
        set
        {
            _locationCordinates = value;
        }
        get
        {
            return _locationCordinates;
        }
    }

    public string IPAddress
    {
        set
        {
            _ipAddress = value;
        }
        get
        {
            return _ipAddress;
        }
    }
    #endregion
}
