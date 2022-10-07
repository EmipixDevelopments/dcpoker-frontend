#if UNITY_WEBGL && !UNITY_EDITOR

using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WebGlFileUploader : IFileUploader
{
    [DllImport("__Internal")]
    private static extern void SelectFile(string gameObjectName, string methodName, string filter);

    private FileUploaderHelper _fileUploaderHelper;
    
    public void Init()
    {
        _fileUploaderHelper = new GameObject("file_browser_web_gl").AddComponent<FileUploaderHelper>();
    }

    public void OpenFileBrowser(Action<string> onFileChanged)
    {
        _fileUploaderHelper.SetAction(onFileChanged);
        SelectFile(_fileUploaderHelper.gameObject.name, "FileCallback", ".png, .jpg, .jpeg");
    }
}

public class FileUploaderHelper : MonoBehaviour
{
    private Action<string> _action;

    public void SetAction(Action<string> action)
    {
        _action = action;
    }

    public void FileCallback(string path)
    {
        if(string.IsNullOrEmpty(path))
        {
            return;
        }
        
        var base64 = path.Substring(path.IndexOf(',') + 1);
        
        _action?.Invoke(base64);
    }
}

#endif