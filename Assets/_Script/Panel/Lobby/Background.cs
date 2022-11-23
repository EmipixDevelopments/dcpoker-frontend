using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private Image _chipsBottom;
    [SerializeField] private Image _backgroundPanel;

    public void SetActiveChipsBottomImage(bool active)
    {
        _chipsBottom.enabled = active;
    }
    
    public void SetActiveBackgroundPanel(bool active)
    {
        _backgroundPanel.enabled = active;
    }
}
