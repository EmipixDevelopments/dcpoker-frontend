using System;

[Serializable]
public class TableCustomizationData
{
    public RoomColor RoomColor;
    public TableColor TableColor;
    public CardsColor CardsColor;

    public void Copy(TableCustomizationData tableCustomizationData)
    {
        RoomColor = tableCustomizationData.RoomColor;
        TableColor = tableCustomizationData.TableColor;
        CardsColor = tableCustomizationData.CardsColor;
    }
}
