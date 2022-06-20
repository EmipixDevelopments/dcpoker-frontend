using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEventManager : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    #endregion

    #region PRIVATE_VARIABLES
    #endregion

    #region UNITY_CALLBACKS
    void Start()
    {
        StartCoroutine(CallProfileEvent());
    }
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS
    public void GetProfileEventCall()
    {
        UIManager.Instance.SocketGameManager.GetProfile((socket, packet, args) =>
        {

            //Debug.Log("Background GetProfile  : " + packet.ToString());

            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse<Profile> resp = JsonUtility.FromJson<PokerEventResponse<Profile>>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                UIManager.Instance.assetOfGame.SavedLoginData.chips = resp.result.chips;
                UIManager.Instance.LobbyScreeen.Chips = resp.result.chips;
                UIManager.Instance.LobbyScreeen.ProfileScreen.PanelMyAccount.ProfilePanel.Chips = resp.result.chips;
                UIManager.Instance.GameScreeen.Chips = resp.result.chips;

            }
            else
            {
                print("GetProfile fail");
            }
        });
    }
    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES
    IEnumerator CallProfileEvent()
    {
        while (true)
        {
            if (Game.Lobby.socketManager != null && Game.Lobby.socketManager.Socket.IsOpen && UIManager.Instance.assetOfGame.SavedLoginData.PlayerId != ""
                && /*!UIManager.Instance.GameScreeen.isActiveAndEnabled &&*/
                !UIManager.Instance.MainHomeScreen.isActiveAndEnabled && !UIManager.Instance.webGLAffiliatePanel.isActiveAndEnabled)
            {
                GetProfileEventCall();
            }
            yield return new WaitForSeconds(8);
        }
    }
    #endregion

    #region GETTER_SETTER
    #endregion
}
