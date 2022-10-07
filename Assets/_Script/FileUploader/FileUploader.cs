
using System;

// It's for select file on pc in editor and webGl
// Add extension logic (if need)
public class FileUploader
{
    private IFileUploader _fileUploader;

    public FileUploader()
    {
        #if UNITY_EDITOR
            _fileUploader = new EditorFileUploader();
        #elif UNITY_WEBGL
            _fileUploader = new WebGlFileUploader();
        #endif
        
        _fileUploader?.Init();
    }

    //return on action base64 string data image
    public void OpenFileBrowser(Action<string> onFileChanged) =>
        _fileUploader?.OpenFileBrowser(onFileChanged);
}
