using BlackjackSharp.Enums;
using BlackjackSharp.NEAT;
using BlackjackSharp.Types;

namespace BlackjackSharp.Players;

// This uses a precalculated table to figure moves, as given by the resource on blackjackapprenticeship.com/blackjack-strategy-charts/
public class OptimalPlayer : IBlackjackPlayer
{
	// An 10 by 10 table mapping [playerHandValue - 8, dealerHandValue - 2] to a game action
	// Player hand starts at a value of 8 and ends at 17
	// Dealer hand starts at 2 and goes to Ace
	readonly Action[,] hardTable =
	{
		{ Action.H, Action.H, Action.H, Action.H, Action.H, Action.H, Action.H, Action.H, Action.H, Action.H },
		{ Action.H, Action.Dh, Action.Dh, Action.Dh, Action.Dh, Action.H, Action.H, Action.H, Action.H, Action.H },
		{ Action.Dh, Action.Dh, Action.Dh, Action.Dh, Action.Dh, Action.Dh, Action.Dh, Action.Dh, Action.H, Action.H },
		{ Action.Dh, Action.Dh, Action.Dh, Action.Dh, Action.Dh, Action.Dh, Action.Dh, Action.Dh, Action.Dh, Action.Dh},
		{ Action.H, Action.H, Action.S, Action.S, Action.S, Action.H, Action.H, Action.H, Action.H, Action.H },
		{ Action.S, Action.S, Action.S, Action.S, Action.S, Action.H, Action.H, Action.H, Action.H, Action.H },
		{ Action.S, Action.S, Action.S, Action.S, Action.S, Action.H, Action.H, Action.H, Action.H, Action.H },
		{ Action.S, Action.S, Action.S, Action.S, Action.S, Action.H, Action.H, Action.H, Action.H, Action.H },
		{ Action.S, Action.S, Action.S, Action.S, Action.S, Action.H, Action.H, Action.H, Action.H, Action.H },
		{ Action.S, Action.S, Action.S, Action.S, Action.S, Action.S, Action.S, Action.S, Action.S, Action.S },
	};
	
	// An 8 by 10 table mapping [playerHandValue - 13, dealerHandValue - 2] to a game action
	// Player hand starts at a value of 13 and ends at 20
	// Dealer hand starts at 2 and goes to Ace
	readonly Action[,] softTable =
	{
		{ Action.H, Action.H, Action.H, Action.Dh, Action.Dh, Action.H, Action.H, Action.H, Action.H, Action.H },
		{ Action.H, Action.H, Action.H, Action.Dh, Action.Dh, Action.H, Action.H, Action.H, Action.H, Action.H },
		{ Action.H, Action.H, Action.Dh, Action.Dh, Action.Dh, Action.H, Action.H, Action.H, Action.H, Action.H },
		{ Action.H, Action.H, Action.Dh, Action.Dh, Action.Dh, Action.H, Action.H, Action.H, Action.H, Action.H },
		{ Action.H, Action.Dh, Action.Dh, Action.Dh, Action.Dh, Action.H, Action.H, Action.H, Action.H, Action.H },
		{ Action.Ds, Action.Ds, Action.Ds, Action.Ds, Action.Ds, Action.S, Action.S, Action.H, Action.H, Action.H },
		{ Action.S, Action.S, Action.S, Action.S, Action.Ds, Action.S, Action.S, Action.S, Action.S, Action.S },
		{ Action.S, Action.S, Action.S, Action.S, Action.S, Action.S, Action.S, Action.S, Action.S, Action.S }
	};

	public PlayerAction GetPlayerAction(Hand playerHand, Hand dealerHand, bool surrenderable = false)
	{
		int playerHandVal = playerHand.Evaluate();
		int dealerHandVal = dealerHand.Evaluate();
		
		if (!playerHand.SoftHand)
		{
			if (playerHandVal < 8) return PlayerAction.Hit;
			if (playerHandVal > 17) return PlayerAction.Stand;
			switch (hardTable[playerHandVal - 8, dealerHandVal - 2])
			{
				case Action.S:
				case Action.Ds:
					return PlayerAction.Stand;
				case Action.H:
				case Action.Dh:
					return PlayerAction.Hit;
			}
		}
		else
		{
			if (playerHandVal < 12) return PlayerAction.Hit;
			if (playerHandVal >= 20) return PlayerAction.Stand;
			switch (softTable[playerHandVal - 12, dealerHandVal - 2])
			{
				case Action.S:
				case Action.Ds:
					return PlayerAction.Stand;
				case Action.H:
				case Action.Dh:
					return PlayerAction.Hit;
			}
		}

		return PlayerAction.Surrender;
	}

	enum Action
	{
		S,
		Dh,
		Ds,
		H,
	}

	
	// At 10,000 hands/eval, avg = 49.92, std = .5500
	// At 5,000, .7489
	// At 1,000 hands/eval, std = 1.631
	// At 500, 5.515
	public static void Benchmark()
	{
		int[] sampEvalCounts = new[] {5};

		foreach (int sampEvalCount in sampEvalCounts)
		{
			List<double> trials = new();


			var optPlayer = new OptimalPlayer();
			
			for (int i=0; i<500; i++)
			{
				BlackJackEvaluator bjeval = new();
				bjeval.EvaluationCount = sampEvalCount;
				var result = bjeval.Evaluate(optPlayer);
				//Console.WriteLine($"The optimal player has a fitness of {result.PrimaryFitness}");
				trials.Add(result.PrimaryFitness);
			}

			double avg = 0;
			foreach (double item in trials) avg += item;
			avg /= trials.Count;

			double stdev = 0;
			foreach (double item in trials) stdev += Math.Pow((item - avg), 2);
			stdev /= trials.Count;
			stdev = Math.Sqrt(stdev);
			Console.WriteLine($"The average and stdev for n={sampEvalCount} are {avg} and {stdev:f2}");
		}
	}
}