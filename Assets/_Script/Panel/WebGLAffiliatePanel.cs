using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BestHTTP;
using UnityEngine;

public class WebGLAffiliatePanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    #endregion

    #region PRIVATE_VARIABLES
    #endregion

    #region UNITY_CALLBACKS
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS

    public void StoreLoginData(string datas)
    {
        return;
        UIManager.Instance.DisplayLoader("");
        string Auth = AESEncryption(datas);
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
            UIManager.Instance.SocketGameManager.VerifyIdentifierToken(ipAddress, Auth, (socket, packet, args) =>
        {
            Debug.Log("VerifyIdentifierToken = " + packet.ToString());
            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp = Source;

            PokerEventResponse<PlayerLoginResponse> loginResponse = JsonUtility.FromJson<PokerEventResponse<PlayerLoginResponse>>(resp);
            if (loginResponse.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                UIManager.Instance.webglToken = loginResponse.result.deviceId;
                UIManager.Instance.IsMultipleTableAllowed = loginResponse.result.isMultipleTableAllowed;
                UIManager.Instance.isChipsTransferAllowed = loginResponse.result.isChipsTransferAllowed;
                UIManager.Instance.assetOfGame.SavedLoginData.chips = loginResponse.result.chips;
                UIManager.Instance.assetOfGame.SavedLoginData.PlayerId = loginResponse.result.id;
                UIManager.Instance.assetOfGame.SavedLoginData.PlayerId = loginResponse.result.playerId;
                UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar = loginResponse.result.profilePic;
                UIManager.Instance.assetOfGame.SavedLoginData.isCash = loginResponse.result.isCash;
                UIManager.Instance.assetOfGame.SavedLoginData.isInAppPurchaseAllowed = loginResponse.result.isInAppPurchaseAllowed;
                UIManager.Instance.ProfilePic = UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar;
                UIManager.Instance.assetOfGame.SavedLoginData.userUuid = loginResponse.result.userUuid;
                UIManager.Instance.assetOfGame.SavedLoginData.isSuperPlayer = loginResponse.result.isSuperPlayer;
                UIManager.Instance.MySuperPlayer = loginResponse.result.isSuperPlayer;
                UIManager.Instance.assetOfGame.SavedLoginData.timeZone = loginResponse.result.timeZone;
                PlayerPrefs.SetString("timezone", loginResponse.result.timeZone);
                StartCoroutine(NextScreen(1.1f));
                UIManager.Instance.assetOfGame.SavedLoginData.Username = loginResponse.result.username;
                UIManager.Instance.assetOfGame.SavedLoginData.password = "";

                UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe = false;
                LocalSaveData.current.Username = UIManager.Instance.assetOfGame.SavedLoginData.Username;
                LocalSaveData.current.password = UIManager.Instance.assetOfGame.SavedLoginData.password;
                LocalSaveData.current.isRememberMe = UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe;

                SaveLoad.SaveGame();

                if (UIManager.Instance.assetOfGame.SavedLoginData.isCash)
                {
                    UIManager.Instance.currencyType = UIManager.CurrencyType.cash;
                }
                else
                {
                    UIManager.Instance.currencyType = UIManager.CurrencyType.coin;
                }
                UIManager.Instance.SetCurrencyImages();
                UIManager.Instance.ipLocationService.SendIPAddress("login");
            }
            else if (loginResponse.status == "forceLogout")
            {

            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(loginResponse.message, null);
            }
        });
        });
        httpRequest.Send();
    }
    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES
    IEnumerator NextScreen(float timer)
    {
        UIManager.Instance.DisplayLoader("");
        yield return new WaitForSeconds(timer);
        UIManager.Instance.webGLAffiliatePanel.Close();
    }
    #endregion

    #region GETTER_SETTER
    public string AESEncryption(string inputData)
    {
        AesCryptoServiceProvider AEScryptoProvider = new AesCryptoServiceProvider();
        AEScryptoProvider.BlockSize = 128;
        AEScryptoProvider.KeySize = 256;
        AEScryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes("6oXaHd88sjQhiL8cjOu7AUwsgi4IaZU2");
        AEScryptoProvider.IV = ASCIIEncoding.ASCII.GetBytes("37bOysXvYlM32WmO");
        AEScryptoProvider.Mode = CipherMode.CBC;
        AEScryptoProvider.Padding = PaddingMode.PKCS7;

        byte[] txtByteData = ASCIIEncoding.ASCII.GetBytes(inputData);
        ICryptoTransform trnsfrm = AEScryptoProvider.CreateEncryptor(AEScryptoProvider.Key, AEScryptoProvider.IV);

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return Convert.ToBase64String(result);
    }
    #endregion
}
