using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "room_cards_reference", menuName = "ScriptableObjects/Room Reference/Cards")]
public class RoomCardsReferenceScriptableObject : ScriptableObject
{
    [Header("Table 0 - Blue 1 - Green 2 - Red 3 - Black")]
    public List<Sprite> CardsBackgroundImages;
    
    [Header("Table 0 - Heart 1 - Spade 2 - Diamond 3 - Club")]
    [SerializeField] private List<Sprite> CardSuitsImages;

    private Dictionary<string, Sprite> _cardSuits;

    private void Init()
    {
        _cardSuits = new Dictionary<string, Sprite>();
        
        _cardSuits.Add("H", CardSuitsImages[0]);
        _cardSuits.Add("S", CardSuitsImages[1]);
        _cardSuits.Add("D", CardSuitsImages[2]);
        _cardSuits.Add("C", CardSuitsImages[3]);
    }
    
    public void GetCardInfo(string card, ref CardInfo cardInfo)
    {
        //if (card == "BC")
          //  return UIManager.Instance.assetOfGame.PokerCards.BackCard;

        string rank = card.Substring(0, 1);
        string suit = card.Substring(1, 1);
        
        cardInfo.CardSuit = _cardSuits[suit];
        
        
    }
}

public class CardInfo
{
    public Sprite CardSuit;
    public string CardText;
}

public enum CardsColor
{
    Blue,
    Green,
    Red,
    Black
}
