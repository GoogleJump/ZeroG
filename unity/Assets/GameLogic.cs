using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Has



public class GameLogic {

	const static int MAX_MRX_MOVES = 24;
	static Dictionary<PlayerID, Player> players = new Dictionary<PlayerID, Player>();
	static Dictionary<int, Node> board =  new Dictionary<int,Node>();

	//public HashSet<Node> gameBoard;\\


	public GameLogic(){

		//gameBoard = new HashSet<Node> ();\\

		//read in each line into a string in an array of strings
		BuildMap("BoardNodes.txt");			
	}


/*	public void initialize(){
		gameBoard.Add ((new Node (81)));
	}
*/


	public static void BuildMap(string textFile){
		//create array of strings to hold lines of text file
		string [] lines = File.ReadAllLines (textFile);

		//loop through each line (string) 
		foreach (string line in lines) {
			//split it into an an array of strings 
			string [] connection = line.Split (',');

			//create new nodes
			Node node0 = Node(parseInt(connection[0]));
			Node node1 = Node(parseInt(connection[1]));

			//add edges to node
			node0.addEdges(node1, connection[2]);
			node1.addEdges(node0, connection[2]);

			//add nodes to board
			board.Add(parseInt(connection[0]), node0);
			board.Add(parseInt(connection[1]), node1);
			//ASK ABOUT REPEATED NODES 

			//	board.get(Integer.parseInt(line[0]).line(board.get(Integer.parseInt(line[1])), taxi);
			//	board.get(Integer.parseInt(line[1]).line(board.get(Integer.parseInt(line[0])), taxi);
			}
		}

	public static bool canMove (Player player) {
		HashSet<Node> allPossibleMoveLocations = new HashSet<Node>();
		HashSet<Node> allOtherPlayerLocations = new HashSet<Node>();

		allPossibleMoveLocations = player.getLocation().getAllEdges();
		allPossibleMoveLocations.ExceptWith(getAllOtherPlayerLocations(player)):

		bool playerBlocked = allPossibleMoveLocations.Any();
		
		if(playerBlocked){
			return false;
		}
		foreach (Node adjacentlLocation in allPossibleMoveLocaitons){
			//create new HashSet that will hold the connection types... not sure if inside or outsidel loop.
			HashSet<TransportType> connectionTypes = getPossibleConnectionTypes(player.getLocation(), adjecentLocation);
			foreach(TransportType type in connectionTypes){
				if(playerHasEnoughTickets(player, transportType){
					return true;
				}
			}
		}
	//player does not have enough tickets
	return false;
	}
}