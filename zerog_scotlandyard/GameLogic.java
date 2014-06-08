package zerog_scotlandyard;

import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public class GameLogic {
	static Map<PlayerID, Player> players = new HashMap<PlayerID, Player>();
	static Set<Node> gameBoard;
	//manually game board graph here

	public boolean canMove (Player player) {
		Set<Node> allPossibleMoveLocations = player.getLocation().getAllEdges();
		Set<Node> allOtherPlayerLocations = getAllOtherPlayerLocations(player);
		allPossibleMoveLocations.removeAll(allOtherPlayerLocations);
		boolean playerBlocked = allPossibleMoveLocations.isEmpty();

		if(playerBlocked){
			return false;
		}

		for(Node adjacentLocation : allPossibleMoveLocations){
			Set<TransportType> connectionTypes = getPossibleConnectionTypes(player.getLocation(), adjacentLocation);
			for(TransportType transportType : connectionTypes){
				if(playerHasEnoughTickets(player, transportType)){
					return true;
				}
			}
		}
		//Player does not have enough tickets
		return false;
	}


	/**
	 * If the set is empty, there is no connection. If the set contains at least one transportType, those
	 * are the tickets you can use to get to the destination.
	 */
	public Set<TransportType> getPossibleConnectionTypes(Node current, Node destination){
		Set<TransportType> usableTickets = new HashSet<TransportType>();
		for(TransportType type : TransportType.values()){
			Set<Node> edges = current.getEdges(type);
			if(edges.contains(destination)){
				usableTickets.add(type);
			}
		}
		return usableTickets;
	}

	public boolean playerHasEnoughTickets(Player player, TransportType transportType){
		return (player.tickets.get(transportType).intValue() > 0);
	}

	public Set<Node> getAllOtherPlayerLocations(Player currentPlayer){
		Set<Node> locations = new HashSet<Node>();
		for(Player player : players.values()){
			if (player != currentPlayer){
				locations.add(player.getLocation());
			}
		}
		return locations;
	}
	
	public boolean DetectiveWin (Node detectDestination, Node MrXLocation) {
		if (detectDestination = MrXlocation)
			return true;
		else
			return false;
	}
	
	public boolean MrXwin (Player[] players, int numPlayers, List<Node> moveLog){
		for (i=1; i < numPlayers, i++)	{		//i is initialized to one because numPlayers includes MrX
			if (!Players[i].canMove)
				return true;
		}
		if (MrX.moveLog.size() = MAX_MRX_MOVES)
				return true;
		else 
			return false;
	}
}
