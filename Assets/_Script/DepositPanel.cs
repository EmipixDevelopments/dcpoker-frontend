using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DepositPanel : MonoBehaviour
{
    public TMP_InputField inputAmount;
    public Text txtImgName;
    public string imageBase64String;
    public Text txtError;
    public Sprite sp = null;
    public Image ProfilePicSprite;
    public Image ProfilePicImg;
    public Button btnBrowse;
    public Button btnBrowseWebGL;
    public bool isDigitPresent;
    [Header("Transform")]
    public Transform transformPopup;

    void OnEnable()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        ResetData();
        UIManager.Instance.isGalleryOpen = true;

        txtError.text = "";

        btnBrowse.Open();
        btnBrowseWebGL.Close();
        ProfilePicSprite.Close();
    }
    private void OnDisable()
    {
        UIManager.Instance.isGalleryOpen = false;
        Debug.Log("isGalleryOpen 2 : " + UIManager.Instance.isGalleryOpen);

        ProfilePicImg.sprite = null;
        ProfilePicImg.Close();
        btnBrowseWebGL.Open();
        btnBrowse.Close();
        ProfilePicSprite.Close();
    }
    void ResetData()
    {
        txtImgName.text = "";
        inputAmount.text = "";
    }
    public void KeyboardOpen(float positionY)
    {
#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
        inputAmount.contentType = TMP_InputField.ContentType.IntegerNumber;
        transformPopup.localPosition = new Vector3(0, positionY, 0);
#endif
    }

    public void KeyboardClose()
    {
#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
        transformPopup.localPosition = new Vector3(0, 0, 0);
#endif
    }
    public void BrowseImg(Texture2D texture)
    {
        sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        ProfilePicSprite.sprite = sp;
        btnBrowse.Close();
        ProfilePicSprite.Open();

        //webgl
        //ExternalCallClass.Instance.OpenDepositBrowsePanel();
    }

    public void DepositBase64Img(string base64)
    {
        this.imageBase64String = base64;

        byte[] imageBytes = Convert.FromBase64String(base64);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imageBytes);
        Sprite sp = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        //   ProfilePicSprite.sprite = sp;
        ProfilePicImg.sprite = sp;
        //  btnBrowse.Close();
        btnBrowseWebGL.Close();
        //  ProfilePicSprite.Open();
        ProfilePicImg.Open();
    }
    public void SubmitButtonTap()
    {
        Debug.Log("isGalleryOpen : " + UIManager.Instance.isGalleryOpen);
        if (isAmountValid())
        {
            if (UIManager.Instance.SocketGameManager.HasInternetConnection())
            {
                string amount = inputAmount.text;
                string Image64 = imageBase64String;
                //    txtImgName.text = Image64;

                if (amount.All(char.IsDigit))
                {
                    UIManager.Instance.DisplayLoader("Uploading process");
                    UIManager.Instance.SocketGameManager.UploadDeposit(amount, Image64, (socket, packet, args) =>
                     {
                         Debug.Log("UploadDeposit = " + packet.ToString());
                         UIManager.Instance.HideLoader();
                         JSONArray arr = new JSONArray(packet.ToString());
                         string Source;
                         Source = arr.getString(arr.length() - 1);
                         var resp = Source;

                         PokerEventResponse updateResp = JsonUtility.FromJson<PokerEventResponse>(resp);
                         if (updateResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                         {
                             UIManager.Instance.DisplayMessagePanel(updateResp.message, null);
                             StartCoroutine("ClosePanel");
                             ResetData();
                         }
                         else
                         {
                             UIManager.Instance.DisplayMessagePanel(updateResp.message, null);
                         }

                     });
                }

                else
                {
                    txtError.text = "Enter Only Digits";
                    StartCoroutine(textempti());
                }
            }
        }
        //   Debug.Log("status 3 : " + UIManager.Instance.isGalleryOpen);

        //    UIManager.Instance.isGalleryOpen = false;
        //Debug.Log("status 4 : " + UIManager.Instance.isGalleryOpen);

    }

    public void CloseImage()
    {
        ProfilePicSprite.Close();
        btnBrowse.Open();
        ProfilePicSprite.sprite = null;
        imageBase64String = "";
    }
    public void CloseWebGLImage()
    {
        ProfilePicImg.Close();
        //      CloseWebGl.Close();
        btnBrowseWebGL.Open();
        //     ProfilePicRawImg = null;
        ProfilePicImg.sprite = null;

        //comment
        UIManager.Instance.LobbyScreeen.ProfileScreen.PanelMyAccount.depositPanel.btnBrowseWebGL.Open();
        imageBase64String = "";
    }

    IEnumerator ClosePanel()
    {
        yield return new WaitForSeconds(0.5f);
        // UIManager.Instance.HidemessagePanelInfoPopup();
        this.Close();
        UIManager.Instance.LobbyScreeen.ProfileScreen.PanelMyAccount.SelectedOptionButtonTap(0);
    }
    private bool isAmountValid()
    {
        txtError.text = "";

        string amount = inputAmount.text;

        if (string.IsNullOrEmpty(amount))
        {
            txtError.text = "Amount is Empty.";

            //    txtError.text = "Amount is Empty.";
            StartCoroutine(textempti());
            return false;
        }
        return true;

    }

    private bool IsDigitValid()
    {
        string amount = inputAmount.text;
        Regex regex = new Regex(@"^(\+[0-9]{9})$");
        Match match = regex.Match(amount);

        if (!match.Success)
        {
            txtError.text = "Enter only Digits";
            StartCoroutine(textempti());
            return false;
        }
        return true;
    }

    IEnumerator textempti()
    {
        yield return new WaitForSeconds(1f);
        txtError.text = "";
    }
}
