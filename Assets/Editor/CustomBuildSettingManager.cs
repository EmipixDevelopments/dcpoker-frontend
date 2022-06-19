using UnityEngine;
using System.Collections;
using UnityEditor;

public class CustomBuildSettingManager : MonoBehaviour
{
#if UNITY_ANDROID
    [MenuItem("AIS/Build Settings/Live Build")]
    public static void LiveServerAndroid()
    {
        //Set server
        UIManager uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uimanager.server = SERVER.Macau;
        uimanager.isLogAllEnabled = false;

        //Disable custom url scene
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        scenes[0].enabled = false;
        scenes[1].enabled = true;
        EditorBuildSettings.scenes = scenes;

        //Disable splash screen
        PlayerSettings.SplashScreen.show = false;

        //Set IL2CPP as scripting backend        
        PlayerSettings.SetScriptingBackend(EditorUserBuildSettings.selectedBuildTargetGroup, ScriptingImplementation.IL2CPP);

        //Set architecture
        AndroidArchitecture aac = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
        PlayerSettings.Android.targetArchitectures = aac;

        //Build app bundle (google play) .abb file
        EditorUserBuildSettings.buildAppBundle = true;

        //Keystore & alias password
        PlayerSettings.Android.useCustomKeystore = true;
        PlayerSettings.keystorePass = "password";
        PlayerSettings.keyaliasPass = "password";

        //ERRORS
        if (PlayerSettings.SplashScreen.show)
        {
            EditorUtility.DisplayDialog("Warning", "Splash screen is active", "Ok");
        }

        string pupupMessage = "";
        pupupMessage = "v" + PlayerSettings.bundleVersion + " (" + PlayerSettings.Android.bundleVersionCode + ")";
        pupupMessage += "\nAndroid live build setting configured";
        EditorUtility.DisplayDialog("Alert", pupupMessage, "Ok");
    }

    [MenuItem("AIS/Build Settings/Staging Build")]
    public static void StagingServerAndroid()
    {
        //Set server
        UIManager uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uimanager.server = SERVER.Developer;
        uimanager.isLogAllEnabled = false;

        //Disable custom url scene
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        scenes[0].enabled = false;
        scenes[1].enabled = true;
        EditorBuildSettings.scenes = scenes;

        //Disable splash screen
        PlayerSettings.SplashScreen.show = false;

        //Set IL2CPP as scripting backend        
        PlayerSettings.SetScriptingBackend(EditorUserBuildSettings.selectedBuildTargetGroup, ScriptingImplementation.Mono2x);

        //Set architecture
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;

        //Build app bundle (google play) .abb file
        EditorUserBuildSettings.buildAppBundle = false;

        //Keystore & alias password
        PlayerSettings.Android.useCustomKeystore = false;
        PlayerSettings.keystorePass = "";
        PlayerSettings.keyaliasPass = "";

        //ERRORS
        if (PlayerSettings.SplashScreen.show)
        {
            EditorUtility.DisplayDialog("Warning", "Splash screen is active", "Ok");
        }

        string pupupMessage = "";
        pupupMessage = "v" + PlayerSettings.bundleVersion + " (" + PlayerSettings.Android.bundleVersionCode + ")";
        pupupMessage += "\nAndroid staging build setting configured";
        EditorUtility.DisplayDialog("Alert", pupupMessage, "Ok");
    }

    [MenuItem("AIS/Build Settings/Custom Build")]
    public static void CustomServerAndroid()
    {
        //Set server
        UIManager uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uimanager.server = SERVER.Custom;
        uimanager.isLogAllEnabled = true;

        //Disable custom url scene
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        scenes[0].enabled = true;
        scenes[1].enabled = true;
        EditorBuildSettings.scenes = scenes;

        //Disable splash screen
        PlayerSettings.SplashScreen.show = false;

        //Set IL2CPP as scripting backend        
        PlayerSettings.SetScriptingBackend(EditorUserBuildSettings.selectedBuildTargetGroup, ScriptingImplementation.Mono2x);

        //Set architecture        
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;

        //Build app bundle (google play) .abb file
        EditorUserBuildSettings.buildAppBundle = false;

        //Keystore & alias password        
        PlayerSettings.Android.useCustomKeystore = false;
        PlayerSettings.keystorePass = "";
        PlayerSettings.keyaliasPass = "";

        //ERRORS
        if (PlayerSettings.SplashScreen.show)
        {
            EditorUtility.DisplayDialog("Warning", "Splash screen is active", "Ok");
        }

        string pupupMessage = "";
        pupupMessage = "v" + PlayerSettings.bundleVersion + " (" + PlayerSettings.Android.bundleVersionCode + ")";
        pupupMessage += "\nAndroid custom build setting configured";
        EditorUtility.DisplayDialog("Alert", pupupMessage, "Ok");
    }

#elif UNITY_WEBGL
    [MenuItem("AIS/Build Settings/Macau Build")]
    public static void LiveServerWebGL()
    {
        //Set server
        UIManager uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uimanager.server = SERVER.Macau;
        uimanager.isLogAllEnabled = false;
        uimanager.isWebGLAffiliateBuild = true;

        //Disable custom url scene
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        scenes[0].enabled = false;
        scenes[1].enabled = true;
        EditorBuildSettings.scenes = scenes;

        //Disable splash screen
        PlayerSettings.SplashScreen.show = false;

        //ERRORS
        if (PlayerSettings.SplashScreen.show)
        {
            EditorUtility.DisplayDialog("Warning", "Splash screen is active", "Ok");
        }

        string pupupMessage = "";
        pupupMessage += "WebGL live build setting configured";
        EditorUtility.DisplayDialog("Alert", pupupMessage, "Ok");
    }

    [MenuItem("AIS/Build Settings/Staging Build")]
    public static void StagingServerWebGL()
    {
        //Set server
        UIManager uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uimanager.server = SERVER.Club;
        uimanager.isLogAllEnabled = false;
        uimanager.isWebGLAffiliateBuild = true;

        //Disable custom url scene
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        scenes[0].enabled = false;
        scenes[1].enabled = true;
        EditorBuildSettings.scenes = scenes;

        //Disable splash screen
        PlayerSettings.SplashScreen.show = false;

        //ERRORS
        if (PlayerSettings.SplashScreen.show)
        {
            EditorUtility.DisplayDialog("Warning", "Splash screen is active", "Ok");
        }

        string pupupMessage = "";
        pupupMessage += "WebGL staging build setting configured";
        EditorUtility.DisplayDialog("Alert", pupupMessage, "Ok");
    }

    [MenuItem("AIS/Build Settings/Custom Build")]
    public static void CustomServerWebGL()
    {
        //Set server
        UIManager uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uimanager.server = SERVER.Custom;
        uimanager.isLogAllEnabled = false;
        uimanager.isWebGLAffiliateBuild = false;

        //Disable custom url scene
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        scenes[0].enabled = true;
        scenes[1].enabled = true;
        EditorBuildSettings.scenes = scenes;

        //Disable splash screen
        PlayerSettings.SplashScreen.show = false;

        //ERRORS
        if (PlayerSettings.SplashScreen.show)
        {
            EditorUtility.DisplayDialog("Warning", "Splash screen is active", "Ok");
        }

        string pupupMessage = "";
        pupupMessage += "WebGL custom build setting configured";
        EditorUtility.DisplayDialog("Alert", pupupMessage, "Ok");
    }
#elif UNITY_IOS
    [MenuItem("AIS/Build Settings/Live Build")]
    public static void LiveServerIOS()
    {
        //Set server
        UIManager uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uimanager.server = SERVER.Macau;
        uimanager.isLogAllEnabled = false;

        //Disable custom url scene
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        scenes[0].enabled = false;
        scenes[1].enabled = true;
        EditorBuildSettings.scenes = scenes;

        //Disable splash screen
        PlayerSettings.SplashScreen.show = false;

        //ERRORS
        if (PlayerSettings.SplashScreen.show)
        {
            EditorUtility.DisplayDialog("Warning", "Splash screen is active", "Ok");
        }

        string pupupMessage = "";
        pupupMessage += "iOS live build setting configured";
        EditorUtility.DisplayDialog("Alert", pupupMessage, "Ok");
    }

    [MenuItem("AIS/Build Settings/Staging Build")]
    public static void StagingServerIOS()
    {
        //Set server
        UIManager uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uimanager.server = SERVER.Developer;
        uimanager.isLogAllEnabled = false;

        //Disable custom url scene
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        scenes[0].enabled = false;
        scenes[1].enabled = true;
        EditorBuildSettings.scenes = scenes;

        //Disable splash screen
        PlayerSettings.SplashScreen.show = false;

        //ERRORS
        if (PlayerSettings.SplashScreen.show)
        {
            EditorUtility.DisplayDialog("Warning", "Splash screen is active", "Ok");
        }

        string pupupMessage = "";
        pupupMessage += "iOS staging build setting configured";
        EditorUtility.DisplayDialog("Alert", pupupMessage, "Ok");
    }

    [MenuItem("AIS/Build Settings/Custom Build")]
    public static void CustomServerIOS()
    {
        //Set server
        UIManager uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uimanager.server = SERVER.Custom;
        uimanager.isLogAllEnabled = false;

        //Disable custom url scene
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        scenes[0].enabled = true;
        scenes[1].enabled = true;
        EditorBuildSettings.scenes = scenes;

        //Disable splash screen
        PlayerSettings.SplashScreen.show = false;

        //ERRORS
        if (PlayerSettings.SplashScreen.show)
        {
            EditorUtility.DisplayDialog("Warning", "Splash screen is active", "Ok");
        }

        string pupupMessage = "";
        pupupMessage += "iOS custom build setting configured";
        EditorUtility.DisplayDialog("Alert", pupupMessage, "Ok");
    }
#else
    [MenuItem("AIS/Build Settings/Live Build")]
    public static void LiveServerOther()
    {
        //Set server
        UIManager uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uimanager.server = SERVER.Macau;
        uimanager.isLogAllEnabled = false;

        //Disable custom url scene
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        scenes[0].enabled = false;
        scenes[1].enabled = true;
        EditorBuildSettings.scenes = scenes;

        //Disable splash screen
        PlayerSettings.SplashScreen.show = false;

        //ERRORS
        if (PlayerSettings.SplashScreen.show)
        {
            EditorUtility.DisplayDialog("Warning", "Splash screen is active", "Ok");
        }

        string pupupMessage = "";
        pupupMessage += EditorUserBuildSettings.activeBuildTarget.ToString() + " live build setting configured";
        EditorUtility.DisplayDialog("Alert", pupupMessage, "Ok");
    }

    [MenuItem("AIS/Build Settings/Staging Build")]
    public static void StagingServerOther()
    {
        //Set server
        UIManager uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uimanager.server = SERVER.Developer;
        uimanager.isLogAllEnabled = false;

        //Disable custom url scene
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        scenes[0].enabled = false;
        scenes[1].enabled = true;
        EditorBuildSettings.scenes = scenes;

        //Disable splash screen
        PlayerSettings.SplashScreen.show = false;

        //ERRORS
        if (PlayerSettings.SplashScreen.show)
        {
            EditorUtility.DisplayDialog("Warning", "Splash screen is active", "Ok");
        }

        string pupupMessage = "";
        pupupMessage += EditorUserBuildSettings.activeBuildTarget.ToString() + " staging build setting configured";
        EditorUtility.DisplayDialog("Alert", pupupMessage, "Ok");
    }

    [MenuItem("AIS/Build Settings/Custom Build")]
    public static void CustomServerOther()
    {
        //Set server
        UIManager uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uimanager.server = SERVER.Custom;
        uimanager.isLogAllEnabled = false;

        //Disable custom url scene
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        scenes[0].enabled = true;
        scenes[1].enabled = true;
        EditorBuildSettings.scenes = scenes;

        //Disable splash screen
        PlayerSettings.SplashScreen.show = false;

        //ERRORS
        if (PlayerSettings.SplashScreen.show)
        {
            EditorUtility.DisplayDialog("Warning", "Splash screen is active", "Ok");
        }

        string pupupMessage = "";
        pupupMessage += EditorUserBuildSettings.activeBuildTarget.ToString() + " custom build setting configured";
        EditorUtility.DisplayDialog("Alert", pupupMessage, "Ok");
    }
#endif
}


