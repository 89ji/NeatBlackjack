using BlackjackSharp.Enums;
using BlackjackSharp.Players;
using BlackjackSharp.Types;
using SharpNeat;

namespace BlackjackSharp.NEAT;

public class NeatPlayer : IBlackjackPlayer
{
	IBlackBox<double> box;
	
	public NeatPlayer(IBlackBox<double> box)
	{
		this.box = box;
	}

	public PlayerAction GetPlayerAction(Hand playerHand, Hand dealerHand, bool surrenderable = false)
	{
		box.Reset();
		var inMem = box.Inputs.Span;
		inMem[0] = playerHand.Evaluate();
		inMem[1] = dealerHand.Evaluate();
		inMem[2] = (playerHand.SoftHand) ? 1 : 0;
		
		box.Activate();

		var outMem = box.Outputs.Span;
		if (outMem[0] >= outMem[1])
		{
			return PlayerAction.Hit;
		}
		else
		{
			return PlayerAction.Stand;
		}
	}
}