using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/" + Application.productName + "SavedData.dat"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/" + Application.productName + "SavedData.dat", FileMode.Open);
            LocalSaveData game = new LocalSaveData();
            game.Username = LocalSaveData.current.Username;
            game.password = LocalSaveData.current.password;
            game.phoneCode = LocalSaveData.current.phoneCode;
            game.phoneNumber = LocalSaveData.current.phoneNumber;
            game.isRememberMe = LocalSaveData.current.isRememberMe;
            bf.Serialize(file, game);
            file.Close();
        }
        PlayerPrefs.SetString("USERNAME", LocalSaveData.current.Username);
        PlayerPrefs.SetString("PASSWORD", LocalSaveData.current.password);
        PlayerPrefs.GetString("PHONECODE", LocalSaveData.current.phoneCode);
        PlayerPrefs.GetString("PHONENUMBER", LocalSaveData.current.phoneNumber);
        PlayerPrefs.SetInt("REMEMBER_ME", LocalSaveData.current.isRememberMe == true ? 1 : 0);
    }

    public static void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/" + Application.productName + "SavedData.dat"))
        {
            //Debug.Log("if => " + PlayerPrefs.GetString("USERNAME"));
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + Application.productName + "SavedData.dat", FileMode.Open);
            LocalSaveData game = (LocalSaveData)bf.Deserialize(file);
            file.Close();
            LocalSaveData.current.Username = game.Username;
            LocalSaveData.current.password = game.password;
            LocalSaveData.current.phoneCode = game.phoneCode;
            LocalSaveData.current.phoneNumber = game.phoneNumber;
            LocalSaveData.current.isRememberMe = game.isRememberMe;

            UIManager.Instance.assetOfGame.SavedLoginData.Username = PlayerPrefs.GetString("USERNAME"); //LocalSaveData.current.Username;
            UIManager.Instance.assetOfGame.SavedLoginData.password = PlayerPrefs.GetString("PASSWORD");//game.password;
            UIManager.Instance.assetOfGame.SavedLoginData.phoneCode = PlayerPrefs.GetString("PHONECODE");
            UIManager.Instance.assetOfGame.SavedLoginData.phoneNumber = PlayerPrefs.GetString("PHONENUMBER");
            UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe = PlayerPrefs.GetInt("REMEMBER_ME", 0) == 1 ? true : false;//game.isRememberMe;
            //Debug.Log("if PASSWORD => " + PlayerPrefs.GetString("PASSWORD"));
            //Debug.Log("if REMEMBER_ME => " + PlayerPrefs.GetString("REMEMBER_ME"));
        }
        else
        {
            Debug.Log("File Does Not Exist");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + Application.productName + "SavedData.dat");
            LocalSaveData game = new LocalSaveData();
            game.Username = LocalSaveData.current.Username;
            game.password = LocalSaveData.current.password;
            game.phoneCode = LocalSaveData.current.phoneCode;
            game.phoneNumber = LocalSaveData.current.phoneNumber;
            game.isRememberMe = LocalSaveData.current.isRememberMe;

            bf.Serialize(file, game);
            Debug.Log("Create file from asset of data" + game.Username);
        }
        UIManager.Instance.assetOfGame.SavedLoginData.Username = PlayerPrefs.GetString("USERNAME", "");
        UIManager.Instance.assetOfGame.SavedLoginData.password = PlayerPrefs.GetString("PASSWORD", "");
        UIManager.Instance.assetOfGame.SavedLoginData.phoneCode = PlayerPrefs.GetString("PHONECODE", "");
        UIManager.Instance.assetOfGame.SavedLoginData.phoneNumber = PlayerPrefs.GetString("PHONENUMBER", "");
        UIManager.Instance.assetOfGame.SavedLoginData.isRememberMe = PlayerPrefs.GetInt("REMEMBER_ME", 0) == 1 ? true : false;
    }
    public static void DeleteFile()
    {
        PlayerPrefs.SetInt("PokerBetAutoLogin", 0);
        PlayerPrefs.SetString("USERNAME", "");
        PlayerPrefs.SetString("PASSWORD", "");
        PlayerPrefs.SetString("PHONECODE", "");
        PlayerPrefs.SetString("PHONENUMBER", "");
        PlayerPrefs.DeleteKey("REMEMBER_ME");

        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + Application.productName + "SavedData.dat";

        if (File.Exists(Application.persistentDataPath + "/" + Application.productName + "SavedData.dat"))
        {
            File.Delete(path);
        }
    }
}