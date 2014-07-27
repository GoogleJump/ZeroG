package zerog_scotlandyard;

import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

/**
*	Provides a game state. Is mostly used for the benefit of the Min-Max tree.
*	Can possibly be merged with GameLogic?
*/
public class GamePosition{
	private Set<Node> gameboard;
	private Map<PlayerID, Player> players = new HashMap<PlayerID, Player>();
	
	public GamePosition(Set<Node> gameboard, Map<PlayerID, Player> players){
		this.gameboard = gameboard;
		this.players = players;
	}
	
	public Set<Node> getGameboard(){
		return gameboard;
	}
	
	public Map<PlayerID, Player> getPlayers(){
		return players;
	}
	
	public GamePosition applyMove(PlayerID player, Node move){
		GamePosition potentialResult = new GamePosition(gameboard, players);
		Set<TransportType> possibleConnection = getPossibleConnectionTypes(potentialResult.getPlayers().get(player).getLocation(), move);
		for(TransportType type : possibleConnection){
			if(playerHasEnoughTickets(potentialResult.getPlayers().get(player), type){
				potentialResult.getPlayers().get(playerID).move(move, type);
				break;
			}
		}
		return potentialResult;
	}
	
	public Set<Node> getPossibleMoves(PlayerID player){
		Node currentLocation = players.get(player).getLocation();
		Set<Node> possibleLocations = new HashSet<Node>();
		Set<Node> occupiedLocations = GameLogic.getAllOtherPlayerLocations(players.get(player));
		Set<Node> checkLocations;
		
		if(playerHasEnoughTickets(players.get(player), TransportType.blackCard)){
			checkLocations = currentLocation.getAllEdges();
			checkLocations.removeAll(occupiedLocations);
			possibleLocations.addAll(checkLocations);
		}
		else{
			for(TransportType type : TransportType.values()){
				if(playerHasEnoughTickets(players.get(player), type)){
					checkLocations = currentLocation.getEdges(type);
					checkLocations.removeAll(occupiedLocations);
					possibleLocations.addAll(checkLocations);
				}
			}
		}
		return possibleLocations;
	}
}