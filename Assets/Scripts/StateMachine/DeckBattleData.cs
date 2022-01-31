using System.Collections.Generic;

public class DeckBattleData
{
    public readonly List<Coin> Deck;
    public readonly List<Coin> Discarded = new List<Coin>();
    public readonly List<Coin> Used = new List<Coin>();
    public readonly List<Coin> Hand = new List<Coin>();

    public DeckBattleData(List<Coin> coins)
    {
        Deck = new List<Coin>(coins);
        Deck.Shuffle();
    }

    public Coin TakeNextDeckCard()
    {
        if (Deck.Count == 0 && Discarded.Count > 0)
        {
            Deck.AddRange(Discarded);
            Deck.Shuffle();
            Discarded.Clear();
        }
        return Deck.Take(Deck.Count - 1);
    }
    
    public bool IsDeckEmpty() => Deck.Count == 0;

    public bool IsDiscardEmpty() => Discarded.Count == 0;


}