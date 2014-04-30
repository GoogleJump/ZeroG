import java.util.*;

public class Player {
	private String name;
	private int id;
	private int[] tickets;
	private Node location;
	private LinkedList<Node> moveLog;
	
	public Player(String name, int id){
		this.name = name;
		this.id = id;
		this.moveLog = new LinkedList<String>();
	}
	
	public boolean move(Node n, TransportType Type){
		switch (Type) {
		case taxi:
			this.tickets[0]--;
			break;
		case bus:
			this.tickets[1]--;
			break;
		case underground:
			this.tickets[2]--;
		setLocation(n);
		}
		
	}
	
	public Node getLocation(){
		return location;
	}
	
	public void setLocation(Node location){
		this.location = location;
		moveLog.add(location);
	}
}
