using SharpNeat.Evaluation;
using SharpNeat.Experiments;
using SharpNeat.Experiments.ConfigModels;
using SharpNeat.IO;
using SharpNeat.NeuralNets;

namespace BlackjackSharpTester;

/*public class BlackjackExperimentFactory : INeatExperimentFactory
{
    public string Id => "Blackjack";
    public INeatExperiment<double> CreateExperiment(Stream jsonConfigStream)
    {
        ExperimentConfig config = JsonUtils.Deserialize<ExperimentConfig>(jsonConfigStream);
        BlackjackEvaluatorScheme evalScheme = new();
        var experiment = new NeatExperiment<double>((IBlackBoxEvaluationScheme<double>)evalScheme, Id)
        {
            IsAcyclic = true,
            ActivationFnName = ActivationFunctionId.LeakyReLU.ToString(),
        };
        experiment.Configure(config);
        return experiment;
    }

    public INeatExperiment<float> CreateExperimentSinglePrecision(Stream jsonConfigStream)
    {
        throw new NotImplementedException();
    }
}*/