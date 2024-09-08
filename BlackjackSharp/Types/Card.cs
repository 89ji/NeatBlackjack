using System;
using BlackjackLearnerVisualizer.Blackjack.Enums;

namespace BlackjackLearnerVisualizer.Blackjack.Types;

public class Card
{
    private static Random rng = new Random();
    public Suit suit { get; set; }
    public Rank rank { get; set; }
    
    public static Card GetRandomCard()
    {
        Card ret = new();
        ret.suit = (Suit)rng.Next(0, 4);
        ret.rank = (Rank)rng.Next(0, 13);
        return ret;
    }

    public string GetName()
    {
        return $"the {Enum.GetName(rank.GetType(), rank)} of {Enum.GetName(suit.GetType(), suit)}";
    }

    public override string ToString()
    {
        return GetName();
    }
}