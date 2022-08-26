using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccountInfoInLobby : MonoBehaviour
{
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Image _ammountImage;
    [SerializeField] private TextMeshProUGUI _ammountText;
    [SerializeField] private TextMeshProUGUI _myAccount;
    [Space]
    [SerializeField] private Sprite _cashSprite;
    [SerializeField] private Sprite _chipsSprite;

    private int _avatarImageIndex = -1;
    private double _ammount = 0;

    void OnEnable()
    {
        if (UIManager.Instance)
        {
            CallProfileEvent();
        }
    }

    private void FixedUpdate()
    {
        UpdateAvatarImage();
    }

    private void UpdateAvatarImage()
    {
        if (UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar != _avatarImageIndex)
        {
            _avatarImageIndex = UIManager.Instance.assetOfGame.SavedLoginData.SelectedAvatar;
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
                _avatarImage.sprite = UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[resp.result.profilePic];
                UIManager.Instance.assetOfGame.SavedLoginData.chips = resp.result.chips;
                UIManager.Instance.assetOfGame.SavedLoginData.cash = resp.result.cash;
            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }
}
