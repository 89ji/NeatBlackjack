using BlackjackSharp.Enums;
using BlackjackSharp.Players;
using BlackjackSharp.Types;

namespace BlackjackSharp.Managers;

// A class that lets you play the game without a blackjackplayer thingy
public class GamePlayer
{
    public readonly Hand DealerHand = new Hand();
    public readonly Hand PlayerHand = new Hand();
    readonly IBlackjackPlayer dealer = new Dealer();

    bool surrenderable = true;
    bool doubledDown = false;
    bool surrendered = false;
    
    public bool GameIsDone = false;
    public GameResult Result = GameResult.Won;

    bool printDealerDraws;

    public GamePlayer(bool printDraws = false)
    {
        printDealerDraws = printDraws;
    }

    public void ResetGame()
    {
        DealerHand.Reset();
        PlayerHand.Reset();

        DealerHand.AddCard(Card.GetRandomCard());
        PlayerHand.AddCard(Card.GetRandomCard());
        PlayerHand.AddCard(Card.GetRandomCard());

        surrenderable = true;
        doubledDown = false;
        surrendered = false;
        
        GameIsDone = false;
    }

    public void SubmitMove(PlayerAction move)
    {
        bool endGame = false;
        switch (move)
        {
            case PlayerAction.Hit:
                PlayerHand.AddCard(Card.GetRandomCard());
                break;
            case PlayerAction.Stand:
                endGame = true;
                break;
            case PlayerAction.DoubleDown:
                PlayerHand.AddCard(Card.GetRandomCard());
                endGame = true;
                doubledDown = true;
                break;
            case PlayerAction.Split:
                throw new NotImplementedException();
                break;
            case PlayerAction.Surrender:
                endGame = true;
                surrendered = true;
                break;
        }
        surrenderable = false;
        if (endGame || PlayerHand.Evaluate() > 21) FinishGame();
    }

    private void FinishGame()
    {
        GameIsDone = true;
        
        while (dealer.GetPlayerAction(DealerHand, DealerHand) == PlayerAction.Hit)
        {
            Card drawnCard = Card.GetRandomCard();
            DealerHand.AddCard(drawnCard);
            if (printDealerDraws) Console.WriteLine($"The dealer drew {drawnCard} and is holding {DealerHand.Evaluate()} points.");
        }
        
        if(surrendered) Result = GameResult.Surrendered;
        if (!doubledDown)
        {
            if (PlayerHand.Evaluate() > 21) Result = GameResult.Lost;
            else if (DealerHand.Evaluate() > 21) Result = GameResult.Won;
            else if (PlayerHand.Evaluate() > DealerHand.Evaluate()) Result = GameResult.Won;
            else if (DealerHand.Evaluate() == PlayerHand.Evaluate()) Result = GameResult.Draw;
            else Result = GameResult.Lost;
        }
        else
        {
            if (PlayerHand.Evaluate() > 21) Result = GameResult.DoubleLost;
            else if (DealerHand.Evaluate() > 21) Result = GameResult.DoubleWon;
            else if (PlayerHand.Evaluate() > DealerHand.Evaluate()) Result = GameResult.DoubleWon;
            else if (DealerHand.Evaluate() == PlayerHand.Evaluate()) Result = GameResult.Draw;
            Result = GameResult.DoubleLost;
        }
    }
}