using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "room_table_reference", menuName = "ScriptableObjects/Room Reference/Table")]
public class RoomTableReferenceScriptableObject : ScriptableObject
{
    [Header("Table 0 - Blue 1 - Dark Blue 2 - Green 3 - Red 4 - Black")]
    public List<Sprite> TableImages;
}

public enum TableColor
{
    Blue,
    DarkBlue,
    Green,
    Red,
    Black
}
