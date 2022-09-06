using UnityEngine;
using UnityEngine.UI;

public class PanelBottom : MonoBehaviour
{

    [SerializeField] private Button _termsOfServiceButton;
    [SerializeField] private Button _privacyPolicyButton;
    [SerializeField] private Button _responsibleGamingButton;
    [Space]
    [SerializeField] private Image _background;

    public void SetBackgroundAlpha(bool isAlpha)
    {
        _background.enabled = !isAlpha;
    }
}
