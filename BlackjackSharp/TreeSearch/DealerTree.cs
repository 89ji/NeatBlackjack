using BlackjackSharp.Enums;

namespace BlackjackSharp.TreeSearch;

public class DealerTree
{
	public Node root = new Node();
		
	Queue<Node> bfsQueue = new Queue<Node>();

	public DealerTree(Rank[] startingHand)
	{
		foreach (Rank card in startingHand) root.Hand.AddCard(card);
		
		bfsQueue.Enqueue(root);
		while (bfsQueue.Count > 0)
		{
			Node current = bfsQueue.Dequeue();
			if (current.Hand.Evaluate() < 17) current.InitializeChildren();
			else current.Pruned = true;
			foreach (Node child in current.Children) { bfsQueue.Enqueue(child); }
		}
	}
}