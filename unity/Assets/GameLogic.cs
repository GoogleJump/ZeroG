using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class GameLogic {

	const static int MAX_MRX_MOVES = 24;
	static Dictionary<PlayerID, Player> players = new Dictionary<PlayerID, Player>();
	static Dictionary<int, Node> board =  new Dictionary<int,Node>();

	public GameLogic(){

		BuildMap("BoardNodes.txt");
	}

	public static void BuildMap(string textFile){
				//create array of strings to hold lines of text file
				string [] lines = File.ReadAllLines (textFile);

				//loop through each line (string) 
				foreach (string line in lines) {
						//split it into an an array of strings 
						string [] connection = line.Split (',');

						//create new nodes
						int nodeID0 = int.Parse (connection [0]);
						int nodeID1 = int.Parse (connection [1]);
						TransportType connType;

						switch (connection [2]) {
						case "taxi":
								connType = TransportType.taxi;
								break;
						case "bus":
								connType = TransportType.bus;
								break;
						case "underground":
								connType = TransportType.underground;
								break;
						}

						//check if nodes are already in board
						if (!board.ContainsKey (nodeID0))
								board.Add (nodeID0, new Node (nodeID0));

						if (!board.ContainsKey (nodeID1))
								board.Add (nodeID1, new Node (nodeID1));

						board [nodeID0].addEdges (board [nodeID1], connType);
						board [nodeID1].addEdges (board [nodeID0], connType);
				}
		}

	public static bool canMove (Player player) {
		HashSet<Node> allPossibleMoveLocations = new HashSet<Node>();

		allPossibleMoveLocations = player.getLocation().getAllEdges();
		allPossibleMoveLocations.ExceptWith (getAllOtherPlayerLocations (player));

		bool playerBlocked = allPossibleMoveLocations.Any();
		
		if(playerBlocked){
			return false;
		}
		foreach (Node adjacentlLocation in allPossibleMoveLocations) {
			//create new HashSet that will hold the connection types... not sure if inside or outside loop.
			HashSet<TransportType> connectionTypes = getPossibleConnectionTypes(player.getLocation(), adjacentLocation);
			foreach(TransportType type in connectionTypes){
				if(playerHasEnoughTickets(player, transportType){
					return true;
				}
			}
		}
	//player does not have enough tickets
	return false;
	}


	public static HashSet<TransportType> getPossibleConnectionTypes (Node current, Node destination){
	HashSet<TransportType> usableTickets = new HashSet<TransportType> ();
	foreach(TransportType type in TransnportType.values()){
		//not sure what TransportType.values() does
		HashSet<Node> edges = current.getEdges(type);
		if(edges.Contains(destination))
			usableTickets.Add(type);
		}
	}

	public static checkWin() {
		foreach (Player player in players.values()){
			if (player is Detective){
				if (player.getLocation() == players.get(0).getLocation(){
					return true;
				}
			}
			else if (player instanceof MrX) {
				int CantMoves = 0;
				foreach(Player detective in players.values()) {
					if (!canMove(detective))
						CantMoves++;
				}
				if (CantMoves == (players.size()-1))
					return true;
				if (player.getMoveLogSize() == MAX_MRX_MOVES)
					return true;
				}
				else 
					throw new IllegalArgumentException();	//throw exception
		}
		return false;
	}
}