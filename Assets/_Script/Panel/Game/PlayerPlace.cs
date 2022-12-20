using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlace : MonoBehaviour
{
    public PokerPlayer pokerPlayer;
    public Button openSeatButton;

    [SerializeField] private PokerPlayer smallPokerPlayer;
    [SerializeField] private PokerPlayer bigPokerPlayer;

    [SerializeField] private FlagsOfCountries _flagsOfCountries;

    public void Init()
    {
        openSeatButton.gameObject.SetActive(false);
        pokerPlayer = smallPokerPlayer;
    }

    public void SetIsBigPlayer(bool isBigPlayer)
    {
        var isNeedActive = false;
        if (pokerPlayer.gameObject.activeSelf)
        {
            pokerPlayer.gameObject.SetActive(false);
            isNeedActive = true;
        }

        pokerPlayer = isBigPlayer ? bigPokerPlayer : smallPokerPlayer;

        if (isNeedActive)
        {
            pokerPlayer.gameObject.SetActive(true);
        }
    }

    public void SetImage(string url)
    {
        UIManager.Instance._avatarUrlSpriteContainer.GetUrlSprite(url, sprite =>
        {
            smallPokerPlayer.SetImage(sprite);
            bigPokerPlayer.SetImage(sprite);
        });
    }

    public void SetFlag(string flag)
    {
        var sprite = _flagsOfCountries.GetSpriteByName(flag);

        smallPokerPlayer.SetFlag(sprite);
        bigPokerPlayer.SetFlag(sprite);
    }

    public void SetAvatar(int avatarId)
    {
        var sprite = UIManager.Instance.assetOfGame.profileAvatarList.profileAvatarSprite[avatarId];

        smallPokerPlayer.SetAvatar(sprite);
        bigPokerPlayer.SetAvatar(sprite);
    }

    public void SetName(string name)
    {
        smallPokerPlayer.txtUsername.text = name;
        bigPokerPlayer.txtUsername.text = name;
    }

    public void SetChip(double chip)
    {
        Debug.LogError("Chip " + chip);
        var chipText = "$ " + chip.ConvertToCommaSeparatedValue();

        smallPokerPlayer.txtChips.text = chipText;
        bigPokerPlayer.txtChips.text = chipText;
    }

    public void SetOpenSeat(Action action)
    {
        openSeatButton.onClick.RemoveAllListeners();
        openSeatButton.onClick.AddListener(action.Invoke);
        //openSeatButton.gameObject.SetActive(true);
    }

    public void SetActiveOpenSeatButton(bool active)
    {
        openSeatButton.gameObject.SetActive(active);
    }

    public void Reset()
    {
    }
}