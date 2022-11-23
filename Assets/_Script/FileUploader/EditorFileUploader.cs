#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;

public class EditorFileUploader : IFileUploader
{
    private readonly string[] _filter = {"Image files", "png,jpg,jpeg"};
    public void Init() { }

    public void OpenFileBrowser(Action<string> onFileChanged)
    {
        var path = EditorUtility.OpenFilePanelWithFilters("Change File", "", _filter);
        
        if (string.IsNullOrEmpty(path))
        {
            return;
        }
        
        byte[] img = File.ReadAllBytes(path);
        onFileChanged?.Invoke(Convert.ToBase64String(img));
    }
}

#endif
