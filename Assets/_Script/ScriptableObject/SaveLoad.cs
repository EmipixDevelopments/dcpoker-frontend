using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static void SaveGame()
    {
        PlayerPrefs.SetString("USERNAME", UIManager.Instance.assetOfGame.SavedLoginData.Username);
        PlayerPrefs.SetString("PASSWORD", UIManager.Instance.assetOfGame.SavedLoginData.password);
        PlayerPrefs.SetString("PHONECODE", UIManager.Instance.assetOfGame.SavedLoginData.phoneCode);
        PlayerPrefs.SetString("PHONENUMBER", UIManager.Instance.assetOfGame.SavedLoginData.phoneNumber);
        PlayerPrefs.SetInt("REMEMBER_ME", UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe == true ? 1 : 0);
    }

    public static void LoadGame()
    {
        UIManager.Instance.assetOfGame.SavedLoginData.Username = PlayerPrefs.GetString("USERNAME", "");
        UIManager.Instance.assetOfGame.SavedLoginData.password = PlayerPrefs.GetString("PASSWORD", "");
        UIManager.Instance.assetOfGame.SavedLoginData.phoneCode = PlayerPrefs.GetString("PHONECODE", "");
        UIManager.Instance.assetOfGame.SavedLoginData.phoneNumber = PlayerPrefs.GetString("PHONENUMBER", "");
        UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe = PlayerPrefs.GetInt("REMEMBER_ME", 0) == 1 ? true : false;
    }
}