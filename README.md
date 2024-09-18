# Neat Blackjack
NeatBlackjack is a project that simulates blackjack gameplay via multiple AI methods, not just Neat as the title would suggest. I used the SharpNeat library for that algorithm and my own implememnation for q-learning and the state-tree things.

## The dream of mankind has been to play gamble and lose money
Unfortunatly, gambling involves losing money so what if you could instead play blackjack for free, on the computer. To be honest, I don't like gambling but for those gamblers out there, I understand your problem. Luckily I have a solution, NeatBlackjack.

## Isn't gambling a sin?
I don't know. However, to avoid this problem, we can have the computer play blackjack for us. This project includes three methods for the computer to play blackjack. 

1. Neuroevolution of Augumenting Topologies
  This one doesn't do very well. It learns for a little bit but gets stuck due to the randomness of everything. Also training it sucks. I'll use this for another, less stochastic, problem.
2. Q-Learning
   This one did ok-ish. I also made a really nice graph representation of all the values in Godot, which ill add later.
3. State-search via tree
   Obviously the best performing, being able to peer into the future (kinda). The only issue is that the tree gets pretty big pretty fast for small starting hand values but this price is worth paying considering the training requirements for the other two methods. Moral of the story, always use the right tool for the job.
