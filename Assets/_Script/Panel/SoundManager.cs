using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public AudioSource Attention;
    public AudioSource Bet;
    public AudioSource call_raise;
    public AudioSource Card_Dealt;
    public AudioSource Check;
    public AudioSource chips_moved_from_pot;
    public AudioSource chips_moved_to_pot;
    public AudioSource End_Break_Warning;
    public AudioSource Fold;
    public AudioSource newsnd17;
    public AudioSource Raise;
    public AudioSource Allin;
    public AudioSource btnClick;
    public AudioSource bgSound;
    public Transform Parent;
    public List<Transform> allAudios;
    #endregion

    #region PRIVATE_VARIABLES
    #endregion

    #region UNITY_CALLBACKS
    void OnEnable()
    {
        //		PlayerPrefs.DeleteKey ("Sound");
        /*if(PlayerPrefs.GetInt("enableallprefas", 0) == 0)
		{
			DataManager.IsSoundEnabled = true;
			DataManager.IsMusicEnabled = true;

			PlayerPrefs.GetInt ("enableallprefas", 100);
		}*/
        if (!PlayerPrefs.HasKey("bgMusic"))
        {
            PlayerPrefs.SetInt("bgMusic", 1);
        }
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        if (!PlayerPrefs.HasKey("Vibration"))
        {
            PlayerPrefs.SetInt("Vibration", 1);
        }
        if (!PlayerPrefs.HasKey("PushNotification"))
        {
            PlayerPrefs.SetInt("PushNotification", 1);
        }

        for (int i = 0; i < Parent.childCount; i++)
        {

            allAudios.Add(Parent.GetChild(i).transform);
        }

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {


            foreach (Transform Source in allAudios)
            {
                Source.GetComponent<AudioSource>().volume = 1;
            }
            //PlayBackground ();
        }
        else
        {

            foreach (Transform Source in allAudios)
            {
                Source.GetComponent<AudioSource>().volume = 0;
            }
        }

    }



    void Update()
    {

    }
    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Plays the button click sound.
    /// </summary>
    /// 
    public void PlayBgSound()
    {

        if (PlayerPrefs.GetInt("bgMusic", 1) == 1 && !bgSound.isPlaying)
        {
            bgSound.Play();
            bgSound.volume = PlayerPrefs.GetInt("bgMusic", 1);
        }
    }
    public void stopBgSound()
    {
        bgSound.Stop();
    }

    public void OnButtonClick()
    {
        //Debug.Log("call");
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            btnClick.Play();
            btnClick.volume = PlayerPrefs.GetInt("Sound", 1);
        }
    }

    public void AttentionSoundOnce()
    {

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            Attention.Play();
            Attention.volume = PlayerPrefs.GetInt("Sound", 1);
        }
    }

    public void BetSoundOnce()
    {
        //		Debug.Log ("PlayerPrefs.GetInt (\"music_status\") => " + PlayerPrefs.GetInt ("music_status"));
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            Bet.Play();
            Bet.volume = PlayerPrefs.GetInt("Sound", 1);
        }
        /*	else {
                StopLoobySound ();
            }*/

    }
    /*	public void StopLoobySound ()
        {
            Bet.Stop ();
        }*/
    public void Card_DealtClickOnce()
    {

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            Card_Dealt.Play();
            Card_Dealt.volume = PlayerPrefs.GetInt("Sound", 1);
        }
    }
    public void CheckClickOnce()
    {

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            Check.Play();
            Check.volume = PlayerPrefs.GetInt("Sound", 1);
        }
    }
    public void allInClickOnce()
    {

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            Allin.Play();
            Allin.volume = PlayerPrefs.GetInt("Sound", 1);
        }
    }
    /*public void SpeedButtonClickOnce (bool loop)
	{

		if (PlayerPrefs.GetInt ("SfxMusic", 1) == 1) {
			SpeedAudio.loop = loop;
			if(loop)
				SpeedAudio.Play ();
			else
				SpeedAudio.Stop ();
		}
	}*/


    public void chips_moved_from_potClickOnce()
    {

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            chips_moved_from_pot.Play();
            chips_moved_from_pot.volume = PlayerPrefs.GetInt("Sound", 1);
        }
    }
    public void chips_moved_to_potClickOnce()
    {

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            chips_moved_to_pot.Play();
            chips_moved_to_pot.volume = PlayerPrefs.GetInt("Sound", 1);
        }
    }
    public void End_Break_WarningClickOnce()
    {

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            End_Break_Warning.Play();
            End_Break_Warning.volume = PlayerPrefs.GetInt("Sound", 1);
        }
    }
    public void FoldClickOnce()
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            Fold.Play();
            Fold.volume = PlayerPrefs.GetInt("Sound", 1);
        }
    }

    public void CallClickOnce()
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            call_raise.Play();
            call_raise.volume = PlayerPrefs.GetInt("Sound", 1);
        }
    }

    public void newsnd17ClickOnce()
    {

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            newsnd17.Play();
            newsnd17.volume = PlayerPrefs.GetInt("Sound", 1);
        }
    }
    public void RaiseClickOnce()
    {

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            Raise.Play();
            Raise.volume = PlayerPrefs.GetInt("Sound", 1);
        }
    }

    public void Vibrate()
    {
        //Vibration is discomfort
        //Better switch to native vibration
#if UNITY_ANDROID || UNITY_IOS
        if (PlayerPrefs.GetInt("Vibration", 1) == 1)
        {
            Handheld.Vibrate();
        }
#endif
    }
    
    //todo Move PlayerPrefs Logic (all classes) to SaveComponent
    public void SetSoundActive(bool active)
    {
        if (!active)
        {
            PlayerPrefs.SetInt("bgMusic", 0);
            PlayerPrefs.SetInt("Sound", 0);
            UIManager.Instance.SoundManager.stopBgSound();
        }
        else
        {
            PlayerPrefs.SetInt("bgMusic", 1);
            PlayerPrefs.SetInt("Sound", 1);
            UIManager.Instance.SoundManager.PlayBgSound();
        }
    }
    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES
    #endregion
}