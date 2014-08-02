using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePosition : MonoBehaviour {
	public Dictionary<int, Node> Board;
	public Dictionary<int, Player> Players;
	public MrX mrX;
	public Detective detective1, detective2, detective3;

	void Awake(){
		Board = new Dictionary<int, Node>();
		Players = new Dictionary<int, Player>();
		Players.Add (0, mrX);
		Players.Add (1, detective1);
		Players.Add (2, detective2);
		Players.Add (3, detective3);

		for (int i = 1; i <= 199; ++i) {
			Board.Add(i, GameObject.Find("Node" + i).GetComponent<Node>());
		}
		

		//Generate distinct starting locations
		System.Random randomGenerator = new System.Random();
		HashSet<int> randomLocations = new HashSet<int>();

		const int numberOfLocations = 4;
		for (int i = 0; i < numberOfLocations; ++i) {
			int possibleLocation = randomGenerator.Next(1,199);
			while(randomLocations.Contains(possibleLocation)){
				possibleLocation = randomGenerator.Next(1,199);
			}
			randomLocations.Add(possibleLocation);
		}

		int playerNumberCounter = 0;
		foreach (int location in randomLocations) {
			Players[playerNumberCounter].Location = Board[location];
			++playerNumberCounter;
		}	
	}
}