using System.Diagnostics;
using BlackjackSharp;

namespace BlackjackSharpTester;

class Program
{
    static void Main(string[] args)
    {
        /*IBlackjackPlayer me = new UIPlayer();
        Hand testHand1 = new Hand();
        Hand testHand2 = new Hand();
        testHand1.AddCard(Card.GetRandomCard());
        testHand1.AddCard(Card.GetRandomCard());
        testHand2.AddCard(Card.GetRandomCard());
        
        me.GetPlayerAction(testHand1, testHand2);*/
        
        /*
        GameManager man = new(new UIPlayer());
        while(true) Console.WriteLine(man.PlayGame());
        */

        uint runIterations = 1000000;
        Stopwatch watch = new();
        watch.Restart();
        
        foreach(var userAgent in new IBlackjackPlayer[] {new RandomAction(), new StandOnly(), new Dealer()} )
        {
        Console.WriteLine($"\nDoing simulations for new thingy");
        
        GameManager man = new(userAgent);
        
        int[] results = new int[6]; 
        for (int i = 0; i < runIterations; i++)
        {
            GameResult result = man.PlayGame();
            results[(int)result]++;
        }

        watch.Stop();
        Console.WriteLine($"Ran {runIterations} times in {watch.ElapsedMilliseconds} ms.");
        
        for (int i = 0; i < results.Length; i++)
        {
            Console.WriteLine($"{(GameResult)i}: \t{results[i]} \t {100 * (double)results[i]/(double)runIterations :F2}%");
        }
        }
        
        //Console.ReadLine();
    }
}