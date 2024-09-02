namespace BlackjackSharpTester;

public class QLearner
{
    // Given that the lowest hand in blackjack is a 2 and that there isn't any reason to
    // account for anything 21 or over, there are only 19 states needed
    private int handRange;
    
    // In this sample, the only options are to hit (0) or stand (1)
    private int decisionSpace;
    
    // The order of states in the table is myHand, dealersHand, if the hand is soft (1 if yes) and the decisions
    private float[,,,] QTable;
    
    // Various constants for learning
    private const float LEARNING_RATE = .1f;
    private const float DISCOUNT = .95f;
    private const int EPISODES = 25_000;

    public QLearner(int hRange = 19, int decSpace = 2)
    {
        handRange = hRange;
        decisionSpace = decSpace;
        QTable = new float[handRange, handRange, 2, decisionSpace];
        randomizeArray();
    }

    private void randomizeArray()
    {
        Random rnd = new Random();
        for(int i = 0; i < handRange; i++)
        for(int j = 0; j < handRange; j++)
        for(int k = 0; k <= 1; k++)
        for(int l = 0; l <= 1; l++)
                SetMoveToTable(i, j, k != 0, l, (float)rnd.NextDouble() * 2 - 1);
    }

    public void DoThing()
    {
        int myHand = 19;
        int dealH = 9;
        bool soft = true;
        
        int action = GetOptimalMove(myHand, dealH, soft);
        
        // TODO sumbit the move to the blackjack game and get info on new game state
        
        float currentQ = GetMovesFromTable(myHand, dealH, soft)[action];
        float maxFutureQ = GetMovesFromTable(myHand, dealH, soft).Max(); // replace vars with new state vars
        float reward = 0;
        
        float newQ = (1 - LEARNING_RATE) * currentQ + LEARNING_RATE * reward + DISCOUNT + maxFutureQ;
        SetMoveToTable(myHand, dealH, soft, action, newQ);
    }
    
    
    public float[] GetMovesFromTable(int myHand, int dealerHand, bool softHand)
    {
        var row = new float[decisionSpace];
        for(var i = 0; i < decisionSpace; i++) row[i] = QTable[myHand, dealerHand, softHand ? 1 : 0, i];
        return row;
    }

    public int GetOptimalMove(int myHand, int dealerHand, bool softHand)
    {
        var row = GetMovesFromTable(myHand, dealerHand, softHand);
        return row[0] > row[1] ? 0 : 1;
    }

    public void SetMoveToTable(int myHand, int dealerHand, bool softHand, int moveId, float value)
    {
        QTable[myHand, dealerHand, softHand ? 1 : 0, moveId] = value;
    }
    
}