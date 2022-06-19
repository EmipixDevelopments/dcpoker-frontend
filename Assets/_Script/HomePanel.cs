using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomePanel : MonoBehaviour
{

    public InputField inptSocketURL;

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        inptSocketURL.text = PlayerPrefs.GetString("CUSTOM_URL", "");
    }

    public void SumbitButtonTap()
    {
        PlayerPrefs.SetString("CUSTOM_URL", inptSocketURL.text);
        //Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
