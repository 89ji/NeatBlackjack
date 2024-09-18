using BlackjackSharp.Enums;

namespace BlackjackSharp.Types;
public class Hand
{
    private List<Rank> cards = new();
    public void AddCard(Rank card) => cards.Add(card);
    public bool Splittable => cards.Count == 2 && cards[0] == cards[1];
    public bool SoftHand = false;
    public int Evaluate()
    {
        int sum = 0;
        int aces = 0;
        foreach (Rank card in cards)
        {
            sum += card switch
            {
                Rank.Ace => 0,
                Rank.Two => 2,
                Rank.Three => 3,
                Rank.Four => 4,
                Rank.Five => 5,
                Rank.Six => 6,
                Rank.Seven => 7,
                Rank.Eight => 8,
                Rank.Nine => 9,
                Rank.Ten => 10,
                Rank.Jack => 10,
                Rank.Queen => 10,
                Rank.King => 10,
                _ => throw new ArgumentOutOfRangeException()
            };
            if (card == Rank.Ace) aces++;
        }

        SoftHand = false;
        for (int i = 0; i < aces; i++)
        {
            if (sum + 11 > 21) sum++;
            else
            {
                sum += 11;
                SoftHand = true;
            }
        }

        return sum;
    }

    public List<Rank> GetCards() => cards;
    public void Reset()
    {
        cards.Clear();
    }
}