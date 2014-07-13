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
	PlayerID mrX;
	Set<PlayerID> detectives;
	
	public GamePosition minMax(GamePosition game, PlayerID mrX, int depth){
		this.mrX = mrX;
		Map<PlayerID, Player> players = game.getPlayers();
		detectives = players.keySet();
		detectives.remove(mrX);
		return MaxMove(game, depth * players.size());
	}
	
	public GamePosition maxMove(GamePosition game, int depth){
		if(depth == 0 || GameLogic.gameEnded(game)){
			return game;
		}
		else{
			GamePosition potentialMove;
			GamePosition bestMove = null;
			Set<GamePosition> moves = generateMoves(game, mrX);
			for(GamePosition move : moves){
				Iterator iterator = detectives.iterator();
				potentialMove = minMove(move, iterator.next(), depth - 1);
				if(bestMove == null || findValue(potentialMove) > findValue(bestMove)){
					bestMove = potentialMove;
				}
			}
			return bestMove;
		}
	}
	
	public GamePosition minMove(GamePosition game, PlayerID detective, int depth){
		if(depth == 0 || GameLogic.gameEnded(game)){
			return game;
		}
		else{
			GamePosition potentialMove;
			GamePosition bestMove = null;
			Set<GamePosition> moves = generateMoves(game, detective);
			for(GamePosition move : moves){
				if(!iterator.hasNext()){
					potentialMove = maxMove(move, depth - 1);
				}
				else{
					potentialMove = minMove(move, iterator.next, depth - 1);
				}
				if(bestMove == null || findValue(potentialMove) < findValue(bestMove)){
					bestMove = potentialMove;
				}
			}
			return bestMove;
		}
	}
	
	public Set<GamePosition> generateMoves(GamePosition game, PlayerID playerID){
		Set<Node> moves = game.getPossibleMoves(playerID);
		Set<GamePosition> possibleMoves = new HashSet<GamePosition>();
		for(Node move : moves){
			possibleMoves.add(game.applyMove(playerID, move));
		}
		return possibleMoves;
	}
	
	public int findValue(GamePosition game){
		int totalValue = 0;
		for(Player player : game.getPlayers()){
			if(!player instanceof MrX){
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
					if(v == game.getPlayers().get(mrX)){
						return v.getValue();
					}
					pq.add(v);
				}
			}
			u.setColor(Color.BLACK);
		}
	}
}