namespace BlackjackSharp;

public class Hand
{
    private List<Card> cards = new();
    public void AddCard(Card card) => cards.Add(card);
    public bool Splittable => cards.Count == 2 && cards[0].rank == cards[1].rank; 
    public int Evaluate()
    {
        int sum = 0;
        int aces = 0;
        foreach (Card card in cards)
        {
            sum += card.rank switch
            {
                Card.Rank.Ace => 0,
                Card.Rank.Two => 2,
                Card.Rank.Three => 3,
                Card.Rank.Four => 4,
                Card.Rank.Five => 5,
                Card.Rank.Six => 6,
                Card.Rank.Seven => 7,
                Card.Rank.Eight => 8,
                Card.Rank.Nine => 9,
                Card.Rank.Ten => 10,
                Card.Rank.Jack => 10,
                Card.Rank.Queen => 10,
                Card.Rank.King => 10,
                _ => throw new ArgumentOutOfRangeException()
            };
            if (card.rank == Card.Rank.Ace) aces++;
        }

        for (int i = 0; i < aces; i++)
        {
            if (sum + 11 > 21) sum++;
            else sum += 11;
        }

        return sum;
    }

    public List<Card> GetCards() => cards;
    public void Reset()
    {
        cards.Clear();
    }
}