using System.Diagnostics;
using BlackjackSharp;
using SharpNeat;
using SharpNeat.EvolutionAlgorithm;
using SharpNeat.Evaluation;
using SharpNeat.Neat;
using SharpNeat.Neat.DistanceMetrics;
using SharpNeat.Neat.DistanceMetrics.Double;
using SharpNeat.Neat.EvolutionAlgorithm;
using SharpNeat.Neat.Speciation;
using SharpNeat.Neat.Genome;
using SharpNeat.Neat.Speciation.GeneticKMeans;
using SharpNeat.NeuralNets.Double.ActivationFunctions;

namespace BlackjackSharpTester;

class Program
{
    static void Main(string[] args)
    {
        sample();
        
        /*var ea = EvolutionAlgorithmFactory.CreateNeatEvolAlgo_Blackjack();
        ea.Initialise();
        var neatPop = ea.Population;

        BlackjackExperimentFactory fac = new();
        
        
        
        for(int i = 0; i < 10_000; i++)
        {
            ea.PerformOneGeneration();
            Console.WriteLine($"{ea.Stats.Generation} {neatPop.Stats.BestFitness.PrimaryFitness} {neatPop.Stats.MeanComplexity} {ea.ComplexityRegulationMode} {neatPop.Stats.MeanFitness}");

            if(ea.Population.Stats.BestFitness.PrimaryFitness >= 2048.0)
            {
                break;
            }
        }*/
    }

    static void sample()
    {
        // Define settings for NEAT
        var neatSettings = new NeatEvolutionAlgorithmSettings();
        neatSettings.SpeciesCount = 150;

        // Define genome and population settings
        var metaNeatGenome = MetaNeatGenome<double>.CreateAcyclic(2, 1, new LeakyReLU());
        var distanceMetric = new EuclideanDistanceMetric();
        
        var speciationStrategy = new  GeneticKMeansSpeciationStrategy<double>(distanceMetric, 10);

        // Create the population
        var population = NeatPopulationFactory<double>.CreatePopulation(
            metaNeatGenome, 
            .1, 
            150);

        // Define an evaluator for the XOR task
        var evaluator = new BlackjackEvaluator();

        // Create the evolution algorithm
        var evolutionAlgorithm = new NeatEvolutionAlgorithm<double>(
            eaSettings: neatSettings,
            evaluator: (IGenomeListEvaluator<NeatGenome<double>>)evaluator,
            speciationStrategy: speciationStrategy,
            population: population,
            complexityRegulationStrategy: null,
            reproductionSexualSettings: null,
            reproductionAsexualSettings: null,
            weightMutationScheme: null
            );

        // Start the evolution process
        evolutionAlgorithm.Initialise();

        for(int gen = 0; gen < 10000; gen++)
        {
            Console.WriteLine($"Generation: {evolutionAlgorithm.Stats.Generation}, Best Fitness: {evolutionAlgorithm.Population.Stats.BestFitness.PrimaryFitness}");
        }
    }
}