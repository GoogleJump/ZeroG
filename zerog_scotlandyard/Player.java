package zerog_scotlandyard;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

public class Player {
	protected String name;
	protected int id;
	protected Map<TransportType, Integer> tickets;
	protected Node location;
	protected List<String> moveLog;

	public Player(String name, int id){
		this.name = name;
		this.id = id;
		this.moveLog = new LinkedList<String>();
		this.tickets = new HashMap<TransportType, Integer>();
	}

	//This method is only called if the move is valid
	//GameController class will check if there are enough tickets
	public void move(Node n, TransportType ticket){
		int numOfTickets = tickets.get(ticket).intValue();
		tickets.put(ticket, --numOfTickets);
		setLocation(n);
	}

	public Node getLocation(){
		return location;
	}

	public void setLocation(Node location){
		this.location = location;
		moveLog.add(location.toString());
	}

	public int getTickets(TransportType ticket){
		return tickets.get(ticket).intValue();
	}
}
