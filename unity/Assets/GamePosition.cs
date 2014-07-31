using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePosition : MonoBehaviour {
	public Dictionary<int, Node> Board = new Dictionary<int, Node>();
	public Dictionary<int, Player> Players = new Dictionary<int, Player>();
	public MrX mrX;
	public Detective detective1, detective2, detective3;

	void Awake(){
		Players.Add (0, mrX);
		Players.Add (1, detective1);
		Players.Add (2, detective2);
		Players.Add (3, detective3);

		//Generate distinct starting locations
		System.Random randomGenerator = new System.Random();
		HashSet<int> randomLocations = new HashSet<int>();

		const int numberOfLocations = 4;
		for (int i = 0; i < numberOfLocations; ++i) {
			int possibleLocation = randomGenerator.Next(1,200);
			while(randomLocations.Contains(possibleLocation)){
				possibleLocation = randomGenerator.Next(1,200);
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