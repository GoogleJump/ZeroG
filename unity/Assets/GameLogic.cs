using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class GameLogic : MonoBehaviour {

	public TextAsset _boardNodesTextAsset;
	readonly int MAX_MRX_MOVES = 24;
	Dictionary<int, Player> _players = new Dictionary<int, Player>();
	Dictionary<int, Node> _board =  new Dictionary<int,Node>();


	// Use this for initialization
	void Start () {
		BuildMap();
	}

	public void BuildMap(){
		//create array of strings to hold lines of text file
		string [] lines = _boardNodesTextAsset.text.Split ("\n" [0]);
		
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
			default:
				throw new Exception();
			}
			
			//check if nodes are already in _board
			if (!_board.ContainsKey (nodeID0))
				_board.Add (nodeID0, new Node (nodeID0));
			
			if (!_board.ContainsKey (nodeID1))
				_board.Add (nodeID1, new Node (nodeID1));
			
			_board [nodeID0].addEdges (_board [nodeID1], connType);
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
	
	public HashSet<Node> getAllOtherPlayerLocations(Player currentPlayer){
		HashSet<Node> locations = new HashSet<Node>();
		foreach (Player player in _players.Values){
			if (player != currentPlayer){
				locations.Add(player.getLocation());
			}
		}
		return locations;
	}
	
	public HashSet<TransportType> getPossibleConnectionTypes (Node current, Node destination){
		HashSet<TransportType> usableTickets = new HashSet<TransportType> ();
		foreach(TransportType type in Enum.GetValues(typeof(TransportType))){
			HashSet<Node> edges = current.getEdges(type);
			if(edges.Contains(destination))
				usableTickets.Add(type);
		}
		return usableTickets;
	}
	
	public bool playerHasEnoughTickets(Player player, TransportType transportType){
		return (player.getTickets(transportType) > 0);
	}
	
	public bool checkWin(Dictionary<int, Player> players, out int winningPlayerId) {
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
}