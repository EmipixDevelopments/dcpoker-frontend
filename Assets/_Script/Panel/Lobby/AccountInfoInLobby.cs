using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccountInfoInLobby : MonoBehaviour
{
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Image _ammountImage;
    [SerializeField] private TextMeshProUGUI _ammountText;
    [SerializeField] private TextMeshProUGUI _myAccount;
    [Space] [SerializeField] private Sprite _cashSprite;
    [SerializeField] private Sprite _chipsSprite;

    [SerializeField] private GameObject _avatarImageContainer;
    [SerializeField] private Image _avatarImageUrl;


    private int _avatarImageIndex = -1;
    private double _ammount = 0;

    private Coroutine _updateAvatarCoroutine;

    void OnEnable()
    {
        _updateAvatarCoroutine = StartCoroutine(UpdateAvatar());
    }

    private void OnDisable()
    {
        StopCoroutine(_updateAvatarCoroutine);
    }

    private IEnumerator UpdateAvatar()
    {
        while (true)
        {
            if (UIManager.Instance)
            {
                CallProfileEvent();
            }

            yield return new WaitForSeconds(3);
        }
    }

    /*private void UpdateAvatarImage()
    {
        var profilePic = UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar;
        if (profilePic == -1)
        {
            Debug.LogError("Need profile Image Implementation");
            profilePic = 0;
        }
        
        if (profilePic != _avatarImageIndex)
        {
            _avatarImageIndex = profilePic;
            _avatarImage.sprite = UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[_avatarImageIndex];
        }
        
        if (UIManager.Instance.assetOfGame.SavedLoginData.isCash)
        {
            if (_ammount != UIManager.Instance.assetOfGame.SavedLoginData.cash)
            {
                _ammount = UIManager.Instance.assetOfGame.SavedLoginData.cash;
                _ammountText.text = _ammount.ToString();
            }
            _ammountImage.sprite = _cashSprite;
        }
        else
        {
            if (_ammount != UIManager.Instance.assetOfGame.SavedLoginData.chips)
            {
                _ammount = UIManager.Instance.assetOfGame.SavedLoginData.chips;
                _ammountText.text = _ammount.ToString();
            }
            _ammountImage.sprite = _chipsSprite;
        }
    }*/
    private void UserWalletBalance()
    {
        string privateKeyString = JsonConvert.SerializeObject(UIManager.Instance.assetOfGame.SavedLoginData.privateKey);
        if (UIManager.Instance.SocketGameManager.HasInternetConnection())
        {
            UIManager.Instance.SoundManager.OnButtonClick();
//            if (IsLoginDetailValid())
            {
                UIManager.Instance.DisplayLoader("");

                UIManager.Instance.SocketGameManager.UserWalletBalance(UIManager.Instance.assetOfGame.SavedLoginData.Username, UIManager.Instance.assetOfGame.SavedLoginData.PlayerId, UIManager.Instance.assetOfGame.SavedLoginData.publicKey
                    , privateKeyString, (socket, packet, args) =>
                    {
                        Debug.Log("UserWalletBalance  => " + packet.ToString());

                        UIManager.Instance.HideLoader();
                        JSONArray arr = new JSONArray(packet.ToString());
                        string Source;
                        Source = arr.getString(arr.length() - 1);
                        var resp = Source;
                        PokerEventResponse registrationResp = JsonUtility.FromJson<PokerEventResponse>(resp);

//                    RecoveryPhraseEventResponse recoveryPhraseEventResponse = JsonUtility.FromJson<RecoveryPhraseEventResponse>(registrationResp.result);

//                    Debug.LogError("Register Player Response : " + registrationResp + " | " + recoveryPhraseEventResponse);

                        if (registrationResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                        {
//                            _phrase = registrationResp.result.recoveryPhrase;
//                            textRecoveryPhraseFromServer.text = _phrase;
//                            panelPassword.SetActive(false);
//                            panelRecovery.SetActive(true);
                        }
                        else if (registrationResp.message == "Username already taken.")
                        {
//                            panelUsername.SetActive(true);
//                            panelPassword.SetActive(false);
                        }
                        else
                        {
                            UIManager.Instance.DisplayMessagePanel(registrationResp.message, null);
                        }
                    });
            }
        }
    }

    public void CallProfileEvent()
    {
        UIManager.Instance.SocketGameManager.GetProfile((socket, packet, args) =>
        {
            Debug.Log("GetProfile  : " + packet.ToString());

            UIManager.Instance.HideLoader();
            JSONArray arr = new JSONArray(packet.ToString());
            string Source;
            Source = arr.getString(arr.length() - 1);
            var resp1 = Source;

            PokerEventResponse<PlayerLoginResponse> resp = JsonUtility.FromJson<PokerEventResponse<PlayerLoginResponse>>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                var profilePic = resp.result.profilePic;
                if (profilePic == -1)
                {
                    _avatarImageContainer.SetActive(true);
                    _avatarImage.gameObject.SetActive(false);

                    UIManager.Instance._avatarUrlSprite.GetUrlSprite(resp.result.profileImage, sprite =>
                        _avatarImageUrl.sprite = sprite);
                }
                else
                {
                    _avatarImageContainer.SetActive(false);
                    _avatarImage.gameObject.SetActive(true);

                    _avatarImage.sprite = UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[profilePic];
                }

                UIManager.Instance.assetOfGame.SavedLoginData.chips = resp.result.chips;
                UIManager.Instance.assetOfGame.SavedLoginData.cash = resp.result.cash;

                string solText = " | "+UIManager.Instance.assetOfGame.SavedLoginData.solBalance + " Sol (US$ " + (float) UIManager.Instance.assetOfGame.SavedLoginData.userUSDBal + ")";

                // 10000 | 8.0 Sol (US$ 108.88)
                
                if (UIManager.Instance.assetOfGame.SavedLoginData.isCash)
                {
                    _ammount = UIManager.Instance.assetOfGame.SavedLoginData.cash;
                    _ammountText.text = _ammount.ToString();
                    _ammountImage.sprite = _cashSprite;
                }
                else
                {
                    _ammount = UIManager.Instance.assetOfGame.SavedLoginData.chips;
                    _ammountText.text = _ammount.ToString();
                    _ammountImage.sprite = _chipsSprite;
                }

                _ammountText.text += solText;
//                UserWalletBalance();
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }
}