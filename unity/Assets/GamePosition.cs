using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePosition {
	public Dictionary<int, Node> Board;
	public Dictionary<int, Player> Players;
	public GamePosition Parent;

	public GamePosition(params Player[] players){
		Board = new Dictionary<int, Node>();
		Players = new Dictionary<int, Player>();

		for (int i = 0; i < players.Length; ++i) {
			Players.Add (i, players [i]);
		}

		for (int i = 1; i <= 199; ++i) {
			Board.Add(i, GameObject.Find("Node" + i).GetComponent<Node>());
		}
		reset();
	}

	public GamePosition(Dictionary<int, Player> players){
		Board = new Dictionary<int, Node>();
		Players = new Dictionary<int, Player>(players);
		
		for (int i = 1; i <= 199; ++i) {
			Board.Add(i, GameObject.Find("Node" + i).GetComponent<Node>());
		}

		reset();
	}

	public void reset(){
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
			Players[playerNumberCounter].resetTickets();
			++playerNumberCounter;
		}	
	}
}