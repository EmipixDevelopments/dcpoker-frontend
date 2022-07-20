using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccountInfoInLobby : MonoBehaviour
{
    [SerializeField] private Image _avatarImage;
    [SerializeField] private TextMeshProUGUI _chip;
    [SerializeField] private TextMeshProUGUI _myAccount;

    private int _avatarImageIndex = -1;

    void OnEnable()
    {
        if (UIManager.Instance)
        {
            CallProfileEvent();
        }
    }

    private void Update()
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

            PokerEventResponse<Profile> resp = JsonUtility.FromJson<PokerEventResponse<Profile>>(resp1);

            if (resp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
            {
                _avatarImage.sprite = UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[resp.result.avatar];
                UIManager.Instance.assetOfGame.SavedLoginData.chips = resp.result.chips;
                _chip.text = UIManager.Instance.assetOfGame.SavedLoginData.chips.ToString();

            }
            else
            {
                UIManager.Instance.DisplayMessagePanel(resp.message);
            }
        });
    }
}
