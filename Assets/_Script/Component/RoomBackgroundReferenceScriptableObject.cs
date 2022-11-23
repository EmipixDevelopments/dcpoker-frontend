using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "room_backgrounds_reference", menuName = "ScriptableObjects/Room Reference/Background")]
public class RoomBackgroundReferenceScriptableObject : ScriptableObject
{
    [Header("Color 0 - Blue 1 - Dark Blue 2 - Green 3 - Red 4 - Gold")]
    public List<Color> Colors;
    [Header("Pattern 0 - Blue 1 - Dark Blue 2 - Green 3 - Red 4 - Gold")]
    public List<Sprite> BackgroundsImages;
}

public enum RoomColor
{
    Blue,
    DarkBlue,
    Green,
    Red,
    Gold,
    PatternBlue,
    PatternDarkBlue,
    PatternGreen,
    PatternRed,
    PatternGold
}
