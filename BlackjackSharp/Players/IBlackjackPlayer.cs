using BlackjackLearnerVisualizer.Blackjack.Enums;

namespace BlackjackSharp;

public interface IBlackjackPlayer
{
    public PlayerAction GetPlayerAction(Hand playerHand, Hand dealerHand, bool surrenderable = false);
}