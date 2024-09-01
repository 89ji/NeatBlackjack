using BlackjackSharp;
using SharpNeat;
using SharpNeat.Evaluation;

namespace BlackjackSharpTester;

public class BlackjackEvaluator : IPhenomeEvaluator<IBlackBox<double>>
{
    public FitnessInfo Evaluate(IBlackBox<double> phenome)
    {
        double fitness = 0.0;
        GameManager man = new GameManager(new NeatPlayer(phenome));
        for (int i = 0; i < 100; i++)
        {
            var result = man.PlayGame();
            if (result == GameResult.Won) fitness += 0.5;
        }

        fitness = Math.Pow(fitness, 1.1);
        return new FitnessInfo(fitness);
    }

    private class NeatPlayer : IBlackjackPlayer
    {
        private IBlackBox<double> phenome;
        public NeatPlayer(IBlackBox<double> phenome) => this.phenome = phenome;
        public PlayerAction GetPlayerAction(Hand playerHand, Hand dealerHand, bool surrenderable = false)
        {
            phenome.Reset();
            var input = phenome.Inputs.Span;
            var output = phenome.Outputs.Span;

            input[0] = 1;
            input[1] = playerHand.Evaluate();
            input[2] = dealerHand.Evaluate();
            
            phenome.Activate();

            if (output[0] > output[1]) return PlayerAction.Hit;
            else return PlayerAction.Stand;
        }
    }
}