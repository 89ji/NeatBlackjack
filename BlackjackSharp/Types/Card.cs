using BlackjackSharp.Enums;

namespace BlackjackSharp.Types;


// Initialization of card objects are now defunct, use enums as cards instead via GetRandomCard()
public class Card
{
    private static Random rng = new Random();
    public Suit suit { get; set; }
    public Rank rank { get; set; }
    
    public static Rank GetRandomCard()
    {
        //Card ret = new();
        //ret.suit = (Suit)rng.Next(0, 4);
        return (Rank)rng.Next(0, 13);
        //return ret;
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