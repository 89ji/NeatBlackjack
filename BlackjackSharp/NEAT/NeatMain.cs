using Redzen.Numerics.Distributions.Double;
using SharpNeat.Evaluation;
using SharpNeat.EvolutionAlgorithm;
using SharpNeat.Neat;
using SharpNeat.Neat.ComplexityRegulation;
using SharpNeat.Neat.DistanceMetrics.Double;
using SharpNeat.Neat.EvolutionAlgorithm;
using SharpNeat.Neat.Genome;
using SharpNeat.Neat.Genome.Double;
using SharpNeat.Neat.Genome.IO;
using SharpNeat.Neat.Reproduction.Asexual;
using SharpNeat.Neat.Reproduction.Asexual.WeightMutation;
using SharpNeat.Neat.Reproduction.Asexual.WeightMutation.Double;
using SharpNeat.Neat.Reproduction.Asexual.WeightMutation.Selection;
using SharpNeat.Neat.Reproduction.Sexual;
using SharpNeat.NeuralNets.Double.ActivationFunctions;

namespace BlackjackSharp.NEAT;

public static class NeatMain
{
	public static void RunNeat()
	{
		var neatSettings = new NeatEvolutionAlgorithmSettings
		{
			SpeciesCount = 15,
			ElitismProportion = .2,
			SelectionProportion = .9,
			OffspringAsexualProportion = .5,
			OffspringSexualProportion = .5,
			InterspeciesMatingProportion = .01,
		};

		var asexualSettings = new NeatReproductionAsexualSettings
		{
			ConnectionWeightMutationProbability = .94,
			AddNodeMutationProbability = .01,
			AddConnectionMutationProbability = .025,
			DeleteConnectionMutationProbability = .025,
		};

		var sexualSettings = new NeatReproductionSexualSettings
		{
			SecondaryParentGeneProbability = .1
		};
		
		var metaNeatGenome = MetaNeatGenome<double>.CreateAcyclic(3, 2, new LeakyReLU());
		
		var complexitySettings = new RelativeComplexityRegulationStrategy(10, 30);
		
		int degOfParallelism = 6;
		
		IGenomeListEvaluator<NeatGenome<double>> eval =
			GenomeListEvaluatorFactory.CreateEvaluator(new NeatGenomeDecoderAcyclic(), new BlackJackEvaluatorScheme(),
				degOfParallelism);

		var stratProbArr = new[] { 1.0 };
		IWeightMutationStrategy<double>[] mutStratArr = new IWeightMutationStrategy<double>[]
		{
			new DeltaWeightMutationStrategy(new ProportionSubsetSelectionStrategy(.9),
				new BoxMullerGaussianStatelessSampler())
		};
		var weightMutSettings = new WeightMutationScheme<double>(stratProbArr, mutStratArr);

		var specStrat = new SharpNeat.Neat.Speciation.GeneticKMeans.Parallelized.GeneticKMeansSpeciationStrategy<double>(new ManhattanDistanceMetric(), 2, degOfParallelism);

		var population = LoadPopulation(metaNeatGenome, 500, asexualSettings, weightMutSettings);
		
		var ea = new NeatEvolutionAlgorithm<double>(
			eaSettings: neatSettings,
			evaluator: eval,
			speciationStrategy: specStrat,
			population: population,
			complexityRegulationStrategy: complexitySettings,
			reproductionAsexualSettings: asexualSettings,
			reproductionSexualSettings: sexualSettings,
			weightMutationScheme: weightMutSettings
			);
		
		ea.Initialise();
		int counter = 0;
		while (true)
		{
			counter++;
			ea.PerformOneGeneration();
			Console.WriteLine($"Current Generation: {ea.Stats.Generation:N0}, Peak Fitness: {ea.Population.BestGenome.FitnessInfo.PrimaryFitness:F1}, Peak Complexity {ea.Population.Stats.BestComplexity:F2}, Current Mode: {ea.ComplexityRegulationMode}");
			if (counter % 25 == 0)
			{
				counter = 0;
				SaveToFolder(population);
			}
		}
	}

	const string savePath =
		"C:\\Users\\Joseph Bolduc\\Documents\\Projects Master\\NeatBlackjack\\BlackjackSharpTester\\bin\\Release\\net8.0\\win-x64\\publish\\Blackjack Thingy";
	private static void SaveToFolder(Population<NeatGenome<double>> population)
	{
		Directory.Delete(savePath, true);
		Directory.CreateDirectory(savePath);
		NeatPopulationSaver.SaveToFolder(population.GenomeList, savePath, "Blackjack Thingy");
	}

	private static NeatPopulation<double> LoadPopulation(MetaNeatGenome<double> meta, int popSize, NeatReproductionAsexualSettings asex, WeightMutationScheme<double> wgat)
	{
		if (!Directory.Exists(savePath))
		{
			Directory.CreateDirectory(savePath);
			return NeatPopulationFactory<double>.CreatePopulation(meta, .05, 500);
		}
		else
		{
			NeatPopulationLoader<double> loader = new(meta);
			var loaded = loader.LoadFromFolder("C:\\Users\\Joseph Bolduc\\Documents\\Projects Master\\NeatBlackjack\\BlackjackSharpTester\\bin\\Release\\net8.0\\win-x64\\publish\\Blackjack Thingy");
			return NeatPopulationFactory<double>.CreatePopulation(meta, 500, loaded, asex, wgat);
		}
	}
}