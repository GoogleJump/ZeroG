using UnityEngine;
using System;
using System.Collections.Generic;
using Priority_Queue;
using System.Linq;

/**
*	This Min-Max tree assumes that the maximizing player is always Mr. X. The main methods used
*		are minMax, maxMove, and minMove which operate in a recursive-like fashion. All other
*		methods are helper methods.
*/
using System.Text;


public static class MinMax
{
	static int _mrX;
	static HashSet<int> _detectives;


	public static GamePosition minMax(GameLogic gameLogic, int mrX, int depth){
		_mrX = mrX;
		Dictionary<int, Player> players = gameLogic.GameBoard.Players;
		_detectives = new HashSet<int>(players.Keys);
		_detectives.Remove(_mrX);


		//don't change the gameposition of the game being played - modify a copy
		GamePosition copyOfGamePosition = new GamePosition (gameLogic.GameBoard.Players);
		GamePosition bestMove = MaxMove(gameLogic, copyOfGamePosition, depth * players.Count + 1);

		List<int> moves = new List<int> ();
		GamePosition nextMove = bestMove;
		while (nextMove.Parent != null) {
			moves.Add(nextMove.Players[0].Location.Id);
			nextMove = nextMove.Parent;
		}

		StringBuilder movesString = new StringBuilder();
		foreach (var move in moves) {
			movesString.Append(move + " ");
		}
		Debug.Log ("moves: " + movesString);
		Debug.Log ("best move: " + nextMove.Players[0].Location.Id);
		return nextMove;
	}

	public static GamePosition MaxMove(GameLogic gameLogic, GamePosition gamePosition, int depth){
		int winningPlayerId;
		if(depth == 0 || gameLogic.winnerExists(gamePosition.Players, out winningPlayerId)){
			return gamePosition;
		} else {
			GamePosition potentialMove;
			GamePosition bestMove = null;
			HashSet<GamePosition> moves = generateMoves(gameLogic, _mrX, gamePosition);
			foreach(GamePosition move in moves){
				const int firstPlayerId = 1;
				potentialMove = MinMove(gameLogic, move, firstPlayerId, depth - 1);
				if(bestMove == null || findValue(potentialMove) > findValue(bestMove)){
					bestMove = potentialMove;
				}
			}

			bestMove.Parent = gamePosition;
			return bestMove;
		}
	}

	public static GamePosition MinMove(GameLogic gameLogic, GamePosition gamePosition, int detectiveId, int depth){
		int winningPlayerId;
		if(depth == 0 || gameLogic.winnerExists(gamePosition.Players, out winningPlayerId)){
			return gamePosition;
		} else {
			GamePosition potentialMove;
			GamePosition bestMove = null;
			HashSet<GamePosition> moves = generateMoves(gameLogic, detectiveId, gamePosition);
			foreach(GamePosition move in moves){
				if(detectiveId == gamePosition.Players.Count - 1){
					potentialMove = MaxMove(gameLogic, move, depth - 1);
				}
				else{
					potentialMove = MinMove(gameLogic, move, detectiveId+1, depth - 1);
				}
				if(bestMove == null || findValue(potentialMove) < findValue(bestMove)){
					bestMove = potentialMove;
				}
			}
			return bestMove;
		}
	}
		
	public static HashSet<GamePosition> generateMoves(GameLogic gameLogic, int playerID, GamePosition gamePosition){
		HashSet<Node> moves = gameLogic.GetPossibleMoves(playerID, gamePosition);
		HashSet<GamePosition> possibleMoves = new HashSet<GamePosition>();
		foreach(Node move in moves){
			possibleMoves.Add(gameLogic.ApplyMove(playerID, move, gamePosition));
		}
		return possibleMoves;
	}

	public static int findValue(GamePosition game){
		int totalValue = 0;
		foreach(Player player in game.Players.Values){
			if(!(player is MrX)){
				totalValue += calculateValue(game, player);
			}
		}
		return totalValue;
	}

	/**
	*	Uses a breadth-first-search to calculate the distance between a given Detective and Mr X.
	*	Only considers the absolute distance between the Detective and Mr. X. Ticket amounts are
	*		not accounted for.
	*/
	public static int calculateValue(GamePosition game, Player player){

		int mrXId = 0;
		var nodes = new HashSet<Node>(game.Board.Values);
		foreach(Node node in nodes){
			node.Color = Color.white;
			node.Value = Int32.MaxValue;
			node.Parent = null;
		}
		player.getLocation().Color = Color.gray;
		player.getLocation().Value = 0;
		player.getLocation().Parent = null;

		int MAX_NUMBER_OF_NODES = 200;
		HeapPriorityQueue<Node> pq = new HeapPriorityQueue<Node>(MAX_NUMBER_OF_NODES);
		Node playerLocation = player.getLocation();
		pq.Enqueue(playerLocation, playerLocation.Value);
		while(pq.First != null){
			Node u = pq.Dequeue();
			foreach(Node v in u.getAllEdges()){
				if(v.Color == Color.white){
					v.Color = Color.gray;
					v.Value = u.Value + 1;
					v.Parent = u;
					if(v == game.Players[mrXId].getLocation()){
						return v.Value;
					}
					pq.Enqueue(v, v.Value);
				}
			}
			u.Color = Color.black;
		}
		throw new Exception ("calculate value error!!!!!!!");
		//Not all code paths return a value, should the following line be here?
		return MAX_NUMBER_OF_NODES;
	}
}