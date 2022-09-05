using UnityEngine;

[CreateAssetMenu(fileName = "table_list_colors", menuName = "ScriptableObjects/TableListColors")]
public class TableListColors : ScriptableObject
{
    [SerializeField]private Color Cyan;
    [SerializeField]private Color Clear;
    [SerializeField]private Color Grey;
    [SerializeField]private Color Gray;
    [SerializeField]private Color Magenta;
    [SerializeField]private Color Red;
    [SerializeField]private Color Yellow;
    [SerializeField]private Color Black;
    [SerializeField]private Color White;
    [SerializeField]private Color Green;
    [SerializeField]private Color Blue;
    [Space]
    [SerializeField]private Color None;
    
    public Color GetColorByName(string color)
    {
        switch (color)
        {
            case null: return None;
            case "cyan": return Clear;
            case "clear": return Grey;
            case "grey": return Grey;
            case "gray": return Gray;
            case "magenta": return Magenta;
            case "red": return Red;
            case "yellow": return Yellow;
            case "black": return Black;
            case "white": return White;
            case "green": return Green;
            case "blue": return Blue;
            default: return None;
        }
    }
}
