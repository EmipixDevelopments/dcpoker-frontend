using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.SocketIO;
using System;
using PlatformSupport.Collections.ObjectModel;
using BestHTTP;

namespace Game
{
    public class Lobby
    {
        [Header("Socket")] public static SocketManager socketManager = null;

        public static string deviceUniqueID;

        //		public static Socket CashSocket;
        public static Socket RegTournamentSocket;
        public static Socket SNGTournamentSocket;
        public static string SOCKET_EVENT_CONNECT = "connect";
        public static string SOCKET_EVENT_RECONNECT = "reconnect";
        public static string SOCKET_EVENT_RECONNECTING = "reconnecting";
        public static string SOCKET_EVENT_RECONNECT_ATTEMPT = "reconnect_attempt";
        public static string SOCKET_EVENT_RECONNECT_FAILED = "reconnect_failed";
        public static string SOCKET_EVENT_DISCONNECT = "disconnect";
        public static string SOCKET_EVENT_ForceLogout = "forceLogOut";
        public static string SOCKET_EVENT_Maintenance = "maintenanceServer";
        public static string SOCKET_EVENT_AdminNotification = "adminNotification";

        static Socket _CashSocket;

        public static Socket CashSocket
        {
            get
            {
                if (UIManager.Instance.selectedGameType == GameType.cash)
                {
                    //_CashSocket = socketManager.GetSocket ("/cash_texas");
                    return _CashSocket;
                }
                else if (UIManager.Instance.selectedGameType == GameType.Touranment)
                {
                    //_CashSocket = socketManager.GetSocket ("/cash_regular_texas");
                    return _CashSocket;
                }
                else if (UIManager.Instance.selectedGameType == GameType.sng)
                {
                    //_CashSocket = socketManager.GetSocket ("/cash_sng_texas");
                    return _CashSocket;
                }

                return _CashSocket;
            }
            set
            {
                /*if (UIManager.Instance.selectedGameType == GameType.cash) {
					_CashSocket = socketManager.GetSocket ("/cash_texas");
				} else if (UIManager.Instance.selectedGameType == GameType.Touranment) {
					_CashSocket = socketManager.GetSocket ("/cash_regular_texas");
				} else if (UIManager.Instance.selectedGameType == GameType.sng) {
					_CashSocket = socketManager.GetSocket ("/cash_sng_texas");
				}	*/
                _CashSocket = value;
            }
        }

        public static string SetSocketNamespace
        {
            set { _CashSocket = socketManager.GetSocket("/" + value); }
            get { return _CashSocket.Namespace; }
        }


        public static void ConnectToSocket()
        {
            deviceUniqueID = SystemInfo.deviceUniqueIdentifier;

            SocketOptions options = new SocketOptions();
            options.ReconnectionAttempts = 600;
            options.AutoConnect = true;
            options.ReconnectionDelay = TimeSpan.FromSeconds(60);
            options.Timeout = TimeSpan.FromSeconds(30);
            options.Reconnection = true;

            options.ConnectWith = BestHTTP.SocketIO.Transports.TransportTypes.WebSocket;


            ObservableDictionary<string, string> param = new ObservableDictionary<string, string>();
            param.Add("__sails_io_sdk_version", "0.12.13");
            param.Add("__sails_io_sdk_platform", "browser");
            param.Add("__sails_io_sdk_language", "javascript");
            param.Add("device_id", deviceUniqueID);
            //param.Add("authToken", UIManager.Instance.tokenHack());
            //Debug.Log(UIManager.Instance.tokenHack());

            options.AdditionalQueryParams = param;
            BestHTTP.HTTPManager.Setup();

            if (UIManager.Instance.server == SERVER.Custom)
                socketManager = new SocketManager(new Uri(PlayerPrefs.GetString("CUSTOM_URL") + "/socket.io/"), options);
            else
                socketManager = new SocketManager(new Uri(Constants.PokerAPI.BaseUrl + "/socket.io/"), options);
            BestHTTP.HTTPManager.Setup();

            //			CashSocket = socketManager.GetSocket("/cash");
            //CashSocket = socketManager.GetSocket ("/cash_texas");
            RegTournamentSocket = socketManager.GetSocket("/cash_regular_texas");
            SNGTournamentSocket = socketManager.GetSocket("/cash_sng_texas");
            //omahaSocket = socketManager.GetSocket("/omaha");
            //texasTournamentSocket = socketManager.GetSocket("/sng_texas");
            socketManager.Socket.On(SOCKET_EVENT_CONNECT, OnConnect);
            socketManager.Socket.On(SOCKET_EVENT_RECONNECT, OnReConnect);
            socketManager.Socket.On(SOCKET_EVENT_RECONNECTING, OnReConnecting);
            socketManager.Socket.On(SOCKET_EVENT_RECONNECT_ATTEMPT, OnReConnectAttempt);
            socketManager.Socket.On(SOCKET_EVENT_RECONNECT_FAILED, OnReConnectFailed);
            socketManager.Socket.On(SOCKET_EVENT_DISCONNECT, OnDisconnect);
            socketManager.Socket.On(SOCKET_EVENT_ForceLogout, OnForceLogout);
            socketManager.Socket.On(SOCKET_EVENT_Maintenance, OnMaintenanceServer);
            socketManager.Socket.On(SOCKET_EVENT_AdminNotification, OnAdminNotification);

            Debug.Log("URL: " + socketManager.Uri);
        }

        private static void OnConnect(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
        {
            Debug.Log("-- Connected --");
            UIManager.Instance.SocketOn();
            UIManager.Instance.HideLoader();
            UIManager.Instance.HidePopup();


            if (UIManager.Instance.IsWebGLAffiliat)
            {
                ExternalCallClass.Instance.RequestGameData();
                return;
            }
        }

        private static void OnReConnect(Socket Socket, Packet Packet, params object[] Args)
        {
            Debug.Log("Re-Connect...");
            //if (UIManager.Instance.SocketGameManager.HasInternetConnection ()) {
            UIManager.Instance.HideLoader();
            UIManager.Instance.HidePopup();
            if (!UIManager.Instance.MainHomeScreen.isActiveAndEnabled && UIManager.Instance.MainHomeScreen.ShowLoader())
            {
                ReconnectPlayerCall();
                UIManager.Instance.MainHomeScreen.loginPageFocusCounter = 0;
            }

            UIManager.Instance.ipLocationService.SendIPAddress("reconnect");
            //}
            //			else 
            //			{
            //					UIManager.Instance.DisplayMessagePanel (Constants.Messages.NoInternetConnection,OnLoginScreenTap);
            //					UIManager.Instance.HideLoader ();
            //			}
        }

        private static void OnReConnecting(Socket socket, Packet packet, params object[] args)
        {
            Debug.Log("Re-Connecting...");

            if (UIManager.Instance.MainHomeScreen.ShowLoader())
            {
                UIManager.Instance.HideLoader();
                UIManager.Instance.DisplayLoader("Reconnecting...");
            }
            else
            {
                UIManager.Instance.MainHomeScreen.loginPageFocusCounter--;
            }
        }

        private static void OnReConnectAttempt(Socket socket, Packet packet, params object[] args)
        {
            Debug.Log("Re-Connect Attempt...");
            if (UIManager.Instance.MainHomeScreen.ShowLoader())
            {
                UIManager.Instance.DisplayLoader("Reconnecting...");
            }
        }

        private static void OnReConnectFailed(Socket socket, Packet packet, params object[] args)
        {
            Debug.Log("Re-ConnectFailed...");
            if (UIManager.Instance.SocketGameManager.HasInternetConnection())
            {
                UIManager.Instance.HideLoader();
                UIManager.Instance.DisplayMessagePanel(Constants.Messages.DisconnectedServer, OnLoginScreenTap);
            }
            else
            {
                UIManager.Instance.HideLoader();
                UIManager.Instance.DisplayMessagePanel(Constants.Messages.NoInternetConnection, OnLoginScreenTap);
            }
        }

        private static void OnDisconnect(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
        {
            if (!UIManager.Instance.isGalleryOpen)
            {
                Debug.Log("-- Disconnected --");
                //Debug.Log("isGalleryOpen4 : " + UIManager.Instance.isGalleryOpen);

                if (UIManager.Instance.MainHomeScreen.ShowLoader())
                {
                    UIManager.Instance.DisplayLoader("Reconnecting...");
                }
            }

            //			if (!UIManager.Instance.isLogOut) 
            //			{
            //				//UIManager.Instance.DisplayMessagePanel ("You Are Not Connect to Server",OnLoginScreenTap);
            //				UIManager.Instance.HideLoader ();
            //			}
        }

        private static void OnForceLogout(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
        {
            Debug.Log("-- OnForceLogout --" + packet.ToString());
            JSONArray arr = new JSONArray(packet.ToString());

            JSON_Object ForceLogutPlayer = new JSON_Object(arr.getString((arr.length() - 1)));
            string ForceLogutPlayerId = ForceLogutPlayer.getString("playerId");

            UIManager.Instance.Reset(false);
            UIManager.Instance.tableManager.RemoveAllMiniTableData();

            if (ForceLogutPlayer.has("message"))
            {
                UIManager.Instance.DisplayMessagePanel(ForceLogutPlayer.getString("message"), null);
            }
        }

        private static void OnMaintenanceServer(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
        {
            Debug.Log("-- OnMaintenanceServer --" + packet.ToString());

            MaintenaceResponse maintenaceResponse = JsonUtility.FromJson<MaintenaceResponse>(Utility.Instance.GetPacketString(packet));
            UIManager.Instance.DisplayMessagePanel(maintenaceResponse.message);
        }

        private static void OnAdminNotification(Socket socket, BestHTTP.SocketIO.Packet packet, params object[] args)
        {
            Debug.Log("-- OnAdminNotification --" + packet.ToString());

            AdminNotificationResponse adminNotificationResponse = JsonUtility.FromJson<AdminNotificationResponse>(Utility.Instance.GetPacketString(packet));
            UIManager.Instance.DisplayMessagePanel(adminNotificationResponse.message);
        }

        public static void OnLoginScreenTap()
        {
#if UNITY_WEBGL
            if (UIManager.Instance.IsWebGLAffiliat)
                ExternalCallClass.Instance.ExitGame();
            else
                UIManager.Instance.messagePanel.Close();
#else
            UIManager.Instance.messagePanel.Close();
#endif
        }

        public void OnLogOutDone()
        {
            if (UIManager.Instance.IsWebGLAffiliat)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                ExternalCallClass.Instance.ExitGame();
            }
            else
            {
                StopCoroutine("LogoutFunction");
                StartCoroutine(LogoutFunction(0f));
            }
        }

        private void StopCoroutine(string v)
        {
            throw new NotImplementedException();
        }

        private void StartCoroutine(IEnumerator enumerator)
        {
            throw new NotImplementedException();
        }

        public static void ReconnectPlayerCall()
        {
            HTTPRequest httpRequest = new HTTPRequest(new Uri("http://ip-api.com/json"), (request, response) =>
            {
                JSON_Object data = new JSON_Object(response.DataAsText);

                string ipAddress = "NA";
                if (data.has("ip"))
                {
                    ipAddress = data.getString("ip");
                }
                else if (data.has("query"))
                {
                    ipAddress = data.getString("query");
                }

                UIManager.Instance.SocketGameManager.GetReconnect(ipAddress, (socket, packet, args) =>
                {
                    Debug.Log("GetReconnect  : " + packet.ToString());
                    UIManager.Instance.HideLoader();
                    try
                    {
                        JSONArray arr = new JSONArray(packet.ToString());
                        string Source;
                        Source = arr.getString(arr.length() - 1);
                        var resp1 = Source;
                        PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);

                        if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                        {
                            UIManager.Instance.tableManager.ReSubscribeMiniTables();

                            if (UIManager.Instance.GameScreeen.gameObject.activeInHierarchy)
                            {
                                Debug.Log("UIManager.Instance.selectedGameType " + UIManager.Instance.selectedGameType.ToString());
                                Debug.Log("ReconnectPlayerCall Namespace " + Game.Lobby.CashSocket.Namespace);
                                GetReconnectGameCall();
                            }
                        }
                        else
                        {
                            if (resp.status == "forceLogout")
                            {
                            }
                            else if (resp.message == "No Running Game Found!" || resp.message == "")
                            {
                            }
                            else
                            {
                                ReconnectPlayerCall();
                            }
                        }
                    }
                    catch (System.Exception e)
                    {
                        UIManager.Instance.DisplayMessagePanel(Constants.Messages.SomethingWentWrong);
                        Debug.LogError("exception  : " + e);
                    }
                });
            });
            httpRequest.Send();
        }

        public static void GetReconnectGameCall()
        {
            UIManager.Instance.DisplayLoader("");
            UIManager.Instance.GameScreeen.reconnectResetData();
            UIManager.Instance.SocketGameManager.GetReconnectGame(UIManager.Instance.GameScreeen.OnSubscribeRoomDone);
            UIManager.Instance.tableManager.ReSubscribeMiniTables();
        }

        IEnumerator LogoutFunction(float timer)
        {
            UIManager.Instance.assetOfGame.SavedLoginData.PlayerId = "";
            UIManager.Instance.isLogOut = true;
            Game.Lobby.socketManager.Close();
            UIManager.Instance.DisplayLoader("");
            Game.Lobby.socketManager.Open();
            Game.Lobby.ConnectToSocket();
            UIManager.Instance.tableManager.RemoveAllMiniTableData();
            yield return new WaitForSeconds(timer);
            UIManager.Instance.Reset(false);
        }
    }
}