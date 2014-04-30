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
	
	public boolean move(Node n, TransportType Type){
		switch (Type) {
		case taxi:
			if (this.tickets[0] == 0)
				move = false;
			else
				this.tickets[0]--;
			break;
		case bus:
			if (this.tickets[1]= 0)
				move = false;
			else
				this.tickets[1]--;
			break;
		case underground:
			if (this.tickets[2] == 0)
				move = false;
			else
				this.tickets[2]--;
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
