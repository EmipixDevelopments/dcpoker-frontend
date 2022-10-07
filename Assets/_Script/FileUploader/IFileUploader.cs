
using System;

public interface IFileUploader
{
    void Init();
    void OpenFileBrowser(Action<string> onFileChanged);
}
