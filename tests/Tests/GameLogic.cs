using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

public class GameLogic {

	static readonly int MAX_MRX_MOVES = 24;
	public static GamePosition GamePosition { get; set; }

	public static void BuildMap(string textFile){
				//create array of strings to hold lines of text file
				string [] lines = File.ReadAllLines(textFile);

				//loop through each line (string) 
				foreach (string line in lines){
					//split it into an an array of strings 
					string [] connection = line.Split(',');

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

					//check if nodes are already in board
					if (!GamePosition.Board.ContainsKey(nodeID0)) {
						GamePosition.Board.Add(nodeID0, new Node(nodeID0));
					}
					if (!GamePosition.Board.ContainsKey(nodeID1)) {
						GamePosition.Board.Add(nodeID1, new Node(nodeID1));
					}

					GamePosition.Board[nodeID0].addEdges(GamePosition.Board[nodeID1], connType);
				}
		}

	public static bool canMove (Player player) {
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

	public static HashSet<Node> getAllOtherPlayerLocations(Player currentPlayer){
		HashSet<Node> locations = new HashSet<Node>();
		foreach (Player player in GamePosition.Players.Values){
			if (player != currentPlayer){
				locations.Add(player.getLocation());
			}
		}
		return locations;
	}


	public static HashSet<TransportType> getPossibleConnectionTypes (Node current, Node destination){
		HashSet<TransportType> usableTickets = new HashSet<TransportType> ();
		foreach(TransportType type in Enum.GetValues(typeof(TransportType))){
			HashSet<Node> edges = current.getEdges(type);
			if(edges.Contains(destination))
				usableTickets.Add(type);
		}
		return usableTickets;
	}

	public static bool playerHasEnoughTickets(Player player, TransportType transportType){
		return (player.getTickets(transportType) > 0);
	}

	public static bool checkWin(Dictionary<int, Player> players, out int winningPlayerId) {
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

	public static GamePosition ApplyMove(int player, Node move, GamePosition gamePosition){

		GamePosition potentialResult = gamePosition;

		HashSet<TransportType> possibleConnection = getPossibleConnectionTypes(potentialResult.Players[player].getLocation(), move);

		foreach (TransportType type in possibleConnection) {
			if (playerHasEnoughTickets(potentialResult.Players[player], type)){
				potentialResult.Players[player].move(move, type);
				break;
			}
		}
		return potentialResult;
	}

	public static HashSet<Node> GetPossibleMoves(int player){
		Node currentLocation = GamePosition.Players[player].getLocation();
		HashSet<Node> possibleLocations = new HashSet<Node>();
		HashSet<Node> occupiedLocations = GameLogic.getAllOtherPlayerLocations(GamePosition.Players[player]);
		HashSet<Node> checkLocations;

		if(playerHasEnoughTickets(GamePosition.Players[player], TransportType.blackCard)){
			checkLocations = currentLocation.getAllEdges();
			checkLocations.ExceptWith(occupiedLocations);
			possibleLocations.Union(checkLocations);
		} else {
			foreach(TransportType type in Enum.GetValues(typeof(TransportType))){
				if(playerHasEnoughTickets(GamePosition.Players[player], type)){
					checkLocations = currentLocation.getEdges(type);
					checkLocations.ExceptWith(occupiedLocations);
					possibleLocations.Union(checkLocations);
				}
			}
		}
		return possibleLocations;
	}
}