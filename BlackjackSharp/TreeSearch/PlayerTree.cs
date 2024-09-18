using BlackjackSharp.Enums;

namespace BlackjackSharp.TreeSearch;

public class PlayerTree
{
	public Node root = new Node();
		
	Queue<Node> bfsQueue = new Queue<Node>();

	public PlayerTree(Rank[] startingHand)
	{
		foreach (Rank card in startingHand) root.Hand.AddCard(card);
		
		bfsQueue.Enqueue(root);
		while (bfsQueue.Count > 0)
		{
			Node current = bfsQueue.Dequeue();
			if (!current.IsTerminal) current.InitializeChildren();
			foreach (Node child in current.Children) { bfsQueue.Enqueue(child); }
		}
	}
}