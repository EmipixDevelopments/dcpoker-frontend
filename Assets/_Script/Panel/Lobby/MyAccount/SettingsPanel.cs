using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES

    //[Header ("Gamobjects")]

    //[Header ("Transforms")]


    //[Header ("ScriptableObjects")]


    //[Header ("DropDowns")]


    [Header("Images")]
    public Image SoundButton;
    public Image bgMusicButton;
    public Image VibrationButton;
    public Image PushNotificationButton;


    [Header("Sprites")]
    public Sprite[] SpriteOnOff;

    //[Header ("Text")]


    //[Header ("Prefabs")]

    //[Header ("Enums")]


    //[Header ("Variables")]

    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    void OnEnable()
    {
        /*Debug.Log ("Sound = > " + PlayerPrefs.GetInt ("Sound"));
		Debug.Log ("Vibration = > " + PlayerPrefs.GetInt ("Vibration"));
		Debug.Log ("PushNotification = > " + PlayerPrefs.GetInt ("PushNotification"));*/

        /*if (PlayerPrefs.GetInt ("Sound") == 1) {
			PlayerPrefs.SetInt ("Sound", 0);

		} else {
			PlayerPrefs.SetInt ("Sound", 1);
		}

		if (PlayerPrefs.GetInt ("Vibration") == 1) {
			PlayerPrefs.SetInt ("Vibration", 0);

		} else {
			PlayerPrefs.SetInt ("Vibration", 1);
		}

		if (PlayerPrefs.GetInt ("PushNotification") == 1) {
			PlayerPrefs.SetInt ("PushNotification", 0);

		} else {
			PlayerPrefs.SetInt ("PushNotification", 1);
		}*/


        bgMusicOnOffBool(PlayerPrefs.GetInt("bgMusic"));
        SoundOnOffBool(PlayerPrefs.GetInt("Sound"));
        VibrationOnOffBool(PlayerPrefs.GetInt("Vibration"));
        PushNotificationOnOffBool(PlayerPrefs.GetInt("PushNotification"));

    }
    void OnDisable()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region DELEGATE_CALLBACKS


    #endregion

    #region PUBLIC_METHODS
    public void gameRuleButtonTap()
    {
        UIManager.Instance.TandCondPopup.SetDataOpen();
    }
    public void bgmusicButtonTap()
    {
        //Debug.Log ("bgMusic = >" + PlayerPrefs.GetInt ("bgMusic"));
        UIManager.Instance.SoundManager.OnButtonClick();
        if (PlayerPrefs.GetInt("bgMusic") == 1)
        {
            PlayerPrefs.SetInt("bgMusic", 0);
            UIManager.Instance.SoundManager.stopBgSound();
        }
        else
        {
            PlayerPrefs.SetInt("bgMusic", 1);
            UIManager.Instance.SoundManager.PlayBgSound();
        }
        bgMusicOnOffBool(PlayerPrefs.GetInt("bgMusic"));
        UIManager.Instance.LobbyScreeen.bgMusicOnOffBool(PlayerPrefs.GetInt("bgMusic"));
        UIManager.Instance.LobbyScreeen.SoundOnOffBool(PlayerPrefs.GetInt("bgMusic"));
    }
    public void SoundButtonTap()
    {
        //Debug.Log ("Sound = >" + PlayerPrefs.GetInt ("Sound"));
        UIManager.Instance.SoundManager.OnButtonClick();
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            PlayerPrefs.SetInt("Sound", 0);

        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        SoundOnOffBool(PlayerPrefs.GetInt("Sound"));
    }
    public void VibrationButtonTap()
    {
        //Debug.Log ("Vibration = >" + PlayerPrefs.GetInt ("Vibration"));
        UIManager.Instance.SoundManager.OnButtonClick();
        if (PlayerPrefs.GetInt("Vibration") == 1)
        {
            PlayerPrefs.SetInt("Vibration", 0);

        }
        else
        {
            PlayerPrefs.SetInt("Vibration", 1);
        }
        VibrationOnOffBool(PlayerPrefs.GetInt("Vibration"));
    }
    public void PushNotificationButtonTap()
    {
        //Debug.Log ("PushNotification = >" + PlayerPrefs.GetInt ("PushNotification"));
        UIManager.Instance.SoundManager.OnButtonClick();
        if (PlayerPrefs.GetInt("PushNotification") == 1)
        {
            PlayerPrefs.SetInt("PushNotification", 0);

        }
        else
        {
            PlayerPrefs.SetInt("PushNotification", 1);
        }
        PushNotificationOnOffBool(PlayerPrefs.GetInt("PushNotification"));
    }

    #endregion

    #region PRIVATE_METHODS
    public void bgMusicOnOffBool(int OnOff)
    {
        bgMusicButton.sprite = SpriteOnOff[OnOff];
    }
    public void SoundOnOffBool(int OnOff)
    {
        SoundButton.sprite = SpriteOnOff[OnOff];
    }

    void VibrationOnOffBool(int OnOff)
    {
        VibrationButton.sprite = SpriteOnOff[OnOff];
    }

    void PushNotificationOnOffBool(int OnOff)
    {
        PushNotificationButton.sprite = SpriteOnOff[OnOff];
    }
    #endregion

    #region COROUTINES



    #endregion


    #region GETTER_SETTER


    #endregion


}
