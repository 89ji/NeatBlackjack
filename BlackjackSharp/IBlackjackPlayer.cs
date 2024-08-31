namespace BlackjackSharp;

public enum PlayerAction
{
    Hit,
    Stand,
    DoubleDown,
    Split,
    Surrender
}

public interface IBlackjackPlayer
{
    public PlayerAction GetPlayerAction(Hand playerHand, Hand dealerHand, bool surrenderable = false);
}

public class Dealer : IBlackjackPlayer
{
    // In this scenario, the player is the dealer and dealerHandValue is unused
    public PlayerAction GetPlayerAction(Hand playerHand, Hand dealerHand, bool surrenderable = false)
    {
        if (playerHand.Evaluate() < 17) return PlayerAction.Hit;
        else return PlayerAction.Stand;
    }
}

public class UIPlayer : IBlackjackPlayer
{
    public PlayerAction GetPlayerAction(Hand playerHand, Hand dealerHand, bool surrenderable = false)
    { 
        Console.WriteLine("\nThe cards in your hand are:");
        foreach (Card card in playerHand.GetCards()) Console.WriteLine(card);
        Console.WriteLine($"for a total of {playerHand.Evaluate()} point{(playerHand.Evaluate() > 1 ? "s" : "")}\n");
        
        Console.WriteLine(
            $"The dealer is showing a {dealerHand.GetCards()[0]} for {dealerHand.Evaluate()} point{(dealerHand.Evaluate() > 1 ? "s" : "")}\n");
        
        while (true)
        {
            Console.WriteLine("You may...");
            Console.WriteLine("1 - Stand");
            Console.WriteLine("2 - Hit");
            Console.WriteLine("3 - Double Down");
            
            if (playerHand.Splittable) Console.WriteLine("4 - Split");
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("You may not split at this time.");
                Console.ResetColor();
            }

            if (surrenderable) Console.WriteLine("5 - Surrender");
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("You may not surrender at this time.");
                Console.ResetColor();
            }

            Console.Write("\nYour move: ");
            string move = Console.ReadLine();
            switch (move)
            {
                case "1":
                    return PlayerAction.Stand;
                    break;
                case "2":
                    return PlayerAction.Hit;
                    break;
                case "3":
                    return PlayerAction.DoubleDown;
                    break;
                case "4":
                    if (playerHand.Splittable) return PlayerAction.Split;
                    else Console.WriteLine("You may not split at this time.");
                    break;
                case "5":
                    if (surrenderable) return PlayerAction.Surrender;
                    else Console.WriteLine("You may not surrender at this time.");
                    break;
                default:
                    Console.WriteLine("Error: Invalid move.");
                    break;
            }
        }
    }
}

public class StandOnly : IBlackjackPlayer
{
    public PlayerAction GetPlayerAction(Hand playerHand, Hand dealerHand, bool surrenderable = false)
    {
        return PlayerAction.Stand;
    }
}

public class RandomAction : IBlackjackPlayer
{
    Random rng = new();
    public PlayerAction GetPlayerAction(Hand playerHand, Hand dealerHand, bool surrenderable = false)
    {
        List<PlayerAction> possibleActions = new();
        possibleActions.Add(PlayerAction.Hit);
        possibleActions.Add(PlayerAction.Stand);
        possibleActions.Add(PlayerAction.DoubleDown);
        if (surrenderable) possibleActions.Add(PlayerAction.Surrender);
        //if (playerHand.Splittable) possibleActions.Add(PlayerAction.Split);

        int randIdx = rng.Next(0, possibleActions.Count);
        return possibleActions[randIdx];
    }
}