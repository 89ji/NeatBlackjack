using System;
using System.Collections.Generic;
using BlackjackLearnerVisualizer.Blackjack.Enums;
using BlackjackLearnerVisualizer.Blackjack.Types;

public class Hand
{
    private List<Card> cards = new();
    public void AddCard(Card card) => cards.Add(card);
    public bool Splittable => cards.Count == 2 && cards[0].rank == cards[1].rank;
    public bool SoftHand = false;
    public int Evaluate()
    {
        int sum = 0;
        int aces = 0;
        foreach (Card card in cards)
        {
            sum += card.rank switch
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
            if (card.rank == Rank.Ace) aces++;
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

    public List<Card> GetCards() => cards;
    public void Reset()
    {
        cards.Clear();
    }
}