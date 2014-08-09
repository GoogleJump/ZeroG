using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

public class GameLogic {
	readonly int MAX_MRX_MOVES = 24;
	public GamePosition GameBoard;

	public GameLogic(TextAsset nodeConnections, params Player[] players){
		Debug.Log ("game logic ctor");
		GameBoard = new GamePosition(players[0], players[1], players[2], players[3]);
		BuildMap(nodeConnections);
	}
	

	public void BuildMap(TextAsset nodeConnections){
		//create array of strings to hold lines of text file
		string [] lines = nodeConnections.text.Split ("\n" [0]);

		foreach (string line in lines){
			//split it into an an array of strings 
			string [] connection = line.Split(',');
			
			//create new nodes
			int nodeID0 = int.Parse (connection [0]);
			int nodeID1 = int.Parse (connection [1]);
			TransportType connType = TransportType.bus; //uninitizalied connection

			switch (connection[2].Trim()) {
				case "taxi":
					connType = TransportType.taxi;
					break;
				case "bus":
					connType = TransportType.bus;
					break;
				case "underground":
					connType = TransportType.underground;
					break;
				default:
					break;
			}
			
			//check if nodes are already in board
			if (!GameBoard.Board.ContainsKey(nodeID0)) {
				GameBoard.Board.Add(nodeID0, GameObject.Find("Node" + nodeID0).GetComponent<Node>());
			}
			if (!GameBoard.Board.ContainsKey(nodeID1)) {
				GameBoard.Board.Add(nodeID1, GameObject.Find("Node" + nodeID1).GetComponent<Node>());
			}
			
			GameBoard.Board[nodeID0].addEdges(GameBoard.Board[nodeID1], connType);
		}
	}

	public bool canMove (Player player) {
		HashSet<Node> allPossibleMoveLocations = new HashSet<Node>();
		
		allPossibleMoveLocations = player.getLocation().getAllEdges();
		allPossibleMoveLocations.ExceptWith (getAllOtherPlayerLocations (player));
		
		bool playerBlocked = (allPossibleMoveLocations.Count == 0);
		
		if(playerBlocked){
			return false;
		}
		
		foreach (Node adjacentLocation in allPossibleMoveLocations) {
			//create new HashSet that will hold the connection types... not sure if inside or outside loop.
			HashSet<TransportType> connectionTypes = getPossibleConnectionTypes(player.getLocation(), adjacentLocation);
			foreach(TransportType type in connectionTypes){
				if(playerHasEnoughTickets(player, type)){
					return true;
				}
			}
		}
		
		//player does not have enough tickets
		return false;
	}

	public bool canMoveToNode(Player player, int nodeID){
		Debug.Log (player.Location.Id + " " + nodeID);
		Node node = GameBoard.Board[nodeID];
		if (playerOnNode (node)) {
			return false;
		}
		HashSet<TransportType> connections = getPossibleConnectionTypes(player.Location, node);
		foreach(TransportType type in connections){
			if(playerHasEnoughTickets(player, type)){
				return true;
			}
		}
		return false;
	}

	public bool playerOnNode(Node node){
		foreach (Player player in GameBoard.Players.Values) {
			if (player.Location == node && !(player is MrX)) {
				return true;
			}
		}
		return false;
	}
	
	public HashSet<Node> getAllOtherPlayerLocations(Player currentPlayer){
		HashSet<Node> locations = new HashSet<Node>();
		foreach (Player player in GameBoard.Players.Values){
			if (player != currentPlayer && !(player is MrX)){
				locations.Add(player.getLocation());
			}
		}
		return locations;
	}
	
	
	public HashSet<TransportType> getPossibleConnectionTypes (Node current, Node destination){
		HashSet<TransportType> usableTickets = new HashSet<TransportType> ();
		foreach(TransportType type in Enum.GetValues(typeof(TransportType))){
			HashSet<Node> edges = current.getEdges(type);
//			foreach(Node node in edges){
//				Debug.Log (node.Id);
//			}
			if(edges.Contains(destination))
				usableTickets.Add(type);
		}
		return usableTickets;
	}
	
	public bool playerHasEnoughTickets(Player player, TransportType transportType){
		return (player.getTickets(transportType) > 0);
	}
	
	public bool winnerExists(Dictionary<int, Player> players, out int winningPlayerId) {
		foreach (Player player in players.Values){
			if (player is Detective){
				if (player.getLocation() == players[0].getLocation()){
					winningPlayerId = player.getId ();
					return true;
				}
			} else if (player is MrX){
				int CantMoves = 0;
				foreach(Player detective in players.Values) {
					if (!canMove(detective))
						CantMoves++;
				}
				if (CantMoves == (players.Count-1)){
					winningPlayerId = player.getId ();
					return true;
				}
				if (player.getMoveLogSize() == MAX_MRX_MOVES){
					winningPlayerId = player.getId ();
					return true;
				}
				
			} else {
				throw new ArgumentException();
			}
		}
		winningPlayerId = -1;
		return false;
	}
	
	public GamePosition ApplyMove(int player, Node move, GamePosition gamePosition){
		int initialPlayerLocation = gamePosition.Players [player].Location.Id;
		//Debug.Log ("initial plaeyr location: " + initialPlayerLocation);
		GamePosition potentialResult = new GamePosition (gamePosition.Players);
		HashSet<TransportType> possibleConnection = getPossibleConnectionTypes(potentialResult.Players[player].getLocation(), move);
		
		foreach (TransportType type in possibleConnection) {
			if (playerHasEnoughTickets(potentialResult.Players[player], type)){
				potentialResult.Players[player].move(move, type);
				break;
			}
		}
		//Debug.Log ("moved player location: " + potentialResult.Players[player].Location.Id);
		return potentialResult;
	}
	
	public HashSet<Node> GetPossibleMoves(int player, GamePosition gamePosition){
		Node currentLocation = gamePosition.Players[player].getLocation();
		HashSet<Node> possibleLocations = new HashSet<Node>();
		HashSet<Node> occupiedLocations = getAllOtherPlayerLocations(gamePosition.Players[player]);
		HashSet<Node> checkLocations;
		
		if(playerHasEnoughTickets(gamePosition.Players[player], TransportType.blackCard)){
			checkLocations = currentLocation.getAllEdges();
			checkLocations.ExceptWith(occupiedLocations);
			possibleLocations = new HashSet<Node>(possibleLocations.Union(checkLocations));
		} else {
			foreach(TransportType type in Enum.GetValues(typeof(TransportType))){
				if(playerHasEnoughTickets(gamePosition.Players[player], type)){
					checkLocations = currentLocation.getEdges(type);
					checkLocations.ExceptWith(occupiedLocations);
					possibleLocations = new HashSet<Node>(possibleLocations.Union(checkLocations));				
				}
			}
		}
		return possibleLocations;
	}
}