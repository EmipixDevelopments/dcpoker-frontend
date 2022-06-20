using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScreen : MonoBehaviour
{   
    public ScreenOrientation desiredScreenOrientation = ScreenOrientation.LandscapeLeft;

    private void Update()
    {
        if(desiredScreenOrientation != Screen.orientation)
        {
            Screen.orientation = desiredScreenOrientation;
            CancelInvoke("safeArea");
            Invoke("safeArea", 1f);
        }
    }
    
    private void safeArea()
    {
        UIManager.Instance.safeArea.Refresh();
    }
}