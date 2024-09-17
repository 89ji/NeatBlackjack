using BlackjackSharp.Managers;
using BlackjackSharp.Players;
using SharpNeat;
using SharpNeat.Evaluation;

namespace BlackjackSharp.NEAT;

public class BlackJackEvaluator : IPhenomeEvaluator<IBlackBox<double>>
{
	public int EvaluationCount = 2500;
	public const double WinWeight = 1;
	public const double DrawWeight = .5;
	public const double LossWeight = 0;
	
	
	public FitnessInfo Evaluate(IBlackBox<double> phenome)
	{
		IBlackjackPlayer player = new NeatPlayer(phenome);
		return Evaluate(player);
	}
	
	
	public FitnessInfo Evaluate(IBlackjackPlayer blackjackPlayer)
	{
		int winCt = 0;
		int drawCt = 0;
		int lossCt = 0;
		
		IBlackjackPlayer player = blackjackPlayer;
		GameManager gameMan = new(player);

		for (int i = 0; i < EvaluationCount; i++)
		{
			var result = gameMan.PlayGame();
			switch (result)
			{
				case GameResult.Won:
					winCt ++;
					break;
				case GameResult.Lost:
					lossCt++;
					break;
				case GameResult.Draw:
					drawCt++;
					break;
				case GameResult.DoubleWon:
					winCt += 2;
					break;
				case GameResult.DoubleLost:
					lossCt += 2;
					break;
				case GameResult.Surrendered:
					break;
			}
		}
		return new FitnessInfo( winCt / (winCt + lossCt + .1) * 100);
	}
}