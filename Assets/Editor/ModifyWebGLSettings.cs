using UnityEngine;
using System.Collections;
using UnityEditor;

public class ModifyWebGLSettings
{
    [MenuItem("AIS/Modify WebGL Settings/ASM - For iOS 512MB")]
    public static void ModifyWebGLSettingASM512()
    {
        PlayerSettings.WebGL.linkerTarget = WebGLLinkerTarget.Asm;
        PlayerSettings.WebGL.memorySize = 512;
        Debug.Log("WebGL Settings Modified ASM iOS 512MB");
    }

    [MenuItem("AIS/Modify WebGL Settings/ASM - For iOS 256MB")]
    public static void ModifyWebGLSettingASM256()
    {
        PlayerSettings.WebGL.linkerTarget = WebGLLinkerTarget.Asm;
        PlayerSettings.WebGL.memorySize = 256;
        Debug.Log("WebGL Settings Modified ASM iOS 256MB");
    }

    [MenuItem("AIS/Modify WebGL Settings/WASM")]
    public static void ModifyWebGLSettingWASM()
    {
        PlayerSettings.WebGL.linkerTarget = WebGLLinkerTarget.Wasm;
        PlayerSettings.WebGL.memorySize = 512;
        Debug.Log("WebGL Settings Modified");
    }
}
