using System.Numerics;
using SharpNeat;
using SharpNeat.Evaluation;
using SharpNeat.Experiments;
using SharpNeat.Experiments.ConfigModels;
using SharpNeat.Neat;
using SharpNeat.Neat.DistanceMetrics;
using SharpNeat.Neat.DistanceMetrics.Double;
using SharpNeat.Neat.EvolutionAlgorithm;
using SharpNeat.Neat.Genome;
using SharpNeat.Neat.Genome.Double;
using SharpNeat.Neat.Reproduction.Asexual.WeightMutation;
using SharpNeat.Neat.Speciation;
using SharpNeat.Neat.Speciation.GeneticKMeans.Parallelized;
using SharpNeat.NeuralNets.Double.ActivationFunctions;

namespace BlackjackSharpTester;

public class EvolutionAlgorithmFactory
{
    public static NeatEvolutionAlgorithm<double> CreateNeatEvolAlgo_Blackjack(INeatExperimentFactory experimentFactory)
    {
        return null;
    }
    
    /*
    private static ISpeciationStrategy<NeatGenome<TScalar>, TScalar> CreateSpeciationStrategy<TScalar>(
        INeatExperiment<TScalar> neatExperiment)
        where TScalar : struct, IBinaryFloatingPointIeee754<TScalar>
    {
        // Resolve a degreeOfParallelism (-1 is allowed in config, but must be resolved here to an actual degree).
        int degreeOfParallelismResolved = neatExperiment.DegreeOfParallelism;

        // Define a distance metric to use for k-means speciation; this is the default from sharpneat 2.x.
        IDistanceMetric<TScalar> distanceMetric = new ManhattanDistanceMetric(1.0, 0.0, 10.0);

        // Use k-means speciation strategy; this is the default from sharpneat 2.x.
        // Create a serial (single threaded) strategy if degreeOfParallelism is one.
        if (degreeOfParallelismResolved == 1)
            
            return new SharpNeat.Neat.Speciation.GeneticKMeans.GeneticKMeansSpeciationStrategy<TScalar>(distanceMetric: distanceMetric, 5);

        // Create a parallel (multi-threaded) strategy for degreeOfParallelism > 1.
        return new Speciation.GeneticKMeans.Parallelized.GeneticKMeansSpeciationStrategy<TScalar>(distanceMetric, 5, degreeOfParallelismResolved);
    }*/
}
