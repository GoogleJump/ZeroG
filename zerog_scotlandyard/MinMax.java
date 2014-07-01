package zerog_scotlandyard;

import java.util.Set;
import java.util.HashSet;
import java.util.PriorityQueue;

/**
*	This Min-Max tree assumes that the maximizing player is always Mr. X. The main methods used
*		are minMax, maxMove, and minMove which operate in a recursive-like fashion. All other
*		methods are helper methods.
*/
public class MinMax{
	PlayerID playerID;
	
	public GamePosition minMax(GamePosition game, PlayerID playerID, int depth){
		this.playerID = playerID;
		return MaxMove(game, depth);
	}
	
	public GamePosition maxMove(GamePosition game, int depth){
		if(depth == 0 || GameLogic.gameEnded(game)){
			return game;
		}
		else{
			GamePosition potentialMove;
			GamePosition bestMove = null;
			Set<GamePosition> moves = generateMrXMoves(game);
			for(GamePosition move : moves){
				potentialMove = minMove(move, depth - 1);
				if(bestMove == null || findValue(potentialMove) > findValue(bestMove)){
					bestMove = potentialMove;
				}
			}
			return bestMove;
		}
	}
	
	public GamePosition minMove(GamePosition game, int depth){
		if(depth == 0 || GameLogic.gameEnded(game)){
			return game;
		}
		else{
			GamePosition potentialMove;
			GamePosition bestMove = null;
			Set<GamePosition> moves = generateDetectiveMoves(game);
			for(GamePosition move : moves){
				potentialMove = maxMove(move, depth - 1);
				if(bestMove == null || findValue(potentialMove) < findValue(bestMove)){
					bestMove = potentialMove;
				}
			}
			return bestMove;
		}
	}
	
	public Set<GamePosition> generateMrXMoves(GamePosition game){
		Set<Node> moves = game.getPossibleMoves(playerID);
		Set<GamePosition> possibleMoves = new HashSet<GamePosition>();
		for(Node move : moves){
			possibleMoves.add(game.applyMove(playerID, move));
		}
		return possibleMoves;
	}
	
	/**
	*	TO-DO: Figure out how to find a GamePosition for every possible move arrangement.
	*/
	public Set<GamePosition> generateDetectiveMoves(GamePosition game){
		Set<HashSet<Node>> moves = new HashSet<HashSet<Node>>();
		for(Player player : game.getPlayers()){
			if(player.getID() != playerID){
				moves.add(game.getPossibleMoves(player.getID()));
			}
		}
		Set<GamePosition> possibleMoves = new HashSet<GamePosition>();
		//implement a permutation of every possible move arrangement. (recursive?)
		return possibleMoves;
	}
	
	public Node findValue(GamePosition game){
		int totalValue = 0;
		for(Player player : game.getPlayers()){
			totalValue += calculateValue(game, player);
		}
		return totalValue;
	}
	
	/**
	*	Uses a breadth-first-search to calculate the distance between a given Detective and Mr X.
	*	Only considers the absolute distance between the Detective and Mr. X. Ticket amounts are
	*		not accounted for.
	*/
	public int calculateValue(GamePosition game, Player player){
		for(Node node : game.getGameboard()){
			node.setColor(Color.WHITE);
			node.setValue(Integer.MAX_VALUE);
			node.setParent(null);
		}
		player.getLocation.setColor(Color.GRAY);
		player.getLocation.setValue(0);
		player.getLocation.setParent(null);
		PriorityQueue<Node> pq = new PriorityQueue<Node>();
		pq.add(player.getLocation);
		while(pq.peek() != null){
			Node u = pq.poll();
			for(Node v : u.getAllEdges()){
				if(v.getColor() == Color.WHITE){
					v.setColor(Color.GRAY);
					v.setValue(u.getValue() + 1);
					v.setParent(u);
					if(v == game.getPlayers().get(playerID)){
						return v.getValue();
					}
					pq.add(v);
				}
			}
			u.setColor(Color.BLACK);
		}
	}
}