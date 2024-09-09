using SharpNeat;
using SharpNeat.Evaluation;

namespace BlackjackSharp.NEAT;

public class BlackJackEvaluatorScheme : IPhenomeEvaluationScheme<IBlackBox<double>>
{
	public IPhenomeEvaluator<IBlackBox<double>> CreateEvaluator()
	{
		return new BlackJackEvaluator();
	}

	public bool TestForStopCondition(FitnessInfo fitnessInfo)
	{
		return fitnessInfo.PrimaryFitness >= 48;
	}

	public bool IsDeterministic { get; } = false;
	public IComparer<FitnessInfo> FitnessComparer { get; } = new PrimaryFitnessInfoComparer();
	public FitnessInfo NullFitness { get; } = FitnessInfo.DefaultFitnessInfo;
	public bool EvaluatorsHaveState { get; } = false;
}