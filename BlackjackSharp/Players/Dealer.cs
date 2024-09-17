using BlackjackSharp.Enums;
using BlackjackSharp.Types;

namespace BlackjackSharp.Players;

public class Dealer : IBlackjackPlayer
{
	// In this scenario, the player is the dealer and dealerHandValue is unused
	public PlayerAction GetPlayerAction(Hand playerHand, Hand dealerHand, bool surrenderable = false)
	{
		if (playerHand.Evaluate() < 17) return PlayerAction.Hit;
		else return PlayerAction.Stand;
	}
}
