package zerog_scotlandyard;

import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public class GameLogic {
	final static int MAX_MRX_MOVES = 24;
	static Map<PlayerID, Player> players = new HashMap<PlayerID, Player>();
	static Map <NodeID, Node> board = new HashMap<NodeID, Node>();

	//read in line into a string
	public void BuildMap(string textFile){
		String line = br.readLine;
		while (line != null){
			line.split(',');	//split string, third element is transportType
			addLines(line, line[2])
		}
	}
	
	//add connections to map
	public void addLines (string line, string type){
		//use inter.parseInt() to get the integer values
		switch(type){
			case taxi:
				board.get(Integer.parseInt(line[0]).line(board.get(Integer.parseInt(line[1])), taxi);
				board.get(Integer.parseInt(line[1]).line(board.get(Integer.parseInt(line[0])), taxi);
			case bus:
				board.get(Integer.parseInt(line[0]).line(board.get(Integer.parseInt(line[1])), bus);
				board.get(Integer.parseInt(line[1]).line(board.get(Integer.parseInt(line[0])), bus);
			case underground:
				board.get(Integer.parseInt(line[0]).line(board.get(Integer.parseInt(line[1])), underground);
				board.get(Integer.parseInt(line[1]).line(board.get(Integer.parseInt(line[0])), underground);
		}
	}
	
	/*
	for (i = 0; i < numLines, i++){		//still working on it
		addLines (taxiLines[i][], Taxi);
		}
	addLines (busLines[i][], Bus);
	addLines (ugLines[i][], Underground);
	*/
	
	
	
	
	public static boolean canMove (Player player) {
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
	public static Set<TransportType> getPossibleConnectionTypes(Node current, Node destination){
		Set<TransportType> usableTickets = new HashSet<TransportType>();
		for(TransportType type : TransportType.values()){
			Set<Node> edges = current.getEdges(type);
			if(edges.contains(destination)){
				usableTickets.add(type);
			}
		}
		return usableTickets;
	}

	public static boolean playerHasEnoughTickets(Player player, TransportType transportType){
		return (player.tickets.get(transportType).intValue() > 0);
	}

	public static Set<Node> getAllOtherPlayerLocations(Player currentPlayer){
		Set<Node> locations = new HashSet<Node>();
		for(Player player : players.values()){
			if (player != currentPlayer){
				locations.add(player.getLocation());
			}
		}
		return locations;
	}
	
	
	public boolean checkWin() {
		for (Player player : players.value()) {
			if (player instanceof Detective) {
				if (player.getLocation() == players.get(0).getLocation())	//assuming Mrx "key" or PlayerID is 0
					return true;
			}
			else if (player instanceof MrX) {
				int CantMoves = 0;
				for (Player detective : players.values()) {
					if (!canMove(detective))
						CantMoves++;
				}
				if (CantMoves == (players.size()-1))
					return true;
				if (player.getMoveLogSize() == MAX_MRX_MOVES)
					return true;
			}
			else 
				throw new IllegalArgumentException();	//throw exception
		}
		return false;
	}
}
