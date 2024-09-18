using BlackjackSharp.Enums;
using BlackjackSharp.Players;
using BlackjackSharp.Types;

namespace BlackjackSharp.Managers;

public enum GameResult
{
    Won,
    Lost,
    DoubleWon,
    DoubleLost,
    Surrendered,
    Draw
}
public class GameManager
{
    readonly Hand dealerHand = new Hand();
    readonly Hand playerHand = new Hand();
    readonly IBlackjackPlayer dealer = new Dealer();
    readonly IBlackjackPlayer player;

    public GameManager(IBlackjackPlayer playerAgent)
    {
        player = playerAgent;
    }

    public GameResult PlayGame(bool printDealerDraws = false)
    {
        dealerHand.Reset();
        playerHand.Reset();

        dealerHand.AddCard(Card.GetRandomCard());
        playerHand.AddCard(Card.GetRandomCard());
        playerHand.AddCard(Card.GetRandomCard());
        
        bool surrenderable = true;
        bool doubledDown = false;
        bool surrendered = false;
        while (true)
        {
            bool endGame = false;
            switch (player.GetPlayerAction(playerHand, dealerHand, surrenderable))
            {
                case PlayerAction.Hit:
                    playerHand.AddCard(Card.GetRandomCard());
                    break;
                case PlayerAction.Stand:
                    endGame = true;
                    break;
                case PlayerAction.DoubleDown:
                    playerHand.AddCard(Card.GetRandomCard());
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
            surrenderable = false;
            if (endGame || playerHand.Evaluate() > 21) break;
        }

        while (dealer.GetPlayerAction(dealerHand, dealerHand) == PlayerAction.Hit)
        {
            var drawnCard = Card.GetRandomCard();
            dealerHand.AddCard(drawnCard);
            if (printDealerDraws) Console.WriteLine($"The dealer drew {drawnCard} and is holding {dealerHand.Evaluate()} points.");
        }
        
        if(surrendered) return GameResult.Surrendered;
        if (!doubledDown)
        {
            if (playerHand.Evaluate() > 21) return GameResult.Lost;
            if (dealerHand.Evaluate() > 21) return GameResult.Won;
            if (playerHand.Evaluate() > dealerHand.Evaluate()) return GameResult.Won;
            if (dealerHand.Evaluate() == playerHand.Evaluate()) return GameResult.Draw;
            return GameResult.Lost;
        }
        else
        {
            if (playerHand.Evaluate() > 21) return GameResult.DoubleLost;
            if (dealerHand.Evaluate() > 21) return GameResult.DoubleWon;
            if (playerHand.Evaluate() > dealerHand.Evaluate()) return GameResult.DoubleWon;
            if (dealerHand.Evaluate() == playerHand.Evaluate()) return GameResult.Draw;
            return GameResult.DoubleLost;
        }
    }
}