using SharpNeat;
using SharpNeat.Evaluation;

namespace BlackjackSharpTester;

public class BlackjackEvaluatorScheme : IBlackBoxEvaluationScheme<double>
{
    
    public IPhenomeEvaluator<IBlackBox<double>> CreateEvaluator()
    {
        return new BlackjackEvaluator();
    }

    public bool TestForStopCondition(FitnessInfo fitnessInfo)
    {
        return false;
    }

    public bool IsDeterministic => false;
    public IComparer<FitnessInfo> FitnessComparer => PrimaryFitnessInfoComparer.Singleton;
    public FitnessInfo NullFitness => FitnessInfo.DefaultFitnessInfo;
    public bool EvaluatorsHaveState => false;
    public int InputCount => 3;
    public int OutputCount => 2;
}