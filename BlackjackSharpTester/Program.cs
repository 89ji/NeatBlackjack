using System.Diagnostics;
using System.Security.Principal;
using BlackjackSharp;

namespace BlackjackSharpTester;

static class Program
{
    static void Main(string[] args)
    {
        // Sample code for a blackjack instance
        var agent = new UIPlayer();
        var bjMan = new GameManager(agent);
        var result = bjMan.PlayGame(true);
        
        Console.WriteLine(result);
    }
}