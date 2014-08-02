using UnityEngine;
using System;
using System.Collections.Generic;
using Priority_Queue;

/**
*	This Min-Max tree assumes that the maximizing player is always Mr. X. The main methods used
*		are minMax, maxMove, and minMove which operate in a recursive-like fashion. All other
*		methods are helper methods.
*/
public class MinMax : MonoBehaviour
{
	int _mrX;
	HashSet<int> _detectives;
	public GameLogic gameLogic;

	public GamePosition minMax(GamePosition gamePosition, int mrX, int depth){
		_mrX = mrX;
		Dictionary<int, Player> players = gamePosition.Players;
		_detectives = new HashSet<int>(players.Keys);
		_detectives.Remove(_mrX);

		//don't change the gameposition of the game being played - modify a copy
		GamePosition copyOfGamePosition = new GamePosition {
			Board = new Dictionary<int, Node>(gamePosition.Board), 
			Players = new Dictionary<int, Player>(gamePosition.Players)
		};
		return MaxMove(gamePosition, depth * players.Count);
	}

	public GamePosition MaxMove(GamePosition gamePosition, int depth){
		int winningPlayerId;
		if(depth == 0 || gameLogic.winnerExists(gamePosition.Players, out winningPlayerId)){
			return gamePosition;
		} else {
			GamePosition potentialMove;
			GamePosition bestMove = null;
			HashSet<GamePosition> moves = generateMoves(_mrX, gamePosition);
			foreach(GamePosition move in moves){
				const int firstPlayerId = 1;
				potentialMove = MinMove(move, firstPlayerId, depth - 1);
				if(bestMove == null || findValue(potentialMove) > findValue(bestMove)){
					bestMove = potentialMove;
				}
			}
			return bestMove;
		}
	}

	public GamePosition MinMove(GamePosition gamePosition, int detectiveId, int depth){
		int winningPlayerId;
		if(depth == 0 || gameLogic.winnerExists(gamePosition.Players, out winningPlayerId)){
			return gamePosition;
		} else {
			GamePosition potentialMove;
			GamePosition bestMove = null;
			HashSet<GamePosition> moves = generateMoves(detectiveId, gamePosition);
			foreach(GamePosition move in moves){
				if(detectiveId == gamePosition.Players.Count){
					potentialMove = MaxMove(move, depth - 1);
				}
				else{
					potentialMove = MinMove(move, ++detectiveId, depth - 1);
				}
				if(bestMove == null || findValue(potentialMove) < findValue(bestMove)){
					bestMove = potentialMove;
				}
			}
			return bestMove;
		}
	}
		
	public HashSet<GamePosition> generateMoves(int playerID, GamePosition gamePosition){
		HashSet<Node> moves = gameLogic.GetPossibleMoves(playerID);
		HashSet<GamePosition> possibleMoves = new HashSet<GamePosition>();
		foreach(Node move in moves){
			possibleMoves.Add(gameLogic.ApplyMove(playerID, move, gamePosition));
		}
		return possibleMoves;
	}

	public int findValue(GamePosition game){
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
	public int calculateValue(GamePosition game, Player player){
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
			
		//Not all code paths return a value, should the following line be here?
		return MAX_NUMBER_OF_NODES;
	}
}