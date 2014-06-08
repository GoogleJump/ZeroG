package zerog_scotlandyard;

import java.util.Set;
import java.util.HashSet;

/**
*	This implementation is currently designed for a 2-player game in mind. It will later be extended for more players
*/
public class MinMax{
	Player playerID;
	int depthLimit;
	int currentDepth = 0;
	
	public NodeID minMax(GamePosition game, PlayerID playerID, int depthLimit){
		this.playerID = playerID;
		this.depthLimit = depthLimit;
		return MaxMove(game);
	}
	
	public NodeID maxMove(GamePosition game){
		if(GameLogic.gameEnded(game) || depthLimitReached()){
			return game.currentMax();
		}
		else{
			NodeID potentialMove;
			NodeID bestMove = null;
			Set<NodeID> moves = generateMoves(game);
			for(NodeID move : moves){
				potentialMove = minMove(game.applyMove(move));
				if(bestMove == null || findValue(potentialMove) > findValue(bestMove)){
					bestMove = potentialMove;
				}
			}
			return bestMove;
		}
	}
	
	public NodeID minMove(GamePosition game){
		if(GameLogic.gameEnded(game) || depthLimitReached()){
			return game.currentMin();
		}
		else{
			NodeID potentialMove;
			NodeID bestMove = null;
			Set<NodeID> moves = generateMoves(game);
			for(NodeID move : moves){
				potentialMove = maxMove(game.applyMove(move));
				if(bestMove == null || findValue(potentialMove) > findValue(bestMove)){
					bestMove = potentialMove;
				}
			}
			return bestMove;
		}
	}
	
	public boolean depthLimitReached(){
		return currentDepth < depthLimit;
	}
	
	/**
	*	Will attempt to implement GamePosition class before this method
	*/
	public Set<NodeID> generateMoves(GamePosition game){
		Set<NodeID> moves = game.getPossibleMoves(playerID);
	}
	
	/**
	*	Will use a breadth-first-search to determine the distance between MrX and Detectives
	*/
	public int findValue(GamePosition move){
	}
}