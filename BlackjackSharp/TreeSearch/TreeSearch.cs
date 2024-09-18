using BlackjackSharp.Enums;

namespace BlackjackSharp.TreeSearch;

public class TreeSearch
{
	public static void TreeMain()
	{
		PlayerTree pTree = new(new[] { Rank.Four, Rank.Eight });

		double proba = pTree.root.CalculateBustChance();
		Console.WriteLine($"Probability of bust on hit: {100 * proba:F2}%");
		
		foreach (var child in pTree.root.Children)
		{
			Console.WriteLine($"Probability in {child.Hand.Evaluate()}: {100 * child.CalculateBustChance():F2}%");
		}
	}
}