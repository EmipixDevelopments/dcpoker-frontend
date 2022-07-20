using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelProfileNew : MonoBehaviour
{
    [SerializeField] private Image _avatarImage;

    private int _avatarImageIndex = -1;

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


}
