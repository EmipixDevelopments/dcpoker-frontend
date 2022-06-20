using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{


    public int index = 1;

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot(Utility.Instance.GetApplicationVersion() + index + ".png");
            print(Utility.Instance.GetApplicationVersion() + index + ".png");
        }
    }
#endif
}
