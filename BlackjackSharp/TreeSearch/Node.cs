using BlackjackSharp.Enums;
using BlackjackSharp.Types;

namespace BlackjackSharp.TreeSearch;

public class Node
{
	public Node? Parent;
	public List<Node> Children { get; init; } = new();
	
	public bool IsTerminal => (Hand.Evaluate() > 21 || Pruned);
	public bool Pruned = false;
	
	public Hand Hand = new();


	public void InitializeChildren()
	{
		if (Children.Count != 0)
		{
			Console.WriteLine("Children already exist");
			return;
		}

		foreach (Rank card in Enum.GetValues(typeof(Rank)))
		{
			var newChild = Clone();
			newChild.Hand.AddCard(card);
			Children.Add(newChild);
		}
	}

	// Clones this node as a child
	private Node Clone()
	{
		Node newNode = new Node();
		newNode.Parent = this;
		foreach (var card in Hand.GetCards()) newNode.Hand.AddCard(card);
		return newNode;
	}

	private double? MemoValue;

	public double CalculateBustChance()
	{
		double prob = 0;
		if (IsTerminal) return 1;
		foreach (var child in Children)
		{
			if (child.IsTerminal) prob++;
		}
		prob /= Children.Count;
		return prob;
	}
}