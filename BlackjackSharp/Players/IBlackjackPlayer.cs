using BlackjackSharp.Enums;
using BlackjackSharp.Types;

namespace BlackjackSharp.Players;

public interface IBlackjackPlayer
{
    public PlayerAction GetPlayerAction(Hand playerHand, Hand dealerHand, bool surrenderable = false);
}