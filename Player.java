import java.util.*;

public class Player {
	protected String name;
	protected int id;
	protected int[] tickets;
	protected Node location;
	protected List<Node> moveLog;
	
	public Player(String name, int id){
		this.name = name;
		this.id = id;
		this.moveLog = new LinkedList<String>();
	}
	
	//This method is only called if the move is valid
	//GameController class will check if there are enough tickets
	public boolean move(Node n, TransportType ticket){
		switch (ticket) {
		case taxi:
			tickets[0]--;
			break;
		case bus:
			tickets[1]--;
			break;
		case underground:
			tickets[2]--;
			break;
		}
		setLocation(n);
	}
	
	public Node getLocation(){
		return location;
	}
	
	public void setLocation(Node location){
		this.location = location;
		moveLog.add(location);
	}
}