using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "room_cards_reference", menuName = "ScriptableObjects/Room Reference/Cards")]
public class RoomCardsReferenceScriptableObject : ScriptableObject
{
    [Header("Table 0 - Blue 1 - Green 2 - Red 3 - Black")]
    public List<Sprite> CardsImages;
}

public enum CardsColor
{
    Blue,
    Green,
    Red,
    Black
}
