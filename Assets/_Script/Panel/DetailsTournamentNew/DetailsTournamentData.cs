
using System;

public class DetailsTournamentData
{
    public IHighlightTableElement HighlightTableElement;
    public getTournamentInfoData TournamentInfoData;
    public string TournamentId;

    public TournamentButtonState TournamentButtonState;
    public Action ButtonAction;
}

public enum TournamentButtonState
{
    None,
    Register,
    Unregister,
    LateRegister,
    Open
}
