
public class RoomData
{
    private RoomsListing.Room _room;

    private RoomGameType _roomGameType;
    private RoomSeedType _roomSeedType;

    public RoomsListing.Room GetRoom() => _room;
    public RoomGameType GetRoomType() => _roomGameType;
    public RoomSeedType GetRoomSeedType() => _roomSeedType;

    public void SetRoom(RoomsListing.Room room)
    {
        _room = room;
        UpdateGameType();
    }

    private void UpdateGameType()
    {
        if (_room.isTournament)
        {
            if (_room.namespaceString.Contains("sng") || _room.namespaceString.Contains("SNG"))
            {
                _roomGameType = RoomGameType.Sng;
                return;
            }
            _roomGameType = RoomGameType.Touranment;
            return;
        }
        
        if (_room.pokerGameType == PokerGameType.texas.ToString())
        {
            _roomGameType = RoomGameType.Texas;
            return;
        }
        if (_room.pokerGameType == PokerGameType.omaha.ToString())
        {
            _roomGameType = RoomGameType.Omaha;
            return;
        }
        if (_room.pokerGameType == PokerGameType.PLO5.ToString())
        {
            _roomGameType = RoomGameType.PLO5;
            return;
        }

        _roomGameType = RoomGameType.None;
        
    }
}

public enum RoomGameType
{
    Sng,
    Touranment,
    Texas,
    Omaha,
    PLO5,
    None
}

public enum RoomSeedType
{
    Cash,
    Chips
}
