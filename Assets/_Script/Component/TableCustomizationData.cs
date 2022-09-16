using System;

[Serializable]
public class TableCustomizationData
{
    public RoomColor RoomColor;
    public TableColor TableColor;
    public CardsColor CardsColor;
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
    PatternRed,
    PatternGreen,
    PatternGold
}

public enum TableColor
{
    Blue,
    DarkBlue,
    Green,
    Red,
    Black
}

public enum CardsColor
{
    Blue,
    DarkBlue,
    Green,
    Red
}
